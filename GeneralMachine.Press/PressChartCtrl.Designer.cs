namespace GeneralMachine.Press
{
    partial class PressChartCtrl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.zedPress = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zedPress
            // 
            this.zedPress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedPress.IsEnableHPan = false;
            this.zedPress.IsEnableHZoom = false;
            this.zedPress.IsEnableVPan = false;
            this.zedPress.IsEnableVZoom = false;
            this.zedPress.Location = new System.Drawing.Point(0, 0);
            this.zedPress.Margin = new System.Windows.Forms.Padding(0);
            this.zedPress.Name = "zedPress";
            this.zedPress.ScrollGrace = 0D;
            this.zedPress.ScrollMaxX = 0D;
            this.zedPress.ScrollMaxY = 0D;
            this.zedPress.ScrollMaxY2 = 0D;
            this.zedPress.ScrollMinX = 0D;
            this.zedPress.ScrollMinY = 0D;
            this.zedPress.ScrollMinY2 = 0D;
            this.zedPress.Size = new System.Drawing.Size(680, 430);
            this.zedPress.TabIndex = 4;
            this.zedPress.UseExtendedPrintDialog = true;
            // 
            // PressChartCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zedPress);
            this.Name = "PressChartCtrl";
            this.Size = new System.Drawing.Size(680, 430);
            this.Load += new System.EventHandler(this.PressChartCtrl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public ZedGraph.ZedGraphControl zedPress;
    }
}
