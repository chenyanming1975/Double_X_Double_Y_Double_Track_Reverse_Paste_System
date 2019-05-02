using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Definition;
using System.Diagnostics;
using General;
using GeneralMachine.Common;
using NationalInstruments.Vision;
using NationalInstruments.Vision.Acquisition.Imaqdx;
using NationalInstruments.Vision.Analysis;
using HalconDotNet;

namespace GeneralMachine
{
    public partial class frm_Wizard_Program : Form
    {
        public frm_Main frm_Main = null;
        public string ProgramName = "";

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
            this.cbModuleMark.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateList(true);
        }


        public void CreateEmptyPro()
        {
            this.Tag = true;
            //UI 清除
            Common.CommonHelper.DelDGV(dGV_Feeder);
            tReachIndex.Text = "";
            cB_UpVisionID.Items.Clear();
            cB_UpVisionID.Text = "";
            cB_DownVisionID.Items.Clear();
            cB_DownVisionID.Text = "";
            tSuckRotate.Text = "";
            tCamRotate.Text = "";
            Common.CommonHelper.DelDGV(dGV_Program);

            Directory.CreateDirectory(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName);
            Directory.CreateDirectory(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName + "\\Vision_UpLabel");
            Directory.CreateDirectory(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName + "\\Vision_DownLabel");
            Directory.CreateDirectory(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName + "\\Vision_UpPaste");
            Directory.CreateDirectory(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName + "\\Vision_BadMark");

            Variable.AlmProgram = new ALMPro();
            Variable.AlmProgram.ALMProName = frm_Main.frm_Program.ProgramName;
            Variable.AlmProgram.WidePosition = double.Parse(tWideValue.Text);
            Variable.AlmProgram.FeederInfo_Module1 = new Common.SerializableDictionary<int, FeederInfo>();
            Variable.AlmProgram.FeederInfo_Module2 = new Common.SerializableDictionary<int, FeederInfo>();
            Variable.AlmProgram.FeederInfo_Module1.Add(0, new FeederInfo());
            Variable.AlmProgram.FeederInfo_Module1.Add(1, new FeederInfo());
            Variable.AlmProgram.FeederInfo_Module1.Add(2, new FeederInfo());
            Variable.AlmProgram.FeederInfo_Module1.Add(3, new FeederInfo());
            Variable.AlmProgram.FeederInfo_Module2.Add(0, new FeederInfo());
            Variable.AlmProgram.FeederInfo_Module2.Add(1, new FeederInfo());
            Variable.AlmProgram.FeederInfo_Module2.Add(2, new FeederInfo());
            Variable.AlmProgram.FeederInfo_Module2.Add(3, new FeederInfo());

            Variable.AlmProgram.VisionLib_DownLabel_List = new List<VisionInfo>();
            Variable.AlmProgram.VisionLib_UpLabel_List = new List<VisionInfo>();
            Variable.AlmProgram.VisionLib_UpPaste_List = new List<VisionInfo>();
            Variable.AlmProgram.VisionLib_BadMark_List = new List<VisionInfo>();

            Variable.AlmProgram.MutiBaseInfo = new MutiBaseInfo();
            Variable.AlmProgram.BadMarkInfo = new BadMarkInfo();
            Variable.AlmProgram.ReadCodeInfo = new ReadCodeInfo();


            Variable.AlmProgram.DownCapture_Module1.NozzlePoint = new PointF[4];
            Variable.AlmProgram.DownCapture_Module1.NozzleCaptureCount = new short[2];
            Variable.AlmProgram.DownCapture_Module1.NozzleBaseIndex = new short[2];

            Variable.AlmProgram.DownCapture_Module2.NozzlePoint = new PointF[4];
            Variable.AlmProgram.DownCapture_Module2.NozzleCaptureCount = new short[2];
            Variable.AlmProgram.DownCapture_Module2.NozzleBaseIndex = new short[2];
        }


