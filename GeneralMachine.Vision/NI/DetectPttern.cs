using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Vision;
using NationalInstruments.Vision.Analysis;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GeneralMachine.Vision
{
    public class DetectPttern : Detect,IDisposable
    {
        public static object locked = new object();

        public DetectPttern() : base()
        {
            this.Name = "灰度匹配";
            DetectPttern.Init();
            this.Type = ResultType.XY | ResultType.Angle | ResultType.Init;
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("参数")]
        [Description("all:所有匹配")]
        [DisplayName("匹配方式")]
        public MatchingAlgorithm MatchingAlgorithm
        {
            get;
            set;
        } = MatchingAlgorithm.MatchGrayValuePyramid;

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
        /// 最小角度
        /// </summary>
        [Category("搜寻角度")]
        [Description("最小为0")]
        [DisplayName("最小搜寻角度")]
        public float MinAngle
        {
            get;
            set;
        } = -20;

        /// <summary>
        /// 最大角度
        /// </summary>
        [Category("搜寻角度")]
        [Description("最大360 范围约小运算时间越快")]
        [DisplayName("最大搜寻角度")]
        public float MaxAngle
        {
            get;
            set;
        } = 20;

        /// <summary>
        /// 模板
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
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
                range.Lower = 0;
                range.Upper = 360;
                Algorithms.LearnPattern2(this.Temp, null, this.MatchingAlgorithm, range);
                rtn = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return rtn;
        }

        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;
            try
            {
                Collection<RotationAngleRange> list = new Collection<RotationAngleRange>();
                list.Add(new RotationAngleRange(this.MinAngle, this.MaxAngle));
                list.Add(new RotationAngleRange(0, 0));

                var roi = this.ROI;
                if (newRoi != null)
                    roi = newRoi;
                Collection<PatternMatchReport> report = null;
                lock (locked)
                {
                    report = Algorithms.MatchPattern3(image, this.Temp, this.MatchingAlgorithm, 1, this.MinScore, list, roi.ConvertToRoi(), PatternOption);
                }

                if (report.Count > 0)
                {
                    rtn.Point = report[0].Position;
                    rtn.Angle = this.AdjuestAngle(report[0].Rotation);

                    foreach (PatternMatchReport match in report)
                    {
                        image.Overlays.Default.AddPolygon(new PolygonContour(match.Corners), Rgb32Value.BlueColor);
                    }
                    image.Overlays.Default.AddPoint(report[0].Position);
                    rtn.Point.X = report[0].Position.X;
                    rtn.Point.Y = report[0].Position.Y;
                    this.AddVisionResc(rtn, $"灰度匹配成功 角度:{rtn.Angle}");

                    rtn.State = VisionResultState.OK;
                }
                else
                {
                    this.AddVisionResc(rtn, "匹配失败");
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

        public static Collection<PMMatchAdvancedSetupDataOption> PatternOption = new Collection<PMMatchAdvancedSetupDataOption>();

        public static void Init()
        {
            if(PatternOption.Count == 0)
            {
                int[] advancedOptionsItems = { 102, 106, 107, 108, 109, 111, 112, 113, 103, 104, 105, 100 };

                double[] advancedOptionsValues = { 10, 300, 0, 6, 0, 20, 10, 20, 1, 20, 0, 4 };

                for (int i = 0; i < 12; ++i)
                {
                    PatternOption.Add(new PMMatchAdvancedSetupDataOption((MatchSetupOption)advancedOptionsItems[i], advancedOptionsValues[i]));
                }
            }
        }

        public void Dispose()
        {
            this.Temp?.Dispose();
        }
    }
}
