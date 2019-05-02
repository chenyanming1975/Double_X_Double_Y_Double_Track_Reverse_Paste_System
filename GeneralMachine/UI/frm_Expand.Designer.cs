namespace GeneralMachine
{
    partial class frm_Expand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Expand));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.msgDiv1 = new MsgDiv();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tM = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tN = new System.Windows.Forms.TextBox();
            this.yPos = new GeneralMachine.BasicUI.XYPos();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xPos = new GeneralMachine.BasicUI.XYPos();
            this.orgPos = new GeneralMachine.BasicUI.XYPos();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::GeneralMachine.Properties.Resources.Expand;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(355, 246);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 836;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.msgDiv1);
            this.groupBox10.Controls.Add(this.bCancel);
            this.groupBox10.Controls.Add(this.bOK);
            this.groupBox10.Controls.Add(this.panel2);
            this.groupBox10.Controls.Add(this.panel1);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox10.Font = new System.Drawing.Font("SimSun", 10F);
            this.groupBox10.Location = new System.Drawing.Point(0, 246);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(355, 281);
            this.groupBox10.TabIndex = 837;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "扩展点";
            // 
            // msgDiv1
            // 
            this.msgDiv1.AutoSize = true;
            this.msgDiv1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.msgDiv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msgDiv1.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.msgDiv1.ForeColor = System.Drawing.Color.Red;
            this.msgDiv1.Location = new System.Drawing.Point(33, 239);
            this.msgDiv1.MaximumSize = new System.Drawing.Size(980, 525);
            this.msgDiv1.Name = "msgDiv1";
            this.msgDiv1.Padding = new System.Windows.Forms.Padding(7);
            this.msgDiv1.Size = new System.Drawing.Size(86, 31);
            this.msgDiv1.TabIndex = 837;
            this.msgDiv1.Text = "msgDiv1";
            this.msgDiv1.Visible = false;
            // 
            // bCancel
            // 
            this.bCancel.Font = new System.Drawing.Font("Tahoma", 12F);
            this.bCancel.Location = new System.Drawing.Point(256, 231);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(94, 46);
            this.bCancel.TabIndex = 836;
            this.bCancel.Text = "取消";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOK
            // 
            this.bOK.BackColor = System.Drawing.Color.LightGreen;
            this.bOK.Font = new System.Drawing.Font("Tahoma", 12F);
            this.bOK.Location = new System.Drawing.Point(149, 231);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(94, 46);
            this.bOK.TabIndex = 835;
            this.bOK.Text = "确认";
            this.bOK.UseVisualStyleBackColor = false;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.yPos);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 122);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(349, 103);
            this.panel2.TabIndex = 834;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tM);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.tN);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(158, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(191, 103);
            this.groupBox3.TabIndex = 833;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "点数目";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 12F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(5, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 280;
            this.label3.Text = "M:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tM
            // 
            this.tM.Font = new System.Drawing.Font("SimSun", 12F);
            this.tM.Location = new System.Drawing.Point(30, 50);
            this.tM.Name = "tM";
            this.tM.Size = new System.Drawing.Size(99, 26);
            this.tM.TabIndex = 279;
            this.tM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 12F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(5, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 278;
            this.label4.Text = "N:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tN
            // 
            this.tN.Font = new System.Drawing.Font("SimSun", 12F);
            this.tN.Location = new System.Drawing.Point(30, 18);
            this.tN.Name = "tN";
            this.tN.Size = new System.Drawing.Size(99, 26);
            this.tN.TabIndex = 277;
            this.tN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // yPos
            // 
            this.yPos.Dock = System.Windows.Forms.DockStyle.Left;
            this.yPos.Location = new System.Drawing.Point(0, 0);
            this.yPos.Name = "yPos";
            this.yPos.Point = ((System.Drawing.PointF)(resources.GetObject("yPos.Point")));
            this.yPos.Size = new System.Drawing.Size(158, 103);
            this.yPos.TabIndex = 834;
            this.yPos.TitleName = "纵坐标";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xPos);
            this.panel1.Controls.Add(this.orgPos);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(349, 103);
            this.panel1.TabIndex = 833;
            // 
            // xPos
            // 
            this.xPos.Dock = System.Windows.Forms.DockStyle.Left;
            this.xPos.Location = new System.Drawing.Point(158, 0);
            this.xPos.Name = "xPos";
            this.xPos.Point = ((System.Drawing.PointF)(resources.GetObject("xPos.Point")));
            this.xPos.Size = new System.Drawing.Size(184, 103);
            this.xPos.TabIndex = 1;
            this.xPos.TitleName = "横坐标";
            // 
            // orgPos
            // 
            this.orgPos.Dock = System.Windows.Forms.DockStyle.Left;
            this.orgPos.Location = new System.Drawing.Point(0, 0);
            this.orgPos.Name = "orgPos";
            this.orgPos.Point = ((System.Drawing.PointF)(resources.GetObject("orgPos.Point")));
            this.orgPos.Size = new System.Drawing.Size(158, 103);
            this.orgPos.TabIndex = 0;
            this.orgPos.TitleName = "原点";
            // 
            // frm_Expand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 589);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_Expand";
            this.Text = "扩展点位";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox10;
        private MsgDiv msgDiv1;
        public System.Windows.Forms.Button bCancel;
        public System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox tM;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox tN;
        private System.Windows.Forms.Panel panel1;
        private BasicUI.XYPos xPos;
        private BasicUI.XYPos orgPos;
        private BasicUI.XYPos yPos;
    }
}