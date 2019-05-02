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
    public class DetectLine : Detect
    {
        public DetectLine() : base()
        {
            this.Name = "抓边";
            this.Type = ResultType.Angle;
        }

        public override void InitOption()
        {
            this.OptionList.Add(new DetectEdgeOptions());
            this.OptionList.Add(new DetectStraightEdgeOptions());
        }

        [Category("参数")]
        [Description("默认false")]
        [DisplayName("是否启用优化")]
        public bool OptimizedMode
        {
            get;
            set;
        } = false;

        [Category("参数")]
        [Description("与线段垂直查找")]
        [DisplayName("寻找放向")]
        public SearchDirection SearchDir
        {
            get;
            set;
        } = SearchDirection.LeftToRight;

        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            try
            {
                var roi = this.ROI.ConvertToRoi();
                if (parent != null && Result != null && !string.IsNullOrEmpty(parent.OrgCrood) && Result.ContainsKey(parent.OrgCrood))
                {
                    CoordinateSystem old = new CoordinateSystem(parent.OrgPoint, parent.BaseAngle);
                    CoordinateSystem newCrood = new CoordinateSystem(Result[parent.OrgCrood].Point, Result[parent.OrgCrood].Angle);
                    Algorithms.TransformRoi(roi, new CoordinateTransform(old, newCrood));
                }

                var op2 = (this.OptionList[0] as DetectEdgeOptions).Options;
                var op1 = (this.OptionList[1] as DetectStraightEdgeOptions).Options;

                StraightEdgeReport report = Algorithms.StraightEdge2(image, roi, this.SearchDir, op2, op1, this.OptimizedMode);

                image.Overlays.Default.AddRectangle(roi.GetBoundingRectangle(), Rgb32Value.BlueColor);
                if (report.StraightEdges.Count > 0)
                {
                    rtn.Line = report.StraightEdges[0].StraightEdge;
                    image.Overlays.Default.AddLine(rtn.Line, Rgb32Value.YellowColor);
                    rtn.Angle = Common.MathHelper.GetAngle(rtn.Line.Start.X, rtn.Line.Start.Y, rtn.Line.End.X, rtn.Line.End.Y);
                    
                    if (this.SearchDir == SearchDirection.LeftToRight || this.SearchDir == SearchDirection.RightToLeft)
                    {
                        if (rtn.Angle > 0)
                        {
                            rtn.Angle = 90 - rtn.Angle;
                        }
                        else if (rtn.Angle < 0)
                        {
                            rtn.Angle = 90 + rtn.Angle;
                        }
                    }
                    else
                    {
                        rtn.Angle = -rtn.Angle;
                    }

                    this.AddVisionResc(rtn, $"侦测直线成功 角度:{rtn.Angle}");
                    rtn.State = VisionResultState.OK;
                }
                else
                {
                    this.AddVisionResc(rtn, "侦测直线失败");
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
    }
}
