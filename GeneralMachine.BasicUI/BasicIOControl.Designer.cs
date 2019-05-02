namespace GeneralMachine.BasicUI
{
    partial class BasicIOControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BasicIOControl));
            this.labelMark = new System.Windows.Forms.Label();
            this.picState = new System.Windows.Forms.PictureBox();
            this.tText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picState)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMark
            // 
            this.labelMark.BackColor = System.Drawing.Color.Yellow;
            this.labelMark.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelMark.Font = new System.Drawing.Font("Microsoft YaHei Light", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMark.Location = new System.Drawing.Point(26, 0);
            this.labelMark.Name = "labelMark";
            this.labelMark.Size = new System.Drawing.Size(61, 26);
            this.labelMark.TabIndex = 0;
            this.labelMark.Text = "号码管";
            this.labelMark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picState
            // 
            this.picState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picState.Dock = System.Windows.Forms.DockStyle.Left;
            this.picState.Image = ((System.Drawing.Image)(resources.GetObject("picState.Image")));
            this.picState.Location = new System.Drawing.Point(0, 0);
            this.picState.Name = "picState";
            this.picState.Size = new System.Drawing.Size(26, 26);
            this.picState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picState.TabIndex = 1;
            this.picState.TabStop = false;
            this.picState.Click += new System.EventHandler(this.picState_Click);
            // 
            // tText
            // 
            this.tText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tText.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tText.Location = new System.Drawing.Point(87, 0);
            this.tText.Name = "tText";
            this.tText.Size = new System.Drawing.Size(91, 23);
            this.tText.TabIndex = 2;
            this.tText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BasicIOControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tText);
            this.Controls.Add(this.labelMark);
            this.Controls.Add(this.picState);
            this.Name = "BasicIOControl";
            this.Size = new System.Drawing.Size(178, 26);
            ((System.ComponentModel.ISupportInitialize)(this.picState)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMark;
        private System.Windows.Forms.PictureBox picState;
        private System.Windows.Forms.TextBox tText;
    }
}
