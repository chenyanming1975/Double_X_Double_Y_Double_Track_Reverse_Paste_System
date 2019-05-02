using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Flow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Cliab
{ 
    public partial class fm_Hardware : UserControl
    {
        public fm_Hardware()
        {
            InitializeComponent();
            this.wizardControl1.CancelButtonClick += WizardControl1_CancelButtonClick;
            this.axisOffsetItem1.Aixs = Motion.GeneralAxis.X;
            this.axisOffsetItem2.Aixs = Motion.GeneralAxis.Y;
        }

        private void WizardControl1_CancelButtonClick(object sender, EventArgs e)
        {
            this.CloseEvent?.Invoke(this, e);
        }

        public event EventHandler CloseEvent;

        private void bAutoCliab1_Click(object sender, EventArgs e)
        {

        }

        private void bLeanTemp_Click(object sender, EventArgs e)
        {

        }

        private void bGoOrg_Click(object sender, EventArgs e)
        {

        }

        private void bSetOrg_Click(object sender, EventArgs e)
        {

        }

        private void bSetNzCenter_Click(object sender, EventArgs e)
        {

        }

        private void bCalRotateCenter_Click(object sender, EventArgs e)
        {

        }

        private void bMoveNzCenter_Click(object sender, EventArgs e)
        {

        }

        private void bMoveRotateCenter_Click(object sender, EventArgs e)
        {

        }

        private void bCheckNz2Cam_Click(object sender, EventArgs e)
        {

        }

        private void bCheckCam2Nz_Click(object sender, EventArgs e)
        {

        }

        private void bCalcDist_Click(object sender, EventArgs e)
        {

        }

        private void bRecrodUp_Click(object sender, EventArgs e)
        {

        }

        private void bRecrodPaste_Click(object sender, EventArgs e)
        {

        }

        private void bRecrodDown_Click(object sender, EventArgs e)
        {

        }

        #region 机械校正
        private PointF Org = new PointF();
        private PointF RightBottom = new PointF();
        private PointF LeftTop = new PointF();
        private PointF RightTop = new PointF();

        private void bFindOrg_Click(object sender, EventArgs e)
        {
            Org = SystemEntiy.Instance[this.module.Module].XYPos;
            //Org = HardwareOrgHelper.Instance[this.module.Module, SystemEntiy.Instance[this.module.Module].XYPos];
        }

        private void bLeftTop_Click(object sender, EventArgs e)
        {
            LeftTop = SystemEntiy.Instance[this.module.Module].XYPos;
        }

        private void bRightBottom_Click(object sender, EventArgs e)
        {
            RightBottom = SystemEntiy.Instance[this.module.Module].XYPos;
        }

        private void bRightTop_Click(object sender, EventArgs e)
        {
            RightTop = SystemEntiy.Instance[this.module.Module].XYPos;
        }

        private void bCal_Click(object sender, EventArgs e)
        {
            double angle = Math.Abs(MathHelper.GetAngle(Org, RightBottom, Org, LeftTop));
            tXYAngle.Value = (decimal)angle;
            //double distX = MathHelper.GetDist(RightBottom, Org);
            //double distY = MathHelper.GetDist(LeftTop, Org);
            //double taryWidth = double.Parse(this.tTrayWidth.Text);
            //double taryHeight = double.Parse(this.tTrayHeight.Text);

            //this.tTrayWidth_Temp.Text = distX.ToString();
            //this.tTrayHeight_Temp.Text = distY.ToString();
            //double scaleX = distX / taryWidth;
            //double scaleY = distY / taryHeight;
            //this.tXScale.Text = scaleX.ToString();
            //this.tYScale.Text = scaleY.ToString();
            this.bUpdate.Enabled = true;
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            var module = this.module.Module;
            HardwareOrgHelper.Instance[module].LeftBottom = Org;
            HardwareOrgHelper.Instance[module].RightBottom = RightBottom;
            HardwareOrgHelper.Instance[module].LeftTop = LeftTop;
            HardwareOrgHelper.Instance[module].RightTop = RightTop;
            HardwareOrgHelper.Instance[module].XYCroodAngle = (double)this.tXYAngle.Value;
            double deg = (HardwareOrgHelper.Instance[module].XYCroodAngle - 90) / 180.0 * Math.PI;
            HardwareOrgHelper.Instance[module].XRate = Math.Cos(deg);
            HardwareOrgHelper.Instance[module].YRate = Math.Sin(deg);


            //this.bUpdate.Enabled = false;
            HardwareOrgHelper.Save();
        }

        private void module_ModuleChange(object sender, Config.Module e)
        {
            //this.bUpdate.Enabled = false;
            Org = HardwareOrgHelper.Instance[e].LeftBottom;
            RightBottom = HardwareOrgHelper.Instance[e].RightBottom = RightBottom;
            LeftTop = HardwareOrgHelper.Instance[e].LeftTop;
            RightTop = HardwareOrgHelper.Instance[e].RightTop;
            this.tXYAngle.Value = (decimal)HardwareOrgHelper.Instance[e].XYCroodAngle;
        }
        #endregion

        private void bUpdateByCliabed_Click(object sender, EventArgs e)
        {
            var org = SystemEntiy.Instance[this.module.Module].MachinePtToActPt(Org);
            var leftTop = SystemEntiy.Instance[this.module.Module].MachinePtToActPt(LeftTop);
            var rightBottom = SystemEntiy.Instance[this.module.Module].MachinePtToActPt(RightBottom);

            double angle = Math.Abs(MathHelper.GetAngle(org, rightBottom, org, leftTop));
            tXYAngle.Text = angle.ToString();
            double distX = MathHelper.GetDist(rightBottom, org);
            double distY = MathHelper.GetDist(leftTop, org);
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"是否初始化[{this.module.Module}模组]机械校验!!! Y/N", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                HardwareOrgHelper.Instance.HardWare[this.module.Module] = new HardwareItem();
                HardwareOrgHelper.Save();
                this.module_ModuleChange(this, this.module.Module);
            }
        }

        private void moduleRadio2_ModuleChange(object sender, Module e)
        {
            this.axisOffsetItem1.Module = e;
        }

        private void moduleRadio3_ModuleChange(object sender, Module e)
        {
            this.axisOffsetItem2.Module = e;
        }

        private void module_ModuleChange_1(object sender, Module e)
        {
            this.axisOffsetItem1.Module = e;
            this.axisOffsetItem2.Module = e;
        }
    }
}
