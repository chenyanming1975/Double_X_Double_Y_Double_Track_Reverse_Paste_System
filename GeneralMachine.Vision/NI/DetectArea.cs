using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using NationalInstruments.Vision;
using NationalInstruments.Vision.Analysis;

namespace GeneralMachine.Vision
{
    /// <summary>
    /// 面积侦测
    /// </summary>
    public class DetectArea : Detect
    {
        public DetectArea() : base()
        {
            this.Name = "面积";
            this.Type = ResultType.Area;
        }

        [Category("面积范围")]
        [DisplayName("无料时面积上限")]
        public double NoLabelArea
        {
            get;
            set;
        } = 500;

        [Category("面积范围")]
        [DisplayName("面积下限")]
        public double MinArea
        {
            get;
            set;
        } = 1000;

        [Category("面积范围")]
        [DisplayName("面积上限")]
        public double MaxArea
        {
            get;
            set;
        } = 10000;

        [Category("参数")]
        [Description("0黑 -  255白")]
        [DisplayName("最小灰度")]
        public double MinThreshold
        {
            get;
            set;
        } = 125;

        [Category("参数")]
        [Description("0黑 -  255白")]
        [DisplayName("最大灰度")]
        public double MaxThreshold
        {
            get;
            set;
        } = 255;

        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;
            try
            {
                using (HImage hImage = VisionHelper.Image(image))
                {
                    var roi = this.ROI;
                    if (newRoi != null)
                        roi = newRoi;
                    RectangleContour rect = roi as RectangleContour;
                    using (HImage reduceImage = hImage.ReduceDomain(
                                new HRegion(rect.Top, rect.Left,
                                rect.Top + rect.Height,
                                rect.Left + rect.Width)))
                    {
                        HRegion hRegion = new HRegion();
                        hRegion = reduceImage.Threshold(this.MinThreshold, this.MaxThreshold);

                        rtn.Area = hRegion.Area.D;
                        this.AddVisionResc(rtn, $"面积侦测成功 面积值:{rtn.Area:N3}");
                        rtn.State = VisionResultState.OK;
                    }
                }

                //using (VisionImage imageMask = new VisionImage(ImageType.U8, 7))
                //{
                //    using (VisionImage image2Process = new VisionImage(ImageType.U8, 7))
                //    {
                //        //Algorithms.Copy(image, image2Process);
                //        Algorithms.Threshold(image, image2Process, new Range(this.Threshold, 255), true, 255);
                //        PixelValue fillValue = new PixelValue(255);
                //        Range intervalRange = new Range(0, 0);

                //        var roi = this.ROI;
                //        if (newRoi != null)
                //            roi = newRoi;

                //        Algorithms.RoiToMask(imageMask, roi.ConvertToRoi(), fillValue, image2Process);
                //        HistogramReport a = new HistogramReport();
                //        a = Algorithms.Histogram(image2Process, 256, intervalRange, imageMask);

                //        image2Process?.Dispose();
                //        imageMask?.Dispose();
                //        if (a.Histogram.Count >= 256)
                //        {
                //            if (CheckWhite)
                //                rtn.Area = a.Histogram[255];//白色区域面积
                //            else
                //                rtn.Area = a.Histogram[0];//黑色区域面积
                //        }

                //        this.AddVisionResc(rtn, $"面积:{rtn.Area}");
                //        rtn.State = VisionResultState.OK;
                //    }
                //}
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
