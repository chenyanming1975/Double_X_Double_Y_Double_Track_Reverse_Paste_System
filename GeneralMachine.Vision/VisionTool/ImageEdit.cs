using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.Vision.Analysis;

namespace GeneralMachine.Vision.VisionTool
{
    public partial class ImageEdit : UserControl
    {
        public ImageEdit()
        {
            InitializeComponent();
            this.imageSet.ShowToolbar = true;
            this.imageSet.ToolsShown = NationalInstruments.Vision.WindowsForms.ViewerTools.All;
            this.imageSet.ZoomToFit = true;
        }

        public VisionFlow flow = new VisionFlow();
        public VisionResult DetectResult = new VisionResult();
        public Detect Detect = null;
        private void ImageEdit_Resize(object sender, EventArgs e)
        {
            this.imageSet.ToolsShown = NationalInstruments.Vision.WindowsForms.ViewerTools.All;
            this.imageSet.ShowToolbar = true;
        }

        private void imageSet_SizeChanged(object sender, EventArgs e)
        {
            this.imageSet.ToolsShown = NationalInstruments.Vision.WindowsForms.ViewerTools.All;
            this.imageSet.ShowToolbar = true;
        }

        private void imageSet_ImageMouseDown(object sender, NationalInstruments.Vision.WindowsForms.ImageMouseEventArgs e)
        {
            GetPt = false;
            //this.imageSet.Image.Overlays.Default.Clear();
            this.ImageMouseDown?.Invoke(sender, e);
        }

        public event EventHandler<NationalInstruments.Vision.WindowsForms.ImageMouseEventArgs> ImageMouseDown;

        #region 获取鼠标点击的地方
        private bool GetPt = false;
        private void toolGetPt_Click(object sender, EventArgs e)
        {
            GetPt = true;
        }

        private void imageSet_ImageMouseMove(object sender, NationalInstruments.Vision.WindowsForms.ImageMouseEventArgs e)
        {
            if (GetPt)
            {
                this.imageSet.Image.Overlays.Default.Clear();
                this.imageSet.Image.Overlays.Default.AddLine(new NationalInstruments.Vision.LineContour(
                    new NationalInstruments.Vision.PointContour(e.Point.X,0),
                    new NationalInstruments.Vision.PointContour(e.Point.X,this.imageSet.Image.Height)));
                this.imageSet.Image.Overlays.Default.AddLine(new NationalInstruments.Vision.LineContour(
                      new NationalInstruments.Vision.PointContour(0, e.Point.Y),
                      new NationalInstruments.Vision.PointContour(this.imageSet.Image.Width, e.Point.Y)));
            }
        }
        #endregion

        #region 视觉识别
        private void toolRefresh_Click(object sender, EventArgs e)
        {
            this.tlVisionList.Items.Clear();
            var test = VisionToolCtrl.GetVisionList().ToArray();
            this.tlVisionList.Items.AddRange(test);
        }

        /// <summary>
        /// 识别
        /// </summary>
        public event Action<string> VisionDetect;

        private void toolDetect_Click(object sender, EventArgs e)
        {
            VisionDetect?.Invoke(this.tlVisionList.Text);
        }
        #endregion
    }
}
