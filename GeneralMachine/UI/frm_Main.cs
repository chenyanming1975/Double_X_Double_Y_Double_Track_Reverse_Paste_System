using System;
using System.Windows.Forms;
using GeneralMachine.Vision;
using GeneralMachine.IO;
using GeneralMachine.Flow;
using GeneralMachine.Product;
using GeneralMachine.SystemManager;
using GeneralMachine.SpeedManager;
using GeneralMachine.Flow.Editer;
using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Cliab;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Threading;
using GeneralMachine.Report;
using GeneralMachine.MES;
using GeneralMachine.Press;

namespace GeneralMachine
{
    public partial class frm_Main : Form
    {
        public frm_Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 系统初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Main_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.Text = this.Text + "[" + Variable.sPermission_CurerentRole + "]" + ":" + "[" + Variable.sPermission_CurerentUserName + "]";
            frm_MessageBox.ShowMessage("设备即将初始化，请清除轨道上的产品，离开危险区域!");
            frm_MessageBox.ShowMessage("设备开始初始化，请耐心等候!");
            SystemEntiy.Instance.Error += Error;

            frm_Loading frm_Loading = new frm_Loading();
            frm_Loading.TopMost = true;
            int rtn = SystemEntiy.Instance.SystemInit();
            if (rtn == -1)
            {
                this.Close();
                Application.Exit();
                return;
            }
            PressHelper.Load();
            ReportHelper.Load();
            SystemEntiy.Instance.SystemHome(frm_Loading);

            this.Show();
            this.dockPanel.Controls.Clear();
            this.ProductPage.Dock = DockStyle.Fill;
            this.dockPanel.Controls.Add(this.ProductPage);
            this.ProductPage.MachineStateChange += ProductPage_MachineStateChange;
        }

        private void ProductPage_MachineStateChange(int state)
        {
            // 0：pause 1:run 2:bypass 3:start drop
            if (state == 0)
                Menu_Main.Enabled = true;
            else
                Menu_Main.Enabled = false;
        }

        #region 报警处理
        private void StateMachine_AlarmEvent(Module arg1, string arg2)
        {
            frm_MessageBox fm = new frm_MessageBox($"{CommonHelper.GetEnumDescription(arg1.GetType(), arg1)}:{arg2}", true, true);
            if (fm.ShowDialog() != DialogResult.Yes)
                SystemEntiy.Instance.FlowMachine[arg1].Pause();
        }
        #endregion

        #region 储存界面
        public ProducteCtrl ProductPage = new ProducteCtrl();
        #endregion

        public int Error(SystemErrorEventArg err)
        {
            if(frm_MessageBox.ShowMessage_Alarm_OKCancel(err.Error) == DialogResult.Yes)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        #region 开始操作
        private void toolBtn_Auto_Click(object sender, EventArgs e)
        {
        }

        private void toolBtn_ProgramNew_Click(object sender, EventArgs e)
        {
     
        }

        private void toolBtn_ProgramLoad_Click(object sender, EventArgs e)
        {
        }

        private void toolBtn_ProgramEdit_Click(object sender, EventArgs e)
        {
        }

        private void toolBtn_ProgramSaveAs_Click(object sender, EventArgs e)
        {
        }

        private void toolBtn_Exit_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance.SystemExit();
            Environment.Exit(0);
        }
        #endregion

        #region 手动操作
        public ToolBoxControl ToolBox = new ToolBoxControl();
        private void toolBtn_Camera_Click(object sender, EventArgs e)
        {
            this.ChangedToToolBox();
            this.ToolBox.AddCameraControl();
        }

        private void ChangedToToolBox()
        {
            if (this.dockPanel.Controls[0].GetType() != typeof(ToolBoxControl))
            {
                this.dockPanel.Controls.Clear();
                this.ToolBox.Dock = DockStyle.Fill;
                this.dockPanel.Controls.Add(this.ToolBox);
            }
        }

        private void toolBtn_IO_Click(object sender, EventArgs e)
        {
            frm_IOState frm_IOState = new frm_IOState();
            frm_IOState.Show();
        }

        private void toolBtn_DryRun_Click(object sender, EventArgs e)
        {
            this.ChangedToToolBox();
            this.ToolBox.AddDryRunCtrl();
        }

        #endregion

        #region 参数设置
        private void toolBtn_Parameter_Click(object sender, EventArgs e)
        {
            fm_SystemSet frm = new fm_SystemSet();
            frm.Show();
        }

        private void toolBtn_AxisPluse_Click(object sender, EventArgs e)
        {
        }

        private void toolBtn_UserManager_Click(object sender, EventArgs e)
        {
            frm_UserManager frm = new frm_UserManager(this);
            frm.Show();
        }

        private void toolBtn_Calibration_Click(object sender, EventArgs e)
        {
            fm_SoftwareCliab fm_Clab = new fm_SoftwareCliab();
            fm_Clab.Show();
        }

        private void toolBtn_Vel_Click(object sender, EventArgs e)
        {
            frm_SpeedConfig frm = new frm_SpeedConfig();
            frm.Show();
        }
        #endregion

        #region 多语言
        private void toolBtn_SChinese_Click(object sender, EventArgs e)
        {

        }

        private void toolBtn_TChinese_Click(object sender, EventArgs e)
        {

        }



        private void toolBtn_English_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 关于
        private void toolBtn_Web_Click(object sender, EventArgs e)
        {

        }

