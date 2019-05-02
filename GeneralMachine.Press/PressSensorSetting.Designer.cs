namespace GeneralMachine.Press
{
    partial class PressSensorSetting
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bUpdate = new System.Windows.Forms.Button();
            this.bZero = new System.Windows.Forms.Button();
            this.bDisConnect = new System.Windows.Forms.Button();
            this.bConnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nAlarmPress = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nAlarmTimes = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listPress = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bAuto = new System.Windows.Forms.Button();
            this.tFixedPress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbNz = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbModule = new GeneralMachine.BasicUI.ModuleRadio();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nAlarmPress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nAlarmTimes)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bUpdate);
            this.panel1.Controls.Add(this.bZero);
            this.panel1.Controls.Add(this.bDisConnect);
            this.panel1.Controls.Add(this.bConnect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(445, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(99, 346);
            this.panel1.TabIndex = 2;
            // 
            // bUpdate
            // 
            this.bUpdate.BackColor = System.Drawing.Color.Yellow;
            this.bUpdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.bUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUpdate.Location = new System.Drawing.Point(0, 105);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(99, 35);
            this.bUpdate.TabIndex = 3;
            this.bUpdate.Text = "更新设置";
            this.bUpdate.UseVisualStyleBackColor = false;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // bZero
            // 
            this.bZero.Dock = System.Windows.Forms.DockStyle.Top;
            this.bZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bZero.Location = new System.Drawing.Point(0, 70);
            this.bZero.Name = "bZero";
            this.bZero.Size = new System.Drawing.Size(99, 35);
            this.bZero.TabIndex = 2;
            this.bZero.Text = "压力清零";
            this.bZero.UseVisualStyleBackColor = true;
            this.bZero.Click += new System.EventHandler(this.bZero_Click);
            // 
            // bDisConnect
            // 
            this.bDisConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.bDisConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDisConnect.Location = new System.Drawing.Point(0, 35);
            this.bDisConnect.Name = "bDisConnect";
            this.bDisConnect.Size = new System.Drawing.Size(99, 35);
            this.bDisConnect.TabIndex = 1;
            this.bDisConnect.Text = "断开";
            this.bDisConnect.UseVisualStyleBackColor = true;
            this.bDisConnect.Click += new System.EventHandler(this.bDisConnect_Click);
            // 
            // bConnect
            // 
            this.bConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.bConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bConnect.Location = new System.Drawing.Point(0, 0);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(99, 35);
            this.bConnect.TabIndex = 0;
            this.bConnect.Text = "连接";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 346);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本设置";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tIP, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nAlarmPress, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nAlarmTimes, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(180, 324);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tIP
            // 
            this.tIP.Location = new System.Drawing.Point(93, 83);
            this.tIP.Name = "tIP";
            this.tIP.Size = new System.Drawing.Size(84, 23);
            this.tIP.TabIndex = 5;
            this.tIP.Text = "192.168.1.30";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "报警克数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nAlarmPress
            // 
            this.nAlarmPress.Location = new System.Drawing.Point(93, 3);
            this.nAlarmPress.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nAlarmPress.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nAlarmPress.Name = "nAlarmPress";
            this.nAlarmPress.Size = new System.Drawing.Size(84, 23);
            this.nAlarmPress.TabIndex = 5;
            this.nAlarmPress.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 40);
            this.label3.TabIndex = 8;
            this.label3.Text = "连续NG次数";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nAlarmTimes
            // 
            this.nAlarmTimes.Location = new System.Drawing.Point(93, 43);
            this.nAlarmTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nAlarmTimes.Name = "nAlarmTimes";
            this.nAlarmTimes.Size = new System.Drawing.Size(84, 23);
            this.nAlarmTimes.TabIndex = 9;
            this.nAlarmTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 40);
            this.label2.TabIndex = 10;
            this.label2.Text = "IP";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listPress);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(186, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 346);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "贴附压力矫正";
            // 
            // listPress
            // 
            this.listPress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPress.FormattingEnabled = true;
            this.listPress.ItemHeight = 17;
            this.listPress.Location = new System.Drawing.Point(3, 79);
            this.listPress.Name = "listPress";
            this.listPress.Size = new System.Drawing.Size(253, 264);
            this.listPress.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.bAuto);
            this.panel3.Controls.Add(this.tFixedPress);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 49);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(253, 30);
            this.panel3.TabIndex = 2;
            // 
            // bAuto
            // 
            this.bAuto.Location = new System.Drawing.Point(183, 3);
            this.bAuto.Name = "bAuto";
            this.bAuto.Size = new System.Drawing.Size(72, 23);
            this.bAuto.TabIndex = 2;
            this.bAuto.Text = "自动测量";
            this.bAuto.UseVisualStyleBackColor = true;
            this.bAuto.Click += new System.EventHandler(this.bAuto_Click);
            // 
            // tFixedPress
            // 
            this.tFixedPress.Dock = System.Windows.Forms.DockStyle.Left;
            this.tFixedPress.Location = new System.Drawing.Point(59, 0);
            this.tFixedPress.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.tFixedPress.Name = "tFixedPress";
            this.tFixedPress.ReadOnly = true;
            this.tFixedPress.Size = new System.Drawing.Size(121, 23);
            this.tFixedPress.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 30);
            this.label5.TabIndex = 0;
            this.label5.Text = "固有压力:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbNz);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 30);
            this.panel2.TabIndex = 0;
            // 
            // cbNz
            // 
            this.cbNz.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbNz.FormattingEnabled = true;
            this.cbNz.Items.AddRange(new object[] {
            "Z1",
            "Z2",
            "Z3",
            "Z4"});
            this.cbNz.Location = new System.Drawing.Point(59, 0);
            this.cbNz.Name = "cbNz";
            this.cbNz.Size = new System.Drawing.Size(121, 25);
            this.cbNz.TabIndex = 1;
            this.cbNz.Text = "Z1";
            this.cbNz.SelectedIndexChanged += new System.EventHandler(this.cbNz_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "选择吸嘴:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbModule
            // 
            this.cbModule.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbModule.Location = new System.Drawing.Point(0, 0);
            this.cbModule.Module = GeneralMachine.Config.Module.Front;
            this.cbModule.Name = "cbModule";
            this.cbModule.Size = new System.Drawing.Size(544, 41);
            this.cbModule.TabIndex = 0;
            this.cbModule.ModuleChange += new System.EventHandler<GeneralMachine.Config.Module>(this.cbModule_ModuleChange);
            // 
            // PressSensorSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(544, 387);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbModule);
            this.Name = "PressSensorSetting";
            this.Text = "压力传感器设置";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nAlarmPress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nAlarmTimes)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BasicUI.ModuleRadio cbModule;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.Button bZero;
        private System.Windows.Forms.Button bDisConnect;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nAlarmPress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbNz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bAuto;
        private System.Windows.Forms.TextBox tFixedPress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listPress;
        private System.Windows.Forms.TextBox tIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nAlarmTimes;
        private System.Windows.Forms.Label label2;
    }
}
