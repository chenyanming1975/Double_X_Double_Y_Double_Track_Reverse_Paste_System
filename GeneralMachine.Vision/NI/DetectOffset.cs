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
    public class DetectOffset:Detect
    {
        public DetectOffset() : base()
        {
            this.Name = "图片预处理";
            this.Type = ResultType.NULL;
        }

        /// <summary>
        /// 增益
        /// </summary>
        [Category("预处理")]
        [Description("越大越亮  1-100")]
        [DisplayName("增益")]
        public int Gain
        {
            get;
            set;
        } = 1;

        /// <summary>
        /// 偏移
        /// </summary>
        [Category("预处理")]
        [Description("越大越暗 0-1000")]
        [DisplayName("偏移")]
        public int Offset
        {
            get;
            set;
        } = 0;

        private PixelValue SetPixelValue(ImageType type, float grayConstant, byte redConstant, byte greenConstant, byte buleConstant)
        {
            PixelValue val = new PixelValue();
            switch(type)
            {
                case ImageType.U8:
                case ImageType.U16:
                case ImageType.I16:
                    val = new PixelValue(grayConstant);
                    break;
                case ImageType.Complex:
                    val = new PixelValue(new Complex());
                    break;
                case ImageType.Rgb32:
                    val = new PixelValue(new Rgb32Value(redConstant, greenConstant, buleConstant));
                    break;
                case ImageType.Hsl32:
                    val = new PixelValue(new Hsl32Value());
                    break;
            }

            return val;
        }

        public override VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null, Shape newRoi = null)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;
            try
            {
                image.Type = ImageType.U16;
                PixelValue va1 = this.SetPixelValue(image.Type, (float)this.Gain, 0, 0, 0);
                Algorithms.Multiply(image, va1, image);
                PixelValue va2 = this.SetPixelValue(image.Type, (float)this.Offset, 0, 0, 0);
                Algorithms.Subtract(image, va2, image);
                Algorithms.Cast(image, image, ImageType.U8, -1);
                this.AddVisionResc(rtn, "增强成功");
                rtn.State = VisionResultState.OK;
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
