﻿namespace GeneralMachine.Test
{
    partial class frm_TestVision
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
            this.visionToolCtrl1 = new GeneralMachine.Vision.VisionToolCtrl();
            this.SuspendLayout();
            // 
            // visionToolCtrl1
            // 
            this.visionToolCtrl1.BackColor = System.Drawing.Color.White;
            this.visionToolCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visionToolCtrl1.Location = new System.Drawing.Point(0, 0);
            this.visionToolCtrl1.Name = "visionToolCtrl1";
            this.visionToolCtrl1.Size = new System.Drawing.Size(1003, 715);
            this.visionToolCtrl1.TabIndex = 0;
            // 
            // frm_TestVision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 715);
            this.Controls.Add(this.visionToolCtrl1);
            this.Name = "frm_TestVision";
            this.Text = "frm_TestVision";
            this.ResumeLayout(false);

        }

        #endregion

        private Vision.VisionToolCtrl visionToolCtrl1;
    }
}