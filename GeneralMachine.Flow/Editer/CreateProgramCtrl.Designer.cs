namespace GeneralMachine.Flow.Editer
{
    partial class CreateProgramCtrl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bCancel = new System.Windows.Forms.Button();
            this.lSelectMode = new System.Windows.Forms.Label();
            this.bCreate = new System.Windows.Forms.Button();
            this.lSelectName = new System.Windows.Forms.Label();
            this.tProgramName = new System.Windows.Forms.ComboBox();
            this.tModule = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.bCancel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lSelectMode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.bCreate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lSelectName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tProgramName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tModule, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(420, 160);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // bCancel
            // 
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bCancel.Location = new System.Drawing.Point(3, 115);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(119, 42);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "取消";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // lSelectMode
            // 
            this.lSelectMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lSelectMode.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lSelectMode.ForeColor = System.Drawing.Color.Red;
            this.lSelectMode.Location = new System.Drawing.Point(3, 56);
            this.lSelectMode.Name = "lSelectMode";
            this.lSelectMode.Size = new System.Drawing.Size(204, 56);
            this.lSelectMode.TabIndex = 3;
            this.lSelectMode.Text = "请选择程式所属模组:";
            this.lSelectMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bCreate
            // 
            this.bCreate.BackColor = System.Drawing.Color.Yellow;
            this.bCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCreate.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bCreate.Location = new System.Drawing.Point(213, 115);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(119, 42);
            this.bCreate.TabIndex = 2;
            this.bCreate.Text = "创建";
            this.bCreate.UseVisualStyleBackColor = false;
            this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
            // 
            // lSelectName
            // 
            this.lSelectName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lSelectName.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lSelectName.ForeColor = System.Drawing.Color.Red;
            this.lSelectName.Location = new System.Drawing.Point(3, 0);
            this.lSelectName.Name = "lSelectName";
            this.lSelectName.Size = new System.Drawing.Size(204, 56);
            this.lSelectName.TabIndex = 1;
            this.lSelectName.Text = "请输入新建程式名称:";
            this.lSelectName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tProgramName
            // 
            this.tProgramName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tProgramName.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tProgramName.Location = new System.Drawing.Point(213, 15);
            this.tProgramName.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.tProgramName.Name = "tProgramName";
            this.tProgramName.Size = new System.Drawing.Size(204, 34);
            this.tProgramName.TabIndex = 2;
            // 
            // tModule
            // 
            this.tModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tModule.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tModule.FormattingEnabled = true;
            this.tModule.Items.AddRange(new object[] {
            "前模组",
            "后模组"});
            this.tModule.Location = new System.Drawing.Point(213, 71);
            this.tModule.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.tModule.Name = "tModule";
            this.tModule.Size = new System.Drawing.Size(204, 34);
            this.tModule.TabIndex = 4;
            this.tModule.SelectedIndexChanged += new System.EventHandler(this.tModule_SelectedIndexChanged_1);
            // 
            // CreateProgramCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(420, 160);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreateProgramCtrl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lSelectName;
        private System.Windows.Forms.ComboBox tProgramName;
        private System.Windows.Forms.Label lSelectMode;
        private System.Windows.Forms.ComboBox tModule;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bCreate;
    }
}
