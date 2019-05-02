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
using GeneralMachine.Common;
using NationalInstruments.Vision.WindowsForms;
using System.Threading;
using ZedGraph;
using GeneralMachine.Definition;
using GeneralMachine.Error;
using System.Diagnostics;
using GeneralMachine.Config;

namespace GeneralMachine
{
    public partial class frm_Auto : DockContent
    {
        public frm_Main frm_Main = null;
        public frm_Auto(object obj)
        {
            InitializeComponent();
            frm_Main = (frm_Main)obj;
        }

        private void frm_Auto_Load(object sender, EventArgs e)
        {
            GeneralSystem.Instance.Machines[0].UpVisionHelper.ImageWindow = this.hWindowUp.HalconWindow;
            GeneralSystem.Instance.Machines[1].UpVisionHelper.ImageWindow = this.hWindowUp.HalconWindow;
            GeneralSystem.Instance.Machines[0].DownVisionManager.ImageWindow = this.hFrontDownWindow.HalconWindow;
            GeneralSystem.Instance.Machines[1].DownVisionManager.ImageWindow = this.hBackDownWindow.HalconWindow;

            this.cb_EnModule1.Checked = !GeneralSystem.Instance.Machines[0].Config.EN_Runing;
            this.cb_EnModule2.Checked = !GeneralSystem.Instance.Machines[1].Config.EN_Runing;

            this.hWindowUp.ImagePart = new Rectangle(0, 0, GeneralSystem.Instance.Machines[0].ROI_Up.Width, GeneralSystem.Instance.Machines[0].ROI_Up.Height);
            this.hFrontDownWindow.ImagePart = new Rectangle(0, 0, GeneralSystem.Instance.Machines[0].ROI_Down.Width, GeneralSystem.Instance.Machines[0].ROI_Down.Height);
            this.hBackDownWindow.ImagePart = new Rectangle(0, 0, GeneralSystem.Instance.Machines[0].ROI_Down.Width, GeneralSystem.Instance.Machines[0].ROI_Down.Height);
            InitUI();

        }

