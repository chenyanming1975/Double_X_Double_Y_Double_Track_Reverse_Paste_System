using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Flow.Step;
using GeneralMachine.Flow.Nodes;
using System.Threading;
using System.Drawing;
using GeneralMachine.Flow.Tool;
using GeneralMachine.Motion;
using System.Windows.Forms;
using GeneralMachine.Track;
using System.Collections.Concurrent;
using System.Diagnostics;
using GeneralMachine.Report;

namespace GeneralMachine.Flow
{
    /// 状态机
    /// </summary>
    public class StateMachine
    {
        public StateMachine(Module module)
        {
            this.Module = module;
            this.RunData = new MachineRunDataHelper(module);

            if (module == Module.Front)
                VisionCalHelper.Module1Handler += VisionCalHelper_ModuleHandler;
            else
                VisionCalHelper.Module2Handler += VisionCalHelper_ModuleHandler;
        }

        #region 固定属性
        public Module Module
        {
            get;
            set;
        } = Module.Front;

        public MachineEntiy MachineEntiy
        {
            get
            {
                return SystemEntiy.Instance[this.Module];
            }
        }

        /// <summary>
        /// 当前流程步骤
        /// </summary>
        public FlowStep CurStep
        {
            get;
            set;
        } = null;

        /// <summary>
        /// 是否退出系统
        /// </summary>
        public bool bSystemExit
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool Pasued
        {
            get;
            set;
        } = true;
        #endregion

        #region 运行数据
        /// <summary>
        /// 运行时参数
        /// </summary>
        public MachineRunDataHelper RunData = null;

        /// <summary>
        /// 贴附拼版信息
        /// </summary>
        public MultiPasteInfo Program = null;
        #endregion

        #region 对外接口
        /// <summary>
        /// 安装流程
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        public string InstallProgram(ProgramFlow program)
        {
            this.Program = program.ConvertToInfo();

            this.RunData = new MachineRunDataHelper(this.Module);
            this.RunData.Restet(this.Program);
            return string.Empty;
        }

        /// <summary>
        /// 开始流程
        /// </summary>
        public void Start()
        {
         
            if (FeederDefine.Instance.InstallFeederName[this.Module][Feeder.Right] == string.Empty
             && FeederDefine.Instance.InstallFeederName[this.Module][Feeder.Left] == string.Empty)
            {
                MessageBox.Show($"没有安装Feeder信息 请给该模组安装Feeder信息!!!");
                return;
            }

            // 相机状态设置
            for(Camera cam = Camera.Top; cam <= Camera.Bottom2; ++cam)
            {
                CameraDefine.Instance.Camera[this.Module][cam].StopGrab();
                CameraDefine.Instance.Camera[this.Module][cam].StartGrab(); // 相机设置可采图
            }

            for (Feeder feeder = Feeder.Left; feeder <= Feeder.Right; ++feeder)
            {
                FeederConfig left = null;

                if (FeederDefine.Instance.InstallFeederName[this.Module][feeder] != string.Empty)
                {
                    string feederName = CommonHelper.GetEnumDescription(typeof(Feeder), feeder);
                    left = FeederDefine.Instance[this.Module, feeder];
                    if (left == null)
                    {
                        MessageBox.Show($"找不到可{feederName} [{FeederDefine.Instance.InstallFeederName[this.Module][feeder]}] 请检查程式!!!");
                        return;
                    }

                    string loadrtn = VisionCalHelper.Instance.InstallVision(left.LabelName);
                    if(!string.IsNullOrEmpty(loadrtn))
                    {
                        MessageBox.Show($"{feederName} 视觉方法导入失败 [{left.LabelName}] 请检查视觉库!!!");
                        return;
                    }
                }
            }

            // 安装视觉
            string rtn = VisionCalHelper.Instance.InstallVision(this.Program.VisionList);

            if (rtn != string.Empty)
            {
                MessageBox.Show(rtn);
                return;
            }

            TrackManager.Instance.TrackStart((Config.Track)this.Module);

            if (CurStep == null)
            {
                CurStep = new SuckLabelStep(this, this.MachineEntiy);
                CurStep.CurState = FlowStep.State.Enter;
                this.RunData.Restet(this.Program);
            }
            else
            {
                CurStep.ReStart();
            }

            if (FlowThd == null)
            {
                FlowThd = new Thread(Thread_Main);
                FlowThd.Start();
            }

            this.MachineEntiy.MachineIO.StopBtnLight.SetIO(false);
            this.MachineEntiy.MachineIO.StartBtnLight.SetIO(true);
            this.Pasued = false;
        }

