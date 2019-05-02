namespace GeneralMachine
{
    partial class frm_Feeder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Feeder));
            GeneralMachine.Config.FeederConfig feederConfig1 = new GeneralMachine.Config.FeederConfig();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listFeeder = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tAdd = new System.Windows.Forms.ToolStripButton();
            this.tDelete = new System.Windows.Forms.ToolStripButton();
            this.tRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolSave = new System.Windows.Forms.ToolStripButton();
            this.toolInstall = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.moduleRadio1 = new GeneralMachine.BasicUI.ModuleRadio();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lLeftUsed = new System.Windows.Forms.Label();
            this.lRightUsed = new System.Windows.Forms.Label();
            this.feederInfo = new GeneralMachine.FeederConfigCtrl();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listFeeder);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 421);
            this.panel1.TabIndex = 2;
            // 
            // listFeeder
            // 
            this.listFeeder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFeeder.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listFeeder.FormattingEnabled = true;
            this.listFeeder.ItemHeight = 19;
            this.listFeeder.Location = new System.Drawing.Point(0, 57);
            this.listFeeder.Name = "listFeeder";
            this.listFeeder.Size = new System.Drawing.Size(185, 364);
            this.listFeeder.TabIndex = 2;
            this.listFeeder.SelectedIndexChanged += new System.EventHandler(this.listFeeder_SelectedIndexChanged);
            this.listFeeder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listFeeder_MouseDoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tAdd,
            this.tDelete,
            this.tRefresh,
            this.toolSave,
            this.toolInstall});
            this.toolStrip1.Location = new System.Drawing.Point(0, 32);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(185, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tAdd
            // 
            this.tAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tAdd.Image = ((System.Drawing.Image)(resources.GetObject("tAdd.Image")));
            this.tAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tAdd.Name = "tAdd";
            this.tAdd.Size = new System.Drawing.Size(23, 22);
            this.tAdd.Text = "新建";
            this.tAdd.Click += new System.EventHandler(this.tAdd_Click);
            // 
            // tDelete
            // 
            this.tDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tDelete.Image = global::GeneralMachine.Properties.Resources.Cancel_16x16;
            this.tDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tDelete.Name = "tDelete";
            this.tDelete.Size = new System.Drawing.Size(23, 22);
            this.tDelete.Text = "toolStripButton2";
            this.tDelete.Click += new System.EventHandler(this.tDelete_Click);
            // 
            // tRefresh
            // 
            this.tRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tRefresh.Image")));
            this.tRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tRefresh.Name = "tRefresh";
            this.tRefresh.Size = new System.Drawing.Size(23, 22);
            this.tRefresh.Text = "刷新";
            this.tRefresh.Click += new System.EventHandler(this.tRefresh_Click);
            // 
            // toolSave
            // 
            this.toolSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSave.Image = ((System.Drawing.Image)(resources.GetObject("toolSave.Image")));
            this.toolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSave.Name = "toolSave";
            this.toolSave.Size = new System.Drawing.Size(23, 22);
            this.toolSave.Text = "保存";
            this.toolSave.Click += new System.EventHandler(this.toolSave_Click);
            // 
            // toolInstall
            // 
            this.toolInstall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolInstall.Image = ((System.Drawing.Image)(resources.GetObject("toolInstall.Image")));
            this.toolInstall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolInstall.Name = "toolInstall";
            this.toolInstall.Size = new System.Drawing.Size(23, 22);
            this.toolInstall.Text = "安裝该Feeder";
            this.toolInstall.Click += new System.EventHandler(this.toolInstall_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Feeder信息列表";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // moduleRadio1
            // 
            this.moduleRadio1.Dock = System.Windows.Forms.DockStyle.Left;
            this.moduleRadio1.Location = new System.Drawing.Point(0, 0);
            this.moduleRadio1.Module = GeneralMachine.Config.Module.Front;
            this.moduleRadio1.Name = "moduleRadio1";
            this.moduleRadio1.Size = new System.Drawing.Size(229, 56);
            this.moduleRadio1.TabIndex = 0;
            this.moduleRadio1.ModuleChange += new System.EventHandler<GeneralMachine.Config.Module>(this.moduleRadio1_ModuleChange);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lLeftUsed);
            this.panel2.Controls.Add(this.lRightUsed);
            this.panel2.Controls.Add(this.moduleRadio1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(660, 56);
            this.panel2.TabIndex = 3;
            // 
            // lLeftUsed
            // 
            this.lLeftUsed.BackColor = System.Drawing.Color.Yellow;
            this.lLeftUsed.Dock = System.Windows.Forms.DockStyle.Right;
            this.lLeftUsed.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lLeftUsed.Location = new System.Drawing.Point(292, 0);
            this.lLeftUsed.Name = "lLeftUsed";
            this.lLeftUsed.Size = new System.Drawing.Size(184, 56);
            this.lLeftUsed.TabIndex = 2;
            this.lLeftUsed.Tag = "0";
            this.lLeftUsed.Text = "左Feeder正在使用:[]";
            this.lLeftUsed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lRightUsed
            // 
            this.lRightUsed.BackColor = System.Drawing.Color.Yellow;
            this.lRightUsed.Dock = System.Windows.Forms.DockStyle.Right;
            this.lRightUsed.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lRightUsed.Location = new System.Drawing.Point(476, 0);
            this.lRightUsed.Name = "lRightUsed";
            this.lRightUsed.Size = new System.Drawing.Size(184, 56);
            this.lRightUsed.TabIndex = 1;
            this.lRightUsed.Tag = "1";
            this.lRightUsed.Text = "右Feeder正在使用:[]";
            this.lRightUsed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // feederInfo
            // 
            this.feederInfo.BackColor = System.Drawing.Color.White;
            this.feederInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            feederConfig1.Feeder = GeneralMachine.Config.Feeder.Left;
            feederConfig1.LabelName = "";
            feederConfig1.LabelTotalNum = 9999;
            feederConfig1.LabelUseNum = 0;
            feederConfig1.LabelWarnNum = 50;
            feederConfig1.NewFeederDelay = 500;
            feederConfig1.PutDelay = 50;
            feederConfig1.SuckDelay = 50;
            feederConfig1.SuckIndex = 0;
            this.feederInfo.FeederConfig = feederConfig1;
            this.feederInfo.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.feederInfo.Location = new System.Drawing.Point(185, 56);
            this.feederInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.feederInfo.Name = "feederInfo";
            this.feederInfo.Size = new System.Drawing.Size(475, 421);
            this.feederInfo.TabIndex = 3;
            // 
            // frm_Feeder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.feederInfo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frm_Feeder";
            this.Size = new System.Drawing.Size(660, 477);
            this.Load += new System.EventHandler(this.frm_Feeder_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BasicUI.ModuleRadio moduleRadio1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tAdd;
        private System.Windows.Forms.ToolStripButton tDelete;
        private System.Windows.Forms.ToolStripButton tRefresh;
        private System.Windows.Forms.ListBox listFeeder;
        private FeederConfigCtrl feederInfo;
        private System.Windows.Forms.ToolStripButton toolSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lLeftUsed;
        private System.Windows.Forms.Label lRightUsed;
        private System.Windows.Forms.ToolStripButton toolInstall;
    }
}