        /// <summary>
        /// UI初始化
        /// </summary>
        private void InitUI()
        {
            #region 统计
            dGV_Statistics.Rows.Add(17);
            dGV_Statistics.Rows[0].HeaderCell.Value = "CT(PCB)";
            dGV_Statistics.Rows[0].Cells[0].Value = "000.000";
            dGV_Statistics.Rows[0].Cells[1].Value = "秒";
            dGV_Statistics.Rows[1].HeaderCell.Value = "PCB总数量";
            dGV_Statistics.Rows[1].Cells[0].Value = "000000";
            dGV_Statistics.Rows[1].Cells[1].Value = "个";
            dGV_Statistics.Rows[2].HeaderCell.Value = "PCB生产数量";
            dGV_Statistics.Rows[2].Cells[0].Value = "000000";
            dGV_Statistics.Rows[2].Cells[1].Value = "个";
            dGV_Statistics.Rows[3].HeaderCell.Value = "标签总数量";
            dGV_Statistics.Rows[3].Cells[0].Value = "000000";
            dGV_Statistics.Rows[3].Cells[1].Value = "个";
            dGV_Statistics.Rows[4].HeaderCell.Value = "标签生产数量";
            dGV_Statistics.Rows[4].Cells[0].Value = "000000";
            dGV_Statistics.Rows[4].Cells[1].Value = "个";
            dGV_Statistics.Rows[5].HeaderCell.Value = "标签抛料率";
            dGV_Statistics.Rows[5].Cells[0].Value = "000.00";
            dGV_Statistics.Rows[5].Cells[1].Value = "%";
            dGV_Statistics.Rows[6].HeaderCell.Value = "机台稼动率";
            dGV_Statistics.Rows[6].Cells[0].Value = "000.00";
            dGV_Statistics.Rows[6].Cells[1].Value = "%";
            dGV_Statistics.Rows[7].HeaderCell.Value = "单标来回";
            dGV_Statistics.Rows[7].Cells[0].Value = "0.000";
            dGV_Statistics.Rows[7].Cells[1].Value = "秒";
            dGV_Statistics.Rows[8].HeaderCell.Value = "开机时间";
            dGV_Statistics.Rows[8].Cells[0].Value = "000:00";
            dGV_Statistics.Rows[8].Cells[1].Value = "时:分";

            dGV_Statistics.Rows[9].HeaderCell.Value = "前模组Z1吸标次数";
            dGV_Statistics.Rows[9].Cells[0].Value = "0";
            dGV_Statistics.Rows[9].Cells[1].Value = "次";

            dGV_Statistics.Rows[10].HeaderCell.Value = "前模组Z1抛料次数";
            dGV_Statistics.Rows[10].Cells[0].Value = "0";
            dGV_Statistics.Rows[10].Cells[1].Value = "次";

            dGV_Statistics.Rows[11].HeaderCell.Value = "前模组Z2吸标次数";
            dGV_Statistics.Rows[11].Cells[0].Value = "0";
            dGV_Statistics.Rows[11].Cells[1].Value = "次";

            dGV_Statistics.Rows[12].HeaderCell.Value = "前模组Z2抛料次数";
            dGV_Statistics.Rows[12].Cells[0].Value = "0";
            dGV_Statistics.Rows[12].Cells[1].Value = "次";

            dGV_Statistics.Rows[13].HeaderCell.Value = "后模组Z1吸标次数";
            dGV_Statistics.Rows[13].Cells[0].Value = "0";
            dGV_Statistics.Rows[13].Cells[1].Value = "次";

            dGV_Statistics.Rows[14].HeaderCell.Value = "后模组Z1抛料次数";
            dGV_Statistics.Rows[14].Cells[0].Value = "0";
            dGV_Statistics.Rows[14].Cells[1].Value = "次";

            dGV_Statistics.Rows[15].HeaderCell.Value = "后模组Z2吸标次数";
            dGV_Statistics.Rows[15].Cells[0].Value = "0";
            dGV_Statistics.Rows[15].Cells[1].Value = "次";

            dGV_Statistics.Rows[16].HeaderCell.Value = "后模组Z2抛料次数";
            dGV_Statistics.Rows[16].Cells[0].Value = "0";
            dGV_Statistics.Rows[16].Cells[1].Value = "次";

            CommonHelper.InitDGV(dGV_Statistics, 12);
            #endregion
            CommonHelper.InitDGV(dGV_Feeder, 12);
        }

        private void frm_Auto_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if(Variable.AlmProgram.ALMProName == string.Empty || Variable.AlmProgram.ALMProName == null)
            {
                MessageBox.Show("请先导入程式后再运行!!!");
                return;
            }

            if (GeneralSystem.Instance.Machines[0].IsRunning || GeneralSystem.Instance.Machines[1].IsRunning)
            {
                MessageBox.Show("机器正在运行");
                return;
            }

            this.cB_EN.Enabled = false;
            this.bClearOut.Enabled = false;
            this.bReset.Enabled = false;
            this.cb_EnModule1.Enabled = false;
            this.cb_EnModule2.Enabled = false;

            this.bHalt.BackColor = Color.White;
            this.bStart.BackColor = Color.Green;
            ErrorHelper.Instance.WriteLog(LogType.MachineStart);

            if (!GeneralSystem.Instance.Machines[0].IsSafeHeight
                || !GeneralSystem.Instance.Machines[1].IsSafeHeight)
            {
                MessageBox.Show("机器不在安全高度!!!");
                GeneralSystem.Instance.Machines[0].ZGoSafeHeight(Definition.SpeedMode.Manual_Normal);
                GeneralSystem.Instance.Machines[1].ZGoSafeHeight(Definition.SpeedMode.Manual_Normal);
                return;
            }

            GeneralSystem.Instance.Machines[0].XYGoPos(GeneralSystem.Instance.Machines[0].Config.ReadyPoint, Definition.SpeedMode.Manual_Normal);
            GeneralSystem.Instance.Machines[1].XYGoPos(GeneralSystem.Instance.Machines[1].Config.ReadyPoint, Definition.SpeedMode.Manual_Normal);

