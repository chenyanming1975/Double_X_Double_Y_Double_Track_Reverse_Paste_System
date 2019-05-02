namespace GeneralMachine
{
    partial class frm_DryRun
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DryRun));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.moduleRadio1 = new GeneralMachine.BasicUI.ModuleRadio();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bTestZ = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.numCycle = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.cbSelectNz = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSelectSp2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bMove = new System.Windows.Forms.Button();
            this.bClearAll = new System.Windows.Forms.Button();
            this.bDeletePos = new System.Windows.Forms.Button();
            this.bAddPos = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.bRUN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numTestCycle = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numTestDelay = new System.Windows.Forms.NumericUpDown();
            this.cb_SelectSpeed = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.bLearnMove = new System.Windows.Forms.Button();
            this.btnStatisTest = new System.Windows.Forms.Button();
            this.btnDynamicTest = new System.Windows.Forms.Button();
            this.startPt = new GeneralMachine.BasicUI.XYPos();
            this.endPos = new GeneralMachine.BasicUI.XYPos();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ndCount = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.ndDelay = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTestCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTestDelay)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndDelay)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.moduleRadio1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 74);
            this.panel1.TabIndex = 7;
            // 
            // moduleRadio1
            // 
            this.moduleRadio1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moduleRadio1.Location = new System.Drawing.Point(0, 0);
            this.moduleRadio1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.moduleRadio1.Module = GeneralMachine.Config.Module.Front;
            this.moduleRadio1.Name = "moduleRadio1";
            this.moduleRadio1.Size = new System.Drawing.Size(590, 74);
            this.moduleRadio1.TabIndex = 0;
            this.moduleRadio1.ModuleChange += new System.EventHandler<GeneralMachine.Config.Module>(this.moduleRadio1_ModuleChange);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 74);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(590, 326);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试点";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bTestZ);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.numCycle);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.numDelay);
            this.groupBox3.Controls.Add(this.cbSelectNz);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cbSelectSp2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(392, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(194, 310);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Z轴测试";
            // 
            // bTestZ
            // 
            this.bTestZ.BackColor = System.Drawing.Color.LightGreen;
            this.bTestZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTestZ.Location = new System.Drawing.Point(58, 160);
            this.bTestZ.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bTestZ.Name = "bTestZ";
            this.bTestZ.Size = new System.Drawing.Size(112, 40);
            this.bTestZ.TabIndex = 33;
            this.bTestZ.Text = "运动";
            this.bTestZ.UseVisualStyleBackColor = false;
            this.bTestZ.Click += new System.EventHandler(this.bTestZ_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 97);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 32;
            this.label6.Text = "循环(次)：";
            // 
            // numCycle
            // 
            this.numCycle.Location = new System.Drawing.Point(97, 95);
            this.numCycle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numCycle.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numCycle.Name = "numCycle";
            this.numCycle.Size = new System.Drawing.Size(90, 23);
            this.numCycle.TabIndex = 33;
            this.numCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCycle.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 135);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 17);
            this.label7.TabIndex = 34;
            this.label7.Text = "延迟(ms)：";
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(97, 132);
            this.numDelay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numDelay.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(90, 23);
            this.numDelay.TabIndex = 35;
            this.numDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDelay.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // cbSelectNz
            // 
            this.cbSelectNz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectNz.FormattingEnabled = true;
            this.cbSelectNz.Items.AddRange(new object[] {
            "Z1",
            "Z2",
            "Z3",
            "Z4"});
            this.cbSelectNz.Location = new System.Drawing.Point(97, 63);
            this.cbSelectNz.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSelectNz.Name = "cbSelectNz";
            this.cbSelectNz.Size = new System.Drawing.Size(91, 25);
            this.cbSelectNz.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 66);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Z轴：";
            // 
            // cbSelectSp2
            // 
            this.cbSelectSp2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectSp2.FormattingEnabled = true;
            this.cbSelectSp2.Items.AddRange(new object[] {
            "慢速",
            "中速",
            "快速",
            ""});
            this.cbSelectSp2.Location = new System.Drawing.Point(97, 31);
            this.cbSelectSp2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSelectSp2.Name = "cbSelectSp2";
            this.cbSelectSp2.Size = new System.Drawing.Size(91, 25);
            this.cbSelectSp2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "选择速度：";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.panel2);
            this.groupBox7.Controls.Add(this.listBox1);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox7.Location = new System.Drawing.Point(193, 16);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox7.Size = new System.Drawing.Size(199, 310);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "点位列表";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bMove);
            this.panel2.Controls.Add(this.bClearAll);
            this.panel2.Controls.Add(this.bDeletePos);
            this.panel2.Controls.Add(this.bAddPos);
            this.panel2.Location = new System.Drawing.Point(158, 16);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(35, 286);
            this.panel2.TabIndex = 13;
            // 
            // bMove
            // 
            this.bMove.BackColor = System.Drawing.Color.White;
            this.bMove.Dock = System.Windows.Forms.DockStyle.Top;
            this.bMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMove.Location = new System.Drawing.Point(0, 111);
            this.bMove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bMove.Name = "bMove";
            this.bMove.Size = new System.Drawing.Size(35, 37);
            this.bMove.TabIndex = 14;
            this.bMove.Text = "M";
            this.bMove.UseVisualStyleBackColor = false;
            this.bMove.Click += new System.EventHandler(this.bMove_Click);
            // 
            // bClearAll
            // 
            this.bClearAll.BackColor = System.Drawing.Color.White;
            this.bClearAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.bClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bClearAll.Location = new System.Drawing.Point(0, 74);
            this.bClearAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bClearAll.Name = "bClearAll";
            this.bClearAll.Size = new System.Drawing.Size(35, 37);
            this.bClearAll.TabIndex = 13;
            this.bClearAll.Text = "C";
            this.bClearAll.UseVisualStyleBackColor = false;
            this.bClearAll.Click += new System.EventHandler(this.bClearAll_Click);
            // 
            // bDeletePos
            // 
            this.bDeletePos.BackColor = System.Drawing.Color.White;
            this.bDeletePos.Dock = System.Windows.Forms.DockStyle.Top;
            this.bDeletePos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDeletePos.Location = new System.Drawing.Point(0, 37);
            this.bDeletePos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bDeletePos.Name = "bDeletePos";
            this.bDeletePos.Size = new System.Drawing.Size(35, 37);
            this.bDeletePos.TabIndex = 11;
            this.bDeletePos.Text = "-";
            this.bDeletePos.UseVisualStyleBackColor = false;
            this.bDeletePos.Click += new System.EventHandler(this.bDeletePos_Click);
            // 
            // bAddPos
            // 
            this.bAddPos.BackColor = System.Drawing.Color.White;
            this.bAddPos.Dock = System.Windows.Forms.DockStyle.Top;
            this.bAddPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAddPos.Location = new System.Drawing.Point(0, 0);
            this.bAddPos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bAddPos.Name = "bAddPos";
            this.bAddPos.Size = new System.Drawing.Size(35, 37);
            this.bAddPos.TabIndex = 12;
            this.bAddPos.Text = "+";
            this.bAddPos.UseVisualStyleBackColor = false;
            this.bAddPos.Click += new System.EventHandler(this.bAddPos_Click);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(0, 16);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(155, 294);
            this.listBox1.TabIndex = 10;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.bRUN);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.numTestCycle);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.numTestDelay);
            this.groupBox6.Controls.Add(this.cb_SelectSpeed);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox6.Location = new System.Drawing.Point(0, 16);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox6.Size = new System.Drawing.Size(193, 310);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "运行参数";
            // 
            // bRUN
            // 
            this.bRUN.BackColor = System.Drawing.Color.LightGreen;
            this.bRUN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRUN.Location = new System.Drawing.Point(46, 160);
            this.bRUN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bRUN.Name = "bRUN";
            this.bRUN.Size = new System.Drawing.Size(112, 40);
            this.bRUN.TabIndex = 32;
            this.bRUN.Text = "运动";
            this.bRUN.UseVisualStyleBackColor = false;
            this.bRUN.Click += new System.EventHandler(this.bRUN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 28;
            this.label2.Text = "循环(次)：";
            // 
            // numTestCycle
            // 
            this.numTestCycle.Location = new System.Drawing.Point(97, 65);
            this.numTestCycle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numTestCycle.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numTestCycle.Name = "numTestCycle";
            this.numTestCycle.Size = new System.Drawing.Size(90, 23);
            this.numTestCycle.TabIndex = 29;
            this.numTestCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTestCycle.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 30;
            this.label3.Text = "延迟(ms)：";
            // 
            // numTestDelay
            // 
            this.numTestDelay.Location = new System.Drawing.Point(97, 102);
            this.numTestDelay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numTestDelay.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numTestDelay.Name = "numTestDelay";
            this.numTestDelay.Size = new System.Drawing.Size(90, 23);
            this.numTestDelay.TabIndex = 31;
            this.numTestDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTestDelay.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // cb_SelectSpeed
            // 
            this.cb_SelectSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SelectSpeed.FormattingEnabled = true;
            this.cb_SelectSpeed.Items.AddRange(new object[] {
            "慢速",
            "中速",
            "快速",
            ""});
            this.cb_SelectSpeed.Location = new System.Drawing.Point(97, 28);
            this.cb_SelectSpeed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_SelectSpeed.Name = "cb_SelectSpeed";
            this.cb_SelectSpeed.Size = new System.Drawing.Size(90, 25);
            this.cb_SelectSpeed.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "选择速度：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnCheck);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.bLearnMove);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox5.Location = new System.Drawing.Point(4, 20);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Size = new System.Drawing.Size(139, 247);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "学习特征";
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(64, 127);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(57, 96);
            this.btnCheck.TabIndex = 677;
            this.btnCheck.Text = "检测";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(4, 88);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(102, 17);
            this.label20.TabIndex = 676;
            this.label20.Text = "使用ROI框住特征";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(10, 49);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 17);
            this.label15.TabIndex = 675;
            this.label15.Text = "在相机画面中";
            // 
            // bLearnMove
            // 
            this.bLearnMove.Location = new System.Drawing.Point(1, 127);
            this.bLearnMove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bLearnMove.Name = "bLearnMove";
            this.bLearnMove.Size = new System.Drawing.Size(57, 96);
            this.bLearnMove.TabIndex = 1;
            this.bLearnMove.Text = "学习";
            this.bLearnMove.UseVisualStyleBackColor = true;
            this.bLearnMove.Click += new System.EventHandler(this.bLearnMove_Click);
            // 
            // btnStatisTest
            // 
            this.btnStatisTest.BackColor = System.Drawing.Color.LightGreen;
            this.btnStatisTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatisTest.Location = new System.Drawing.Point(194, 37);
            this.btnStatisTest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStatisTest.Name = "btnStatisTest";
            this.btnStatisTest.Size = new System.Drawing.Size(81, 60);
            this.btnStatisTest.TabIndex = 23;
            this.btnStatisTest.Text = "静态测试";
            this.btnStatisTest.UseVisualStyleBackColor = false;
            this.btnStatisTest.Click += new System.EventHandler(this.btnStatisTest_Click);
            // 
            // btnDynamicTest
            // 
            this.btnDynamicTest.BackColor = System.Drawing.Color.LightGreen;
            this.btnDynamicTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDynamicTest.Location = new System.Drawing.Point(278, 37);
            this.btnDynamicTest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDynamicTest.Name = "btnDynamicTest";
            this.btnDynamicTest.Size = new System.Drawing.Size(81, 60);
            this.btnDynamicTest.TabIndex = 24;
            this.btnDynamicTest.Text = "动态测试";
            this.btnDynamicTest.UseVisualStyleBackColor = false;
            this.btnDynamicTest.Click += new System.EventHandler(this.btnDynamicTest_Click);
            // 
            // startPt
            // 
            this.startPt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startPt.Location = new System.Drawing.Point(145, 144);
            this.startPt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.startPt.Name = "startPt";
            this.startPt.Point = ((System.Drawing.PointF)(resources.GetObject("startPt.Point")));
            this.startPt.Size = new System.Drawing.Size(174, 108);
            this.startPt.TabIndex = 28;
            this.startPt.TitleName = "起点";
            // 
            // endPos
            // 
            this.endPos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.endPos.Location = new System.Drawing.Point(325, 144);
            this.endPos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.endPos.Name = "endPos";
            this.endPos.Point = ((System.Drawing.PointF)(resources.GetObject("endPos.Point")));
            this.endPos.Size = new System.Drawing.Size(174, 108);
            this.endPos.TabIndex = 29;
            this.endPos.TitleName = "终点";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.ndCount);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.ndDelay);
            this.groupBox4.Controls.Add(this.btnDynamicTest);
            this.groupBox4.Controls.Add(this.btnStatisTest);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(143, 20);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(443, 115);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "参数配置";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 41);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 17);
            this.label11.TabIndex = 21;
            this.label11.Text = "运行次数:";
            // 
            // ndCount
            // 
            this.ndCount.Location = new System.Drawing.Point(94, 36);
            this.ndCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ndCount.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.ndCount.Name = "ndCount";
            this.ndCount.Size = new System.Drawing.Size(90, 23);
            this.ndCount.TabIndex = 22;
            this.ndCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ndCount.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 79);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 17);
            this.label12.TabIndex = 26;
            this.label12.Text = "取像延迟:";
            // 
            // ndDelay
            // 
            this.ndDelay.Location = new System.Drawing.Point(94, 74);
            this.ndDelay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ndDelay.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.ndDelay.Name = "ndDelay";
            this.ndDelay.Size = new System.Drawing.Size(90, 23);
            this.ndDelay.TabIndex = 27;
            this.ndDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ndDelay.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.endPos);
            this.groupBox2.Controls.Add(this.startPt);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 400);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(590, 271);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "视觉GRR测试";
            // 
            // frm_DryRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frm_DryRun";
            this.Size = new System.Drawing.Size(590, 671);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTestCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTestDelay)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndDelay)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private BasicUI.ModuleRadio moduleRadio1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_SelectSpeed;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnCheck;
        public System.Windows.Forms.Label label20;
        public System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button bLearnMove;
        private System.Windows.Forms.Button btnStatisTest;
        private System.Windows.Forms.Button btnDynamicTest;
        private BasicUI.XYPos startPt;
        private BasicUI.XYPos endPos;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown ndCount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown ndDelay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numTestCycle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numTestDelay;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button bDeletePos;
        private System.Windows.Forms.Button bAddPos;
        private System.Windows.Forms.Button bRUN;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bClearAll;
        private System.Windows.Forms.Button bMove;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbSelectNz;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbSelectSp2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bTestZ;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numCycle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numDelay;
    }
}