        private void toolBtn_AboutPage_Click(object sender, EventArgs e)
        {

        }


        #endregion

        private void toolBtn_Mes_Click(object sender, EventArgs e)
        {
            MFlexMesControl fm = new MFlexMesControl();
            fm.ShowDialog();
        }

        private void toolFeeder_Click(object sender, EventArgs e)
        {
            this.ChangedToToolBox();
            this.ToolBox.AddFeederEditCtrl();
        }

        private void toolProduct_Click(object sender, EventArgs e)
        {
            if (this.dockPanel.Controls[0].GetType() != typeof(ProducteCtrl))
            {
                this.dockPanel.Controls.Clear();
                this.ProductPage.Dock = DockStyle.Fill;
                this.dockPanel.Controls.Add(this.ProductPage);
            }
        }

        private void toolVisionEdit_Click(object sender, EventArgs e)
        {
            this.ChangedToToolBox();
            this.ToolBox.AddVisionEditCtrl();
        }

        private void toolBtn_Program_Click(object sender, EventArgs e)
        {
        
        }

        private void toolExpand_Click(object sender, EventArgs e)
        {
            if(this.dockPanel.Controls[0].GetType() == typeof(ToolBoxControl))
            {
                var ctrl = this.dockPanel.Controls[0] as ToolBoxControl;
                if(ctrl.toolPanel.Controls.Count > 0 && ctrl.toolPanel.Controls[0].GetType() == typeof(ProgramEditCtrl))
                {
                    Common.Variable.IsExpand = false;
                    frm_Expand.Instance.Module = (ctrl.toolPanel.Controls[0] as ProgramEditCtrl).Module;
                }

                frm_Expand.Instance.Show();
            }
        }

        private void toolEditProgram_Click(object sender, EventArgs e)
        {
            this.ChangedToToolBox();
            this.ToolBox.AddProgramEditCtrl();
        }

        private void toolLoad_Click(object sender, EventArgs e)
        {
            this.LoadProgram(Module.Front);
        }

        private void toolLoadRight_Click(object sender, EventArgs e)
        {
            this.LoadProgram(Module.After);
        }

        private void LoadProgram(Module module)
        {
            CreateProgramCtrl fm = new CreateProgramCtrl(false);
            if (fm.ShowDialog() == DialogResult.OK)
            {
                string moduleName = CommonHelper.GetEnumDescription(typeof(Module), module);

                var flow = ProgramFlow.Load($"{PathDefine.sPathProgram}{moduleName}//{fm.ProgramName}.json");

                if (flow == null)
                {
                    MessageBox.Show($"导入[{fm.ProgramName}]程式失败!!!");
                    return;
                }

                if (flow.Module != module)
                {
                    MessageBox.Show("导入程式与所属模组不对应!!!");
                    return;
                }

                string rtn = SystemEntiy.Instance.FlowMachine[module].InstallProgram(flow);
                if (!string.IsNullOrEmpty(rtn))
                    MessageBox.Show($"{moduleName} 导入 [{fm.ProgramName}] 失败【{rtn}】");
                else
                {
                    MessageBox.Show($"{moduleName} 导入 [{fm.ProgramName}] 成功");
                }

            }
        }

        private void toolAuto_Slow_Click(object sender, EventArgs e)
        {
            SystemConfig.Instance.Machines[Module.Front].AutoSpeedMode = Motion.Shceme.AutoSlow;
            SystemConfig.Instance.Machines[Module.After].AutoSpeedMode = Motion.Shceme.AutoSlow;
        }

        private void toolAuto_Manual_Click(object sender, EventArgs e)
        {
            SystemConfig.Instance.Machines[Module.Front].AutoSpeedMode = Motion.Shceme.AutoNormal;
            SystemConfig.Instance.Machines[Module.After].AutoSpeedMode = Motion.Shceme.AutoNormal;
        }

        private void toolAuto_Fast_Click(object sender, EventArgs e)
        {
            SystemConfig.Instance.Machines[Module.Front].AutoSpeedMode = Motion.Shceme.AutoFast;
            SystemConfig.Instance.Machines[Module.After].AutoSpeedMode = Motion.Shceme.AutoFast;
        }

        private void toolHardwareCliab_Click(object sender, EventArgs e)
        {
            if (Common.CommonHelper.InputCheck())
            {
                this.ChangedToToolBox();
                this.ToolBox.AddHardwareCtrl();
            }
        }

        private void toolSoftwareCliab_Click(object sender, EventArgs e)
        {
            if (Common.CommonHelper.InputCheck())
            {
                this.ChangedToToolBox();
                this.ToolBox.AddSoftwareCliab();
            }
        }

        private void toolPressCliab_Click(object sender, EventArgs e)
        {
            PressSensorSetting fm = new PressSensorSetting();
            fm.ShowDialog();
        }

        private void frm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Motion.MotionHelper.Instance.bSystemExit = true;
            SystemEntiy.Instance.SystemExit();
        }

        private void toolBtn_ReCheck_Click(object sender, EventArgs e)
        {

        }

        private void toolChangeDay_Click(object sender, EventArgs e)
        {
            fmReportConfig fm = new fmReportConfig();
            fm.ShowDialog();
        }

        private void toolMesConfig_Click(object sender, EventArgs e)
        {
          
        }

        private void toolPressConfig_Click(object sender, EventArgs e)
        {
           
        }
    }
}