        /// <summary>
        /// 流程暂停
        /// </summary>
        public void Pause()
        {
            this.MachineEntiy.MachineIO.StartBtnLight.SetIO(false);
            this.MachineEntiy.MachineIO.StopBtnLight.SetIO(true);
            this.Pasued = true;
            this.CurStep?.Pause();
        }
        #endregion

        #region 视觉计算方法
        /// <summary>
        /// 视觉计算分配处理
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="e">视觉参数</param>
        private void VisionCalHelper_ModuleHandler(object sender, ResultItem e)
        {
            switch(e.Key)
            {
                case ResultKey.Mark:
                    this.CalMark(e);
                    break;
                case ResultKey.DownVision:
                    this.CalDownMark(e);
                    break;
                case ResultKey.Badmark:
                    this.CalBadmark(e);
                    break;
                case ResultKey.PCSCode:
                    this.CalPCSCode(e);
                    break;
                case ResultKey.PanelCode:
                    this.CalPanelCode(e);
                    break;
            }
        }

        private void CalBadmark(ResultItem item)
        {

        }

        /// <summary>
        /// 上视觉处理
        /// </summary>
        /// <param name="item"></param>
        private void CalMark(ResultItem item)
        {
            if(item.Result.State == Vision.VisionResultState.NG)
            {
                this.RunData[item.PCBIndex].MarkData.MarkSuccess = false;
                for (int i = 0; i < this.RunData[item.PCBIndex].PcsData.Length; ++i)
                    this.RunData.SetPasteState(item.PCBIndex, i, 4);

                MsgHelper.Instance.AddMessage(MsgLevel.Error, "Mark点识别失败!!");
                return;
            }

            #region 空跑
            if (SystemConfig.Instance.General.RunMode == RunMode.TestRun)
            {
                if (this.Program.PasteInfos[item.PCBIndex].MarkPtList.Count == 1)
                {
                    this.RunData[item.PCBIndex].MarkData.Mark1IsCaled = true;
                    this.RunData[item.PCBIndex].MarkData.MarkSuccess = true;
                }
                else
                {
                    if (item.Mark == Mark.Mark点1)
                    {
                        this.RunData[item.PCBIndex].MarkData.Mark1IsCaled = true;
                    }
                    else
                    {
                        this.RunData[item.PCBIndex].MarkData.Mark2IsCaled = true;
                        this.RunData[item.PCBIndex].MarkData.MarkSuccess = true;
                    }
                }
                return;
            }
            #endregion

            PointF[] pasteList = new PointF[this.Program.PasteInfos[item.PCBIndex].PasteList.Count];
            for(int i = 0; i < pasteList.Length; ++i)
            {
                pasteList[i] = this.Program.PasteInfos[item.PCBIndex].PasteList[i].Pos;
            }

            if (this.Program.PasteInfos[item.PCBIndex].MarkPtList.Count == 1) // 1个mark点
            {
                #region 单mark计算
                PointF markPt = new PointF();
                PointF oldMark = this.Program.PasteInfos[item.PCBIndex].MarkPtList[0].Pos;
                this.MachineEntiy.WroldPt(item.Camera, item.CaptruePos, item.Result.Point, out markPt);

                this.RunData[item.PCBIndex].MarkData.Mark1 = markPt;
                this.RunData[item.PCBIndex].MarkData.Mark1IsCaled = true;
                pasteList = MathHelper.TransformPointsForm1Mark(pasteList, oldMark, markPt);

                this.RunData[item.PCBIndex].SetPos(pasteList);
                this.RunData[item.PCBIndex].MarkData.MarkSuccess = true;
                #endregion
            }
            else if (this.Program.PasteInfos[item.PCBIndex].MarkPtList.Count == 2) // 2个mark点
            {
                #region 双mark计算
                PointF markPt = new PointF();
                this.MachineEntiy.WroldPt(item.Camera, item.CaptruePos, item.Result.Point, out markPt);

                if (item.Mark == Mark.Mark点1)
                {
                    this.RunData[item.PCBIndex].MarkData.Mark1 = markPt;
                    this.RunData[item.PCBIndex].MarkData.Mark1IsCaled = true;
                }
                else
                {
                    this.RunData[item.PCBIndex].MarkData.Mark2 = markPt;
                    this.RunData[item.PCBIndex].MarkData.Mark2IsCaled = true;
                }

                if (this.RunData[item.PCBIndex].MarkData.Mark1IsCaled
                    && this.RunData[item.PCBIndex].MarkData.Mark2IsCaled)
                {
                    PointF oldMark1 = new PointF();
                    PointF oldMark2 = new PointF();
                    if (this.Program.PasteInfos[item.PCBIndex].MarkPtList[0].MarkID == Mark.Mark点1)
                        oldMark1 = this.Program.PasteInfos[item.PCBIndex].MarkPtList[0].Pos;
                    else
                        oldMark1 = this.Program.PasteInfos[item.PCBIndex].MarkPtList[0].Pos;

                    if (this.Program.PasteInfos[item.PCBIndex].MarkPtList[1].MarkID == Mark.Mark点2)
                        oldMark2 = this.Program.PasteInfos[item.PCBIndex].MarkPtList[1].Pos;
                    else
                        oldMark2 = this.Program.PasteInfos[item.PCBIndex].MarkPtList[1].Pos;

                    pasteList = MathHelper.TransformPointsForm2Mark(pasteList, oldMark1, oldMark2,
                        this.RunData[item.PCBIndex].MarkData.Mark1,
                        this.RunData[item.PCBIndex].MarkData.Mark2,
                        ref this.RunData[item.PCBIndex].MarkData.UpAngle);

                    this.RunData[item.PCBIndex].SetPos(pasteList);
                    this.RunData[item.PCBIndex].MarkData.MarkSuccess = true;
                }
                #endregion
            }
        }

