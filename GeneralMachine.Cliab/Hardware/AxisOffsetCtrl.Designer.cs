namespace GeneralMachine.Cliab
{
    partial class AxisOffsetCtrl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.gTitle = new System.Windows.Forms.GroupBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bAdd = new System.Windows.Forms.Button();
            this.bCliab = new System.Windows.Forms.Button();
            this.bDown = new System.Windows.Forms.Button();
            this.bUp = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.dGVAxisOffset = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVAxisOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // gTitle
            // 
            this.gTitle.Controls.Add(this.chart1);
            this.gTitle.Controls.Add(this.panel1);
            this.gTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gTitle.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gTitle.Location = new System.Drawing.Point(0, 0);
            this.gTitle.Name = "gTitle";
            this.gTitle.Size = new System.Drawing.Size(593, 424);
            this.gTitle.TabIndex = 0;
            this.gTitle.TabStop = false;
            this.gTitle.Text = "{0}轴机械误差校正";
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(261, 30);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(329, 391);
            this.chart1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bAdd);
            this.panel1.Controls.Add(this.bCliab);
            this.panel1.Controls.Add(this.bDown);
            this.panel1.Controls.Add(this.bUp);
            this.panel1.Controls.Add(this.bClear);
            this.panel1.Controls.Add(this.dGVAxisOffset);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(3, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 391);
            this.panel1.TabIndex = 0;
            // 
            // bAdd
            // 
            this.bAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.bAdd.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bAdd.Location = new System.Drawing.Point(204, 58);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(54, 29);
            this.bAdd.TabIndex = 5;
            this.bAdd.Text = "添加";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bCliab
            // 
            this.bCliab.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bCliab.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bCliab.Location = new System.Drawing.Point(204, 333);
            this.bCliab.Name = "bCliab";
            this.bCliab.Size = new System.Drawing.Size(54, 29);
            this.bCliab.TabIndex = 4;
            this.bCliab.Text = "校正";
            this.bCliab.UseVisualStyleBackColor = true;
            this.bCliab.Click += new System.EventHandler(this.bCliab_Click);
            // 
            // bDown
            // 
            this.bDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.bDown.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bDown.Location = new System.Drawing.Point(204, 29);
            this.bDown.Name = "bDown";
            this.bDown.Size = new System.Drawing.Size(54, 29);
            this.bDown.TabIndex = 3;
            this.bDown.Text = "下移";
            this.bDown.UseVisualStyleBackColor = true;
            this.bDown.Click += new System.EventHandler(this.bDown_Click);
            // 
            // bUp
            // 
            this.bUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.bUp.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bUp.Location = new System.Drawing.Point(204, 0);
            this.bUp.Name = "bUp";
            this.bUp.Size = new System.Drawing.Size(54, 29);
            this.bUp.TabIndex = 2;
            this.bUp.Text = "上移";
            this.bUp.UseVisualStyleBackColor = true;
            this.bUp.Click += new System.EventHandler(this.bUp_Click);
            // 
            // bClear
            // 
            this.bClear.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bClear.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bClear.Location = new System.Drawing.Point(204, 362);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(54, 29);
            this.bClear.TabIndex = 1;
            this.bClear.Text = "清空";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // dGVAxisOffset
            // 
            this.dGVAxisOffset.AllowUserToAddRows = false;
            this.dGVAxisOffset.AllowUserToDeleteRows = false;
            this.dGVAxisOffset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVAxisOffset.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dGVAxisOffset.Dock = System.Windows.Forms.DockStyle.Left;
            this.dGVAxisOffset.Location = new System.Drawing.Point(0, 0);
            this.dGVAxisOffset.Name = "dGVAxisOffset";
            this.dGVAxisOffset.ReadOnly = true;
            this.dGVAxisOffset.RowTemplate.Height = 23;
            this.dGVAxisOffset.Size = new System.Drawing.Size(204, 391);
            this.dGVAxisOffset.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "位移量";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "位移误差";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // AxisOffsetCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gTitle);
            this.Name = "AxisOffsetCtrl";
            this.Size = new System.Drawing.Size(593, 424);
            this.gTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGVAxisOffset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dGVAxisOffset;
        private System.Windows.Forms.Button bDown;
        private System.Windows.Forms.Button bUp;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bCliab;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}
