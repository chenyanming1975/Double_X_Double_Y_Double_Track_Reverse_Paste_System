using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Definition;
using GeneralMachine.MotionControl;

namespace GeneralMachine
{
    public partial class frm_Wizard_Program : Form
    {
        public frm_Main frm_Main = null;
        public frm_Wizard_Program()
        {
            InitializeComponent();
        }
        public frm_Wizard_Program(Object obj)
        {
            InitializeComponent();
            frm_Main = (frm_Main)obj;
        }

        private void frm_Wizard_Program_Load(object sender, EventArgs e)
        {
            Common.CommonHelper.InitDGV(dGV_FeederNO, 10);
            Common.CommonHelper.InitDGV(dGV_Feeder, 12);
            Common.CommonHelper.InitDGV(dGV_CamDownPoint, 12);
            #region dGV_FeederNO
            dGV_FeederNO.Rows.Add(8);
            dGV_FeederNO.TopLeftHeaderCell.Value = "模块";
            dGV_FeederNO.Rows[0].HeaderCell.Value = "1";
            dGV_FeederNO.Rows[0].Cells[0].Value = "1";
            dGV_FeederNO.Rows[1].HeaderCell.Value = "1";
            dGV_FeederNO.Rows[1].Cells[0].Value = "2";
            dGV_FeederNO.Rows[2].HeaderCell.Value = "1";
            dGV_FeederNO.Rows[2].Cells[0].Value = "3";
            dGV_FeederNO.Rows[3].HeaderCell.Value = "1";
            dGV_FeederNO.Rows[3].Cells[0].Value = "4";
            dGV_FeederNO.Rows[4].HeaderCell.Value = "2";
            dGV_FeederNO.Rows[4].Cells[0].Value = "1";
            dGV_FeederNO.Rows[5].HeaderCell.Value = "2";
            dGV_FeederNO.Rows[5].Cells[0].Value = "2";
            dGV_FeederNO.Rows[6].HeaderCell.Value = "2";
            dGV_FeederNO.Rows[6].Cells[0].Value = "3";
            dGV_FeederNO.Rows[7].HeaderCell.Value = "2";
            dGV_FeederNO.Rows[7].Cells[0].Value = "4";
            #endregion
            #region dGV_CamDownPoint
            dGV_CamDownPoint.Rows.Add(6);
            dGV_CamDownPoint.Rows[0].HeaderCell.Value = "比较输出起点";
            dGV_CamDownPoint.Rows[0].Cells[0].Value = "0.000";
            dGV_CamDownPoint.Rows[0].Cells[1].Value = "0.000";
            dGV_CamDownPoint.Rows[1].HeaderCell.Value = "吸嘴1拍照点1";
            dGV_CamDownPoint.Rows[1].Cells[0].Value = "0.000";
            dGV_CamDownPoint.Rows[1].Cells[1].Value = "0.000";
            dGV_CamDownPoint.Rows[2].HeaderCell.Value = "吸嘴1拍照点2";
            dGV_CamDownPoint.Rows[2].Cells[0].Value = "0.000";
            dGV_CamDownPoint.Rows[2].Cells[1].Value = "0.000";
            dGV_CamDownPoint.Rows[3].HeaderCell.Value = "吸嘴2拍照点1";
            dGV_CamDownPoint.Rows[3].Cells[0].Value = "0.000";
            dGV_CamDownPoint.Rows[3].Cells[1].Value = "0.000";
            dGV_CamDownPoint.Rows[4].HeaderCell.Value = "吸嘴2拍照点2";
            dGV_CamDownPoint.Rows[4].Cells[0].Value = "0.000";
            dGV_CamDownPoint.Rows[4].Cells[1].Value = "0.000";
            dGV_CamDownPoint.Rows[5].HeaderCell.Value = "比较输出终点";
            dGV_CamDownPoint.Rows[5].Cells[0].Value = "0.000";
            dGV_CamDownPoint.Rows[5].Cells[1].Value = "0.000";
            #endregion
            cB_Module.SelectedIndex = 0;
            Variable.bExpand = false;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateList(true);
        }


        /// <summary>
        /// Next 按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void wizardControl1_NextButtonClick(WizardBase.WizardControl sender, WizardBase.WizardNextButtonClickEventArgs args)
        {
            if (wizardControl1.CurrentStepIndex == 0)
            {
                if(tWideValue.Text == "")
                {
                    msgDiv1.MsgDivShow("调宽未设置!", 1);
                    args.Cancel = true;
                }
            }
            if (wizardControl1.CurrentStepIndex == 1)
            {
                //args.Cancel = true;
            }
            if (wizardControl1.CurrentStepIndex == 2)
            {
                //args.Cancel = true;
            }
            if (wizardControl1.CurrentStepIndex == 3)
            {
                //args.Cancel = true;
            }
        }

        /// <summary>
        /// Finish 按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wizardControl1_FinishButtonClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Cancel 按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wizardControl1_CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("确定退出程式编辑吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            else
            {
               frm_Main.frm_Program.Close();
            }
        }

