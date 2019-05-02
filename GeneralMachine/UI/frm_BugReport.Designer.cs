namespace GeneralMachine
{
    partial class frm_BugReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BugReport));
            this.lblErrorCode = new System.Windows.Forms.Label();
            this.txtBugInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblErrorCode
            // 
            this.lblErrorCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblErrorCode.Location = new System.Drawing.Point(37, 8);
            this.lblErrorCode.Name = "lblErrorCode";
            this.lblErrorCode.Size = new System.Drawing.Size(297, 58);
            this.lblErrorCode.TabIndex = 0;
            this.lblErrorCode.Text = "label1";
            // 
            // txtBugInfo
            // 
            this.txtBugInfo.Location = new System.Drawing.Point(3, 28);
            this.txtBugInfo.Multiline = true;
            this.txtBugInfo.Name = "txtBugInfo";
            this.txtBugInfo.Size = new System.Drawing.Size(682, 387);
            this.txtBugInfo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label1.Font = new System.Drawing.Font("KaiTi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(18, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "程序发生了错误，即将关闭，错误信息:";
            // 
            // frm_BugReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(688, 417);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBugInfo);
            this.Controls.Add(this.lblErrorCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_BugReport";
            this.Text = "错误报告";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_BugReport_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblErrorCode;
        private System.Windows.Forms.TextBox txtBugInfo;
        private System.Windows.Forms.Label label1;
    }
}