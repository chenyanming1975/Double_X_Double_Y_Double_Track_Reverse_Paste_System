using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Vision;
using System.ComponentModel;
using HalconDotNet;
using Newtonsoft.Json;
using NationalInstruments.Vision.Analysis;
using System.Threading;

namespace GeneralMachine.Vision
{
    /// <summary>
    /// 计算精度模式
    /// </summary>
    public enum SubPixel
    {
        none,
        interpolation,
        least_squares,
        least_squares_high,
        least_squares_very_high,
    }

    public class DetectShapeMatch : Detect,IDisposable
    {
        public DetectShapeMatch() : base()
        {
            this.Name = "轮廓匹配";
            this.Type = ResultType.XY | ResultType.Init | ResultType.Angle;
        }

        //参数AngleStart、AngleExtent定义了模板可能发生旋转的范围。注意模板在find_shape_model函数中只能找到这个范围内的匹配。参数AngleStep定义了旋转角度范围内的步长。 如果在find_shape_model函数中没有指定亚像素精度，这个参数指定的精度是可以实现find_shape_mode函数中的角度的。参数AngleStep的选择是基于目标的大小的，如果模板图像太小不能产生许多不同离散角度的图像，因此对于较小的模板图像AngleStep应该设置的比较大。如果AngleExtent不是AngleStep的整数倍, 将会相应的修改AngleStep。
        /// <summary>
        /// 搜寻的起始角度
        /// </summary>
        [Category("搜索角度")]
        [Description("搜索的模板最小角度")]
        [DisplayName("起始角度")]
        public int StartAngle
        {
            get;
            set;
        } = -45;

        /// <summary>
        /// 搜寻的角度范围
        /// </summary>
        [Category("搜索角度")]
        [Description("搜索的模板最大角度")]
        [DisplayName("终止角度")]
        public int EndAngle
        {
            get;
            set;
        } = 45;

        [Category("X方向可变系数")]
        [Description("1为与模板等比")]
        [DisplayName("最小尺寸系数")]
        public double XMinScale
        {
            get;
            set;
        } = 0.8;

        [Category("X方向可变系数")]
        [Description("1为与模板等比")]
        [DisplayName("最大尺寸系数")]
        public double XMaxScale
        {
            get;
            set;
        } = 1.2;

        [Category("Y方向可变系数")]
        [Description("1为与模板等比")]
        [DisplayName("最小尺寸系数")]
        public double YMinScale
        {
            get;
            set;
        } = 0.8;

        [Category("Y方向可变系数")]
        [Description("1=与模板等比")]
        [DisplayName("最大尺寸系数")]
        public double YMaxScale
        {
            get;
            set;
        } = 1.2;

        /// <summary>
        /// 最小匹配分数
        /// </summary>
        [Category("匹配")]
        [Description("1=完全匹配")]
        [DisplayName("最小匹配分数")]
        public double MinScore
        {
            get;
            set;
        } = 0.6;

        /// <summary>
        /// 模糊搜索级别
        /// </summary>
        [Category("图片模糊化搜索")]
        [Description("1=不模糊 5=模糊化高")]
        [DisplayName("最小模糊度")]
        public int MinNumLevels
        {
            get;
            set;
        } = 1;

        /// <summary>
        /// 模糊搜索级别
        /// </summary>
        [Category("图片模糊化搜索")]
        [Description("1=不模糊 5=模糊化高")]
        [DisplayName("最大模糊度")]
        public int MaxNumLevels
        {
            get;
            set;
        } = 3;

        /// <summary>
        /// 重叠比率
        /// </summary>
        [Category("匹配")]
        [Description("0不重叠  1完全重叠")]
        [DisplayName("可重叠比率")]
        public double MaxOverlap
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 贪心程度 贪婪度，搜索启发式，一般都设为0.9，越高速度快 容易出现找不到的情况
        /// </summary>
        [Category("匹配")]
        [Description("0:保守 1:激进 一般都设为0.9，越高速度快 容易出现找不到的情况")]
        [DisplayName("贪心程度")]
        public double Greediness
        {
            get;
            set;
        } = 0.9;

        [Category("高级参数")]
        [Description("")]
        [DisplayName("像素精度")]
        public SubPixel Pixel
        {
            get;
            set;
        } = SubPixel.least_squares;

        [Category("搜索角度")]
        [Description("做模板时的角度")]
        [DisplayName("模板角度")]
        public double ModelAngle
        {
            get;
            set;
        } = 0;

        [JsonIgnore]
        [Browsable(false)]
        public HShapeModel ShapeModel = new HShapeModel();

        public override bool Lerarn(VisionImage image, Roi roi)
        {
            using (VisionImage temp = new VisionImage())
            {
                Algorithms.Extract(image, temp, roi);
                fmCreateShapeModel fm = new fmCreateShapeModel(this);
                temp.WriteBmpFile("D://h2NI.bmp");
                Thread.Sleep(20);
                fm.SetImage(new HImage("D://h2NI.bmp"));
                if (fm.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    return true;
                }
            }
            return false;
        }

        public static object locked = new object();

        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;

            try
            {
                lock (locked)
                {
                    using (VisionImage temp = new VisionImage())
                    {
                        var roi = this.ROI;
                        if (newRoi != null)
                            roi = newRoi;

                        using (HImage hImage = VisionHelper.Image(image))
                        {
                            RectangleContour rect = roi as RectangleContour;

                            using (HImage reduceImage = hImage.ReduceDomain(
                                new HRegion(rect.Top, rect.Left,
                                rect.Top + rect.Height,
                                rect.Left + rect.Width)))
                            {
                                HTuple numLevels = new HTuple();
                                numLevels.TupleAdd(this.MaxNumLevels);
                                numLevels.TupleAdd(this.MinNumLevels);

                                HTuple row, col, scaleX, scaleY, score, angle;
                                reduceImage.FindAnisoShapeModel(this.ShapeModel,
                                    this.StartAngle, this.EndAngle - this.StartAngle,
                                    this.YMinScale, this.YMaxScale,
                                    this.XMinScale, this.XMaxScale,
                                    this.MinScore, 1, this.MaxOverlap,
                                    this.Pixel.ToString(),
                                    0,
                                    this.Greediness,
                                    out row, out col, out angle, out scaleY, out scaleX, out score);

                                if (score.Length > 0)
                                {
                                    rtn.State = VisionResultState.OK;
                                    image.Overlays.Default.AddLine
                                        (new LineContour(new PointContour(col.D - 50, row.D), new PointContour(col.D + 50, row.D)), Rgb32Value.RedColor);

                                    image.Overlays.Default.AddLine
                                      (new LineContour(new PointContour(col.D, row.D - 50), new PointContour(col.D, row.D + 50)), Rgb32Value.RedColor);
                                    rtn.Point = new PointContour(col.D, row.D);
                                    rtn.Angle = angle.TupleDeg().D;
                                    this.AddVisionResc(rtn, $"轮廓匹配成功 角度:{rtn.Angle:N3} 分数:{score[0].D:N3}");
                                }
                                else
                                {
                                    rtn.State = VisionResultState.NG;
                                    this.AddVisionResc(rtn, $"没有找到匹配模板");
                                }
                            }
                        }
                    }
                }
            }
            catch(HalconException ex)
            {
                this.AddVisionResc(rtn, $"轮廓匹配失败 {ex.GetErrorMessage()}");
            }
            return rtn;
        }

        public void Dispose()
        {
            this.ShapeModel?.Dispose();
        }
    }
}
