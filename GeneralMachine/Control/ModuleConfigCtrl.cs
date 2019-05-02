using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Config;
using GeneralMachine.Flow;

namespace GeneralMachine.SystemManager
{
    public partial class ModuleConfigCtrl : UserControl
    {
        public ModuleConfigCtrl()
        {
            InitializeComponent();

            foreach(Nozzle nozzle in Enum.GetValues(typeof(Nozzle)))
            {
                this.cb_SelectZ.Items.Add(Common.CommonHelper.GetEnumDescription(typeof(Nozzle), nozzle));
            }
            this.cb_SelectZ.SelectedIndex = 0;

            this.moduleRadio1.ModuleChange += (sender,module) =>
            {
                try
                {
                    this.module = module;
                    machineConfig = SystemConfig.Instance.Machines[this.module].Clone() as MachineConfig;
                    this.zuConfigCtrl1.NozzleConfig = SystemConfig.Instance.Machines[this.module][this.selectNz].Clone() as NozzleConfig;

                    this.zuConfigCtrl1.Module = this.module;
                    this.dropPos.Set(this.module);
                    this.waitPos.Set(this.module);
                    this.xLimit.Axis = SystemEntiy.Instance[this.module].MachineAxis.X;
                    this.yLimit.Axis = SystemEntiy.Instance[this.module].MachineAxis.Y;
                    this.tXWorkLimit.Axis = SystemEntiy.Instance[this.module].MachineAxis.X;
                    this.tYWorkLimit.Axis = SystemEntiy.Instance[this.module].MachineAxis.Y;

                    UpdateToUI();
                }
                catch { }
            };
        }

        private Module module = Module.Front;
        private MachineConfig machineConfig = new MachineConfig();
        private Nozzle selectNz = Nozzle.Nz1;

        public MachineConfig Config
        {
            get
            {
                return machineConfig;
            }

            set
            {
                if(value != null)
                 this.machineConfig = value.Clone() as MachineConfig;
            }
        }
        /// <summary>
        /// 数据更新到UI
        /// </summary>
        private void UpdateToUI()
        {
            this.waitPos.Point = this.machineConfig.ReadyPoint;
            this.dropPos.Point = this.machineConfig.DropPoint;
            this.tThrowDelay1.Text = this.machineConfig.DropDelay.ToString();
            this.xLimit.Range = this.machineConfig.XLimit;
            this.yLimit.Range = this.machineConfig.YLimit;
            this.tXWorkLimit.Range = this.machineConfig.XWorkRange;
            this.tYWorkLimit.Range = this.machineConfig.YWorkRange;
            this.tPasteAngle.Text = this.machineConfig.TrunPasteAngle.ToString("f3");
            this.tSuckAngle.Text = this.machineConfig.TrunSuckAngle.ToString("f3");
            this.zuConfigCtrl1.UpdateUI();
        }

        /// <summary>
        /// UI数据更新到数据上
        /// </summary>
        private void UIToValue()
        {
            this.machineConfig.ReadyPoint = this.waitPos.Point;
            this.machineConfig.DropPoint = this.dropPos.Point;
            this.machineConfig.DropDelay = int.Parse(this.tThrowDelay1.Text);
            this.machineConfig.XLimit = this.xLimit.Range;
            this.machineConfig.YLimit = this.yLimit.Range;
            this.machineConfig.XWorkRange = this.tXWorkLimit.Range;
            this.machineConfig.YWorkRange = this.tYWorkLimit.Range;
            this.machineConfig.NozzleMap[selectNz] = this.zuConfigCtrl1.NozzleConfig;
            this.machineConfig.TrunPasteAngle = float.Parse(this.tPasteAngle.Text);
            this.machineConfig.TrunSuckAngle = float.Parse(this.tSuckAngle.Text);
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            UIToValue();
            SystemConfig.Instance.Machines[this.module] = this.machineConfig;
            SystemConfig.Save();
        }

        private void cb_SelectZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectNz = (Nozzle)this.cb_SelectZ.SelectedIndex;
            this.zuConfigCtrl1.NozzleConfig = machineConfig[this.selectNz];
            this.zuConfigCtrl1.Nozzle = this.selectNz;
        }

        private void bSetTrunPasteAngle_Click(object sender, EventArgs e)
        {
            this.tPasteAngle.Text = SystemEntiy.Instance[module].MachineAxis.Trun.Pos.ToString("f3");
        }

        private void bSetSuckAngle_Click(object sender, EventArgs e)
        {
            this.tSuckAngle.Text = SystemEntiy.Instance[module].MachineAxis.Trun.Pos.ToString("f3");
        }

        private void ModuleConfigCtrl_Load(object sender, EventArgs e)
        {
            this.moduleRadio1.Module = Module.Front;
            this.cb_SelectZ.SelectedIndex = 0;
            //UpdateToUI();
        }
    }
}