        /// <summary>
        /// 下视觉视觉处理
        /// </summary>
        /// <param name="item"></param>
        private void CalDownMark(ResultItem item)
        {
            if (SystemConfig.Instance.General.RunMode == RunMode.TestRun)
            {
                item.Result.Point = this.MachineEntiy.MachineConfig[item.NZIndex].RotatePoint;
                item.Result.Angle = 0;
            }

            this.RunData.RUN_NzData[item.NZIndex].Item = item;
            if (this.RunData[item.PCBIndex].MarkData.MarkSuccess
                && item.Result.State == Vision.VisionResultState.OK)
            {
                this.CalPaste(item.NZIndex);
            }

            if (item.Result.State == Vision.VisionResultState.OK)
                this.RunData.RUN_NzData[item.NZIndex].State = NZ_State.DownSuccessed;
            else
                this.RunData.RUN_NzData[item.NZIndex].State = NZ_State.DownFailed;
        }

        private void CalPanelCode(ResultItem item)
        {
        }
        private void CalPCSCode(ResultItem item)
        {

        }

        /// <summary>
        /// 计算贴标位
        /// </summary>
        /// <param name="nz"></param>
        public void CalPaste(Nozzle nz)
        {
            var item = this.RunData.RUN_NzData[nz].Item;
            PointF camPt = this.RunData[item.PCBIndex]
                [item.PCSIndex].UpPastePt;

            this.RunData.RUN_NzData[nz].RealAngle =
               this.Program.PasteInfos[item.PCBIndex].PasteList[item.PCSIndex].PasteAngle
               + item.Result.Angle
               + this.RunData[item.PCBIndex].MarkData.UpAngle
               + this.Program.PasteInfos[item.PCBIndex].BaseAngle
               + this.Program.NzUOffset[nz];

            this.MachineEntiy.RGoAngle(this.RunData.RUN_NzData[nz].RealAngle,
                nz, this.MachineEntiy.MachineConfig.AutoSpeedMode);

            PointF result = this.MachineEntiy.RotatePtDown(item.NZIndex, item.Result.Point, -this.RunData.RUN_NzData[item.NZIndex].RealAngle);

            PointF offset = new PointF();
            offset.X = result.X - this.MachineEntiy.MachineConfig[item.NZIndex].DownMarkPt.X;
            offset.Y = result.Y - this.MachineEntiy.MachineConfig[item.NZIndex].DownMarkPt.Y;
            PointF realPt = new PointF();

            realPt.X = camPt.X + offset.X + this.MachineEntiy.MachineConfig[item.NZIndex].NzToCam.X
                + this.Program.NzOffset[item.NZIndex].X + (float)this.Program.PasteInfos[item.PCBIndex].PasteList[item.PCSIndex].OffsetX;
            realPt.Y = camPt.Y + offset.Y + this.MachineEntiy.MachineConfig[item.NZIndex].NzToCam.Y
                + this.Program.NzOffset[item.NZIndex].Y + (float)this.Program.PasteInfos[item.PCBIndex].PasteList[item.PCSIndex].OffsetY;

            realPt = this.MachineEntiy.GetPasteOffset(nz, this.Program.PasteInfos[item.PCBIndex].PasteList[item.PCSIndex].Pos, realPt);

            this.RunData.RUN_NzData[item.NZIndex].RealPt = realPt;
            this.RunData[item.PCBIndex][item.PCSIndex].RealPastePt = realPt;
            this.RunData[item.PCBIndex][item.PCSIndex].Angle = this.RunData.RUN_NzData[item.NZIndex].RealAngle;
            this.RunData[item.PCBIndex][item.PCSIndex].PasteNozzle = item.NZIndex;
            this.RunData[item.PCBIndex][item.PCSIndex].SuckFeeder = this.RunData.RUN_NzData[item.NZIndex].SuckFeeder;
            this.RunData[item.PCBIndex][item.PCSIndex].LabelName = item.funcName;
            this.RunData.RUN_NzData[item.NZIndex].IsCalPastePt = true;
        }
        #endregion

