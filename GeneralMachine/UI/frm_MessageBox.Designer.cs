namespace GeneralMachine
{
    partial class frm_MessageBox
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
            this.bStopAlarm = new System.Windows.Forms.Button();
            this.tInfo = new System.Windows.Forms.TextBox();
            this.bGOON = new System.Windows.Forms.Button();
            this.bHalt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bStopAlarm
            // 
            this.bStopAlarm.BackColor = System.Drawing.Color.White;
            this.bStopAlarm.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bStopAlarm.Location = new System.Drawing.Point(1, 230);
            this.bStopAlarm.Name = "bStopAlarm";
            this.bStopAlarm.Size = new System.Drawing.Size(160, 49);
            this.bStopAlarm.TabIndex = 0;
            this.bStopAlarm.Text = "停止蜂鸣器";
            this.bStopAlarm.UseVisualStyleBackColor = false;
            this.bStopAlarm.Click += new System.EventHandler(this.bStopAlarm_Click);
            // 
            // tInfo
            // 
            this.tInfo.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.tInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tInfo.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tInfo.Location = new System.Drawing.Point(0, 0);
            this.tInfo.Multiline = true;
            this.tInfo.Name = "tInfo";
            this.tInfo.ReadOnly = true;
            this.tInfo.Size = new System.Drawing.Size(543, 224);
            this.tInfo.TabIndex = 2;
            this.tInfo.Text = "提示信息";
            this.tInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bGOON
            // 
            this.bGOON.BackColor = System.Drawing.Color.Yellow;
            this.bGOON.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bGOON.Location = new System.Drawing.Point(194, 230);
            this.bGOON.Name = "bGOON";
            this.bGOON.Size = new System.Drawing.Size(160, 49);
            this.bGOON.TabIndex = 3;
            this.bGOON.Text = "继续";
            this.bGOON.UseVisualStyleBackColor = false;
            this.bGOON.Click += new System.EventHandler(this.bGOON_Click);
            // 
            // bHalt
            // 
            this.bHalt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bHalt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bHalt.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bHalt.Location = new System.Drawing.Point(379, 230);
            this.bHalt.Name = "bHalt";
            this.bHalt.Size = new System.Drawing.Size(160, 49);
            this.bHalt.TabIndex = 4;
            this.bHalt.Text = "暂停";
            this.bHalt.UseVisualStyleBackColor = false;
            this.bHalt.Click += new System.EventHandler(this.bHalt_Click);
            // 
            // frm_MessageBox
            // 
            this.AcceptButton = this.bGOON;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.CancelButton = this.bHalt;
            this.ClientSize = new System.Drawing.Size(543, 283);
            this.ControlBox = false;
            this.Controls.Add(this.bGOON);
            this.Controls.Add(this.tInfo);
            this.Controls.Add(this.bStopAlarm);
            this.Controls.Add(this.bHalt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frm_MessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frm_MessageBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bStopAlarm;
        private System.Windows.Forms.TextBox tInfo;
        private System.Windows.Forms.Button bGOON;
        private System.Windows.Forms.Button bHalt;
    }
}