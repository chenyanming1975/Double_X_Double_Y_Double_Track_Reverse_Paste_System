namespace GeneralMachine.BasicUI
{
    partial class XYPos
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
            this.title = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bGo = new System.Windows.Forms.Button();
            this.bSet = new System.Windows.Forms.Button();
            this.tY = new System.Windows.Forms.TextBox();
            this.lY = new System.Windows.Forms.Label();
            this.lX = new System.Windows.Forms.Label();
            this.tX = new System.Windows.Forms.TextBox();
            this.title.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Controls.Add(this.tableLayoutPanel1);
            this.title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.title.Name = "title";
            this.title.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.title.Size = new System.Drawing.Size(185, 105);
            this.title.TabIndex = 0;
            this.title.TabStop = false;
            this.title.Text = "Name";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.bGo, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.bSet, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tY, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lY, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lX, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tX, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 24);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(177, 76);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // bGo
            // 
            this.bGo.BackColor = System.Drawing.Color.White;
            this.bGo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGo.Location = new System.Drawing.Point(127, 43);
            this.bGo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bGo.Name = "bGo";
            this.bGo.Size = new System.Drawing.Size(46, 28);
            this.bGo.TabIndex = 5;
            this.bGo.Text = "Go";
            this.bGo.UseVisualStyleBackColor = false;
            this.bGo.Click += new System.EventHandler(this.bGo_Click);
            // 
            // bSet
            // 
            this.bSet.BackColor = System.Drawing.Color.Yellow;
            this.bSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSet.Location = new System.Drawing.Point(127, 5);
            this.bSet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSet.Name = "bSet";
            this.bSet.Size = new System.Drawing.Size(46, 28);
            this.bSet.TabIndex = 4;
            this.bSet.Text = "Set";
            this.bSet.UseVisualStyleBackColor = false;
            this.bSet.Click += new System.EventHandler(this.bSet_Click);
            // 
            // tY
            // 
            this.tY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tY.Location = new System.Drawing.Point(39, 43);
            this.tY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tY.Name = "tY";
            this.tY.Size = new System.Drawing.Size(80, 26);
            this.tY.TabIndex = 3;
            // 
            // lY
            // 
            this.lY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lY.Location = new System.Drawing.Point(4, 38);
            this.lY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lY.Name = "lY";
            this.lY.Size = new System.Drawing.Size(27, 38);
            this.lY.TabIndex = 1;
            this.lY.Text = "Y:";
            this.lY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lX
            // 
            this.lX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lX.Location = new System.Drawing.Point(4, 0);
            this.lX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lX.Name = "lX";
            this.lX.Size = new System.Drawing.Size(27, 38);
            this.lX.TabIndex = 0;
            this.lX.Text = "X:";
            this.lX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tX
            // 
            this.tX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tX.Location = new System.Drawing.Point(39, 5);
            this.tX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tX.Name = "tX";
            this.tX.Size = new System.Drawing.Size(80, 26);
            this.tX.TabIndex = 2;
            // 
            // XYPos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "XYPos";
            this.Size = new System.Drawing.Size(185, 105);
            this.title.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox title;
        private System.Windows.Forms.Label lX;
        private System.Windows.Forms.Label lY;
        private System.Windows.Forms.Button bGo;
        private System.Windows.Forms.Button bSet;
        private System.Windows.Forms.TextBox tY;
        private System.Windows.Forms.TextBox tX;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