        /// <summary>
        /// Next 按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void wizardControl1_NextButtonClick(WizardBase.WizardControl sender, WizardBase.WizardNextButtonClickEventArgs args)
        {
            //welcome
            #region welcome
            if (wizardControl1.CurrentStepIndex == 0)
            {
                if (frm_Main.frm_Program.ProgramName == "")
                {
                    args.Cancel = true;
                    frm_Main.ShowMessage("信息:" + "程式名称为空");
                    return;
                }
                if (tWideValue.Text == "")
                {
                    args.Cancel = true;
                    frm_Main.ShowMessage("信息:" + "轨道调宽未设置");
                    return;
                }

                if (!frm_Main.Init_LoadProgram)
                {
                    #region Creat New Pro
                    if (Directory.Exists(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName))
                    {
                        DialogResult aa = MessageBox.Show("程式已经存在,是否覆盖?", "提示", MessageBoxButtons.OKCancel);
                        if (aa == DialogResult.OK)
                        {
                            CreateEmptyPro();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName);
                        CreateEmptyPro();
                    }
                    #endregion
                }
                else//
                {


                }
            }
            #endregion

            #region 视觉程序窗口
            if (wizardControl1.CurrentStepIndex == 1)
            {


            }
            #endregion

            #region 吸取位置
            if (wizardControl1.CurrentStepIndex == 2)
            {
                rB_Module1.Checked = true;
                DownCamPoint_Module = 1;
                rB_Module1.BackColor = Color.CornflowerBlue;
                rB_Module2.BackColor = Color.Transparent;
                updateUI(DownCamPoint_Module);
            }
            #endregion

            #region 下视觉拍照点位
            if (wizardControl1.CurrentStepIndex == 3)
            {
                CommonHelper.DelDGV(this.dGV_Program);
                if (Variable.AlmProgram.MutiBaseInfo.VisionProName_Base == null || Variable.AlmProgram.MutiBaseInfo.VisionProName_Base.Count == 0)
                    return;
                dGV_Program.Rows.Add(Variable.AlmProgram.MutiBaseInfo.VisionProName_Base.Count);

                for (int i = 0; i < Variable.AlmProgram.MutiBaseInfo.VisionProName_Base.Count; ++i)
                {
                    this.dGV_Program.Rows[i].Cells[0].Value = Variable.AlmProgram.MutiBaseInfo.VisionProName_Base[i];
                    this.dGV_Program.Rows[i].Cells[1].Value = Variable.AlmProgram.MutiBaseInfo.FOVCount_Base[i];
                    this.dGV_Program.Rows[i].Cells[2].Value = Variable.AlmProgram.MutiBaseInfo.Cam_Mark1Point[i].X.ToString("f3");
                    this.dGV_Program.Rows[i].Cells[3].Value = Variable.AlmProgram.MutiBaseInfo.Cam_Mark1Point[i].Y.ToString("f3");
                    this.dGV_Program.Rows[i].Cells[4].Value = Variable.AlmProgram.MutiBaseInfo.Cam_Mark2Point[i].X.ToString("f3");
                    this.dGV_Program.Rows[i].Cells[5].Value = Variable.AlmProgram.MutiBaseInfo.Cam_Mark2Point[i].Y.ToString("f3");
                    this.dGV_Program.Rows[i].Cells[6].Value = Variable.AlmProgram.MutiBaseInfo.OffsetFly[i];

                    this.dGV_Program.Rows[i].Cells[7].Value = Variable.AlmProgram.MutiBaseInfo.OffsetX[0][i];
                    this.dGV_Program.Rows[i].Cells[8].Value = Variable.AlmProgram.MutiBaseInfo.OffsetY[0][i];
                    this.dGV_Program.Rows[i].Cells[9].Value = Variable.AlmProgram.MutiBaseInfo.OffsetR[0][i];
                    this.dGV_Program.Rows[i].Cells[10].Value = Variable.AlmProgram.MutiBaseInfo.OffsetX[1][i];
                    this.dGV_Program.Rows[i].Cells[11].Value =Variable.AlmProgram.MutiBaseInfo.OffsetY[1][i];
                    this.dGV_Program.Rows[i].Cells[12].Value = Variable.AlmProgram.MutiBaseInfo.OffsetR[1][i];
                }
                CommonHelper.AddRowHeader(this.dGV_Program);
            }
            #endregion

            #region 拼版模式
            if (wizardControl1.CurrentStepIndex == 4)
            {
                #region 拼板 信息储存
                Variable.AlmProgram.MutiBaseInfo.PCB_Now = new PointF();
                Variable.AlmProgram.MutiBaseInfo.PCB_Origin = new PointF();
                Variable.AlmProgram.MutiBaseInfo.VisionProName_Base = new List<string>();
                Variable.AlmProgram.MutiBaseInfo.Cam_Mark1Point = new List<PointF>();
                Variable.AlmProgram.MutiBaseInfo.Cam_Mark2Point = new List<PointF>();
                Variable.AlmProgram.MutiBaseInfo.FOVCount_Base = new List<short>();
                Variable.AlmProgram.MutiBaseInfo.OffsetX = new Common.SerializableDictionary<int, List<double>>();
                Variable.AlmProgram.MutiBaseInfo.OffsetX.Add(0, new List<double>());
                Variable.AlmProgram.MutiBaseInfo.OffsetX.Add(1, new List<double>());

                Variable.AlmProgram.MutiBaseInfo.OffsetY = new Common.SerializableDictionary<int, List<double>>();
                Variable.AlmProgram.MutiBaseInfo.OffsetY.Add(0, new List<double>());
                Variable.AlmProgram.MutiBaseInfo.OffsetY.Add(1, new List<double>());

                Variable.AlmProgram.MutiBaseInfo.OffsetR = new Common.SerializableDictionary<int, List<double>>();
                Variable.AlmProgram.MutiBaseInfo.OffsetR.Add(0, new List<double>());
                Variable.AlmProgram.MutiBaseInfo.OffsetR.Add(1, new List<double>());

                Variable.AlmProgram.MutiBaseInfo.OffsetFly = new List<double>();

                //赋值
                Variable.AlmProgram.MutiBaseInfo.PCB_Now.X = float.Parse(tNowOriginX.Text);
                Variable.AlmProgram.MutiBaseInfo.PCB_Now.Y = float.Parse(tNowOriginY.Text);
                Variable.AlmProgram.MutiBaseInfo.PCB_Origin.X = float.Parse(tProgramOriginX.Text);
                Variable.AlmProgram.MutiBaseInfo.PCB_Origin.Y = float.Parse(tProgramOriginY.Text);
                for (int i = 0; i < dGV_Program.Rows.Count - 1; i++)
                {
                    Variable.AlmProgram.MutiBaseInfo.VisionProName_Base.Add(dGV_Program.Rows[i].Cells[0].Value.ToString());
                    PointF temp = new PointF();
                    temp.X = float.Parse(dGV_Program.Rows[i].Cells[2].Value.ToString());
                    temp.Y = float.Parse(dGV_Program.Rows[i].Cells[3].Value.ToString());
                    Variable.AlmProgram.MutiBaseInfo.Cam_Mark1Point.Add(temp);
                    temp = new PointF();
                    temp.X = float.Parse(dGV_Program.Rows[i].Cells[4].Value.ToString()); ;
                    temp.Y = float.Parse(dGV_Program.Rows[i].Cells[5].Value.ToString()); ;
                    Variable.AlmProgram.MutiBaseInfo.Cam_Mark2Point.Add(temp);
                    Variable.AlmProgram.MutiBaseInfo.FOVCount_Base.Add(short.Parse(dGV_Program.Rows[i].Cells[1].Value.ToString()));

                    if (dGV_Program.Rows[i].Cells[6].Value == null)
                        Variable.AlmProgram.MutiBaseInfo.OffsetFly.Add(0);
                    else
                        Variable.AlmProgram.MutiBaseInfo.OffsetFly.Add(double.Parse(dGV_Program.Rows[i].Cells[6].Value.ToString()));

                    Variable.AlmProgram.MutiBaseInfo.OffsetX[0].Add(double.Parse(dGV_Program.Rows[i].Cells[7].Value.ToString()));
                    Variable.AlmProgram.MutiBaseInfo.OffsetY[0].Add(double.Parse(dGV_Program.Rows[i].Cells[8].Value.ToString()));
                    Variable.AlmProgram.MutiBaseInfo.OffsetR[0].Add(double.Parse(dGV_Program.Rows[i].Cells[9].Value.ToString()));
                    Variable.AlmProgram.MutiBaseInfo.OffsetX[1].Add(double.Parse(dGV_Program.Rows[i].Cells[10].Value.ToString()));
                    Variable.AlmProgram.MutiBaseInfo.OffsetY[1].Add(double.Parse(dGV_Program.Rows[i].Cells[11].Value.ToString()));
                    Variable.AlmProgram.MutiBaseInfo.OffsetR[1].Add(double.Parse(dGV_Program.Rows[i].Cells[12].Value.ToString()));
                }
                #endregion

                #region Badmark 信息读取
                this.cbBadmarkModule.SelectedIndex = Variable.AlmProgram.BadMarkInfo.CaptureModule;
                this.bEnableBadmark.Checked = Variable.AlmProgram.BadMarkInfo.EnableBadMark;
                this.cbBadMark.Items.Clear();
                DirectoryInfo a = new DirectoryInfo(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName + "\\Vision_BadMark\\");
                var info = a.GetDirectories();

                foreach(var name in info)
                {
                    this.cbBadMark.Items.Add(name.Name);
                }

                CommonHelper.DelDGV(this.dGV_Badmark);
                if (Variable.AlmProgram.BadMarkInfo.VisionName.Count == 0) return;
                dGV_Badmark.Rows.Add(Variable.AlmProgram.BadMarkInfo.VisionName.Count);

                for (int i = 0; i < Variable.AlmProgram.BadMarkInfo.VisionName.Count; ++i)
                {
                    this.dGV_Badmark.Rows[i].Cells[0].Value = Variable.AlmProgram.BadMarkInfo.VisionName[i];
                    this.dGV_Badmark.Rows[i].Cells[1].Value = Variable.AlmProgram.BadMarkInfo.CamPos[i].X.ToString("F3");
                    this.dGV_Badmark.Rows[i].Cells[2].Value = Variable.AlmProgram.BadMarkInfo.CamPos[i].Y.ToString("F3");
                    this.dGV_Badmark.Rows[i].Cells[3].Value = Variable.AlmProgram.BadMarkInfo.FlyOffset[i].ToString("f3");
                    this.dGV_Badmark.Rows[i].Cells[4].Value = Variable.AlmProgram.BadMarkInfo.BindModule1Paste[i].ToString();
                    this.dGV_Badmark.Rows[i].Cells[5].Value = Variable.AlmProgram.BadMarkInfo.BindModule2Paste[i].ToString();
                }
                CommonHelper.AddRowHeader(this.dGV_Badmark);
                #endregion
            }
            #endregion

            #region Badmark 界面
            if (wizardControl1.CurrentStepIndex == 5)
            {
                #region Badmark 信息储存
                Variable.AlmProgram.BadMarkInfo.CaptureModule = this.cbBadmarkModule.SelectedIndex;
                Variable.AlmProgram.BadMarkInfo.EnableBadMark = this.bEnableBadmark.Checked;
                Variable.AlmProgram.BadMarkInfo.VisionName = new List<string>();
                Variable.AlmProgram.BadMarkInfo.CamPos = new List<PointF>();
                Variable.AlmProgram.BadMarkInfo.BindModule1Paste = new List<int>();
                Variable.AlmProgram.BadMarkInfo.BindModule2Paste = new List<int>();
                Variable.AlmProgram.BadMarkInfo.FlyOffset = new List<double>();

                for (int i = 0; i < dGV_Badmark.Rows.Count - 1; i++)
                {
                    Variable.AlmProgram.BadMarkInfo.VisionName.Add(dGV_Badmark.Rows[i].Cells[0].Value.ToString());
                    PointF temp = new PointF();
                    temp.X = float.Parse(dGV_Badmark.Rows[i].Cells[1].Value.ToString());
                    temp.Y = float.Parse(dGV_Badmark.Rows[i].Cells[2].Value.ToString());
                    Variable.AlmProgram.BadMarkInfo.CamPos.Add(temp);
                    Variable.AlmProgram.BadMarkInfo.FlyOffset.Add(double.Parse(dGV_Badmark.Rows[i].Cells[3].Value.ToString()));
                    Variable.AlmProgram.BadMarkInfo.BindModule1Paste.Add(int.Parse(dGV_Badmark.Rows[i].Cells[4].Value.ToString()));
                    Variable.AlmProgram.BadMarkInfo.BindModule2Paste.Add(int.Parse(dGV_Badmark.Rows[i].Cells[5].Value.ToString()));
                }
                #endregion

                #region Barcode 界面读取
                this.cbReadCodeModule.SelectedIndex = Variable.AlmProgram.ReadCodeInfo.CaptureModule;
                this.bEnableReadCode.Checked = Variable.AlmProgram.ReadCodeInfo.EnableReadCode;
                CommonHelper.DelDGV(this.dGV_Barcode);
                if (Variable.AlmProgram.ReadCodeInfo.CodeType.Count == 0) return;

                dGV_Barcode.Rows.Add(Variable.AlmProgram.ReadCodeInfo.CodeType.Count);

                for (int i = 0; i < Variable.AlmProgram.ReadCodeInfo.CodeType.Count; ++i)
                {
                    this.dGV_Barcode.Rows[i].Cells[0].Value = Variable.AlmProgram.ReadCodeInfo.CodeType[i];
                    this.dGV_Barcode.Rows[i].Cells[1].Value = Variable.AlmProgram.ReadCodeInfo.CamPos[i].X.ToString("F3");
                    this.dGV_Barcode.Rows[i].Cells[2].Value = Variable.AlmProgram.ReadCodeInfo.CamPos[i].Y.ToString("F3");
                    this.dGV_Barcode.Rows[i].Cells[3].Value = Variable.AlmProgram.ReadCodeInfo.FlyOffset[i].ToString("f3");
                    this.dGV_Barcode.Rows[i].Cells[4].Value = Variable.AlmProgram.ReadCodeInfo.BindModule1Paste[i].ToString();
                    this.dGV_Barcode.Rows[i].Cells[5].Value = Variable.AlmProgram.ReadCodeInfo.BindModule2Paste[i].ToString();
                    this.dGV_Barcode.Rows[i].Cells[6].Value = Variable.AlmProgram.ReadCodeInfo.Gain[i].ToString();
                    this.dGV_Barcode.Rows[i].Cells[7].Value = Variable.AlmProgram.ReadCodeInfo.Offset[i].ToString();
                }
                CommonHelper.AddRowHeader(this.dGV_Barcode);
                #endregion
            }

            #endregion

            #region Barcode 界面
            if (wizardControl1.CurrentStepIndex == 6)
            {
                Variable.AlmProgram.ReadCodeInfo.CaptureModule = this.cbBadmarkModule.SelectedIndex;
                Variable.AlmProgram.ReadCodeInfo.EnableReadCode = this.bEnableReadCode.Checked;
                Variable.AlmProgram.ReadCodeInfo.CodeType = new List<string>();
                Variable.AlmProgram.ReadCodeInfo.CamPos = new List<PointF>();
                Variable.AlmProgram.ReadCodeInfo.BindModule1Paste = new List<int>();
                Variable.AlmProgram.ReadCodeInfo.BindModule2Paste = new List<int>();
                Variable.AlmProgram.ReadCodeInfo.FlyOffset = new List<double>();
                Variable.AlmProgram.ReadCodeInfo.Gain = new List<int>();
                Variable.AlmProgram.ReadCodeInfo.Offset = new List<int>();

                for (int i = 0; i < dGV_Barcode.Rows.Count - 1; i++)
                {
                    Variable.AlmProgram.ReadCodeInfo.CodeType.Add(dGV_Barcode.Rows[i].Cells[0].Value.ToString());
                    PointF temp = new PointF();
                    temp.X = float.Parse(dGV_Barcode.Rows[i].Cells[1].Value.ToString());
                    temp.Y = float.Parse(dGV_Barcode.Rows[i].Cells[2].Value.ToString());
                    Variable.AlmProgram.ReadCodeInfo.CamPos.Add(temp);
                    Variable.AlmProgram.ReadCodeInfo.FlyOffset.Add(double.Parse(dGV_Barcode.Rows[i].Cells[3].Value.ToString()));
                    Variable.AlmProgram.ReadCodeInfo.BindModule1Paste.Add(int.Parse(dGV_Barcode.Rows[i].Cells[4].Value.ToString()));
                    Variable.AlmProgram.ReadCodeInfo.BindModule2Paste.Add(int.Parse(dGV_Barcode.Rows[i].Cells[5].Value.ToString()));
                    Variable.AlmProgram.ReadCodeInfo.Gain.Add(int.Parse(dGV_Barcode.Rows[i].Cells[6].Value.ToString()));
                    Variable.AlmProgram.ReadCodeInfo.Offset.Add(int.Parse(dGV_Barcode.Rows[i].Cells[7].Value.ToString()));
                }
            }
            #endregion
        }

