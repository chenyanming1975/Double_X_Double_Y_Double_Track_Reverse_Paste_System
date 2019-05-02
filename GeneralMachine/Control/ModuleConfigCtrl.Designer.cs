namespace GeneralMachine.SystemManager
{
    partial class ModuleConfigCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleConfigCtrl));
            this.panel3 = new System.Windows.Forms.Panel();
            this.zuConfigCtrl1 = new GeneralMachine.BasicUI.ZUConfigCtrl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_SelectZ = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dropPos = new GeneralMachine.BasicUI.XYPos();
            this.tThrowDelay1 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.waitPos = new GeneralMachine.BasicUI.XYPos();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tXWorkLimit = new GeneralMachine.BasicUI.ValueLimitCtrl();
            this.bSetSuckAngle = new System.Windows.Forms.Button();
            this.bSetTrunPasteAngle = new System.Windows.Forms.Button();
            this.tSuckAngle = new System.Windows.Forms.TextBox();
            this.tPasteAngle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tYWorkLimit = new GeneralMachine.BasicUI.ValueLimitCtrl();
            this.bUpdate = new System.Windows.Forms.Button();
            this.moduleRadio1 = new GeneralMachine.BasicUI.ModuleRadio();
            this.yLimit = new GeneralMachine.BasicUI.ValueLimitCtrl();
            this.xLimit = new GeneralMachine.BasicUI.ValueLimitCtrl();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.zuConfigCtrl1);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel3.Location = new System.Drawing.Point(4, 77);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(391, 286);
            this.panel3.TabIndex = 379;
            // 
            // zuConfigCtrl1
            // 
            this.zuConfigCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zuConfigCtrl1.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zuConfigCtrl1.Location = new System.Drawing.Point(0, 48);
            this.zuConfigCtrl1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.zuConfigCtrl1.Module = GeneralMachine.Config.Module.Front;
            this.zuConfigCtrl1.Name = "zuConfigCtrl1";
            this.zuConfigCtrl1.Nozzle = GeneralMachine.Config.Nozzle.Nz1;
            this.zuConfigCtrl1.NozzleConfig = ((GeneralMachine.Config.NozzleConfig)(resources.GetObject("zuConfigCtrl1.NozzleConfig")));
            this.zuConfigCtrl1.Size = new System.Drawing.Size(391, 238);
            this.zuConfigCtrl1.TabIndex = 371;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cb_SelectZ);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(391, 48);
            this.panel1.TabIndex = 370;
            // 
            // cb_SelectZ
            // 
            this.cb_SelectZ.BackColor = System.Drawing.SystemColors.Window;
            this.cb_SelectZ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SelectZ.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_SelectZ.FormattingEnabled = true;
            this.cb_SelectZ.Location = new System.Drawing.Point(67, 10);
            this.cb_SelectZ.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_SelectZ.Name = "cb_SelectZ";
            this.cb_SelectZ.Size = new System.Drawing.Size(296, 28);
            this.cb_SelectZ.TabIndex = 1;
            this.cb_SelectZ.SelectedIndexChanged += new System.EventHandler(this.cb_SelectZ_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Z轴:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.waitPos);
            this.panel2.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(403, 67);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(279, 296);
            this.panel2.TabIndex = 378;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dropPos);
            this.groupBox2.Controls.Add(this.tThrowDelay1);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 103);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(279, 178);
            this.groupBox2.TabIndex = 363;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "抛料位置(mm ms)";
            // 
            // dropPos
            // 
            this.dropPos.Dock = System.Windows.Forms.DockStyle.Top;
            this.dropPos.Location = new System.Drawing.Point(4, 24);
            this.dropPos.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.dropPos.Name = "dropPos";
            this.dropPos.Point = ((System.Drawing.PointF)(resources.GetObject("dropPos.Point")));
            this.dropPos.Size = new System.Drawing.Size(271, 102);
            this.dropPos.TabIndex = 370;
            this.dropPos.TitleName = "抛料位";
            // 
            // tThrowDelay1
            // 
            this.tThrowDelay1.Location = new System.Drawing.Point(64, 138);
            this.tThrowDelay1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tThrowDelay1.Name = "tThrowDelay1";
            this.tThrowDelay1.Size = new System.Drawing.Size(124, 26);
            this.tThrowDelay1.TabIndex = 8;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(21, 141);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(40, 20);
            this.label23.TabIndex = 7;
            this.label23.Text = "延时:";
            // 
            // waitPos
            // 
            this.waitPos.Dock = System.Windows.Forms.DockStyle.Top;
            this.waitPos.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.waitPos.Location = new System.Drawing.Point(0, 0);
            this.waitPos.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.waitPos.Name = "waitPos";
            this.waitPos.Point = ((System.Drawing.PointF)(resources.GetObject("waitPos.Point")));
            this.waitPos.Size = new System.Drawing.Size(279, 103);
            this.waitPos.TabIndex = 369;
            this.waitPos.TitleName = "待料位";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tXWorkLimit);
            this.groupBox3.Controls.Add(this.bSetSuckAngle);
            this.groupBox3.Controls.Add(this.bSetTrunPasteAngle);
            this.groupBox3.Controls.Add(this.tSuckAngle);
            this.groupBox3.Controls.Add(this.tPasteAngle);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.tYWorkLimit);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(685, 63);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(296, 369);
            this.groupBox3.TabIndex = 377;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "翻转位置(mm)";
            // 
            // tXWorkLimit
            // 
            this.tXWorkLimit.Axis = null;
            this.tXWorkLimit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tXWorkLimit.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tXWorkLimit.Location = new System.Drawing.Point(4, 114);
            this.tXWorkLimit.Margin = new System.Windows.Forms.Padding(0);
            this.tXWorkLimit.Name = "tXWorkLimit";
            this.tXWorkLimit.Range = ((GeneralMachine.Definition.LimitRange)(resources.GetObject("tXWorkLimit.Range")));
            this.tXWorkLimit.Size = new System.Drawing.Size(288, 125);
            this.tXWorkLimit.TabIndex = 15;
            this.tXWorkLimit.Title = "X工作区域限定";
            // 
            // bSetSuckAngle
            // 
            this.bSetSuckAngle.BackColor = System.Drawing.Color.Yellow;
            this.bSetSuckAngle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSetSuckAngle.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bSetSuckAngle.Location = new System.Drawing.Point(217, 74);
            this.bSetSuckAngle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSetSuckAngle.Name = "bSetSuckAngle";
            this.bSetSuckAngle.Size = new System.Drawing.Size(51, 28);
            this.bSetSuckAngle.TabIndex = 14;
            this.bSetSuckAngle.Text = "Set";
            this.bSetSuckAngle.UseVisualStyleBackColor = false;
            this.bSetSuckAngle.Click += new System.EventHandler(this.bSetSuckAngle_Click);
            // 
            // bSetTrunPasteAngle
            // 
            this.bSetTrunPasteAngle.BackColor = System.Drawing.Color.Yellow;
            this.bSetTrunPasteAngle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSetTrunPasteAngle.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bSetTrunPasteAngle.Location = new System.Drawing.Point(217, 34);
            this.bSetTrunPasteAngle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSetTrunPasteAngle.Name = "bSetTrunPasteAngle";
            this.bSetTrunPasteAngle.Size = new System.Drawing.Size(51, 28);
            this.bSetTrunPasteAngle.TabIndex = 13;
            this.bSetTrunPasteAngle.Text = "Set";
            this.bSetTrunPasteAngle.UseVisualStyleBackColor = false;
            this.bSetTrunPasteAngle.Click += new System.EventHandler(this.bSetTrunPasteAngle_Click);
            // 
            // tSuckAngle
            // 
            this.tSuckAngle.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tSuckAngle.Location = new System.Drawing.Point(110, 75);
            this.tSuckAngle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tSuckAngle.Name = "tSuckAngle";
            this.tSuckAngle.Size = new System.Drawing.Size(97, 26);
            this.tSuckAngle.TabIndex = 12;
            this.tSuckAngle.Text = "0";
            // 
            // tPasteAngle
            // 
            this.tPasteAngle.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tPasteAngle.Location = new System.Drawing.Point(110, 35);
            this.tPasteAngle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tPasteAngle.Name = "tPasteAngle";
            this.tPasteAngle.Size = new System.Drawing.Size(97, 26);
            this.tPasteAngle.TabIndex = 11;
            this.tPasteAngle.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(22, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "吸标时角度:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(22, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "贴标时角度:";
            // 
            // tYWorkLimit
            // 
            this.tYWorkLimit.Axis = null;
            this.tYWorkLimit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tYWorkLimit.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tYWorkLimit.Location = new System.Drawing.Point(4, 239);
            this.tYWorkLimit.Margin = new System.Windows.Forms.Padding(0);
            this.tYWorkLimit.Name = "tYWorkLimit";
            this.tYWorkLimit.Range = ((GeneralMachine.Definition.LimitRange)(resources.GetObject("tYWorkLimit.Range")));
            this.tYWorkLimit.Size = new System.Drawing.Size(288, 125);
            this.tYWorkLimit.TabIndex = 8;
            this.tYWorkLimit.Title = "Y工作区域限定";
            // 
            // bUpdate
            // 
            this.bUpdate.BackColor = System.Drawing.Color.Lime;
            this.bUpdate.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bUpdate.Location = new System.Drawing.Point(689, 442);
            this.bUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(292, 55);
            this.bUpdate.TabIndex = 382;
            this.bUpdate.Text = "更新";
            this.bUpdate.UseVisualStyleBackColor = false;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // moduleRadio1
            // 
            this.moduleRadio1.Dock = System.Windows.Forms.DockStyle.Top;
            this.moduleRadio1.Location = new System.Drawing.Point(0, 0);
            this.moduleRadio1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.moduleRadio1.Module = GeneralMachine.Config.Module.Front;
            this.moduleRadio1.Name = "moduleRadio1";
            this.moduleRadio1.Size = new System.Drawing.Size(992, 67);
            this.moduleRadio1.TabIndex = 383;
            // 
            // yLimit
            // 
            this.yLimit.Axis = null;
            this.yLimit.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.yLimit.Location = new System.Drawing.Point(358, 375);
            this.yLimit.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.yLimit.Name = "yLimit";
            this.yLimit.Range = ((GeneralMachine.Definition.LimitRange)(resources.GetObject("yLimit.Range")));
            this.yLimit.Size = new System.Drawing.Size(305, 122);
            this.yLimit.TabIndex = 381;
            this.yLimit.Title = "Y轴极限";
            // 
            // xLimit
            // 
            this.xLimit.Axis = null;
            this.xLimit.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xLimit.Location = new System.Drawing.Point(3, 375);
            this.xLimit.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.xLimit.Name = "xLimit";
            this.xLimit.Range = ((GeneralMachine.Definition.LimitRange)(resources.GetObject("xLimit.Range")));
            this.xLimit.Size = new System.Drawing.Size(347, 122);
            this.xLimit.TabIndex = 380;
            this.xLimit.Title = "X轴极限";
            // 
            // ModuleConfigCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.moduleRadio1);
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.yLimit);
            this.Controls.Add(this.xLimit);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ModuleConfigCtrl";
            this.Size = new System.Drawing.Size(992, 513);
            this.Load += new System.EventHandler(this.ModuleConfigCtrl_Load);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private BasicUI.ValueLimitCtrl yLimit;
        private BasicUI.ValueLimitCtrl xLimit;
        private System.Windows.Forms.Panel panel3;
        private BasicUI.ZUConfigCtrl zuConfigCtrl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cb_SelectZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private BasicUI.XYPos dropPos;
        private System.Windows.Forms.TextBox tThrowDelay1;
        private System.Windows.Forms.Label label23;
        private BasicUI.XYPos waitPos;
        private System.Windows.Forms.GroupBox groupBox3;
        private BasicUI.ValueLimitCtrl tYWorkLimit;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tSuckAngle;
        private System.Windows.Forms.TextBox tPasteAngle;
        private System.Windows.Forms.Button bSetSuckAngle;
        private System.Windows.Forms.Button bSetTrunPasteAngle;
        private BasicUI.ModuleRadio moduleRadio1;
        private BasicUI.ValueLimitCtrl tXWorkLimit;
    }
}
