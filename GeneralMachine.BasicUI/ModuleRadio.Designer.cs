namespace GeneralMachine.BasicUI
{
    partial class ModuleRadio
    {
        /// <summary> 
        /// 必需的设计器变量。
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rModule1 = new System.Windows.Forms.RadioButton();
            this.rModule2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rModule1
            // 
            this.rModule1.AutoSize = true;
            this.rModule1.BackColor = System.Drawing.SystemColors.Highlight;
            this.rModule1.Dock = System.Windows.Forms.DockStyle.Left;
            this.rModule1.Font = new System.Drawing.Font("Microsoft YaHei", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rModule1.Location = new System.Drawing.Point(0, 0);
            this.rModule1.Name = "rModule1";
            this.rModule1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.rModule1.Size = new System.Drawing.Size(120, 60);
            this.rModule1.TabIndex = 0;
            this.rModule1.TabStop = true;
            this.rModule1.Text = "前模组";
            this.rModule1.UseVisualStyleBackColor = false;
            this.rModule1.Click += new System.EventHandler(this.rModule1_Click);
            // 
            // rModule2
            // 
            this.rModule2.AutoSize = true;
            this.rModule2.Dock = System.Windows.Forms.DockStyle.Left;
            this.rModule2.Font = new System.Drawing.Font("Microsoft YaHei", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rModule2.Location = new System.Drawing.Point(120, 0);
            this.rModule2.Name = "rModule2";
            this.rModule2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.rModule2.Size = new System.Drawing.Size(120, 60);
            this.rModule2.TabIndex = 1;
            this.rModule2.TabStop = true;
            this.rModule2.Text = "后模组";
            this.rModule2.UseVisualStyleBackColor = true;
            this.rModule2.Click += new System.EventHandler(this.rModule2_Click);
            // 
            // ModuleRadio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rModule2);
            this.Controls.Add(this.rModule1);
            this.Name = "ModuleRadio";
            this.Size = new System.Drawing.Size(267, 60);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rModule1;
        private System.Windows.Forms.RadioButton rModule2;
    }
}
