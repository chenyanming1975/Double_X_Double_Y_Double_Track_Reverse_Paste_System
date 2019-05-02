namespace GeneralMachine.Cliab
{
    partial class fm_Hardware
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_Hardware));
            this.startStep1 = new WizardBase.StartStep();
            this.panel7 = new System.Windows.Forms.Panel();
            this.bClear = new System.Windows.Forms.Button();
            this.bUpdate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tXYAngle = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bCal = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bRightBottom = new System.Windows.Forms.Button();
            this.bOrg = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bSetRightTop = new System.Windows.Forms.Button();
            this.bLeftTop = new System.Windows.Forms.Button();
            this.wizardControl1 = new WizardBase.WizardControl();
            this.startStep6 = new WizardBase.StartStep();
            this.startStep2 = new WizardBase.StartStep();
            this.panel5 = new System.Windows.Forms.Panel();
            this.startStep3 = new WizardBase.StartStep();
            this.panel6 = new System.Windows.Forms.Panel();
            this.startStep4 = new WizardBase.StartStep();
            this.startStep5 = new WizardBase.StartStep();
            this.module = new GeneralMachine.BasicUI.ModuleRadio();
            this.axisOffsetItem1 = new GeneralMachine.Cliab.AxisOffsetItem();
            this.axisOffsetItem2 = new GeneralMachine.Cliab.AxisOffsetItem();
            this.startStep1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tXYAngle)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.startStep6.SuspendLayout();
            this.startStep2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.startStep3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // startStep1
            // 
            this.startStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep1.BindingImage")));
            this.startStep1.Controls.Add(this.panel7);
            this.startStep1.Icon = ((System.Drawing.Image)(resources.GetObject("startStep1.Icon")));
            this.startStep1.Name = "startStep1";
            this.startStep1.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.startStep1.Title = "第一步：确认Y轴与X轴的夹角";
            this.startStep1.TitleFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.bClear);
            this.panel7.Controls.Add(this.bUpdate);
            this.panel7.Controls.Add(this.tableLayoutPanel1);
            this.panel7.Controls.Add(this.panel3);
            this.panel7.Location = new System.Drawing.Point(165, 78);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(482, 268);
            this.panel7.TabIndex = 8;
            // 
            // bClear
            // 
            this.bClear.BackColor = System.Drawing.Color.Red;
            this.bClear.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bClear.Location = new System.Drawing.Point(209, 176);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(120, 41);
            this.bClear.TabIndex = 17;
            this.bClear.Text = "初始化机械误差";
            this.bClear.UseVisualStyleBackColor = false;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bUpdate
            // 
            this.bUpdate.BackColor = System.Drawing.Color.Yellow;
            this.bUpdate.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bUpdate.Location = new System.Drawing.Point(360, 176);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(119, 41);
            this.bUpdate.TabIndex = 6;
            this.bUpdate.Text = "更新硬件参数";
            this.bUpdate.UseVisualStyleBackColor = false;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tXYAngle, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(209, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(273, 172);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 34);
            this.label4.TabIndex = 6;
            this.label4.Text = "X轴角度:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tXYAngle
            // 
            this.tXYAngle.DecimalPlaces = 3;
            this.tXYAngle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.tXYAngle.Location = new System.Drawing.Point(139, 3);
            this.tXYAngle.Maximum = new decimal(new int[] {
            92,
            0,
            0,
            0});
            this.tXYAngle.Minimum = new decimal(new int[] {
            88,
            0,
            0,
            0});
            this.tXYAngle.Name = "tXYAngle";
            this.tXYAngle.Size = new System.Drawing.Size(83, 21);
            this.tXYAngle.TabIndex = 7;
            this.tXYAngle.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(209, 268);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.bCal);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 44);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(209, 180);
            this.panel4.TabIndex = 3;
            // 
            // bCal
            // 
            this.bCal.BackColor = System.Drawing.Color.Yellow;
            this.bCal.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bCal.Location = new System.Drawing.Point(50, 61);
            this.bCal.Name = "bCal";
            this.bCal.Size = new System.Drawing.Size(87, 50);
            this.bCal.TabIndex = 5;
            this.bCal.Text = "计算=>";
            this.bCal.UseVisualStyleBackColor = false;
            this.bCal.Click += new System.EventHandler(this.bCal_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 0;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bRightBottom);
            this.panel2.Controls.Add(this.bOrg);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 224);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(209, 44);
            this.panel2.TabIndex = 3;
            // 
            // bRightBottom
            // 
            this.bRightBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.bRightBottom.Dock = System.Windows.Forms.DockStyle.Right;
            this.bRightBottom.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bRightBottom.Location = new System.Drawing.Point(134, 0);
            this.bRightBottom.Name = "bRightBottom";
            this.bRightBottom.Size = new System.Drawing.Size(75, 44);
            this.bRightBottom.TabIndex = 2;
            this.bRightBottom.Text = "右下角";
            this.bRightBottom.UseVisualStyleBackColor = false;
            this.bRightBottom.Click += new System.EventHandler(this.bRightBottom_Click);
            // 
            // bOrg
            // 
            this.bOrg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.bOrg.Dock = System.Windows.Forms.DockStyle.Left;
            this.bOrg.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bOrg.Location = new System.Drawing.Point(0, 0);
            this.bOrg.Name = "bOrg";
            this.bOrg.Size = new System.Drawing.Size(75, 44);
            this.bOrg.TabIndex = 0;
            this.bOrg.Text = "原点";
            this.bOrg.UseVisualStyleBackColor = false;
            this.bOrg.Click += new System.EventHandler(this.bFindOrg_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bSetRightTop);
            this.panel1.Controls.Add(this.bLeftTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 44);
            this.panel1.TabIndex = 2;
            // 
            // bSetRightTop
            // 
            this.bSetRightTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.bSetRightTop.Dock = System.Windows.Forms.DockStyle.Right;
            this.bSetRightTop.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bSetRightTop.Location = new System.Drawing.Point(134, 0);
            this.bSetRightTop.Name = "bSetRightTop";
            this.bSetRightTop.Size = new System.Drawing.Size(75, 44);
            this.bSetRightTop.TabIndex = 1;
            this.bSetRightTop.Text = "右上角";
            this.bSetRightTop.UseVisualStyleBackColor = false;
            this.bSetRightTop.Click += new System.EventHandler(this.bRightTop_Click);
            // 
            // bLeftTop
            // 
            this.bLeftTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.bLeftTop.Dock = System.Windows.Forms.DockStyle.Left;
            this.bLeftTop.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bLeftTop.Location = new System.Drawing.Point(0, 0);
            this.bLeftTop.Name = "bLeftTop";
            this.bLeftTop.Size = new System.Drawing.Size(75, 44);
            this.bLeftTop.TabIndex = 0;
            this.bLeftTop.Text = "左上角";
            this.bLeftTop.UseVisualStyleBackColor = false;
            this.bLeftTop.Click += new System.EventHandler(this.bLeftTop_Click);
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackButtonEnabled = true;
            this.wizardControl1.BackButtonVisible = true;
            this.wizardControl1.CancelButtonEnabled = true;
            this.wizardControl1.CancelButtonVisible = true;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.HelpButtonEnabled = true;
            this.wizardControl1.HelpButtonVisible = true;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextButtonEnabled = true;
            this.wizardControl1.NextButtonVisible = true;
            this.wizardControl1.Size = new System.Drawing.Size(650, 520);
            this.wizardControl1.WizardSteps.Add(this.startStep6);
            this.wizardControl1.WizardSteps.Add(this.startStep1);
            this.wizardControl1.WizardSteps.Add(this.startStep2);
            this.wizardControl1.WizardSteps.Add(this.startStep3);
            this.wizardControl1.WizardSteps.Add(this.startStep4);
            this.wizardControl1.WizardSteps.Add(this.startStep5);
            // 
            // startStep6
            // 
            this.startStep6.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep6.BindingImage")));
            this.startStep6.Controls.Add(this.module);
            this.startStep6.Icon = ((System.Drawing.Image)(resources.GetObject("startStep6.Icon")));
            this.startStep6.Name = "startStep6";
            this.startStep6.Subtitle = "";
            this.startStep6.SubtitleFont = new System.Drawing.Font("Microsoft YaHei", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startStep6.Title = "选择标定模组";
            this.startStep6.TitleFont = new System.Drawing.Font("Microsoft YaHei", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // startStep2
            // 
            this.startStep2.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep2.BindingImage")));
            this.startStep2.Controls.Add(this.panel5);
            this.startStep2.Icon = ((System.Drawing.Image)(resources.GetObject("startStep2.Icon")));
            this.startStep2.Name = "startStep2";
            this.startStep2.Subtitle = "计算X轴方向直线度";
            this.startStep2.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.startStep2.Title = "第二步:确认X轴直线度";
            this.startStep2.TitleFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.axisOffsetItem1);
            this.panel5.Location = new System.Drawing.Point(3, 50);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(644, 432);
            this.panel5.TabIndex = 2;
            // 
            // startStep3
            // 
            this.startStep3.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep3.BindingImage")));
            this.startStep3.Controls.Add(this.panel6);
            this.startStep3.Icon = ((System.Drawing.Image)(resources.GetObject("startStep3.Icon")));
            this.startStep3.Name = "startStep3";
            this.startStep3.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.startStep3.Title = "第三步:确认Y轴直线度";
            this.startStep3.TitleFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.axisOffsetItem2);
            this.panel6.Location = new System.Drawing.Point(3, 49);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(644, 431);
            this.panel6.TabIndex = 3;
            // 
            // startStep4
            // 
            this.startStep4.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep4.BindingImage")));
            this.startStep4.Icon = ((System.Drawing.Image)(resources.GetObject("startStep4.Icon")));
            this.startStep4.Name = "startStep4";
            this.startStep4.Subtitle = "用贴标高度下和拍照高度下图像做对比，同一个点的像素误差";
            this.startStep4.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.startStep4.Title = "第四步：吸嘴夹角确认";
            this.startStep4.TitleFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            // 
            // startStep5
            // 
            this.startStep5.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep5.BindingImage")));
            this.startStep5.Icon = ((System.Drawing.Image)(resources.GetObject("startStep5.Icon")));
            this.startStep5.Name = "startStep5";
            this.startStep5.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.startStep5.Title = "第五步：物料相机与X轴夹角";
            this.startStep5.TitleFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            // 
            // module
            // 
            this.module.Font = new System.Drawing.Font("SimSun", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.module.Location = new System.Drawing.Point(222, 135);
            this.module.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.module.Module = GeneralMachine.Config.Module.Front;
            this.module.Name = "module";
            this.module.Size = new System.Drawing.Size(320, 165);
            this.module.TabIndex = 1;
            this.module.ModuleChange += new System.EventHandler<GeneralMachine.Config.Module>(this.module_ModuleChange_1);
            // 
            // axisOffsetItem1
            // 
            this.axisOffsetItem1.BackColor = System.Drawing.Color.White;
            this.axisOffsetItem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisOffsetItem1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.axisOffsetItem1.Location = new System.Drawing.Point(0, 0);
            this.axisOffsetItem1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.axisOffsetItem1.Module = GeneralMachine.Config.Module.Front;
            this.axisOffsetItem1.Name = "axisOffsetItem1";
            this.axisOffsetItem1.Size = new System.Drawing.Size(644, 432);
            this.axisOffsetItem1.TabIndex = 2;
            this.axisOffsetItem1.Tilte = "X轴直线度";
            // 
            // axisOffsetItem2
            // 
            this.axisOffsetItem2.BackColor = System.Drawing.Color.White;
            this.axisOffsetItem2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisOffsetItem2.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.axisOffsetItem2.Location = new System.Drawing.Point(0, 0);
            this.axisOffsetItem2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.axisOffsetItem2.Module = GeneralMachine.Config.Module.Front;
            this.axisOffsetItem2.Name = "axisOffsetItem2";
            this.axisOffsetItem2.Size = new System.Drawing.Size(644, 431);
            this.axisOffsetItem2.TabIndex = 2;
            this.axisOffsetItem2.Tilte = "Y轴直线度";
            // 
            // fm_Hardware
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.wizardControl1);
            this.Name = "fm_Hardware";
            this.Size = new System.Drawing.Size(650, 520);
            this.startStep1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tXYAngle)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.startStep6.ResumeLayout(false);
            this.startStep2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.startStep3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WizardBase.StartStep startStep1;
        private WizardBase.WizardControl wizardControl1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button bCal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bRightBottom;
        private System.Windows.Forms.Button bOrg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bLeftTop;
        private System.Windows.Forms.Label label4;
        private WizardBase.StartStep startStep4;
        private System.Windows.Forms.Panel panel7;
        private WizardBase.StartStep startStep5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bSetRightTop;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.Button bClear;
        private WizardBase.StartStep startStep2;
        private WizardBase.StartStep startStep3;
        private System.Windows.Forms.Panel panel5;
        private AxisOffsetItem axisOffsetItem1;
        private System.Windows.Forms.Panel panel6;
        private AxisOffsetItem axisOffsetItem2;
        private System.Windows.Forms.NumericUpDown tXYAngle;
        private WizardBase.StartStep startStep6;
        private BasicUI.ModuleRadio module;
    }
}