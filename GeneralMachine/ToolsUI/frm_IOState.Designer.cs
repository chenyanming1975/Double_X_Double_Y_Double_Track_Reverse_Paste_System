namespace GeneralMachine.IO
{
    partial class frm_IOState
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_IOState));
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ioStateControl1 = new GeneralMachine.IO.IOStateControl();
            this.bWrite = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bWrite);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 44);
            this.panel1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(122, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(361, 25);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(26, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择轴卡:";
            // 
            // ioStateControl1
            // 
            this.ioStateControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ioStateControl1.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ioStateControl1.Location = new System.Drawing.Point(0, 44);
            this.ioStateControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ioStateControl1.Name = "ioStateControl1";
            this.ioStateControl1.Size = new System.Drawing.Size(559, 555);
            this.ioStateControl1.TabIndex = 1;
            // 
            // bWrite
            // 
            this.bWrite.BackColor = System.Drawing.Color.Yellow;
            this.bWrite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bWrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bWrite.Image = ((System.Drawing.Image)(resources.GetObject("bWrite.Image")));
            this.bWrite.Location = new System.Drawing.Point(503, 3);
            this.bWrite.Name = "bWrite";
            this.bWrite.Size = new System.Drawing.Size(42, 34);
            this.bWrite.TabIndex = 2;
            this.bWrite.UseVisualStyleBackColor = false;
            this.bWrite.Click += new System.EventHandler(this.bWrite_Click);
            // 
            // frm_IOState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(559, 599);
            this.Controls.Add(this.ioStateControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_IOState";
            this.Text = "IO状态显示器";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private IOStateControl ioStateControl1;
        private System.Windows.Forms.Button bWrite;
    }
}