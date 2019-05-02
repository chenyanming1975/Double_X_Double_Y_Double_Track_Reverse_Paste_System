using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using General;
using GeneralMachine.MotionControl;
using HalconDotNet;

namespace GeneralMachine
{
    public partial class frm_Wizard_Vision : Form
    {
        //**********************image**********************
        public HObject imageSet;
        public Vision.RectangleRegion roi;
        public HObject Hroi;
        public int shutter;
        public bool bLight_RD, bLight_GD, bLight_BD, bLight_U;
        public int iLight_RD, iLight_GD, iLight_BD, iLight_U;

        /// <summary>
        /// 更新图像信息
        /// </summary>
        /// <returns></returns>
        private bool UpdateImage_Roi()
        {
            try
            {
                Hroi.Dispose();
                imageSet.Dispose();
                imageSet = frm_Main.Image(frm_Main.frm_Camera.imageSet.Image);
                roi = frm_Main.CROI(frm_Main.frm_Camera.imageSet.Roi);
                Hroi = frm_Main.HROI(frm_Main.frm_Camera.imageSet.Roi);
                shutter = (int)frm_Main.frm_Camera.ntCamShutter.Value;
                bLight_U = frm_Main.frm_Camera.cB_U.Checked;
                bLight_RD = frm_Main.frm_Camera.cB_RD.Checked;
                bLight_GD = frm_Main.frm_Camera.cB_GD.Checked;
                bLight_BD = frm_Main.frm_Camera.cB_BD.Checked;
                iLight_U = frm_Main.frm_Camera.tB_U.Value;
                iLight_RD = frm_Main.frm_Camera.tB_RD.Value;
                iLight_GD = frm_Main.frm_Camera.tB_GD.Value;
                iLight_BD = frm_Main.frm_Camera.tB_BD.Value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public frm_Main frm_Main = null;
        public frm_Wizard_Vision()
        {
            InitializeComponent();
        }
        public frm_Wizard_Vision(Object obj)
        {
            InitializeComponent();
            frm_Main = (frm_Main)obj;
        }
        private void frm_Wizard_Vision_Load(object sender, EventArgs e)
        {
            Variable.AlmVisionInfo = new Definition.VisionInfo();
            Variable.AlmBaseInfo = new Definition.BaseInfo();
        }

        private void wizardControl1_NextButtonClick(WizardBase.WizardControl sender, WizardBase.WizardNextButtonClickEventArgs args)
        {
            if(wizardControl1.CurrentStepIndex == 0)
            {
                if (tProgramName.Text == "")
                {
                    msgDiv1.MsgDivShow("程式名称未设定!", 1);
                    args.Cancel = true;
                }
                Variable.AlmVisionInfo.T1_InitROI = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T1_InitAreaROI = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T1_CircleROI = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T1_PatternROI = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T1_CrossLineROI1 = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T1_CrossLineROI2 = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T2_InitROI = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T2_InitAreaROI = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T2_CircleROI = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T2_PatternROI = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T2_CrossLineROI1 = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T2_CrossLineROI2 = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.T2_LineROI = new Vision.RectangleRegion();
                Variable.AlmVisionInfo.VsionProName = tProgramName.Text;
                Variable.AlmVisionInfo.Type_Vision = (short)cB_VisionType.SelectedIndex;
            }
            if (wizardControl1.CurrentStepIndex == 1)
            {
                try
                {
                    Variable.AlmVisionInfo.Type_FOVCount = (short)(cB_FOV.SelectedIndex + 1);
                    Variable.AlmVisionInfo.Type_Align = (short)(cB_AlignStyle.SelectedIndex + 1);
                    if (cB_VisionType.SelectedIndex == 2)
                    {
                        Variable.AlmBaseInfo.Base_Angle = double.Parse(tBaseAngle.Text);
                        Variable.AlmBaseInfo.BaseXY_Align1 = new PointF();
                        Variable.AlmBaseInfo.BaseXY_Align1.X = float.Parse(tX1.Text);
                        Variable.AlmBaseInfo.BaseXY_Align1.Y = float.Parse(tY1.Text);
                        Variable.AlmBaseInfo.BaseXY_Align2 = new PointF();
                        Variable.AlmBaseInfo.BaseXY_Align2.X = float.Parse(tX2.Text);
                        Variable.AlmBaseInfo.BaseXY_Align2.Y = float.Parse(tY2.Text);
                    }
                }
                catch (Exception)
                {
                    msgDiv1.MsgDivShow("定位参数输入错误!", 1);
                    args.Cancel = true;
                }
                #region UI
                if (Variable.AlmVisionInfo.Type_Align == 1)//1.点1+点2（点1与点2中点定XY；点1与点2连线定R）
                {
                    cB_Algthrim1.Items.Clear();
                    cB_Algthrim1.Items.Add("点-找圆");
                    cB_Algthrim1.Items.Add("点-匹配");
                    cB_Algthrim1.Items.Add("点-线+线");
                    cB_Algthrim2.Items.Clear();
                    cB_Algthrim2.Items.Add("点-找圆");
                    cB_Algthrim2.Items.Add("点-匹配");
                    cB_Algthrim2.Items.Add("点-线+线");
                    cB_Algthrim1.SelectedIndex = 0;
                    cB_Algthrim2.SelectedIndex = 0;
                    gB_Circle1.Visible = true;
                    gB_Pattern1.Visible = false;
                    gB_LineCross1.Visible = false;
                    gB_Line1.Visible = false;

                    gB_Circle2.Visible = true;
                    gB_Pattern2.Visible = false;
                    gB_LineCross2.Visible = false;
                    gB_Line2.Visible = false;
                }
                if (Variable.AlmVisionInfo.Type_Align == 2)//2.线1 + 线2（线1与线2交点定XY；线1定R）
                {
                    cB_FOV.SelectedIndex = 0;
                    Variable.AlmVisionInfo.Type_FOVCount = (short)(cB_FOV.SelectedIndex + 1);
                    cB_Algthrim1.Items.Clear();
                    cB_Algthrim1.Items.Add("线");
                    cB_Algthrim2.Items.Clear();
                    cB_Algthrim2.Items.Add("线");
                    cB_Algthrim1.SelectedIndex = 0;
                    cB_Algthrim2.SelectedIndex = 0;
                    gB_Circle1.Visible = false;
                    gB_Pattern1.Visible = false;
                    gB_LineCross1.Visible = false;
                    gB_Line1.Visible = true;

                    gB_Circle2.Visible = false;
                    gB_Pattern2.Visible = false;
                    gB_LineCross2.Visible = false;
                    gB_Line2.Visible = true;
                }
                if (Variable.AlmVisionInfo.Type_Align == 3)//3.点1+点2（点1定XY；点1与点2连线定R）
                {
                    cB_Algthrim1.Items.Clear();
                    cB_Algthrim1.Items.Add("点-找圆");
                    cB_Algthrim1.Items.Add("点-匹配");
                    cB_Algthrim1.Items.Add("点-线+线");
                    cB_Algthrim2.Items.Clear();
                    cB_Algthrim2.Items.Add("点-找圆");
                    cB_Algthrim2.Items.Add("点-匹配");
                    cB_Algthrim2.Items.Add("点-线+线");
                    cB_Algthrim1.SelectedIndex = 0;
                    cB_Algthrim2.SelectedIndex = 0;
                    gB_Circle1.Visible = false;
                    gB_Pattern1.Visible = false;
                    gB_LineCross1.Visible = false;
                    gB_Line1.Visible = true;

                    gB_Circle2.Visible = false;
                    gB_Pattern2.Visible = false;
                    gB_LineCross2.Visible = false;
                    gB_Line2.Visible = true;
                }
                if (Variable.AlmVisionInfo.Type_Align == 4)//4.点1+点2（点2定XY；点1与点2连线定R）
                {
                    cB_Algthrim1.Items.Clear();
                    cB_Algthrim1.Items.Add("点-找圆");
                    cB_Algthrim1.Items.Add("点-匹配");
                    cB_Algthrim1.Items.Add("点-线+线");
                    cB_Algthrim2.Items.Clear();
                    cB_Algthrim2.Items.Add("点-找圆");
                    cB_Algthrim2.Items.Add("点-匹配");
                    cB_Algthrim2.Items.Add("点-线+线");
                    cB_Algthrim1.SelectedIndex = 0;
                    cB_Algthrim2.SelectedIndex = 0;
                    gB_Circle1.Visible = false;
                    gB_Pattern1.Visible = false;
                    gB_LineCross1.Visible = false;
                    gB_Line1.Visible = true;

                    gB_Circle2.Visible = false;
                    gB_Pattern2.Visible = false;
                    gB_LineCross2.Visible = false;
                    gB_Line2.Visible = true;
                }
                if (Variable.AlmVisionInfo.Type_Align == 5)//5.线1+线2（线1与线2交点定XY；线1与线2平分线定R）
                {
                    cB_FOV.SelectedIndex = 0;
                    Variable.AlmVisionInfo.Type_FOVCount = (short)(cB_FOV.SelectedIndex + 1);
                    cB_Algthrim1.Items.Clear();
                    cB_Algthrim1.Items.Add("线");
                    cB_Algthrim2.Items.Clear();
                    cB_Algthrim2.Items.Add("线");
                    cB_Algthrim1.SelectedIndex = 0;
                    cB_Algthrim2.SelectedIndex = 0;
                    gB_Circle1.Visible = false;
                    gB_Pattern1.Visible = false;
                    gB_LineCross1.Visible = false;
                    gB_Line1.Visible = true;

                    gB_Circle2.Visible = false;
                    gB_Pattern2.Visible = false;
                    gB_LineCross2.Visible = false;
                    gB_Line2.Visible = true;
                }
                if (Variable.AlmVisionInfo.Type_Align == 6)//6.线1+线2（线1与线2交点定XY；线2定R）
                {
                    cB_FOV.SelectedIndex = 0;
                    Variable.AlmVisionInfo.Type_FOVCount = (short)(cB_FOV.SelectedIndex + 1);
                    cB_Algthrim1.Items.Clear();
                    cB_Algthrim1.Items.Add("线");
                    cB_Algthrim2.Items.Clear();
                    cB_Algthrim2.Items.Add("线");
                    cB_Algthrim1.SelectedIndex = 0;
                    cB_Algthrim2.SelectedIndex = 0;
                    cB_Algthrim1.Items.Clear();
                    cB_Algthrim1.Items.Add("线");
                    cB_Algthrim2.Items.Clear();
                    cB_Algthrim2.Items.Add("线");
                    cB_Algthrim1.SelectedIndex = 0;
                    cB_Algthrim2.SelectedIndex = 0;
                    gB_Circle1.Visible = false;
                    gB_Pattern1.Visible = false;
                    gB_LineCross1.Visible = false;
                    gB_Line1.Visible = true;

                    gB_Circle2.Visible = false;
                    gB_Pattern2.Visible = false;
                    gB_LineCross2.Visible = false;
                    gB_Line2.Visible = true;
                }
                if (Variable.AlmVisionInfo.Type_Align == 7)//7.点1+线1（点1定XY；线定R）
                {
                    cB_Algthrim1.Items.Clear();
                    cB_Algthrim1.Items.Add("点-找圆");
                    cB_Algthrim1.Items.Add("点-匹配");
                    cB_Algthrim1.Items.Add("点-线+线");
                    cB_Algthrim2.Items.Clear();
                    cB_Algthrim2.Items.Add("线");
                    cB_Algthrim1.SelectedIndex = 0;
                    cB_Algthrim2.SelectedIndex = 0;
                    gB_Circle1.Visible = true;
                    gB_Pattern1.Visible = false;
                    gB_LineCross1.Visible = false;
                    gB_Line1.Visible = false;

                    gB_Circle2.Visible = false;
                    gB_Pattern2.Visible = false;
                    gB_LineCross2.Visible = false;
                    gB_Line2.Visible = true;
                }
                if (Variable.AlmVisionInfo.Type_Align == 8)//8.单视野匹配XYR(初定位)
                {
                    cB_Algthrim1.Items.Clear();
                    cB_Algthrim1.Items.Add("点-匹配");
                    cB_Algthrim2.Items.Clear();
                    gB_Circle1.Visible = false;
                    gB_Pattern1.Visible = true;
                    gB_LineCross1.Visible = false;
                    gB_Line1.Visible = false;

                    gB_Circle2.Visible = false;
                    gB_Pattern2.Visible = false;
                    gB_LineCross2.Visible = false;
                    gB_Line2.Visible = false;
                }
                #endregion
                #region 视野
                if (Variable.AlmVisionInfo.Type_FOVCount == 1)
                {
                    gB_Cam1.Visible = true;
                    gB_Shutter1.Visible = true;
                    gB_Light1.Visible = true;
                    gB_PreProcess1.Visible = true;
                    gB_Init1.Visible = true;

                    gB_Cam2.Visible = false;
                    gB_Shutter2.Visible = false;
                    gB_Light2.Visible = false;
                    gB_PreProcess2.Visible = false;
                    gB_Init2.Visible = false;
                }
                else
                {
                    gB_Cam1.Visible = true;
                    gB_Shutter1.Visible = true;
                    gB_Light1.Visible = true;
                    gB_PreProcess1.Visible = true;
                    gB_Init1.Visible = true;

                    gB_Cam2.Visible = true;
                    gB_Shutter2.Visible = true;
                    gB_Light2.Visible = true;
                    gB_PreProcess2.Visible = true;
                    gB_Init2.Visible = true;
                }
                #endregion
            }
            //特征1
            if (wizardControl1.CurrentStepIndex == 2)
            {
                try
                {
                    Variable.AlmVisionInfo.T1_CamPoint = new PointF();
                    Variable.AlmVisionInfo.T1_CamPoint.X = float.Parse(tCamPointX1.Text);
                    Variable.AlmVisionInfo.T1_CamPoint.Y = float.Parse(tCamPointY1.Text);
                    Variable.AlmVisionInfo.T1_Shutter = int.Parse(tShutter1.Text);
                    Variable.AlmVisionInfo.T1_Light_RedUse = cB_Light_R1.Checked;
                    Variable.AlmVisionInfo.T1_Light_GreenUse = cB_Light_G1.Checked;
                    Variable.AlmVisionInfo.T1_Light_BlueUse = cB_Light_B1.Checked;
                    Variable.AlmVisionInfo.T1_Light_RedValue = short.Parse(tLight_R1.Text);
                    Variable.AlmVisionInfo.T1_Light_GreenValue = short.Parse(tLight_G1.Text);
                    Variable.AlmVisionInfo.T1_Light_BlueValue = short.Parse(tLight_B1.Text);

                    Variable.AlmVisionInfo.T1_PreProcess_Gain = short.Parse(tGain1.Text);
                    Variable.AlmVisionInfo.T1_PreProcess_Offset = short.Parse(tOffset1.Text);
                    Variable.AlmVisionInfo.T1_InitROIEN = cB_InitAlign1.Checked;
                    Variable.AlmVisionInfo.T1_InitScore = double.Parse(tInitScore1.Text);
                    Variable.AlmVisionInfo.T1_InitMinDegree = double.Parse(tInitMinAngle1.Text);
                    Variable.AlmVisionInfo.T1_InitMaxDegree = double.Parse(tInitMaxAngle1.Text);
                    Variable.AlmVisionInfo.T1_InitAreaEN = cB_Area1.Checked;
                    Variable.AlmVisionInfo.T1_InitAreaIsWhite = rB_White1.Checked;
                    Variable.AlmVisionInfo.T1_InitMinArea = double.Parse(tMinAreaValue1.Text);
                    Variable.AlmVisionInfo.T1_InitMaxArea = double.Parse(tMaxAreaValue1.Text);
                    if (cB_Algthrim1.Text == "点-找圆")
                    {
                        Variable.AlmVisionInfo.T1_AlignIndex = 0;
                    }
                    if (cB_Algthrim1.Text == "点-匹配")
                    {
                        Variable.AlmVisionInfo.T1_AlignIndex = 1;
                    }
                    if (cB_Algthrim1.Text == "点-线+线")
                    {
                        Variable.AlmVisionInfo.T1_AlignIndex = 2;
                    }
                    if (cB_Algthrim1.Text == "线")
                    {
                        Variable.AlmVisionInfo.T1_AlignIndex = 3;
                    }
                    //0-找圆
                    Variable.AlmVisionInfo.T1_CircleMinR = double.Parse(tMinR1.Text);
                    Variable.AlmVisionInfo.T1_CircleMaxR = double.Parse(tMaxR1.Text);
                    //1-匹配
                    Variable.AlmVisionInfo.T1_PatternScore = double.Parse(tScore1.Text);
                    Variable.AlmVisionInfo.T1_PatternMinAngle = double.Parse(tMinAngle1.Text);
                    Variable.AlmVisionInfo.T1_PatternMaxAngle = double.Parse(tMaxAngle1.Text);
                    //2 - 点-线+线
                    Variable.AlmVisionInfo.T1_Crosspicdir1 = (Vision.PicDir)cB_PicDirCrossA1.SelectedIndex;
                    Variable.AlmVisionInfo.T1_Crossgraydir1 = (Vision.GrayDir)cB_GrayDirCrossA1.SelectedIndex;
                    Variable.AlmVisionInfo.T1_Crosspicdir2 = (Vision.PicDir)cB_PicDirCrossB1.SelectedIndex;
                    Variable.AlmVisionInfo.T1_Crossgraydir2 = (Vision.GrayDir)cB_GrayDirCrossB1.SelectedIndex;
                    //3-找边
                    Variable.AlmVisionInfo.T1_Linepicdir = (Vision.PicDir)cB_PicDir1.SelectedIndex;
                    Variable.AlmVisionInfo.T1_Linegraydir = (Vision.GrayDir)cB_GrayDir1.SelectedIndex;
                }
                catch (Exception)
                {
                    msgDiv1.MsgDivShow("参数未设置!", 1);
                    args.Cancel = true;
                }
            }

            //特征2
            if (wizardControl1.CurrentStepIndex == 3)
            {
                try
                {
                    Variable.AlmVisionInfo.T2_CamPoint = new PointF();
                    Variable.AlmVisionInfo.T2_CamPoint.X = float.Parse(tCamPointX2.Text);
                    Variable.AlmVisionInfo.T2_CamPoint.Y = float.Parse(tCamPointY2.Text);
                    Variable.AlmVisionInfo.T2_Shutter = int.Parse(tShutter2.Text);
                    Variable.AlmVisionInfo.T2_Light_RedUse = cB_Light_R2.Checked;
                    Variable.AlmVisionInfo.T2_Light_GreenUse = cB_Light_G2.Checked;
                    Variable.AlmVisionInfo.T2_Light_BlueUse = cB_Light_B2.Checked;
                    Variable.AlmVisionInfo.T2_Light_RedValue = short.Parse(tLight_R2.Text);
                    Variable.AlmVisionInfo.T2_Light_GreenValue = short.Parse(tLight_G2.Text);
                    Variable.AlmVisionInfo.T2_Light_BlueValue = short.Parse(tLight_B2.Text);

                    Variable.AlmVisionInfo.T2_PreProcess_Gain = short.Parse(tGain2.Text);
                    Variable.AlmVisionInfo.T2_PreProcess_Offset = short.Parse(tOffset2.Text);
                    Variable.AlmVisionInfo.T2_InitROIEN = cB_InitAlign2.Checked;
                    Variable.AlmVisionInfo.T2_InitScore = double.Parse(tInitScore2.Text);
                    Variable.AlmVisionInfo.T2_InitMinDegree = double.Parse(tInitMinAngle2.Text);
                    Variable.AlmVisionInfo.T2_InitMaxDegree = double.Parse(tInitMaxAngle2.Text);
                    Variable.AlmVisionInfo.T2_InitAreaEN = cB_Area2.Checked;
                    Variable.AlmVisionInfo.T2_InitAreaIsWhite = rB_White2.Checked;
                    Variable.AlmVisionInfo.T2_InitMinArea = double.Parse(tMinAreaValue2.Text);
                    Variable.AlmVisionInfo.T2_InitMaxArea = double.Parse(tMaxAreaValue2.Text);
                    if (cB_Algthrim2.Text == "点-找圆")
                    {
                        Variable.AlmVisionInfo.T2_AlignIndex = 0;
                    }
                    if (cB_Algthrim2.Text == "点-匹配")
                    {
                        Variable.AlmVisionInfo.T2_AlignIndex = 1;
                    }
                    if (cB_Algthrim2.Text == "点-线+线")
                    {
                        Variable.AlmVisionInfo.T2_AlignIndex = 2;
                    }
                    if (cB_Algthrim2.Text == "线")
                    {
                        Variable.AlmVisionInfo.T2_AlignIndex = 3;
                    }
                    //0-找圆
                    Variable.AlmVisionInfo.T2_CircleMinR = double.Parse(tMinR2.Text);
                    Variable.AlmVisionInfo.T2_CircleMaxR = double.Parse(tMaxR2.Text);
                    //1-匹配
                    Variable.AlmVisionInfo.T2_PatternScore = double.Parse(tScore2.Text);
                    Variable.AlmVisionInfo.T2_PatternMinAngle = double.Parse(tMinAngle2.Text);
                    Variable.AlmVisionInfo.T2_PatternMaxAngle = double.Parse(tMaxAngle2.Text);
                    //2 - 点-线+线
                    Variable.AlmVisionInfo.T2_Crosspicdir1 = (Vision.PicDir)cB_PicDirCrossA2.SelectedIndex;
                    Variable.AlmVisionInfo.T2_Crossgraydir1 = (Vision.GrayDir)cB_GrayDirCrossA2.SelectedIndex;
                    Variable.AlmVisionInfo.T2_Crosspicdir2 = (Vision.PicDir)cB_PicDirCrossB2.SelectedIndex;
                    Variable.AlmVisionInfo.T2_Crossgraydir2 = (Vision.GrayDir)cB_GrayDirCrossB2.SelectedIndex;
                    //3-找边
                    Variable.AlmVisionInfo.T2_Linepicdir = (Vision.PicDir)cB_PicDir2.SelectedIndex;
                    Variable.AlmVisionInfo.T2_Linegraydir = (Vision.GrayDir)cB_GrayDir2.SelectedIndex;
                }
                catch (Exception)
                {
                    msgDiv1.MsgDivShow("参数未设置!", 1);
                    args.Cancel = true;
                }
            }
            
            //4-基板-贴附点位
            if (wizardControl1.CurrentStepIndex == 4)
            {
                short PasteCount = (short)(dGV_Program.Rows.Count - 1);
                if (PasteCount == 0)
                {
                    msgDiv1.MsgDivShow("请设置基板贴附参数!", 1);
                    return;
                }
                Variable.AlmBaseInfo.Base_EN_Paste = new bool[PasteCount];
                
                Variable.AlmBaseInfo.Base_PasteXY = new PointF[PasteCount];
                Variable.AlmBaseInfo.Base_PasteR = new double[PasteCount];
                Variable.AlmBaseInfo.Base_PasteZ = new double[2,PasteCount];
                Variable.AlmBaseInfo.Base_Floor = new short[PasteCount];
                Variable.AlmBaseInfo.Base_Feeder = new short[PasteCount];
                Variable.AlmBaseInfo.Base_Nozzle = new short[PasteCount];
                Variable.AlmBaseInfo.Base_Delay = new short[PasteCount];

                Variable.AlmBaseInfo.Base_EN_BadMark = new bool[PasteCount];
                Variable.AlmBaseInfo.Base_CamPoint_BadMark = new PointF[PasteCount];
                Variable.AlmBaseInfo.Base_ColorIsWhite_BadMark = new bool[PasteCount];
                Variable.AlmBaseInfo.Base_ROI_BadMark = new Vision.RectangleRegion[PasteCount];
                Variable.AlmBaseInfo.Base_MinArea_BadMark = new double[PasteCount];
                Variable.AlmBaseInfo.Base_MaxArea_BadMark = new double[PasteCount];
                Variable.AlmBaseInfo.Base_Shutter_BadMark = new short[PasteCount];
                Variable.AlmBaseInfo.Base_LightRedUse_BadMark = new bool[PasteCount];
                Variable.AlmBaseInfo.Base_LightGreenUse_BadMark = new bool[PasteCount];
                Variable.AlmBaseInfo.Base_LightBlueUse_BadMark = new bool[PasteCount];
                Variable.AlmBaseInfo.Base_LightRedValue_BadMark = new short[PasteCount];
                Variable.AlmBaseInfo.Base_LightGreenValue_BadMark = new short[PasteCount];
                Variable.AlmBaseInfo.Base_LightBlueValue_BadMark = new short[PasteCount];
                for (int i = 0; i < PasteCount; i++)
                {
                    Variable.AlmBaseInfo.Base_EN_Paste[i] = dGV_Program.Rows[i].Cells[0].Value.ToString() == "1" ? true : false;
                    Variable.AlmBaseInfo.Base_Floor[i] = short.Parse(dGV_Program.Rows[i].Cells[1].Value.ToString());
                    Variable.AlmBaseInfo.Base_Feeder[i] = short.Parse(dGV_Program.Rows[i].Cells[2].Value.ToString());
                    Variable.AlmBaseInfo.Base_Nozzle[i] = short.Parse(dGV_Program.Rows[i].Cells[3].Value.ToString());
                    Variable.AlmBaseInfo.Base_PasteXY[i] = new PointF();
                    Variable.AlmBaseInfo.Base_PasteXY[i].X = float.Parse(dGV_Program.Rows[i].Cells[4].Value.ToString());
                    Variable.AlmBaseInfo.Base_PasteXY[i].Y = float.Parse(dGV_Program.Rows[i].Cells[5].Value.ToString());
                    Variable.AlmBaseInfo.Base_PasteR[i] = double.Parse(dGV_Program.Rows[i].Cells[6].Value.ToString());
                    Variable.AlmBaseInfo.Base_PasteZ[0,i] = double.Parse(dGV_Program.Rows[i].Cells[7].Value.ToString());
                    Variable.AlmBaseInfo.Base_PasteZ[1,i] = double.Parse(dGV_Program.Rows[i].Cells[8].Value.ToString());
                    Variable.AlmBaseInfo.Base_Delay[i] = short.Parse(dGV_Program.Rows[i].Cells[9].Value.ToString());
                    Variable.AlmBaseInfo.Base_EN_BadMark[i] = dGV_Program.Rows[i].Cells[10].Value.ToString() == "1" ? true : false;
                    Variable.AlmBaseInfo.Base_CamPoint_BadMark[i] = new PointF();
                    Variable.AlmBaseInfo.Base_CamPoint_BadMark[i].X = float.Parse(dGV_Program.Rows[i].Cells[11].Value.ToString());
                    Variable.AlmBaseInfo.Base_CamPoint_BadMark[i].X = float.Parse(dGV_Program.Rows[i].Cells[12].Value.ToString());
                    Variable.AlmBaseInfo.Base_ColorIsWhite_BadMark[i] = dGV_Program.Rows[i].Cells[13].Value.ToString() == "1" ? true : false;
                    Variable.AlmBaseInfo.Base_ROI_BadMark[i] = new Vision.RectangleRegion();
                    Variable.AlmBaseInfo.Base_ROI_BadMark[i].TopLeftX = short.Parse(dGV_Program.Rows[i].Cells[14].Value.ToString());
                    Variable.AlmBaseInfo.Base_ROI_BadMark[i].TopLeftY = short.Parse(dGV_Program.Rows[i].Cells[15].Value.ToString());
                    Variable.AlmBaseInfo.Base_ROI_BadMark[i].Width = short.Parse(dGV_Program.Rows[i].Cells[16].Value.ToString());
                    Variable.AlmBaseInfo.Base_ROI_BadMark[i].Height = short.Parse(dGV_Program.Rows[i].Cells[17].Value.ToString());
                    Variable.AlmBaseInfo.Base_MinArea_BadMark[i] = double.Parse(dGV_Program.Rows[i].Cells[18].Value.ToString());
                    Variable.AlmBaseInfo.Base_MaxArea_BadMark[i] = double.Parse(dGV_Program.Rows[i].Cells[19].Value.ToString());

                    try
                    {
                        Variable.AlmBaseInfo.Base_Shutter_BadMark[i] = short.Parse(dGV_Program.Rows[i].Cells[20].Value.ToString());
                        Variable.AlmBaseInfo.Base_LightRedUse_BadMark[i] = dGV_Program.Rows[i].Cells[21].Value.ToString() == "1" ? true : false;
                        Variable.AlmBaseInfo.Base_LightGreenUse_BadMark[i] = dGV_Program.Rows[i].Cells[22].Value.ToString() == "1" ? true : false;
                        Variable.AlmBaseInfo.Base_LightBlueUse_BadMark[i] = dGV_Program.Rows[i].Cells[23].Value.ToString() == "1" ? true : false;
                        Variable.AlmBaseInfo.Base_LightRedValue_BadMark[i] = short.Parse(dGV_Program.Rows[i].Cells[24].Value.ToString());
                        Variable.AlmBaseInfo.Base_LightGreenValue_BadMark[i] = short.Parse(dGV_Program.Rows[i].Cells[25].Value.ToString());
                        Variable.AlmBaseInfo.Base_LightBlueValue_BadMark[i] = short.Parse(dGV_Program.Rows[i].Cells[26].Value.ToString());

                    }
                    catch (Exception)
                    {
                        Variable.AlmBaseInfo.Base_Shutter_BadMark[i] = 1000;
                        Variable.AlmBaseInfo.Base_LightRedUse_BadMark[i] = false;
                        Variable.AlmBaseInfo.Base_LightGreenUse_BadMark[i] = false;
                        Variable.AlmBaseInfo.Base_LightBlueUse_BadMark[i] = false;
                        Variable.AlmBaseInfo.Base_LightRedValue_BadMark[i] = 0;
                        Variable.AlmBaseInfo.Base_LightGreenValue_BadMark[i] = 0;
                        Variable.AlmBaseInfo.Base_LightBlueValue_BadMark[i] = 0;
                    }
                }
            }
        }

        private void wizardControl1_CancelButtonClick(object sender, EventArgs e)
        {

        }

        private void wizardControl1_FinishButtonClick(object sender, EventArgs e)
        {

        }

        //1-FOV**********************************************
        private void cB_FOV_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cB_AlignStyle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bSetPt1_Click(object sender, EventArgs e)
        {
            tX1.Text = Motion.getXY(1).X.ToString("F3");
            tY1.Text = Motion.getXY(1).Y.ToString("F3");
        }

        private void bSetPt2_Click(object sender, EventArgs e)
        {
            tX2.Text = Motion.getXY(1).X.ToString("F3");
            tY2.Text = Motion.getXY(1).Y.ToString("F3");
        }

        private void bCalAngle_Click(object sender, EventArgs e)
        {
            PointF PT1 = new PointF();
            PointF PT2 = new PointF();
            try
            {
                PT1.X = float.Parse(tX1.Text);
                PT1.Y = float.Parse(tY1.Text);
                PT2.X = float.Parse(tX2.Text);
                PT2.Y = float.Parse(tY2.Text);
                tBaseAngle.Text = Common.MathHelper.GetAngle(PT1, PT2).ToString("F3");
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("校验角度失败，点位未设置!", 1);
            }
        }
        
        //1-特征1**********************************************
        private void bSetCamPoint1_Click(object sender, EventArgs e)
        {
            tCamPointX1.Text = Motion.getXY(1).X.ToString("F3");
            tCamPointY1.Text = Motion.getXY(1).Y.ToString("F3");
        }

        private void bSetShutter1_Click(object sender, EventArgs e)
        {
            if(UpdateImage_Roi())
            {
                tShutter1.Text = shutter.ToString();
            }
            else
            {
                msgDiv1.MsgDivShow("更新曝光值失败!", 1);
            }
        }

        private void bSetLight1_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新光源值失败!", 1);
                return;
            }
            if (cB_VisionType.SelectedIndex == 0 || cB_VisionType.SelectedIndex == 2)
            {
                cB_Light_R1.Checked = bLight_U;
                cB_Light_G1.Checked = false;
                cB_Light_B1.Checked = false;
                tLight_R1.Text = iLight_U.ToString();
                tLight_G1.Text = "0";
                tLight_B1.Text = "0";
            }
            else
            {
                cB_Light_R1.Checked = bLight_RD;
                cB_Light_G1.Checked = bLight_GD;
                cB_Light_B1.Checked = bLight_BD;
                tLight_R1.Text = iLight_RD.ToString();
                tLight_G1.Text = iLight_GD.ToString();
                tLight_B1.Text = iLight_BD.ToString();
            }
        }

