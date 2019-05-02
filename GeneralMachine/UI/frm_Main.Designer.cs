namespace GeneralMachine
{
    partial class frm_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Main));
            this.Menu_Main = new System.Windows.Forms.MenuStrip();
            this.toolBtn_Start = new System.Windows.Forms.ToolStripMenuItem();
            this.toolProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_Program = new System.Windows.Forms.ToolStripMenuItem();
            this.toolEditProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.toolLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolLoadRight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolFeeder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolVisionEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_Permission = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_Tool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_Camera = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_IO = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_DryRun = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_AdvSet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_Parameter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_Vel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_Mes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_UserManager = new System.Windows.Forms.ToolStripMenuItem();
            this.toolChangeDay = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_VelChoose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAuto_Slow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAuto_Manual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAuto_Fast = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolHardwareCliab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSoftwareCliab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolPressCliab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_Language = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_SChinese = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_TChinese = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_English = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_About = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_Web = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_AboutPage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtn_Manual = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dockPanel = new System.Windows.Forms.Panel();
            this.iO表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu_Main
            // 
            this.Menu_Main.BackColor = System.Drawing.Color.Silver;
            this.Menu_Main.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Menu_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtn_Start,
            this.toolBtn_Tool,
            this.toolBtn_AdvSet,
            this.toolStrip_VelChoose,
            this.toolStripMenuItem,
            this.toolBtn_Language,
            this.toolBtn_About});
            this.Menu_Main.Location = new System.Drawing.Point(0, 0);
            this.Menu_Main.Name = "Menu_Main";
            this.Menu_Main.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.Menu_Main.Size = new System.Drawing.Size(1008, 27);
            this.Menu_Main.TabIndex = 1;
            this.Menu_Main.Text = "menuStrip1";
            // 
            // toolBtn_Start
            // 
            this.toolBtn_Start.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolProduct,
            this.toolBtn_Program,
            this.toolFeeder,
            this.toolVisionEdit,
            this.toolBtn_Permission,
            this.toolBtn_Exit});
            this.toolBtn_Start.Name = "toolBtn_Start";
            this.toolBtn_Start.Size = new System.Drawing.Size(49, 23);
            this.toolBtn_Start.Text = "开始";
            // 
            // toolProduct
            // 
            this.toolProduct.Name = "toolProduct";
            this.toolProduct.Size = new System.Drawing.Size(180, 24);
            this.toolProduct.Text = "生产界面";
            this.toolProduct.Click += new System.EventHandler(this.toolProduct_Click);
            // 
            // toolBtn_Program
            // 
            this.toolBtn_Program.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEditProgram,
            this.toolLoad,
            this.toolLoadRight});
            this.toolBtn_Program.Image = global::GeneralMachine.Properties.Resources.Design_16x16;
            this.toolBtn_Program.Name = "toolBtn_Program";
            this.toolBtn_Program.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_Program.Text = "贴附程式";
            // 
            // toolEditProgram
            // 
            this.toolEditProgram.Name = "toolEditProgram";
            this.toolEditProgram.Size = new System.Drawing.Size(176, 24);
            this.toolEditProgram.Text = "程式编辑";
            this.toolEditProgram.Click += new System.EventHandler(this.toolEditProgram_Click);
            // 
            // toolLoad
            // 
            this.toolLoad.Name = "toolLoad";
            this.toolLoad.Size = new System.Drawing.Size(176, 24);
            this.toolLoad.Text = "导入前模组程式";
            this.toolLoad.Click += new System.EventHandler(this.toolLoad_Click);
            // 
            // toolLoadRight
            // 
            this.toolLoadRight.Name = "toolLoadRight";
            this.toolLoadRight.Size = new System.Drawing.Size(176, 24);
            this.toolLoadRight.Text = "导入后模组程式";
            this.toolLoadRight.Click += new System.EventHandler(this.toolLoadRight_Click);
            // 
            // toolFeeder
            // 
            this.toolFeeder.Name = "toolFeeder";
            this.toolFeeder.Size = new System.Drawing.Size(180, 24);
            this.toolFeeder.Text = "Feeder配置";
            this.toolFeeder.Click += new System.EventHandler(this.toolFeeder_Click);
            // 
            // toolVisionEdit
            // 
            this.toolVisionEdit.Name = "toolVisionEdit";
            this.toolVisionEdit.Size = new System.Drawing.Size(180, 24);
            this.toolVisionEdit.Text = "视觉编辑";
            this.toolVisionEdit.Click += new System.EventHandler(this.toolVisionEdit_Click);
            // 
            // toolBtn_Permission
            // 
            this.toolBtn_Permission.Image = global::GeneralMachine.Properties.Resources.Customer_16x16;
            this.toolBtn_Permission.Name = "toolBtn_Permission";
            this.toolBtn_Permission.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_Permission.Text = "用户切换";
            // 
            // toolBtn_Exit
            // 
            this.toolBtn_Exit.Image = global::GeneralMachine.Properties.Resources.Cancel_16x16;
            this.toolBtn_Exit.Name = "toolBtn_Exit";
            this.toolBtn_Exit.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_Exit.Text = "退出";
            this.toolBtn_Exit.Click += new System.EventHandler(this.toolBtn_Exit_Click);
            // 
            // toolBtn_Tool
            // 
            this.toolBtn_Tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtn_Camera,
            this.toolBtn_IO,
            this.toolBtn_DryRun,
            this.toolExpand});
            this.toolBtn_Tool.Name = "toolBtn_Tool";
            this.toolBtn_Tool.Size = new System.Drawing.Size(49, 23);
            this.toolBtn_Tool.Text = "工具";
            // 
            // toolBtn_Camera
            // 
            this.toolBtn_Camera.Image = global::GeneralMachine.Properties.Resources.Video_16x16;
            this.toolBtn_Camera.Name = "toolBtn_Camera";
            this.toolBtn_Camera.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_Camera.Text = "相机轴控";
            this.toolBtn_Camera.Click += new System.EventHandler(this.toolBtn_Camera_Click);
            // 
            // toolBtn_IO
            // 
            this.toolBtn_IO.Image = global::GeneralMachine.Properties.Resources.Table_16x16;
            this.toolBtn_IO.Name = "toolBtn_IO";
            this.toolBtn_IO.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_IO.Text = "IO监控";
            this.toolBtn_IO.Click += new System.EventHandler(this.toolBtn_IO_Click);
            // 
            // toolBtn_DryRun
            // 
            this.toolBtn_DryRun.Image = global::GeneralMachine.Properties.Resources.DropLines_16x16;
            this.toolBtn_DryRun.Name = "toolBtn_DryRun";
            this.toolBtn_DryRun.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_DryRun.Text = "空跑测试";
            this.toolBtn_DryRun.Click += new System.EventHandler(this.toolBtn_DryRun_Click);
            // 
            // toolExpand
            // 
            this.toolExpand.Image = ((System.Drawing.Image)(resources.GetObject("toolExpand.Image")));
            this.toolExpand.Name = "toolExpand";
            this.toolExpand.Size = new System.Drawing.Size(180, 24);
            this.toolExpand.Text = "扩展工具";
            this.toolExpand.Click += new System.EventHandler(this.toolExpand_Click);
            // 
            // toolBtn_AdvSet
            // 
            this.toolBtn_AdvSet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtn_Parameter,
            this.toolBtn_Vel,
            this.toolBtn_Mes,
            this.toolBtn_UserManager,
            this.toolChangeDay});
            this.toolBtn_AdvSet.Name = "toolBtn_AdvSet";
            this.toolBtn_AdvSet.Size = new System.Drawing.Size(49, 23);
            this.toolBtn_AdvSet.Text = "设置";
            // 
            // toolBtn_Parameter
            // 
            this.toolBtn_Parameter.Image = global::GeneralMachine.Properties.Resources.Customization_16x16;
            this.toolBtn_Parameter.Name = "toolBtn_Parameter";
            this.toolBtn_Parameter.Size = new System.Drawing.Size(183, 24);
            this.toolBtn_Parameter.Text = "系统基本参数";
            this.toolBtn_Parameter.Click += new System.EventHandler(this.toolBtn_Parameter_Click);
            // 
            // toolBtn_Vel
            // 
            this.toolBtn_Vel.Image = global::GeneralMachine.Properties.Resources.GaugeStyleThreeForthCircular_16x16;
            this.toolBtn_Vel.Name = "toolBtn_Vel";
            this.toolBtn_Vel.Size = new System.Drawing.Size(183, 24);
            this.toolBtn_Vel.Text = "机台速度参数";
            this.toolBtn_Vel.Click += new System.EventHandler(this.toolBtn_Vel_Click);
            // 
            // toolBtn_Mes
            // 
            this.toolBtn_Mes.Image = global::GeneralMachine.Properties.Resources.BOPivotChart_16x16;
            this.toolBtn_Mes.Name = "toolBtn_Mes";
            this.toolBtn_Mes.Size = new System.Drawing.Size(183, 24);
            this.toolBtn_Mes.Text = "客户端对接信息";
            this.toolBtn_Mes.Click += new System.EventHandler(this.toolBtn_Mes_Click);
            // 
            // toolBtn_UserManager
            // 
            this.toolBtn_UserManager.Image = global::GeneralMachine.Properties.Resources.UserGroup_16x16;
            this.toolBtn_UserManager.Name = "toolBtn_UserManager";
            this.toolBtn_UserManager.Size = new System.Drawing.Size(183, 24);
            this.toolBtn_UserManager.Text = "用户权限管理";
            this.toolBtn_UserManager.Click += new System.EventHandler(this.toolBtn_UserManager_Click);
            // 
            // toolChangeDay
            // 
            this.toolChangeDay.Name = "toolChangeDay";
            this.toolChangeDay.Size = new System.Drawing.Size(183, 24);
            this.toolChangeDay.Text = "白/夜班换班设置";
            this.toolChangeDay.Click += new System.EventHandler(this.toolChangeDay_Click);
            // 
            // toolStrip_VelChoose
            // 
            this.toolStrip_VelChoose.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAuto_Slow,
            this.toolAuto_Manual,
            this.toolAuto_Fast});
            this.toolStrip_VelChoose.Name = "toolStrip_VelChoose";
            this.toolStrip_VelChoose.Size = new System.Drawing.Size(77, 23);
            this.toolStrip_VelChoose.Text = "速度模式";
            // 
            // toolAuto_Slow
            // 
            this.toolAuto_Slow.Image = ((System.Drawing.Image)(resources.GetObject("toolAuto_Slow.Image")));
            this.toolAuto_Slow.Name = "toolAuto_Slow";
            this.toolAuto_Slow.Size = new System.Drawing.Size(180, 24);
            this.toolAuto_Slow.Text = "自动慢速";
            this.toolAuto_Slow.Click += new System.EventHandler(this.toolAuto_Slow_Click);
            // 
            // toolAuto_Manual
            // 
            this.toolAuto_Manual.Name = "toolAuto_Manual";
            this.toolAuto_Manual.Size = new System.Drawing.Size(180, 24);
            this.toolAuto_Manual.Text = "自动中速";
            this.toolAuto_Manual.Click += new System.EventHandler(this.toolAuto_Manual_Click);
            // 
            // toolAuto_Fast
            // 
            this.toolAuto_Fast.Name = "toolAuto_Fast";
            this.toolAuto_Fast.Size = new System.Drawing.Size(180, 24);
            this.toolAuto_Fast.Text = "自动快速";
            this.toolAuto_Fast.Click += new System.EventHandler(this.toolAuto_Fast_Click);
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolHardwareCliab,
            this.toolSoftwareCliab,
            this.toolPressCliab});
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(77, 23);
            this.toolStripMenuItem.Text = "设备校验";
            // 
            // toolHardwareCliab
            // 
            this.toolHardwareCliab.Image = ((System.Drawing.Image)(resources.GetObject("toolHardwareCliab.Image")));
            this.toolHardwareCliab.Name = "toolHardwareCliab";
            this.toolHardwareCliab.Size = new System.Drawing.Size(180, 24);
            this.toolHardwareCliab.Text = "机械误差校验";
            this.toolHardwareCliab.Click += new System.EventHandler(this.toolHardwareCliab_Click);
            // 
            // toolSoftwareCliab
            // 
            this.toolSoftwareCliab.Image = ((System.Drawing.Image)(resources.GetObject("toolSoftwareCliab.Image")));
            this.toolSoftwareCliab.Name = "toolSoftwareCliab";
            this.toolSoftwareCliab.Size = new System.Drawing.Size(180, 24);
            this.toolSoftwareCliab.Text = "视觉标定";
            this.toolSoftwareCliab.Click += new System.EventHandler(this.toolSoftwareCliab_Click);
            // 
            // toolPressCliab
            // 
            this.toolPressCliab.Image = ((System.Drawing.Image)(resources.GetObject("toolPressCliab.Image")));
            this.toolPressCliab.Name = "toolPressCliab";
            this.toolPressCliab.Size = new System.Drawing.Size(180, 24);
            this.toolPressCliab.Text = "压力校验";
            this.toolPressCliab.Click += new System.EventHandler(this.toolPressCliab_Click);
            // 
            // toolBtn_Language
            // 
            this.toolBtn_Language.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtn_SChinese,
            this.toolBtn_TChinese,
            this.toolBtn_English});
            this.toolBtn_Language.Name = "toolBtn_Language";
            this.toolBtn_Language.Size = new System.Drawing.Size(49, 23);
            this.toolBtn_Language.Text = "语言";
            // 
            // toolBtn_SChinese
            // 
            this.toolBtn_SChinese.Image = ((System.Drawing.Image)(resources.GetObject("toolBtn_SChinese.Image")));
            this.toolBtn_SChinese.Name = "toolBtn_SChinese";
            this.toolBtn_SChinese.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_SChinese.Text = "简体中文";
            this.toolBtn_SChinese.Click += new System.EventHandler(this.toolBtn_SChinese_Click);
            // 
            // toolBtn_TChinese
            // 
            this.toolBtn_TChinese.Name = "toolBtn_TChinese";
            this.toolBtn_TChinese.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_TChinese.Text = "繁体中文";
            this.toolBtn_TChinese.Click += new System.EventHandler(this.toolBtn_TChinese_Click);
            // 
            // toolBtn_English
            // 
            this.toolBtn_English.Name = "toolBtn_English";
            this.toolBtn_English.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_English.Text = "英文";
            this.toolBtn_English.Click += new System.EventHandler(this.toolBtn_English_Click);
            // 
            // toolBtn_About
            // 
            this.toolBtn_About.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtn_Web,
            this.toolBtn_AboutPage,
            this.toolBtn_Manual,
            this.iO表ToolStripMenuItem});
            this.toolBtn_About.Name = "toolBtn_About";
            this.toolBtn_About.Size = new System.Drawing.Size(49, 23);
            this.toolBtn_About.Text = "关于";
            // 
            // toolBtn_Web
            // 
            this.toolBtn_Web.Name = "toolBtn_Web";
            this.toolBtn_Web.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_Web.Text = "官网";
            this.toolBtn_Web.Click += new System.EventHandler(this.toolBtn_Web_Click);
            // 
            // toolBtn_AboutPage
            // 
            this.toolBtn_AboutPage.Name = "toolBtn_AboutPage";
            this.toolBtn_AboutPage.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_AboutPage.Text = "关于";
            this.toolBtn_AboutPage.Click += new System.EventHandler(this.toolBtn_AboutPage_Click);
            // 
            // toolBtn_Manual
            // 
            this.toolBtn_Manual.Name = "toolBtn_Manual";
            this.toolBtn_Manual.Size = new System.Drawing.Size(180, 24);
            this.toolBtn_Manual.Text = "说明书";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dockPanel
            // 
            this.dockPanel.AutoScroll = true;
            this.dockPanel.AutoSize = true;
            this.dockPanel.BackColor = System.Drawing.Color.White;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 27);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(1008, 702);
            this.dockPanel.TabIndex = 16;
            // 
            // iO表ToolStripMenuItem
            // 
            this.iO表ToolStripMenuItem.Name = "iO表ToolStripMenuItem";
            this.iO表ToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.iO表ToolStripMenuItem.Text = "IO表";
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.ControlBox = false;
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.Menu_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frm_Main";
            this.Text = "鸿仕达智能科技-全自动贴标机";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_Main_FormClosed);
            this.Load += new System.EventHandler(this.frm_Main_Load);
            this.Menu_Main.ResumeLayout(false);
            this.Menu_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip Menu_Main;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_Start;
        public System.Windows.Forms.ToolStripMenuItem toolBtn_Program;
        public System.Windows.Forms.ToolStripMenuItem toolBtn_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_About;
        public System.Windows.Forms.ToolStripMenuItem toolBtn_Web;
        public System.Windows.Forms.ToolStripMenuItem toolBtn_AboutPage;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_Tool;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_IO;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_AdvSet;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_UserManager;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_Mes;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_Language;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_SChinese;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_TChinese;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_English;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_Camera;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_DryRun;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_Parameter;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_Manual;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_Vel;
        private System.Windows.Forms.ToolStripMenuItem toolBtn_Permission;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_VelChoose;
        private System.Windows.Forms.ToolStripMenuItem toolAuto_Slow;
        private System.Windows.Forms.ToolStripMenuItem toolAuto_Manual;
        private System.Windows.Forms.ToolStripMenuItem toolAuto_Fast;
        private System.Windows.Forms.ToolStripMenuItem toolFeeder;
        private System.Windows.Forms.ToolStripMenuItem toolVisionEdit;
        private System.Windows.Forms.ToolStripMenuItem toolProduct;
        private System.Windows.Forms.ToolStripMenuItem toolExpand;
        private System.Windows.Forms.ToolStripMenuItem toolEditProgram;
        private System.Windows.Forms.ToolStripMenuItem toolLoad;
        private System.Windows.Forms.ToolStripMenuItem toolLoadRight;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel dockPanel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolHardwareCliab;
        private System.Windows.Forms.ToolStripMenuItem toolSoftwareCliab;
        private System.Windows.Forms.ToolStripMenuItem toolPressCliab;
        private System.Windows.Forms.ToolStripMenuItem toolChangeDay;
        private System.Windows.Forms.ToolStripMenuItem iO表ToolStripMenuItem;
    }
}

