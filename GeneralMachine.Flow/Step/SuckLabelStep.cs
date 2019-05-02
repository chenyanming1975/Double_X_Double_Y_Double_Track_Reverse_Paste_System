using GeneralMachine.Common;
using GeneralMachine.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Flow.Step
{
    public class SuckLabelStep : FlowStep
    {
        public SuckLabelStep(StateMachine machine,MachineEntiy entiy)
            :base(machine,entiy)
        {
            this.FlowName = "吸标";
        }

        #region 动作暂存变量
        /// <summary>
        /// 吸标Feeder
        /// </summary>
        public FeederConfig Feeder = null;

        /// <summary>
        /// 吸标队列
        /// </summary>
        public Queue<Nozzle> SuckQueue = new Queue<Nozzle>();

        /// <summary>
        /// 重吸次数
        /// </summary>
        public int ReSuckCount = 0;

        public Nozzle StartNz = Nozzle.Nz1;
        #endregion

        #region 需要预先到
        /// <summary>
        /// 分配吸嘴 --- 吸嘴吸那个位置贴那个位置
        /// </summary>
        /// <returns></returns>
        public void ConfigNz()
        {
            using (LogTraceLife log = new LogTraceLife("吸标", "分配吸嘴"))
            {
                #region 分配吸嘴
                List<Nozzle> queue = new List<Nozzle>();
                for (Nozzle nz = Nozzle.Nz1; nz <= Nozzle.Nz4; ++nz)
                {
                    if (this.machine.Program.NzUsed[nz]
                        && this.machine.RunData.RUN_NzData[nz].State == NZ_State.NoUsed)
                        queue.Add(nz);
                }

                if (queue.Count == 0)
                {
                    MsgHelper.Instance.AddMessage(MsgLevel.Error, "没有找到可用吸嘴", (int)this.entiy.Module);
                }

                SuckQueue.Clear();

                for (int pcbIndex = 0; pcbIndex < this.machine.RunData.BoardCount; ++pcbIndex)
                {
                    int pcsLen = this.machine.RunData[pcbIndex].PCSCount;
                    for (int pcsIndex = 0; pcsIndex < pcsLen; ++pcsIndex)
                    {
                        if (this.machine.RunData[pcbIndex][pcsIndex].iPasteState > 0)
                            continue;

                        for (int i = 0; i < queue.Count; ++i)
                        {
                            Nozzle nz = queue[i];
                            var pastePt = this.machine.Program.PasteInfos[pcbIndex].PasteList[pcsIndex].Pos;
                            var nzPt = this.entiy.NozzleToCam(nz, pastePt);
                            if (this.entiy.MachineConfig.XWorkRange.InSafe(nzPt.X))
                            {
                                this.machine.RunData.RUN_NzData[nz].PCBIndex = pcbIndex;
                                this.machine.RunData.RUN_NzData[nz].PCSIndex = pcsIndex;
                                Feeder feeder = this.machine.Program.PasteInfos[pcbIndex].PasteList[pcsIndex].Feeder;
                                this.machine.RunData.RUN_NzData[nz].SuckFeeder = feeder;
                                this.machine.RunData.RUN_NzData[nz].SuckLabel = FeederDefine.Instance[this.machine.Module, feeder].LabelName;
                                SuckQueue.Enqueue(nz);
                                queue.Remove(nz);
                                if (queue.Count == 0)
                                    goto OUTFIND;
                                else
                                    break;
                            }
                        }
                    }
                }

                OUTFIND: // 寻找完成
                {
                    var list = SuckQueue.ToList();
                    list.Sort((a, b) => {
                        if (a >= b)
                            return 1;
                        else
                            return -1;
                    });
                    SuckQueue.Clear();
                    for(int i =0; i< list.Count;++i)
                    {
                        SuckQueue.Enqueue(list[i]);
                    }
                    if (SuckQueue.Count == 0)
                    {
                        MsgHelper.Instance.AddMessage(MsgLevel.Error, "吸嘴分配失败", (int)this.entiy.Module);
                    }
                    else
                    {
                        StartNz = SuckQueue.First();
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 获得下一个吸嘴吸标参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool GetNextSuck(ref MoveParam param)
        {
            if (SuckQueue.Count == 0)
                return false;

            Nozzle nz = SuckQueue.Dequeue();
            param = new MoveParam();
            this.MoveParam.TrunAngle = machine.MachineEntiy.MachineConfig.TrunSuckAngle;
            param.Feeder = this.machine.RunData.RUN_NzData[nz].SuckFeeder;
            param.Nozzle = nz;
            param.MoveZ = true;
            param.MoveR = true;
            param.NzUsed[(int)nz] = true;
            param.ZPos[(int)nz] = this.entiy.MachineConfig[nz].XIHeight;
            return true;
        }
        #endregion

        #region 流程终止条件 参数准备
        /// <summary>
        /// 进入吸标步骤
        /// </summary>
        public override void OnEnter()
        {
            this.ConfigNz();
            base.OnEnter();
        }

        /// <summary>
        /// 离开吸标步骤开始下视觉步骤
        /// </summary>
        public override void OnExit()
        {
            base.OnExit();
            this.machine.CurStep = new DownVisionStep(this.machine, this.entiy);
            this.machine.CurStep.CurState = State.Enter;
        }

        /// <summary>
        /// 获得下一个吸标位置
        /// </summary>
        public override void CheckContinue()
        {
            if (this.GetNextSuck(ref this.MoveParam))
            {
                this.Feeder = FeederDefine.Instance[this.machine.Module,this.MoveParam.Feeder];
                base.CheckContinue();
            }
            else
                this.OnExit(); // 获取不到进行一个步骤
        }
        #endregion

        #region 流程动作
        public override void StartMoveXYR()
        {
            if (this.Feeder.GetSuckPos(ref this.MoveParam.XYPos, SystemConfig.Instance.General.RunMode == RunMode.TestRun))
            {
                this.MoveParam.XYPos = this.machine.MachineEntiy.LabelToNz(this.MoveParam.Nozzle, this.MoveParam.XYPos);
                base.StartMoveXYR();
            }
        }

        public override void MoveXYRFinished(MoveParam param)
        {
            this.entiy.MachineIO.VaccumPO[(int)param.Nozzle].SetIO(false);

            // 提前开真空
            if (SystemConfig.Instance.General.RunMode == RunMode.Normal)
            {
                this.entiy.MachineIO.VaccumSuck[(int)param.Nozzle].SetIO(true);
            }

            if (Feeder.NewOut && StartNz != param.Nozzle)
            {
                Thread.Sleep(Feeder.NewFeederDelay);
                Feeder.NewOut = false;
            }
            else
            {
                Feeder.NewOut = false;
            }

            base.MoveXYRFinished(param);
        }

        public override void MoveZFinished(MoveParam param)
        {
            if (SystemConfig.Instance.General.RunMode == RunMode.TestRun)
            {
                this.entiy.MachineIO.VaccumSuck[(int)param.Nozzle].SetIO(true);
            }
            else
            {
                // 提前开真空
                //if (!LabelDefine.Instance[this.Feeder.LabelName].PreSuck)
                {
                    this.entiy.MachineIO.VaccumSuck[(int)param.Nozzle].SetIO(true);
                }
            }

            Thread.Sleep(this.Feeder.SuckDelay);
            base.MoveZFinished(param);
        }

        /// <summary>
        /// 回到安全高度开始检测
        /// </summary>
        public override void ZGoSafeFinished()
        {
            if (SystemConfig.Instance.General.RunMode == RunMode.TestRun)
            {
                this.machine.RunData.RUN_NzData[this.MoveParam.Nozzle].State = NZ_State.Sucked;
                this.machine.RunData.RUN_NzData[this.MoveParam.Nozzle].SuckFeeder = this.Feeder.Feeder;
            }
            else
            {
                if (true/*this.entiy.MachineIO.VaccumCheck[(int)this.MoveParam.Nozzle].GetIO()*/)
                {
                    this.machine.RunData.RUN_NzData[this.MoveParam.Nozzle].State = NZ_State.Sucked;
                    this.machine.RunData.RUN_NzData[this.MoveParam.Nozzle].SuckFeeder = this.Feeder.Feeder;
                }
                else
                {
                    ReSuckCount++;
                    if (ReSuckCount < SystemConfig.Instance.General.ContinuousSuckAlarm)
                    {
                        ReSuckCount = 0;
                        bool rtn = false; // ture：重吸 false：跳过
                        // 报警跳过
                        if (rtn)
                            this.machine.RunData.RUN_NzData[this.MoveParam.Nozzle].State = NZ_State.Sucked;
                        else
                            this.machine.RunData.RUN_NzData[this.MoveParam.Nozzle].State = NZ_State.Sucked;
                    }
                }
            }

            base.ZGoSafeFinished();
        }
        #endregion
    }
}