        private void bSetProcess1_Click(object sender, EventArgs e)
        {
            short gain = 0;
            short offset = 0;
            try
            {
                gain = short.Parse(tGain1.Text);
                offset = short.Parse(tOffset1.Text);
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("预处理参数输入错误!", 1);
                return;
            }
            if (UpdateImage_Roi())
            {
                try
                {
                    Vision.GainOffset(gain, offset, ref imageSet);
                }
                catch (Exception a)
                {
                    msgDiv1.MsgDivShow("预处理参数输入错误!", 1);
                }
            }
            else
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
            }
        }

        private void bInitLearn1_Click(object sender, EventArgs e)
        {
            if (UpdateImage_Roi())
            {
                HObject con = null;
                Vision.CreatShapeTemplate(imageSet, Hroi, out Variable.AlmVisionInfo.T1_InitModelID, out con);
                con.Dispose();
                msgDiv1.MsgDivShow("成功!", 1);
            }
            else
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
            }
           
        }

        private void bInitDetect1_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            tAreaValue1.Text = "";
            double minScore = 0, minAngle = 0, maxAngle = 0;
            Vision.MatchResults[] temp;
            try
            {
                minScore = double.Parse(tInitScore1.Text);
                minAngle = double.Parse(tInitMinAngle1.Text);
                maxAngle = double.Parse(tInitMaxAngle1.Text);
                Vision.FindShapeTemplate(imageSet, Hroi, Variable.AlmVisionInfo.T1_InitModelID, 1, minScore, minAngle, maxAngle, out temp);
                Variable.AlmVisionInfo.T1_InitROI = new Vision.RectangleRegion(roi.TopLeftX,roi.TopLeftY,roi.Width,roi.Height);
                if (temp != null && temp.Length > 0)
                {
                    Variable.AlmVisionInfo.T1_Init_ResultPoint = new PointF();
                    Variable.AlmVisionInfo.T1_Init_ResultPoint.X = temp[0].XYCoord.X;
                    Variable.AlmVisionInfo.T1_Init_ResultPoint.Y = temp[0].XYCoord.Y;
                    Variable.AlmVisionInfo.T1_Init_Angle = temp[0].Angle;
                    frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, Variable.AlmVisionInfo.T1_Init_ResultPoint, temp[0].Angle);
                }
                else
                {
                    msgDiv1.MsgDivShow("初定位失败!", 1);
                }
                Variable.AlmVisionInfo.T1_InitAreaEN = cB_Area1.Checked;
                bool rtn = double.TryParse(tMinAreaValue1.Text, out Variable.AlmVisionInfo.T1_InitMinArea);
                rtn = rtn && double.TryParse(tMaxAreaValue1.Text, out Variable.AlmVisionInfo.T1_InitMaxArea);
                Variable.AlmVisionInfo.T1_InitAreaIsWhite = rB_White1.Checked;
                if (rtn && Variable.AlmVisionInfo.T1_InitAreaEN)
                {
                    double area;
                    rtn = Vision.AreaCount(imageSet, Hroi,
                        Variable.AlmVisionInfo.T1_InitAreaIsWhite, out area);
                    if (rtn)
                    {
                        if (Variable.AlmVisionInfo.T1_InitMinArea <= area && area <= Variable.AlmVisionInfo.T1_InitMaxArea)
                        {
                            msgDiv1.MsgDivShow("面积侦测OK!", 1);
                        }
                        else
                        {
                            msgDiv1.MsgDivShow("面积侦测Fail!", 1);
                        }
                        tAreaValue1.Text = area.ToString("F0");
                    }
                    else
                    {
                        msgDiv1.MsgDivShow("面积计算失败!", 1);
                    }
                }
                else
                {
                    msgDiv1.MsgDivShow("请正确输入面积设定值!", 1);
                }
            }
            catch (Exception a)
            {
                msgDiv1.MsgDivShow("初定位失败,错误原因:" + a.ToString(), 1);
            }
        }

        private void cB_Algthrim1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cB_Algthrim1.Text == "点-找圆")
            {
                gB_Circle1.Visible = true;
                gB_Pattern1.Visible = false;
                gB_LineCross1.Visible = false;
                gB_Line1.Visible = false;
            }
            if (cB_Algthrim1.Text == "点-匹配")
            {
                gB_Circle1.Visible = false;
                gB_Pattern1.Visible = true;
                gB_LineCross1.Visible = false;
                gB_Line1.Visible = false;
            }
            if (cB_Algthrim1.Text == "点-线+线")
            {
                gB_Circle1.Visible = false;
                gB_Pattern1.Visible = false;
                gB_LineCross1.Visible = true;
                gB_Line1.Visible = false;
            }
            if (cB_Algthrim1.Text == "线")
            {
                gB_Circle1.Visible = false;
                gB_Pattern1.Visible = false;
                gB_LineCross1.Visible = false;
                gB_Line1.Visible = true;
            }
        }

        private void bDetectCircle1_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            double minR = 0, maxR = 0;
            HObject con, cross;
            PointF temp;
            double R = 0;
            bool result = false;
            try
            {
                minR = double.Parse(tMinR1.Text);
                maxR = double.Parse(tMaxR1.Text);
                result = Vision.DetectCircle(imageSet, Hroi, minR, maxR, out temp, out R, out con, out cross);
                Variable.AlmVisionInfo.T1_CircleROI = new Vision.RectangleRegion(roi.TopLeftX,roi.TopLeftY,roi.Width,roi.Height);
                if (result)
                {
                    tR1.Text = R.ToString("F3");
                    Variable.AlmVisionInfo.T1_Circle_ResultPoint = new PointF();
                    Variable.AlmVisionInfo.T1_Circle_ResultPoint.X = temp.X;
                    Variable.AlmVisionInfo.T1_Circle_ResultPoint.Y = temp.Y;
                    frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, temp,0);
                }
                else
                {
                    msgDiv1.MsgDivShow("圆侦测失败!", 1);
                }
            }
            catch (Exception a)
            {
                msgDiv1.MsgDivShow("圆侦测失败!", 1);
            }
        }

        private void bLearn1_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            try
            {
                HObject con = null;
                Vision.CreatShapeTemplate(imageSet, Hroi, out Variable.AlmVisionInfo.T1_PatternModelID, out con);
                con.Dispose();
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("特征学习失败!", 1);
            }
        }

        private void bDetect1_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            double minScore = 0, minAngle = 0, maxAngle = 0;
            Vision.MatchResults[] temp;
            try
            {
                minScore = double.Parse(tScore1.Text);
                minAngle = double.Parse(tMinAngle1.Text);
                maxAngle = double.Parse(tMaxAngle1.Text);
                Vision.FindShapeTemplate(imageSet, Hroi, Variable.AlmVisionInfo.T1_PatternModelID, 1, minScore, minAngle, maxAngle, out temp);
                Variable.AlmVisionInfo.T1_PatternROI = new Vision.RectangleRegion(roi.TopLeftX,roi.TopLeftY,roi.Width,roi.Height);
                if (temp.Length > 0)
                {
                    Variable.AlmVisionInfo.T1_Pattern_ResultPoint = new PointF();
                    Variable.AlmVisionInfo.T1_Pattern_ResultPoint.X = temp[0].XYCoord.X;
                    Variable.AlmVisionInfo.T1_Pattern_ResultPoint.Y = temp[0].XYCoord.Y;
                    Variable.AlmVisionInfo.T1_Pattern_ResultAngle = temp[0].Angle;
                    frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, Variable.AlmVisionInfo.T1_Pattern_ResultPoint, temp[0].Angle);
                }
                else
                {
                    msgDiv1.MsgDivShow("定位失败!", 1);
                }
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("定位失败!", 1);
            }
        }

        private void bDetectCrossA1_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            PointF startPoint;
            PointF endPoint;
            HObject ho_line = null;
            Vision.PicDir picdir;
            Vision.GrayDir graydir;
            picdir = (Vision.PicDir)cB_PicDirCrossA1.SelectedIndex;
            graydir = (Vision.GrayDir)cB_GrayDirCrossA1.SelectedIndex;
            Variable.AlmVisionInfo.T1_CrossLineROI1 = new Vision.RectangleRegion(roi.TopLeftX,roi.TopLeftY,roi.Width,roi.Height);
            bool rtn = Vision.DetectLine(imageSet, Variable.AlmVisionInfo.T1_CrossLineROI1, picdir, graydir, out startPoint, out endPoint, out ho_line);
            if (rtn)
            {
                Variable.AlmVisionInfo.T1_Cross_StartPoint1 = new PointF();
                Variable.AlmVisionInfo.T1_Cross_StartPoint1.X = startPoint.X;
                Variable.AlmVisionInfo.T1_Cross_StartPoint1.Y = startPoint.Y;
                Variable.AlmVisionInfo.T1_Cross_EndPoint1 = new PointF();
                Variable.AlmVisionInfo.T1_Cross_EndPoint1.X = endPoint.X;
                Variable.AlmVisionInfo.T1_Cross_EndPoint1.Y = endPoint.Y;
                frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, Variable.AlmVisionInfo.T1_Cross_StartPoint1
                    , Variable.AlmVisionInfo.T1_Cross_EndPoint1);
                ho_line.Dispose();
            }
            else
            {
                msgDiv1.MsgDivShow("侦测直线失败!", 1);
            }
        }

        private void bDetectCrossB1_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            PointF startPoint;
            PointF endPoint;
            HObject ho_line = null;
            Vision.PicDir picdir;
            Vision.GrayDir graydir;
            picdir = (Vision.PicDir)cB_PicDirCrossB1.SelectedIndex;
            graydir = (Vision.GrayDir)cB_GrayDirCrossB1.SelectedIndex;
            Variable.AlmVisionInfo.T1_CrossLineROI2 = new Vision.RectangleRegion(roi.TopLeftX, roi.TopLeftY, roi.Width, roi.Height);
            bool rtn = Vision.DetectLine(imageSet, Variable.AlmVisionInfo.T1_CrossLineROI2, picdir, graydir, out startPoint, out endPoint, out ho_line);
            if (rtn)
            {
                Variable.AlmVisionInfo.T1_Cross_StartPoint2 = new PointF();
                Variable.AlmVisionInfo.T1_Cross_StartPoint2.X = startPoint.X;
                Variable.AlmVisionInfo.T1_Cross_StartPoint2.Y = startPoint.Y;
                Variable.AlmVisionInfo.T1_Cross_EndPoint2 = new PointF();
                Variable.AlmVisionInfo.T1_Cross_EndPoint2.X = endPoint.X;
                Variable.AlmVisionInfo.T1_Cross_EndPoint2.Y = endPoint.Y;
                frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, Variable.AlmVisionInfo.T1_Cross_StartPoint2
                    , Variable.AlmVisionInfo.T1_Cross_EndPoint2);
                ho_line.Dispose();
            }
            else
            {
                msgDiv1.MsgDivShow("侦测直线失败!", 1);
            }
        }

        private void bDetectLine1_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            PointF startPoint;
            PointF endPoint;
            HObject ho_line = null;
            Vision.PicDir picdir;
            Vision.GrayDir graydir;
            picdir = (Vision.PicDir)cB_PicDir1.SelectedIndex;
            graydir = (Vision.GrayDir)cB_GrayDir1.SelectedIndex;
            Variable.AlmVisionInfo.T1_LineROI =  new Vision.RectangleRegion(roi.TopLeftX, roi.TopLeftY, roi.Width, roi.Height);
            bool rtn = Vision.DetectLine(imageSet, Variable.AlmVisionInfo.T1_LineROI, picdir, graydir, out startPoint, out endPoint, out ho_line);
            if (rtn)
            {
                Variable.AlmVisionInfo.T1_Line_StartPoint = new PointF();
                Variable.AlmVisionInfo.T1_Line_StartPoint.X = startPoint.X;
                Variable.AlmVisionInfo.T1_Line_StartPoint.Y = startPoint.Y;
                Variable.AlmVisionInfo.T1_Line_EndPoint = new PointF();
                Variable.AlmVisionInfo.T1_Line_EndPoint.X = endPoint.X;
                Variable.AlmVisionInfo.T1_Line_EndPoint.Y = endPoint.Y;
                frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, Variable.AlmVisionInfo.T1_Line_StartPoint
                    , Variable.AlmVisionInfo.T1_Line_EndPoint);
                ho_line.Dispose();
            }
            else
            {
                msgDiv1.MsgDivShow("侦测直线失败!", 1);
            }
        }

        //1-特征2**********************************************
        private void bSetCamPoint2_Click(object sender, EventArgs e)
        {
            tCamPointX2.Text = Motion.getXY(1).X.ToString("F3");
            tCamPointY2.Text = Motion.getXY(1).Y.ToString("F3");
        }

        private void bSetShutter2_Click(object sender, EventArgs e)
        {
            if (UpdateImage_Roi())
            {
                tShutter2.Text = shutter.ToString();
            }
            else
            {
                msgDiv1.MsgDivShow("更新曝光值失败!", 1);
            }
        }

        private void bSetLight2_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新光源值失败!", 1);
                return;
            }
            if (cB_VisionType.SelectedIndex == 0 || cB_VisionType.SelectedIndex == 2)
            {
                cB_Light_R2.Checked = bLight_U;
                cB_Light_G2.Checked = false;
                cB_Light_B2.Checked = false;
                tLight_R2.Text = iLight_U.ToString();
                tLight_G2.Text = "0";
                tLight_B2.Text = "0";
            }
            else
            {
                cB_Light_R2.Checked = bLight_RD;
                cB_Light_G2.Checked = bLight_GD;
                cB_Light_B2.Checked = bLight_BD;
                tLight_R2.Text = iLight_RD.ToString();
                tLight_G2.Text = iLight_GD.ToString();
                tLight_B2.Text = iLight_BD.ToString();
            }
        }

        private void bSetProcess2_Click(object sender, EventArgs e)
        {
            short gain = 0;
            short offset = 0;
            try
            {
                gain = short.Parse(tGain2.Text);
                offset = short.Parse(tOffset2.Text);
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("预处理参数输入错误!", 1);
                return;
            }
            if (UpdateImage_Roi())
            {
                try
                {
                    Vision.GainOffset(gain, offset, ref imageSet);
                }
                catch (Exception a)
                {
                    msgDiv1.MsgDivShow("预处理参数输入错误!", 1);
                }
            }
            else
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
            }
        }

        private void bInitLearn2_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            try
            {
                HObject con = null;
                Vision.CreatShapeTemplate(imageSet, Hroi, out Variable.AlmVisionInfo.T2_PatternModelID, out con);
                con.Dispose();
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("特征学习失败!", 1);
            }
        }

        private void bInitDetect2_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            double minScore = 0, minAngle = 0, maxAngle = 0;
            Vision.MatchResults[] temp;
            try
            {
                minScore = double.Parse(tScore2.Text);
                minAngle = double.Parse(tMinAngle2.Text);
                maxAngle = double.Parse(tMaxAngle2.Text);
                Vision.FindShapeTemplate(imageSet, Hroi, Variable.AlmVisionInfo.T2_PatternModelID, 1, minScore, minAngle, maxAngle, out temp);
                Variable.AlmVisionInfo.T2_PatternROI = new Vision.RectangleRegion(roi.TopLeftX, roi.TopLeftY, roi.Width, roi.Height);
                if (temp.Length > 0)
                {
                    Variable.AlmVisionInfo.T2_Pattern_ResultPoint = new PointF();
                    Variable.AlmVisionInfo.T2_Pattern_ResultPoint.X = temp[0].XYCoord.X;
                    Variable.AlmVisionInfo.T2_Pattern_ResultPoint.Y = temp[0].XYCoord.Y;
                    Variable.AlmVisionInfo.T2_Pattern_ResultAngle = temp[0].Angle;
                    frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, Variable.AlmVisionInfo.T2_Pattern_ResultPoint, temp[0].Angle);
                }
                else
                {
                    msgDiv1.MsgDivShow("定位失败!", 1);
                }
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("定位失败!", 1);
            }
        }

        private void cB_Algthrim2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cB_Algthrim2.Text == "点-找圆")
            {
                gB_Circle2.Visible = true;
                gB_Pattern2.Visible = false;
                gB_LineCross2.Visible = false;
                gB_Line2.Visible = false;
            }
            if (cB_Algthrim2.Text == "点-匹配")
            {
                gB_Circle2.Visible = false;
                gB_Pattern2.Visible = true;
                gB_LineCross2.Visible = false;
                gB_Line2.Visible = false;
            }
            if (cB_Algthrim2.Text == "点-线+线")
            {
                gB_Circle2.Visible = false;
                gB_Pattern2.Visible = false;
                gB_LineCross2.Visible = true;
                gB_Line2.Visible = false;
            }
            if (cB_Algthrim2.Text == "线")
            {
                gB_Circle2.Visible = false;
                gB_Pattern2.Visible = false;
                gB_LineCross2.Visible = false;
                gB_Line2.Visible = true;
            }
        }

        private void bDetectCircle2_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            double minR = 0, maxR = 0;
            HObject con, cross;
            PointF temp;
            double R = 0;
            bool result = false;
            try
            {
                minR = double.Parse(tMinR2.Text);
                maxR = double.Parse(tMaxR2.Text);
                result = Vision.DetectCircle(imageSet, Hroi, minR, maxR, out temp, out R, out con, out cross);
                Variable.AlmVisionInfo.T2_CircleROI = new Vision.RectangleRegion(roi.TopLeftX, roi.TopLeftY, roi.Width, roi.Height);
                if (result)
                {
                    tR1.Text = R.ToString("F3");
                    Variable.AlmVisionInfo.T2_Circle_ResultPoint = new PointF();
                    Variable.AlmVisionInfo.T2_Circle_ResultPoint.X = temp.X;
                    Variable.AlmVisionInfo.T2_Circle_ResultPoint.Y = temp.Y;
                    frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, temp, 0);
                }
                else
                {
                    msgDiv1.MsgDivShow("圆侦测失败!", 1);
                }
            }
            catch (Exception a)
            {
                msgDiv1.MsgDivShow("圆侦测失败!", 1);
            }
        }

        private void bLearn2_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            try
            {
                HObject con = null;
                Vision.CreatShapeTemplate(imageSet, Hroi, out Variable.AlmVisionInfo.T2_PatternModelID, out con);
                con.Dispose();
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("特征学习失败!", 1);
            }
        }

        private void bDetect2_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            double minScore = 0, minAngle = 0, maxAngle = 0;
            Vision.MatchResults[] temp;
            try
            {
                minScore = double.Parse(tScore2.Text);
                minAngle = double.Parse(tMinAngle2.Text);
                maxAngle = double.Parse(tMaxAngle2.Text);
                Vision.FindShapeTemplate(imageSet, Hroi, Variable.AlmVisionInfo.T2_PatternModelID, 1, minScore, minAngle, maxAngle, out temp);
                Variable.AlmVisionInfo.T2_PatternROI = new Vision.RectangleRegion(roi.TopLeftX, roi.TopLeftY, roi.Width, roi.Height);
                if (temp.Length > 0)
                {
                    Variable.AlmVisionInfo.T2_Pattern_ResultPoint = new PointF();
                    Variable.AlmVisionInfo.T2_Pattern_ResultPoint.X = temp[0].XYCoord.X;
                    Variable.AlmVisionInfo.T2_Pattern_ResultPoint.Y = temp[0].XYCoord.Y;
                    Variable.AlmVisionInfo.T2_Pattern_ResultAngle = temp[0].Angle;
                    frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, Variable.AlmVisionInfo.T2_Pattern_ResultPoint, temp[0].Angle);
                }
                else
                {
                    msgDiv1.MsgDivShow("定位失败!", 1);
                }
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("定位失败!", 1);
            }
        }

        private void bDetectCrossA2_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            PointF startPoint;
            PointF endPoint;
            HObject ho_line = null;
            Vision.PicDir picdir;
            Vision.GrayDir graydir;
            picdir = (Vision.PicDir)cB_PicDirCrossA2.SelectedIndex;
            graydir = (Vision.GrayDir)cB_GrayDirCrossA2.SelectedIndex;
            Variable.AlmVisionInfo.T2_CrossLineROI1 = new Vision.RectangleRegion(roi.TopLeftX, roi.TopLeftY, roi.Width, roi.Height);
            bool rtn = Vision.DetectLine(imageSet, Variable.AlmVisionInfo.T1_CrossLineROI1, picdir, graydir, out startPoint, out endPoint, out ho_line);
            if (rtn)
            {
                Variable.AlmVisionInfo.T2_Cross_StartPoint1 = new PointF();
                Variable.AlmVisionInfo.T2_Cross_StartPoint1.X = startPoint.X;
                Variable.AlmVisionInfo.T2_Cross_StartPoint1.Y = startPoint.Y;
                Variable.AlmVisionInfo.T2_Cross_EndPoint1 = new PointF();
                Variable.AlmVisionInfo.T2_Cross_EndPoint1.X = endPoint.X;
                Variable.AlmVisionInfo.T2_Cross_EndPoint1.Y = endPoint.Y;
                frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, Variable.AlmVisionInfo.T2_Cross_StartPoint1
                    , Variable.AlmVisionInfo.T2_Cross_EndPoint1);
                ho_line.Dispose();
            }
            else
            {
                msgDiv1.MsgDivShow("侦测直线失败!", 1);
            }
        }

        private void bDetectCrossB2_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            PointF startPoint;
            PointF endPoint;
            HObject ho_line = null;
            Vision.PicDir picdir;
            Vision.GrayDir graydir;
            picdir = (Vision.PicDir)cB_PicDirCrossB2.SelectedIndex;
            graydir = (Vision.GrayDir)cB_GrayDirCrossB2.SelectedIndex;
            Variable.AlmVisionInfo.T2_CrossLineROI2 = new Vision.RectangleRegion(roi.TopLeftX, roi.TopLeftY, roi.Width, roi.Height);
            bool rtn = Vision.DetectLine(imageSet, Variable.AlmVisionInfo.T2_CrossLineROI2, picdir, graydir, out startPoint, out endPoint, out ho_line);
            if (rtn)
            {
                Variable.AlmVisionInfo.T2_Cross_StartPoint2 = new PointF();
                Variable.AlmVisionInfo.T2_Cross_StartPoint2.X = startPoint.X;
                Variable.AlmVisionInfo.T2_Cross_StartPoint2.Y = startPoint.Y;
                Variable.AlmVisionInfo.T2_Cross_EndPoint2 = new PointF();
                Variable.AlmVisionInfo.T2_Cross_EndPoint2.X = endPoint.X;
                Variable.AlmVisionInfo.T2_Cross_EndPoint2.Y = endPoint.Y;
                frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, Variable.AlmVisionInfo.T2_Cross_StartPoint2
                    , Variable.AlmVisionInfo.T2_Cross_EndPoint2);
                ho_line.Dispose();
            }
            else
            {
                msgDiv1.MsgDivShow("侦测直线失败!", 1);
            }
        }

        private void bDetectLine2_Click(object sender, EventArgs e)
        {
            if (!UpdateImage_Roi())
            {
                msgDiv1.MsgDivShow("更新图像失败!", 1);
                return;
            }
            PointF startPoint;
            PointF endPoint;
            HObject ho_line = null;
            Vision.PicDir picdir;
            Vision.GrayDir graydir;
            picdir = (Vision.PicDir)cB_PicDir2.SelectedIndex;
            graydir = (Vision.GrayDir)cB_GrayDir2.SelectedIndex;
            Variable.AlmVisionInfo.T2_LineROI = new Vision.RectangleRegion(roi.TopLeftX, roi.TopLeftY, roi.Width, roi.Height);
            bool rtn = Vision.DetectLine(imageSet, Variable.AlmVisionInfo.T2_LineROI, picdir, graydir, out startPoint, out endPoint, out ho_line);
            if (rtn)
            {
                Variable.AlmVisionInfo.T2_Line_StartPoint = new PointF();
                Variable.AlmVisionInfo.T2_Line_StartPoint.X = startPoint.X;
                Variable.AlmVisionInfo.T2_Line_StartPoint.Y = startPoint.Y;
                Variable.AlmVisionInfo.T2_Line_EndPoint = new PointF();
                Variable.AlmVisionInfo.T2_Line_EndPoint.X = endPoint.X;
                Variable.AlmVisionInfo.T2_Line_EndPoint.Y = endPoint.Y;
                frm_Main.ShowResult(frm_Main.frm_Camera.imageSet, Variable.AlmVisionInfo.T2_Line_StartPoint
                    , Variable.AlmVisionInfo.T2_Line_EndPoint);
                ho_line.Dispose();
            }
            else
            {
                msgDiv1.MsgDivShow("侦测直线失败!", 1);
            }
        }
        
        //1-基板贴附**********************************************
        private void bAdd_Click(object sender, EventArgs e)
        {
            //获取当前选定的Index
            DataGridViewRow item;
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.Rows.Count - 1))
            {
                dGV_Program.Rows.Insert(dGV_Program.SelectedRows[0].Index + 1, 1);
                Common.CommonHelper.AddRowHeader(dGV_Program);
                item = dGV_Program.Rows[dGV_Program.SelectedRows[0].Index + 1];
            }
            else
            {
                dGV_Program.Rows.Insert(0, 1);
                Common.CommonHelper.AddRowHeader(dGV_Program);
                item = dGV_Program.Rows[0];
            }
            item.Cells[0].Value = "0";
            item.Cells[1].Value = "0";
            item.Cells[2].Value = "0";
            item.Cells[3].Value = "0";
            item.Cells[4].Value = "0";
            item.Cells[5].Value = "0";
            item.Cells[6].Value = "1";
            item.Cells[7].Value = "1";
            item.Cells[8].Value = "1";
            item.Cells[9].Value = "1";
            item.Cells[10].Value = "10";
            item.Cells[11].Value = "1";
            item.Cells[12].Value = "0";
            item.Cells[13].Value = "0";
            item.Cells[14].Value = "0";
            item.Cells[15].Value = "0";
            item.Cells[16].Value = "0";
            item.Cells[17].Value = "0";
            item.Cells[18].Value = "0";
            item.Cells[19].Value = "0";
            item.Cells[20].Value = "0";
            item.Cells[21].Value = "1000";
            item.Cells[22].Value = "0";
            item.Cells[23].Value = "0";
            item.Cells[24].Value = "0";
            item.Cells[25].Value = "0";
            item.Cells[26].Value = "0";
            item.Cells[27].Value = "0";
        }

        private void bDel_Click(object sender, EventArgs e)
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

        private void bMoveUp_Click(object sender, EventArgs e)
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

        private void bMoveDown_Click(object sender, EventArgs e)
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

        private void bExpandPara_Click(object sender, EventArgs e)
        {

        }

        private void bExpand_Click(object sender, EventArgs e)
        {

        }







    }
}
