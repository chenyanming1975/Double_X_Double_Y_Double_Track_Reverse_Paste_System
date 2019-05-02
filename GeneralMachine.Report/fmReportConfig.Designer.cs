namespace GeneralMachine.Report
{
    partial class fmReportConfig
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.numNight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numDay = new System.Windows.Forms.NumericUpDown();
            this.tPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bCanel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDay)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numNight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numDay, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tPath, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.bCanel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.bOK, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(291, 200);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "白板换班时间";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numNight
            // 
            this.numNight.Location = new System.Drawing.Point(145, 60);
            this.numNight.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.numNight.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numNight.Minimum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.numNight.Name = "numNight";
            this.numNight.Size = new System.Drawing.Size(73, 26);
            this.numNight.TabIndex = 3;
            this.numNight.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 50);
            this.label2.TabIndex = 2;
            this.label2.Text = "夜班换班时间";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numDay
            // 
            this.numDay.Location = new System.Drawing.Point(145, 10);
            this.numDay.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.numDay.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numDay.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numDay.Name = "numDay";
            this.numDay.Size = new System.Drawing.Size(73, 26);
            this.numDay.TabIndex = 1;
            this.numDay.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // tPath
            // 
            this.tPath.Location = new System.Drawing.Point(145, 110);
            this.tPath.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tPath.Name = "tPath";
            this.tPath.Size = new System.Drawing.Size(146, 26);
            this.tPath.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 50);
            this.label3.TabIndex = 5;
            this.label3.Text = "报表存储地址";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bCanel
            // 
            this.bCanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCanel.Location = new System.Drawing.Point(148, 153);
            this.bCanel.Name = "bCanel";
            this.bCanel.Size = new System.Drawing.Size(140, 44);
            this.bCanel.TabIndex = 6;
            this.bCanel.Text = "取消";
            this.bCanel.UseVisualStyleBackColor = true;
            this.bCanel.Click += new System.EventHandler(this.bCanel_Click);
            // 
            // bOK
            // 
            this.bOK.BackColor = System.Drawing.Color.Yellow;
            this.bOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOK.Location = new System.Drawing.Point(3, 153);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(139, 44);
            this.bOK.TabIndex = 7;
            this.bOK.Text = "确定";
            this.bOK.UseVisualStyleBackColor = false;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // fmReportConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(291, 200);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "fmReportConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fmReportConfig";
            this.Load += new System.EventHandler(this.fmReportConfig_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numNight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numDay;
        private System.Windows.Forms.TextBox tPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bCanel;
        private System.Windows.Forms.Button bOK;
    }
}