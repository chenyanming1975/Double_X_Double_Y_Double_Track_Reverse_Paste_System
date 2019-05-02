using HalconDotNet;
using NationalInstruments.Vision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Vision
{
    public enum CodeType
    {
        Code_2D_Mat,
        Code_QR,
    }

    public class DetectCode:Detect
    {
        public DetectCode() : base()
        {
            this.Name = "条码";
            this.Type = ResultType.Code;
        }

        [Category("条码")]
        [Description("2D 条码 QR码")]
        [DisplayName("类型")]
        public CodeType CodeType
        {
            get;
            set;
        } = CodeType.Code_2D_Mat;

        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;
            if (ROI == null || image == null)
            {
                rtn.State = VisionResultState.NG;
                return rtn;
            }

            try
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
                            string code = string.Empty;
                            switch (CodeType)
                            {
                                case CodeType.Code_2D_Mat:
                                    code = "Data Matrix ECC 200";
                                    break;
                                case CodeType.Code_QR:
                                    code = "QR Code";
                                    break;
                            }

                            using (HDataCode2D code2D = new HDataCode2D(code, "default_parameters", "maximum_recognition"))
                            {
                                HTuple result = new HTuple();
                                HTuple resultString = new HTuple();
                                code2D.FindDataCode2d(reduceImage, "stop_after_result_num", 1, out result, out resultString);
                                if (resultString.Length > 0)
                                {
                                    rtn.BarCode = resultString.SArr[0];
                                    this.AddVisionResc(rtn, $"条码寻找成功:{rtn.BarCode}");
                                    rtn.State = VisionResultState.OK;
                                    image.Overlays.Default.AddText(rtn.BarCode, new PointContour(rect.Left, rect.Top), Rgb32Value.BlueColor, new OverlayTextOptions("Consolas", 128));
                                }
                                else
                                {
                                    rtn.State = VisionResultState.NG;
                                    this.AddVisionResc(rtn, $"没有找到条码");
                                }
                            }
                        }
                    }
                }
            }
            catch (HalconException ex)
            {
                this.AddVisionResc(rtn, $"没有找到条码 {ex.GetErrorMessage()}");
            }
            return rtn;
        }
    }
}
