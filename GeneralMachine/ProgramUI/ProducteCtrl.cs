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
using System.Diagnostics;
using System.Threading;
using GeneralMachine.Common;
using NationalInstruments.Vision;
using NationalInstruments.Vision.Analysis;
using GeneralMachine.Flow.Tool;
using GeneralMachine.Flow.Editer;
using GeneralMachine.Report;

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
      
        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProducteCtrl_Load(object sender, EventArgs e)
        {
            this.selectModule.ModuleChange += SelectModuleChange;
            this.selectModule.Module = Module.Front;
            VisionCalHelper.Instance.VisionCaledEvent += Instance_VisionCaledEvent;
            VisionCalHelper.Instance.ImageShowEvent += Instance_ImageShowEvent;
            MsgHelper.Instance.InfoRaised += Instance_InfoRaised;
            MsgHelper.Instance.ErrorRaised += Instance_ErrorRaised;
            MachineRunDataHelper.ChangePasteRegion += StateMachine_ProgramInstall;
            MachineRunDataHelper.UpdateChart += MachineRunDataHelper_UpdateChart;

            this.imageControl1.imageSet.Image.ReadFile(PathDefine.sPathConfigure + "Image//1.bmp");
            this.imageControl2.imageSet.Image.ReadFile(PathDefine.sPathConfigure + "Image//2.bmp");
            this.imageControl3.imageSet.Image.ReadFile(PathDefine.sPathConfigure + "Image//2.bmp");
            this.imageControl1.imageSet.ZoomToFit = true;
            this.imageControl2.imageSet.ZoomToFit = true;
            this.imageControl3.imageSet.ZoomToFit = true;

            MachineEntiy.ModuleStart += (module) => {
                this.bStartRun_Click(this, new EventArgs());
            };

            MachineEntiy.ModuleStop += (module) => {
                this.bPause_Click(this, new EventArgs());
            };

            MachineEntiy.ModuleReset += (module) => {
                // 清除错误报警
            };

            Report.ReportHelper.Instance[Module.Front].StartTiming(Report.StatisticType.WaitProducte, "机器复位完成",true);
            Report.ReportHelper.Instance[Module.After].StartTiming(Report.StatisticType.WaitProducte, "机器复位完成",true);
        }

        #region 界面操作
        private void bStartRun_Click(object sender, EventArgs e)
        {
            if (SystemEntiy.Instance.FlowMachine[this.Module].Program == null)
            {
                MessageBox.Show($"{Common.CommonHelper.GetEnumDescription(typeof(Module), this.Module)}没有导入程式请重新导入再运行!!!");
                return;
            }

            if (this.cbAll.Checked)
            {
                SystemEntiy.Instance[Module.Front].ZGoSafeTillStop();
                SystemEntiy.Instance.FlowMachine[Module.Front].Start();
                Report.ReportHelper.Instance[Module.Front].StartTiming(Report.StatisticType.Producte, "开始生产");
                SystemEntiy.Instance[Module.After].ZGoSafeTillStop();
                SystemEntiy.Instance.FlowMachine[Module.After].Start();
                Report.ReportHelper.Instance[Module.After].StartTiming(Report.StatisticType.Producte, "开始生产");
            }
            else
            {
                SystemEntiy.Instance[this.Module].ZGoSafeTillStop();
                SystemEntiy.Instance.FlowMachine[this.Module].Start();
                Report.ReportHelper.Instance[this.Module].StartTiming(Report.StatisticType.Producte, "开始生产");
            }

            this.EnableUI(1);
        }

        private void bPause_Click(object sender, EventArgs e)
        {
            if (cbAll.Checked)
            {
                SystemEntiy.Instance.FlowMachine[Module.Front].Pause();
                SystemEntiy.Instance.FlowMachine[Module.After].Pause();
                // 相机状态设置
                for (Camera cam = Camera.Top; cam <= Camera.Bottom2; ++cam)
                {
                    CameraDefine.Instance.Camera[Module.Front][cam].StopGrab();
                    CameraDefine.Instance.Camera[Module.After][cam].StopGrab();
                }
                Report.ReportHelper.Instance[Module.Front].StartTiming(Report.StatisticType.WaitProducte, "机器暂停");
                Report.ReportHelper.Instance[Module.After].StartTiming(Report.StatisticType.WaitProducte, "机器暂停");
            }
            else
            {
                // 相机状态设置
                for (Camera cam = Camera.Top; cam <= Camera.Bottom2; ++cam)
                {
                    CameraDefine.Instance.Camera[this.Module][cam].StopGrab();
                }
                SystemEntiy.Instance.FlowMachine[this.Module].Pause();
                Report.ReportHelper.Instance[this.Module].StartTiming(Report.StatisticType.WaitProducte, "机器暂停");
            }

            this.EnableUI(0);
        }

        private void selectModule_ModuleChange(object sender, Module e)
        {
            this.Module = this.selectModule.Module;
            int rtn = 0;
            //MK:0426初始化屏蔽
            if (false && SystemEntiy.Instance.FlowMachine[this.Module].Program == null)
                rtn = 0;
            //MK:0426初始化屏蔽
            if (false && SystemEntiy.Instance.FlowMachine[this.Module].Pasued)
                rtn = 0;
            else
                rtn = 1;
            this.EnableUI(rtn);
        }

        private void bByPass_Click(object sender, EventArgs e)
        {
            this.EnableUI(2);
            Report.ReportHelper.Instance[this.Module].StartTiming(Report.StatisticType.Producte, "机器ByPass");
        }

        /// <summary>
        /// 0：pause 1:run 2:bypass 3:start 清料
        /// </summary>
        /// <param name="isStart"></param>
        private void EnableUI(int isStart)
        {
            MachineStateChange?.Invoke(isStart);
            switch (isStart)
            {
                case 0:
                    this.bStartRun.Enabled = true;
                    this.bStartRun.BackColor = Color.White;
                    this.bByPass.BackColor = Color.White;
                    this.bPause.BackColor = Color.LightGray;
                    this.bByPass.Enabled = true;
                    this.bSrvoOn.Enabled = true;
                    this.gOper.Enabled = true;
                    break;
                case 1:
                    this.bStartRun.Enabled = false;
                    this.bStartRun.BackColor = Color.LightGray;
                    this.bPause.BackColor = Color.White;
                    this.bByPass.Enabled = false;
                    this.bSrvoOn.Enabled = false;
                    this.groupClear.Enabled = false;
                    this.gOper.Enabled = false;
                    break;
                case 2:
                    this.bStartRun.Enabled = false;
                    this.bStartRun.BackColor = Color.White;
                    this.bPause.BackColor = Color.LightGray;
                    this.bByPass.Enabled = false;
                    this.bSrvoOn.Enabled = false;
                    this.groupClear.Enabled = false;
                    this.gOper.Enabled = false;
                    break;
                case 3:
                    this.bStartRun.Enabled = false;
                    this.bByPass.Enabled = false;
                    this.bSrvoOn.Enabled = false;
                    this.groupClear.Enabled = false;
                    this.gOper.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void bSrvoOn_CheckedChanged(object sender, EventArgs e)
        {
            this.groupClear.Enabled = this.bSrvoOn.Checked;
        }

        private void bClearOut_Click(object sender, EventArgs e)
        {
            if(cbAll.Checked)
            {
                if(SystemEntiy.Instance.FlowMachine[Module.Front].Pasued)
                {
                    this.ClearOutAsync(Module.Front);
                }
                else
                {
                    MessageBox.Show("请暂停前模组!!!");
                }

                if (SystemEntiy.Instance.FlowMachine[Module.After].Pasued)
                {
                    this.ClearOutAsync(Module.After);
                }
                else
                {
                    MessageBox.Show("请暂停后模组!!!");
                }
            }
            else
            {
                if(SystemEntiy.Instance.FlowMachine[this.Module].Pasued)
                {
                    this.ClearOutAsync(this.Module);
                }
                else
                {
                    MessageBox.Show("请暂停当前模组!!!");
                }
            }
        }

        private void ClearOut(Module module)
        {
            #region 抛料
            Stopwatch sw = new Stopwatch();
            sw.Start();
            const int timeout = 10000;

            // 如果头部不准
            while (!SystemEntiy.Instance[module].SafeCheck() && sw.ElapsedMilliseconds < timeout)
            {
                CommonHelper.DoEvent(50);
            }

            if (sw.ElapsedMilliseconds > timeout)
            {
                MessageBox.Show("抛料超时!!!");
                this.EnableUI(0);
                return;
            }

            #region 到吸标角度
            if (SystemEntiy.Instance[module].TurnReachPaste)
            {
                sw.Restart();
                SystemEntiy.Instance[module].XYGoSafePt();
                while (!SystemEntiy.Instance[module].XYReach(SystemEntiy.Instance[module].MachineConfig.ReadyPoint)
                    && sw.ElapsedMilliseconds < timeout)
                {
                    CommonHelper.DoEvent(50);
                }

                if (sw.ElapsedMilliseconds > timeout)
                {
                    MessageBox.Show("抛料超时!!!");
                    this.EnableUI(0);
                    return;
                }

                sw.Restart();
                SystemEntiy.Instance[module].TurnGoSuck();
                while (!SystemEntiy.Instance[module].TurnReachSuck
                  && sw.ElapsedMilliseconds < timeout)
                {
                    CommonHelper.DoEvent(50);
                }

                if (sw.ElapsedMilliseconds > timeout)
                {
                    MessageBox.Show("抛料超时!!!");
                    return;
                }
            }
            #endregion

            sw.Restart(); // 到抛料位
            SystemEntiy.Instance[module].XYGoPos(SystemEntiy.Instance[module].MachineConfig.DropPoint);
            while (!SystemEntiy.Instance[module].XYReach(SystemEntiy.Instance[module].MachineConfig.DropPoint)
                 && sw.ElapsedMilliseconds < timeout)
            {
                CommonHelper.DoEvent(50);
            }

            if (sw.ElapsedMilliseconds > timeout)
            {
                MessageBox.Show("抛料超时!!!");
                return;
            }


            for (Nozzle nz = Nozzle.Nz1; nz <= Nozzle.Nz4; ++nz)
            {
                int index = (int)nz;
                Task.Factory.StartNew(() => {
                    SystemEntiy.Instance[module].MachineIO.VaccumSuck[index].SetIO(false);
                    Thread.Sleep(100);
                    SystemEntiy.Instance[module].MachineIO.VaccumPO[index].SetIO(true);
                    Thread.Sleep(500);
                    SystemEntiy.Instance[module].MachineIO.VaccumPO[index].SetIO(false);
                });
            }

            SystemEntiy.Instance.FlowMachine[module].CurStep = null;
            TrackManager.Instance.TrackReset((Config.Track)module);
            IODefine.Instance.TrackIO[(Config.Track)module].IO_Stop.SetIO(false);
            IODefine.Instance.TrackIO[(Config.Track)module].IO_Carry.SetIO(false);
            #endregion
        }

        public async void ClearOutAsync(Module module)
        {
            this.EnableUI(3);
            await Task.Factory.StartNew(() => {
                this.ClearOut(module);
            });
            this.EnableUI(0);
        }

        private void SelectModuleChange(object sender, Module module)
        {
            this.Module = module;
        }
        #endregion

        #region 界面显示刷新
        /// <summary>
        /// 显示视觉图像
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        private void Instance_ImageShowEvent(Module arg1, Camera arg2, VisionImage arg3)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (arg1 == this.Module)
                {
                    if (arg2 == Camera.Top)
                        CommonAlgorithms.Copy(arg3, imageControl1.imageSet.Image);
                    else if (arg2 == Camera.Bottom1)
                        CommonAlgorithms.Copy(arg3, imageControl2.imageSet.Image);
                    else if (arg2 == Camera.Bottom2)
                        CommonAlgorithms.Copy(arg3, imageControl3.imageSet.Image);
                }

                arg3?.Dispose();
            }));
        }
        /// <summary>
        /// 显示视觉计算结果
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        private void Instance_VisionCaledEvent(Module arg1, ResultItem arg2, long arg3)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (dGV_Vision.Rows.Count >= 500)
                    this.dGV_Vision.Rows.Clear();

                object[] param = new object[6];
                param[0] = CommonHelper.GetEnumDescription(arg1.GetType(), arg1);
                param[1] = DateTime.Now.ToString("HH:mm:ss");
                param[2] = CommonHelper.GetEnumDescription(arg2.Key.GetType(), arg2.Key);

                if (arg2.Key == ResultKey.DownVision)
                    param[3] = CommonHelper.GetEnumDescription(arg2.NZIndex.GetType(), arg2.NZIndex);
                else if (arg2.Key == ResultKey.Mark || arg2.Key == ResultKey.PanelCode)
                    param[3] = $"Panel-{arg2.PCBIndex + 1}";
                else if (arg2.Key == ResultKey.PCSCode || arg2.Key == ResultKey.Badmark)
                    param[3] = $"{arg2.PCBIndex}-{arg2.PCSIndex + 1}";

                param[4] = arg2.Result.State.ToString();
                param[5] = arg3;

                this.dGV_Vision.Rows.Insert(0, param);
                this.dGV_Vision.Rows[0].Selected = true;
            }));
        }
        /// <summary>
        /// 显示错误报警
        /// </summary>
        /// <param name="obj"></param>
        private void Instance_ErrorRaised(HostarLogBean obj)
        {
            var reseult = this.BeginInvoke(new Action(() =>
            {
                if (dGV_Error.Rows.Count >= 500)
                    this.dGV_Error.Rows.Clear();

                object[] param = new object[3];
                if (obj.Module == -1)
                    param[0] = "全局";
                else if (obj.Module == 0)
                    param[0] = "前模组";
                else if (obj.Module == 1)
                    param[0] = "后模组";

                param[1] = obj.Time.ToString("HH:mm:ss");
                param[2] = obj.Message;
                this.dGV_Error.Rows.Insert(0, param);

                lock (MsgHelper.Instance.ErrLock[obj.Module+1])
                {
                    frm_MessageBox frm = null;
                    if (obj.LogLevel <= MsgLevel.Warn) // 提示不停机 报警灯蜂鸣器开启
                    {
                        frm = new frm_MessageBox(obj.Message, false, false);
                        frm.Show();
                    }
                    else if (obj.LogLevel == MsgLevel.Error)
                    {
                        SystemEntiy.Instance.FlowMachine[(Module)obj.Module].Pause();
                        SystemEntiy.Instance[(Module)obj.Module].StopAllAxis(false);

                        ReportHelper.Instance[(Module)obj.Module].StartTiming(StatisticType.DT, obj.Message, true);
                        ReportHelper.Instance[(Module)obj.Module].AlarmCount += 1;

                        frm = new frm_MessageBox(obj.Message, true, true);
                        if (frm.ShowDialog() == DialogResult.Yes) // 是否继续
                        {
                            SystemEntiy.Instance.FlowMachine[(Module)obj.Module].CurStep.ReStart();
                            SystemEntiy.Instance.FlowMachine[(Module)obj.Module].Pasued = false;
                        }
                        else
                        {
                            if (this.Module == (Module)obj.Module)
                            {
                                this.EnableUI(0);
                            }
                        }
                    }
                    else if (obj.LogLevel == MsgLevel.Fatal)
                    {
                        ReportHelper.Instance[Module.Front].StartTiming(StatisticType.DT, obj.Message, true);
                        ReportHelper.Instance[Module.After].StartTiming(StatisticType.DT, obj.Message, true);
                        ReportHelper.Instance[Module.Front].AlarmCount += 1;
                        ReportHelper.Instance[Module.After].AlarmCount += 1;

                        SystemEntiy.Instance.FlowMachine[Module.Front].Pause();
                        SystemEntiy.Instance.FlowMachine[Module.After].Pause();
                        SystemEntiy.Instance[Module.Front].StopAllAxis();
                        SystemEntiy.Instance[Module.After].StopAllAxis();
                        frm = new frm_MessageBox(obj.Message, false, false);
                        frm.ShowDialog();
                        this.EnableUI(0);
                    }
                }
            }));
        }
        /// <summary>
        /// 显示机器Log
        /// </summary>
        /// <param name="obj"></param>
        private void Instance_InfoRaised(HostarLogBean obj)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (dGV_Log.Rows.Count >= 500)
                    this.dGV_Log.Rows.Clear();

                object[] param = new object[3];
                if (obj.Module == -1)
                    param[0] = "全局";
                else if (obj.Module == 0)
                    param[0] = "前模组";
                else if (obj.Module == 1)
                    param[0] = "后模组";

                param[1] = obj.Time.ToString("HH:mm:ss");
                param[2] = obj.Message;
                this.dGV_Log.Rows.Insert(0, param);
            }));
        }
        /// <summary>
        /// 机器状态加载
        /// </summary>
        /// <param name="module"></param>
        private void StateMachine_ProgramInstall(Module module)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => { this.StateMachine_ProgramInstall(module); }));
            }
            else
            {
                if (module == Module.Front)
                {
                    this.gFront.Text = $"前模组-生产程式:{SystemEntiy.Instance.FlowMachine[module].Program.PasteName}";
                    this.pasteInfoFront.ChangedXYRegion();
                }
                else
                {
                    this.gAfter.Text = $"后模组-生产程式:{SystemEntiy.Instance.FlowMachine[module].Program.PasteName}";
                    this.pasteInfoAfter.ChangedXYRegion();
                }
            }
        }
        /// <summary>
        /// 机器状态刷新
        /// </summary>
        /// <param name="obj"></param>
        private void MachineRunDataHelper_UpdateChart(Module obj)
        {
            this.BeginInvoke(new Action(() => {
                if (Module == Module.Front)
                    this.pasteInfoFront.Refresh();
                else
                    this.pasteInfoAfter.Refresh();
            }));
        }
        /// <summary>
        /// 机器状态变更
        /// </summary>
        public event Action<int> MachineStateChange;

        #endregion

        private void bReCheck_CheckedChanged(object sender, EventArgs e)
        {
            frmPasteReCheck frm = new frmPasteReCheck(this.Module);
            if(frm.CanShow)
            {
                frm.ShowDialog();
            }
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            if (cbAll.Checked)
            {
                if (SystemEntiy.Instance.FlowMachine[Module.Front].Pasued)
                {
                    SystemEntiy.Instance.SystemHomeAsync(Module.Front);
                }
                else
                {
                    MessageBox.Show("请暂停前模组!!!");
                }

                if (SystemEntiy.Instance.FlowMachine[Module.After].Pasued)
                {
                    SystemEntiy.Instance.SystemHomeAsync(Module.After);
                }
                else
                {
                    MessageBox.Show("请暂停后模组!!!");
                }
            }
            else
            {
                if (SystemEntiy.Instance.FlowMachine[this.Module].Pasued)
                {
                    SystemEntiy.Instance.SystemHomeAsync(this.Module);
                }
                else
                {
                    MessageBox.Show("请暂停当前模组!!!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i= 0; i < SystemEntiy.Instance.FlowMachine.Count; ++i)
            {
                for(int j = 0; j < FeederDefine.Instance.InstallFeeder.Count;++j)
                {
                    FeederDefine.Instance[(Module)i, (Feeder)j].SuckIndex = 0;
                }
            }
        }

        private void bGoFeederPos_Click(object sender, EventArgs e)
        {
            if(SystemEntiy.Instance[this.Module].TurnReachSafe)
            {
                SystemEntiy.Instance[this.Module].XYGoPosTillStop(SystemEntiy.Instance[this.Module].MachineConfig.DropPoint);
            }
        }

        private void bGoTrunPos_Click(object sender, EventArgs e)
        {
            if (SystemEntiy.Instance[this.Module].TurnReachSafe)
            {
                SystemEntiy.Instance[this.Module].XYGoPosTillStop(SystemEntiy.Instance[this.Module].MachineConfig.ReadyPoint);
            }
        }

        private void tXIIndex_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
