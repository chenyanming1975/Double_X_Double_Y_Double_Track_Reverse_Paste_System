using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Vision;
using NationalInstruments.Vision.Analysis;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Newtonsoft.Json;

namespace GeneralMachine.Vision
{
    public class DetectGeometric : Detect,IDisposable
    {
        public static object locked = new object();

        public DetectGeometric() : base()
        {
            this.Name = "形状匹配";
            DetectGeometric.Init();
            this.Type = ResultType.XY | ResultType.Angle | ResultType.Init;
        }

        public override void InitOption()
        {
            this.OptionList.Add(new DetectCurveOptions());
        }

        /// <summary>
        /// 最小分数
        /// </summary>
        [Category("参数")]
        [Description("最小匹配分数,低于该分数不输出")]
        [DisplayName("匹配分数")]
        public float MinScore
        {
            get;
            set;
        } = 600;

        /// <summary>
        /// 模板
        /// </summary>
        [Browsable(false)]
        [JsonIgnore]
        public VisionImage Temp
        {
            get;
            set;
        } = new VisionImage();

        public override bool Lerarn(VisionImage image, Roi roi)
        {
            if (roi.Count == 0)
                return false;
            bool rtn = false;
            try
            {
                Algorithms.Extract(image, this.Temp, roi);
                RotationAngleRange range = new RotationAngleRange();
                var op1 = (this.OptionList[0] as DetectCurveOptions).Options;

                Algorithms.LearnGeometricPatternEdgeBased(this.Temp, new PointContour(), 0, op1, LearnOptions);
                rtn = true;
            }
            catch(VisionException ex)
            {
                Debug.WriteLine(ex.VisionErrorText);
            }
            return rtn;
        }


        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;
            try
            {
                var op1 = (this.OptionList[0] as DetectCurveOptions).Options;

                var roi = this.ROI;
                if (newRoi != null)
                    roi = newRoi;

                Collection<GeometricEdgeBasedPatternMatch> report = null;
                lock (locked)
                {
                    report = Algorithms.MatchGeometricPatternEdgeBased(image, this.Temp, op1, DetectGeometric.MatchOptions, roi.ConvertToRoi());
                }

                if (report.Count > 0)
                {
                    report[0].Rotation = this.AdjuestAngle(report[0].Rotation);
                    rtn.Point = report[0].Position;
                    rtn.Angle = report[0].Rotation;

                    image.Overlays.Default.AddPoint(rtn.Point, Rgb32Value.RedColor, new PointSymbol(PointSymbolType.Cross));

                    foreach (var match in report)
                    {
                        image.Overlays.Default.AddPolygon(new PolygonContour(match.Corners), Rgb32Value.BlueColor);
                    }

                    this.AddVisionResc(rtn, $"边缘匹配成功 角度:{rtn.Angle}");
                    rtn.State = VisionResultState.OK;
                }
                else
                {
                    this.AddVisionResc(rtn, "边缘匹配失败");
                    rtn.State = VisionResultState.NG;
                }
            }
            catch (Exception ex)
            {
                this.AddVisionResc(rtn, ex.Message);

                rtn.State = VisionResultState.NG;
            }

            return rtn;
        }

        public static MatchGeometricPatternEdgeBasedOptions MatchOptions = null;

        /// <summary>
        /// 学习基于几何图形边缘的高级选项
        /// </summary>
        public static LearnGeometricPatternEdgeBasedAdvancedOptions LearnOptions = null;
        
        public static void Init()
        {
            if(MatchOptions == null)
            {
                MatchOptions = new MatchGeometricPatternEdgeBasedOptions();

                //[Description("Original:原始 Reversed:反转 Both:全")]
                //[DisplayName("对比模式")]
                MatchOptions.Advanced.ContrastMode = ContrastMode.Original;
                //[Description("Balanced:平衡 Conservative:保守 Aggressive进取")]
                //[DisplayName("几何匹配搜索策略")]
                MatchOptions.Advanced.MatchStrategy = GeometricMatchingSearchStrategy.Balanced;
                MatchOptions.MinimumMatchScore = 500;

                /*
                 匹配策略
                 角度范围 0-360
                 缩放范围 90-110
                 遮挡范围 0-25
                 */
                MatchOptions.Mode = GeometricMatchModes.RotationInvariant | GeometricMatchModes.ScaleInvariant | GeometricMatchModes.OcclusionInvariant;
                MatchOptions.NumberOfMatchesRequested = 1;
                MatchOptions.RotationAngleRanges.Add(new Range(-20, 20));
                MatchOptions.ScaleRange = new Range(90,110);
                MatchOptions.OcclusionRange = new Range(0, 25);

                // 压像素精度
                MatchOptions.SubpixelAccuracy = true;
            }

            if(LearnOptions == null)
            {
                LearnOptions = new LearnGeometricPatternEdgeBasedAdvancedOptions();
                LearnOptions.ImageSamplingFactor = 2; // 图像采样因子
                LearnOptions.RotationAngleRange = new Range(0, 360); // 设置模板的旋转角度范围
                LearnOptions.ScaleRange = new Range(90, 110); // 设置模板的缩放范围
            }
        }

        public void Dispose()
        {
            this.Temp?.Dispose();
        }
    }
}
