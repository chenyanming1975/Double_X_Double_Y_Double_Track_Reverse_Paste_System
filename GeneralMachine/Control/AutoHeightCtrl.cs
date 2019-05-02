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
using System.Threading;
using GeneralMachine.Flow;

namespace GeneralMachine
{
    public partial class AutoHeightCtrl : Form
    {
        public AutoHeightCtrl(Module module,Nozzle nozzle)
        {
            InitializeComponent();
            this.Nozzle = nozzle;
            this.Module = module;
        }

        public Nozzle Nozzle = Nozzle.Nz1;
        public Module Module = Module.Front;

        private void bStart_Click(object sender, EventArgs e)
        {
            double limitHiehgt = (double)this.numLimitHeight.Value;
            // 开真空
            // 压力清零

            IODefine.Instance.MachineIO[this.Module].VaccumPO[(int)this.Nozzle].SetIO(false);
            IODefine.Instance.MachineIO[this.Module].VaccumSuck[(int)this.Nozzle].SetIO(true);
            Thread.Sleep(200);
            bool find = false;
            SystemEntiy.Instance[this.Module].ZGoSafeTillStop();
            Thread.Sleep(200);
            double dist = 0.5;
            if (this.Nozzle == Nozzle.Nz2 || this.Nozzle == Nozzle.Nz3)
                dist = -0.5;

            while (Math.Abs(SystemEntiy.Instance[this.Module].MachineAxis.Z[(int)Nozzle].Pos - limitHiehgt) > 0.5)
            {
                if (this.cbUseVaccum.Checked && IODefine.Instance.MachineIO[this.Module].VaccumCheck[(int)this.Nozzle].GetIO())
                {
                    find = true;
                    break;
                }

                SystemEntiy.Instance[this.Module].ZGoPosTillStop(this.Nozzle, SystemEntiy.Instance[this.Module].MachineAxis.Z[(int)this.Nozzle].Pos + dist);
            }

            
            MessageBox.Show(find ? "自动对高成功" : "自动对高失败");

            if (find)
            {
                this.DialogResult = DialogResult.Yes;
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void AutoHeightCtrl_Load(object sender, EventArgs e)
        {
            try
            {
                this.numLimitHeight.Value = (decimal)SystemConfig.Instance.Machines[this.Module][this.Nozzle].PasteHeight;
            }
            catch { }
        }

        private void cbUsePress_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbUsePress.Checked)
                this.numLimitPress.Visible = true;
            else
                this.numLimitPress.Visible = false;
        }
    }
}
