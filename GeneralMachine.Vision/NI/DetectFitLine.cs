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
    public class DetectFitLine:Detect
    {
        public DetectFitLine():base()
        {
            this.Name = "拟合直线";
            this.Type = ResultType.Angle;
        }

        /// <summary>
        /// 起点
        /// </summary>

        [Category("点位")]
        [DisplayName("起点步骤ID")]
        public string StartPt { get; set; } = Detect.Defatlu;

        /// <summary>
        /// 终点
        /// </summary>
        [Category("点位")]
        [DisplayName("终点步骤ID")]
        public string EndPt { get; set; } = Detect.Defatlu;

        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;
            if (parent != null && Result != null)
            {
                if (Result.ContainsKey(this.StartPt) && Result.ContainsKey(this.EndPt))
                {
                    rtn.Line = new LineContour(Result[this.StartPt].Point, Result[this.EndPt].Point);
                    image.Overlays.Default.AddLine(rtn.Line, Rgb32Value.GreenColor);
                    rtn.Angle = Common.MathHelper.GetAngle(rtn.Line.Start.X, rtn.Line.Start.Y, rtn.Line.End.X, rtn.Line.End.Y);
                }

                this.AddVisionResc(rtn, "拟合直线成功");
                rtn.State = VisionResultState.OK;
            }
            else
            {
                this.AddVisionResc(rtn, "拟合直线失败");
                rtn.State = VisionResultState.NG;
            }

            return rtn;
        }
    }
}
