using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Cliab;
using GeneralMachine.Flow.Editer;
using GeneralMachine.Flow;
using GeneralMachine.Config;

namespace GeneralMachine
{
    public partial class ToolBoxControl : UserControl
    {
        public ToolBoxControl()
        {
            InitializeComponent();
          
        }

        #region 添加视觉标定控件
        public fm_SoftwareCliab fm_Software = null;
        public void AddSoftwareCliab()
        {
            if (fm_Software == null)
            {
                fm_Software = new fm_SoftwareCliab();
                fm_Software.Dock = DockStyle.Top;
                fm_Software.CloseEvent += Fm_Software_CloseEvent;
            }

            this.toolPanel.Controls.Add(fm_Software);
        }

        private void Fm_Software_CloseEvent(object sender, EventArgs e)
        {
            this.toolPanel.Controls.Remove(sender as Control);
        }
        #endregion

        #region 添加相机轴控界面
        public frm_Camera frm_Camera = null;
        public void AddCameraControl()
        {
            if(frm_Camera == null)
            {
                frm_Camera = new frm_Camera();
                frm_Camera.Dock = DockStyle.Top;
                frm_Camera.CloseEvent += Instance_CloseEvent;
            }

            this.camPanel.Controls.Add(frm_Camera);
        }

        private void Instance_CloseEvent(object sender, EventArgs e)
        {
            this.camPanel.Controls.Remove(frm_Camera);
        }
        #endregion

        #region 添加机械标定界面
        public fm_Hardware fm_Hardware = null;

        public void AddHardwareCtrl()
        {
            if(fm_Hardware == null)
            {
                fm_Hardware = new fm_Hardware();
                fm_Hardware.Dock = DockStyle.Top;
                fm_Hardware.CloseEvent += Fm_Hardware_CloseEvent;
            }

            this.toolPanel.Controls.Add(fm_Hardware);
        }

        private void Fm_Hardware_CloseEvent(object sender, EventArgs e)
        {
            this.toolPanel.Controls.Remove(sender as Control);
        }
        #endregion

        #region 添加视觉编辑界面
        public Vision.VisionToolCtrl visionEditCtrl = new Vision.VisionToolCtrl();

        public void AddVisionEditCtrl()
        {
            this.toolPanel.Controls.Clear();
            visionEditCtrl.Dock = DockStyle.Top;
            this.toolPanel.Controls.Add(visionEditCtrl);
        }
        #endregion

        #region 添加流程编辑界面
        public ProgramEditCtrl programEditCtrl = new ProgramEditCtrl();

        public void AddProgramEditCtrl()
        {
            this.toolPanel.Controls.Clear();
            programEditCtrl.Dock = DockStyle.Top;
            this.toolPanel.Controls.Add(programEditCtrl);
        }
        #endregion

        #region 添加Feeder编辑界面
        public frm_Feeder feederEditCtrl = new frm_Feeder();

        public void AddFeederEditCtrl()
        {
            this.toolPanel.Controls.Clear();
            feederEditCtrl.Dock = DockStyle.Top;
            this.toolPanel.Controls.Add(feederEditCtrl);
        }
        #endregion

        #region 添加空跑界面
        public frm_DryRun dryRunCtrl = new frm_DryRun();

        public void AddDryRunCtrl()
        {
            this.toolPanel.Controls.Clear();
            dryRunCtrl.Dock = DockStyle.Top;
            this.toolPanel.Controls.Add(dryRunCtrl);
        }
        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            frm_Camera?.KeyDown(ref msg, keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ToolBoxControl_Load(object sender, EventArgs e)
        {
            //if (SystemEntiy.Instance.WorkStatus == Definition.WorkStatus.Ready)
            //{
            //    this.axisStateFront.Module = Module.Front;
            //    this.axisStateAfter.Module = Module.After;
            //    this.axisStateFront.StartMointor();
            //    this.axisStateAfter.StartMointor();
            //}
        }
    }
}