            Thread.Sleep(100);
            while (!GeneralSystem.Instance.Machines[0].XYReach(GeneralSystem.Instance.Machines[0].Config.ReadyPoint) ||
                !GeneralSystem.Instance.Machines[1].XYReach(GeneralSystem.Instance.Machines[1].Config.ReadyPoint))
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            try
            {
                GeneralSystem.Instance.Machines[0].RUN_PasteInfo = new Definition.MutiBaseInfo();
                GeneralSystem.Instance.Machines[1].RUN_PasteInfo = new Definition.MutiBaseInfo();

                GeneralSystem.Instance.Machines[0].FeederList = Variable.AlmProgram.FeederInfo_Module1.Values.ToList();
                GeneralSystem.Instance.Machines[1].FeederList = Variable.AlmProgram.FeederInfo_Module2.Values.ToList();
                GeneralSystem.Instance.Machines[0].RUN_PasteInfo.BaseInfoList = new List<Definition.BaseInfo>();
                GeneralSystem.Instance.Machines[1].RUN_PasteInfo.BaseInfoList = new List<Definition.BaseInfo>();
                GeneralSystem.Instance.Machines[0].DownParam = Variable.AlmProgram.DownCapture_Module1;
                GeneralSystem.Instance.Machines[1].DownParam = Variable.AlmProgram.DownCapture_Module2;

                GeneralSystem.Instance.Machines[0].RUN_PasteInfo.OffsetX = new SerializableDictionary<int, List<double>>();
                GeneralSystem.Instance.Machines[0].RUN_PasteInfo.OffsetX.Add(0, new List<double>());

                GeneralSystem.Instance.Machines[0].RUN_PasteInfo.OffsetY = new SerializableDictionary<int, List<double>>();
                GeneralSystem.Instance.Machines[0].RUN_PasteInfo.OffsetY.Add(0, new List<double>());

                GeneralSystem.Instance.Machines[0].RUN_PasteInfo.OffsetR = new SerializableDictionary<int, List<double>>();
                GeneralSystem.Instance.Machines[0].RUN_PasteInfo.OffsetR.Add(0, new List<double>());
                GeneralSystem.Instance.Machines[0].RUN_PasteInfo.OffsetFly = new List<double>();

                GeneralSystem.Instance.Machines[1].RUN_PasteInfo.OffsetX = new SerializableDictionary<int, List<double>>();
                GeneralSystem.Instance.Machines[1].RUN_PasteInfo.OffsetX.Add(0, new List<double>());

                GeneralSystem.Instance.Machines[1].RUN_PasteInfo.OffsetY = new SerializableDictionary<int, List<double>>();
                GeneralSystem.Instance.Machines[1].RUN_PasteInfo.OffsetY.Add(0, new List<double>());

                GeneralSystem.Instance.Machines[1].RUN_PasteInfo.OffsetR = new SerializableDictionary<int, List<double>>();
                GeneralSystem.Instance.Machines[1].RUN_PasteInfo.OffsetR.Add(0, new List<double>());
                GeneralSystem.Instance.Machines[1].RUN_PasteInfo.OffsetFly = new List<double>();

                try
                {
                    GeneralSystem.Instance.Machines[0]._session_Up.Acquisition.Unconfigure();
                    GeneralSystem.Instance.Machines[0]._session_Up.ConfigureGrab();

                    GeneralSystem.Instance.Machines[1]._session_Up.Acquisition.Unconfigure();
                    GeneralSystem.Instance.Machines[1]._session_Up.ConfigureGrab();

                    GeneralSystem.Instance.Machines[0]._session_Down.Acquisition.Unconfigure();
                    GeneralSystem.Instance.Machines[0]._session_Down.ConfigureGrab();

                    GeneralSystem.Instance.Machines[1]._session_Down.Acquisition.Unconfigure();
                    GeneralSystem.Instance.Machines[1]._session_Down.ConfigureGrab();
                }
                catch { }
     
                // 分配板子
                for (int i = 0; i < Variable.AlmProgram.MutiBaseInfo.VisionProName_Base.Count; ++i)
                {
                    if (Variable.AlmProgram.MutiBaseInfo.BaseInfoList[i].Base_Module[0] == 1)
                    {
                        GeneralSystem.Instance.Machines[0].RUN_PasteInfo.FOVCount_Base.Add(Variable.AlmProgram.MutiBaseInfo.FOVCount_Base[i]);
                        GeneralSystem.Instance.Machines[0].RUN_PasteInfo.BaseInfoList.Add(Variable.AlmProgram.MutiBaseInfo.BaseInfoList[i]);
                        GeneralSystem.Instance.Machines[0].RUN_PasteInfo.Cam_Mark1Point.Add(Variable.AlmProgram.MutiBaseInfo.Cam_Mark1Point[i]);
                        GeneralSystem.Instance.Machines[0].RUN_PasteInfo.Cam_Mark2Point.Add(Variable.AlmProgram.MutiBaseInfo.Cam_Mark2Point[i]);
                        GeneralSystem.Instance.Machines[0].RUN_PasteInfo.VisionProName_Base.Add(Variable.AlmProgram.MutiBaseInfo.VisionProName_Base[i]);

                        GeneralSystem.Instance.Machines[0].RUN_PasteInfo.OffsetX[0].Add(Variable.AlmProgram.MutiBaseInfo.OffsetX[0][i]);
                        GeneralSystem.Instance.Machines[0].RUN_PasteInfo.OffsetY[0].Add(Variable.AlmProgram.MutiBaseInfo.OffsetY[0][i]);
                        GeneralSystem.Instance.Machines[0].RUN_PasteInfo.OffsetR[0].Add(Variable.AlmProgram.MutiBaseInfo.OffsetR[0][i]);
                        GeneralSystem.Instance.Machines[0].RUN_PasteInfo.OffsetFly.Add(Variable.AlmProgram.MutiBaseInfo.OffsetFly[i]);
                    }
                    else
                    {
                        GeneralSystem.Instance.Machines[1].RUN_PasteInfo.FOVCount_Base.Add(Variable.AlmProgram.MutiBaseInfo.FOVCount_Base[i]);
                        GeneralSystem.Instance.Machines[1].RUN_PasteInfo.BaseInfoList.Add(Variable.AlmProgram.MutiBaseInfo.BaseInfoList[i]);
                        GeneralSystem.Instance.Machines[1].RUN_PasteInfo.Cam_Mark1Point.Add(Variable.AlmProgram.MutiBaseInfo.Cam_Mark1Point[i]);
                        GeneralSystem.Instance.Machines[1].RUN_PasteInfo.Cam_Mark2Point.Add(Variable.AlmProgram.MutiBaseInfo.Cam_Mark2Point[i]);
                        GeneralSystem.Instance.Machines[1].RUN_PasteInfo.VisionProName_Base.Add(Variable.AlmProgram.MutiBaseInfo.VisionProName_Base[i]);

                        GeneralSystem.Instance.Machines[1].RUN_PasteInfo.OffsetX[0].Add(Variable.AlmProgram.MutiBaseInfo.OffsetX[1][i]);
                        GeneralSystem.Instance.Machines[1].RUN_PasteInfo.OffsetY[0].Add(Variable.AlmProgram.MutiBaseInfo.OffsetY[1][i]);
                        GeneralSystem.Instance.Machines[1].RUN_PasteInfo.OffsetR[0].Add(Variable.AlmProgram.MutiBaseInfo.OffsetR[1][i]);
                        GeneralSystem.Instance.Machines[1].RUN_PasteInfo.OffsetFly.Add(Variable.AlmProgram.MutiBaseInfo.OffsetFly[i]);
                    }
                }

                GeneralSystem.Instance.Machines[0].IsMain = true;
                GeneralSystem.Instance.Machines[1].IsMain = true;

                lock (ConveryManager.Instance.lockobj)
                {
                    if(ConveryManager.Instance.CurrentStatus == ConveryManager.ConveryStatus.Idle)
                    {
                        ConveryManager.Instance.FlowInit = false;
                        ConveryManager.Instance.CurrentStatus = ConveryManager.ConveryStatus.Input;
                    }
                }

                if(!GeneralSystem.Instance.Machines[0].Config.EN_Runing &&
                    !GeneralSystem.Instance.Machines[1].Config.EN_Runing)
                {
                    frm_MessageBox fm = new frm_MessageBox(frm_Main, "两个模组都被屏蔽，请开启！！！", false, true);
                    fm.ShowDialog();
                    return;
                }

                if(GeneralSystem.Instance.Machines[0].Config.EN_Runing)
                {
                    GeneralSystem.Instance.Machines[0].StartMachine();
                }
                else
                {
                    GeneralSystem.Instance.Machines[0].PasueMachine();
                }

                if (GeneralSystem.Instance.Machines[1].Config.EN_Runing)
                {
                    GeneralSystem.Instance.Machines[1].StartMachine();
                }
                else
                {
                    GeneralSystem.Instance.Machines[1].PasueMachine();
                }

                ConveryManager.Instance.StartConvery();
                this.timerRefresh.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"程序自动运行失败{ex.Message}");
                return;
            }
        }

        public void bHalt_Click(object sender, EventArgs e)
        {
            Convery.ConveryManager.Instance.CT.Stop();

            this.bStart.BackColor = Color.White;
            this.bHalt.BackColor = Color.Green;

            ErrorHelper.Instance.WriteLog(LogType.MachinePause);
            this.cB_EN.Checked = false;
            this.cB_EN.Enabled = true;
            this.cb_EnModule1.Enabled = true;
            this.cb_EnModule2.Enabled = true;

            GeneralSystem.Instance.Machines[0].PasueMachine();
            GeneralSystem.Instance.Machines[1].PasueMachine();

            try
            {
                // 设置外触发
                GeneralSystem.Instance.Machines[0]._session_Up.Acquisition.Unconfigure();
                GeneralSystem.Instance.Machines[1]._session_Up.Acquisition.Unconfigure();
                GeneralSystem.Instance.Machines[0].FlyTool.ClearImage();
                GeneralSystem.Instance.Machines[1].FlyTool.ClearImage();
                frm_Main.SetCameraTrigger_Balser(GeneralSystem.Instance.Machines[0]._session_Up, true);
                frm_Main.SetCameraTrigger_Balser(GeneralSystem.Instance.Machines[1]._session_Up, true);
            }
            catch { }
 
            ConveryManager.Instance.StopConvery();
            this.timerRefresh.Stop();
            GC.Collect();
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("是否需要 复位机台!!!","Tips", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Thread thread_SystemInit = new Thread(new ThreadStart(frm_Main.SystemInit));
                thread_SystemInit.Start();
                frm_Main.frm_Loading = new frm_Loading(frm_Main);
                frm_Main.frm_Loading.ShowDialog();
            }
        }

        private void bClearOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否请料!!!", "Tips", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (!GeneralSystem.Instance.Machines[0].IsRunning && !GeneralSystem.Instance.Machines[1].IsRunning)
                {
                    ConveryManager.Instance.StopConvery();

                    ErrorHelper.Instance.WriteLog(LogType.MachineStop);

                    GeneralSystem.Instance.Machines[0].RUN_SuckTimes_Z1 = 0;
                    GeneralSystem.Instance.Machines[0].RUN_DropTimes_Z1 = 0;
                    GeneralSystem.Instance.Machines[0].RUN_SuckTimes_Z2 = 0;
                    GeneralSystem.Instance.Machines[0].RUN_DropTimes_Z2 = 0;

                    GeneralSystem.Instance.Machines[1].RUN_SuckTimes_Z1 = 0;
                    GeneralSystem.Instance.Machines[1].RUN_DropTimes_Z1 = 0;
                    GeneralSystem.Instance.Machines[1].RUN_SuckTimes_Z2 = 0;
                    GeneralSystem.Instance.Machines[1].RUN_DropTimes_Z2 = 0;

                    Convery.ConveryManager.Instance.CT.Stop();
                    Convery.ConveryManager.Instance.CT.Reset();
                    GeneralSystem.Instance.Machines[0].ZGoSafeHeight(Definition.SpeedMode.Manual_Normal);
                    GeneralSystem.Instance.Machines[1].ZGoSafeHeight(Definition.SpeedMode.Manual_Normal);
                    Thread.Sleep(100);

                    while (!GeneralSystem.Instance.Machines[0].IsSafeHeight
                       || !GeneralSystem.Instance.Machines[1].IsSafeHeight)
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }


                    PointF pt1 = new PointF();
                    PointF pt2 = new PointF();
                    pt1.X = (float)GeneralSystem.Instance.Machines[0].Config.ThrowPointZ1List[0].X;
                    pt1.Y = (float)GeneralSystem.Instance.Machines[0].Config.ThrowPointZ1List[0].Y;
                    pt2.X = (float)GeneralSystem.Instance.Machines[1].Config.ThrowPointZ1List[0].X;
                    pt2.Y = (float)GeneralSystem.Instance.Machines[1].Config.ThrowPointZ1List[0].Y;
                    GeneralSystem.Instance.Machines[0].XYGoPos(pt1, SpeedMode.Manual_Normal);
                    GeneralSystem.Instance.Machines[1].XYGoPos(pt2, SpeedMode.Manual_Normal);
                    while (!GeneralSystem.Instance.Machines[0].XYReach(pt1)
                        || !GeneralSystem.Instance.Machines[1].XYReach(pt2))
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }

                    GeneralSystem.Instance.Machines[0].CurrentStatus = MachineEntiy.MachineStatus.FlowReay;
                    GeneralSystem.Instance.Machines[0].AllProductPasteCompleted = false;
                    GeneralSystem.Instance.Machines[1].CurrentStatus = MachineEntiy.MachineStatus.FlowReay;
                    GeneralSystem.Instance.Machines[1].AllProductPasteCompleted = false;
                    
                    GeneralSystem.Instance.Machines[0].Config.XI_Index[0] = 0;
                    GeneralSystem.Instance.Machines[0].Config.XI_Index[1] = 0;
                    GeneralSystem.Instance.Machines[0].Config.XI_Index[2] = 0;
                    GeneralSystem.Instance.Machines[0].Config.XI_Index[3] = 0;
                    GeneralSystem.Instance.Machines[1].Config.XI_Index[0] = 0;
                    GeneralSystem.Instance.Machines[1].Config.XI_Index[1] = 0;
                    GeneralSystem.Instance.Machines[1].Config.XI_Index[2] = 0;
                    GeneralSystem.Instance.Machines[1].Config.XI_Index[3] = 0;
                    Variable.Conveyor.Stop();

                    lock (ConveryManager.Instance.lockobj)
                    {
                        ConveryManager.Instance.RUN_bReachOK = false;
                        ConveryManager.Instance.FlowInit = false;
                        ConveryManager.Instance.LastStatus = ConveryManager.ConveryStatus.Input;
                        ConveryManager.Instance.CurrentStatus = ConveryManager.ConveryStatus.Input;
                    }

                    GeneralSystem.Instance.Machines[0].ZMap[0].XI_vaccum.ResetIO();
                    GeneralSystem.Instance.Machines[0].ZMap[1].XI_vaccum.ResetIO();
                    GeneralSystem.Instance.Machines[1].ZMap[0].XI_vaccum.ResetIO();
                    GeneralSystem.Instance.Machines[1].ZMap[1].XI_vaccum.ResetIO();
                    GeneralSystem.Instance.Machines[0].ZMap[0].PO_vaccum.SetIO();
                    GeneralSystem.Instance.Machines[0].ZMap[1].PO_vaccum.SetIO();
                    GeneralSystem.Instance.Machines[1].ZMap[0].PO_vaccum.SetIO();
                    GeneralSystem.Instance.Machines[1].ZMap[1].PO_vaccum.SetIO();
                    ConveryManager.Instance.OUT_Stop?.ResetIO();
                    Thread.Sleep(100);
                    ConveryManager.Instance.OUT_Carry?.ResetIO();

                    Thread.Sleep(1500);
                    GeneralSystem.Instance.Machines[0].ZMap[0].PO_vaccum.ResetIO();
                    GeneralSystem.Instance.Machines[0].ZMap[1].PO_vaccum.ResetIO();

                    GeneralSystem.Instance.Machines[1].ZMap[0].PO_vaccum.ResetIO();
                    GeneralSystem.Instance.Machines[1].ZMap[1].PO_vaccum.ResetIO();


                    GeneralSystem.Instance.Machines[0].UpVisionHelper.MarkResultList.Clear();
                    GeneralSystem.Instance.Machines[1].UpVisionHelper.MarkResultList.Clear();
                    GeneralSystem.Instance.Machines[0].DownVisionManager.ResetQueue();
                    GeneralSystem.Instance.Machines[1].DownVisionManager.ResetQueue();

                    if(ConveryManager.Instance.IN_ConveryOutput != null)
                    {
                        Stopwatch watch = new Stopwatch();
                        watch.Start();
                        Variable.Conveyor.Jog(Variable.VelMode_RunMode_Conveyor, ConveryManager.Instance.InFlowDir);
                        while (!ConveryManager.Instance.IN_ConveryOutput.GetIO() && watch.ElapsedMilliseconds < 3000)
                        {
                            Thread.Sleep(100);
                            Application.DoEvents();
                        }

                        Variable.Conveyor.Stop();
                    }
                }
            }
        }

        public void RefreshPasteUI(int moduleID, GraphPane panel)
        {
            MutiBaseInfo baseInfo = GeneralSystem.Instance.Machines[moduleID].RUN_PasteInfo;
            BaseInfo[] items = GeneralSystem.Instance.Machines[moduleID].baseInfoArray;
            if (items != null)
            {
                panel.GraphObjList.Clear();
                for (int i = 0; i < baseInfo.Cam_Mark1Point.Count; ++i)
                {
                    foreach (var item in items)
                    {
                        if (item.Base_PasteXY != null)
                        {
                            for (int index = 0; index < item.Base_PasteXY.Length; index++)
                            {
                                PointF pt = GeneralSystem.Instance.Machines[moduleID].TransformPoints(item.Base_PasteXY[index], item.Result_Point, 0, baseInfo.Cam_Mark1Point[i], 0);
                                
                                LineItem curve = panel.AddCurve("", new double[] { pt.X }, new double[] { pt.Y }, Color.Black, SymbolType.Circle);
                                curve.Line.IsVisible = false;
                                curve.Symbol.Size = 15;
                                if (item.Base_PasteDone == null || item.Base_PasteDone[index] == 0)
                                {
                                    curve.Symbol.Fill = new Fill(Color.Yellow);
                                }
                                else
                                {
                                    curve.Symbol.Fill = new Fill(Color.Green);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cB_EN_CheckedChanged(object sender, EventArgs e)
        {
            if(cB_EN.Checked)
            {
                this.bClearOut.Enabled = true;
                this.bReset.Enabled = true;
            }
            else
            {
                this.bClearOut.Enabled = false;
                this.bReset.Enabled = false;
            }
        }

        private void bInput_Click(object sender, EventArgs e)
        {
            
        }

        private void bOutput_Click(object sender, EventArgs e)
        {
          
        }

        private void bClearStatic_Click(object sender, EventArgs e)
        {
            Variable.iTotalByPcs = 0;
            Variable.iTotalByPCB = 0;
        }

        private void cb_EnModule1_CheckedChanged(object sender, EventArgs e)
        {
            GeneralSystem.Instance.Machines[0].Config.EN_Runing = !this.cb_EnModule1.Checked;
            GeneralSystem.Instance.Save();
        }

        private void cb_EnModule2_CheckedChanged(object sender, EventArgs e)
        {
            GeneralSystem.Instance.Machines[1].Config.EN_Runing = !this.cb_EnModule2.Checked;
            GeneralSystem.Instance.Save();
        }
    }
}
