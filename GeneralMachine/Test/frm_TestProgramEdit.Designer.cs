namespace GeneralMachine.Test
{
    partial class frm_TestProgramEdit
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
            this.programEditCtrl1 = new GeneralMachine.Flow.Editer.ProgramEditCtrl();
            this.SuspendLayout();
            // 
            // programEditCtrl1
            // 
            this.programEditCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programEditCtrl1.Location = new System.Drawing.Point(0, 0);
            this.programEditCtrl1.Module = GeneralMachine.Config.Module.Front;
            this.programEditCtrl1.Name = "programEditCtrl1";
            this.programEditCtrl1.Size = new System.Drawing.Size(1002, 727);
            this.programEditCtrl1.TabIndex = 0;
            // 
            // frm_TestProgramEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 727);
            this.Controls.Add(this.programEditCtrl1);
            this.Name = "frm_TestProgramEdit";
            this.Text = "frm_TestProgramEdit";
            this.ResumeLayout(false);

        }

        #endregion

        private Flow.Editer.ProgramEditCtrl programEditCtrl1;
    }
}