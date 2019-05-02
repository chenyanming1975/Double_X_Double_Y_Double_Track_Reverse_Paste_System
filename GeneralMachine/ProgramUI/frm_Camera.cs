
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Motion;
using NationalInstruments.Vision.Acquisition.Imaqdx;
using NationalInstruments.Vision;
using NationalInstruments.Vision.Analysis;
using GeneralMachine.Definition;
using NationalInstruments.Vision.WindowsForms;
using System.Threading;
using GeneralMachine.Flow;
using GeneralMachine.Config;
using GeneralMachine.Vision;
using GeneralMachine.Cliab;
using GeneralMachine.Flow.Tool;
using GeneralMachine.Flow.Editer;
using GeneralMachine.Track;

namespace GeneralMachine
{

    public enum MoveFunc
    {
        Jog,// 持续
        MoveTrim,// 点动
    }
    public partial class frm_Camera : UserControl
    {
        public frm_Camera()
        {
            InitializeComponent();

            cameraComboBox.SelectedIndex = 0;
            cB_NozzleIndex.SelectedIndex = 0;
            this.cB_Keyboard.Checked = false;
            this.moduleRadio1.ModuleChange += (sender, module) =>
            {
                if (bSet_CamLive)
                {
                    this.bCamLive_Click(this, new EventArgs());
                }

                this.Module = module;
            };
            for(FlowInOutMode flow = FlowInOutMode.左进右出; flow <= FlowInOutMode.右进右出;++flow)
            {
                this.cbFlowDir.Items.Add(Common.CommonHelper.GetEnumDescription(typeof(FlowInOutMode), flow));
            }

            this.cbFlowDir.SelectedIndex = 2;
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

        #region 实时拍照
        /// <summary>
        /// true: 正在实时拍照 false:未拍照
        /// </summary>
        private bool bSet_CamLive;

        private void imageSet_ImageMouseDown(object sender, ImageMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (cB_MouseTrim.Checked)
                {
                    PointF pt = SystemEntiy.Instance[Module].XYPos;
                    PointF world = new PointF();
                    if(SystemEntiy.Instance[this.Module].WroldPt(this.Camera, pt,
                        e.Point,out world))
                    {
                        SystemEntiy.Instance[this.Module].XYGoPos(world);
                    }
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
            if (CameraDefine.Instance.Camera[this.Module][Camera].ShowCross)
                bShowCross.Text = "显示十字";
            else
                bShowCross.Text = "关闭十字";

            CameraDefine.Instance.Camera[this.Module][Camera].ShowCross = !CameraDefine.Instance.Camera[this.Module][Camera].ShowCross;
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

        #region 持续移动
        private void p_YP_MouseDown_1(object sender, MouseEventArgs e)
        {
            this.p_YP.Image = GeneralMachine.Properties.Resources.Up2;
            SystemEntiy.Instance[Module].YJog(true, this.ManualSpeed);
        }

        private void p_XP_MouseDown_1(object sender, MouseEventArgs e)
        {
            this.p_XP.Image = GeneralMachine.Properties.Resources.Right2;
            SystemEntiy.Instance[Module].XJog(true, this.ManualSpeed);
        }

        private void p_XN_MouseDown_1(object sender, MouseEventArgs e)
        {
            this.p_XN.Image = GeneralMachine.Properties.Resources.Left2;
            SystemEntiy.Instance[Module].XJog(false, this.ManualSpeed);
        }

        private void p_YN_MouseDown_1(object sender, MouseEventArgs e)
        {
            this.p_YN.Image = GeneralMachine.Properties.Resources.Down2;
            SystemEntiy.Instance[Module].YJog(false, this.ManualSpeed);
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

        public Shceme ManualSpeed = Shceme.ManualNormal;

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
            if (!SystemEntiy.Instance.GetTrack(Module).TrackIO.IO_StopOrg.GetIO())
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

        private void bTurnN_MouseMove(object sender, MouseEventArgs e)
        {
            this.bTurnN.Image = GeneralMachine.Properties.Resources.RN2;
            SystemEntiy.Instance[Module].TurnJog(false, Shceme.ManualNormal);
        }

        private void bTurnP_MouseDown(object sender, MouseEventArgs e)
        {
            this.bTurnP.Image = GeneralMachine.Properties.Resources.RS2;
            SystemEntiy.Instance[Module].TurnJog(true);
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

        #region 轴移动和控制
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
            p_ZN.Image = GeneralMachine.Properties.Resources.Up2;
            SystemEntiy.Instance[Module].ZJog(NzIndex, NzIndex == Nozzle.Nz2 || NzIndex == Nozzle.Nz3, this.ManualSpeed);
        }

        private void p_ZP_MouseDown(object sender, MouseEventArgs e)
        {
            p_ZP.Image = GeneralMachine.Properties.Resources.Down2;
            SystemEntiy.Instance[Module].ZJog(NzIndex, NzIndex == Nozzle.Nz1 || NzIndex == Nozzle.Nz4, this.ManualSpeed);
        }

        private void p_ZP_MouseUp(object sender, MouseEventArgs e)
        {
            p_ZN.Image = GeneralMachine.Properties.Resources.Up;
            p_ZP.Image = GeneralMachine.Properties.Resources.Down;
            SystemEntiy.Instance[Module].MachineAxis.Z[(int)NzIndex].Stop();
        }

        private void pUN_MouseDown(object sender, MouseEventArgs e)
        {
            pUN.Image = GeneralMachine.Properties.Resources.RN2;
            SystemEntiy.Instance[Module].RJog(NzIndex, false, this.ManualSpeed);
        }

        private void pUP_MouseDown(object sender, MouseEventArgs e)
        {
            pUP.Image = GeneralMachine.Properties.Resources.RS2;
            SystemEntiy.Instance[Module].RJog(NzIndex, true, this.ManualSpeed);
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
            if (!temp.GetIO())
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
            if (!temp.GetIO())
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
            if (!temp.GetIO())
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
            if (!temp.GetIO())
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
        #endregion

        private void frm_Camera_FormClosed(object sender, FormClosedEventArgs e)
        {
            fm_SoftwareCliab.LearnTemp -= this.LearnTemp;
            fm_SoftwareCliab.FindTemp -= this.FindTemp;
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
            CameraDefine.Instance.Light(this.Module, Camera.Bottom1, new LightParam {
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

        private void bGoThrowHeight_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[Module].ZGoSuck(NzIndex);
        }

        #region 视觉
        public DetectPttern pttern = new DetectPttern() { MinAngle = 0, MaxAngle = 360 };

        public void  LearnTemp()
        {
            try
            {
                if (!pttern.Lerarn(this.imageSet.imageSet.Image, this.imageSet.imageSet.Roi))
                {
                    MessageBox.Show("学习失败!!");
                }
                else
                {
                    MessageBox.Show("学习成功!!");
                }
            }
            catch { }
        }

        public VisionResult FindTemp(Module module, Camera camera)
        {
            if(this.bSet_CamLive)
            {
                MessageBox.Show("请关闭实时!!");
                return new VisionResult();
            }

            
            if(this.imageSet.imageSet.Roi.Count > 0)
            {
                Thread.Sleep(50);
                var image = CameraDefine.Instance.Camera[module][camera].Snap();
                Algorithms.Copy(image, this.imageSet.imageSet.Image);
                image?.Dispose();
                pttern.ROI = this.imageSet.imageSet.Roi[0].Shape;
                return pttern.Detected(this.imageSet.imageSet.Image);
            }
            else
            {
                MessageBox.Show("请画去 侦测框!!!");
                return new VisionResult();
            }
        }
        #endregion
        private void bClose_Click(object sender, EventArgs e)
        {
            if (bSet_CamLive)
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

            this.CloseEvent?.Invoke(this, new EventArgs());
        }

        public event EventHandler CloseEvent;

        private void frm_Camera_Load(object sender, EventArgs e)
        {
            fm_SoftwareCliab.LearnTemp += this.LearnTemp;
            fm_SoftwareCliab.FindTemp += this.FindTemp;
            VisionToolCtrl.GetImage += GetImage;
            VisionToolCtrl.GetLightAndExp += VisionToolCtrl_GetLightAndExp;
            VisionToolCtrl.SetLightAndExp += VisionToolCtrl_SetLightAndExp;
            ProgramEditCtrl.DetectUI += ProgramEditCtrl_DetectUI;
            ProgramEditCtrl.GetROI += Fm_SoftwareCliab_GetRoi;
            fm_SoftwareCliab.GetRoi += Fm_SoftwareCliab_GetRoi;
            fm_SoftwareCliab.DetectLabel += Fm_SoftwareCliab_DetectLabel;
            fm_SoftwareCliab.ShowROI += Fm_SoftwareCliab_ShowROI;
            fm_SoftwareCliab.GetImage += GetImage;
            this.imageSet.VisionDetect += ImageSet_VisionDetect;
            AxisOffsetItem.GetRoi += Fm_SoftwareCliab_GetRoi;
            AxisOffsetItem.VisionDetect += AxisOffsetItem_VisionDetect;
            this.cbSpeedMode.SelectedIndex = 1;
            this.imageSet.imageSet.ZoomToFit = true;
        }

        private void AxisOffsetItem_VisionDetect(Module arg1, string arg2)
        {
            if(arg1 == this.moduleRadio1.Module)
            {
                this.ImageSet_VisionDetect(arg2);
            }
        }

        private void ImageSet_VisionDetect(string func)
        {
            if (this.imageSet.imageSet.Roi.Count > 0)
            {
                Camera camera = (Camera)this.cameraComboBox.SelectedIndex;
                VisionResult rtn = new VisionResult();
                if (this.bSet_CamLive)
                {
                    this.bCamLive_Click(this, new EventArgs());
                    Thread.Sleep(100);
                }

                using (VisionImage img = CameraDefine.Instance.Camera[this.Module][camera].Snap())
                {
                    rtn = VisionCalHelper.Instance.DetectUI(func, img, this.imageSet.imageSet.Roi[0].Shape);
                    Algorithms.Copy(img, this.imageSet.imageSet.Image);
                    if(rtn.State == VisionResultState.OK)
                    {
                        var xyPt = SystemEntiy.Instance[this.Module].XYPos;
                        PointF wroldPt = new PointF();
                        if(SystemEntiy.Instance[this.Module].WroldPt(camera, xyPt, rtn.Point, out wroldPt))
                        {
                            SystemEntiy.Instance[this.Module].XYGoPosTillStop(wroldPt);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请绘制ROI");
            }
        }

        private VisionResult ProgramEditCtrl_DetectUI(string arg1, RectangleContour arg2, Module arg3)
        {
            VisionResult rtn = new VisionResult();
            if (this.bSet_CamLive)
            {
                this.bCamLive_Click(this, new EventArgs());
                Thread.Sleep(100);
            }

            using (VisionImage img = CameraDefine.Instance.Camera[arg3][Camera.Top].Snap())
            {
                rtn = VisionCalHelper.Instance.DetectUI(arg1, img, arg2);
                Algorithms.Copy(img, this.imageSet.imageSet.Image);
            }

            return rtn;
        }

        private void Fm_SoftwareCliab_ShowROI(object sender, RectangleContour e)
        {
            this.imageSet.imageSet.Roi.Clear();
            this.imageSet.imageSet.Roi.Add(e);
        }

        private Roi Fm_SoftwareCliab_GetRoi()
        {
            return this.imageSet.imageSet.Roi;
        }

        private VisionResult Fm_SoftwareCliab_DetectLabel(string arg1, Module arg2, Nozzle arg3)
        {
            VisionResult rtn = new VisionResult();
            if(this.bSet_CamLive)
            {
                this.bCamLive_Click(this, new EventArgs());
                Thread.Sleep(100);
            }

            Camera camera = Camera.Bottom1;
            if (arg3 == Nozzle.Nz3 || arg3 == Nozzle.Nz4)
                camera = Camera.Bottom2;

            var roi = SystemEntiy.Instance[arg2].MachineConfig[arg3].ViewRoi;

            using (VisionImage img = CameraDefine.Instance.Camera[arg2][camera].Snap())
            {
                rtn = VisionCalHelper.Instance.DetectUI(arg1, img, roi);
                Algorithms.Copy(img, this.imageSet.imageSet.Image);
            }

            return rtn;
        }

        private void VisionToolCtrl_SetLightAndExp(object sender, Tuple<bool, bool, bool, int, int, int, int> e)
        {
            CameraDefine.Instance.Camera[this.Module][this.Camera].Exposure = e.Item7;

            if (this.Camera == Camera.Top)
            {
                cB_U.Checked = e.Item1;
                CameraDefine.Instance.Light(this.Module, this.Camera, new LightParam { bRed = e.Item1, R_Value = e.Item4 });
            }
            else
            {
                CameraDefine.Instance.Light(this.Module, Camera.Bottom1, new LightParam
                {
                    bRed = e.Item1,
                    bGreen = e.Item2,
                    bBlue = e.Item3,
                    R_Value = e.Item4,
                    G_Value = e.Item5,
                    B_Value = e.Item6,
                });
            }
        }

        private Tuple<bool, bool, bool, int, int, int, int> VisionToolCtrl_GetLightAndExp()
        {
            if (this.cameraComboBox.SelectedIndex <= 0)
            {
                return new Tuple<bool, bool, bool, int, int, int, int>
                                 (cB_U.Checked
                                 , false
                                 , false
                                 , tB_U.Value
                                 , 0
                                 , 0
                                 , (int)ntCamShutter.Value);
            }
            else
            {
                return new Tuple<bool, bool, bool, int, int, int, int>
                           (cB_RD.Checked
                           , cB_GD.Checked
                           , cB_BD.Checked
                           , tB_RD.Value
                           , tB_GD.Value
                           , tB_BD.Value
                           , (int)ntCamShutter.Value);
            }
        }

        private VisionImage GetImage()
        {
            return this.imageSet.imageSet.Image;
        }

        private void bGoCamPos_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[this.Module].XYGoPos(SystemEntiy.Instance[this.Module].MachineConfig[Nozzle.Nz1].RotateCamPoint);
        }

        private void bInput_Click(object sender, EventArgs e)
        {
            TrackManager.Instance.TrackEntiy[(Config.Track)Module].ManualInput((FlowInOutMode)this.cbFlowDir.SelectedIndex);
        }

        private void bOutput_Click(object sender, EventArgs e)
        {
            TrackManager.Instance.TrackEntiy[(Config.Track)Module].ManualOutput((FlowInOutMode)this.cbFlowDir.SelectedIndex);
        }

        private void lTrunNeg_Click(object sender, EventArgs e)
        {
        }

        private void lTurnPos_Click(object sender, EventArgs e)
        {

        }

        private void cbSpeedMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ManualSpeed = (Shceme)this.cbSpeedMode.SelectedIndex;
        }

        private void cB_Keyboard_CheckedChanged(object sender, EventArgs e)
        {
            this.numStep.Enabled = this.cB_Keyboard.Checked;
        }

        public void KeyDown(ref Message msg, Keys keyData)
        {
            if (!this.cB_Keyboard.Checked) return;

            PointF curPos = SystemEntiy.Instance[this.Module].XYPos;
            switch (keyData)
            {
                case Keys.W:
                    curPos.Y += (float)this.numStep.Value;
                    SystemEntiy.Instance[this.Module].XYGoPos(curPos);
                    //SystemEntiy.Instance[this.Module].YGoPos((SystemEntiy.Instance[this.Module].MachineAxis.Y.Pos + (double)this.numStep.Value), this.ManualSpeed);
                    break;
                case Keys.S:
                    curPos.Y -= (float)this.numStep.Value;
                    //SystemEntiy.Instance[this.Module].YGoPos((SystemEntiy.Instance[this.Module].MachineAxis.Y.Pos - (double)this.numStep.Value), this.ManualSpeed);
                    SystemEntiy.Instance[this.Module].XYGoPos(curPos);
                    break;
                case Keys.R:
                    int dir = -1;
                    if (this.NzIndex == Nozzle.Nz2 || this.NzIndex == Nozzle.Nz3)
                        dir = 1;

                    SystemEntiy.Instance[this.Module].ZGoPos(
                        this.NzIndex,
                        (SystemEntiy.Instance[this.Module].MachineAxis.Z[(int)this.NzIndex].Pos + (dir)*(double)this.numStep.Value)
                        , this.ManualSpeed);
                    break;
                case Keys.F:
                    int dirr = 1;
                    if (this.NzIndex == Nozzle.Nz2 || this.NzIndex == Nozzle.Nz3)
                        dirr = -1;

                    SystemEntiy.Instance[this.Module].ZGoPos(
                        this.NzIndex,
                        (SystemEntiy.Instance[this.Module].MachineAxis.Z[(int)this.NzIndex].Pos + (dirr) * (double)this.numStep.Value)
                        , this.ManualSpeed); break;
                case Keys.A:
                    curPos.X -= (float)this.numStep.Value;
                    SystemEntiy.Instance[this.Module].XYGoPos(curPos);
                    //SystemEntiy.Instance[this.Module].XGoPos((SystemEntiy.Instance[this.Module].MachineAxis.X.Pos - (double)this.numStep.Value), this.ManualSpeed);
                    break;
                case Keys.D:
                    curPos.X += (float)this.numStep.Value;
                    SystemEntiy.Instance[this.Module].XYGoPos(curPos);
                    //SystemEntiy.Instance[this.Module].XGoPos((SystemEntiy.Instance[this.Module].MachineAxis.X.Pos + (double)this.numStep.Value), this.ManualSpeed);
                    break;
            }
        }

        private void frm_Camera_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                if (bSet_CamLive)
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
        }

        private void bCur_Click(object sender, EventArgs e)
        {
            var pt = SystemEntiy.Instance[this.Module].XYPos;
            this.tX.Text = pt.X.ToString("f3");
            this.tY.Text = pt.Y.ToString("f3");
        }

        private void bAutoHeight_Click(object sender, EventArgs e)
        {
            AutoHeightCtrl fm = new AutoHeightCtrl(this.Module, (Nozzle)this.cB_NozzleIndex.SelectedIndex);
            fm.ShowDialog();
        }
    }
}
