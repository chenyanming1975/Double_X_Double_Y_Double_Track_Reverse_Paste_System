using GeneralMachine.Vision;
using GeneralMachine.Common;
using HalconDotNet;
using NationalInstruments.Vision.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.UIData
{
    /// <summary>
    /// UI 助手 帮助获得图像
    /// </summary>
    public class UIDataHelper : SingletionProvider<UIDataHelper>
    {
        public ImageViewer Viewer = null;

        private HImage image = new HImage();
        public HImage Image
        {
            get
            {
                return Image;   
            }

            set
            {
                this.image = value;
            }
        }

        public HObject ROI
        {
            get
            {
                return new HObject();
            }
            set { }
        }
    }
}
