namespace GeneralMachine
{
    partial class FrmRoles
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gbRoleLeft = new System.Windows.Forms.GroupBox();
            this.panRoleBtn = new System.Windows.Forms.Panel();
            this.panRole = new System.Windows.Forms.Panel();
            this.tbRemark = new System.Windows.Forms.TextBox();
            this.lbRemark = new System.Windows.Forms.Label();
            this.tbRoleName = new System.Windows.Forms.TextBox();
            this.lbRoleBame = new System.Windows.Forms.Label();
            this.btnRoleDelete = new System.Windows.Forms.Button();
            this.btnCencel = new System.Windows.Forms.Button();
            this.btnRoleModify = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnRoleAdd = new System.Windows.Forms.Button();
            this.lbRole = new System.Windows.Forms.ListBox();
            this.gbRoleRight = new System.Windows.Forms.GroupBox();
            this.tvModdleInfo = new System.Windows.Forms.TreeView();
            this.gbRoleLeft.SuspendLayout();
            this.panRoleBtn.SuspendLayout();
            this.panRole.SuspendLayout();
            this.gbRoleRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRoleLeft
            // 
            this.gbRoleLeft.Controls.Add(this.panRoleBtn);
            this.gbRoleLeft.Controls.Add(this.lbRole);
            this.gbRoleLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbRoleLeft.Location = new System.Drawing.Point(0, 0);
            this.gbRoleLeft.Name = "gbRoleLeft";
            this.gbRoleLeft.Size = new System.Drawing.Size(255, 471);
            this.gbRoleLeft.TabIndex = 0;
            this.gbRoleLeft.TabStop = false;
            this.gbRoleLeft.Text = "权限角色";
            // 
            // panRoleBtn
            // 
            this.panRoleBtn.Controls.Add(this.panRole);
            this.panRoleBtn.Controls.Add(this.btnRoleDelete);
            this.panRoleBtn.Controls.Add(this.btnCencel);
            this.panRoleBtn.Controls.Add(this.btnRoleModify);
            this.panRoleBtn.Controls.Add(this.btnOK);
            this.panRoleBtn.Controls.Add(this.btnRoleAdd);
            this.panRoleBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panRoleBtn.Location = new System.Drawing.Point(3, 300);
            this.panRoleBtn.Name = "panRoleBtn";
            this.panRoleBtn.Size = new System.Drawing.Size(249, 168);
            this.panRoleBtn.TabIndex = 2;
            // 
            // panRole
            // 
            this.panRole.Controls.Add(this.tbRemark);
            this.panRole.Controls.Add(this.lbRemark);
            this.panRole.Controls.Add(this.tbRoleName);
            this.panRole.Controls.Add(this.lbRoleBame);
            this.panRole.Enabled = false;
            this.panRole.Location = new System.Drawing.Point(3, 3);
            this.panRole.Name = "panRole";
            this.panRole.Size = new System.Drawing.Size(243, 98);
            this.panRole.TabIndex = 9;
            // 
            // tbRemark
            // 
            this.tbRemark.Location = new System.Drawing.Point(64, 39);
            this.tbRemark.Multiline = true;
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.Size = new System.Drawing.Size(170, 51);
            this.tbRemark.TabIndex = 11;
            // 
            // lbRemark
            // 
            this.lbRemark.AutoSize = true;
            this.lbRemark.Location = new System.Drawing.Point(5, 42);
            this.lbRemark.Name = "lbRemark";
            this.lbRemark.Size = new System.Drawing.Size(53, 12);
            this.lbRemark.TabIndex = 10;
            this.lbRemark.Text = "备  注：";
            // 
            // tbRoleName
            // 
            this.tbRoleName.Location = new System.Drawing.Point(64, 8);
            this.tbRoleName.Name = "tbRoleName";
            this.tbRoleName.Size = new System.Drawing.Size(170, 21);
            this.tbRoleName.TabIndex = 9;
            // 
            // lbRoleBame
            // 
            this.lbRoleBame.AutoSize = true;
            this.lbRoleBame.Location = new System.Drawing.Point(5, 11);
            this.lbRoleBame.Name = "lbRoleBame";
            this.lbRoleBame.Size = new System.Drawing.Size(53, 12);
            this.lbRoleBame.TabIndex = 8;
            this.lbRoleBame.Text = "角色名：";
            // 
            // btnRoleDelete
            // 
            this.btnRoleDelete.Enabled = false;
            this.btnRoleDelete.Location = new System.Drawing.Point(171, 107);
            this.btnRoleDelete.Name = "btnRoleDelete";
            this.btnRoleDelete.Size = new System.Drawing.Size(75, 23);
            this.btnRoleDelete.TabIndex = 8;
            this.btnRoleDelete.Text = "删 除";
            this.btnRoleDelete.UseVisualStyleBackColor = true;
            this.btnRoleDelete.Click += new System.EventHandler(this.btnRoleDelete_Click);
            // 
            // btnCencel
            // 
            this.btnCencel.Location = new System.Drawing.Point(90, 136);
            this.btnCencel.Name = "btnCencel";
            this.btnCencel.Size = new System.Drawing.Size(75, 23);
            this.btnCencel.TabIndex = 3;
            this.btnCencel.Text = "取 消";
            this.btnCencel.UseVisualStyleBackColor = true;
            this.btnCencel.Click += new System.EventHandler(this.btnCencel_Click);
            // 
            // btnRoleModify
            // 
            this.btnRoleModify.Enabled = false;
            this.btnRoleModify.Location = new System.Drawing.Point(90, 107);
            this.btnRoleModify.Name = "btnRoleModify";
            this.btnRoleModify.Size = new System.Drawing.Size(75, 23);
            this.btnRoleModify.TabIndex = 2;
            this.btnRoleModify.Text = "修 改";
            this.btnRoleModify.UseVisualStyleBackColor = true;
            this.btnRoleModify.Click += new System.EventHandler(this.btnRoleModify_Click);
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(9, 136);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确 定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnRoleAdd
            // 
            this.btnRoleAdd.Location = new System.Drawing.Point(9, 107);
            this.btnRoleAdd.Name = "btnRoleAdd";
            this.btnRoleAdd.Size = new System.Drawing.Size(75, 23);
            this.btnRoleAdd.TabIndex = 0;
            this.btnRoleAdd.Text = "添 加";
            this.btnRoleAdd.UseVisualStyleBackColor = true;
            this.btnRoleAdd.Click += new System.EventHandler(this.btnRoleAdd_Click);
            // 
            // lbRole
            // 
            this.lbRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRole.FormattingEnabled = true;
            this.lbRole.ItemHeight = 12;
            this.lbRole.Location = new System.Drawing.Point(3, 17);
            this.lbRole.Name = "lbRole";
            this.lbRole.Size = new System.Drawing.Size(249, 451);
            this.lbRole.TabIndex = 0;
            this.lbRole.Click += new System.EventHandler(this.lbRole_Click);
            // 
            // gbRoleRight
            // 
            this.gbRoleRight.Controls.Add(this.tvModdleInfo);
            this.gbRoleRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRoleRight.Location = new System.Drawing.Point(255, 0);
            this.gbRoleRight.Name = "gbRoleRight";
            this.gbRoleRight.Size = new System.Drawing.Size(443, 471);
            this.gbRoleRight.TabIndex = 1;
            this.gbRoleRight.TabStop = false;
            this.gbRoleRight.Text = "具体权限模块";
            // 
            // tvModdleInfo
            // 
            this.tvModdleInfo.CheckBoxes = true;
            this.tvModdleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvModdleInfo.Enabled = false;
            this.tvModdleInfo.FullRowSelect = true;
            this.tvModdleInfo.Location = new System.Drawing.Point(3, 17);
            this.tvModdleInfo.Name = "tvModdleInfo";
            this.tvModdleInfo.Size = new System.Drawing.Size(437, 451);
            this.tvModdleInfo.TabIndex = 0;
            // 
            // FrmRoles
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 471);
            this.Controls.Add(this.gbRoleRight);
            this.Controls.Add(this.gbRoleLeft);
            this.Name = "FrmRoles";
            this.Text = "权限管理";
            this.Load += new System.EventHandler(this.FrmRoles_Load);
            this.gbRoleLeft.ResumeLayout(false);
            this.panRoleBtn.ResumeLayout(false);
            this.panRole.ResumeLayout(false);
            this.panRole.PerformLayout();
            this.gbRoleRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbRoleLeft;
        private System.Windows.Forms.GroupBox gbRoleRight;
        private System.Windows.Forms.TreeView tvModdleInfo;
        private System.Windows.Forms.ListBox lbRole;
        private System.Windows.Forms.Panel panRoleBtn;
        private System.Windows.Forms.Button btnCencel;
        private System.Windows.Forms.Button btnRoleModify;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnRoleAdd;
        private System.Windows.Forms.Button btnRoleDelete;
        private System.Windows.Forms.Panel panRole;
        private System.Windows.Forms.TextBox tbRemark;
        private System.Windows.Forms.Label lbRemark;
        private System.Windows.Forms.TextBox tbRoleName;
        private System.Windows.Forms.Label lbRoleBame;
    }
}