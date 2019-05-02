namespace GeneralMachine
{
    partial class frm_LoadCell
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dGV_Press = new System.Windows.Forms.DataGridView();
            this.col1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tBaseZ = new System.Windows.Forms.TextBox();
            this.tK = new System.Windows.Forms.TextBox();
            this.tD = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tSetZ = new System.Windows.Forms.Button();
            this.bTest1 = new System.Windows.Forms.Button();
            this.bTest2 = new System.Windows.Forms.Button();
            this.bTest3 = new System.Windows.Forms.Button();
            this.bTest4 = new System.Windows.Forms.Button();
            this.bTest5 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lPressResult = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bGetK = new System.Windows.Forms.Button();
            this.ChartPaneFront = new ZedGraph.ZedGraphControl();
            this.moduleRadio1 = new GeneralMachine.BasicUI.ModuleRadio();
            this.nozzleRadio1 = new GeneralMachine.BasicUI.NozzleRadio();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Press)).BeginInit();
            this.SuspendLayout();
            // 
            // dGV_Press
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGV_Press.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dGV_Press.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_Press.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col1,
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGV_Press.DefaultCellStyle = dataGridViewCellStyle2;
            this.dGV_Press.Dock = System.Windows.Forms.DockStyle.Left;
            this.dGV_Press.Location = new System.Drawing.Point(0, 0);
            this.dGV_Press.Name = "dGV_Press";
            this.dGV_Press.RowTemplate.Height = 23;
            this.dGV_Press.Size = new System.Drawing.Size(298, 688);
            this.dGV_Press.TabIndex = 4;
            // 
            // col1
            // 
            this.col1.FillWeight = 60F;
            this.col1.HeaderText = "Z轴高度";
            this.col1.Name = "col1";
            this.col1.Width = 60;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 60F;
            this.Column1.HeaderText = "机器压力值";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 60F;
            this.Column2.HeaderText = "设备压力值";
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 60F;
            this.Column3.HeaderText = "校验后压力";
            this.Column3.Name = "Column3";
            this.Column3.Width = 60;
            // 
            // tBaseZ
            // 
            this.tBaseZ.Location = new System.Drawing.Point(490, 97);
            this.tBaseZ.Name = "tBaseZ";
            this.tBaseZ.Size = new System.Drawing.Size(78, 21);
            this.tBaseZ.TabIndex = 7;
            // 
            // tK
            // 
            this.tK.Location = new System.Drawing.Point(490, 230);
            this.tK.Name = "tK";
            this.tK.Size = new System.Drawing.Size(78, 21);
            this.tK.TabIndex = 16;
            // 
            // tD
            // 
            this.tD.Location = new System.Drawing.Point(490, 262);
            this.tD.Name = "tD";
            this.tD.Size = new System.Drawing.Size(78, 21);
            this.tD.TabIndex = 17;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(490, 295);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(78, 21);
            this.textBox4.TabIndex = 19;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(355, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Z轴基础高度:";
            // 
            // tSetZ
            // 
            this.tSetZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tSetZ.Location = new System.Drawing.Point(574, 96);
            this.tSetZ.Name = "tSetZ";
            this.tSetZ.Size = new System.Drawing.Size(60, 23);
            this.tSetZ.TabIndex = 8;
            this.tSetZ.Text = "Set";
            this.tSetZ.UseVisualStyleBackColor = true;
            this.tSetZ.Click += new System.EventHandler(this.tSetZ_Click);
            // 
            // bTest1
            // 
            this.bTest1.BackColor = System.Drawing.Color.Yellow;
            this.bTest1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTest1.Location = new System.Drawing.Point(359, 149);
            this.bTest1.Name = "bTest1";
            this.bTest1.Size = new System.Drawing.Size(84, 30);
            this.bTest1.TabIndex = 9;
            this.bTest1.Text = "0.0mm 测试";
            this.bTest1.UseVisualStyleBackColor = false;
            this.bTest1.Click += new System.EventHandler(this.bTest1_Click);
            // 
            // bTest2
            // 
            this.bTest2.BackColor = System.Drawing.Color.Yellow;
            this.bTest2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTest2.Location = new System.Drawing.Point(449, 149);
            this.bTest2.Name = "bTest2";
            this.bTest2.Size = new System.Drawing.Size(84, 30);
            this.bTest2.TabIndex = 10;
            this.bTest2.Text = "0.5mm 测试";
            this.bTest2.UseVisualStyleBackColor = false;
            this.bTest2.Click += new System.EventHandler(this.bTest2_Click);
            // 
            // bTest3
            // 
            this.bTest3.BackColor = System.Drawing.Color.Yellow;
            this.bTest3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTest3.Location = new System.Drawing.Point(539, 149);
            this.bTest3.Name = "bTest3";
            this.bTest3.Size = new System.Drawing.Size(84, 30);
            this.bTest3.TabIndex = 11;
            this.bTest3.Text = "1.0mm 测试";
            this.bTest3.UseVisualStyleBackColor = false;
            this.bTest3.Click += new System.EventHandler(this.bTest3_Click);
            // 
            // bTest4
            // 
            this.bTest4.BackColor = System.Drawing.Color.Yellow;
            this.bTest4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTest4.Location = new System.Drawing.Point(359, 185);
            this.bTest4.Name = "bTest4";
            this.bTest4.Size = new System.Drawing.Size(84, 30);
            this.bTest4.TabIndex = 12;
            this.bTest4.Text = "1.5mm 测试";
            this.bTest4.UseVisualStyleBackColor = false;
            this.bTest4.Click += new System.EventHandler(this.bTest4_Click);
            // 
            // bTest5
            // 
            this.bTest5.BackColor = System.Drawing.Color.Yellow;
            this.bTest5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTest5.Location = new System.Drawing.Point(449, 185);
            this.bTest5.Name = "bTest5";
            this.bTest5.Size = new System.Drawing.Size(84, 30);
            this.bTest5.TabIndex = 13;
            this.bTest5.Text = "2.0mm 测试";
            this.bTest5.UseVisualStyleBackColor = false;
            this.bTest5.Click += new System.EventHandler(this.bTest5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(405, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "线性 K:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(405, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "线性 D:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(365, 295);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "输入值验证:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(569, 295);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 20);
            this.label7.TabIndex = 20;
            this.label7.Text = "=>";
            // 
            // lPressResult
            // 
            this.lPressResult.AutoSize = true;
            this.lPressResult.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lPressResult.Location = new System.Drawing.Point(604, 295);
            this.lPressResult.Name = "lPressResult";
            this.lPressResult.Size = new System.Drawing.Size(19, 20);
            this.lPressResult.TabIndex = 21;
            this.lPressResult.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(640, 295);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 20);
            this.label9.TabIndex = 22;
            this.label9.Text = "g";
            // 
            // bGetK
            // 
            this.bGetK.BackColor = System.Drawing.Color.Yellow;
            this.bGetK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGetK.Location = new System.Drawing.Point(539, 185);
            this.bGetK.Name = "bGetK";
            this.bGetK.Size = new System.Drawing.Size(84, 30);
            this.bGetK.TabIndex = 23;
            this.bGetK.Text = "计算线性";
            this.bGetK.UseVisualStyleBackColor = false;
            this.bGetK.Click += new System.EventHandler(this.bGetK_Click);
            // 
            // ChartPaneFront
            // 
            this.ChartPaneFront.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChartPaneFront.Location = new System.Drawing.Point(298, 356);
            this.ChartPaneFront.Name = "ChartPaneFront";
            this.ChartPaneFront.ScrollGrace = 0D;
            this.ChartPaneFront.ScrollMaxX = 0D;
            this.ChartPaneFront.ScrollMaxY = 0D;
            this.ChartPaneFront.ScrollMaxY2 = 0D;
            this.ChartPaneFront.ScrollMinX = 0D;
            this.ChartPaneFront.ScrollMinY = 0D;
            this.ChartPaneFront.ScrollMinY2 = 0D;
            this.ChartPaneFront.Size = new System.Drawing.Size(376, 332);
            this.ChartPaneFront.TabIndex = 29;
            this.ChartPaneFront.UseExtendedPrintDialog = true;
            // 
            // moduleRadio1
            // 
            this.moduleRadio1.Dock = System.Windows.Forms.DockStyle.Top;
            this.moduleRadio1.Location = new System.Drawing.Point(298, 0);
            this.moduleRadio1.Name = "moduleRadio1";
            this.moduleRadio1.Size = new System.Drawing.Size(376, 36);
            this.moduleRadio1.TabIndex = 30;
            // 
            // nozzleRadio1
            // 
            this.nozzleRadio1.Dock = System.Windows.Forms.DockStyle.Top;
            this.nozzleRadio1.Location = new System.Drawing.Point(298, 36);
            this.nozzleRadio1.Name = "nozzleRadio1";
            this.nozzleRadio1.SelectNz = GeneralMachine.Config.Nozzle.Nz1;
            this.nozzleRadio1.Size = new System.Drawing.Size(376, 37);
            this.nozzleRadio1.TabIndex = 31;
            // 
            // frm_LoadCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.nozzleRadio1);
            this.Controls.Add(this.moduleRadio1);
            this.Controls.Add(this.ChartPaneFront);
            this.Controls.Add(this.bGetK);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lPressResult);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tD);
            this.Controls.Add(this.tK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bTest5);
            this.Controls.Add(this.bTest4);
            this.Controls.Add(this.bTest3);
            this.Controls.Add(this.bTest2);
            this.Controls.Add(this.bTest1);
            this.Controls.Add(this.tSetZ);
            this.Controls.Add(this.tBaseZ);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dGV_Press);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frm_LoadCell";
            this.Size = new System.Drawing.Size(674, 688);
            this.Load += new System.EventHandler(this.frm_LoadCell_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Press)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGV_Press;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBaseZ;
        private System.Windows.Forms.Button tSetZ;
        private System.Windows.Forms.Button bTest1;
        private System.Windows.Forms.Button bTest2;
        private System.Windows.Forms.Button bTest3;
        private System.Windows.Forms.Button bTest4;
        private System.Windows.Forms.Button bTest5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tK;
        private System.Windows.Forms.TextBox tD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lPressResult;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bGetK;
        public ZedGraph.ZedGraphControl ChartPaneFront;
        private System.Windows.Forms.DataGridViewTextBoxColumn col1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private BasicUI.ModuleRadio moduleRadio1;
        private BasicUI.NozzleRadio nozzleRadio1;
    }
}