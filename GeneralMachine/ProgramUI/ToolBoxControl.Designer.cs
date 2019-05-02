namespace GeneralMachine
{
    partial class ToolBoxControl
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
            this.camPanel = new System.Windows.Forms.Panel();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SuspendLayout();
            // 
            // camPanel
            // 
            this.camPanel.AutoScroll = true;
            this.camPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.camPanel.Location = new System.Drawing.Point(645, 0);
            this.camPanel.Margin = new System.Windows.Forms.Padding(0);
            this.camPanel.Name = "camPanel";
            this.camPanel.Size = new System.Drawing.Size(355, 700);
            this.camPanel.TabIndex = 0;
            // 
            // toolPanel
            // 
            this.toolPanel.AutoScroll = true;
            this.toolPanel.AutoScrollMinSize = new System.Drawing.Size(630, 2000);
            this.toolPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolPanel.Location = new System.Drawing.Point(0, 0);
            this.toolPanel.Margin = new System.Windows.Forms.Padding(0);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(645, 700);
            this.toolPanel.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(435, 703);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ToolBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolPanel);
            this.Controls.Add(this.camPanel);
            this.Name = "ToolBoxControl";
            this.Size = new System.Drawing.Size(1000, 700);
            this.Load += new System.EventHandler(this.ToolBoxControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel camPanel;
        public System.Windows.Forms.Panel toolPanel;
        private System.Windows.Forms.TabPage tabPage1;
    }
}