        /// <summary>
        /// Finish 按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wizardControl1_FinishButtonClick(object sender, EventArgs e)
        {
            Variable.SaveAlmPro(frm_Main.frm_Program.ProgramName, ref Variable.AlmProgram);
            this.wizardControl1.CurrentStepIndex = 1;
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
            tWideValue.Text = Variable.Wide.Pos.ToString("F3");
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
                Variable.Wide.GoPos(p, Variable.VelMode_RunMode_Wide);
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
            if (frm_Main.frm_Program.ProgramName == null || frm_Main.frm_Program.ProgramName == "" || !Directory.Exists(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName))
            {
                return;
            }
            if (cB_VisionType.SelectedIndex == 0)
            {
                a = new DirectoryInfo(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName + "\\Vision_UpLabel\\");
                info = a.GetDirectories();
            }
            if (cB_VisionType.SelectedIndex == 1)
            {

                a = new DirectoryInfo(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName + "\\Vision_DownLabel\\");
                info = a.GetDirectories();
            }
            if (cB_VisionType.SelectedIndex == 2)
            {
                a = new DirectoryInfo(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName + "\\Vision_UpPaste\\");
                info = a.GetDirectories();
            }
            if (cB_VisionType.SelectedIndex == 3)
            {
                a = new DirectoryInfo(Variable.sPath_SYS_Program + frm_Main.frm_Program.ProgramName + "\\Vision_BadMark\\");
                info = a.GetDirectories();
            }

            if (updateNow)
            {
                list_LibVision.Items.Clear();
                if (cB_VisionType.SelectedIndex == 1)
                {
                    this.cB_DownVisionID.Items.Clear();
                }

                if (cB_VisionType.SelectedIndex ==0)
                {
                    this.cB_UpVisionID.Items.Clear();
                }

                if (cB_VisionType.SelectedIndex == 2)
                {
                    this.cB_PasteInfo.Items.Clear();
                }

                //if (cB_VisionType.SelectedIndex == 3)
                //{
                //    this.cb_bas.Items.Clear();
                //}

                for (int i = 0; i < info.Length; i++)
                {
                    list_LibVision.Items.Add(info[i].Name);
                    if (cB_VisionType.SelectedIndex == 1)
                    {
                        this.cB_DownVisionID.Items.Add(info[i].Name);
                    }

                    if (cB_VisionType.SelectedIndex == 0)
                    {
                        this.cB_UpVisionID.Items.Add(info[i].Name);
                    }

                    if (cB_VisionType.SelectedIndex == 2)
                    {
                        this.cB_PasteInfo.Items.Add(info[i].Name);
                    }
                }
            }
            else
            {
                if (list_LibVision.Items.Count != info.Length)
                {
                    list_LibVision.Items.Clear();

                    if (cB_VisionType.SelectedIndex == 1)
                    {
                        this.cB_DownVisionID.Items.Clear();
                    }

                    if (cB_VisionType.SelectedIndex == 0)
                    {
                        this.cB_UpVisionID.Items.Clear();
                    }

                    if (cB_VisionType.SelectedIndex == 2)
                    {
                        this.cB_PasteInfo.Items.Clear();
                    }

                    for (int i = 0; i < info.Length; i++)
                    {
                        list_LibVision.Items.Add(info[i].Name);

                        if (cB_VisionType.SelectedIndex == 1)
                        {
                            this.cB_DownVisionID.Items.Add(info[i].Name);
                        }

                        if (cB_VisionType.SelectedIndex == 0)
                        {
                            this.cB_UpVisionID.Items.Add(info[i].Name);
                        }

                        if (cB_VisionType.SelectedIndex == 2)
                        {
                            this.cB_PasteInfo.Items.Add(info[i].Name);
                        }
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
            frm_Main.frm_Wizard_Vision.TopLevel = false;
            frm_Main.frm_Wizard_Vision.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm_Main.frm_Wizard_Vision.cB_VisionType.SelectedIndex = this.cB_VisionType.SelectedIndex;
            frm_Main.frm_Program.pProgram.Controls.Clear();//移除所有控件
            frm_Main.frm_Program.pProgram.Controls.Add(frm_Main.frm_Wizard_Vision);
            frm_Main.frm_Wizard_Vision.Dock = DockStyle.Fill;
            frm_Main.frm_Wizard_Vision.Show();
        }

        private void bNewVision_Click(object sender, EventArgs e)
        {
            frm_Main.frm_Wizard_Vision = new frm_Wizard_Vision(frm_Main);
            Variable.AlmVisionInfo = new Definition.VisionInfo();
            Variable.AlmBaseInfo = new Definition.BaseInfo();

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
            frm_Main.frm_Wizard_Vision.cB_Module.SelectedIndex = Variable.AlmVisionInfo.Module - 1;

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
                frm_Main.frm_Wizard_Vision.cB_Algthrim1.Text = "线";
            }

            frm_Main.frm_Wizard_Vision.cB_Algthrim1.SelectedIndex = Variable.AlmVisionInfo.T1_AlignIndex;
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
                frm_Main.frm_Wizard_Vision.cB_Algthrim2.Text = "线";
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
                        try
                        {
                            frm_Main.frm_Wizard_Vision.cB_Module.SelectedIndex = Variable.AlmBaseInfo.Base_Module[i] - 1;
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[0].Value = Variable.AlmBaseInfo.Base_EN_Paste[i] == true ? "1" : "0";
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[1].Value = Variable.AlmBaseInfo.Base_PasteXY[i].X.ToString("F3");
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[2].Value = Variable.AlmBaseInfo.Base_PasteXY[i].Y.ToString("F3");
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[3].Value = Variable.AlmBaseInfo.Base_PasteR[i].ToString("F3");
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[4].Value = Variable.AlmBaseInfo.Base_PasteZ[0][i].ToString("F3");
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[5].Value = Variable.AlmBaseInfo.Base_PasteZ[1][i].ToString("F3");
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[6].Value = Variable.AlmBaseInfo.Base_Module[i].ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[7].Value = Variable.AlmBaseInfo.Base_Floor[i].ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[8].Value = Variable.AlmBaseInfo.Base_Feeder[i].ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[9].Value = Variable.AlmBaseInfo.Base_Nozzle[i].ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[10].Value = Variable.AlmBaseInfo.Base_Delay[i].ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[11].Value = Variable.AlmBaseInfo.Base_EN_BadMark[i] == true ? "1" : "0";
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[12].Value = Variable.AlmBaseInfo.Base_CamPoint_BadMark[i].X.ToString("F3");
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[13].Value = Variable.AlmBaseInfo.Base_CamPoint_BadMark[i].Y.ToString("F3");
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[14].Value = Variable.AlmBaseInfo.Base_ColorIsWhite_BadMark[i] == true ? "1" : "0";
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[15].Value = Variable.AlmBaseInfo.Base_ROI_BadMark[i].TopLeftX.ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[16].Value = Variable.AlmBaseInfo.Base_ROI_BadMark[i].TopLeftY.ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[17].Value = Variable.AlmBaseInfo.Base_ROI_BadMark[i].Width.ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[18].Value = Variable.AlmBaseInfo.Base_ROI_BadMark[i].Height.ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[19].Value = Variable.AlmBaseInfo.Base_MinArea_BadMark[i].ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[20].Value = Variable.AlmBaseInfo.Base_MaxArea_BadMark[i].ToString();

                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[21].Value = Variable.AlmBaseInfo.Base_Shutter_BadMark[i].ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[22].Value = Variable.AlmBaseInfo.Base_LightRedUse_BadMark[i] == true ? "1" : "0";
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[23].Value = Variable.AlmBaseInfo.Base_LightGreenUse_BadMark[i] == true ? "1" : "0";
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[24].Value = Variable.AlmBaseInfo.Base_LightBlueUse_BadMark[i] == true ? "1" : "0";
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[25].Value = Variable.AlmBaseInfo.Base_LightRedValue_BadMark[i].ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[26].Value = Variable.AlmBaseInfo.Base_LightGreenValue_BadMark[i].ToString();
                            frm_Main.frm_Wizard_Vision.dGV_Program.Rows[i].Cells[27].Value = Variable.AlmBaseInfo.Base_LightBlueValue_BadMark[i].ToString();

                        }
                        catch { }
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
                        Variable.AlmBaseInfo = Variable.LoadBaseInfo(frm_Main.frm_Program.ProgramName, proname);
                    }
                    Variable.AlmVisionInfo = Variable.LoadVisionInfo((short)cB_VisionType.SelectedIndex, frm_Main.frm_Program.ProgramName, proname);
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
            frm_Main.frm_Program.pProgram.Controls.Clear();
            frm_Main.frm_Wizard_Vision = new frm_Wizard_Vision(frm_Main);
            ShowVisionUI(proname);
            frm_Main.frm_Wizard_Vision.tProgramName.Enabled = false;
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
                        dGV_Feeder.Rows[i].Cells[7].Value = Variable.AlmProgram.FeederInfo_Module1[Index].Suck_Z[0][i].ToString("F3");
                        dGV_Feeder.Rows[i].Cells[8].Value = Variable.AlmProgram.FeederInfo_Module1[Index].Suck_Z[1][i].ToString("F3");
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
                        dGV_Feeder.Rows[i].Cells[7].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_Z[0][i].ToString("F3");
                        dGV_Feeder.Rows[i].Cells[8].Value = Variable.AlmProgram.FeederInfo_Module2[Index - 4].Suck_Z[1][i].ToString("F3");
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
            if (dGV_FeederNO.SelectedRows.Count > 0 && (dGV_FeederNO.SelectedRows[0].Index != dGV_FeederNO.Rows.Count - 1))
            {
                int Index = dGV_FeederNO.SelectedRows[0].Index;
                int PointCount = dGV_Feeder.Rows.Count - 1;
                if (Index >= 0 && Index <= 3)
                {
                  
                    FeederInfo info = new FeederInfo();
                    info.PointCount = (short)PointCount;
                    info.SensorID_Reach = new short[PointCount];
                    info.VisonProName_DownLabel = new string[PointCount];
                    info.DownCam_R = new double[PointCount];
                    info.ReachXI = new bool[PointCount];
                    info.Suck_R = new double[PointCount];
                    info.Suck_XY = new PointF[PointCount];
                    info.Suck_Z = new Common.SerializableDictionary<int, List<double>>();

                    info.Suck_Z.Clear();
                    info.Suck_Z.Add(0, new List<double>());
                    info.Suck_Z.Add(1, new List<double>());
                    for(int i = 0; i < PointCount; ++i)
                    {
                        info.Suck_Z[0].Add(0);
                        info.Suck_Z[1].Add(0);
                    }

                    info.VisonProName_UpLabel = new string[PointCount];
                    info.UpCam_XY = new PointF[PointCount];
                    for (int i = 0; i < PointCount; i++)
                    {
                        info.SensorID_Reach[i] = short.Parse(dGV_Feeder.Rows[i].Cells[0].Value.ToString());
                        info.VisonProName_DownLabel[i] = dGV_Feeder.Rows[i].Cells[1].Value.ToString();
                        info.DownCam_R[i] = double.Parse(dGV_Feeder.Rows[i].Cells[2].Value.ToString());
                        info.ReachXI[i] = dGV_Feeder.Rows[i].Cells[3].Value.ToString() == "1" ? true : false;
                        info.Suck_R[i] = double.Parse(dGV_Feeder.Rows[i].Cells[4].Value.ToString());
                        info.Suck_XY[i].X = float.Parse(dGV_Feeder.Rows[i].Cells[5].Value.ToString());
                        info.Suck_XY[i].Y = float.Parse(dGV_Feeder.Rows[i].Cells[6].Value.ToString());

                        info.Suck_Z[0][i] = double.Parse(dGV_Feeder.Rows[i].Cells[7].Value.ToString());
                        info.Suck_Z[1][i] = double.Parse(dGV_Feeder.Rows[i].Cells[8].Value.ToString());
                        info.VisonProName_UpLabel[i] = dGV_Feeder.Rows[i].Cells[9].Value.ToString();
                        info.UpCam_XY[i].X = float.Parse(dGV_Feeder.Rows[i].Cells[10].Value.ToString());
                        info.UpCam_XY[i].Y = float.Parse(dGV_Feeder.Rows[i].Cells[11].Value.ToString());
                    }

                    if (!Variable.AlmProgram.FeederInfo_Module1.ContainsKey(Index))
                    {
                        Variable.AlmProgram.FeederInfo_Module1.Add(Index, info);
                    }
                    else
                    {
                        Variable.AlmProgram.FeederInfo_Module1[Index] = info;
                    }
                }
                else
                {
                    FeederInfo info = new FeederInfo();
                    info.PointCount = (short)PointCount;
                    info.SensorID_Reach = new short[PointCount];
                    info.VisonProName_DownLabel = new string[PointCount];
                    info.DownCam_R = new double[PointCount];
                    info.ReachXI = new bool[PointCount];
                    info.Suck_R = new double[PointCount];
                    info.Suck_XY = new PointF[PointCount];
                    info.Suck_Z = new Common.SerializableDictionary<int, List<double>>();

                    info.Suck_Z.Clear();
                    info.Suck_Z.Add(0, new List<double>());
                    info.Suck_Z.Add(1, new List<double>());
                    for (int i = 0; i < PointCount; ++i)
                    {
                        info.Suck_Z[0].Add(0);
                        info.Suck_Z[1].Add(0);
                    }

                    info.VisonProName_UpLabel = new string[PointCount];
                    info.UpCam_XY = new PointF[PointCount];
                    for (int i = 0; i < PointCount; i++)
                    {
                        info.SensorID_Reach[i] = short.Parse(dGV_Feeder.Rows[i].Cells[0].Value.ToString());
                        info.VisonProName_DownLabel[i] = dGV_Feeder.Rows[i].Cells[1].Value.ToString();
                        info.DownCam_R[i] = double.Parse(dGV_Feeder.Rows[i].Cells[2].Value.ToString());
                        info.ReachXI[i] = dGV_Feeder.Rows[i].Cells[3].Value.ToString() == "1" ? true : false;
                        info.Suck_R[i] = double.Parse(dGV_Feeder.Rows[i].Cells[4].Value.ToString());
                        info.Suck_XY[i].X = float.Parse(dGV_Feeder.Rows[i].Cells[5].Value.ToString());
                        info.Suck_XY[i].Y = float.Parse(dGV_Feeder.Rows[i].Cells[6].Value.ToString());
                        info.Suck_Z[0][i] = double.Parse(dGV_Feeder.Rows[i].Cells[7].Value.ToString());
                        info.Suck_Z[1][i] = double.Parse(dGV_Feeder.Rows[i].Cells[8].Value.ToString());
                        info.VisonProName_UpLabel[i] = dGV_Feeder.Rows[i].Cells[9].Value.ToString();
                        info.UpCam_XY[i].X = float.Parse(dGV_Feeder.Rows[i].Cells[10].Value.ToString());
                        info.UpCam_XY[i].Y = float.Parse(dGV_Feeder.Rows[i].Cells[11].Value.ToString());
                    }

                    if (!Variable.AlmProgram.FeederInfo_Module2.ContainsKey(Index - 4))
                    {
                        Variable.AlmProgram.FeederInfo_Module2.Add(Index - 4, info);
                    }
                    else
                    {
                        Variable.AlmProgram.FeederInfo_Module2[Index - 4] = info;
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
                tCamRotate.Text = GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].RMap[0].Pos.ToString("F3");
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
                        PointF pt = GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].CurPos;
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[10].Value = pt.X;
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[11].Value = pt.Y;
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
                             GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].XYGoPos(TEMP, Definition.SpeedMode.Manual_Normal);
                            
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
                        PointF pt = GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].CurPos;
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[5].Value = pt.X.ToString("F3");
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[6].Value = pt.Y.ToString("F3");
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
                            TEMP.X = float.Parse(dGV_Feeder.SelectedRows[0].Cells[5].Value.ToString());
                            TEMP.Y = float.Parse(dGV_Feeder.SelectedRows[0].Cells[6].Value.ToString());
                            GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].XYGoPos(TEMP, Definition.SpeedMode.Manual_Normal);
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
                        short NzIndex = (short)(short.Parse(cB_NzXY.Text) + 1);
                        PointF pt = GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].CurPos;
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[5].Value = pt.X;//GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].CamtoNozzlePoint((short)(NzIndex -1), pt).X.ToString("F3");
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[6].Value = pt.Y;// GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].CamtoNozzlePoint((short)(NzIndex - 1), pt).Y.ToString("F3");
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
                            short NzIndex = (short)(short.Parse(cB_NzXY.Text));
                            TEMP.X = float.Parse(dGV_Feeder.SelectedRows[0].Cells[5].Value.ToString());
                            TEMP.Y = float.Parse(dGV_Feeder.SelectedRows[0].Cells[6].Value.ToString());
                            PointF pt = GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].NozzleToNozzlePoint((short)(NzIndex-1), TEMP);
                            GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].XYGoPos(pt, Definition.SpeedMode.Manual_Normal);
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
                        dGV_Feeder.Rows[dGV_Feeder.SelectedRows[i_Temp].Index].Cells[7 + NzIndex].Value = GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].ZMap[NzIndex].Pos.ToString("F3");
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

                            GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].ZMap[NzIndex].GoPos(TEMP, GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].GetVel(SpeedMode.Manual_Normal));

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
                tSuckRotate.Text = GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].RMap[0].Pos.ToString("F3");
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

                            GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].ZMap[NzIndex].GoPos(TEMP, GeneralSystem.Instance.Machines[cB_Module.SelectedIndex].GetVel(SpeedMode.Manual_Normal));
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


        public short XI_Module = 0;
        public short XI_NzIndex = 0;
        public double XI_Depth = 0;
        public PointF XI_Point = new PointF();

        private void bSuckLabel_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                try
                {
                    XI_Module = (short)(cB_Module.SelectedIndex);
                    XI_NzIndex = (short)cB_NzA.SelectedIndex;
                    if (!GeneralSystem.Instance.Machines[XI_Module].IsSafeHeight)
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
            PointF pt = GeneralSystem.Instance.Machines[XI_Module].NozzletoCamPoint(XI_NzIndex, XI_Point);
            RTN = GeneralSystem.Instance.Machines[XI_Module].XYGoPos(XI_Point, SpeedMode.Manual_Normal);
            if (RTN != 0)
            {
                return;
            }

            while (!GeneralSystem.Instance.Machines[XI_Module].XYReach(XI_Point))
            {
                Thread.Sleep(1);
                if(sw.ElapsedMilliseconds > 60*1000)
                {
                    return;
                }
            }

            RTN = GeneralSystem.Instance.Machines[XI_Module].ZGoPos(XI_NzIndex, XI_Depth, SpeedMode.Manual_Normal);
            if (RTN != 0)
            {
                return;
            }
            while (!GeneralSystem.Instance.Machines[XI_Module].ZReach(XI_NzIndex, XI_Depth))
            {
                Thread.Sleep(1);
                if (sw.ElapsedMilliseconds > 60 * 1000)
                {
                    return;
                }
            }

            RTN = GeneralSystem.Instance.Machines[XI_Module].ZMap[XI_NzIndex].XI_vaccum.SetIO();
            Thread.Sleep(1000);
            GeneralSystem.Instance.Machines[XI_Module].ZGoSafeHeight(SpeedMode.Manual_Normal);
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
                tNZCaptureCount.Text = Variable.AlmProgram.DownCapture_Module1.NozzleCaptureCount[XI_NzIndex].ToString();
                tNZAlignBase.Text = Variable.AlmProgram.DownCapture_Module1.NozzleBaseIndex[XI_NzIndex].ToString();
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
                tNZCaptureCount.Text = Variable.AlmProgram.DownCapture_Module2.NozzleCaptureCount[XI_NzIndex].ToString();
                tNZAlignBase.Text = Variable.AlmProgram.DownCapture_Module2.NozzleBaseIndex[XI_NzIndex].ToString();
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

                    Variable.AlmProgram.DownCapture_Module1.NozzleCaptureCount[DownCamPoint_NzIndex] = i0;
                    Variable.AlmProgram.DownCapture_Module1.NozzleBaseIndex[DownCamPoint_NzIndex] = i1;
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

                    Variable.AlmProgram.DownCapture_Module2.NozzleCaptureCount[DownCamPoint_NzIndex] = i0;
                    Variable.AlmProgram.DownCapture_Module2.NozzleBaseIndex[DownCamPoint_NzIndex] = i1;
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
            int module = rB_Module1.Checked ? 0 : 1;
            if (dGV_CamDownPoint.SelectedRows.Count > 0 && (dGV_CamDownPoint.SelectedRows[0].Index != dGV_CamDownPoint.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_CamDownPoint.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_CamDownPoint.SelectedRows[i_Temp].Index != dGV_CamDownPoint.Rows.Count - 1)
                    {
                        dGV_CamDownPoint.Rows[dGV_CamDownPoint.SelectedRows[i_Temp].Index].Cells[0].Value = GeneralSystem.Instance.Machines[module].CurPos.X.ToString("F3");
                        dGV_CamDownPoint.Rows[dGV_CamDownPoint.SelectedRows[i_Temp].Index].Cells[1].Value = GeneralSystem.Instance.Machines[module].CurPos.Y.ToString("F3");
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
                            GeneralSystem.Instance.Machines[DownCamPoint_Module - 1].XYGoPos(TEMP, SpeedMode.Manual_Normal);

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
            tNowOriginX.Text = GeneralSystem.Instance.Machines[0].CurPos.X.ToString("F3");
            tNowOriginY.Text = GeneralSystem.Instance.Machines[0].CurPos.Y.ToString("F3");
        }

        private void bSetCoord_Program_Click(object sender, EventArgs e)
        {
            tNowOriginX.Text = GeneralSystem.Instance.Machines[0].CurPos.X.ToString("F3");
            tNowOriginY.Text = GeneralSystem.Instance.Machines[0].CurPos.Y.ToString("F3");
        }

        #region 拼版界面 相关操作
        private void bAddPaste_Click(object sender, EventArgs e)
        {
            //获取当前选定的Index
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.Rows.Count - 1))
            {
                dGV_Program.Rows.Insert(dGV_Program.SelectedRows[0].Index + 1, 1);
                Common.CommonHelper.AddRowHeader(dGV_Program);
                DataGridViewRow item = dGV_Program.Rows[dGV_Program.SelectedRows[0].Index + 1];
                item.Cells[0].Value = "";
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
                item.Cells[12].Value = "0";
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
                item.Cells[12].Value = "0";
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
                        dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[2].Value = GeneralSystem.Instance.Machines[this.cbModuleMark.SelectedIndex].CurPos.X.ToString("F3");
                        dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[3].Value = GeneralSystem.Instance.Machines[this.cbModuleMark.SelectedIndex].CurPos.Y.ToString("F3");
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
                        dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[4].Value = GeneralSystem.Instance.Machines[this.cbModuleMark.SelectedIndex].CurPos.X.ToString("F3");
                        dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[5].Value = GeneralSystem.Instance.Machines[this.cbModuleMark.SelectedIndex].CurPos.Y.ToString("F3");
                    }
                }
            }
        }

        private void bCamGoUp1_Click(object sender, EventArgs e)
        {
            if (!GeneralSystem.Instance.Machines[this.cbModuleMark.SelectedIndex].Other.IsOutSafeArea())
            {
                return;
            }

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
                                GeneralSystem.Instance.Machines[this.cbModuleMark.SelectedIndex].XYGoPos(TEMP, SpeedMode.Manual_Normal);
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
        }

        private void bCamGoUp2_Click(object sender, EventArgs e)
        {
            if(!GeneralSystem.Instance.Machines[this.cbModuleMark.SelectedIndex].Other.IsOutSafeArea())
            {
                return;
            }

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
                            GeneralSystem.Instance.Machines[this.cbModuleMark.SelectedIndex].XYGoPos(TEMP, SpeedMode.Manual_Normal);
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
                VisionInfo temp = Variable.LoadVisionInfo(2,frm_Main.frm_Program.ProgramName, cB_PasteInfo.Text);
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
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[7].Value = X.ToString("F3");
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[8].Value = Y.ToString("F3");
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[9].Value = R.ToString("F3");
                            }
                            else
                            {
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[10].Value = X.ToString("F3");
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[11].Value = Y.ToString("F3");
                                dGV_Program.Rows[dGV_Program.SelectedRows[i_Temp].Index].Cells[12].Value = R.ToString("F3");
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
                    dGV_Program.Rows[dGV_Program.Rows.Count - 1 - Points1.Length + i * dGV_Program.SelectedRows.Count + j].Cells[12].Value = "0";
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

        private void cB_VisionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateList(true);
        }

        private void list_LibVision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bEditVision_Click(sender, e);
        }

        private void dGV_Program_SelectionChanged(object sender, EventArgs e)
        {
            if (dGV_Program.SelectedRows.Count > 0 && (dGV_Program.SelectedRows[0].Index != dGV_Program.RowCount - 1))
            {
                if (dGV_Program.SelectedRows[0].Cells[0].Value != null)
                {
                    BaseInfo temp = Variable.LoadBaseInfo(frm_Main.frm_Program.ProgramName, dGV_Program.SelectedRows[0].Cells[0].Value.ToString());
                    if (temp.IsLoadSuccessful)
                    {
                        if (temp.Base_Module != null && temp.Base_Module.Length > 0)
                        {
                            this.cbModuleMark.SelectedIndex = temp.Base_Module[0] - 1;
                            if(this.cbModuleMark.SelectedIndex == 0)
                            {
                                this.tOffSetX_Paste.Text = dGV_Program.SelectedRows[0].Cells[7].Value.ToString();
                                this.tOffSetY_Paste.Text = dGV_Program.SelectedRows[0].Cells[8].Value.ToString();
                                this.tOffSetR_Paste.Text = dGV_Program.SelectedRows[0].Cells[9].Value.ToString();
                            }
                            else
                            {
                                this.tOffSetX_Paste.Text = dGV_Program.SelectedRows[0].Cells[10].Value.ToString();
                                this.tOffSetY_Paste.Text = dGV_Program.SelectedRows[0].Cells[11].Value.ToString();
                                this.tOffSetR_Paste.Text = dGV_Program.SelectedRows[0].Cells[12].Value.ToString();
                            }
                        }
                    }
                    else
                    {
                        msgDiv1.MsgDivShow("程式加载失败!", 1);
                    }
                }
            }
        }

        private void bCalMark_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dGV_Program.SelectedRows.Count > 0)
                {
                    int module = this.cbModuleMark.SelectedIndex;
                    VisionImage image = new VisionImage();
                    HObject hImage = new HObject();
                    hImage = frm_Main.Image(frm_Main.frm_Camera.imageSet.Image);
                    string name = this.dGV_Program.SelectedRows[0].Cells[0].Value.ToString();
                    BaseInfo basePcb = Variable.LoadBaseInfo(frm_Main.frm_Program.ProgramName, dGV_Program.SelectedRows[0].Cells[0].Value.ToString());

                    VisionInfo visionInfo = Variable.LoadVisionInfo(2, frm_Main.frm_Program.ProgramName, dGV_Program.SelectedRows[0].Cells[0].Value.ToString());
                    Vision.GainOffset(visionInfo.T1_PreProcess_Gain, visionInfo.T1_PreProcess_Offset, ref hImage);

                    if (frm_Main.DetectFOV1(hImage, ref visionInfo, out GeneralSystem.Instance.Machines[module].UpVisionHelper.ShowList,
                        out GeneralSystem.Instance.Machines[module].UpVisionHelper.Colors))
                    {
                        PointF pt = GeneralSystem.Instance.Machines[module].CurPos;
                        PointF resultPt = new PointF();
                        double resultAngle = 0;

                        if (frm_Main.CalResult(module, 1, visionInfo, pt, pt, ref resultPt, ref resultAngle))
                        {
                            if (MessageBox.Show("是否移动到 识别的Mark 点", "TIPS", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                GeneralSystem.Instance.Machines[module].XYGoPos(resultPt, SpeedMode.Manual_Normal);
                                if (MessageBox.Show("是否移动到第一个贴服点!!!", "TIPS", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                {
                                    PointF newPoint = GeneralSystem.Instance.Machines[module].TransformPoints(basePcb.Base_PasteXY[0], visionInfo.Result_Point, visionInfo.Result_R, resultPt, resultAngle);
                                    GeneralSystem.Instance.Machines[module].XYGoPos(newPoint, SpeedMode.Manual_Normal);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("识别Mark点出错!!!");
                    }

                    image?.Dispose();
                    hImage?.Dispose();
                }
                else
                {
                    MessageBox.Show("请选中需要识别的Mark点!!!");
                }
            }
            catch { }
        }

        public struct MarkItemInfo
        {
            public PointF mark1;
            public int rowIndex;
            public double Offset;
        }

        public List<List<MarkItemInfo>> OptimizePath(List<MarkItemInfo> ModuleMark)
        {
            if (ModuleMark.Count == 0)
                return new List<List<MarkItemInfo>>();

            // 根据Y方向排序
            ModuleMark.Sort((a, b) =>
            {
                if (a.mark1.Y < b.mark1.Y)
                    return -1;
                else if (a.mark1.Y == b.mark1.Y)
                    return 0;
                else
                    return 1;
            });

            List<List<MarkItemInfo>> ModuleLine = new List<List<MarkItemInfo>>();
            ModuleLine.Add(new List<MarkItemInfo>());
            ModuleLine[0].Add(ModuleMark[0]);
            int line = 0;
            for (int i = 1; i < ModuleMark.Count; ++i)
            {
                if (Math.Abs(ModuleMark[i - 1].mark1.Y - ModuleMark[i].mark1.Y) < 3) // 小于3mm 认为属于一行的
                {
                    MarkItemInfo temp = ModuleMark[i];
                    temp.mark1.Y = ModuleLine[line][0].mark1.Y;
                    ModuleLine[line].Add(temp);
                }
                else
                {
                    line++;
                    ModuleLine.Add(new List<MarkItemInfo>());
                    ModuleLine[line].Add(ModuleMark[i]);
                }
            }

            // 蛇形排序
            for (int i = 0; i < ModuleLine.Count; ++i)
            {
                if (i % 2 == 0)
                {
                    ModuleLine[i].Sort((a, b) =>
                    {
                        if (a.mark1.X < b.mark1.X)
                            return -1;
                        else if (a.mark1.X == b.mark1.X)
                            return 0;
                        else
                            return 1;
                    });
                }
                else
                {
                    ModuleLine[i].Sort((a, b) =>
                    {
                        if (a.mark1.X > b.mark1.X)
                            return -1;
                        else if (a.mark1.X == b.mark1.X)
                            return 0;
                        else
                            return 1;
                    });
                }
            }

            ModuleMark.Clear();
            foreach(var item in ModuleLine)
            {
                ModuleMark.AddRange(item);
            }

            return ModuleLine;
        }

        private void bAutoFly_Click(object sender, EventArgs e)
        {
            // Mark 1 整理
            if(dGV_Program.Rows.Count > 0)
            {
                List<MarkItemInfo> Module1Mark = new List<MarkItemInfo>();
                List<MarkItemInfo> Module2Mark = new List<MarkItemInfo>();

                // 模组分化 Mark点
                for (int index = 0; index < dGV_Program.Rows.Count - 1; ++index)
                {
                    string baseName = dGV_Program.Rows[index].Cells[0].Value.ToString();
                    var info = Variable.LoadBaseInfo(frm_Main.frm_Program.ProgramName, baseName);
                    if(info.Base_Module.Length > 0)
                    {
                        MarkItemInfo item = new MarkItemInfo();
                        item.mark1 = new PointF();
                        item.mark1.X = float.Parse(dGV_Program.Rows[index].Cells[2].Value.ToString());
                        item.mark1.Y = float.Parse(dGV_Program.Rows[index].Cells[3].Value.ToString());
                        item.rowIndex = index;
                        if (info.Base_Module[0] == 1)
                            Module1Mark.Add(item);
                        else
                            Module2Mark.Add(item);
                    }
                }

                var Module1Line = OptimizePath(Module1Mark);

                var Module2Line = OptimizePath(Module2Mark);

                // 重新整理 GV 顺序
                List<int> refreshList = new List<int>();
                for (int i = 0; i < Module1Line.Count; ++i)
                {
                    for(int j = 0; j< Module1Line[i].Count; ++j)
                    {
                        refreshList.Add(Module1Line[i][j].rowIndex);
                    }
                }

                for (int i = 0; i < Module2Line.Count; ++i)
                {
                    for (int j = 0; j < Module2Line[i].Count; ++j)
                    {
                        refreshList.Add(Module2Line[i][j].rowIndex);
                    }
                }

                CommonHelper.DelDGV(this.dGV_Program);
                
                dGV_Program.Rows.Add(refreshList.Count);

                for (int i = 0; i < refreshList.Count; ++i)
                {
                    this.dGV_Program.Rows[i].Cells[0].Value = Variable.AlmProgram.MutiBaseInfo.VisionProName_Base[refreshList[i]];
                    this.dGV_Program.Rows[i].Cells[1].Value = Variable.AlmProgram.MutiBaseInfo.FOVCount_Base[refreshList[i]];
                    this.dGV_Program.Rows[i].Cells[2].Value = Variable.AlmProgram.MutiBaseInfo.Cam_Mark1Point[refreshList[i]].X.ToString("f3");
                    this.dGV_Program.Rows[i].Cells[3].Value = Variable.AlmProgram.MutiBaseInfo.Cam_Mark1Point[refreshList[i]].Y.ToString("f3");
                    this.dGV_Program.Rows[i].Cells[4].Value = Variable.AlmProgram.MutiBaseInfo.Cam_Mark2Point[refreshList[i]].X.ToString("f3");
                    this.dGV_Program.Rows[i].Cells[5].Value = Variable.AlmProgram.MutiBaseInfo.Cam_Mark2Point[refreshList[i]].Y.ToString("f3");
                    this.dGV_Program.Rows[i].Cells[6].Value = Variable.AlmProgram.MutiBaseInfo.OffsetFly[refreshList[i]];

                    this.dGV_Program.Rows[i].Cells[7].Value = Variable.AlmProgram.MutiBaseInfo.OffsetX[0][refreshList[i]];
                    this.dGV_Program.Rows[i].Cells[8].Value = Variable.AlmProgram.MutiBaseInfo.OffsetY[0][refreshList[i]];
                    this.dGV_Program.Rows[i].Cells[9].Value = Variable.AlmProgram.MutiBaseInfo.OffsetR[0][refreshList[i]];
                    this.dGV_Program.Rows[i].Cells[10].Value = Variable.AlmProgram.MutiBaseInfo.OffsetX[1][refreshList[i]];
                    this.dGV_Program.Rows[i].Cells[11].Value = Variable.AlmProgram.MutiBaseInfo.OffsetY[1][refreshList[i]];
                    this.dGV_Program.Rows[i].Cells[12].Value = Variable.AlmProgram.MutiBaseInfo.OffsetR[1][refreshList[i]];
                }
                CommonHelper.AddRowHeader(this.dGV_Program);
            }
        }

        public void DoEvent(int module,PointF pt)
        {
            while (!GeneralSystem.Instance.Machines[module].XYReach(pt))
            {
                Thread.Sleep(10);
                //double curCmp = 0;
                //GeneralSystem.Instance.Machines[module].X.GetAxisPos();
                //GeneralSystem.Instance.Machines[module].X.GetCompareCurData(ref curCmp);
                //Debug.WriteLine($"CMP: {curCmp} Cur:{GeneralSystem.Instance.Machines[module].X.Pos}");
                Application.DoEvents();
            }
        }

        public List<PointF> DoFlyTest(int module, List<MarkItemInfo> ModuleMark, bool calAll, ref List<PointF> PastePoints)
        {
            List<PointF> ResultList = new List<PointF>();

            GeneralSystem.Instance.Machines[module].X.SetComapreData(0, 0, 0, 0, 0, 5);//取消飞拍
            GeneralSystem.Instance.Machines[module].X.SetComapreData(0, 1, 0, 0, 0, 5);//取消飞拍

            UpFlyTool tool = new UpFlyTool(frm_Main, module);
            tool.StartFly();

            // 优化路径
            var ModuleLine = OptimizePath(ModuleMark);
            #region 飞拍
            // 到清料位等待
            PointF pt = GeneralSystem.Instance.Machines[module].Config.ReadyPoint;
            for (int i = 0; i < ModuleLine.Count; ++i)
            {
                if (ModuleLine[i].Count > 1)
                {
                    PointF startP = ModuleLine[i][0].mark1;
                    PointF endP = ModuleLine[i].Last().mark1;
                    int dir = 0;
                    if(startP.X > endP.X)
                    {
                        startP.X += 5;
                        endP.X -= 5;
                        dir = 1;
                    }
                    else
                    {
                        startP.X -= 5;
                        endP.X += 5;
                        dir = 0;
                    }

                    endP.Y = startP.Y;
                    GeneralSystem.Instance.Machines[module].XYGoPos(startP, SpeedMode.Manual_Normal);
                    DoEvent(module, startP);
                    Thread.Sleep(50);
                    double[] Table = new double[6000];
                    short TabCount = 0;
                    for (int j = 0; j < ModuleLine[i].Count; ++j)
                    {
                        if(module == 0)
                        {
                            Table[2*j] = (int)((ModuleLine[i][j].mark1.X + ModuleLine[i][j].Offset) * GeneralSystem.Instance.Machines[module].X.AxisRatio);
                            Table[2*j+1] = (int)((ModuleLine[i][j].mark1.X + ModuleLine[i][j].Offset) * GeneralSystem.Instance.Machines[module].X.AxisRatio);
                            TabCount = (short)(ModuleLine[i].Count*2);
                        }
                        else
                        {
                            Table[j] = (int)((ModuleLine[i][j].mark1.X + ModuleLine[i][j].Offset) * GeneralSystem.Instance.Machines[module].X.AxisRatio);
                            TabCount = (short)(ModuleLine[i].Count);
                        }
                    }

                    short rtn = GeneralSystem.Instance.Machines[module].X.SetComapreTable(Table, TabCount);
                    Array.Clear(Table,0, Table.Length);
                    rtn+=GeneralSystem.Instance.Machines[module].X.SetComapreData(1, (uint)dir, 0, 0, 0, 5);//设置飞拍
                    if(i == 0)
                        tool.ClearImage();

                    GeneralSystem.Instance.Machines[module].XYGoPos(endP, GeneralSystem.Instance.Machines[module].Config.FlySpeedConfig);
                    DoEvent(module, endP);
                    rtn +=GeneralSystem.Instance.Machines[module].X.SetComapreData(0, (uint)dir, 0, 0, 0, 5);//取消飞拍
                }
                else
                {
                    GeneralSystem.Instance.Machines[0].XYGoPos(ModuleLine[i][0].mark1, SpeedMode.Manual_Normal);
                    DoEvent(module, ModuleLine[i][0].mark1);
                }
            }
            tool.StopFly();
            if(tool.GrabList.Count != ModuleMark.Count)
            {
                MessageBox.Show($"比较输出点 {ModuleMark.Count} 个  采集到图片 {tool.GrabList.Count} 张");
                tool.ClearImage();
                return new List<PointF>();
            }
            #endregion

            #region 计算
            for (int i = 0; i < ModuleMark.Count;++i)
            {
                int row = ModuleMark[i].rowIndex;
                HObject hImage = new HObject();
                hImage = frm_Main.Image(tool.GrabList[i]);
                string name = this.dGV_Program.Rows[row].Cells[0].Value.ToString();
                BaseInfo basePcb = Variable.LoadBaseInfo(frm_Main.frm_Program.ProgramName, dGV_Program.Rows[row].Cells[0].Value.ToString());

                VisionInfo visionInfo = Variable.LoadVisionInfo(2, frm_Main.frm_Program.ProgramName, name);
                Vision.GainOffset(visionInfo.T1_PreProcess_Gain, visionInfo.T1_PreProcess_Offset, ref hImage);

                PointF imagePt = new PointF();
                PointF worldPt = new PointF();

                if(calAll)
                {
                    if (frm_Main.DetectFOV1(hImage, ref visionInfo, out GeneralSystem.Instance.Machines[module].UpVisionHelper.ShowList,
                        out GeneralSystem.Instance.Machines[module].UpVisionHelper.Colors))
                    {
                        PointF camPos = ModuleMark[i].mark1;
                        double resultAngle = 0;
                        if (frm_Main.CalResult(module, 1, visionInfo, camPos, camPos, ref worldPt, ref resultAngle))
                        {
                            PointF newPoint = GeneralSystem.Instance.Machines[module].TransformPoints(basePcb.Base_PasteXY[0], visionInfo.Result_Point, visionInfo.Result_R, worldPt, resultAngle);
                            PastePoints.Add(newPoint);
                            ResultList.Add(worldPt);
                        }
                        else
                            ResultList.Add(camPos);
                    }


                }
                else
                {
                    // 比较图像
                    if (frm_Main.DetectFly(module, ModuleMark[i].mark1, ref hImage, ref visionInfo, ref imagePt, ref worldPt))
                    {
                        tool.GrabList[i].Overlays.Default.AddLine(new LineContour(new PointContour(imagePt.X - 50, imagePt.Y), new PointContour(imagePt.X + 50, imagePt.Y)), Rgb32Value.BlueColor);
                        tool.GrabList[i].Overlays.Default.AddLine(new LineContour(new PointContour(imagePt.X, imagePt.Y - 50), new PointContour(imagePt.X, imagePt.Y + 50)), Rgb32Value.BlueColor);
                        tool.GrabList[i].Overlays.Default.AddText($"X:{imagePt.X} Y:{imagePt.Y}", new PointContour(100, 100), Rgb32Value.BlueColor, new OverlayTextOptions("Consolas", 125));
                        ResultList.Add(worldPt);
                    }
                    else
                        ResultList.Add(ModuleMark[i].mark1);

                    tool.GrabList[i].WriteBmpFile($"D:\\ALMInfo\\FlyCapture\\Fly\\{module}\\{i}.bmp");
                    tool.GrabList[i].Overlays.Default.Clear();
                }

                Variable.ClearVisionInfo(ref visionInfo);
                tool.GrabList[i]?.Dispose();
                hImage?.Dispose();
            }
            #endregion

            return ResultList;
        }

        public List<PointF> DoPointCal(int module, List<MarkItemInfo> ModuleLine)
        {
            List<PointF> ResultList = new List<PointF>();
            try
            {
                GeneralSystem.Instance.Machines[module]._session_Up.Acquisition.Unconfigure();
                GeneralSystem.Instance.Machines[module]._session_Up.ConfigureGrab();
                frm_Main.SetCameraTrigger_Balser(GeneralSystem.Instance.Machines[module]._session_Up, true);

                for (int i = 0; i < ModuleLine.Count; ++i)
                {
                    GeneralSystem.Instance.Machines[module].XYGoPos(ModuleLine[i].mark1, SpeedMode.Manual_Normal);
                    DoEvent(module, ModuleLine[i].mark1);
                    Thread.Sleep(100);
                    int row = ModuleLine[i].rowIndex;
                    VisionImage image = new VisionImage();
                    HObject hImage = new HObject();
                    image = GeneralSystem.Instance.Machines[module]._session_Up.Grab(null, true);
                    hImage = frm_Main.Image(image);
                    string name = this.dGV_Program.Rows[row].Cells[0].Value.ToString();

                    VisionInfo visionInfo = Variable.LoadVisionInfo(2, frm_Main.frm_Program.ProgramName, name);
                    Vision.GainOffset(visionInfo.T1_PreProcess_Gain, visionInfo.T1_PreProcess_Offset, ref hImage);

                    PointF imagePt = new PointF();
                    PointF worldPt = new PointF();
                    // 比较图像
                    if (frm_Main.DetectFly(module, ModuleLine[i].mark1,ref hImage, ref visionInfo, ref imagePt, ref worldPt))
                    {
                        image.Overlays.Default.AddLine(new LineContour(new PointContour(imagePt.X - 50, imagePt.Y), new PointContour(imagePt.X + 50, imagePt.Y)), Rgb32Value.BlueColor);
                        image.Overlays.Default.AddLine(new LineContour(new PointContour(imagePt.X, imagePt.Y - 50), new PointContour(imagePt.X, imagePt.Y + 50)), Rgb32Value.BlueColor);
                        image.Overlays.Default.AddText($"X:{imagePt.X} Y:{imagePt.Y}", new PointContour(100, 100), Rgb32Value.BlueColor, new OverlayTextOptions("Consolas", 125));
                        ResultList.Add(worldPt);
                    }
                    else
                        ResultList.Add(ModuleLine[i].mark1);

                    //image.Overlays.Default.Merge();
                    image.WriteBmpFile($"D:\\ALMInfo\\FlyCapture\\Point\\{module}\\{i}.bmp");
                    image.Overlays.Default.Clear();
                    image?.Dispose();
                    hImage?.Dispose();

                    Variable.ClearVisionInfo(ref visionInfo);
                }
                GeneralSystem.Instance.Machines[module]._session_Up.Acquisition.Unconfigure();
            }
            catch { }

            PointF pt = GeneralSystem.Instance.Machines[module].Config.ReadyPoint;
            GeneralSystem.Instance.Machines[module].XYGoPos(pt, SpeedMode.Manual_Normal);
            DoEvent(module, pt);

            return ResultList;
        }

        public List<PointF> ModulePointCalResult = new List<PointF>();

        public List<PointF> ModuleFlyCalResult = new List<PointF>();

        public List<PointF> ModuleRealResult = new List<PointF>();

        private void bTestFly_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("是否开始飞拍", "Tips", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            // 到清料位等待
            PointF pt1 = new PointF();
            PointF pt2 = new PointF();
            pt1 =  GeneralSystem.Instance.Machines[0].Config.ReadyPoint;
            pt2 = GeneralSystem.Instance.Machines[1].Config.ReadyPoint;

            GeneralSystem.Instance.Machines[0].XYGoPos(pt1, SpeedMode.Manual_Normal);
            GeneralSystem.Instance.Machines[1].XYGoPos(pt2, SpeedMode.Manual_Normal);
            DoEvent(0, pt1);
            DoEvent(1, pt2);

            // Mark 1 整理
            if (dGV_Program.Rows.Count > 0)
            {
                List<MarkItemInfo> Module1Mark = new List<MarkItemInfo>();
                List<MarkItemInfo> Module2Mark = new List<MarkItemInfo>();

                // 模组分化 Mark点
                for (int index = 0; index < dGV_Program.Rows.Count - 1; ++index)
                {
                    string baseName = dGV_Program.Rows[index].Cells[0].Value.ToString();
                    var info = Variable.LoadBaseInfo(frm_Main.frm_Program.ProgramName, baseName);
                    if (info.Base_Module.Length > 0)
                    {
                        MarkItemInfo item = new MarkItemInfo();
                        item.mark1 = new PointF();
                        item.mark1.X = float.Parse(dGV_Program.Rows[index].Cells[2].Value.ToString());
                        item.mark1.Y = float.Parse(dGV_Program.Rows[index].Cells[3].Value.ToString());
                        item.Offset = float.Parse(dGV_Program.Rows[index].Cells[6].Value.ToString());

                        item.rowIndex = index;
                        if (info.Base_Module[0] == 1)
                            Module1Mark.Add(item);
                        else
                            Module2Mark.Add(item);
                    }
                }

                this.ModulePointCalResult.Clear();
                this.ModuleFlyCalResult.Clear();
                this.ModuleFlyCalResult.AddRange(this.DoFlyTest(0, Module1Mark, false, ref ModuleRealResult));
                this.ModulePointCalResult.AddRange(this.DoPointCal(0, Module1Mark));
                this.ModuleFlyCalResult.AddRange(this.DoFlyTest(1, Module2Mark, false, ref ModuleRealResult));
                this.ModulePointCalResult.AddRange(this.DoPointCal(1, Module2Mark));

                if(this.ModuleFlyCalResult.Count == this.ModulePointCalResult.Count)
                {
                    // 与点拍进行比较
                    for (int index = 0; index < dGV_Program.Rows.Count - 1; ++index)
                    {
                        double offset = double.Parse(dGV_Program.Rows[index].Cells[6].Value.ToString());
                        dGV_Program.Rows[index].Cells[6].Value = (offset + (this.ModuleFlyCalResult[index].X - this.ModulePointCalResult[index].X)).ToString("f3");
                    }
                }

                Module1Mark.Clear();
                Module2Mark.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否开始飞拍", "Tips", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            // 到清料位等待
            PointF pt1 = new PointF();
            PointF pt2 = new PointF();
            pt1 = GeneralSystem.Instance.Machines[0].Config.ReadyPoint;
            pt2 = GeneralSystem.Instance.Machines[1].Config.ReadyPoint;

            GeneralSystem.Instance.Machines[0].XYGoPos(pt1, SpeedMode.Manual_Normal);
            GeneralSystem.Instance.Machines[1].XYGoPos(pt2, SpeedMode.Manual_Normal);
            DoEvent(0, pt1);
            DoEvent(1, pt2);

            // Mark 1 整理
            if (dGV_Program.Rows.Count > 0)
            {
                List<MarkItemInfo> Module1Mark = new List<MarkItemInfo>();
                List<MarkItemInfo> Module2Mark = new List<MarkItemInfo>();

                // 模组分化 Mark点
                for (int index = 0; index < dGV_Program.Rows.Count - 1; ++index)
                {
                    string baseName = dGV_Program.Rows[index].Cells[0].Value.ToString();
                    var info = Variable.LoadBaseInfo(frm_Main.frm_Program.ProgramName, baseName);
                    if (info.Base_Module.Length > 0)
                    {
                        MarkItemInfo item = new MarkItemInfo();
                        item.mark1 = new PointF();
                        item.mark1.X = float.Parse(dGV_Program.Rows[index].Cells[2].Value.ToString());
                        item.mark1.Y = float.Parse(dGV_Program.Rows[index].Cells[3].Value.ToString());
                        item.Offset = float.Parse(dGV_Program.Rows[index].Cells[6].Value.ToString());

                        item.rowIndex = index;
                        if (info.Base_Module[0] == 1)
                            Module1Mark.Add(item);
                        else
                            Module2Mark.Add(item);
                    }
                }

                this.ModulePointCalResult.Clear();
                this.ModuleFlyCalResult.Clear();
                ModuleRealResult.Clear();
                this.ModuleFlyCalResult.AddRange(this.DoFlyTest(0, Module1Mark, true, ref ModuleRealResult));
                GeneralSystem.Instance.Machines[0].XYGoPos(pt1, SpeedMode.Manual_Normal);
                DoEvent(0, pt1);

                this.ModuleFlyCalResult.AddRange(this.DoFlyTest(1, Module2Mark, true, ref ModuleRealResult));
                GeneralSystem.Instance.Machines[1].XYGoPos(pt2, SpeedMode.Manual_Normal);
                DoEvent(1, pt2);
            }
        }

        private void bGoFly1_Click(object sender, EventArgs e)
        {
            if (!GeneralSystem.Instance.Machines[this.cbModuleMark.SelectedIndex].Other.IsOutSafeArea())
            {
                return;
            }

            if (this.dGV_Program.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("是否移动到Mark点","Tips",MessageBoxButtons.OK) != DialogResult.OK)
                {
                    return;
                }

                    int selectIndex = this.dGV_Program.SelectedRows[0].Index;
                int module = this.cbModuleMark.SelectedIndex;
                if (this.ModuleFlyCalResult.Count > selectIndex)
                {
                    GeneralSystem.Instance.Machines[module].XYGoPos(this.ModuleFlyCalResult[selectIndex], SpeedMode.Manual_Normal);
                }
            }
        }

        private void bGoFly2_Click(object sender, EventArgs e)
        {
            if (!GeneralSystem.Instance.Machines[this.cbModuleMark.SelectedIndex].Other.IsOutSafeArea())
            {
                return;
            }

            if (this.dGV_Program.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("是否移动到贴附点", "Tips", MessageBoxButtons.OK) != DialogResult.OK)
                {
                    return;
                }

                int selectIndex = this.dGV_Program.SelectedRows[0].Index;
                int module = this.cbModuleMark.SelectedIndex;

                if (this.ModuleFlyCalResult.Count > selectIndex)
                {
                    GeneralSystem.Instance.Machines[module].XYGoPos(ModuleRealResult[selectIndex], SpeedMode.Manual_Normal);
                }
            }
        }
        #endregion

        #region badmark 界面 相关操作
        private void bAddBadmark_Click(object sender, EventArgs e)
        {
            //获取当前选定的Index
            if (dGV_Badmark.SelectedRows.Count > 0 && (dGV_Badmark.SelectedRows[0].Index != dGV_Badmark.Rows.Count - 1))
            {
                dGV_Badmark.Rows.Insert(dGV_Badmark.SelectedRows[0].Index + 1, 1);
                Common.CommonHelper.AddRowHeader(dGV_Badmark);
                DataGridViewRow item = dGV_Badmark.Rows[dGV_Badmark.SelectedRows[0].Index + 1];
                item.Cells[0].Value = "0";
                item.Cells[1].Value = "0";
                item.Cells[2].Value = "0";
                item.Cells[3].Value = "0";
                item.Cells[4].Value = "0";
                item.Cells[5].Value = "0";
            }
            else
            {
                dGV_Badmark.Rows.Insert(0, 1);
                Common.CommonHelper.AddRowHeader(dGV_Badmark);
                DataGridViewRow item = dGV_Badmark.Rows[0];
                item.Cells[0].Value = "0";
                item.Cells[1].Value = "0";
                item.Cells[2].Value = "0";
                item.Cells[3].Value = "0";
                item.Cells[4].Value = "0";
                item.Cells[5].Value = "0";
            }
        }

        private void bDelBadmark_Click(object sender, EventArgs e)
        {
            this.DeleGVRow(this.dGV_Badmark);
        }

        private void bUpBadmark_Click(object sender, EventArgs e)
        {
            this.UpRow(this.dGV_Badmark);
        }

        private void bDownBadmark_Click(object sender, EventArgs e)
        {
            this.DownRow(this.dGV_Badmark);
        }

        private void bRecrodeBadmark_Click(object sender, EventArgs e)
        {
            if (dGV_Badmark.SelectedRows.Count > 0 && (dGV_Badmark.SelectedRows[0].Index != dGV_Badmark.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Badmark.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Badmark.SelectedRows[i_Temp].Index != dGV_Badmark.Rows.Count - 1)
                    {
                        int selecIndex = dGV_Badmark.SelectedRows[i_Temp].Index;
                        int module = this.cbBadmarkModule.SelectedIndex;

                        dGV_Badmark.Rows[selecIndex].Cells[1].Value = GeneralSystem.Instance.Machines[module].CurPos.X.ToString("F3");
                        dGV_Badmark.Rows[selecIndex].Cells[2].Value = GeneralSystem.Instance.Machines[module].CurPos.Y.ToString("F3");
                    }
                }
            }
        }

        private void bMoveBadmark_Click(object sender, EventArgs e)
        {
            if (!GeneralSystem.Instance.Machines[this.cbBadmarkModule.SelectedIndex].Other.IsOutSafeArea())
            {
                return;
            }

            {
                if (dGV_Badmark.SelectedRows.Count > 0 && (dGV_Badmark.SelectedRows[0].Index != dGV_Badmark.RowCount - 1))
                {
                    try
                    {
                        var RSS = MessageBox.Show(this, "确定要移动到点吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        switch (RSS)
                        {
                            case DialogResult.Yes:
                                PointF TEMP = new PointF();
                                TEMP.X = float.Parse(dGV_Badmark.SelectedRows[0].Cells[1].Value.ToString());
                                TEMP.Y = float.Parse(dGV_Badmark.SelectedRows[0].Cells[2].Value.ToString());
                                GeneralSystem.Instance.Machines[this.cbBadmarkModule.SelectedIndex].XYGoPos(TEMP, SpeedMode.Manual_Normal);
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
        }

        private void bExpendBadmark1_Click(object sender, EventArgs e)
        {
            frm_Expand frm = new frm_Expand();
            frm.Show();
        }

        private void bExpendBadmark2_Click(object sender, EventArgs e)
        {
            if (Variable.bExpand)
            {
                if (dGV_Badmark.SelectedRows.Count > 0 && dGV_Badmark.SelectedRows[0].Index != dGV_Badmark.Rows.Count - 1)
                {
                    List<PointF> Point2Expand_Mark1 = new List<PointF>();
                    for (int ii = 0; ii < dGV_Badmark.SelectedRows.Count; ii++)
                    {
                        Point2Expand_Mark1.Add(new PointF(float.Parse(dGV_Badmark.SelectedRows[ii].Cells[1].Value.ToString())
                            , float.Parse(dGV_Badmark.SelectedRows[ii].Cells[2].Value.ToString())));
                    }

                    PointF[] Points2Add_Mark1 = Common.MathHelper.Expand2AddPoints(
                        Point2Expand_Mark1.ToArray()
                        , Variable.pExpand_O
                        , Variable.pExpand_B
                        , Variable.pExpand_A
                        , Variable.iExpand_N
                        , Variable.iExpand_M);
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

        private void bFindBadmark_Click(object sender, EventArgs e)
        {
            if (this.dGV_Badmark.SelectedRows.Count > 0)
            {
                this.UpdateImage_Roi();
                int selectIndex = this.dGV_Badmark.SelectedRows[0].Index;
                string markName = this.dGV_Badmark.Rows[selectIndex].Cells[0].Value.ToString();
                VisionInfo visionInfo = Variable.LoadVisionInfo(3, frm_Main.frm_Program.ProgramName, markName);
                Vision.GainOffset(visionInfo.T1_PreProcess_Gain, visionInfo.T1_PreProcess_Offset, ref imageSet);
                HOperatorSet.WriteImage(imageSet, "bmp", 0, "D://test.bmp");

                if (frm_Main.DetectFOV1(imageSet, ref visionInfo, out GeneralSystem.Instance.Machines[0].UpVisionHelper.ShowList,
                        out GeneralSystem.Instance.Machines[0].UpVisionHelper.Colors))
                {
                    MessageBox.Show("识别BadMark点成功");
                }
                else
                {
                    MessageBox.Show("识别BadMark点出错!!!");
                }

                imageSet?.Dispose();
            }
            else
            {
                MessageBox.Show("请选中需要识别的 BadMark点!!!");
            }
        }

        private void bBadFlyOpimze_Click(object sender, EventArgs e)
        {

        }

        private void bFlyBadmarkTest_Click(object sender, EventArgs e)
        {

        }

        private void bSetMap_Click(object sender, EventArgs e)
        {
            if (dGV_Badmark.SelectedRows.Count > 0 && (dGV_Badmark.SelectedRows[0].Index != dGV_Badmark.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Badmark.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Badmark.SelectedRows[i_Temp].Index != dGV_Badmark.Rows.Count - 1)
                    {
                        int selecIndex = dGV_Badmark.SelectedRows[i_Temp].Index;
                        dGV_Badmark.Rows[selecIndex].Cells[0].Value = this.cbBadMark.Text;
                        dGV_Badmark.Rows[selecIndex].Cells[4].Value = this.tBadMarkMap1.Text;
                        dGV_Badmark.Rows[selecIndex].Cells[5].Value = this.tBadMarkMap2.Text;
                    }
                }
            }
        }

        private void bGoModule1Pos_Click(object sender, EventArgs e)
        {

        }

        private void bGoModule2Pos_Click(object sender, EventArgs e)
        {

        }

        private void dGV_Badmark_SelectionChanged(object sender, EventArgs e)
        {
            if (dGV_Badmark.SelectedRows.Count > 0 && (dGV_Badmark.SelectedRows[0].Index != dGV_Badmark.RowCount - 1))
            {
                int selecIndex = dGV_Badmark.SelectedRows[0].Index;

                if (dGV_Badmark.Rows[selecIndex].Cells[0].Value != null)
                {
                    this.cbBadMark.Text = dGV_Badmark.Rows[selecIndex].Cells[0].Value.ToString();
                    this.tBadMarkMap1.Text = dGV_Badmark.Rows[selecIndex].Cells[4].Value.ToString();
                    this.tBadMarkMap2.Text = dGV_Badmark.Rows[selecIndex].Cells[5].Value.ToString();
                }
            }
        }
        #endregion

        #region 读条码 相关操作
        private void bAddCode_Click(object sender, EventArgs e)
        {
            //获取当前选定的Index
            if (dGV_Barcode.SelectedRows.Count > 0 && (dGV_Barcode.SelectedRows[0].Index != dGV_Barcode.Rows.Count - 1))
            {
                dGV_Barcode.Rows.Insert(dGV_Barcode.SelectedRows[0].Index + 1, 1);
                Common.CommonHelper.AddRowHeader(dGV_Barcode);
                DataGridViewRow item = dGV_Barcode.Rows[dGV_Barcode.SelectedRows[0].Index + 1];
                item.Cells[0].Value = "2D_QR";
                item.Cells[1].Value = "0";
                item.Cells[2].Value = "0";
                item.Cells[3].Value = "0";
                item.Cells[4].Value = "0";
                item.Cells[5].Value = "0";
                item.Cells[6].Value = "1";
                item.Cells[7].Value = "0";
            }
            else
            {
                dGV_Barcode.Rows.Insert(0, 1);
                Common.CommonHelper.AddRowHeader(dGV_Barcode);
                DataGridViewRow item = dGV_Barcode.Rows[0];
                item.Cells[0].Value = "2D_QR";
                item.Cells[1].Value = "0";
                item.Cells[2].Value = "0";
                item.Cells[3].Value = "0";
                item.Cells[4].Value = "0";
                item.Cells[5].Value = "0";
                item.Cells[6].Value = "1";
                item.Cells[7].Value = "0";
            }
        }

        private void DeleGVRow(DataGridView view)
        {
            if (view.SelectedRows.Count > 0)
            {
                //获取当前选中的行（单行或者多行）
                DialogResult RSS;
                RSS = MessageBox.Show(this, "确定要删除选中行数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                switch (RSS)
                {
                    case DialogResult.Yes:
                        //获取当前选定的Index
                        for (int i = view.SelectedRows.Count; i > 0; i--)
                        {
                            if (view.SelectedRows[i - 1].Index == (view.RowCount - 1))
                            {
                                msgDiv1.MsgDivShow("末尾行无数据不可删除!", 1);
                                break;
                            }
                            int Index = view.SelectedRows[i - 1].Index;
                            view.Rows.RemoveAt(Index);
                        }
                        Common.CommonHelper.AddRowHeader(view);
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }

        private void UpRow(DataGridView view)
        {
            if (view.SelectedRows.Count > 0 && (view.SelectedRows[0].Index != view.RowCount - 1))
            {
                int index = view.SelectedRows[0].Index;
                if (index == 0)
                {
                    return;
                }
                List<string> list = new List<string>();
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    list.Add(view.SelectedRows[0].Cells[i].Value.ToString());   //把当前选中行的数据存入list数组中  
                }
                for (int j = 0; j < view.Columns.Count; j++)
                {
                    view.Rows[index].Cells[j].Value = view.Rows[index - 1].Cells[j].Value;
                    view.Rows[index - 1].Cells[j].Value = list[j].ToString();
                }
                view.Rows[index].Selected = false;
                view.Rows[index - 1].Selected = true;
            }
        }

        private void DownRow(DataGridView view)
        {
            if (view.SelectedRows.Count > 0 && (view.SelectedRows[0].Index != view.RowCount - 1))
            {
                int index = view.SelectedRows[0].Index;
                if (index == view.RowCount - 2)
                {
                    return;
                }
                List<string> list = new List<string>();
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    list.Add(view.SelectedRows[0].Cells[i].Value.ToString());   //把当前选中行的数据存入list数组中  
                }

                for (int j = 0; j < view.Columns.Count; j++)
                {
                    view.Rows[index].Cells[j].Value = view.Rows[index + 1].Cells[j].Value;
                    view.Rows[index + 1].Cells[j].Value = list[j].ToString();
                }
                view.Rows[index + 1].Selected = true;
                view.Rows[index].Selected = false;
            }
        }

        private void bDelCode_Click(object sender, EventArgs e)
        {
            this.DeleGVRow(this.dGV_Barcode);
        }

        private void bUpCode_Click(object sender, EventArgs e)
        {
            this.UpRow(this.dGV_Barcode);
        }

        private void bDownCode_Click(object sender, EventArgs e)
        {
            this.DownRow(this.dGV_Barcode);
        }

        private void bRecrodeCodePos_Click(object sender, EventArgs e)
        {
            if (dGV_Barcode.SelectedRows.Count > 0 && (dGV_Barcode.SelectedRows[0].Index != dGV_Barcode.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Barcode.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Barcode.SelectedRows[i_Temp].Index != dGV_Barcode.Rows.Count - 1)
                    {
                        int selecIndex = dGV_Barcode.SelectedRows[i_Temp].Index;
                        int module = this.cbReadCodeModule.SelectedIndex;
                        dGV_Barcode.Rows[selecIndex].Cells[1].Value = GeneralSystem.Instance.Machines[module].CurPos.X.ToString("F3");
                        dGV_Barcode.Rows[selecIndex].Cells[2].Value = GeneralSystem.Instance.Machines[module].CurPos.Y.ToString("F3");
                    }
                }
            }
        }

        private void bMoveReadPos_Click(object sender, EventArgs e)
        {
            if (!GeneralSystem.Instance.Machines[this.cbReadCodeModule.SelectedIndex].Other.IsOutSafeArea())
            {
                return;
            }

            {
                if (dGV_Barcode.SelectedRows.Count > 0 && (dGV_Barcode.SelectedRows[0].Index != dGV_Barcode.RowCount - 1))
                {
                    try
                    {
                        var RSS = MessageBox.Show(this, "确定要移动到点吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        switch (RSS)
                        {
                            case DialogResult.Yes:
                                PointF TEMP = new PointF();
                                TEMP.X = float.Parse(dGV_Barcode.SelectedRows[0].Cells[1].Value.ToString());
                                TEMP.Y = float.Parse(dGV_Barcode.SelectedRows[0].Cells[2].Value.ToString());
                                GeneralSystem.Instance.Machines[this.cbReadCodeModule.SelectedIndex].XYGoPos(TEMP, SpeedMode.Manual_Normal);
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
        }

        private void bExpendCode1_Click(object sender, EventArgs e)
        {
            frm_Expand frm = new frm_Expand();
            frm.Show();
        }

        private void bExpendCode2_Click(object sender, EventArgs e)
        {
            if (Variable.bExpand)
            {
                if (dGV_Barcode.SelectedRows.Count > 0 && dGV_Barcode.SelectedRows[0].Index != dGV_Barcode.Rows.Count - 1)
                {
                    List<PointF> Point2Expand_Mark1 = new List<PointF>();
                    for (int ii = 0; ii < dGV_Barcode.SelectedRows.Count; ii++)
                    {
                        PointF pt = new PointF();
                        pt.X = float.Parse(dGV_Barcode.SelectedRows[ii].Cells[1].Value.ToString());
                        pt.Y = float.Parse(dGV_Barcode.SelectedRows[ii].Cells[2].Value.ToString());
                        Point2Expand_Mark1.Add(pt);
                    }

                    PointF[] Points2Add_Mark1 = Common.MathHelper.Expand2AddPoints(
                        Point2Expand_Mark1.ToArray(),
                        Variable.pExpand_O,
                        Variable.pExpand_B,
                        Variable.pExpand_A,
                        Variable.iExpand_N,
                        Variable.iExpand_M);

                    //AddPasteInfoPoints(Points2Add_Mark1, Points2Add_Mark2);
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


        /// <summary>
        /// 更新图像信息
        /// </summary>
        /// <returns></returns>
        private bool UpdateImage_Roi()
        {
            try
            {
                Hroi?.Dispose();
                imageSet?.Dispose();
                frm_Main.frm_Camera.imageSet.Image.BorderWidth = 0;
                imageSet = frm_Main.Image(frm_Main.frm_Camera.imageSet.Image);
                roi = frm_Main.CROI(frm_Main.frm_Camera.imageSet.Roi);
                Hroi = frm_Main.HROI(frm_Main.frm_Camera.imageSet.Roi);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private HObject Hroi = null;
        private HObject imageSet = null;
        private Vision.RectangleRegion roi = new Vision.RectangleRegion();

        private void bReadCode_Click(object sender, EventArgs e)
        {
            this.UpdateImage_Roi();
            string code = string.Empty;
            HObject box = null;
            Vision.BarcodeType type = Vision.BarcodeType.Type_2D_DataMatrix;

            if (this.cbCodeType.SelectedIndex == 0)
            {
                type = Vision.BarcodeType.Type_1D_128;
            }
            else if(this.cbCodeType.SelectedIndex == 1)
            {
                type = Vision.BarcodeType.Type_1D_39;
            }
            else if (this.cbCodeType.SelectedIndex == 2)
            {
                type = Vision.BarcodeType.Type_2D_DataMatrix;
            }
            else if (this.cbCodeType.SelectedIndex == 3)
            {
                type = Vision.BarcodeType.Type_2D_QR;
            }

            Vision.ReadBarcode(imageSet, Hroi, type, out code, out box);
            box?.Dispose();
            if(code!= string.Empty)
            {
                MessageBox.Show(code);
            }
        }

        private void bCodeFlyOpimze_Click(object sender, EventArgs e)
        {

        }

        private void bCodeFlyTest_Click(object sender, EventArgs e)
        {

        }

        private void bSetBarcodeMap_Click(object sender, EventArgs e)
        {
            if (dGV_Barcode.SelectedRows.Count > 0 && (dGV_Barcode.SelectedRows[0].Index != dGV_Barcode.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Barcode.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Barcode.SelectedRows[i_Temp].Index != dGV_Barcode.Rows.Count - 1)
                    {
                        int selecIndex = dGV_Barcode.SelectedRows[i_Temp].Index;
                        dGV_Barcode.Rows[selecIndex].Cells[0].Value = this.cbCodeType.Text;
                        dGV_Barcode.Rows[selecIndex].Cells[4].Value = this.tBarCodeMap1.Text;
                        dGV_Barcode.Rows[selecIndex].Cells[5].Value = this.tBarCodeMap2.Text;
                    }
                }
            }
        }

        private void dGV_Barcode_SelectionChanged(object sender, EventArgs e)
        {
            if (dGV_Barcode.SelectedRows.Count > 0 && (dGV_Barcode.SelectedRows[0].Index != dGV_Barcode.RowCount - 1))
            {
                int selecIndex = dGV_Barcode.SelectedRows[0].Index;
                if(dGV_Barcode.Rows[selecIndex].Cells[0].Value != null)
                {
                    this.cbCodeType.Text = dGV_Barcode.Rows[selecIndex].Cells[0].Value.ToString();
                    this.tBarCodeMap1.Text = dGV_Barcode.Rows[selecIndex].Cells[4].Value.ToString();
                    this.tBarCodeMap2.Text = dGV_Barcode.Rows[selecIndex].Cells[5].Value.ToString();
                    this.tGain1.Text = dGV_Barcode.Rows[selecIndex].Cells[6].Value.ToString();
                    this.tOffset1.Text = dGV_Barcode.Rows[selecIndex].Cells[7].Value.ToString();
                }
            }
        }
        #endregion

        private void bSetProcess1_Click(object sender, EventArgs e)
        {
            if (dGV_Barcode.SelectedRows.Count > 0 && (dGV_Barcode.SelectedRows[0].Index != dGV_Barcode.RowCount - 1))
            {
                for (int i_Temp = 0; i_Temp < dGV_Barcode.SelectedRows.Count; i_Temp++)
                {
                    if (dGV_Barcode.SelectedRows[i_Temp].Index != dGV_Barcode.Rows.Count - 1)
                    {
                        int selecIndex = dGV_Barcode.SelectedRows[i_Temp].Index;
                        dGV_Barcode.Rows[selecIndex].Cells[6].Value = this.tGain1.Text;
                        dGV_Barcode.Rows[selecIndex].Cells[7].Value = this.tOffset1.Text;
                    }
                }
            }
        }

        private void bCalDown_Click(object sender, EventArgs e)
        {
            if (dGV_Feeder.SelectedRows.Count > 0 && (dGV_Feeder.SelectedRows[0].Index != dGV_Feeder.RowCount - 1))
            {
                try
                {
                    int module = this.cB_Module.SelectedIndex;
                    string visionName = dGV_Feeder.SelectedRows[0].Cells[1].Value.ToString();
                    if (visionName == "-1" || string.IsNullOrEmpty(visionName)) return;
                    VisionImage image = new VisionImage();
                    HObject hImage = new HObject();
                    hImage = frm_Main.Image(frm_Main.frm_Camera.imageSet.Image);

                    VisionInfo visionInfo = Variable.LoadVisionInfo(1, frm_Main.frm_Program.ProgramName, visionName);
                    Vision.GainOffset(visionInfo.T1_PreProcess_Gain, visionInfo.T1_PreProcess_Offset, ref hImage);

                    if (frm_Main.DetectFOV1(hImage, ref visionInfo, out GeneralSystem.Instance.Machines[module].UpVisionHelper.ShowList,
                        out GeneralSystem.Instance.Machines[module].UpVisionHelper.Colors))
                    {
                        PointF pt = GeneralSystem.Instance.Machines[module].CurPos;
                        PointF resultPt = new PointF();
                        double resultAngle = 0;
                        PointF rotatedPoint = new PointF();

                        frm_Main.CalResult(module, 2, visionInfo, pt, pt, ref resultPt, ref resultAngle);
                        GeneralSystem.Instance.Machines[module].RMap[0].GoPos(GeneralSystem.Instance.Machines[module].RMap[0].Pos +
                            resultAngle, new VelMode(0, 50, 100, 100));
                        GeneralSystem.Instance.Machines[module].PtRotate(resultPt, GeneralSystem.Instance.Machines[module].ZMap[0].Cali_RotateCenter, -resultAngle, out rotatedPoint);

                        GeneralSystem.Instance.Machines[module].XYGoPos(rotatedPoint, SpeedMode.Manual_Normal);
                    }
                    else
                    {
                        MessageBox.Show("识别Mark点出错!!!");
                    }

                    image?.Dispose();
                    hImage?.Dispose();
                }
                catch { }
            }
            else
            {
                MessageBox.Show("请选中需要识别的Mark点!!!");
            }
        }
    }
}
