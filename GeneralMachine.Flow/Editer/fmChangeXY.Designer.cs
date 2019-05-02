namespace GeneralMachine.Flow.Editer
{
    partial class fmChangeXY
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
            this.bCancel = new System.Windows.Forms.Button();
            this.bUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tX = new System.Windows.Forms.TextBox();
            this.tY = new System.Windows.Forms.TextBox();
            this.bSetCur = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numOffsetX = new System.Windows.Forms.NumericUpDown();
            this.numOffsetY = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetY)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tX, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tY, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numOffsetX, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.numOffsetY, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.bCancel, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.bUpdate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.bSetCur, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(397, 129);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // bCancel
            // 
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bCancel.Location = new System.Drawing.Point(240, 85);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(113, 41);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "取消";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bUpdate
            // 
            this.bUpdate.BackColor = System.Drawing.Color.Yellow;
            this.bUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUpdate.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bUpdate.Location = new System.Drawing.Point(82, 85);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(113, 41);
            this.bUpdate.TabIndex = 2;
            this.bUpdate.Text = "修改";
            this.bUpdate.UseVisualStyleBackColor = false;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 41);
            this.label1.TabIndex = 4;
            this.label1.Text = "X:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 41);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tX
            // 
            this.tX.Location = new System.Drawing.Point(82, 10);
            this.tX.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.tX.Name = "tX";
            this.tX.ReadOnly = true;
            this.tX.Size = new System.Drawing.Size(113, 26);
            this.tX.TabIndex = 6;
            // 
            // tY
            // 
            this.tY.Location = new System.Drawing.Point(82, 51);
            this.tY.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.tY.Name = "tY";
            this.tY.ReadOnly = true;
            this.tY.Size = new System.Drawing.Size(113, 26);
            this.tY.TabIndex = 7;
            // 
            // bSetCur
            // 
            this.bSetCur.BackColor = System.Drawing.Color.Yellow;
            this.bSetCur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSetCur.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bSetCur.Location = new System.Drawing.Point(3, 85);
            this.bSetCur.Name = "bSetCur";
            this.bSetCur.Size = new System.Drawing.Size(73, 41);
            this.bSetCur.TabIndex = 8;
            this.bSetCur.Text = "当前";
            this.bSetCur.UseVisualStyleBackColor = false;
            this.bSetCur.Click += new System.EventHandler(this.bSetCur_Click);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(201, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 41);
            this.label3.TabIndex = 10;
            this.label3.Text = "+";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(201, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 41);
            this.label4.TabIndex = 11;
            this.label4.Text = "+";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numOffsetX
            // 
            this.numOffsetX.DecimalPlaces = 2;
            this.numOffsetX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numOffsetX.Location = new System.Drawing.Point(240, 10);
            this.numOffsetX.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.numOffsetX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numOffsetX.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numOffsetX.Name = "numOffsetX";
            this.numOffsetX.Size = new System.Drawing.Size(113, 26);
            this.numOffsetX.TabIndex = 12;
            // 
            // numOffsetY
            // 
            this.numOffsetY.DecimalPlaces = 2;
            this.numOffsetY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numOffsetY.Location = new System.Drawing.Point(240, 51);
            this.numOffsetY.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.numOffsetY.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numOffsetY.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numOffsetY.Name = "numOffsetY";
            this.numOffsetY.Size = new System.Drawing.Size(113, 26);
            this.numOffsetY.TabIndex = 13;
            // 
            // fmChangeXY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(397, 129);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "fmChangeXY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fmChangeXY";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetY)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.NumericUpDown numOffsetY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tX;
        private System.Windows.Forms.TextBox tY;
        private System.Windows.Forms.NumericUpDown numOffsetX;
        private System.Windows.Forms.Button bSetCur;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}