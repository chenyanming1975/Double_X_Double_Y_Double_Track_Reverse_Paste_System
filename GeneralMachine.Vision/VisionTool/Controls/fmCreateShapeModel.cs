using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using NationalInstruments.Vision;
using System.Diagnostics;

namespace GeneralMachine.Vision
{
    public partial class fmCreateShapeModel : Form
    {
        public fmCreateShapeModel(DetectShapeMatch parent)
        {
            this.DeParent = parent;
            InitializeComponent();
        }

        private DetectShapeMatch DeParent = null;

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                this.image?.Dispose();
                this.EdgesRegion?.Dispose();
                this.IgnoreRegion?.Dispose();
                this.Edges?.Dispose();
                this.ShapeModel?.Dispose();
            }
            catch { }
            base.OnClosed(e);
        }

        /// <summary>
        /// 轮廓区域
        /// </summary>
        private HRegion EdgesRegion = null;

        /// <summary>
        /// 忽略区域
        /// </summary>
        private HRegion IgnoreRegion = null;

        /// <summary>
        /// 模板对象
        /// </summary>
        public HShapeModel ShapeModel = null;

        private HXLDCont Edges = null;

        /// <summary>
        /// 图片
        /// </summary>
        private HImage image = null;

        /// <summary>
        /// 设置图片
        /// </summary>
        public HImage Image
        {
            get
            {
                return image;
            }

            set
            {
                HImage oldImage = this.image;
                this.image = value;
                oldImage?.Dispose();
                this.RefreshBackgroundImage();
            }
        }

        /// <summary>
        /// 适中显示图像大小
        /// </summary>
        public void FitImage()
        {
            HTuple width, height;
            this.image.GetImageSize(out width, out height);
            this.hWindows.HalconWindow.SetPart(0, 0, height - 1, width - 1);
        }

        /// <summary>
        /// 设置图像并显示图像到窗体
        /// </summary>
        /// <param name="image"></param>
        public void SetImage(HImage image)
        {
            this.image?.Dispose();
            this.image = null;
            this.image = image;
            if (this.image != null)
            {
                int width = 0, height;
                this.image.GetImageSize(out width, out height);
                this.hWindows.HalconWindow.SetPart(0, 0, height-1, width-1);
                this.hWindows.HalconWindow.AttachBackgroundToWindow(this.image);
            }
        }
        /// <summary>
        /// 刷新图像
        /// </summary>
        private void RefreshBackgroundImage()
        {
            try
            {
                if (image == null)
                {
                    this.hWindows.HalconWindow.DetachBackgroundFromWindow();
                }
                else
                {
                    this.hWindows.HalconWindow.AttachBackgroundToWindow(image);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void fmCreateShapeModel_Load(object sender, EventArgs e)
        {
        }

        private void bReadFromFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.SetImage(new HImage(dialog.FileName));
                }
            }
        }

        private void bDrawFind_Click(object sender, EventArgs e)
        {
            this.hWindows.HalconWindow.SetDraw("margin");
            this.hWindows.HalconWindow.SetColor("blue");

            this.EdgesRegion = this.hWindows.HalconWindow.DrawRegion();
            this.hWindows.HalconWindow.DispRegion(this.EdgesRegion);
        }

        private void bDrawE_Click(object sender, EventArgs e)
        {
            this.hWindows.HalconWindow.SetDraw("margin");
            this.hWindows.HalconWindow.SetColor("yellow");
            this.IgnoreRegion = this.hWindows.HalconWindow.DrawRegion();
            this.hWindows.HalconWindow.DispRegion(this.IgnoreRegion);
        }

        private void bFindShape_Click(object sender, EventArgs e)
        {
            if(this.Image != null && this.Image.IsInitialized()
                && this.EdgesRegion != null && this.EdgesRegion.IsInitialized())
            {
                if(this.IgnoreRegion != null)
                    this.EdgesRegion = this.EdgesRegion.Difference(this.IgnoreRegion);
                HImage reductImage = this.image.ReduceDomain(this.EdgesRegion);

                this.Edges = reductImage.EdgesSubPix("canny", 3, (double)this.minContrast.Value, (double)this.contrast.Value);
                reductImage.Dispose();
                this.hWindows.HalconWindow.SetDraw("margin");
                this.hWindows.HalconWindow.SetColor("red");
                this.hWindows.HalconWindow.DispXld(this.Edges);
            }
        }

        private void bCreateModel_Click(object sender, EventArgs e)
        {
            if (Edges != null)
            {
                HTuple startAngle = new HTuple((double)this.startAngle.Value).TupleRad();
                HTuple extendAngle = new HTuple((double)(this.endAngle.Value - this.startAngle.Value)).TupleRad();

                try
                {
                    this.ShapeModel = new HShapeModel
                        (this.Edges,
                        "auto", // 金子塔级别
                        startAngle,
                        extendAngle,
                        "auto", // 角度步长
                        this.DeParent.YMinScale,
                        this.DeParent.YMaxScale,
                        "auto", // X放大步长
                        this.DeParent.XMinScale,
                        this.DeParent.XMaxScale,
                        "auto", // Y放大步长
                        this.cbOpmizition.Text,
                        this.cbProtiy.Text,
                        (int)this.minContrast.Value);

                    MessageBox.Show("学习模板成功!!!");
                }
                catch (HalconException ex)
                {
                    MessageBox.Show($"识别失败原因:{ex.GetErrorMessage()}");
                }
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (this.ShapeModel != null)
            {
                try
                {
                    this.DeParent.ModelAngle = double.Parse(this.lAngle.Text.Split(':')[1]);
                    this.DeParent.ShapeModel = this.ShapeModel.Clone();
                    this.DialogResult = DialogResult.Yes;
                }
                catch { }
            }
            else
                this.DialogResult = DialogResult.No;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void bClearEdges_Click(object sender, EventArgs e)
        {
            this.Edges?.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.hWindows.HalconWindow.SetColor("red");
            this.hWindows.HalconWindow.SetLineWidth(1);
            this.hWindows.HalconWindow.SetDraw("margin");
            double beginX = 0, beginY = 0, endX = 0, endY = 0;
            this.hWindows.HalconWindow.DrawLine(out beginX, out beginY, out endX, out endY);
            HTuple anglePI = 0;
            HOperatorSet.LineOrientation(beginX, beginY, endX, endY, out anglePI);
            this.hWindows.HalconWindow.DispLine(beginX, beginY, endX, endY);
            this.lAngle.Text = $"角度:{anglePI.TupleDeg().D.ToString("f3")}";
        }
    }
}