        //1-轨道调宽**************************************************
        /// <summary>
        /// 轨道宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bRecordWidth_Click(object sender, EventArgs e)
        {
            tWideValue.Text = Variable.Wide.ActAxisValue.ToString("F3");
        }
        /// <summary>
        /// 轨道宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bGoWidePos_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("请确认无产品在轨道里面", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                string str = tWideValue.Text;
                double p = 0;
                try
                {
                    p = Convert.ToDouble(str);
                }
                catch
                {
                    msgDiv1.MsgDivShow("输入数值有误!", 1);
                    return;
                }
                Motion.WideGoPos(p, Variable.VelMode_RunMode_Wide);
            }
        }

        //2-视觉库**************************************************
        /// <summary>
        /// 更新程式列表
        /// </summary>
        /// <param name="updateNow"></param>
        public void updateList(bool updateNow)
        {
            DirectoryInfo a;
            DirectoryInfo[] info = null;
            if (Variable.AlmProgram.ALMProName == null || Variable.AlmProgram.ALMProName == "" || !Directory.Exists(Variable.sPath_SYS_Program + Variable.AlmProgram.ALMProName))
            {
                return;
            }
            if (cB_VisionType.SelectedIndex == 0)
            {
                a = new DirectoryInfo(Variable.sPath_SYS_Program + Variable.AlmProgram.ALMProName + "\\Vision_UpLabel\\");
                info = a.GetDirectories();
            }
            if (cB_VisionType.SelectedIndex == 1)
            {
                a = new DirectoryInfo(Variable.sPath_SYS_Program + Variable.AlmProgram.ALMProName + "\\Vision_DownLabel\\");
                info = a.GetDirectories();
            }
            if (cB_VisionType.SelectedIndex == 2)
            {
                a = new DirectoryInfo(Variable.sPath_SYS_Program + Variable.AlmProgram.ALMProName + "\\Vision_UpPaste\\");
                info = a.GetDirectories();
            }
            if (updateNow)
            {
                list_LibVision.Items.Clear();
                for (int i = 0; i < info.Length; i++)
                {
                    list_LibVision.Items.Add(info[i].Name);
                }
            }
            else
            {
                if (list_LibVision.Items.Count != info.Length)
                {
                    list_LibVision.Items.Clear();
                    for (int i = 0; i < info.Length; i++)
                    {
                        list_LibVision.Items.Add(info[i].Name);
                    }
                }
            }

        }

        private void bUpdateList_Click(object sender, EventArgs e)
        {
            updateList(true);
        }

        private void ShowVisionProgram()
        {
            frm_Main.frm_Wizard_Vision = new frm_Wizard_Vision(frm_Main);
            frm_Main.frm_Wizard_Vision.TopLevel = false;
            frm_Main.frm_Wizard_Vision.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm_Main.frm_Program.pProgram.Controls.Clear();//移除所有控件
            frm_Main.frm_Program.pProgram.Controls.Add(frm_Main.frm_Wizard_Vision);
            frm_Main.frm_Wizard_Vision.Dock = DockStyle.Fill;
            frm_Main.frm_Wizard_Vision.Show();
        }

        private void bNewVision_Click(object sender, EventArgs e)
        {
            ShowVisionProgram();
            frm_Main.frm_Wizard_Vision.tProgramName.Enabled = true;
        }

        private void ShowVisionUI(string proname)
        {
            //welcom
            frm_Main.frm_Wizard_Vision.cB_VisionType.SelectedIndex = cB_VisionType.SelectedIndex;
            frm_Main.frm_Wizard_Vision.tProgramName.Enabled = false;
            frm_Main.frm_Wizard_Vision.tProgramName.Text = proname;
            //Vision type
            frm_Main.frm_Wizard_Vision.cB_FOV.SelectedIndex = Variable.AlmVisionInfo.Type_FOVCount - 1;
            frm_Main.frm_Wizard_Vision.cB_AlignStyle.SelectedIndex = Variable.AlmVisionInfo.Type_Align - 1;
            if (cB_VisionType.SelectedIndex == 2)
            {
                frm_Main.frm_Wizard_Vision.tX1.Text = Variable.AlmBaseInfo.BaseXY_Align1.X.ToString("F3");
                frm_Main.frm_Wizard_Vision.tY1.Text = Variable.AlmBaseInfo.BaseXY_Align1.Y.ToString("F3");
                frm_Main.frm_Wizard_Vision.tX2.Text = Variable.AlmBaseInfo.BaseXY_Align2.X.ToString("F3");
                frm_Main.frm_Wizard_Vision.tY2.Text = Variable.AlmBaseInfo.BaseXY_Align2.Y.ToString("F3");
                frm_Main.frm_Wizard_Vision.tBaseAngle.Text = Variable.AlmBaseInfo.Base_Angle.ToString("F3");
            }
            else
            {
                frm_Main.frm_Wizard_Vision.pBase.Visible = false;
                frm_Main.frm_Wizard_Vision.gB_Paste.Visible = false;
            }
            #region T1
            frm_Main.frm_Wizard_Vision.tCamPointX1.Text = Variable.AlmVisionInfo.T1_CamPoint.X.ToString();
            frm_Main.frm_Wizard_Vision.tCamPointY1.Text = Variable.AlmVisionInfo.T1_CamPoint.Y.ToString();
            frm_Main.frm_Wizard_Vision.tShutter1.Text = Variable.AlmVisionInfo.T1_Shutter.ToString();
            frm_Main.frm_Wizard_Vision.cB_Light_R1.Checked = Variable.AlmVisionInfo.T1_Light_RedUse;
            frm_Main.frm_Wizard_Vision.cB_Light_G1.Checked = Variable.AlmVisionInfo.T1_Light_GreenUse;
            frm_Main.frm_Wizard_Vision.cB_Light_B1.Checked = Variable.AlmVisionInfo.T1_Light_BlueUse;
            frm_Main.frm_Wizard_Vision.tLight_R1.Text = Variable.AlmVisionInfo.T1_Light_RedValue.ToString();
            frm_Main.frm_Wizard_Vision.tLight_G1.Text = Variable.AlmVisionInfo.T1_Light_GreenValue.ToString();
            frm_Main.frm_Wizard_Vision.tLight_B1.Text = Variable.AlmVisionInfo.T1_Light_BlueValue.ToString();
            frm_Main.frm_Wizard_Vision.tGain1.Text = Variable.AlmVisionInfo.T1_PreProcess_Gain.ToString();
            frm_Main.frm_Wizard_Vision.tOffset1.Text = Variable.AlmVisionInfo.T1_PreProcess_Offset.ToString();
            frm_Main.frm_Wizard_Vision.cB_InitAlign1.Checked = Variable.AlmVisionInfo.T1_InitROIEN;
            frm_Main.frm_Wizard_Vision.tInitScore1.Text = Variable.AlmVisionInfo.T1_InitScore.ToString("F1");
            frm_Main.frm_Wizard_Vision.tInitMinAngle1.Text = Variable.AlmVisionInfo.T1_InitMinDegree.ToString();
            frm_Main.frm_Wizard_Vision.tInitMaxAngle1.Text = Variable.AlmVisionInfo.T1_InitMaxDegree.ToString();
            frm_Main.frm_Wizard_Vision.cB_Area1.Checked = Variable.AlmVisionInfo.T1_InitAreaEN;
            frm_Main.frm_Wizard_Vision.rB_White1.Checked = Variable.AlmVisionInfo.T1_InitAreaIsWhite;
            frm_Main.frm_Wizard_Vision.rB_Black1.Checked = !Variable.AlmVisionInfo.T1_InitAreaIsWhite;
            frm_Main.frm_Wizard_Vision.tMinAreaValue1.Text = Variable.AlmVisionInfo.T1_InitMinArea.ToString();
            frm_Main.frm_Wizard_Vision.tMaxAreaValue1.Text = Variable.AlmVisionInfo.T1_InitMaxArea.ToString();
            if (Variable.AlmVisionInfo.T1_AlignIndex == 0)
            {
                frm_Main.frm_Wizard_Vision.cB_Algthrim1.Text = "点-找圆";
            }
            if (Variable.AlmVisionInfo.T1_AlignIndex == 1)
            {
                frm_Main.frm_Wizard_Vision.cB_Algthrim1.Text = "点-匹配";
            }
            if (Variable.AlmVisionInfo.T1_AlignIndex == 2)
            {
                frm_Main.frm_Wizard_Vision.cB_Algthrim1.Text = "点-边边相交";
            }
            if (Variable.AlmVisionInfo.T1_AlignIndex == 3)
            {
                frm_Main.frm_Wizard_Vision.cB_Algthrim1.Text = "边";
            }
            frm_Main.frm_Wizard_Vision.tMinR1.Text = Variable.AlmVisionInfo.T1_CircleMinR.ToString();
            frm_Main.frm_Wizard_Vision.tMaxR1.Text = Variable.AlmVisionInfo.T1_CircleMaxR.ToString();
            frm_Main.frm_Wizard_Vision.tScore1.Text = Variable.AlmVisionInfo.T1_PatternScore.ToString();
            frm_Main.frm_Wizard_Vision.tMinAngle1.Text = Variable.AlmVisionInfo.T1_PatternMinAngle.ToString();
            frm_Main.frm_Wizard_Vision.tMaxAngle1.Text = Variable.AlmVisionInfo.T1_PatternMaxAngle.ToString();
            frm_Main.frm_Wizard_Vision.cB_PicDirCrossA1.SelectedIndex = (int)Variable.AlmVisionInfo.T1_Crosspicdir1;
            frm_Main.frm_Wizard_Vision.cB_GrayDirCrossA1.SelectedIndex = (int)Variable.AlmVisionInfo.T1_Crossgraydir1;
            frm_Main.frm_Wizard_Vision.cB_PicDirCrossB1.SelectedIndex = (int)Variable.AlmVisionInfo.T1_Crosspicdir2;
            frm_Main.frm_Wizard_Vision.cB_GrayDirCrossB1.SelectedIndex = (int)Variable.AlmVisionInfo.T1_Crossgraydir2;
            frm_Main.frm_Wizard_Vision.cB_PicDir1.SelectedIndex = (int)Variable.AlmVisionInfo.T1_Linepicdir;
            frm_Main.frm_Wizard_Vision.cB_GrayDir1.SelectedIndex = (int)Variable.AlmVisionInfo.T1_Linegraydir;
            #endregion
            #region T2
            frm_Main.frm_Wizard_Vision.tCamPointX2.Text = Variable.AlmVisionInfo.T2_CamPoint.X.ToString();
            frm_Main.frm_Wizard_Vision.tCamPointY2.Text = Variable.AlmVisionInfo.T2_CamPoint.Y.ToString();
            frm_Main.frm_Wizard_Vision.tShutter2.Text = Variable.AlmVisionInfo.T2_Shutter.ToString();
            frm_Main.frm_Wizard_Vision.cB_Light_R2.Checked = Variable.AlmVisionInfo.T2_Light_RedUse;
            frm_Main.frm_Wizard_Vision.cB_Light_G2.Checked = Variable.AlmVisionInfo.T2_Light_GreenUse;
            frm_Main.frm_Wizard_Vision.cB_Light_B2.Checked = Variable.AlmVisionInfo.T2_Light_BlueUse;
            frm_Main.frm_Wizard_Vision.tLight_R2.Text = Variable.AlmVisionInfo.T2_Light_RedValue.ToString();
            frm_Main.frm_Wizard_Vision.tLight_G2.Text = Variable.AlmVisionInfo.T2_Light_GreenValue.ToString();
            frm_Main.frm_Wizard_Vision.tLight_B2.Text = Variable.AlmVisionInfo.T2_Light_BlueValue.ToString();
            frm_Main.frm_Wizard_Vision.tGain2.Text = Variable.AlmVisionInfo.T2_PreProcess_Gain.ToString();
            frm_Main.frm_Wizard_Vision.tOffset2.Text = Variable.AlmVisionInfo.T2_PreProcess_Offset.ToString();
            frm_Main.frm_Wizard_Vision.cB_InitAlign2.Checked = Variable.AlmVisionInfo.T2_InitROIEN;
            frm_Main.frm_Wizard_Vision.tInitScore2.Text = Variable.AlmVisionInfo.T2_InitScore.ToString("F1");
            frm_Main.frm_Wizard_Vision.tInitMinAngle2.Text = Variable.AlmVisionInfo.T2_InitMinDegree.ToString();
            frm_Main.frm_Wizard_Vision.tInitMaxAngle2.Text = Variable.AlmVisionInfo.T2_InitMaxDegree.ToString();
            frm_Main.frm_Wizard_Vision.cB_Area2.Checked = Variable.AlmVisionInfo.T2_InitAreaEN;
            frm_Main.frm_Wizard_Vision.rB_White2.Checked = Variable.AlmVisionInfo.T2_InitAreaIsWhite;
            frm_Main.frm_Wizard_Vision.rB_Black2.Checked = !Variable.AlmVisionInfo.T2_InitAreaIsWhite;
            frm_Main.frm_Wizard_Vision.tMinAreaValue2.Text = Variable.AlmVisionInfo.T2_InitMinArea.ToString();
            frm_Main.frm_Wizard_Vision.tMaxAreaValue2.Text = Variable.AlmVisionInfo.T2_InitMaxArea.ToString();
            if (Variable.AlmVisionInfo.T2_AlignIndex == 0)
            {
                frm_Main.frm_Wizard_Vision.cB_Algthrim2.Text = "点-找圆";
            }
            if (Variable.AlmVisionInfo.T2_AlignIndex == 1)
            {
                frm_Main.frm_Wizard_Vision.cB_Algthrim2.Text = "点-匹配";
            }
            if (Variable.AlmVisionInfo.T2_AlignIndex == 2)
            {
                frm_Main.frm_Wizard_Vision.cB_Algthrim2.Text = "点-边边相交";
            }
            if (Variable.AlmVisionInfo.T2_AlignIndex == 3)
            {
                frm_Main.frm_Wizard_Vision.cB_Algthrim2.Text = "边";
            }
            frm_Main.frm_Wizard_Vision.tMinR2.Text = Variable.AlmVisionInfo.T2_CircleMinR.ToString();
            frm_Main.frm_Wizard_Vision.tMaxR2.Text = Variable.AlmVisionInfo.T2_CircleMaxR.ToString();
            frm_Main.frm_Wizard_Vision.tScore2.Text = Variable.AlmVisionInfo.T2_PatternScore.ToString();
            frm_Main.frm_Wizard_Vision.tMinAngle2.Text = Variable.AlmVisionInfo.T2_PatternMinAngle.ToString();
            frm_Main.frm_Wizard_Vision.tMaxAngle2.Text = Variable.AlmVisionInfo.T2_PatternMaxAngle.ToString();
            frm_Main.frm_Wizard_Vision.cB_PicDirCrossA2.SelectedIndex = (int)Variable.AlmVisionInfo.T2_Crosspicdir1;
            frm_Main.frm_Wizard_Vision.cB_GrayDirCrossA2.SelectedIndex = (int)Variable.AlmVisionInfo.T2_Crossgraydir1;
            frm_Main.frm_Wizard_Vision.cB_PicDirCrossB2.SelectedIndex = (int)Variable.AlmVisionInfo.T2_Crosspicdir2;
            frm_Main.frm_Wizard_Vision.cB_GrayDirCrossB2.SelectedIndex = (int)Variable.AlmVisionInfo.T2_Crossgraydir2;
            frm_Main.frm_Wizard_Vision.cB_PicDir2.SelectedIndex = (int)Variable.AlmVisionInfo.T2_Linepicdir;
            frm_Main.frm_Wizard_Vision.cB_GrayDir2.SelectedIndex = (int)Variable.AlmVisionInfo.T2_Linegraydir;

            #endregion
            //base
            Common.CommonHelper.DelDGV(frm_Main.frm_Wizard_Vision.dGV_Program);
            if (cB_VisionType.SelectedIndex == 2)
            {
                if (Variable.AlmBaseInfo.Base_EN_Paste != null && Variable.AlmBaseInfo.Base_EN_Paste.Length != 0)
                {
                    int length1 = Variable.AlmBaseInfo.Base_EN_Paste.Length;
                    frm_Main.frm_Wizard_Vision.dGV_Program.Rows.Add(length1);
                    for (int i = 0; i < length1; i++)
                    {
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[0].Value = Variable.AlmBaseInfo.Base_EN_Paste[i] == true ? "1" : "0";
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[1].Value = Variable.AlmBaseInfo.Base_Floor[i].ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[2].Value = Variable.AlmBaseInfo.Base_Feeder[i].ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[3].Value = Variable.AlmBaseInfo.Base_Nozzle[i].ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[4].Value = Variable.AlmBaseInfo.Base_PasteXY[i].X.ToString("F3");
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[5].Value = Variable.AlmBaseInfo.Base_PasteXY[i].Y.ToString("F3");
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[6].Value = Variable.AlmBaseInfo.Base_PasteR[i].ToString("F3");
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[7].Value = Variable.AlmBaseInfo.Base_PasteZ[0,i].ToString("F3");
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[8].Value = Variable.AlmBaseInfo.Base_PasteZ[1,i].ToString("F3");
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[9].Value = Variable.AlmBaseInfo.Base_Delay[i].ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[10].Value = Variable.AlmBaseInfo.Base_EN_BadMark[i] == true ? "1" : "0";
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[11].Value = Variable.AlmBaseInfo.Base_CamPoint_BadMark[i].X.ToString("F3");
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[12].Value = Variable.AlmBaseInfo.Base_CamPoint_BadMark[i].Y.ToString("F3");
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[13].Value = Variable.AlmBaseInfo.Base_ColorIsWhite_BadMark[i] == true ? "1" : "0";
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[14].Value = Variable.AlmBaseInfo.Base_ROI_BadMark[i].TopLeftX.ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[15].Value = Variable.AlmBaseInfo.Base_ROI_BadMark[i].TopLeftY.ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[16].Value = Variable.AlmBaseInfo.Base_ROI_BadMark[i].Width.ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[17].Value = Variable.AlmBaseInfo.Base_ROI_BadMark[i].Height.ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[18].Value = Variable.AlmBaseInfo.Base_MinArea_BadMark[i].ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[19].Value = Variable.AlmBaseInfo.Base_MaxArea_BadMark[i].ToString();

                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[20].Value = Variable.AlmBaseInfo.Base_Shutter_BadMark[i].ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[21].Value = Variable.AlmBaseInfo.Base_LightRedUse_BadMark[i] == true ? "1" : "0";
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[22].Value = Variable.AlmBaseInfo.Base_LightGreenUse_BadMark[i] == true ? "1" : "0";
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[23].Value = Variable.AlmBaseInfo.Base_LightBlueUse_BadMark[i] == true ? "1" : "0";
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[24].Value = Variable.AlmBaseInfo.Base_LightRedValue_BadMark[i].ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[25].Value = Variable.AlmBaseInfo.Base_LightGreenValue_BadMark[i].ToString();
                        frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[26].Value = Variable.AlmBaseInfo.Base_LightBlueValue_BadMark[i].ToString();
                    }
                }
            }
        }

        private void bEditVision_Click(object sender, EventArgs e)
        {
            if (list_LibVision.SelectedItem == null)
            {
                msgDiv1.MsgDivShow("请选择程式!", 1);
                return;
            }
            string proname = list_LibVision.SelectedItem.ToString();
            try
            {
                if (list_LibVision.Items.Count > 0 && list_LibVision.SelectedItem.ToString() != "")
                {
                    if (cB_VisionType.SelectedIndex == 2)
                    {
                        Variable.AlmBaseInfo = frm_Main.LoadBaseInfo(Variable.AlmProgram.ALMProName, proname);
                    }
                    Variable.AlmVisionInfo = frm_Main.LoadVisionInfo((short)cB_VisionType.SelectedIndex, Variable.AlmProgram.ALMProName, proname);
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("导入程式错误!", 1);
                return;
            }
            frm_Main.frm_Wizard_Vision.tProgramName.Enabled = false;
            ShowVisionUI(proname);
            ShowVisionProgram();
        }

        //3-Feeder**************************************************
        private void UpdateFeederUI(int Index)
        {
            int PointCount = 0;
            try
            {
                Common.CommonHelper.DelDGV(dGV_Feeder);
                if (Index >= 0 && Index <= 3)
                {
                    PointCount = Variable.AlmProgram.FeederInfo_Module1[Index].PointCount;
                    dGV_Feeder.Rows.Add(PointCount);
                    for (int i = 0; i < PointCount; i++)
                    {
                        dGV_Feeder.Rows[i].Cells[0].Value = Variable.AlmProgram.FeederInfo_Module1[Index].SensorID_Reach[i].ToString();
                        dGV_Feeder.Rows[i].Cells[1].Value = Variable.AlmProgram.FeederInfo_Module1[Index].VisonProName_DownLabel[i];
                        dGV_Feeder.Rows[i].Cells[2].Value = Variable.AlmProgram.FeederInfo_Module1[Index].DownCam_R[i].ToString("F3");
                        dGV_Feeder.Rows[i].Cells[3].Value = Variable.AlmProgram.FeederInfo_Module1[Index].ReachXI[i] == true?"1":"0";
                        dGV_Feeder.Rows[i].Cells[4].Value = Variable.AlmProgram.FeederInfo_Module1[Index].Suck_R[i].ToString("F3");
                        dGV_Feeder.Rows[i].Cells[5].Value = Variable.AlmProgram.FeederInfo_Module1[Index].Suck_XY[i].X.ToString("F3");
                        dGV_Feeder.Rows[i].Cells[6].Value = Variable.AlmProgram.FeederInfo_Module1[Index].Suck_XY[i].Y.ToString("F3");
                        dGV_Feeder.Rows[i].Cells[7].Value = Variable.AlmProgram.FeederInfo_Module1[Index].Suck_Z[0, i].ToString("F3");
                        dGV_Feeder.Rows[i].Cells[8].Value = Variable.AlmProgram.FeederInfo_Module1[Index].Suck_Z[1, i].ToString("F3");
                        dGV_Feeder.Rows[i].Cells[9].Value = Variable.AlmProgram.FeederInfo_Module1[Index].VisonProName_UpLabel[i];
                        dGV_Feeder.Rows[i].Cells[10].Value = Variable.AlmProgram.FeederInfo_Module1[Index].UpCam_XY[i].X.ToString("F3");
                        dGV_Feeder.Rows[i].Cells[11].Value = Variable.AlmProgram.FeederInfo_Module1[Index].UpCam_XY[i].Y.ToString("F3");
                    }
                }
                else
                {
                    PointCount = Variable.AlmProgram.FeederInfo_Module2[Index - 4].PointCount;
                    dGV_Feeder.Rows.Add(PointCount);
                    for (int i = 0; i < PointCount; i++)
                    {
                        dGV_Feeder.Rows[i].Cells[0].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].SensorID_Reach[i].ToString();
                        dGV_Feeder.Rows[i].Cells[1].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].VisonProName_DownLabel[i];
                        dGV_Feeder.Rows[i].Cells[2].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].DownCam_R[i].ToString("F3");
                        dGV_Feeder.Rows[i].Cells[3].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].ReachXI[i] == true ? "1" : "0";
                        dGV_Feeder.Rows[i].Cells[4].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_R[i].ToString("F3");
                        dGV_Feeder.Rows[i].Cells[5].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_XY[i].X.ToString("F3");
                        dGV_Feeder.Rows[i].Cells[6].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_XY[i].Y.ToString("F3");
                        dGV_Feeder.Rows[i].Cells[7].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_Z[0, i].ToString("F3");
                        dGV_Feeder.Rows[i].Cells[8].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_Z[1, i].ToString("F3");
                        dGV_Feeder.Rows[i].Cells[9].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].VisonProName_UpLabel[i];
                        dGV_Feeder.Rows[i].Cells[10].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].UpCam_XY[i].X.ToString("F3");
                        dGV_Feeder.Rows[i].Cells[11].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].UpCam_XY[i].Y.ToString("F3");
                    }
                }
            }
            catch
            {
                msgDiv1.MsgDivShow("Feeder信息更新失败!", 1);
            }
        }

        private void dGV_FeederNO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dGV_FeederNO.SelectedRows.Count > 0 && (dGV_FeederNO.SelectedRows[0].Index != dGV_FeederNO.Rows.Count - 1))
            {
                UpdateFeederUI(dGV_FeederNO.SelectedRows[0].Index);
                if (dGV_FeederNO.SelectedRows[0].Index >= 0 && dGV_FeederNO.SelectedRows[0].Index <= 3)
                {
                    cB_Module.SelectedIndex = 0;
                }
                else
                {
                    cB_Module.SelectedIndex = 1;
                }
            }
        }

        private void bSave_FeederNO_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.Rows.Count - 1))
            {
                int Index = dGV_Feeder.SelectedRows[0].Index;
                int PointCount = dGV_Feeder.Rows.Count - 1;
                if (Index >= 0 && Index <= 3)
                {
                    Variable.AlmProgram.FeederInfo_Module1[Index].PointCount = (short)PointCount;
                    Variable.AlmProgram.FeederInfo_Module1[Index].SensorID_Reach = new short[PointCount];
                    Variable.AlmProgram.FeederInfo_Module1[Index].VisonProName_DownLabel = new string[PointCount];
                    Variable.AlmProgram.FeederInfo_Module1[Index].DownCam_R = new double[PointCount];
                    Variable.AlmProgram.FeederInfo_Module1[Index].ReachXI = new bool[PointCount];
                    Variable.AlmProgram.FeederInfo_Module1[Index].Suck_R = new double[PointCount];
                    Variable.AlmProgram.FeederInfo_Module1[Index].Suck_XY = new PointF[PointCount];
                    Variable.AlmProgram.FeederInfo_Module1[Index].Suck_Z = new double[2, PointCount];
                    Variable.AlmProgram.FeederInfo_Module1[Index].VisonProName_UpLabel = new string[PointCount];
                    Variable.AlmProgram.FeederInfo_Module1[Index].UpCam_XY = new PointF[PointCount];
                    for (int i = 0; i < PointCount; i++)
                    {
                        Variable.AlmProgram.FeederInfo_Module1[Index].SensorID_Reach[i] = short.Parse(dGV_Feeder.Rows[i].Cells[0].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module1[Index].VisonProName_DownLabel[i] = dGV_Feeder.Rows[i].Cells[1].Value.ToString();
                        Variable.AlmProgram.FeederInfo_Module1[Index].DownCam_R[i] = double.Parse(dGV_Feeder.Rows[i].Cells[2].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module1[Index].ReachXI[i] = dGV_Feeder.Rows[i].Cells[3].Value.ToString() == "1" ? true : false;
                        Variable.AlmProgram.FeederInfo_Module1[Index].Suck_R[i] = double.Parse(dGV_Feeder.Rows[i].Cells[4].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module1[Index].Suck_XY[i].X = float.Parse(dGV_Feeder.Rows[i].Cells[5].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module1[Index].Suck_XY[i].Y = float.Parse(dGV_Feeder.Rows[i].Cells[6].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module1[Index].Suck_Z[0, i] = double.Parse(dGV_Feeder.Rows[i].Cells[7].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module1[Index].Suck_Z[1, i] = double.Parse(dGV_Feeder.Rows[i].Cells[8].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module1[Index].VisonProName_UpLabel[i] = dGV_Feeder.Rows[i].Cells[9].Value.ToString();
                        Variable.AlmProgram.FeederInfo_Module1[Index].UpCam_XY[i].X = float.Parse(dGV_Feeder.Rows[i].Cells[10].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module1[Index].UpCam_XY[i].Y = float.Parse(dGV_Feeder.Rows[i].Cells[11].Value.ToString());
                    }
                }
                else
                {
                    PointCount = Variable.AlmProgram.FeederInfo_Module2[Index - 4].PointCount;
                    dGV_Feeder.Rows.Add(PointCount);
                    for (int i = 0; i < PointCount; i++)
                    {
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].SensorID_Reach[i] = short.Parse(dGV_Feeder.Rows[i].Cells[0].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].VisonProName_DownLabel[i] = dGV_Feeder.Rows[i].Cells[1].Value.ToString();
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].DownCam_R[i] = double.Parse(dGV_Feeder.Rows[i].Cells[2].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].ReachXI[i] = dGV_Feeder.Rows[i].Cells[3].Value.ToString() == "1" ? true : false;
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_R[i] = double.Parse(dGV_Feeder.Rows[i].Cells[4].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_XY[i].X = float.Parse(dGV_Feeder.Rows[i].Cells[5].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_XY[i].Y = float.Parse(dGV_Feeder.Rows[i].Cells[6].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_Z[0, i] = double.Parse(dGV_Feeder.Rows[i].Cells[7].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_Z[1, i] = double.Parse(dGV_Feeder.Rows[i].Cells[8].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].VisonProName_UpLabel[i] = dGV_Feeder.Rows[i].Cells[9].Value.ToString();
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].UpCam_XY[i].X = float.Parse(dGV_Feeder.Rows[i].Cells[10].Value.ToString());
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4].UpCam_XY[i].Y = float.Parse(dGV_Feeder.Rows[i].Cells[11].Value.ToString());
                    }
                }
            }
        }

        private void bAdd_Feeder_Click(object sender, EventArgs e)
        {
            //获取当前选定的Index
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.Rows.Count - 1))
            {
                dGV_Feeder.Rows.Insert(dGV_Feeder.SelectedRows[0].Index + 1, 1);
                Common.CommonHelper.AddRowHeader(dGV_Feeder);
                DataGridViewRow item = dGV_Feeder.Rows[dGV_Feeder.SelectedRows[0].Index + 1];
                item.Cells[0].Value = "0";
                item.Cells[1].Value = "-1";
                item.Cells[2].Value = "0";
                item.Cells[3].Value = "0";
                item.Cells[4].Value = "0";
                item.Cells[5].Value = "0";
                item.Cells[6].Value = "0";
                item.Cells[7].Value = "0";
                item.Cells[8].Value = "0";
                item.Cells[9].Value = "-1";
                item.Cells[10].Value = "0";
                item.Cells[11].Value = "0";
            }
            else
            {
                dGV_Feeder.Rows.Insert(0, 1);
                Common.CommonHelper.AddRowHeader(dGV_Feeder);
                DataGridViewRow item = dGV_Feeder.Rows[0];
                item.Cells[0].Value = "0";
                item.Cells[1].Value = "-1";
                item.Cells[2].Value = "0";
                item.Cells[3].Value = "0";
                item.Cells[4].Value = "0";
                item.Cells[5].Value = "0";
                item.Cells[6].Value = "0";
                item.Cells[7].Value = "0";
                item.Cells[8].Value = "0";
                item.Cells[9].Value = "-1";
                item.Cells[10].Value = "0";
                item.Cells[11].Value = "0";
            }
        }

        private void bDel_Feeder_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0)
            {
                //获取当前选中的行（单行或者多行）
                DialogResult RSS;
                RSS = MessageBox.Show(this, "确定要删除选中行数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                switch (RSS)
                {
                    case DialogResult.Yes:
                        //获取当前选定的Index
                        for (int i = this.dGV_Feeder.SelectedRows.Count; i > 0; i--)
                        {
                            if (dGV_Feeder.SelectedRows[i - 1].Index == (dGV_Feeder.RowCount - 1))
                            {
                                msgDiv1.MsgDivShow("末尾行无数据不可删除!", 1);
                                break;
                            }
                            int Index = dGV_Feeder.SelectedRows[i - 1].Index;
                            dGV_Feeder.Rows.RemoveAt(Index);
                        }
                        Common.CommonHelper.AddRowHeader(dGV_Feeder);
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }

        private void bUp_Feeder_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                int index = dGV_Feeder.SelectedRows[0].Index;
                if (index == 0)
                {
                    return;
                }
                List<string> list = new List<string>();
                for (int i = 0; i < dGV_Feeder.Columns.Count; i++)
                {
                    list.Add(dGV_Feeder.SelectedRows[0].Cells[i].Value.ToString());   //把当前选中行的数据存入list数组中  
                }
                for (int j = 0; j < dGV_Feeder.Columns.Count; j++)
                {
                    dGV_Feeder.Rows[index].Cells[j].Value = dGV_Feeder.Rows[index - 1].Cells[j].Value;
                    dGV_Feeder.Rows[index - 1].Cells[j].Value = list[j].ToString();
                }
                dGV_Feeder.Rows[index].Selected = false;
                dGV_Feeder.Rows[index - 1].Selected = true;
            }
        }

        private void bDown_Feeder_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                int index = dGV_Feeder.SelectedRows[0].Index;
                if (index == dGV_Feeder.RowCount - 2)
                {
                    return;
                }
                List<string> list = new List<string>();
                for (int i = 0; i < dGV_Feeder.Columns.Count; i++)
                {
                    list.Add(dGV_Feeder.SelectedRows[0].Cells[i].Value.ToString());   //把当前选中行的数据存入list数组中  
                }

                for (int j = 0; j < dGV_Feeder.Columns.Count; j++)
                {
                    dGV_Feeder.Rows[index].Cells[j].Value = dGV_Feeder.Rows[index + 1].Cells[j].Value;
                    dGV_Feeder.Rows[index + 1].Cells[j].Value = list[j].ToString();
                }
                dGV_Feeder.Rows[index + 1].Selected = true;
                dGV_Feeder.Rows[index].Selected = false;
            }
        }

        private void bDownVisionID_Click(object sender, EventArgs e)
        {
            if (cB_DownVisionID.Text == "")
            {
                msgDiv1.MsgDivShow("出标下视觉定位设置错误!", 1);
                return;
            }
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Feeder.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Feeder.SelectedRows[i_Temp].Index != dGV_Feeder.Rows.Count - 1)
                    {
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[1].Value = cB_DownVisionID.Text;
                    }
                }
            }
        }

        private void bSetCamRotate_Click(object sender, EventArgs e)
        {
            try
            {
                double r = double.Parse(tCamRotate.Text);
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("出标下视觉拍照角度设置错误!", 1);
                return;
            }
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Feeder.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Feeder.SelectedRows[i_Temp].Index != dGV_Feeder.Rows.Count - 1)
                    {
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[2].Value = tCamRotate.Text;
                    }
                }
            }
        }

        private void bUpVisionID_Click(object sender, EventArgs e)
        {
            if (cB_UpVisionID.Text == "")
            {
                msgDiv1.MsgDivShow("出标上视觉定位设置错误!", 1);
                return;
            }
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Feeder.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Feeder.SelectedRows[i_Temp].Index != dGV_Feeder.Rows.Count - 1)
                    {
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[9].Value = cB_UpVisionID.Text;
                    }
                }
            }
        }

        private void bSetUpCam_XY_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Feeder.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Feeder.SelectedRows[i_Temp].Index != dGV_Feeder.Rows.Count - 1)
                    {
                        if (cB_Module.SelectedIndex == 0)
                        {
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[10].Value = Variable.X_Module1.ActAxisValue.ToString("F3");
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[11].Value = Variable.Y_Module1.ActAxisValue.ToString("F3");
                        }
                        else
                        {
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[10].Value = Variable.X_Module2.ActAxisValue.ToString("F3");
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[11].Value = Variable.Y_Module2.ActAxisValue.ToString("F3");
                        }
                    }
                }
            }
        }

        private void bGoUpCam_XY_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                try
                {
                    var RSS = MessageBox.Show(this, "确定要移动到点吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (RSS)
                    {
                        case DialogResult.Yes:
                            PointF TEMP = new PointF();
                            TEMP.X = float.Parse(dGV_Feeder.SelectedRows[0].Cells[8].Value.ToString());
                            TEMP.Y = float.Parse(dGV_Feeder.SelectedRows[0].Cells[8].Value.ToString());
                            if (cB_Module.SelectedIndex == 0)
                            {
                                Motion.XYGoPos(1,TEMP, Variable.VelMode_DebugMode_Manual_Module1);
                            }
                            else
                            {
                                Motion.XYGoPos(2,TEMP, Variable.VelMode_DebugMode_Manual_Module2);
                            }
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                catch
                {
                    msgDiv1.MsgDivShow("走到点错误!", 1);
                }
            }
        }
        //到位感应
        private void bReachIndex_Click(object sender, EventArgs e)
        {
            short index = 0;
            try
            {
                index = short.Parse(tReachIndex.Text);
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("到位Sensor设置错误!", 1);
                return;
            }
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Feeder.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Feeder.SelectedRows[i_Temp].Index != dGV_Feeder.Rows.Count - 1)
                    {
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[0].Value = index.ToString();
                    }
                }
            }
        }
        //XY
        private void bXY_Cam_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Feeder.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Feeder.SelectedRows[i_Temp].Index != dGV_Feeder.Rows.Count - 1)
                    {
                        if (cB_Module.SelectedIndex == 0)
                        {
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[5].Value = Variable.X_Module1.ActAxisValue.ToString("F3");
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[6].Value = Variable.Y_Module1.ActAxisValue.ToString("F3");
                        }
                        else
                        {
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[5].Value = Variable.X_Module2.ActAxisValue.ToString("F3");
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[6].Value = Variable.Y_Module2.ActAxisValue.ToString("F3");
                        }
                    }
                }
            }
        }

        private void bCam2Pos_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                try
                {
                    var RSS = MessageBox.Show(this, "确定要移动到点吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (RSS)
                    {
                        case DialogResult.Yes:
                            PointF TEMP = new PointF();
                            TEMP.X = float.Parse(dGV_Feeder.SelectedRows[0].Cells[8].Value.ToString());
                            TEMP.Y = float.Parse(dGV_Feeder.SelectedRows[0].Cells[8].Value.ToString());
                            if (cB_Module.SelectedIndex == 0)
                            {
                                Motion.XYGoPos(1, TEMP, Variable.VelMode_DebugMode_Manual_Module1);
                            }
                            else
                            {
                                Motion.XYGoPos(2, TEMP, Variable.VelMode_DebugMode_Manual_Module2);
                            }
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                catch
                {
                    msgDiv1.MsgDivShow("走到点错误!", 1);
                }
            }
        }

        private void bXY_Nz_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Feeder.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Feeder.SelectedRows[i_Temp].Index != dGV_Feeder.Rows.Count - 1)
                    {
                        short NzIndex = (short)(short.Parse(cB_NzXY.Text) - 1);
                        if (cB_Module.SelectedIndex == 0)
                        {
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[5].Value = frm_Main.CamtoNozzlePoint(1, NzIndex, Motion.getXY(1)).X.ToString("F3");
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[6].Value = frm_Main.CamtoNozzlePoint(1, NzIndex, Motion.getXY(1)).Y.ToString("F3");
                        }
                        else
                        {
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[5].Value = frm_Main.CamtoNozzlePoint(2, NzIndex, Motion.getXY(2)).X.ToString("F3");
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[6].Value = frm_Main.CamtoNozzlePoint(2, NzIndex, Motion.getXY(2)).Y.ToString("F3");
                        }
                    }
                }
            }
        }

        private void b1Nozzle2Pos_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                try
                {
                    var RSS = MessageBox.Show(this, "确定要移动到点吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (RSS)
                    {
                        case DialogResult.Yes:
                            PointF TEMP = new PointF();
                            short NzIndex = (short)(short.Parse(cB_NzXY.Text) - 1);
                            TEMP.X = float.Parse(dGV_Feeder.SelectedRows[0].Cells[5].Value.ToString());
                            TEMP.Y = float.Parse(dGV_Feeder.SelectedRows[0].Cells[6].Value.ToString());
                            if (cB_Module.SelectedIndex == 0)
                            {
                                Motion.XYGoPos(1, frm_Main.NozzletoCamPoint(1, NzIndex, TEMP), Variable.VelMode_DebugMode_Manual_Module1);
                            }
                            else
                            {
                                Motion.XYGoPos(2, frm_Main.NozzletoCamPoint(2, NzIndex, TEMP), Variable.VelMode_DebugMode_Manual_Module2);
                            }
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                catch
                {
                    msgDiv1.MsgDivShow("走到点错误!", 1);
                }
            }
        }

        private void bCrossAdjust_Click(object sender, EventArgs e)
        {

        }

        private void bCamAdjust_Click(object sender, EventArgs e)
        {

        }

        private void bZPos_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Feeder.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Feeder.SelectedRows[i_Temp].Index != dGV_Feeder.Rows.Count - 1)
                    {
                        short NzIndex = (short)(short.Parse(cB_NzZ.Text) - 1);
                        if (cB_Module.SelectedIndex == 0)
                        {
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[7 + NzIndex].Value = Motion.getZ(1, NzIndex).ToString("F3");
                        }
                        else
                        {
                            dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[7 + NzIndex].Value = Motion.getZ(2, NzIndex).ToString("F3");
                        }
                    }
                }
            }
        }

        private void bZ1GoPos_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                try
                {
                    var RSS = MessageBox.Show(this, "确定要移动到点吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (RSS)
                    {
                        case DialogResult.Yes:
                            double TEMP = 0 ;
                            short NzIndex = (short)(short.Parse(cB_NzZ.Text) - 1);
                            if(NzIndex == 0)
                            {
                                TEMP = double.Parse(dGV_Feeder.SelectedRows[0].Cells[7].Value.ToString());
                            }
                            if (NzIndex == 1)
                            {
                                TEMP = double.Parse(dGV_Feeder.SelectedRows[0].Cells[8].Value.ToString());
                            }
                            if (cB_Module.SelectedIndex == 0)
                            {
                                Motion.ZGoPos(1, NzIndex, TEMP, Variable.VelMode_DebugMode_Manual_Module1);
                            }
                            else
                            {
                                Motion.ZGoPos(2, NzIndex, TEMP, Variable.VelMode_DebugMode_Manual_Module2);
                            }
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                catch
                {
                    msgDiv1.MsgDivShow("走到点错误!", 1);
                }
            }
        }

        private void bSetSuckRotate_Click(object sender, EventArgs e)
        {
            try
            {
                double r = double.Parse(tSuckRotate.Text);
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("出标吸取角度设置错误!", 1);
                return;
            }
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Feeder.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Feeder.SelectedRows[i_Temp].Index != dGV_Feeder.Rows.Count - 1)
                    {
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[4].Value = tSuckRotate.Text;
                    }
                }
            }
        }

        private void bGoSuckRotate_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                try
                {
                    var RSS = MessageBox.Show(this, "确定要移动到点吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (RSS)
                    {
                        case DialogResult.Yes:
                            double TEMP = 0;
                            short NzIndex = (short)(short.Parse(cB_NzU.Text) - 1);
                            TEMP = double.Parse(dGV_Feeder.SelectedRows[0].Cells[4].Value.ToString());
                            if (cB_Module.SelectedIndex == 0)
                            {
                                Motion.UGoPos(1, NzIndex, TEMP, Variable.VelMode_DebugMode_Manual_Module1);
                            }
                            else
                            {
                                Motion.ZGoPos(2, NzIndex, TEMP, Variable.VelMode_DebugMode_Manual_Module2);
                            }
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                catch
                {
                    msgDiv1.MsgDivShow("走到点错误!", 1);
                }
            }
        }

        private void cB_SuckP_CheckedChanged(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Feeder.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Feeder.SelectedRows[i_Temp].Index != dGV_Feeder.Rows.Count - 1)
                    {
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[3].Value = cB_SuckP.Checked == true ? "1" : "0";
                    }
                }
            }
        }

        private short XI_Module = 0;
        private short XI_NzIndex = 0;
        private double XI_Depth = 0;
        private PointF XI_Point = default(PointF);
        private void bSuckLabel_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                try
                {
                    XI_Module = (short)(cB_Module.SelectedIndex + 1);
                    XI_NzIndex = (short)cB_NzA.SelectedIndex;
                    if (!Motion.IsZSafeHeigt(XI_Module))
                    {
                        msgDiv1.MsgDivShow("吸嘴未在安全高度!", 1);
                        return;
                    }
                    if (XI_NzIndex == 0)
                    {
                        XI_Depth = double.Parse(dGV_Feeder.SelectedRows[0].Cells[7].Value.ToString());
                    }
                    else
                    {
                        XI_Depth = double.Parse(dGV_Feeder.SelectedRows[0].Cells[8].Value.ToString());
                    }
                    XI_Point.X = float.Parse(dGV_Feeder.SelectedRows[0].Cells[5].Value.ToString());
                    XI_Point.Y = float.Parse(dGV_Feeder.SelectedRows[0].Cells[6].Value.ToString());
                    Thread Thread_NozzleTest = new Thread(new ThreadStart(thread_NozzleXI));
                    Thread_NozzleTest.Start();
                }
                catch
                {
                    msgDiv1.MsgDivShow("吸嘴吸单张失败!", 1);
                }
            }
        }

        //吸嘴自动吸标签
        private void thread_NozzleXI()
        {
            short RTN = 0;
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            RTN = Motion.XYGoPos(XI_Module, frm_Main.NozzletoCamPoint(XI_Module, XI_NzIndex,XI_Point), Variable.VelMode_TestMode);
            if (RTN != 0)
            {
                return;
            }
            while (!Motion.GetXYReach(XI_Module, frm_Main.NozzletoCamPoint(XI_Module, XI_NzIndex, XI_Point)))
            {
                if(sw.ElapsedMilliseconds > 60*1000)
                {
                    return;
                }
            }
            RTN = Motion.ZGoPos(XI_Module, XI_NzIndex, XI_Depth, Variable.VelMode_TestMode);
            if (RTN != 0)
            {
                return;
            }
            while (!Motion.GetZReach(XI_Module, XI_NzIndex, XI_Depth))
            {
                if (sw.ElapsedMilliseconds > 60 * 1000)
                {
                    return;
                }
            }
            RTN = Motion.Z_XI(XI_Module, XI_NzIndex);
            Thread.Sleep(1000);
            Motion.ZGoSafeHeight(XI_Module, XI_NzIndex,Variable.VelMode_TestMode);
        }

        //4-DownCamPoint**************************************************
        private short DownCamPoint_Module = 1;
        private short DownCamPoint_NzIndex = 0;
        private void updateUI(short Module)
        {
            if(DownCamPoint_Module == 1)
            {
                dGV_CamDownPoint.Rows[0].Cells[0].Value = Variable.AlmProgram.DownCapture_Module1.StartPoint.X.ToString("F3");
                dGV_CamDownPoint.Rows[0].Cells[1].Value = Variable.AlmProgram.DownCapture_Module1.StartPoint.Y.ToString("F3");
                dGV_CamDownPoint.Rows[1].Cells[0].Value = Variable.AlmProgram.DownCapture_Module1.NozzlePoint[0].X.ToString("F3");
                dGV_CamDownPoint.Rows[1].Cells[1].Value = Variable.AlmProgram.DownCapture_Module1.NozzlePoint[0].Y.ToString("F3");
                dGV_CamDownPoint.Rows[2].Cells[0].Value = Variable.AlmProgram.DownCapture_Module1.NozzlePoint[1].X.ToString("F3");
                dGV_CamDownPoint.Rows[2].Cells[1].Value = Variable.AlmProgram.DownCapture_Module1.NozzlePoint[1].Y.ToString("F3");
                dGV_CamDownPoint.Rows[3].Cells[0].Value = Variable.AlmProgram.DownCapture_Module1.NozzlePoint[2].X.ToString("F3");
                dGV_CamDownPoint.Rows[3].Cells[1].Value = Variable.AlmProgram.DownCapture_Module1.NozzlePoint[2].Y.ToString("F3");
                dGV_CamDownPoint.Rows[4].Cells[0].Value = Variable.AlmProgram.DownCapture_Module1.NozzlePoint[3].X.ToString("F3");
                dGV_CamDownPoint.Rows[4].Cells[1].Value = Variable.AlmProgram.DownCapture_Module1.NozzlePoint[3].Y.ToString("F3");
                dGV_CamDownPoint.Rows[5].Cells[0].Value = Variable.AlmProgram.DownCapture_Module1.EndPoint.X.ToString("F3");
                dGV_CamDownPoint.Rows[5].Cells[1].Value = Variable.AlmProgram.DownCapture_Module1.EndPoint.Y.ToString("F3");
                tNZCaptureCount.Text = Variable.AlmProgram.DownCapture_Module1.NozzleCaptureCount[XI_NzIndex].ToString("F3");
                tNZAlignBase.Text = Variable.AlmProgram.DownCapture_Module1.NozzleBaseIndex[XI_NzIndex].ToString("F3");
                cB_FlyMode.Checked = Variable.AlmProgram.DownCapture_Module1.FlyModeEN;
            }
            else
            {
                dGV_CamDownPoint.Rows[0].Cells[0].Value = Variable.AlmProgram.DownCapture_Module2.StartPoint.X.ToString("F3");
                dGV_CamDownPoint.Rows[0].Cells[1].Value = Variable.AlmProgram.DownCapture_Module2.StartPoint.Y.ToString("F3");
                dGV_CamDownPoint.Rows[1].Cells[0].Value = Variable.AlmProgram.DownCapture_Module2.NozzlePoint[0].X.ToString("F3");
                dGV_CamDownPoint.Rows[1].Cells[1].Value = Variable.AlmProgram.DownCapture_Module2.NozzlePoint[0].Y.ToString("F3");
                dGV_CamDownPoint.Rows[2].Cells[0].Value = Variable.AlmProgram.DownCapture_Module2.NozzlePoint[1].X.ToString("F3");
                dGV_CamDownPoint.Rows[2].Cells[1].Value = Variable.AlmProgram.DownCapture_Module2.NozzlePoint[1].Y.ToString("F3");
                dGV_CamDownPoint.Rows[3].Cells[0].Value = Variable.AlmProgram.DownCapture_Module2.NozzlePoint[2].X.ToString("F3");
                dGV_CamDownPoint.Rows[3].Cells[1].Value = Variable.AlmProgram.DownCapture_Module2.NozzlePoint[2].Y.ToString("F3");
                dGV_CamDownPoint.Rows[4].Cells[0].Value = Variable.AlmProgram.DownCapture_Module2.NozzlePoint[3].X.ToString("F3");
                dGV_CamDownPoint.Rows[4].Cells[1].Value = Variable.AlmProgram.DownCapture_Module2.NozzlePoint[3].Y.ToString("F3");
                dGV_CamDownPoint.Rows[5].Cells[0].Value = Variable.AlmProgram.DownCapture_Module2.EndPoint.X.ToString("F3");
                dGV_CamDownPoint.Rows[5].Cells[1].Value = Variable.AlmProgram.DownCapture_Module2.EndPoint.Y.ToString("F3");
                tNZCaptureCount.Text = Variable.AlmProgram.DownCapture_Module2.NozzleCaptureCount[XI_NzIndex].ToString("F3");
                tNZAlignBase.Text = Variable.AlmProgram.DownCapture_Module2.NozzleBaseIndex[XI_NzIndex].ToString("F3");
                cB_FlyMode.Checked = Variable.AlmProgram.DownCapture_Module2.FlyModeEN;
            }
        }

        private void cB_NzDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            DownCamPoint_NzIndex = (short)cB_NzDown.SelectedIndex;
        }

        private void rB_Module1_CheckedChanged(object sender, EventArgs e)
        {
            if (rB_Module1.Checked)
            {
                DownCamPoint_Module = 1;
                rB_Module1.BackColor = Color.CornflowerBlue;
                rB_Module2.BackColor = Color.Transparent;
                updateUI(DownCamPoint_Module);
            }
            else
            {
                DownCamPoint_Module = 2;
                rB_Module1.BackColor = Color.Transparent;
                rB_Module2.BackColor = Color.CornflowerBlue;
                updateUI(DownCamPoint_Module);
            }
        }

        private void rB_Module2_CheckedChanged(object sender, EventArgs e)
        {
            if (rB_Module1.Checked)
            {
                DownCamPoint_Module = 1;
                rB_Module1.BackColor = Color.CornflowerBlue;
                rB_Module2.BackColor = Color.Transparent;
                updateUI(DownCamPoint_Module);
            }
            else
            {
                DownCamPoint_Module = 2;
                rB_Module1.BackColor = Color.Transparent;
                rB_Module2.BackColor = Color.CornflowerBlue;
                updateUI(DownCamPoint_Module);
            }
        }

        private void bSetDownCapture_Click(object sender, EventArgs e)
        {
            short i0, i1;
            try
            {
                i0 = short.Parse(tNZCaptureCount.Text);
                i1 = short.Parse(tNZAlignBase.Text);
                if (DownCamPoint_Module == 1)
                {
                    Variable.AlmProgram.DownCapture_Module1.StartPoint.X = float.Parse(dGV_CamDownPoint.Rows[0].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module1.StartPoint.Y = float.Parse(dGV_CamDownPoint.Rows[0].Cells[1].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module1.NozzlePoint[0].X = float.Parse(dGV_CamDownPoint.Rows[1].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module1.NozzlePoint[0].Y = float.Parse(dGV_CamDownPoint.Rows[1].Cells[1].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module1.NozzlePoint[1].X = float.Parse(dGV_CamDownPoint.Rows[2].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module1.NozzlePoint[1].Y = float.Parse(dGV_CamDownPoint.Rows[2].Cells[1].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module1.NozzlePoint[2].X = float.Parse(dGV_CamDownPoint.Rows[3].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module1.NozzlePoint[2].Y = float.Parse(dGV_CamDownPoint.Rows[3].Cells[1].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module1.NozzlePoint[3].X = float.Parse(dGV_CamDownPoint.Rows[4].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module1.NozzlePoint[3].Y = float.Parse(dGV_CamDownPoint.Rows[4].Cells[1].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module1.EndPoint.X = float.Parse(dGV_CamDownPoint.Rows[5].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module1.EndPoint.Y = float.Parse(dGV_CamDownPoint.Rows[5].Cells[1].Value.ToString());

                    Variable.AlmProgram.DownCapture_Module1.NozzleCaptureCount[XI_NzIndex] = i0;
                    Variable.AlmProgram.DownCapture_Module1.NozzleBaseIndex[XI_NzIndex] = i1;
                    Variable.AlmProgram.DownCapture_Module1.FlyModeEN = cB_FlyMode.Checked;
                }
                else
                {
                    Variable.AlmProgram.DownCapture_Module2.StartPoint.X = float.Parse(dGV_CamDownPoint.Rows[0].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module2.StartPoint.Y = float.Parse(dGV_CamDownPoint.Rows[0].Cells[1].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module2.NozzlePoint[0].X = float.Parse(dGV_CamDownPoint.Rows[1].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module2.NozzlePoint[0].Y = float.Parse(dGV_CamDownPoint.Rows[1].Cells[1].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module2.NozzlePoint[1].X = float.Parse(dGV_CamDownPoint.Rows[2].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module2.NozzlePoint[1].Y = float.Parse(dGV_CamDownPoint.Rows[2].Cells[1].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module2.NozzlePoint[2].X = float.Parse(dGV_CamDownPoint.Rows[3].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module2.NozzlePoint[2].Y = float.Parse(dGV_CamDownPoint.Rows[3].Cells[1].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module2.NozzlePoint[3].X = float.Parse(dGV_CamDownPoint.Rows[4].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module2.NozzlePoint[3].Y = float.Parse(dGV_CamDownPoint.Rows[4].Cells[1].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module2.EndPoint.X = float.Parse(dGV_CamDownPoint.Rows[5].Cells[0].Value.ToString());
                    Variable.AlmProgram.DownCapture_Module2.EndPoint.Y = float.Parse(dGV_CamDownPoint.Rows[5].Cells[1].Value.ToString());

                    Variable.AlmProgram.DownCapture_Module2.NozzleCaptureCount[XI_NzIndex] = i0;
                    Variable.AlmProgram.DownCapture_Module2.NozzleBaseIndex[XI_NzIndex] = i1;
                    Variable.AlmProgram.DownCapture_Module2.FlyModeEN = cB_FlyMode.Checked;
                }
            }
            catch
            {
                msgDiv1.MsgDivShow("数值输入错误!", 1);
            }
        }

        private void bDownCamXY_Click(object sender, EventArgs e)
        {
            if (dGV_CamDownPoint.SelectedRows.Count > 0 && (dGV_CamDownPoint.SelectedRows[0].Index != dGV_CamDownPoint.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_CamDownPoint.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_CamDownPoint.SelectedRows[i_Temp].Index != dGV_CamDownPoint.Rows.Count - 1)
                    {
                        dGV_CamDownPoint.Rows[dGV_CamDownPoint.SelectedRows[i_Temp].Index].Cells[0].Value = Motion.getXY(DownCamPoint_Module).X.ToString("F3");
                        dGV_CamDownPoint.Rows[dGV_CamDownPoint.SelectedRows[i_Temp].Index].Cells[1].Value = Motion.getXY(DownCamPoint_Module).Y.ToString("F3");
                    }
                }
            }
        }

        private void bGoDownCamXY_Click(object sender, EventArgs e)
        {
            if (dGV_CamDownPoint.SelectedRows.Count > 0 && (dGV_CamDownPoint.SelectedRows[0].Index != dGV_CamDownPoint.RowCount - 1))
            {
                try
                {
                    var RSS = MessageBox.Show(this, "确定要移动到点吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (RSS)
                    {
                        case DialogResult.Yes:
                            PointF TEMP = new PointF();
                            TEMP.X = float.Parse(dGV_CamDownPoint.SelectedRows[0].Cells[0].Value.ToString());
                            TEMP.Y = float.Parse(dGV_CamDownPoint.SelectedRows[0].Cells[1].Value.ToString());
                            if (DownCamPoint_Module == 1)
                            {
                                Motion.XYGoPos(1, TEMP, Variable.VelMode_DebugMode_Manual_Module1);
                            }
                            else
                            {
                                Motion.XYGoPos(2, TEMP, Variable.VelMode_DebugMode_Manual_Module2);
                            }
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                catch
                {
                    msgDiv1.MsgDivShow("走到点错误!", 1);
                }
            }
        }
        
        //5-MultiPoint**************************************************
        private void bSetCoord_Now_Click(object sender, EventArgs e)
        {
            tNowOriginX.Text = Motion.getXY(1).X.ToString("F3");
            tNowOriginY.Text = Motion.getXY(1).Y.ToString("F3");
        }

        private void bSetCoord_Program_Click(object sender, EventArgs e)
        {
            tNowOriginX.Text = Motion.getXY(1).X.ToString("F3");
            tNowOriginY.Text = Motion.getXY(1).Y.ToString("F3");
        }

        private void bAddPaste_Click(object sender, EventArgs e)
        {
            //获取当前选定的Index
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.Rows.Count - 1))
            {
                dGV_Program.Rows.Insert(dGV_Program.SelectedRows[0].Index + 1, 1);
                Common.CommonHelper.AddRowHeader(dGV_Program);
                DataGridViewRow item = dGV_Program.Rows[dGV_Program.SelectedRows[0].Index + 1];
                item.Cells[0].Value = "0";
                item.Cells[1].Value = "0";
                item.Cells[2].Value = "0";
                item.Cells[3].Value = "0";
                item.Cells[4].Value = "0";
                item.Cells[5].Value = "0";
                item.Cells[6].Value = "0";
                item.Cells[7].Value = "0";
                item.Cells[8].Value = "0";
                item.Cells[9].Value = "0";
                item.Cells[10].Value = "0";
                item.Cells[11].Value = "0";
            }
            else
            {
                dGV_Program.Rows.Insert(0, 1);
                Common.CommonHelper.AddRowHeader(dGV_Program);
                DataGridViewRow item = dGV_Program.Rows[0];
                item.Cells[0].Value = "0";
                item.Cells[1].Value = "0";
                item.Cells[2].Value = "0";
                item.Cells[3].Value = "0";
                item.Cells[4].Value = "0";
                item.Cells[5].Value = "0";
                item.Cells[6].Value = "0";
                item.Cells[7].Value = "0";
                item.Cells[8].Value = "0";
                item.Cells[9].Value = "0";
                item.Cells[10].Value = "0";
                item.Cells[11].Value = "0";
            }
        }

        private void bDelPaste_Click(object sender, EventArgs e)
        {
            if (dGV_Program.SelectedRows.Count > 0)
            {
                //获取当前选中的行（单行或者多行）
                DialogResult RSS;
                RSS = MessageBox.Show(this, "确定要删除选中行数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                switch (RSS)
                {
                    case DialogResult.Yes:
                        //获取当前选定的Index
                        for (int i = this.dGV_Program.SelectedRows.Count; i > 0; i--)
                        {
                            if (dGV_Program.SelectedRows[i - 1].Index == (dGV_Program.RowCount - 1))
                            {
                                msgDiv1.MsgDivShow("末尾行无数据不可删除!", 1);
                                break;
                            }
                            int Index = dGV_Program.SelectedRows[i - 1].Index;
                            dGV_Program.Rows.RemoveAt(Index);
                        }
                        Common.CommonHelper.AddRowHeader(dGV_Program);
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }

        private void bPasteUp_Click(object sender, EventArgs e)
        {
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.RowCount - 1))
            {
                int index = dGV_Program.SelectedRows[0].Index;
                if (index == 0)
                {
                    return;
                }
                List<string> list = new List<string>();
                for (int i = 0; i < dGV_Program.Columns.Count; i++)
                {
                    list.Add(dGV_Program.SelectedRows[0].Cells[i].Value.ToString());   //把当前选中行的数据存入list数组中  
                }
                for (int j = 0; j < dGV_Program.Columns.Count; j++)
                {
                    dGV_Program.Rows[index].Cells[j].Value = dGV_Program.Rows[index - 1].Cells[j].Value;
                    dGV_Program.Rows[index - 1].Cells[j].Value = list[j].ToString();
                }
                dGV_Program.Rows[index].Selected = false;
                dGV_Program.Rows[index - 1].Selected = true;
            }
        }

        private void bPasteDown_Click(object sender, EventArgs e)
        {
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.RowCount - 1))
            {
                int index = dGV_Program.SelectedRows[0].Index;
                if (index == dGV_Program.RowCount - 2)
                {
                    return;
                }
                List<string> list = new List<string>();
                for (int i = 0; i < dGV_Program.Columns.Count; i++)
                {
                    list.Add(dGV_Program.SelectedRows[0].Cells[i].Value.ToString());   //把当前选中行的数据存入list数组中  
                }

                for (int j = 0; j < dGV_Program.Columns.Count; j++)
                {
                    dGV_Program.Rows[index].Cells[j].Value = dGV_Program.Rows[index + 1].Cells[j].Value;
                    dGV_Program.Rows[index + 1].Cells[j].Value = list[j].ToString();
                }
                dGV_Program.Rows[index + 1].Selected = true;
                dGV_Program.Rows[index].Selected = false;
            }
        }

        private void bUpCamXYCoord1_Click(object sender, EventArgs e)
        {
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Program.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Program.SelectedRows[i_Temp].Index != dGV_Program.Rows.Count - 1)
                    {
                        dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[2].Value = Motion.getXY(1).X.ToString("F3");
                        dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[3].Value = Motion.getXY(1).Y.ToString("F3");
                    }
                }
            }
        }

        private void bUpCamXYCoord2_Click(object sender, EventArgs e)
        {
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Program.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Program.SelectedRows[i_Temp].Index != dGV_Program.Rows.Count - 1)
                    {
                        dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[4].Value = Motion.getXY(1).X.ToString("F3");
                        dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[5].Value = Motion.getXY(1).Y.ToString("F3");
                    }
                }
            }
        }

        private void bCamGoUp1_Click(object sender, EventArgs e)
        {
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.RowCount - 1))
            {
                try
                {
                    var RSS = MessageBox.Show(this, "确定要移动到点吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (RSS)
                    {
                        case DialogResult.Yes:
                            PointF TEMP = new PointF();
                            TEMP.X = float.Parse(dGV_Program.SelectedRows[0].Cells[2].Value.ToString());
                            TEMP.Y = float.Parse(dGV_Program.SelectedRows[0].Cells[3].Value.ToString());
                            Motion.XYGoPos(1, TEMP, Variable.VelMode_DebugMode_Manual_Module1);
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                catch
                {
                    msgDiv1.MsgDivShow("走到点错误!", 1);
                }
            }
        }

        private void bCamGoUp2_Click(object sender, EventArgs e)
        {
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.RowCount - 1))
            {
                try
                {
                    var RSS = MessageBox.Show(this, "确定要移动到点吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (RSS)
                    {
                        case DialogResult.Yes:
                            PointF TEMP = new PointF();
                            TEMP.X = float.Parse(dGV_Program.SelectedRows[0].Cells[4].Value.ToString());
                            TEMP.Y = float.Parse(dGV_Program.SelectedRows[0].Cells[5].Value.ToString());
                            Motion.XYGoPos(1, TEMP, Variable.VelMode_DebugMode_Manual_Module1);
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                catch
                {
                    msgDiv1.MsgDivShow("走到点错误!", 1);
                }
            }
        }

        private void bLoadPASTE_Click(object sender, EventArgs e)
        {
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.RowCount - 1))
            {
                VisionInfo temp = frm_Main.LoadVisionInfo(2,frm_Main.frm_Program.ProgramName, cB_PasteInfo.Text);
                if (temp.IsLoadSuccessful)
                {
                    for (int i_Temp = 0; i_Temp < dGV_Program.SelectedRows.Count; i_Temp++)
                    {
                        if (dGV_Program.SelectedRows[i_Temp].Index != dGV_Program.Rows.Count - 1)
                        {
                            dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[0].Value = cB_PasteInfo.Text;
                            dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[1].Value = temp.Type_FOVCount.ToString();
                        }
                    }
                }
                else
                {
                    msgDiv1.MsgDivShow("程式加载失败!", 1);
                }
            }
        }

        private void bSetOffsetXYR_Click(object sender, EventArgs e)
        {
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.RowCount - 1))
            {
                try
                {
                    double X = double.Parse(tOffSetX_Paste.Text);
                    double Y = double.Parse(tOffSetY_Paste.Text);
                    double R = double.Parse(tOffSetR_Paste.Text);
                    for (int i_Temp = 0; i_Temp < dGV_Program.SelectedRows.Count; i_Temp++)
                    {
                        if (dGV_Program.SelectedRows[i_Temp].Index != dGV_Program.Rows.Count - 1)
                        {
                            if (cB_NzOffset.SelectedIndex == 0)
                            {
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[6].Value = X.ToString("F3");
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[7].Value = Y.ToString("F3");
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[8].Value = R.ToString("F3");
                            }
                            else
                            {
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[9].Value = X.ToString("F3");
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[10].Value = Y.ToString("F3");
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[11].Value = R.ToString("F3");
                            }
                        }
                    }
                }
                catch
                {
                    msgDiv1.MsgDivShow("吸嘴偏移量设置失败!", 1);
                }
            }
        }

        private void bExpandPara_Click(object sender, EventArgs e)
        {
            frm_Expand frm = new frm_Expand();
            frm.Show();
        }

        private void AddPasteInfoPoints(PointF[] Points1, PointF[] Points2)
        {
            dGV_Program.Rows.Add(Points1.Length);
            for (int i = 0; i < Points1.Count() / dGV_Program.SelectedRows.Count; i++)
            {
                for (int j = 0; j < dGV_Program.SelectedRows.Count; j++)
                {
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[0].Value = dGV_Program.SelectedRows[j].Cells[0].Value.ToString();
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[1].Value = dGV_Program.SelectedRows[j].Cells[1].Value.ToString();
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[2].Value = Points1[i * dGV_Program.SelectedRows.Count + j].X.ToString();
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[3].Value = Points1[i * dGV_Program.SelectedRows.Count + j].Y.ToString();
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[4].Value = Points2[i * dGV_Program.SelectedRows.Count + j].X.ToString();
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[5].Value = Points2[i * dGV_Program.SelectedRows.Count + j].Y.ToString();
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[6].Value = "0";
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[7].Value = "0";
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[8].Value = "0";
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[9].Value = "0";
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[10].Value = "0";
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[11].Value = "0";
                }
            }
            Common.CommonHelper.AddRowHeader(dGV_Program);
        }

        private void bExpand_Click(object sender, EventArgs e)
        {
            if(Variable.bExpand)
            {
                if (dGV_Program.SelectedRows.Count > 0 && dGV_Program.SelectedRows[0].Index != dGV_Program.Rows.Count - 1)
                {
                    List<PointF> Point2Expand_Mark1 = new List<PointF>();
                    for (int ii = 0; ii < dGV_Program.SelectedRows.Count; ii++)
                    {
                        Point2Expand_Mark1.Add(new PointF(float.Parse(dGV_Program.SelectedRows[ii].Cells[2].Value.ToString()), float.Parse(dGV_Program.SelectedRows[ii].Cells[3].Value.ToString())));
                    }
                    PointF[] Points2Add_Mark1 = Common.MathHelper.Expand2AddPoints(Point2Expand_Mark1.ToArray(), Variable.pExpand_O, Variable.pExpand_B, Variable.pExpand_A, Variable.iExpand_N, Variable.iExpand_M);

                    List<PointF> Point2Expand_Mark2 = new List<PointF>();
                    for (int ii = 0; ii < dGV_Program.SelectedRows.Count; ii++)
                    {
                        Point2Expand_Mark2.Add(new PointF(float.Parse(dGV_Program.SelectedRows[ii].Cells[4].Value.ToString()), float.Parse(dGV_Program.SelectedRows[ii].Cells[5].Value.ToString())));
                    }
                    PointF[] Points2Add_Mark2 = Common.MathHelper.Expand2AddPoints(Point2Expand_Mark2.ToArray(), Variable.pExpand_O, Variable.pExpand_B, Variable.pExpand_A, Variable.iExpand_N, Variable.iExpand_M);

                    AddPasteInfoPoints(Points2Add_Mark1, Points2Add_Mark2);
                }
                else
                {
                    msgDiv1.MsgDivShow("请选择要扩展的点!", 1);
                }
            }
            else
            {
                msgDiv1.MsgDivShow("请先设置扩展参数!", 1);
            }
        }
    }
}
