using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Vision;
using NationalInstruments.Vision.Analysis;
using System.Collections.ObjectModel;

namespace GeneralMachine.Vision
{
    public class DetectFitCicle:Detect
    {
        public DetectFitCicle():base()
        {
            this.Name = "拟合圆";
            this.Type = ResultType.XY;
        }

        /// <summary>
        /// 点位集合
        /// </summary>

        [Category("拟合点")]
        [Description("点位单元的名称 最少需要3个单元")]
        [DisplayName("单元列表")]
        public List<string> CrossList { get; set; } = new List<string>();


        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;

            try
            {
                if (parent != null && image != null && Result != null)
                {
                    Collection<PointContour> pt = new Collection<PointContour>();

                    if (CrossList.Count >= 3)
                    {
                        for (int i = 0; i < CrossList.Count; ++i)
                        {
                            if (Result.ContainsKey(this.CrossList[i]))
                            {
                                pt.Add(Result[this.CrossList[i]].Point);
                            }
                            else
                                return rtn;
                        }

                        FitCircleReport report = Algorithms.FitCircle(pt);
                        rtn.Point = report.Center;
                        this.AddVisionResc(rtn, "拟合成功");
                        image.Overlays.Default.AddOval(report.Circle, Rgb32Value.RedColor);
                        image.Overlays.Default.AddPoint(rtn.Point, Rgb32Value.RedColor);
                        image.Overlays.Default.AddRoi(report.Circle.ConvertToRoi());
                        rtn.State = VisionResultState.OK;
                    }
                    else
                    {
                        this.AddVisionResc(rtn, "拟合点数据少于3");
                        rtn.State = VisionResultState.NG;
                    }
                }
                else
                    throw new Exception("没有找到对应结果");
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
