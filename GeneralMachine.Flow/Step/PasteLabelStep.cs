using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Report;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Flow.Step
{
    public class PasteLabelStep : FlowStep
    {
        public PasteLabelStep(StateMachine machine, MachineEntiy entiy) : base(machine, entiy)
        {
            this.FlowName = "贴附";
            this.MoveParam.TrunAngle = machine.MachineEntiy.MachineConfig.TrunPasteAngle;
        }

        #region 固有参数
        public Nozzle PasteNozzle = Nozzle.Nz1;
        public int PCBIndex = 0;
        public int PCSIndex = 0;
        public List<Nozzle> PasteNz = new List<Nozzle>();
        public Stopwatch sw = new Stopwatch();
        #endregion

        #region 贴附逻辑
        /// <summary>
        /// 重新分配吸嘴 --- 吸嘴吸那个位置贴那个位置
        /// </summary>
        /// <returns></returns>
        public bool SetNZ(Nozzle NZ)
        {
            for (int pcbIndex = 0; pcbIndex < this.machine.RunData.BoardCount; ++pcbIndex)
            {
                for (int pcsIndex = 0; pcsIndex < this.machine.RunData[pcbIndex].PCSCount; ++pcsIndex)
                {
                    if (this.machine.RunData[pcbIndex][pcsIndex].iPasteState <= 0)
                    {
                        var pastePt = this.machine.Program.PasteInfos[pcbIndex].PasteList[pcsIndex].Pos;
                        var nzPt = this.entiy.CamToNozzle(NZ, pastePt);
                        if (this.entiy.MachineConfig.XLimit.InSafe(nzPt.X))
                        {
                            this.machine.RunData.RUN_NzData[NZ].PCBIndex = pcbIndex;
                            this.machine.RunData.RUN_NzData[NZ].PCSIndex = pcsIndex;
                            var feeder = this.machine.Program.PasteInfos[pcbIndex].PasteList[pcsIndex].Feeder;
                            this.machine.RunData.RUN_NzData[NZ].SuckFeeder = feeder;
                            this.machine.RunData.RUN_NzData[NZ].SuckLabel = FeederDefine.Instance[this.machine.Module, feeder].LabelName;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 获得下一个步贴附信息 0:FIND OK -1:FIND NG -2:WAIT VISION
        /// </summary>
        /// <param name="NZ">吸嘴</param>
        /// <returns>0:FIND OK -1:FIND NG -2:WAIT VISION</returns>
        public int GetNextPaste(ref Nozzle NZ)
        {
            #region 获取一个可贴附状态的吸嘴
            int rtn = -1;
            for (int i = 0; i < this.PasteNz.Count;)
            {
                // 计算失败的设置抛料不贴附
                if (this.machine.RunData.RUN_NzData[this.PasteNz[i]].State == NZ_State.DownFailed)
                {
                    this.PasteNz.RemoveAt(i);
                    i = 0;
                    continue;
                }
                // 该吸嘴还在进行下视觉计算跳过
                if(this.machine.RunData.RUN_NzData[this.PasteNz[i]].State == NZ_State.Sucked)
                {
                    i++;
                    rtn = -2;
                    continue;
                }
                else if(this.machine.RunData.RUN_NzData[this.PasteNz[i]].State == NZ_State.DownSuccessed)
                {
                    // 受 badmark 和 mark点 影响 贴附位置是否发生变更
                    var pcbIndex = this.machine.RunData.RUN_NzData[this.PasteNz[i]].PCBIndex;
                    var pcsIndex = this.machine.RunData.RUN_NzData[this.PasteNz[i]].PCSIndex;
                    if(this.machine.RunData[pcbIndex][pcsIndex].iPasteState > 0) // mark点失败 或者 被标记为 badmark
                    {
                        if(this.SetNZ(this.PasteNz[i])) // 重新寻抓可贴附点
                            this.machine.RunData.RUN_NzData[this.PasteNz[i]].IsCalPastePt = false; // 重新计算
                        else
                        {
                            this.PasteNz.RemoveAt(i); // 需要抛掉 从贴附吸嘴中移除
                            i = 0;
                            this.machine.RunData.RUN_NzData[this.PasteNz[i]].State = NZ_State.DownFailed;
                            continue;
                        }
                    }

                    if (!this.machine.RunData.RUN_NzData[this.PasteNz[i]].IsCalPastePt) // 是否已经计算过贴附位置
                    {
                        this.machine.CalPaste(this.PasteNz[i]);
                    }

                    NZ = this.PasteNz[i];
                    this.PasteNz.Remove(NZ);
                    rtn = 0;
                    break;
                }

                i++;
            }
            #endregion

            return rtn; // 没有找到
        }

        /// <summary>
        /// 获取需要贴附可贴附吸嘴列表
        /// </summary>
        public void GetPasteNz()
        {
            for(Nozzle nz = Nozzle.Nz1; nz <= Nozzle.Nz4;++nz)
            {
                if(this.machine.RunData.RUN_NzData[nz].State == NZ_State.Sucked
                    || this.machine.RunData.RUN_NzData[nz].State == NZ_State.DownSuccessed)
                PasteNz.Add(nz);
            }
        }

        /// <summary>
        /// 获得贴附运动参数
        /// </summary>
        /// <returns></returns>
        public bool GetMoveParam()
        {
            this.PCBIndex = this.machine.RunData.RUN_NzData[this.PasteNozzle].PCBIndex;
            this.PCSIndex = this.machine.RunData.RUN_NzData[this.PasteNozzle].PCSIndex;

            this.MoveParam.XYPos = this.machine.RunData.RUN_NzData[this.PasteNozzle].RealPt;
            this.MoveParam.ZPos[(int)this.PasteNozzle] = this.entiy.MachineConfig[this.PasteNozzle].PasteHeight;
            this.MoveParam.RPos[(int)this.PasteNozzle] = -this.machine.RunData.RUN_NzData[this.PasteNozzle].RealAngle;

            this.MoveParam.NzUsed = new bool[this.MoveParam.ZPos.Length];
            this.MoveParam.NzUsed[(int)this.PasteNozzle] = true;
            this.MoveParam.MoveZ = true;
            this.MoveParam.MoveR = true;
            return false;
        }

        /// <summary>
        /// 可继续贴附条件检查
        /// </summary>
        public override void CheckContinue()
        {
            if (!sw.IsRunning)
                sw.Restart();

            int rtn = this.GetNextPaste(ref this.PasteNozzle);

            if (rtn == 0)
            {
                sw.Stop();
                MsgHelper.Instance.AddMessage(MsgLevel.Debug, $"寻找贴附点耗时:{sw.ElapsedMilliseconds} ms", (int)this.entiy.Module);
                this.GetMoveParam();
                base.CheckContinue();
            }
            else if (rtn == -1)
            {
                this.machine.CalCanOuput();
                this.OnExit();
            }
            else if (rtn == -2) // 等待
            {
                Thread.Sleep(5);
            }
        }
        #endregion

        #region 流程动作
        public override void OnEnter()
        {
            this.GetPasteNz();
            base.OnEnter();
        }

        public override void OnExit()
        {
            this.machine.CurStep = new ReadPcsCodeStep(this.machine, this.entiy);
            this.machine.CurStep.CurState = State.Enter;
            base.OnExit();
        }

        public override void MoveXYRFinished(MoveParam param)
        {
            base.MoveXYRFinished(param);
        }

        public override void MoveZFinished(MoveParam param)
        {
            this.entiy.MachineIO.VaccumSuck[(int)this.PasteNozzle].SetIO(false);
            this.entiy.MachineIO.VaccumPO[(int)this.PasteNozzle].SetIO(true);
            Thread.Sleep(FeederDefine.Instance[this.machine.Module,this.machine.RunData.RUN_NzData[this.PasteNozzle].SuckFeeder].PutDelay);
            ReportHelper.Instance[this.machine.Module].PCSCount += 1;
            base.MoveZFinished(param);
        }

        public override void ZGoSafeFinished()
        {
            this.entiy.MachineIO.VaccumPO[(int)this.PasteNozzle].SetIO(false);
            this.machine.RunData.SetPasteState(this.PCBIndex,this.PCSIndex,1); // 标志已贴附
            this.machine.RunData.RUN_NzData[this.PasteNozzle].State = NZ_State.Pasted; 
            this.machine.RunData.RUN_NzData[this.PasteNozzle].IsCalPastePt = false; // 吸嘴需要重新计算

            // U轴提前转到0度
            this.entiy.RGoAngle(0, this.PasteNozzle, this.entiy.MachineConfig.AutoSpeedMode);
            base.ZGoSafeFinished();
        }
        #endregion
    }
}
