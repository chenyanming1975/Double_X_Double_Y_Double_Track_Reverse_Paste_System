namespace GeneralMachine.Flow.Editer
{
    partial class frmPasteReCheck
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
            this.dGVMark = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bGoPaste = new System.Windows.Forms.Button();
            this.dGVPaste = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bGoMark2 = new System.Windows.Forms.Button();
            this.bGoMark1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lCur = new System.Windows.Forms.Label();
            this.bPrev = new System.Windows.Forms.Button();
            this.bNext = new System.Windows.Forms.Button();
            this.bUpGrab = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.bGetPaste = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bUpdateToFlow = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.bFull = new System.Windows.Forms.Button();
            this.bSetToSelect = new System.Windows.Forms.Button();
            this.bSetToNz4 = new System.Windows.Forms.Button();
            this.bSetToNz3 = new System.Windows.Forms.Button();
            this.bSetToNz2 = new System.Windows.Forms.Button();
            this.bSetToNz1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tOffsetY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tOffsetX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.imageSet = new NationalInstruments.Vision.WindowsForms.ImageViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dGVMark)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVPaste)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dGVMark
            // 
            this.dGVMark.AllowUserToAddRows = false;
            this.dGVMark.AllowUserToDeleteRows = false;
            this.dGVMark.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVMark.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dGVMark.Dock = System.Windows.Forms.DockStyle.Left;
            this.dGVMark.Location = new System.Drawing.Point(0, 0);
            this.dGVMark.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dGVMark.Name = "dGVMark";
            this.dGVMark.ReadOnly = true;
            this.dGVMark.RowHeadersVisible = false;
            this.dGVMark.RowTemplate.Height = 23;
            this.dGVMark.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVMark.Size = new System.Drawing.Size(308, 201);
            this.dGVMark.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "板号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Mark1";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Mark2";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(395, 666);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "识别点位";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bGoPaste);
            this.panel2.Controls.Add(this.dGVPaste);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 225);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(387, 436);
            this.panel2.TabIndex = 3;
            // 
            // bGoPaste
            // 
            this.bGoPaste.Dock = System.Windows.Forms.DockStyle.Top;
            this.bGoPaste.Location = new System.Drawing.Point(308, 0);
            this.bGoPaste.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bGoPaste.Name = "bGoPaste";
            this.bGoPaste.Size = new System.Drawing.Size(79, 38);
            this.bGoPaste.TabIndex = 2;
            this.bGoPaste.Text = "到贴附点";
            this.bGoPaste.UseVisualStyleBackColor = true;
            this.bGoPaste.Click += new System.EventHandler(this.bGoPaste_Click);
            // 
            // dGVPaste
            // 
            this.dGVPaste.AllowUserToAddRows = false;
            this.dGVPaste.AllowUserToDeleteRows = false;
            this.dGVPaste.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVPaste.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dGVPaste.Dock = System.Windows.Forms.DockStyle.Left;
            this.dGVPaste.Location = new System.Drawing.Point(0, 0);
            this.dGVPaste.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dGVPaste.Name = "dGVPaste";
            this.dGVPaste.ReadOnly = true;
            this.dGVPaste.RowHeadersVisible = false;
            this.dGVPaste.RowTemplate.Height = 23;
            this.dGVPaste.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVPaste.Size = new System.Drawing.Size(308, 436);
            this.dGVPaste.TabIndex = 1;
            this.dGVPaste.SelectionChanged += new System.EventHandler(this.dGVPaste_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "板号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "穴位号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "贴附坐标";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "吸嘴";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Feeder";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bGoMark2);
            this.panel1.Controls.Add(this.bGoMark1);
            this.panel1.Controls.Add(this.dGVMark);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 201);
            this.panel1.TabIndex = 2;
            // 
            // bGoMark2
            // 
            this.bGoMark2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bGoMark2.Location = new System.Drawing.Point(308, 38);
            this.bGoMark2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bGoMark2.Name = "bGoMark2";
            this.bGoMark2.Size = new System.Drawing.Size(79, 38);
            this.bGoMark2.TabIndex = 2;
            this.bGoMark2.Text = "到Mark2";
            this.bGoMark2.UseVisualStyleBackColor = true;
            this.bGoMark2.Click += new System.EventHandler(this.bGoMark2_Click);
            // 
            // bGoMark1
            // 
            this.bGoMark1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bGoMark1.Location = new System.Drawing.Point(308, 0);
            this.bGoMark1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bGoMark1.Name = "bGoMark1";
            this.bGoMark1.Size = new System.Drawing.Size(79, 38);
            this.bGoMark1.TabIndex = 1;
            this.bGoMark1.Text = "到Mark1";
            this.bGoMark1.UseVisualStyleBackColor = true;
            this.bGoMark1.Click += new System.EventHandler(this.bGoMark1_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lCur);
            this.panel3.Controls.Add(this.bPrev);
            this.panel3.Controls.Add(this.bNext);
            this.panel3.Controls.Add(this.bUpGrab);
            this.panel3.Controls.Add(this.numericUpDown1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(395, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(674, 42);
            this.panel3.TabIndex = 2;
            // 
            // lCur
            // 
            this.lCur.AutoSize = true;
            this.lCur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lCur.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lCur.Location = new System.Drawing.Point(276, 9);
            this.lCur.Name = "lCur";
            this.lCur.Size = new System.Drawing.Size(196, 25);
            this.lCur.TabIndex = 5;
            this.lCur.Text = "当前第 [1] 板第 [1] 个";
            // 
            // bPrev
            // 
            this.bPrev.Location = new System.Drawing.Point(483, 7);
            this.bPrev.Name = "bPrev";
            this.bPrev.Size = new System.Drawing.Size(91, 29);
            this.bPrev.TabIndex = 4;
            this.bPrev.Text = "上一个";
            this.bPrev.UseVisualStyleBackColor = true;
            this.bPrev.Click += new System.EventHandler(this.bPrev_Click);
            // 
            // bNext
            // 
            this.bNext.Location = new System.Drawing.Point(580, 7);
            this.bNext.Name = "bNext";
            this.bNext.Size = new System.Drawing.Size(91, 29);
            this.bNext.TabIndex = 3;
            this.bNext.Text = "下一个";
            this.bNext.UseVisualStyleBackColor = true;
            this.bNext.Click += new System.EventHandler(this.bNext_Click);
            // 
            // bUpGrab
            // 
            this.bUpGrab.Location = new System.Drawing.Point(178, 8);
            this.bUpGrab.Name = "bUpGrab";
            this.bUpGrab.Size = new System.Drawing.Size(91, 29);
            this.bUpGrab.TabIndex = 2;
            this.bUpGrab.Text = "上视觉拍照";
            this.bUpGrab.UseVisualStyleBackColor = true;
            this.bUpGrab.Click += new System.EventHandler(this.bUpGrab_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 1;
            this.numericUpDown1.Location = new System.Drawing.Point(99, 10);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            135,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(73, 26);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "坐标系角度:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bGetPaste
            // 
            this.bGetPaste.Location = new System.Drawing.Point(26, 72);
            this.bGetPaste.Name = "bGetPaste";
            this.bGetPaste.Size = new System.Drawing.Size(95, 29);
            this.bGetPaste.TabIndex = 5;
            this.bGetPaste.Text = "拾取贴附位置";
            this.bGetPaste.UseVisualStyleBackColor = true;
            this.bGetPaste.Click += new System.EventHandler(this.bGetPaste_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.bUpdateToFlow);
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Controls.Add(this.bFull);
            this.panel4.Controls.Add(this.bSetToSelect);
            this.panel4.Controls.Add(this.bSetToNz4);
            this.panel4.Controls.Add(this.bSetToNz3);
            this.panel4.Controls.Add(this.bSetToNz2);
            this.panel4.Controls.Add(this.bSetToNz1);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.bGetPaste);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(936, 42);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(133, 624);
            this.panel4.TabIndex = 3;
            // 
            // bUpdateToFlow
            // 
            this.bUpdateToFlow.BackColor = System.Drawing.Color.Yellow;
            this.bUpdateToFlow.Location = new System.Drawing.Point(4, 573);
            this.bUpdateToFlow.Name = "bUpdateToFlow";
            this.bUpdateToFlow.Size = new System.Drawing.Size(126, 46);
            this.bUpdateToFlow.TabIndex = 16;
            this.bUpdateToFlow.Text = "更新到程式";
            this.bUpdateToFlow.UseVisualStyleBackColor = false;
            this.bUpdateToFlow.Click += new System.EventHandler(this.bUpdateToFlow_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(23, 42);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(98, 24);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "显示十字架";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // bFull
            // 
            this.bFull.BackColor = System.Drawing.Color.Yellow;
            this.bFull.Location = new System.Drawing.Point(26, 6);
            this.bFull.Name = "bFull";
            this.bFull.Size = new System.Drawing.Size(95, 29);
            this.bFull.TabIndex = 14;
            this.bFull.Text = "Full";
            this.bFull.UseVisualStyleBackColor = false;
            this.bFull.Click += new System.EventHandler(this.bFull_Click);
            // 
            // bSetToSelect
            // 
            this.bSetToSelect.BackColor = System.Drawing.Color.Red;
            this.bSetToSelect.Location = new System.Drawing.Point(26, 364);
            this.bSetToSelect.Name = "bSetToSelect";
            this.bSetToSelect.Size = new System.Drawing.Size(95, 50);
            this.bSetToSelect.TabIndex = 13;
            this.bSetToSelect.Text = "应用到已选择贴附位";
            this.bSetToSelect.UseVisualStyleBackColor = false;
            this.bSetToSelect.Click += new System.EventHandler(this.bSetToSelect_Click);
            // 
            // bSetToNz4
            // 
            this.bSetToNz4.BackColor = System.Drawing.Color.Red;
            this.bSetToNz4.Location = new System.Drawing.Point(26, 329);
            this.bSetToNz4.Name = "bSetToNz4";
            this.bSetToNz4.Size = new System.Drawing.Size(95, 29);
            this.bSetToNz4.TabIndex = 12;
            this.bSetToNz4.Text = "应用到吸嘴4";
            this.bSetToNz4.UseVisualStyleBackColor = false;
            this.bSetToNz4.Click += new System.EventHandler(this.bSetToNz4_Click);
            // 
            // bSetToNz3
            // 
            this.bSetToNz3.BackColor = System.Drawing.Color.Red;
            this.bSetToNz3.Location = new System.Drawing.Point(26, 294);
            this.bSetToNz3.Name = "bSetToNz3";
            this.bSetToNz3.Size = new System.Drawing.Size(95, 29);
            this.bSetToNz3.TabIndex = 11;
            this.bSetToNz3.Text = "应用到吸嘴3";
            this.bSetToNz3.UseVisualStyleBackColor = false;
            this.bSetToNz3.Click += new System.EventHandler(this.bSetToNz3_Click);
            // 
            // bSetToNz2
            // 
            this.bSetToNz2.BackColor = System.Drawing.Color.Red;
            this.bSetToNz2.Location = new System.Drawing.Point(26, 259);
            this.bSetToNz2.Name = "bSetToNz2";
            this.bSetToNz2.Size = new System.Drawing.Size(95, 29);
            this.bSetToNz2.TabIndex = 10;
            this.bSetToNz2.Text = "应用到吸嘴2";
            this.bSetToNz2.UseVisualStyleBackColor = false;
            this.bSetToNz2.Click += new System.EventHandler(this.bSetToNz2_Click);
            // 
            // bSetToNz1
            // 
            this.bSetToNz1.BackColor = System.Drawing.Color.Red;
            this.bSetToNz1.Location = new System.Drawing.Point(26, 224);
            this.bSetToNz1.Name = "bSetToNz1";
            this.bSetToNz1.Size = new System.Drawing.Size(95, 29);
            this.bSetToNz1.TabIndex = 9;
            this.bSetToNz1.Text = "应用到吸嘴1";
            this.bSetToNz1.UseVisualStyleBackColor = false;
            this.bSetToNz1.Click += new System.EventHandler(this.bSetToNz1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tOffsetY);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tOffsetX);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(4, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 95);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "误差";
            // 
            // tOffsetY
            // 
            this.tOffsetY.Location = new System.Drawing.Point(38, 58);
            this.tOffsetY.Name = "tOffsetY";
            this.tOffsetY.ReadOnly = true;
            this.tOffsetY.Size = new System.Drawing.Size(68, 26);
            this.tOffsetY.TabIndex = 9;
            this.tOffsetY.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Y:";
            // 
            // tOffsetX
            // 
            this.tOffsetX.Location = new System.Drawing.Point(38, 25);
            this.tOffsetX.Name = "tOffsetX";
            this.tOffsetX.ReadOnly = true;
            this.tOffsetX.Size = new System.Drawing.Size(68, 26);
            this.tOffsetX.TabIndex = 7;
            this.tOffsetX.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "X:";
            // 
            // imageSet
            // 
            this.imageSet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageSet.Location = new System.Drawing.Point(395, 42);
            this.imageSet.Margin = new System.Windows.Forms.Padding(0);
            this.imageSet.Name = "imageSet";
            this.imageSet.ShowImageInfo = true;
            this.imageSet.ShowToolbar = true;
            this.imageSet.Size = new System.Drawing.Size(541, 624);
            this.imageSet.TabIndex = 295;
            this.imageSet.ImageMouseDown += new System.EventHandler<NationalInstruments.Vision.WindowsForms.ImageMouseEventArgs>(this.imageSet_ImageMouseDown);
            this.imageSet.ImageMouseMove += new System.EventHandler<NationalInstruments.Vision.WindowsForms.ImageMouseEventArgs>(this.imageSet_ImageMouseMove);
            this.imageSet.Resize += new System.EventHandler(this.imageSet_Resize);
            // 
            // frmPasteReCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 666);
            this.Controls.Add(this.imageSet);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmPasteReCheck";
            this.Text = "回看";
            this.Load += new System.EventHandler(this.frmPasteReCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVMark)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGVPaste)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bGoPaste;
        private System.Windows.Forms.DataGridView dGVPaste;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bGoMark2;
        private System.Windows.Forms.Button bGoMark1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bUpGrab;
        private System.Windows.Forms.Button bPrev;
        private System.Windows.Forms.Button bNext;
        private System.Windows.Forms.Button bGetPaste;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tOffsetY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tOffsetX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bSetToSelect;
        private System.Windows.Forms.Button bSetToNz4;
        private System.Windows.Forms.Button bSetToNz3;
        private System.Windows.Forms.Button bSetToNz2;
        private System.Windows.Forms.Button bSetToNz1;
        public NationalInstruments.Vision.WindowsForms.ImageViewer imageSet;
        private System.Windows.Forms.Label lCur;
        private System.Windows.Forms.Button bFull;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button bUpdateToFlow;
    }
}