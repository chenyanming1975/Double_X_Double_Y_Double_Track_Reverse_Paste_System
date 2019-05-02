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
using System.Threading;

namespace GeneralMachine
{
    public partial class FeederConfigCtrl : UserControl
    {
        public FeederConfigCtrl()
        {
            InitializeComponent();
        }

        public FeederConfig FeederConfig
        {
            get
            {
                return this.propertyFeeder.SelectedObject as FeederConfig;
            }

            set
            {
                if (value != null)
                {
                    this.propertyFeeder.SelectedObject = value;
                    this.feederLabelInfoBindingSource.DataSource = value.Labels;
                    this.Visible = true;
                }
                else
                    this.Visible = false;
            }
        }

        public void ShowFeederInfo(FeederConfig config)
        {
            this.FeederConfig = config.Clone() as FeederConfig;
        }

        private void bMoveCam_Click(object sender, EventArgs e)
        {
            if(this.dGV_FeederLabelInfo.SelectedRows.Count > 0)
            {
                int labelIndex = this.dGV_FeederLabelInfo.SelectedRows[0].Index;
                SystemEntiy.Instance[this.FeederConfig.Module].XYGoPos(this.FeederConfig.Labels[labelIndex].GetPos());
            }
        }

        private void bMoveNz_Click(object sender, EventArgs e)
        {
            if (this.dGV_FeederLabelInfo.SelectedRows.Count > 0)
            {
                int labelIndex = this.dGV_FeederLabelInfo.SelectedRows[0].Index;
                SystemEntiy.Instance[this.FeederConfig.Module].XYGoPos(SystemEntiy.Instance[this.FeederConfig.Module].LabelToNz(this.nozzleRadio1.SelectNz,this.FeederConfig.Labels[labelIndex].GetPos()));
            }
        }

        private void bMoveSuckHeight_Click(object sender, EventArgs e)
        {
            if (this.dGV_FeederLabelInfo.SelectedRows.Count > 0)
            {
                int labelIndex = this.dGV_FeederLabelInfo.SelectedRows[0].Index;
                double zpos = SystemEntiy.Instance[this.FeederConfig.Module].MachineConfig.NozzleMap[this.nozzleRadio1.SelectNz].XIHeight;
                zpos += this.FeederConfig.Labels[labelIndex].ZHeight;
                SystemEntiy.Instance[this.FeederConfig.Module].ZGoPos(this.nozzleRadio1.SelectNz, zpos, Motion.Shceme.ManualNormal);
            }
        }

        private void bSuckTest_Click(object sender, EventArgs e)
        {
            if (this.dGV_FeederLabelInfo.SelectedRows.Count > 0)
            {
                int labelIndex = this.dGV_FeederLabelInfo.SelectedRows[0].Index;
                Task.Factory.StartNew(() =>
                {
                    PointF pt = SystemEntiy.Instance[this.FeederConfig.Module].LabelToNz(this.nozzleRadio1.SelectNz,this.FeederConfig.Labels[labelIndex].GetPos());
                    double zpos = SystemEntiy.Instance[this.FeederConfig.Module].MachineConfig.NozzleMap[this.nozzleRadio1.SelectNz].XIHeight;
                    zpos += this.FeederConfig.Labels[labelIndex].ZHeight;
                    SystemEntiy.Instance[this.FeederConfig.Module].XYGoPosTillStop(pt);
                    SystemEntiy.Instance[this.FeederConfig.Module].ZGoPosTillStop(this.nozzleRadio1.SelectNz, zpos, Motion.Shceme.ManualNormal);
                    IODefine.Instance.MachineIO[this.FeederConfig.Module].VaccumPO[(int)this.nozzleRadio1.SelectNz].SetIO(false);
                    IODefine.Instance.MachineIO[this.FeederConfig.Module].VaccumSuck[(int)this.nozzleRadio1.SelectNz].SetIO(true);
                    Thread.Sleep(500);

                    SystemEntiy.Instance[this.FeederConfig.Module].ZGoSafeTillStop();
                    SystemEntiy.Instance[this.FeederConfig.Module].XYGoPosTillStop(
                        SystemConfig.Instance.Machines[this.FeederConfig.Module].ReadyPoint);
                });
            }
        }

        private void bRecordCam_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < this.dGV_FeederLabelInfo.SelectedRows.Count; ++index)
            {
                int rowIndex = this.dGV_FeederLabelInfo.SelectedRows[index].Index;
                PointF pos = SystemEntiy.Instance[this.FeederConfig.Module].XYPos;

                this.FeederConfig.Labels[rowIndex].SuckX = pos.X;
                this.FeederConfig.Labels[rowIndex].SuckY = pos.Y;
                this.dGV_FeederLabelInfo.UpdateCellValue(2, rowIndex);
                this.dGV_FeederLabelInfo.UpdateCellValue(3, rowIndex);
            }
        }

        private void bRecordNZ_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < this.dGV_FeederLabelInfo.SelectedRows.Count; ++index)
            {
                int rowIndex = this.dGV_FeederLabelInfo.SelectedRows[index].Index;
                PointF pos = SystemEntiy.Instance[this.FeederConfig.Module].NzToLabel(this.nozzleRadio1.SelectNz, SystemEntiy.Instance[this.FeederConfig.Module].XYPos);
                this.FeederConfig.Labels[rowIndex].SuckX = pos.X;
                this.FeederConfig.Labels[rowIndex].SuckY = pos.Y;
                this.dGV_FeederLabelInfo.UpdateCellValue(2, rowIndex);
                this.dGV_FeederLabelInfo.UpdateCellValue(3, rowIndex);
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            FeederDefine.Instance.SaveFeederConfig(this.FeederConfig);
        }

        private void bAddRow_Click(object sender, EventArgs e)
        {
            this.feederLabelInfoBindingSource.AddNew();
        }

        private void bDeleteRow_Click(object sender, EventArgs e)
        {
            if (this.dGV_FeederLabelInfo.SelectedRows.Count > 0)
            {
                this.feederLabelInfoBindingSource.RemoveCurrent();
            }
        }
    }
}
