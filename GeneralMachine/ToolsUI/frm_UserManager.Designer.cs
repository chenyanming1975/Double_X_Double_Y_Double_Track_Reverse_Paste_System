namespace GeneralMachine
{
    partial class frm_UserManager
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
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cH1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cH2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cH3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cH4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rB_Operator = new System.Windows.Forms.RadioButton();
            this.rB_Techinical = new System.Windows.Forms.RadioButton();
            this.rB_Engineer = new System.Windows.Forms.RadioButton();
            this.rB_Manager = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tPasswordAgain = new System.Windows.Forms.TextBox();
            this.tPassword = new System.Windows.Forms.TextBox();
            this.tLoginName = new System.Windows.Forms.TextBox();
            this.tNoteInfo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bDelete = new System.Windows.Forms.Button();
            this.bAdd = new System.Windows.Forms.Button();
            this.bUpdate = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.cH1,
            this.cH2,
            this.cH3,
            this.cH4});
            this.listView.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView.Font = new System.Drawing.Font("宋体", 12F);
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(599, 713);
            this.listView.TabIndex = 28;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.Click += new System.EventHandler(this.listView_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 11;
            // 
            // cH1
            // 
            this.cH1.Text = "序号";
            this.cH1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cH1.Width = 61;
            // 
            // cH2
            // 
            this.cH2.Text = "登录名";
            this.cH2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cH2.Width = 100;
            // 
            // cH3
            // 
            this.cH3.Text = "权限类别";
            this.cH3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cH3.Width = 138;
            // 
            // cH4
            // 
            this.cH4.Text = "备注";
            this.cH4.Width = 68;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rB_Operator);
            this.groupBox2.Controls.Add(this.rB_Techinical);
            this.groupBox2.Controls.Add(this.rB_Engineer);
            this.groupBox2.Controls.Add(this.rB_Manager);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox2.Location = new System.Drawing.Point(599, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 75);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "权限类别列表";
            // 
            // rB_Operator
            // 
            this.rB_Operator.AutoSize = true;
            this.rB_Operator.Location = new System.Drawing.Point(124, 47);
            this.rB_Operator.Name = "rB_Operator";
            this.rB_Operator.Size = new System.Drawing.Size(74, 20);
            this.rB_Operator.TabIndex = 3;
            this.rB_Operator.Text = "操作员";
            this.rB_Operator.UseVisualStyleBackColor = true;
            // 
            // rB_Techinical
            // 
            this.rB_Techinical.AutoSize = true;
            this.rB_Techinical.Location = new System.Drawing.Point(124, 20);
            this.rB_Techinical.Name = "rB_Techinical";
            this.rB_Techinical.Size = new System.Drawing.Size(74, 20);
            this.rB_Techinical.TabIndex = 2;
            this.rB_Techinical.Text = "技术员";
            this.rB_Techinical.UseVisualStyleBackColor = true;
            // 
            // rB_Engineer
            // 
            this.rB_Engineer.AutoSize = true;
            this.rB_Engineer.Location = new System.Drawing.Point(10, 47);
            this.rB_Engineer.Name = "rB_Engineer";
            this.rB_Engineer.Size = new System.Drawing.Size(74, 20);
            this.rB_Engineer.TabIndex = 1;
            this.rB_Engineer.Text = "工程师";
            this.rB_Engineer.UseVisualStyleBackColor = true;
            // 
            // rB_Manager
            // 
            this.rB_Manager.AutoSize = true;
            this.rB_Manager.Checked = true;
            this.rB_Manager.Location = new System.Drawing.Point(10, 20);
            this.rB_Manager.Name = "rB_Manager";
            this.rB_Manager.Size = new System.Drawing.Size(74, 20);
            this.rB_Manager.TabIndex = 0;
            this.rB_Manager.TabStop = true;
            this.rB_Manager.Text = "管理员";
            this.rB_Manager.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.treeView);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox3.Location = new System.Drawing.Point(599, 75);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(288, 413);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "权限管理";
            // 
            // treeView
            // 
            this.treeView.CheckBoxes = true;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(3, 22);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(282, 388);
            this.treeView.TabIndex = 0;
            this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tPasswordAgain);
            this.groupBox1.Controls.Add(this.tPassword);
            this.groupBox1.Controls.Add(this.tLoginName);
            this.groupBox1.Controls.Add(this.tNoteInfo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox1.Location = new System.Drawing.Point(599, 488);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 144);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置用户信息";
            // 
            // tPasswordAgain
            // 
            this.tPasswordAgain.Location = new System.Drawing.Point(98, 82);
            this.tPasswordAgain.Name = "tPasswordAgain";
            this.tPasswordAgain.PasswordChar = '*';
            this.tPasswordAgain.Size = new System.Drawing.Size(128, 26);
            this.tPasswordAgain.TabIndex = 7;
            this.tPasswordAgain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tPassword
            // 
            this.tPassword.Location = new System.Drawing.Point(98, 53);
            this.tPassword.Name = "tPassword";
            this.tPassword.PasswordChar = '*';
            this.tPassword.Size = new System.Drawing.Size(128, 26);
            this.tPassword.TabIndex = 6;
            this.tPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tLoginName
            // 
            this.tLoginName.Location = new System.Drawing.Point(98, 24);
            this.tLoginName.Name = "tLoginName";
            this.tLoginName.Size = new System.Drawing.Size(128, 26);
            this.tLoginName.TabIndex = 5;
            this.tLoginName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tNoteInfo
            // 
            this.tNoteInfo.Location = new System.Drawing.Point(98, 111);
            this.tNoteInfo.Name = "tNoteInfo";
            this.tNoteInfo.Size = new System.Drawing.Size(128, 26);
            this.tNoteInfo.TabIndex = 4;
            this.tNoteInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "确认密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "登录名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "备注：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bDelete);
            this.panel1.Controls.Add(this.bAdd);
            this.panel1.Controls.Add(this.bUpdate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(599, 632);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 81);
            this.panel1.TabIndex = 40;
            // 
            // bDelete
            // 
            this.bDelete.Font = new System.Drawing.Font("宋体", 12F);
            this.bDelete.Location = new System.Drawing.Point(84, 6);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(83, 39);
            this.bDelete.TabIndex = 33;
            this.bDelete.Text = "删除";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // bAdd
            // 
            this.bAdd.Font = new System.Drawing.Font("宋体", 12F);
            this.bAdd.Location = new System.Drawing.Point(1, 6);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(83, 39);
            this.bAdd.TabIndex = 31;
            this.bAdd.Text = "添加";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bUpdate
            // 
            this.bUpdate.Font = new System.Drawing.Font("宋体", 12F);
            this.bUpdate.Location = new System.Drawing.Point(167, 6);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(83, 39);
            this.bUpdate.TabIndex = 32;
            this.bUpdate.Text = "替换";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
          
            // 
            // frm_UserManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 713);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.listView);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_UserManager";
            this.Text = "用户权限管理";
            this.Load += new System.EventHandler(this.frm_UserManager_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader cH1;
        private System.Windows.Forms.ColumnHeader cH2;
        private System.Windows.Forms.ColumnHeader cH3;
        private System.Windows.Forms.ColumnHeader cH4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rB_Operator;
        private System.Windows.Forms.RadioButton rB_Techinical;
        private System.Windows.Forms.RadioButton rB_Engineer;
        private System.Windows.Forms.RadioButton rB_Manager;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tPasswordAgain;
        private System.Windows.Forms.TextBox tPassword;
        private System.Windows.Forms.TextBox tLoginName;
        private System.Windows.Forms.TextBox tNoteInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}