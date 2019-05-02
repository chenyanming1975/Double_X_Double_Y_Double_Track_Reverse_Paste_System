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
using GeneralMachine.Track;
using GeneralMachine.Flow;

namespace GeneralMachine.Product
{
    public partial class ProducteCtrl : UserControl
    {
        public ProducteCtrl()
        {
            InitializeComponent();
        }
        public Module Module
        {
            get;
            set;
        } = Module.Front;

        private void SelectModuleChange(Module module)
        {
            this.Module = module;
        }
        private void ProducteCtrl_Load(object sender, EventArgs e)
        {
            this.moduleRadio1.ModuleChange += SelectModuleChange;
            this.cb_ManualTrackMode.Items.Clear();
            foreach(FlowInOutMode mode in Enum.GetValues(typeof(FlowInOutMode)))
            {
                this.cb_ManualTrackMode.Items.Add(Enum.GetName(typeof(FlowInOutMode), mode));
            }

            this.cb_ManualTrackMode.SelectedIndex = 0;
        }

        private void bInput_Click(object sender, EventArgs e)
        {

            TrackManager.Instance.TrackEntiy[SystemEntiy.Instance[Module].MachineConfig.UsedTrack].ManualInput((FlowInOutMode)this.cb_ManualTrackMode.SelectedIndex);
        }

        private void bOutput_Click(object sender, EventArgs e)
        {
            TrackManager.Instance.TrackEntiy[SystemEntiy.Instance[Module].MachineConfig.UsedTrack].ManualOutput((FlowInOutMode)this.cb_ManualTrackMode.SelectedIndex);
        }

        private void bByPass_Click(object sender, EventArgs e)
        {
            if (bAllOptor.Checked)
            {
                SystemEntiy.Instance.FlowMachine[Module.After].Start();
                SystemEntiy.Instance.FlowMachine[Module.Front].Start();
            }
            else
            {
                if(SystemEntiy.Instance.FlowMachine[this.Module].Program == null)
                {
                    MessageBox.Show($"{Common.CommonHelper.GetEnumDescription(typeof(Module), this.Module)}没有导入程式请重新导入再运行!!!");
                    return;
                }

                SystemEntiy.Instance[this.Module].ZGoSafeTillStop();

                SystemEntiy.Instance.FlowMachine[this.Module].Start();
            }
        }

        private void bTestRun_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bTestRun.Checked)
                SystemConfig.Instance.General.RunMode = RunMode.TestRun;
            else
                SystemConfig.Instance.General.RunMode = RunMode.Normal;
        }

        private void bHaltCycle_Click(object sender, EventArgs e)
        {
            if (bAllOptor.Checked)
            {
                SystemEntiy.Instance.FlowMachine[Module.After].Pause();
                SystemEntiy.Instance.FlowMachine[Module.Front].Pause();
            }
            else
            {
                SystemEntiy.Instance.FlowMachine[this.Module].Pause();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
