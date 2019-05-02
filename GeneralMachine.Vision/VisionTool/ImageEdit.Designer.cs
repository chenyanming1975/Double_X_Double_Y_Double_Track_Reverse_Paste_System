namespace GeneralMachine.Vision.VisionTool
{
    partial class ImageEdit
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageEdit));
            this.toolVisionFunc = new System.Windows.Forms.ToolStrip();
            this.toolGetPt = new System.Windows.Forms.ToolStripButton();
            this.toolFunc = new System.Windows.Forms.ToolStripLabel();
            this.tlVisionList = new System.Windows.Forms.ToolStripComboBox();
            this.toolRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolDetect = new System.Windows.Forms.ToolStripButton();
            this.imageSet = new NationalInstruments.Vision.WindowsForms.ImageViewer();
            this.toolVisionFunc.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolVisionFunc
            // 
            this.toolVisionFunc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGetPt,
            this.toolFunc,
            this.tlVisionList,
            this.toolRefresh,
            this.toolDetect});
            this.toolVisionFunc.Location = new System.Drawing.Point(0, 0);
            this.toolVisionFunc.Name = "toolVisionFunc";
            this.toolVisionFunc.Size = new System.Drawing.Size(320, 25);
            this.toolVisionFunc.TabIndex = 0;
            this.toolVisionFunc.Text = "toolStrip1";
            // 
            // toolGetPt
            // 
            this.toolGetPt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolGetPt.Image = ((System.Drawing.Image)(resources.GetObject("toolGetPt.Image")));
            this.toolGetPt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGetPt.Name = "toolGetPt";
            this.toolGetPt.Size = new System.Drawing.Size(23, 22);
            this.toolGetPt.Text = "寻找点";
            this.toolGetPt.Click += new System.EventHandler(this.toolGetPt_Click);
            // 
            // toolFunc
            // 
            this.toolFunc.Name = "toolFunc";
            this.toolFunc.Size = new System.Drawing.Size(68, 22);
            this.toolFunc.Text = "视觉方法：";
            // 
            // tlVisionList
            // 
            this.tlVisionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tlVisionList.Name = "tlVisionList";
            this.tlVisionList.Size = new System.Drawing.Size(93, 25);
            // 
            // toolRefresh
            // 
            this.toolRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolRefresh.Image")));
            this.toolRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRefresh.Name = "toolRefresh";
            this.toolRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolRefresh.Text = "刷新视觉列表";
            this.toolRefresh.Click += new System.EventHandler(this.toolRefresh_Click);
            // 
            // toolDetect
            // 
            this.toolDetect.BackColor = System.Drawing.Color.Olive;
            this.toolDetect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolDetect.Image = ((System.Drawing.Image)(resources.GetObject("toolDetect.Image")));
            this.toolDetect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDetect.Name = "toolDetect";
            this.toolDetect.Size = new System.Drawing.Size(36, 22);
            this.toolDetect.Text = "识别";
            this.toolDetect.Click += new System.EventHandler(this.toolDetect_Click);
            // 
            // imageSet
            // 
            this.imageSet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageSet.Location = new System.Drawing.Point(0, 25);
            this.imageSet.Margin = new System.Windows.Forms.Padding(0);
            this.imageSet.Name = "imageSet";
            this.imageSet.ShowImageInfo = true;
            this.imageSet.ShowToolbar = true;
            this.imageSet.Size = new System.Drawing.Size(320, 295);
            this.imageSet.TabIndex = 291;
            this.imageSet.ImageMouseDown += new System.EventHandler<NationalInstruments.Vision.WindowsForms.ImageMouseEventArgs>(this.imageSet_ImageMouseDown);
            this.imageSet.ImageMouseMove += new System.EventHandler<NationalInstruments.Vision.WindowsForms.ImageMouseEventArgs>(this.imageSet_ImageMouseMove);
            this.imageSet.SizeChanged += new System.EventHandler(this.imageSet_SizeChanged);
            // 
            // ImageEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageSet);
            this.Controls.Add(this.toolVisionFunc);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ImageEdit";
            this.Size = new System.Drawing.Size(320, 320);
            this.Resize += new System.EventHandler(this.ImageEdit_Resize);
            this.toolVisionFunc.ResumeLayout(false);
            this.toolVisionFunc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolVisionFunc;
        public NationalInstruments.Vision.WindowsForms.ImageViewer imageSet;
        private System.Windows.Forms.ToolStripButton toolGetPt;
        private System.Windows.Forms.ToolStripLabel toolFunc;
        private System.Windows.Forms.ToolStripComboBox tlVisionList;
        private System.Windows.Forms.ToolStripButton toolDetect;
        private System.Windows.Forms.ToolStripButton toolRefresh;
    }
}
