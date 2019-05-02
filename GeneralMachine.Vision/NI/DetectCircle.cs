using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Vision;
using NationalInstruments.Vision.Analysis;
using System.ComponentModel;

namespace GeneralMachine.Vision
{
    /// <summary>
    /// 计算圆
    /// </summary>
    public class DetectCircle : Detect
    {
        public DetectCircle() : base()
        {
            this.Name = "抓圆";
            this.Type = ResultType.XY | ResultType.Init;
        }

        public override void InitOption()
        {
            this.OptionList.Add(new DetectCurveOptions());
        }

        [Category("半径范围")]
        [Description("最小半径 最小>=3")]
        [DisplayName("最小半径")]
        public double MinRadius
        {
            get;
            set;
        } = 3;

        [Category("半径范围")]
        [Description("最大半径 最大<=500")]
        [DisplayName("最大半径")]
        public double MaxRadius
        {
            get;
            set;
        } = 500;

        /// <summary>
        /// 找圆
        /// </summary>
        /// <param name="image"></param>
        /// <param name="roi"></param>
        /// <returns></returns>
        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;
            try
            {
                var op1 = (this.OptionList[0] as DetectCurveOptions).Options;

                var roi = this.ROI.ConvertToRoi();
                if (parent != null && Result != null && !string.IsNullOrEmpty(parent.OrgCrood) && Result.ContainsKey(parent.OrgCrood))
                {
                    CoordinateSystem old = new CoordinateSystem(parent.OrgPoint);
                    CoordinateSystem newCrood = new CoordinateSystem(Result[parent.OrgCrood].Point);
                    Algorithms.TransformRoi(roi, new CoordinateTransform(old, newCrood));
                }
                else if(newRoi != null)
                {
                    roi = newRoi.ConvertToRoi();
                }

                CircleDescriptor dsec = new CircleDescriptor(new Range(this.MinRadius, this.MaxRadius));
                var circles = Algorithms.DetectCircles(image, dsec, roi, op1);

                if (circles.Count > 0)
                {
                    image.Overlays.Default.AddOval(circles[0].Circle, Rgb32Value.RedColor);
                    image.Overlays.Default.AddPoint(circles[0].Center, Rgb32Value.RedColor);
                    this.AddVisionResc(rtn, $"半径[{circles[0].Radius:N2}] 侦测到个数{circles.Count}");
                    rtn.Point = circles[0].Center;
                    rtn.State = VisionResultState.OK;
                }
                else
                {
                    this.AddVisionResc(rtn, "没有侦测到圆");
                    rtn.State = VisionResultState.NG;
                }
            }
            catch (VisionException ex)
            {
                this.AddVisionResc(rtn, ex.Message);
                rtn.State = VisionResultState.NG;
            }
            return rtn;
        }
    }
}
