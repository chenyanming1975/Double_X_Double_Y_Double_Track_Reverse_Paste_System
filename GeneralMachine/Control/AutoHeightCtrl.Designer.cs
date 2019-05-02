namespace GeneralMachine
{
    partial class AutoHeightCtrl
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
            this.label1 = new System.Windows.Forms.Label();
            this.numLimitHeight = new System.Windows.Forms.NumericUpDown();
            this.cbUseVaccum = new System.Windows.Forms.CheckBox();
            this.cbUsePress = new System.Windows.Forms.CheckBox();
            this.numLimitPress = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.bStart = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numLimitHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLimitPress)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(23, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "对高极限高度:";
            // 
            // numLimitHeight
            // 
            this.numLimitHeight.DecimalPlaces = 2;
            this.numLimitHeight.Location = new System.Drawing.Point(139, 17);
            this.numLimitHeight.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numLimitHeight.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            -2147483648});
            this.numLimitHeight.Name = "numLimitHeight";
            this.numLimitHeight.Size = new System.Drawing.Size(120, 26);
            this.numLimitHeight.TabIndex = 1;
            // 
            // cbUseVaccum
            // 
            this.cbUseVaccum.AutoSize = true;
            this.cbUseVaccum.Checked = true;
            this.cbUseVaccum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseVaccum.Location = new System.Drawing.Point(24, 52);
            this.cbUseVaccum.Name = "cbUseVaccum";
            this.cbUseVaccum.Size = new System.Drawing.Size(112, 24);
            this.cbUseVaccum.TabIndex = 2;
            this.cbUseVaccum.Text = "使用真空对高";
            this.cbUseVaccum.UseVisualStyleBackColor = true;
            // 
            // cbUsePress
            // 
            this.cbUsePress.AutoSize = true;
            this.cbUsePress.Location = new System.Drawing.Point(24, 82);
            this.cbUsePress.Name = "cbUsePress";
            this.cbUsePress.Size = new System.Drawing.Size(112, 24);
            this.cbUsePress.TabIndex = 3;
            this.cbUsePress.Text = "使用压力对高";
            this.cbUsePress.UseVisualStyleBackColor = true;
            this.cbUsePress.CheckedChanged += new System.EventHandler(this.cbUsePress_CheckedChanged);
            // 
            // numLimitPress
            // 
            this.numLimitPress.Location = new System.Drawing.Point(139, 81);
            this.numLimitPress.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.numLimitPress.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numLimitPress.Name = "numLimitPress";
            this.numLimitPress.Size = new System.Drawing.Size(120, 26);
            this.numLimitPress.TabIndex = 4;
            this.numLimitPress.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numLimitPress.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "g";
            // 
            // bStart
            // 
            this.bStart.BackColor = System.Drawing.Color.Yellow;
            this.bStart.Location = new System.Drawing.Point(24, 117);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(93, 31);
            this.bStart.TabIndex = 6;
            this.bStart.Text = "开始对高";
            this.bStart.UseVisualStyleBackColor = false;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bCancel
            // 
            this.bCancel.BackColor = System.Drawing.Color.White;
            this.bCancel.Location = new System.Drawing.Point(269, 117);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(93, 31);
            this.bCancel.TabIndex = 7;
            this.bCancel.Text = "取消";
            this.bCancel.UseVisualStyleBackColor = false;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // AutoHeightCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(369, 156);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numLimitPress);
            this.Controls.Add(this.cbUsePress);
            this.Controls.Add(this.cbUseVaccum);
            this.Controls.Add(this.numLimitHeight);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AutoHeightCtrl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.AutoHeightCtrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numLimitHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLimitPress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numLimitHeight;
        private System.Windows.Forms.CheckBox cbUseVaccum;
        private System.Windows.Forms.CheckBox cbUsePress;
        private System.Windows.Forms.NumericUpDown numLimitPress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bCancel;
    }
}
