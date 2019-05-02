namespace GeneralMachine.BasicUI
{
    partial class SpeedControl
    {
        /// <summary> 
        /// Required designer variable.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.teStartSpeed = new System.Windows.Forms.TextBox();
            this.teMaxSpeed = new System.Windows.Forms.TextBox();
            this.teAcc = new System.Windows.Forms.TextBox();
            this.teDec = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // teStartSpeed
            // 
            this.teStartSpeed.Location = new System.Drawing.Point(3, 3);
            this.teStartSpeed.Name = "teStartSpeed";
            this.teStartSpeed.Size = new System.Drawing.Size(63, 21);
            this.teStartSpeed.TabIndex = 0;
            this.teStartSpeed.Text = "10";
            // 
            // teMaxSpeed
            // 
            this.teMaxSpeed.Location = new System.Drawing.Point(3, 25);
            this.teMaxSpeed.Name = "teMaxSpeed";
            this.teMaxSpeed.Size = new System.Drawing.Size(63, 21);
            this.teMaxSpeed.TabIndex = 1;
            this.teMaxSpeed.Text = "100";
            // 
            // teAcc
            // 
            this.teAcc.Location = new System.Drawing.Point(3, 47);
            this.teAcc.Name = "teAcc";
            this.teAcc.Size = new System.Drawing.Size(63, 21);
            this.teAcc.TabIndex = 2;
            this.teAcc.Text = "100";
            // 
            // teDec
            // 
            this.teDec.Location = new System.Drawing.Point(3, 69);
            this.teDec.Name = "teDec";
            this.teDec.Size = new System.Drawing.Size(63, 21);
            this.teDec.TabIndex = 3;
            this.teDec.Text = "100";
            // 
            // SpeedControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.teDec);
            this.Controls.Add(this.teAcc);
            this.Controls.Add(this.teMaxSpeed);
            this.Controls.Add(this.teStartSpeed);
            this.Name = "SpeedControl";
            this.Size = new System.Drawing.Size(69, 90);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox teStartSpeed;
        private System.Windows.Forms.TextBox teMaxSpeed;
        private System.Windows.Forms.TextBox teAcc;
        private System.Windows.Forms.TextBox teDec;
    }
}