        #region 轨道对接
        /// <summary>
        /// 进板是否完成
        /// </summary>
        public bool PCBReach
        {
            get
            {
                return Track.TrackManager.Instance.TrackEntiy[(Config.Track)this.Module].ReachOK;
            }
        }

        /// <summary>
        /// 是否可以出板
        /// </summary>
        public bool CanOuput
        {
            set
            {
                Track.TrackManager.Instance.TrackEntiy[(Config.Track)this.Module].PastedOK = value;
            }

            get
            {
                return Track.TrackManager.Instance.TrackEntiy[(Config.Track)this.Module].PastedOK;
            }
        }

        /// <summary>
        /// 计算是否可以出板
        /// </summary>
        public void CalCanOuput()
        {
            for (int i = 0; i < this.RunData.BoardCount; ++i)
            {
                for (int j = 0; j < this.RunData[i].PCSCount; ++j)
                {
                    if (this.RunData[i][j].iPasteState <= 0)
                    {
                        this.CanOuput = false;
                        return;
                    }
                }
            }

            this.CT.Stop();
            ReportHelper.Instance[this.Module].PCBCount += 1;
            this.CanOuput = true;
        }
        #endregion

        /// <summary>
        /// 主线程
        /// </summary>
        private Thread FlowThd = null;

        /// <summary>
        /// 状态机主流程
        /// </summary>
        private void Thread_Main()
        {
            while (!this.bSystemExit)
            {
                Thread.Sleep(1);

                // 检查软件是否有报错
                if (this.Pasued)
                {
                    Thread.Sleep(100);
                    continue;
                }

                // 急停被拍
                if (MotionHelper.Instance.Emg)
                {
                    MsgHelper.Instance.AddMessage(MsgLevel.Fatal, "急停启用!!!", (int)this.Module);
                    Thread.Sleep(100);
                    continue;
                }
                
                // 伺服报警 Y轴伺服报警
                if(this.MachineEntiy.MachineAxis.Y.bAxisServoWarning)
                {
                    MsgHelper.Instance.AddMessage(MsgLevel.Fatal, "Y轴伺服报警!!!", (int)this.Module);
                    Thread.Sleep(100);
                    continue;
                }

                // 其他轴伺服报警
                if(this.MachineEntiy.MachineAxis.X.bAxisServoWarning)
                {
                    MsgHelper.Instance.AddMessage(MsgLevel.Error, "X轴伺服报警!!!", (int)this.Module);
                    Thread.Sleep(100);
                    continue;
                }

                for(Nozzle NZ = Nozzle.Nz1; NZ <= Nozzle.Nz4; ++NZ)
                {
                    if (this.MachineEntiy.MachineAxis.Z[(int)NZ].bAxisServoWarning)
                    {
                        MsgHelper.Instance.AddMessage(MsgLevel.Error, "Z轴伺服报警!!!", (int)this.Module);
                        Thread.Sleep(100);
                        break;
                    }

                    if(this.MachineEntiy.MachineAxis.R[(int)NZ].bAxisServoWarning)
                    {
                        MsgHelper.Instance.AddMessage(MsgLevel.Error, "U轴步进异常报警!!!", (int)this.Module);
                        Thread.Sleep(100);
                        break;
                    }
                }

                // 安全门报警
                if (!this.MachineEntiy.MachineIO.SafeDoor.GetIO())
                {
                    MsgHelper.Instance.AddMessage(MsgLevel.Error, "安全门打开!!!", (int)this.Module);
                    Thread.Sleep(100);
                    continue;
                }

                this.CurStep?.Handler();
            }
        }

        public Stopwatch CT = new Stopwatch();
    }
}
