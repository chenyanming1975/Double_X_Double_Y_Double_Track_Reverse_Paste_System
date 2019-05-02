using GeneralMachine.Motion;

namespace GeneralMachine.SpeedManager
{
    partial class frm_SpeedConfig
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SpeedConfig));
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.trunSpeed = new GeneralMachine.BasicUI.SpeedControl();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.uSpeed = new GeneralMachine.BasicUI.SpeedControl();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.zSpeed = new GeneralMachine.BasicUI.SpeedControl();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ySpeed = new GeneralMachine.BasicUI.SpeedControl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.xSpeed = new GeneralMachine.BasicUI.SpeedControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_Select = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.vel = new GeneralMachine.BasicUI.VelWithLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cb_SelectOther = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.segmentItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bUpdate = new System.Windows.Forms.Button();
            this.moduleRadio1 = new GeneralMachine.BasicUI.ModuleRadio();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.segmentItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.groupBox9);
            this.groupBox10.Controls.Add(this.groupBox8);
            this.groupBox10.Controls.Add(this.groupBox7);
            this.groupBox10.Controls.Add(this.groupBox6);
            this.groupBox10.Controls.Add(this.groupBox5);
            this.groupBox10.Controls.Add(this.groupBox4);
            this.groupBox10.Controls.Add(this.groupBox2);
            this.groupBox10.Controls.Add(this.panel1);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox10.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox10.Location = new System.Drawing.Point(0, 42);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox10.Size = new System.Drawing.Size(871, 214);
            this.groupBox10.TabIndex = 365;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "标准速度配置";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label5);
            this.groupBox9.Controls.Add(this.label6);
            this.groupBox9.Controls.Add(this.label7);
            this.groupBox9.Controls.Add(this.label8);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox9.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox9.Location = new System.Drawing.Point(592, 56);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(49, 158);
            this.groupBox9.TabIndex = 374;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "单位";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(6, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "ms";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(6, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "ms";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(6, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 17);
            this.label7.TabIndex = 5;
            this.label7.Text = "mm/s";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(6, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "mm/s";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.trunSpeed);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox8.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox8.Location = new System.Drawing.Point(488, 56);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(104, 158);
            this.groupBox8.TabIndex = 373;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "翻转轴";
            // 
            // trunSpeed
            // 
            this.trunSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trunSpeed.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.trunSpeed.HostarSpeed = ((GeneralMachine.Motion.HostarSpeed)(resources.GetObject("trunSpeed.HostarSpeed")));
            this.trunSpeed.Location = new System.Drawing.Point(3, 19);
            this.trunSpeed.Margin = new System.Windows.Forms.Padding(5);
            this.trunSpeed.MaxSpeed = 3000D;
            this.trunSpeed.MaxStard = 100D;
            this.trunSpeed.MinAccTime = 100D;
            this.trunSpeed.MinDecTime = 100D;
            this.trunSpeed.Name = "trunSpeed";
            this.trunSpeed.Size = new System.Drawing.Size(98, 136);
            this.trunSpeed.TabIndex = 1;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.uSpeed);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox7.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox7.Location = new System.Drawing.Point(384, 56);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(104, 158);
            this.groupBox7.TabIndex = 372;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "U轴";
            // 
            // uSpeed
            // 
            this.uSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uSpeed.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uSpeed.HostarSpeed = ((GeneralMachine.Motion.HostarSpeed)(resources.GetObject("uSpeed.HostarSpeed")));
            this.uSpeed.Location = new System.Drawing.Point(3, 19);
            this.uSpeed.Margin = new System.Windows.Forms.Padding(5);
            this.uSpeed.MaxSpeed = 5000D;
            this.uSpeed.MaxStard = 500D;
            this.uSpeed.MinAccTime = 100D;
            this.uSpeed.MinDecTime = 100D;
            this.uSpeed.Name = "uSpeed";
            this.uSpeed.Size = new System.Drawing.Size(98, 136);
            this.uSpeed.TabIndex = 1;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.zSpeed);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox6.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.Location = new System.Drawing.Point(280, 56);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(104, 158);
            this.groupBox6.TabIndex = 371;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Z轴";
            // 
            // zSpeed
            // 
            this.zSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zSpeed.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zSpeed.HostarSpeed = ((GeneralMachine.Motion.HostarSpeed)(resources.GetObject("zSpeed.HostarSpeed")));
            this.zSpeed.Location = new System.Drawing.Point(3, 19);
            this.zSpeed.Margin = new System.Windows.Forms.Padding(5);
            this.zSpeed.MaxSpeed = 5000D;
            this.zSpeed.MaxStard = 500D;
            this.zSpeed.MinAccTime = 20D;
            this.zSpeed.MinDecTime = 20D;
            this.zSpeed.Name = "zSpeed";
            this.zSpeed.Size = new System.Drawing.Size(98, 136);
            this.zSpeed.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ySpeed);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox5.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(176, 56);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(104, 158);
            this.groupBox5.TabIndex = 370;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Y轴";
            // 
            // ySpeed
            // 
            this.ySpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ySpeed.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ySpeed.HostarSpeed = ((GeneralMachine.Motion.HostarSpeed)(resources.GetObject("ySpeed.HostarSpeed")));
            this.ySpeed.Location = new System.Drawing.Point(3, 19);
            this.ySpeed.Margin = new System.Windows.Forms.Padding(5);
            this.ySpeed.MaxSpeed = 1600D;
            this.ySpeed.MaxStard = 100D;
            this.ySpeed.MinAccTime = 100D;
            this.ySpeed.MinDecTime = 100D;
            this.ySpeed.Name = "ySpeed";
            this.ySpeed.Size = new System.Drawing.Size(98, 136);
            this.ySpeed.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.xSpeed);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(72, 56);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(104, 158);
            this.groupBox4.TabIndex = 369;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "X轴";
            // 
            // xSpeed
            // 
            this.xSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xSpeed.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xSpeed.HostarSpeed = ((GeneralMachine.Motion.HostarSpeed)(resources.GetObject("xSpeed.HostarSpeed")));
            this.xSpeed.Location = new System.Drawing.Point(3, 19);
            this.xSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.xSpeed.MaxSpeed = 2200D;
            this.xSpeed.MaxStard = 100D;
            this.xSpeed.MinAccTime = 100D;
            this.xSpeed.MinDecTime = 100D;
            this.xSpeed.Name = "xSpeed";
            this.xSpeed.Size = new System.Drawing.Size(98, 136);
            this.xSpeed.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(72, 158);
            this.groupBox2.TabIndex = 368;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "项目";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(8, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "减速时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(8, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "加速时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(8, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "最高速度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(20, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "初速度";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cb_Select);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 16);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(871, 40);
            this.panel1.TabIndex = 367;
            // 
            // cb_Select
            // 
            this.cb_Select.BackColor = System.Drawing.Color.White;
            this.cb_Select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Select.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_Select.FormattingEnabled = true;
            this.cb_Select.Location = new System.Drawing.Point(193, 6);
            this.cb_Select.Name = "cb_Select";
            this.cb_Select.Size = new System.Drawing.Size(251, 25);
            this.cb_Select.TabIndex = 1;
            this.cb_Select.SelectedIndexChanged += new System.EventHandler(this.cb_Select_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(34, 11);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "选择速度方案：";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.vel);
            this.groupBox11.Controls.Add(this.panel2);
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox11.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox11.Location = new System.Drawing.Point(0, 256);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(205, 318);
            this.groupBox11.TabIndex = 370;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "其他速度";
            // 
            // vel
            // 
            this.vel.Dock = System.Windows.Forms.DockStyle.Top;
            this.vel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.vel.Location = new System.Drawing.Point(3, 59);
            this.vel.Margin = new System.Windows.Forms.Padding(4);
            this.vel.Name = "vel";
            this.vel.Size = new System.Drawing.Size(199, 138);
            this.vel.Speed = ((GeneralMachine.Motion.HostarSpeed)(resources.GetObject("vel.Speed")));
            this.vel.TabIndex = 292;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cb_SelectOther);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(3, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(199, 40);
            this.panel2.TabIndex = 291;
            // 
            // cb_SelectOther
            // 
            this.cb_SelectOther.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_SelectOther.BackColor = System.Drawing.Color.White;
            this.cb_SelectOther.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SelectOther.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_SelectOther.FormattingEnabled = true;
            this.cb_SelectOther.Location = new System.Drawing.Point(98, 8);
            this.cb_SelectOther.Name = "cb_SelectOther";
            this.cb_SelectOther.Size = new System.Drawing.Size(83, 25);
            this.cb_SelectOther.TabIndex = 1;
            this.cb_SelectOther.SelectedIndexChanged += new System.EventHandler(this.cb_SelectOther_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(13, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "选择方案:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // segmentItemBindingSource
            // 
            this.segmentItemBindingSource.DataSource = typeof(GeneralMachine.Motion.SegmentItem);
            // 
            // bUpdate
            // 
            this.bUpdate.BackColor = System.Drawing.Color.Yellow;
            this.bUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUpdate.Font = new System.Drawing.Font("Microsoft YaHei Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bUpdate.Location = new System.Drawing.Point(719, 4);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(140, 35);
            this.bUpdate.TabIndex = 372;
            this.bUpdate.Text = "更新到机器";
            this.bUpdate.UseVisualStyleBackColor = false;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // moduleRadio1
            // 
            this.moduleRadio1.Dock = System.Windows.Forms.DockStyle.Top;
            this.moduleRadio1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.moduleRadio1.Location = new System.Drawing.Point(0, 0);
            this.moduleRadio1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moduleRadio1.Module = GeneralMachine.Config.Module.Front;
            this.moduleRadio1.Name = "moduleRadio1";
            this.moduleRadio1.Size = new System.Drawing.Size(871, 42);
            this.moduleRadio1.TabIndex = 0;
            // 
            // frm_SpeedConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(871, 574);
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.moduleRadio1);
            this.Font = new System.Drawing.Font("Microsoft YaHei Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_SpeedConfig";
            this.Text = "速度配置";
            this.Load += new System.EventHandler(this.frm_SpeedConfig_Load);
            this.groupBox10.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.segmentItemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BasicUI.ModuleRadio moduleRadio1;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_Select;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private BasicUI.SpeedControl xSpeed;
        private System.Windows.Forms.GroupBox groupBox5;
        private BasicUI.SpeedControl ySpeed;
        private System.Windows.Forms.GroupBox groupBox6;
        private BasicUI.SpeedControl zSpeed;
        private System.Windows.Forms.GroupBox groupBox7;
        private BasicUI.SpeedControl uSpeed;
        private System.Windows.Forms.GroupBox groupBox8;
        private BasicUI.SpeedControl trunSpeed;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox11;
        private BasicUI.VelWithLabel vel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cb_SelectOther;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.BindingSource segmentItemBindingSource;
    }
}