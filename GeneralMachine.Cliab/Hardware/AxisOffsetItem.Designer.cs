namespace GeneralMachine.Cliab
{
    partial class AxisOffsetItem
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
            this.gAxis = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bEnable = new System.Windows.Forms.Button();
            this.bFit = new System.Windows.Forms.Button();
            this.bStart = new System.Windows.Forms.Button();
            this.gCheck = new System.Windows.Forms.GroupBox();
            this.bRefresh = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbVisionList = new System.Windows.Forms.ComboBox();
            this.bGoSelect = new System.Windows.Forms.Button();
            this.bConfig = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numSpace = new System.Windows.Forms.NumericUpDown();
            this.bSetStart = new System.Windows.Forms.Button();
            this.numPoint = new System.Windows.Forms.NumericUpDown();
            this.bSetEnd = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.argList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numPow = new System.Windows.Forms.NumericUpDown();
            this.dAxis = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gAxis.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dAxis)).BeginInit();
            this.SuspendLayout();
            // 
            // gAxis
            // 
            this.gAxis.Controls.Add(this.panel1);
            this.gAxis.Controls.Add(this.gCheck);
            this.gAxis.Controls.Add(this.textBox2);
            this.gAxis.Controls.Add(this.button1);
            this.gAxis.Controls.Add(this.textBox1);
            this.gAxis.Controls.Add(this.label3);
            this.gAxis.Controls.Add(this.label2);
            this.gAxis.Controls.Add(this.argList);
            this.gAxis.Controls.Add(this.label1);
            this.gAxis.Controls.Add(this.numPow);
            this.gAxis.Controls.Add(this.dAxis);
            this.gAxis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gAxis.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gAxis.Location = new System.Drawing.Point(0, 0);
            this.gAxis.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gAxis.Name = "gAxis";
            this.gAxis.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gAxis.Size = new System.Drawing.Size(540, 455);
            this.gAxis.TabIndex = 0;
            this.gAxis.TabStop = false;
            this.gAxis.Text = "{0}轴直线度";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bEnable);
            this.panel1.Controls.Add(this.bFit);
            this.panel1.Controls.Add(this.bStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(199, 179);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(87, 271);
            this.panel1.TabIndex = 16;
            // 
            // bEnable
            // 
            this.bEnable.BackColor = System.Drawing.Color.Red;
            this.bEnable.Dock = System.Windows.Forms.DockStyle.Top;
            this.bEnable.Enabled = false;
            this.bEnable.Location = new System.Drawing.Point(0, 90);
            this.bEnable.Name = "bEnable";
            this.bEnable.Size = new System.Drawing.Size(87, 45);
            this.bEnable.TabIndex = 14;
            this.bEnable.Text = "应用数据";
            this.bEnable.UseVisualStyleBackColor = false;
            this.bEnable.Click += new System.EventHandler(this.bEnable_Click);
            // 
            // bFit
            // 
            this.bFit.BackColor = System.Drawing.Color.Yellow;
            this.bFit.Dock = System.Windows.Forms.DockStyle.Top;
            this.bFit.Location = new System.Drawing.Point(0, 45);
            this.bFit.Name = "bFit";
            this.bFit.Size = new System.Drawing.Size(87, 45);
            this.bFit.TabIndex = 15;
            this.bFit.Text = "拟合曲线";
            this.bFit.UseVisualStyleBackColor = false;
            this.bFit.Click += new System.EventHandler(this.bFit_Click);
            // 
            // bStart
            // 
            this.bStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.bStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.bStart.Location = new System.Drawing.Point(0, 0);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(87, 45);
            this.bStart.TabIndex = 13;
            this.bStart.Text = "开始量测";
            this.bStart.UseVisualStyleBackColor = false;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // gCheck
            // 
            this.gCheck.Controls.Add(this.bRefresh);
            this.gCheck.Controls.Add(this.label6);
            this.gCheck.Controls.Add(this.cbVisionList);
            this.gCheck.Controls.Add(this.bGoSelect);
            this.gCheck.Controls.Add(this.bConfig);
            this.gCheck.Controls.Add(this.label5);
            this.gCheck.Controls.Add(this.label4);
            this.gCheck.Controls.Add(this.numSpace);
            this.gCheck.Controls.Add(this.bSetStart);
            this.gCheck.Controls.Add(this.numPoint);
            this.gCheck.Controls.Add(this.bSetEnd);
            this.gCheck.Dock = System.Windows.Forms.DockStyle.Top;
            this.gCheck.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gCheck.Location = new System.Drawing.Point(199, 27);
            this.gCheck.Name = "gCheck";
            this.gCheck.Size = new System.Drawing.Size(337, 152);
            this.gCheck.TabIndex = 12;
            this.gCheck.TabStop = false;
            this.gCheck.Text = "量测";
            // 
            // bRefresh
            // 
            this.bRefresh.Location = new System.Drawing.Point(206, 89);
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.Size = new System.Drawing.Size(61, 28);
            this.bRefresh.TabIndex = 19;
            this.bRefresh.Text = "刷新";
            this.bRefresh.UseVisualStyleBackColor = true;
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "量测算法：";
            // 
            // cbVisionList
            // 
            this.cbVisionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVisionList.FormattingEnabled = true;
            this.cbVisionList.Location = new System.Drawing.Point(91, 89);
            this.cbVisionList.Name = "cbVisionList";
            this.cbVisionList.Size = new System.Drawing.Size(109, 28);
            this.cbVisionList.TabIndex = 17;
            // 
            // bGoSelect
            // 
            this.bGoSelect.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bGoSelect.Location = new System.Drawing.Point(282, 18);
            this.bGoSelect.Name = "bGoSelect";
            this.bGoSelect.Size = new System.Drawing.Size(49, 65);
            this.bGoSelect.TabIndex = 16;
            this.bGoSelect.Text = "到选中";
            this.bGoSelect.UseVisualStyleBackColor = true;
            this.bGoSelect.Click += new System.EventHandler(this.bGoSelect_Click);
            // 
            // bConfig
            // 
            this.bConfig.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bConfig.Location = new System.Drawing.Point(230, 18);
            this.bConfig.Name = "bConfig";
            this.bConfig.Size = new System.Drawing.Size(49, 65);
            this.bConfig.TabIndex = 15;
            this.bConfig.Text = "生成";
            this.bConfig.UseVisualStyleBackColor = true;
            this.bConfig.Click += new System.EventHandler(this.bConfig_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "点数:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "点距:";
            // 
            // numSpace
            // 
            this.numSpace.DecimalPlaces = 2;
            this.numSpace.Location = new System.Drawing.Point(68, 52);
            this.numSpace.Name = "numSpace";
            this.numSpace.Size = new System.Drawing.Size(75, 26);
            this.numSpace.TabIndex = 12;
            this.numSpace.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // bSetStart
            // 
            this.bSetStart.BackColor = System.Drawing.Color.Yellow;
            this.bSetStart.Location = new System.Drawing.Point(149, 18);
            this.bSetStart.Name = "bSetStart";
            this.bSetStart.Size = new System.Drawing.Size(75, 31);
            this.bSetStart.TabIndex = 9;
            this.bSetStart.Text = "起始点";
            this.bSetStart.UseVisualStyleBackColor = false;
            this.bSetStart.Click += new System.EventHandler(this.bSetStart_Click);
            // 
            // numPoint
            // 
            this.numPoint.Location = new System.Drawing.Point(68, 20);
            this.numPoint.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPoint.Name = "numPoint";
            this.numPoint.Size = new System.Drawing.Size(75, 26);
            this.numPoint.TabIndex = 11;
            this.numPoint.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // bSetEnd
            // 
            this.bSetEnd.BackColor = System.Drawing.Color.Yellow;
            this.bSetEnd.Location = new System.Drawing.Point(149, 52);
            this.bSetEnd.Name = "bSetEnd";
            this.bSetEnd.Size = new System.Drawing.Size(75, 31);
            this.bSetEnd.TabIndex = 10;
            this.bSetEnd.Text = "终点";
            this.bSetEnd.UseVisualStyleBackColor = false;
            this.bSetEnd.Click += new System.EventHandler(this.bSetEnd_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(416, 390);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(102, 29);
            this.textBox2.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(324, 390);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 32);
            this.button1.TabIndex = 7;
            this.button1.Text = "输出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(416, 339);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(102, 29);
            this.textBox1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 342);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "输入测试坐标：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "多项式系数：";
            // 
            // argList
            // 
            this.argList.FormattingEnabled = true;
            this.argList.ItemHeight = 21;
            this.argList.Location = new System.Drawing.Point(416, 224);
            this.argList.Name = "argList";
            this.argList.Size = new System.Drawing.Size(102, 109);
            this.argList.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(320, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "拟合幂数：";
            // 
            // numPow
            // 
            this.numPow.Location = new System.Drawing.Point(416, 189);
            this.numPow.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numPow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPow.Name = "numPow";
            this.numPow.Size = new System.Drawing.Size(102, 29);
            this.numPow.TabIndex = 1;
            this.numPow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dAxis
            // 
            this.dAxis.AllowUserToAddRows = false;
            this.dAxis.AllowUserToDeleteRows = false;
            this.dAxis.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dAxis.Dock = System.Windows.Forms.DockStyle.Left;
            this.dAxis.Location = new System.Drawing.Point(4, 27);
            this.dAxis.MultiSelect = false;
            this.dAxis.Name = "dAxis";
            this.dAxis.ReadOnly = true;
            this.dAxis.RowHeadersVisible = false;
            this.dAxis.RowTemplate.Height = 23;
            this.dAxis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dAxis.Size = new System.Drawing.Size(195, 423);
            this.dAxis.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "XY";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "机构误差";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // AxisOffsetItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gAxis);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AxisOffsetItem";
            this.Size = new System.Drawing.Size(540, 455);
            this.Load += new System.EventHandler(this.AxisOffsetItem_Load);
            this.gAxis.ResumeLayout(false);
            this.gAxis.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gCheck.ResumeLayout(false);
            this.gCheck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dAxis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gAxis;
        private System.Windows.Forms.DataGridView dAxis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numPow;
        private System.Windows.Forms.ListBox argList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bSetEnd;
        private System.Windows.Forms.Button bSetStart;
        private System.Windows.Forms.GroupBox gCheck;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numSpace;
        private System.Windows.Forms.NumericUpDown numPoint;
        private System.Windows.Forms.Button bConfig;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bEnable;
        private System.Windows.Forms.Button bFit;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bGoSelect;
        private System.Windows.Forms.Button bRefresh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbVisionList;
    }
}
