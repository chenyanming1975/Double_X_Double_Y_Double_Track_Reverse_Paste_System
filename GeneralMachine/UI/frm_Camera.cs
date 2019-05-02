
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using GeneralMachine.Motion;
using NationalInstruments.Vision.Acquisition.Imaqdx;
using NationalInstruments.Vision;
using NationalInstruments.Vision.Analysis;
using GeneralMachine.Definition;
using NationalInstruments.Vision.WindowsForms;
using System.Threading;
using GeneralMachine.Flow;
using GeneralMachine.Config;

namespace GeneralMachine
{

    public enum MoveFunc
    {
        Jog,// 持续
        MoveTrim,// 点动
    }
    public partial class frm_Camera : DockContent
    {
        private static frm_Camera instance = null;

        public static frm_Camera Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new frm_Camera();
                return instance;
            }
        }

        private frm_Camera()
        {
            InitializeComponent();
        }

        #region 参数
        /// <summary>
        /// 1-模组1 2-模组2
        /// </summary>
        private Module Module = Module.Front;

        /// <summary>
        /// 1-上相机 2-下相机
        /// </summary>
        private Camera Camera = Camera.Top;

        /// <summary>
        /// 0-吸嘴1 1-吸嘴2
        /// </summary>
        private Nozzle NzIndex = Nozzle.Nz1;
        #endregion

        #region 移动参数
        /// <summary>
        /// 1-连续 2-寸动
        /// </summary>
        private MoveFunc MoveFunc = MoveFunc.Jog;

        /// <summary>
        /// 寸动距离
        /// </summary>
        private double XYMoveDist = 1;

        /// <summary>
        /// 寸动距离
        /// </summary>
        private double ZMoveDist = 1;

        /// <summary>
        /// 寸动距离
        /// </summary>
        private double RMoveDist = 1;

        private double TurnMoveDist = 1;

        private Shceme XYSpeedMode = Shceme.ManualNormal;

        private Shceme ZSpeedMode = Shceme.ManualNormal;

        private Shceme RSpeedMode = Shceme.ManualNormal;

        private Shceme TurnSpeedMode = Shceme.ManualNormal;
        #endregion

        #region 实时拍照
        private uint bufNum;

        /// <summary>
        /// true: 正在实时拍照 false:未拍照
        /// </summary>
        private bool bSet_CamLive;
        private bool bSet_Cross;

        private void imageSet_ImageMouseDown(object sender, ImageMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (cB_MouseTrim.Checked)
                {
                    PointF pt = SystemEntiy.Instance[Module].XYPos;
                    PointF temp_Point = new PointF();
                    temp_Point.X = (float)e.Point.X;// *imageViewer2.Image.Width / imageViewer2.Width;
                    temp_Point.Y = (float)e.Point.Y;// *imageViewer2.Image.Height / imageViewer2.Height;
                }
            }
        }

        private void bSetHardTrigger_CheckedChanged(object sender, EventArgs e)
        {
            if (bSet_CamLive)
            {
                MessageBox.Show("请先关闭实时取像!!!");
                return;
            }

            Thread.Sleep(200);
        }

        private void cameraComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bSet_CamLive)
            {
                this.bCamLive_Click(this, new EventArgs());
            }

            Camera = (Camera)(cameraComboBox.SelectedIndex);
        }


        #endregion

        private void frm_Camera_Load(object sender, EventArgs e)
        {
            cameraComboBox.SelectedIndex = 0;
            cB_NozzleIndex.SelectedIndex = 0;
            rB_Jog.Checked = true;

            this.moduleRadio1.ModuleChange += new Action<Module>((module) => {
                if (bSet_CamLive)
                {
                    this.bCamLive_Click(this, new EventArgs());
                }

                this.Module = module;
                //foreach(Module mod in Enum.GetValues(typeof(Module)))
                //{
                //    if(mod != module)
                //        SystemEntiy.Instance[mod].XYGoSafePt();
                //}
            });
        }

        /// <summary>
        /// 移动方式变更
        /// </summary>
        private void MoveFuncChange()
        {
            if (rB_Jog.Checked)
            {
                MoveFunc = MoveFunc.Jog;
                bXY_Mode.ContextMenuStrip = this.selectSpeedMode;
                bZ_Mode.ContextMenuStrip = this.selectSpeedMode;
                bU_Mode.ContextMenuStrip = this.selectSpeedMode;
                bTurn_Mode.ContextMenuStrip = this.selectSpeedMode;

                bXY_Mode.Text = "中速";
                bZ_Mode.Text = "中速";
                bU_Mode.Text = "中速";
                bTurn_Mode.Text = "中速";
            }
            else
            {
                MoveFunc = MoveFunc.MoveTrim;
                bXY_Mode.ContextMenuStrip = this.selectMoveDist;
                bZ_Mode.ContextMenuStrip = this.selectMoveDist;
                bU_Mode.ContextMenuStrip = this.selectMoveDist;

                bXY_Mode.Text = "0.1";
                bZ_Mode.Text = "0.1";
                bU_Mode.Text = "0.1";
                bTurn_Mode.Text = "0.1";
            }
        }

        private void rB_Jog_CheckedChanged(object sender, EventArgs e)
        {
            MoveFuncChange();
        }

        private void rB_Trim_CheckedChanged(object sender, EventArgs e)
        {
            MoveFuncChange();
        }

        private void bStopAxis_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[Module].StopAllAxis();
        }

        #region[camera]****************************************************************
        private void bCamLive_Click(object sender, EventArgs e)
        {
            if (!bSet_CamLive)
            {
                try
                {

                    bSet_CamLive = true;
                    bCamLive.Text = "关闭实时";
                    CameraDefine.Instance.Camera[this.Module][Camera].UIStartGrab(this.imageSet.imageSet);
                    bSnap.Enabled = false;
                }
                catch
                {
                }
            }
            else
            {
                try//关闭相机
                {
                    bSet_CamLive = false;
                    CameraDefine.Instance.Camera[this.Module][Camera].StopGrab();
                    bCamLive.Text = "实时";
                    bSnap.Enabled = true;

                }
                catch
                {
                }
            }
        }

        private void bSnap_Click(object sender, EventArgs e)
        {
            try
            {
                CameraDefine.Instance.Camera[this.Module][Camera].UISnap();
            }
            catch { }
        }

        private void bShowCross_Click(object sender, EventArgs e)
        {
            if (bSet_Cross == false)
            {
                bSet_Cross = true;
                bShowCross.Text = "关闭十字";
            }
            else
            {
                bSet_Cross = false;
                bShowCross.Text = "显示十字";
            }
        }

        private void bLoadPic_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "打开图片";
            openFileDialog.FileName = "1";
            openFileDialog.Filter = "图片(*.bmp)|*.bmp|(*.png)|*.png";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageSet.imageSet.Image.ReadFile(openFileDialog.FileName, true);
            }
        }

        private void bSavePic_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "图片另存为";
            saveFileDialog.FileName = "1";
            saveFileDialog.Filter = "图片(*.bmp)|*.bmp";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageSet.imageSet.Image.WriteBmpFile(saveFileDialog.FileName);
            }
        }

        private void bAngle_Click(object sender, EventArgs e)
        {
            try
            {
                Algorithms.Rotate(imageSet.imageSet.Image, imageSet.imageSet.Image, double.Parse(tAngle.Text));
            }
            catch
            {
            }
        }

        private void bUpdateShutter_Click(object sender, EventArgs e)
        {
            try
            {
                CameraDefine.Instance.Camera[this.Module][Camera].Exposure = (int)ntCamShutter.Value;
            }
            catch
            {
            }
        }

        private void bZoomFit_Click(object sender, EventArgs e)
        {
            imageSet.imageSet.ZoomToFit = true;
        }
        #endregion

        private void cB_NozzleIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            NzIndex = (Nozzle)(cB_NozzleIndex.SelectedIndex);
        }

        private void bX_Click(object sender, EventArgs e)
        {
            try
            {
                double temp = double.Parse(tX.Text);
                SystemEntiy.Instance[Module].XGoPos(temp);
            }
            catch
            {
                frm_MessageBox.ShowMessage("输入坐标有误轻重新输入");
            }
        }

        private void bY_Click(object sender, EventArgs e)
        {
            try
            {
                double temp = double.Parse(tY.Text);
                SystemEntiy.Instance[Module].YGoPos(temp);
            }
            catch
            {
                frm_MessageBox.ShowMessage("输入坐标有误轻重新输入");
            }
        }

        private void bU_Click(object sender, EventArgs e)
        {
            try
            {
                double temp = double.Parse(tU.Text);
                SystemEntiy.Instance[Module].RGoAngle(temp, NzIndex);
            }
            catch
            {
                frm_MessageBox.ShowMessage("输入坐标有误轻重新输入");
            }
        }

        private void bUInit_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[Module].RGoAngle(0, NzIndex);
        }



        #region 点动
        private void p_XN_Click_1(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.MoveTrim)
            {
                SystemEntiy.Instance[Module].MachineAxis.X.MoveTrim(-XYMoveDist, SpeedDefine.Instance[Module][Shceme.ManualNormal, GeneralAxis.X]);
            }
        }

        private void p_YP_Click_1(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.MoveTrim)
            {
                SystemEntiy.Instance[Module].MachineAxis.Y.MoveTrim(XYMoveDist, SpeedDefine.Instance[Module][Shceme.ManualNormal, GeneralAxis.Y]);
            }
        }

        private void p_YN_Click_1(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.MoveTrim)
            {
                SystemEntiy.Instance[Module].MachineAxis.Y.MoveTrim(-XYMoveDist, SpeedDefine.Instance[Module][Shceme.ManualNormal, GeneralAxis.Y]);
            }
        }

        private void p_XP_Click_1(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.MoveTrim)
            {
                SystemEntiy.Instance[Module].MachineAxis.X.MoveTrim(XYMoveDist, SpeedDefine.Instance[Module][Shceme.ManualNormal, GeneralAxis.X]);
            }
        }


        private void p_ZP_Click_1(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.MoveTrim)
            {
                SystemEntiy.Instance[Module].MachineAxis.Z[(int)NzIndex].MoveTrim(ZMoveDist, SpeedDefine.Instance[Module][Shceme.ManualNormal, GeneralAxis.Z]);
            }
        }

        private void p_ZN_Click_1(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.MoveTrim)
            {
                SystemEntiy.Instance[Module].MachineAxis.Z[(int)NzIndex].MoveTrim(-ZMoveDist, SpeedDefine.Instance[Module][Shceme.ManualNormal, GeneralAxis.Z]);
            }
        }

        private void pUN_Click_1(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.MoveTrim)
            {
                SystemEntiy.Instance[Module].MachineAxis.R[(int)NzIndex].MoveTrim(-RMoveDist, SpeedDefine.Instance[Module][Shceme.ManualNormal, GeneralAxis.Z]);
            }
        }

        private void pUP_Click_1(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.MoveTrim)
            {
                SystemEntiy.Instance[Module].MachineAxis.R[(int)NzIndex].MoveTrim(RMoveDist, SpeedDefine.Instance[Module][Shceme.ManualNormal, GeneralAxis.Z]);
            }
        }
        #endregion

        #region 持续移动
        private void p_YP_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
            {
                this.p_YP.Image = GeneralMachine.Properties.Resources.Up2;
                SystemEntiy.Instance[Module].YJog(true, XYSpeedMode);
            }
        }

        private void p_XP_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
            {
                this.p_XP.Image = GeneralMachine.Properties.Resources.Right2;
                SystemEntiy.Instance[Module].XJog(true, XYSpeedMode);
            }
        }

        private void p_XN_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
            {
                this.p_XN.Image = GeneralMachine.Properties.Resources.Left2;
                SystemEntiy.Instance[Module].XJog(false, XYSpeedMode);
            }
        }

        private void p_YN_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
            {
                this.p_YN.Image = GeneralMachine.Properties.Resources.Down2;
                SystemEntiy.Instance[Module].YJog(false, XYSpeedMode);
            }
        }

        private void p_YP_MouseUp_1(object sender, MouseEventArgs e)
        {
            this.p_YN.Image = GeneralMachine.Properties.Resources.Down;
            this.p_YP.Image = GeneralMachine.Properties.Resources.Up;
            this.p_XN.Image = GeneralMachine.Properties.Resources.Left;
            this.p_XP.Image = GeneralMachine.Properties.Resources.Right;
            SystemEntiy.Instance[Module].StopAllAxis();
        }
        #endregion

        #region 选择速度 和 移动距离
        public Shceme GetShceme(string text)
        {
            if (text == "慢速")
                return Shceme.MaunalSlow;
            else if (text == "快速")
                return Shceme.ManualFast;

            return Shceme.ManualNormal;
        }

        private void bXY_Mode_ContextMenuStripChanged(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
                XYSpeedMode = GetShceme(this.bXY_Mode.Text);
            else
                XYMoveDist = double.Parse(this.bXY_Mode.Text);
        }

        private void bZ_Mode_ContextMenuStripChanged(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
                ZSpeedMode = GetShceme(this.bZ_Mode.Text);
            else
                ZMoveDist = double.Parse(this.bZ_Mode.Text);
        }

        private void bU_Mode_ContextMenuStripChanged(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
                RSpeedMode = GetShceme(this.bU_Mode.Text);
            else
                RMoveDist = double.Parse(this.bU_Mode.Text);

        }

        private void bTurn_Mode_ContextMenuStripChanged(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
                TurnSpeedMode = GetShceme(this.bTurn_Mode.Text);
            else
                TurnMoveDist = double.Parse(this.bTurn_Mode.Text);
        }
        #endregion

        #region 轨道操作
        private void p_CN_MouseDown_1(object sender, MouseEventArgs e)
        {
            SystemEntiy.Instance.GetTrack(Module).ConveryRun(false);
        }

        private void p_CP_MouseDown_1(object sender, MouseEventArgs e)
        {
            SystemEntiy.Instance.GetTrack(Module).ConveryRun(true);
        }

        private void p_CP_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (rB_Realse.Checked)
                SystemEntiy.Instance.GetTrack(Module).ConveryStop();
        }

        private void bStopC_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance.GetTrack(Module).ConveryStop();
        }

        private void bOpCarry_Click(object sender, EventArgs e)
        {
            // 是否在原点 
            if (SystemEntiy.Instance.GetTrack(Module).TrackIO.IO_CarryOrg.GetIO())
            {
                SystemEntiy.Instance.GetTrack(Module).CarryMove(true);
                this.bOpCarry.Text = "顶升-降";
            }
            else
            {
                SystemEntiy.Instance.GetTrack(Module).CarryMove(false);
                this.bOpCarry.Text = "顶升-升";
            }
        }

        private void bOpStop_Click(object sender, EventArgs e)
        {
            // 是否在原点 
            if (SystemEntiy.Instance.GetTrack(Module).TrackIO.IO_StopDone.GetIO())
            {
                SystemEntiy.Instance.GetTrack(Module).StopMove(false);
                this.bOpStop.Text = "阻挡-升";
            }
            else
            {
                SystemEntiy.Instance.GetTrack(Module).StopMove(true);
                this.bOpStop.Text = "阻挡-降";
            }
        }
        #endregion

        #region 翻转轴操作
        private void bTurnN_Click(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.MoveTrim)
                SystemEntiy.Instance[Module].MachineAxis.Trun.MoveTrim(-TurnMoveDist, SpeedDefine.Instance[Module][Shceme.ManualNormal, GeneralAxis.TRUN]);
        }

        private void bTurnP_Click(object sender, EventArgs e)
        {
            if (MoveFunc == MoveFunc.MoveTrim)
                SystemEntiy.Instance[Module].MachineAxis.Trun.MoveTrim(TurnMoveDist, SpeedDefine.Instance[Module][Shceme.ManualNormal, GeneralAxis.TRUN]);
        }

        private void bTurnN_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
            {
                this.bTurnN.Image = GeneralMachine.Properties.Resources.RN2;
                SystemEntiy.Instance[Module].TurnJog(false, Shceme.ManualNormal);
            }
        }

        private void bTurnP_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
            {
                this.bTurnP.Image = GeneralMachine.Properties.Resources.RS2;
                SystemEntiy.Instance[Module].TurnJog(true);
            }
        }


        private void bTurnP_MouseUp(object sender, MouseEventArgs e)
        {
            this.bTurnN.Image = GeneralMachine.Properties.Resources.RN;
            this.bTurnP.Image = GeneralMachine.Properties.Resources.RS;
            SystemEntiy.Instance[Module].MachineAxis.Trun.Stop(true);
        }

        private void bTurnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (SystemEntiy.Instance[Module].XYReach(SystemConfig.Instance.Machines[Module].ReadyPoint))
                {
                    double pos = double.Parse(this.tTurnPos.Text);
                    SystemEntiy.Instance[Module].TurnGoPos(pos);
                }
                else
                {
                    SystemEntiy.Instance[Module].XYGoPos(SystemConfig.Instance.Machines[Module].ReadyPoint);
                }
            }
            catch { }
        }

        private void bTurnGoPaste_Click(object sender, EventArgs e)
        {
            if (SystemEntiy.Instance[Module].XYReach(SystemConfig.Instance.Machines[Module].ReadyPoint))
            {
                SystemEntiy.Instance[Module].TurnGoPaste();
            }
            else
            {
                SystemEntiy.Instance[Module].XYGoPos(SystemConfig.Instance.Machines[Module].ReadyPoint);
            }
        }

        private void bTurnGoSuck_Click(object sender, EventArgs e)
        {
            if (SystemEntiy.Instance[Module].XYReach(SystemConfig.Instance.Machines[Module].ReadyPoint))
            {
                SystemEntiy.Instance[Module].TurnGoSuck();
            }
            else
            {
                SystemEntiy.Instance[Module].XYGoPos(SystemConfig.Instance.Machines[Module].ReadyPoint);
            }
        }
        #endregion

        private void bUpGoNz_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[Module].XYGoPos(SystemEntiy.Instance[Module].CamToNozzle(NzIndex, SystemEntiy.Instance[Module].XYPos));
        }

        private void bNzGoUpCam_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[Module].XYGoPos(SystemEntiy.Instance[Module].NozzleToCam(NzIndex, SystemEntiy.Instance[Module].XYPos));
        }

        private void p_ZN_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
            {
                p_ZN.Image = GeneralMachine.Properties.Resources.Up2;
                SystemEntiy.Instance[Module].ZJog(NzIndex, false, ZSpeedMode);
            }
        }

        private void p_ZP_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
            {
                p_ZP.Image = GeneralMachine.Properties.Resources.Down2;
                SystemEntiy.Instance[Module].ZJog(NzIndex, true, ZSpeedMode);
            }
        }

        private void p_ZP_MouseUp(object sender, MouseEventArgs e)
        {
            p_ZN.Image = GeneralMachine.Properties.Resources.Up;
            p_ZP.Image = GeneralMachine.Properties.Resources.Down;
            SystemEntiy.Instance[Module].MachineAxis.Z[(int)NzIndex].Stop();
        }

        private void pUN_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
            {
                pUN.Image = GeneralMachine.Properties.Resources.RN2;
                SystemEntiy.Instance[Module].RJog(NzIndex, true, RSpeedMode);
            }
        }

        private void pUP_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveFunc == MoveFunc.Jog)
            {
                pUP.Image = GeneralMachine.Properties.Resources.RS2;
                SystemEntiy.Instance[Module].RJog(NzIndex, true, RSpeedMode);
            }
        }

        private void pUP_MouseUp(object sender, MouseEventArgs e)
        {
            pUN.Image = GeneralMachine.Properties.Resources.RN;
            pUP.Image = GeneralMachine.Properties.Resources.RS;
            SystemEntiy.Instance[Module].MachineAxis.R[(int)NzIndex].Stop();
        }

        private void bGoSafeHeight_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[Module].ZGoSafe(NzIndex);
        }

        private void bGoPasteHeight_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[Module].ZGoPaste(NzIndex);
        }

        private void bGoSuckHeight_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[Module].ZGoSuck(NzIndex);
        }

        private void bOpVaccum_Click(object sender, EventArgs e)
        {
            var temp = SystemEntiy.Instance[Module].MachineIO.VaccumSuck[(int)NzIndex];
            if (temp.GetIO())
            {
                this.bOpVaccum.BackColor = Color.Green;
                this.bOpVaccum.Text = "真空-关";
                temp.SetIO(true);
            }
            else
            {
                this.bOpVaccum.BackColor = Color.Transparent;
                this.bOpVaccum.Text = "真空-开";
                temp.SetIO(false);
            }
        }

        private void bOpBreak_Click(object sender, EventArgs e)
        {
            var temp = SystemEntiy.Instance[Module].MachineIO.VaccumPO[(int)NzIndex];
            if (temp.GetIO())
            {
                this.bOpBreak.BackColor = Color.Green;
                this.bOpBreak.Text = "破真空-关";
                temp.SetIO(true);
            }
            else
            {
                this.bOpBreak.BackColor = Color.Transparent;
                this.bOpBreak.Text = "破真空-开";
                temp.SetIO(false);
            }
        }

        private void bOpAllVaccum_Click(object sender, EventArgs e)
        {
            var temp = SystemEntiy.Instance[Module].MachineIO.VaccumSuck[(int)NzIndex];
            if (temp.GetIO())
            {
                this.bOpAllVaccum.BackColor = Color.Green;
                this.bOpAllVaccum.Text = "All真空-关";
                foreach (var nz in Enum.GetValues(typeof(Nozzle)))
                {
                    SystemEntiy.Instance[Module].MachineIO.VaccumSuck[(int)nz].SetIO(true);
                }
            }
            else
            {
                this.bOpAllVaccum.BackColor = Color.Transparent;
                this.bOpAllVaccum.Text = "All真空-开";
                foreach (var nz in Enum.GetValues(typeof(Nozzle)))
                {
                    SystemEntiy.Instance[Module].MachineIO.VaccumSuck[(int)nz].SetIO(false);
                }
            }
        }

        private void bOpAllBreak_Click(object sender, EventArgs e)
        {
            var temp = SystemEntiy.Instance[Module].MachineIO.VaccumPO[(int)NzIndex];
            if (temp.GetIO())
            {
                this.bOpAllBreak.BackColor = Color.Green;
                this.bOpAllBreak.Text = "All破真空-关";
                foreach (var nz in Enum.GetValues(typeof(Nozzle)))
                {
                    SystemEntiy.Instance[Module].MachineIO.VaccumPO[(int)nz].SetIO(true);
                }
            }
            else
            {
                this.bOpAllBreak.BackColor = Color.Transparent;
                this.bOpAllBreak.Text = "All破真空-开";
                foreach (var nz in Enum.GetValues(typeof(Nozzle)))
                {
                    SystemEntiy.Instance[Module].MachineIO.VaccumPO[(int)nz].SetIO(false);
                }
            }
        }

        private void frm_Camera_FormClosed(object sender, FormClosedEventArgs e)
        {
        }


        #region[光源]****************************************************************
        private void UpdateLightPlan()
        {
            bool bU = cB_U.Checked;
            bool bRedD = cB_RD.Checked;
            bool bGreenD = cB_GD.Checked;
            bool bBlueD = cB_BD.Checked;
            int dU = (tB_U.Value);
            int dRD = (tB_RD.Value);
            int dGD = (tB_GD.Value);
            int dBD = (tB_BD.Value);
            var upLight = new Tuple<bool, bool, bool, int, int, int>(bU, false, false, dU, 0, 0);
            var downLight = new Tuple<bool, bool, bool, int, int, int>(bRedD, bGreenD, bBlueD, dRD, dGD, dBD);

            CameraDefine.Instance.Light(this.Module, Camera.Top, new LightParam { bRed = bU, R_Value = dU });
            CameraDefine.Instance.Light(this.Module, Camera.Bottom, new LightParam {
                bRed = bRedD,
                bGreen = bGreenD,
                bBlue = bBlueD,
                R_Value = dRD,
                G_Value = dGD,
                B_Value = dBD,
            });
        }

        private void cB_GD_Click(object sender, EventArgs e)
        {
            UpdateLightPlan();
        }


        private void tB_U_Scroll(object sender, EventArgs e)
        {
            this.tUpRedLight.Text = this.tB_U.Value.ToString();
            this.tDownRed.Text = this.tB_RD.Value.ToString();
            this.tDownGreen.Text =  this.tB_GD.Value.ToString();
            this.tDownBlue.Text = this.tB_BD.Value.ToString();
            this.UpdateLightPlan();
        }
        #endregion

    }
}
