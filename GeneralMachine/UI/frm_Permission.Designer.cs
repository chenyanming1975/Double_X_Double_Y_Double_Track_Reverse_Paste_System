namespace GeneralMachine
{
    partial class frm_Permission
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
            this.rB_Operator = new System.Windows.Forms.RadioButton();
            this.rB_Techinical = new System.Windows.Forms.RadioButton();
            this.rB_Engineer = new System.Windows.Forms.RadioButton();
            this.rB_Manager = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.bSelectAll = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rB_Operator
            // 
            this.rB_Operator.AutoSize = true;
            this.rB_Operator.Location = new System.Drawing.Point(264, 19);
            this.rB_Operator.Name = "rB_Operator";
            this.rB_Operator.Size = new System.Drawing.Size(74, 20);
            this.rB_Operator.TabIndex = 7;
            this.rB_Operator.Text = "操作员";
            this.rB_Operator.UseVisualStyleBackColor = true;
            // 
            // rB_Techinical
            // 
            this.rB_Techinical.AutoSize = true;
            this.rB_Techinical.Location = new System.Drawing.Point(180, 19);
            this.rB_Techinical.Name = "rB_Techinical";
            this.rB_Techinical.Size = new System.Drawing.Size(74, 20);
            this.rB_Techinical.TabIndex = 6;
            this.rB_Techinical.Text = "技术员";
            this.rB_Techinical.UseVisualStyleBackColor = true;
            // 
            // rB_Engineer
            // 
            this.rB_Engineer.AutoSize = true;
            this.rB_Engineer.Location = new System.Drawing.Point(96, 19);
            this.rB_Engineer.Name = "rB_Engineer";
            this.rB_Engineer.Size = new System.Drawing.Size(74, 20);
            this.rB_Engineer.TabIndex = 5;
            this.rB_Engineer.Text = "工程师";
            this.rB_Engineer.UseVisualStyleBackColor = true;
            // 
            // rB_Manager
            // 
            this.rB_Manager.AutoSize = true;
            this.rB_Manager.Checked = true;
            this.rB_Manager.Location = new System.Drawing.Point(12, 19);
            this.rB_Manager.Name = "rB_Manager";
            this.rB_Manager.Size = new System.Drawing.Size(74, 20);
            this.rB_Manager.TabIndex = 4;
            this.rB_Manager.TabStop = true;
            this.rB_Manager.Text = "管理员";
            this.rB_Manager.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rB_Operator);
            this.groupBox1.Controls.Add(this.rB_Manager);
            this.groupBox1.Controls.Add(this.rB_Techinical);
            this.groupBox1.Controls.Add(this.rB_Engineer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 12F);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 47);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "角色选择";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(347, 289);
            this.panel1.TabIndex = 4;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(347, 289);
            this.treeView1.TabIndex = 0;
            // 
            // bSelectAll
            // 
            this.bSelectAll.Font = new System.Drawing.Font("SimSun", 12F);
            this.bSelectAll.Location = new System.Drawing.Point(25, 51);
            this.bSelectAll.Name = "bSelectAll";
            this.bSelectAll.Size = new System.Drawing.Size(83, 28);
            this.bSelectAll.TabIndex = 5;
            this.bSelectAll.Text = "全选";
            this.bSelectAll.UseVisualStyleBackColor = true;
            // 
            // bClear
            // 
            this.bClear.Font = new System.Drawing.Font("SimSun", 12F);
            this.bClear.Location = new System.Drawing.Point(131, 51);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(83, 28);
            this.bClear.TabIndex = 6;
            this.bClear.Text = "清空";
            this.bClear.UseVisualStyleBackColor = true;
            // 
            // bSave
            // 
            this.bSave.Font = new System.Drawing.Font("SimSun", 12F);
            this.bSave.Location = new System.Drawing.Point(237, 51);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(83, 28);
            this.bSave.TabIndex = 7;
            this.bSave.Text = "保存";
            this.bSave.UseVisualStyleBackColor = true;
            // 
            // frm_Permission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 374);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bSelectAll);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_Permission";
            this.Text = "权限管理";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton rB_Operator;
        private System.Windows.Forms.RadioButton rB_Techinical;
        private System.Windows.Forms.RadioButton rB_Engineer;
        private System.Windows.Forms.RadioButton rB_Manager;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button bSelectAll;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bSave;
    }
}