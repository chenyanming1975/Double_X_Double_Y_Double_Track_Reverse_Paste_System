using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Vision;
using GeneralMachine.Common;

namespace GeneralMachine.Vision
{
    public class DetectAdjustOutput:Detect
    {
        public DetectAdjustOutput() : base()
        {
            this.Name = "输出位调整";
            this.Type = ResultType.NULL;
        }

        /// <summary>
        /// 偏移X
        /// </summary>
        [Category("输出位")]
        [Description("相对视觉抓取位的偏移X")]
        [DisplayName("X偏移")]
        public int OffsetX
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 偏移Y
        /// </summary>
        [Category("输出位")]
        [Description("相对视觉抓取位的偏移X")]
        [DisplayName("Y偏移")]
        public int OffsetY
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 角度
        /// </summary>
        [Category("基准")]
        [Description("做输出偏移时参考模板的当前角度")]
        [DisplayName("基准角度")]
        public double BaseAngle
        {
            get;
            set;
        } = 0;

        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;

            try
            {
                if (parent.Detects.Count > 0 && Result != null)
                {
                    if (parent.ResultX != string.Empty
                        && parent.ResultY != string.Empty
                        && parent.Angle != string.Empty
                        && Result.ContainsKey(parent.ResultX)
                        && Result.ContainsKey(parent.ResultY)
                        && Result.ContainsKey(parent.Angle))
                    {
                        PointContour center = new PointContour(Result[parent.ResultX].Point.X, Result[parent.ResultX].Point.Y);
                        PointContour offset = new PointContour(center.X + this.OffsetX, center.Y + this.OffsetY);
                        PointContour output = new PointContour();
                        
                        this.PtRotate(offset, center, Result[parent.Angle].Angle - this.BaseAngle, out output);

                        Result[parent.ResultX].Point.X = output.X;
                        Result[parent.ResultY].Point.Y = output.Y;
                        rtn.Point = output;

                        image.Overlays.Default.AddPoint(output, Rgb32Value.YellowColor, new PointSymbol(PointSymbolType.Cross));
                        this.AddVisionResc(rtn, $"焦点({rtn.Point.X:n2},{rtn.Point.Y:n2})");
                        rtn.State = VisionResultState.OK;
                    }
                }
                else
                    rtn.State = VisionResultState.NG;
            }
            catch (Exception ex)
            {
                this.AddVisionResc(rtn, ex.Message);
                rtn.State = VisionResultState.NG;
            }

            return rtn;
        }

        /// <summary>
        /// 点绕点旋转
        /// </summary>
        /// <param name="PTtoRotate"></param>
        /// <param name="RotateCenter"></param>
        /// <param name="RotatethetaAngle"></param>
        /// <param name="PTRotated"></param>
        public void PtRotate(PointContour PTtoRotate, PointContour RotateCenter, double RotatethetaAngle, out PointContour PTRotated)//点绕点旋转算法（逆时针为正）
        {
            double deg = RotatethetaAngle / 180.0 * Math.PI;
            PTRotated = new PointContour();
            PTRotated.X = (PTtoRotate.X - RotateCenter.X) * (float)Math.Cos(deg) + (PTtoRotate.Y - RotateCenter.Y) * (float)Math.Sin(deg) + RotateCenter.X;
            PTRotated.Y = -(PTtoRotate.X - RotateCenter.X) * (float)Math.Sin(deg) + (PTtoRotate.Y - RotateCenter.Y) * (float)Math.Cos(deg) + RotateCenter.Y;
        }

    }
}
