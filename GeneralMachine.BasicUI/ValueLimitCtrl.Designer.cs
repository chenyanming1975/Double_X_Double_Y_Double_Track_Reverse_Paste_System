namespace GeneralMachine.BasicUI
{
    partial class ValueLimitCtrl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bSetUpLimit = new System.Windows.Forms.Button();
            this.bSetLowLimt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tUp = new System.Windows.Forms.TextBox();
            this.tLow = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(201, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "极限";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.bSetUpLimit, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.bSetLowLimt, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tUp, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tLow, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 19);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(201, 66);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // bSetUpLimit
            // 
            this.bSetUpLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bSetUpLimit.BackColor = System.Drawing.Color.Yellow;
            this.bSetUpLimit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bSetUpLimit.Location = new System.Drawing.Point(134, 38);
            this.bSetUpLimit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSetUpLimit.Name = "bSetUpLimit";
            this.bSetUpLimit.Size = new System.Drawing.Size(63, 23);
            this.bSetUpLimit.TabIndex = 5;
            this.bSetUpLimit.Text = "设置";
            this.bSetUpLimit.UseVisualStyleBackColor = false;
            this.bSetUpLimit.Click += new System.EventHandler(this.bSetUpLimit_Click);
            // 
            // bSetLowLimt
            // 
            this.bSetLowLimt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bSetLowLimt.BackColor = System.Drawing.Color.Yellow;
            this.bSetLowLimt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bSetLowLimt.Location = new System.Drawing.Point(134, 5);
            this.bSetLowLimt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSetLowLimt.Name = "bSetLowLimt";
            this.bSetLowLimt.Size = new System.Drawing.Size(63, 23);
            this.bSetLowLimt.TabIndex = 2;
            this.bSetLowLimt.Text = "设置";
            this.bSetLowLimt.UseVisualStyleBackColor = false;
            this.bSetLowLimt.Click += new System.EventHandler(this.bSetLowLimt_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "下限:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tUp
            // 
            this.tUp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tUp.Location = new System.Drawing.Point(54, 38);
            this.tUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tUp.Name = "tUp";
            this.tUp.Size = new System.Drawing.Size(72, 26);
            this.tUp.TabIndex = 4;
            this.tUp.Text = "0";
            // 
            // tLow
            // 
            this.tLow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tLow.Location = new System.Drawing.Point(54, 5);
            this.tLow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tLow.Name = "tLow";
            this.tLow.Size = new System.Drawing.Size(72, 26);
            this.tLow.TabIndex = 1;
            this.tLow.Text = "0";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "上限:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ValueLimitCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ValueLimitCtrl";
            this.Size = new System.Drawing.Size(201, 85);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bSetLowLimt;
        private System.Windows.Forms.TextBox tLow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bSetUpLimit;
        private System.Windows.Forms.TextBox tUp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
