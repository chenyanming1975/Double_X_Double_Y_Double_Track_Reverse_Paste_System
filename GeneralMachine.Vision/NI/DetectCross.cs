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
    public class DetectCross:Detect
    {
        public DetectCross():base()
        {
            this.Name = "计算交点";
            this.Type = ResultType.XY;
        }

        [Category("直线")]
        [DisplayName("直线1的步骤ID")]
        public string Line1ID
        {
            get;
            set;
        } = string.Empty;

        [Category("直线")]
        [DisplayName("直线2的步骤ID")]
        public string Line2ID
        {
            get;
            set;
        } = string.Empty;

        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;

            try
            {
                if (parent.Detects.Count > 0 && Result != null)
                {
                    if (Result.ContainsKey(this.Line1ID)
                        && Result.ContainsKey(this.Line2ID))
                    {
                        rtn.Point = Algorithms.FindIntersectionPoint(Result[this.Line1ID].Line, Result[this.Line2ID].Line);
                        image.Overlays.Default.AddPoint(rtn.Point, Rgb32Value.RedColor, new PointSymbol(PointSymbolType.Cross));
                        this.AddVisionResc(rtn,$"焦点({rtn.Point.X:n2},{rtn.Point.Y:n2})");
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
    }
}
