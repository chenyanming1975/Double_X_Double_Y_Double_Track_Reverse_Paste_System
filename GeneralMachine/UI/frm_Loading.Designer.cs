namespace GeneralMachine
{
    partial class frm_Loading
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bConfim = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pHostarLog = new System.Windows.Forms.PictureBox();
            this.pBar_Front = new System.Windows.Forms.ProgressBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.listAfter = new System.Windows.Forms.ListBox();
            this.pBar_After = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listFront = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHostarLog)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bConfim);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pHostarLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 81);
            this.panel1.TabIndex = 0;
            // 
            // bConfim
            // 
            this.bConfim.Dock = System.Windows.Forms.DockStyle.Top;
            this.bConfim.Font = new System.Drawing.Font("Tahoma", 22F);
            this.bConfim.Location = new System.Drawing.Point(301, 0);
            this.bConfim.Name = "bConfim";
            this.bConfim.Size = new System.Drawing.Size(251, 80);
            this.bConfim.TabIndex = 2;
            this.bConfim.Text = "确认";
            this.bConfim.Visible = false;
            this.bConfim.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = global::GeneralMachine.Properties.Resources.loading;
            this.pictureBox2.Location = new System.Drawing.Point(301, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(251, 81);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pHostarLog
            // 
            this.pHostarLog.Dock = System.Windows.Forms.DockStyle.Left;
            this.pHostarLog.Image = global::GeneralMachine.Properties.Resources.HostarGroup;
            this.pHostarLog.Location = new System.Drawing.Point(0, 0);
            this.pHostarLog.Name = "pHostarLog";
            this.pHostarLog.Size = new System.Drawing.Size(301, 81);
            this.pHostarLog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pHostarLog.TabIndex = 0;
            this.pHostarLog.TabStop = false;
            this.pHostarLog.Click += new System.EventHandler(this.pHostarLog_Click);
            // 
            // pBar_Front
            // 
            this.pBar_Front.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pBar_Front.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBar_Front.Location = new System.Drawing.Point(0, 216);
            this.pBar_Front.Name = "pBar_Front";
            this.pBar_Front.Size = new System.Drawing.Size(281, 33);
            this.pBar_Front.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 81);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(552, 249);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.listAfter);
            this.panel4.Controls.Add(this.pBar_After);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(281, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(271, 249);
            this.panel4.TabIndex = 2;
            // 
            // listAfter
            // 
            this.listAfter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listAfter.Font = new System.Drawing.Font("SimSun", 16F);
            this.listAfter.FormattingEnabled = true;
            this.listAfter.ItemHeight = 21;
            this.listAfter.Location = new System.Drawing.Point(0, 25);
            this.listAfter.Name = "listAfter";
            this.listAfter.Size = new System.Drawing.Size(271, 191);
            this.listAfter.TabIndex = 0;
            // 
            // pBar_After
            // 
            this.pBar_After.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pBar_After.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBar_After.Location = new System.Drawing.Point(0, 216);
            this.pBar_After.Name = "pBar_After";
            this.pBar_After.Size = new System.Drawing.Size(271, 33);
            this.pBar_After.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Lime;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(271, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "后模组正在回零.....";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listFront);
            this.panel2.Controls.Add(this.pBar_Front);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(281, 249);
            this.panel2.TabIndex = 1;
            // 
            // listFront
            // 
            this.listFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFront.Font = new System.Drawing.Font("SimSun", 16F);
            this.listFront.FormattingEnabled = true;
            this.listFront.ItemHeight = 21;
            this.listFront.Location = new System.Drawing.Point(0, 25);
            this.listFront.Name = "listFront";
            this.listFront.Size = new System.Drawing.Size(281, 191);
            this.listFront.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Lime;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "前模组正在回零.....";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm_Loading
            // 
            this.AcceptButton = this.bConfim;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(552, 330);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_Loading";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Loading";
            this.Load += new System.EventHandler(this.frm_Loading_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHostarLog)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pHostarLog;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.ProgressBar pBar_Front;
        public System.Windows.Forms.ListBox listFront;
        private System.Windows.Forms.Button bConfim;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.ListBox listAfter;
        public System.Windows.Forms.ProgressBar pBar_After;
        private System.Windows.Forms.Label label2;
    }
}