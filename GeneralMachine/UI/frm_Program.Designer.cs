namespace GeneralMachine
{
    partial class frm_Program
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
            this.Graph_Program = new ZedGraph.ZedGraphControl();
            this.pProgram = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Graph_Program
            // 
            this.Graph_Program.Dock = System.Windows.Forms.DockStyle.Top;
            this.Graph_Program.Location = new System.Drawing.Point(0, 0);
            this.Graph_Program.Name = "Graph_Program";
            this.Graph_Program.ScrollGrace = 0D;
            this.Graph_Program.ScrollMaxX = 0D;
            this.Graph_Program.ScrollMaxY = 0D;
            this.Graph_Program.ScrollMaxY2 = 0D;
            this.Graph_Program.ScrollMinX = 0D;
            this.Graph_Program.ScrollMinY = 0D;
            this.Graph_Program.ScrollMinY2 = 0D;
            this.Graph_Program.Size = new System.Drawing.Size(712, 198);
            this.Graph_Program.TabIndex = 32;
            this.Graph_Program.UseExtendedPrintDialog = true;
            // 
            // pProgram
            // 
            this.pProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pProgram.Location = new System.Drawing.Point(0, 198);
            this.pProgram.Name = "pProgram";
            this.pProgram.Size = new System.Drawing.Size(712, 551);
            this.pProgram.TabIndex = 33;
            // 
            // frm_Program
            // 
            this.AllowEndUserDocking = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 749);
            this.Controls.Add(this.pProgram);
            this.Controls.Add(this.Graph_Program);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frm_Program";
            this.Text = "程式编辑";
            this.Load += new System.EventHandler(this.frm_Program_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public ZedGraph.ZedGraphControl Graph_Program;
        public System.Windows.Forms.Panel pProgram;
    }
}