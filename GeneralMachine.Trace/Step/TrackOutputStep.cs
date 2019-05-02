using GeneralMachine.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Track
{
    public class TrackOutputStep : TrackStep
    {
        public TrackOutputStep(TrackEntiy entiy, TrackStateMachine machine)
            : base(entiy, machine)
        {
        }

        /// <summary>
        /// 等待出板
        /// </summary>
        /// <param name="entiy"></param>
        /// <returns></returns>
        public override void DoAction()
        {
            if (entiy.PastedOK) // 等待贴装完成
            {
                this.machine.OutputTime.Restart();

                entiy.ReachOK = false;
                entiy.StopMove(false);
                entiy.CarryMove(false);
                Thread.Sleep(200);
                entiy.ConveryRun(entiy.Config.FlowInOutMode, false);
                this.CurState = State.WaitFinished;
            }
        }

        /// <summary>
        /// 等待出板完全
        /// </summary>
        /// <param name="entiy"></param>
        /// <returns></returns>
        public override void WaitFinished()
        {
            if (entiy.TrackOutput(entiy.Config.FlowInOutMode) || SystemConfig.Instance.General.RunMode == RunMode.TestRun)
            {
                if(SystemConfig.Instance.General.RunMode == RunMode.TestRun)
                {
                    Thread.Sleep(1500);
                    this.CurState = State.Finished;
                    return;
                }

                if (entiy.Config.OnLine) // 是否是在线式
                {
                    if(entiy.Config.MointorAfterRequeset) // 侦测后要板
                    {
                        if(entiy.TrackIO.IO_AfterLineRequest.GetIO())
                        {
                            entiy.ConveryRun(entiy.Config.FlowInOutMode, false);
                            this.CurState = State.Finished;
                        }
                        else
                        {
                            entiy.ConveryStop();// 暂停等待后要办信号
                        }
                    }
                    else
                    {
                        entiy.ConveryStop(); // 不侦测手动拿出
                        this.CurState = State.Finished;
                    }
                }
                else // 离线式，运动一直到，IO没有感应到
                {
                    this.CurState = State.Finished;
                }
            }
        }

        public override void Finished()
        {
            if (!entiy.TrackOutput(entiy.Config.FlowInOutMode)
                 || SystemConfig.Instance.General.RunMode == RunMode.TestRun) // 感应不到出板口了
            {
                Thread.Sleep(entiy.Config.OutputDelay); // 出板延时后
                if (!entiy.TrackOutput(entiy.Config.FlowInOutMode)
                     || SystemConfig.Instance.General.RunMode == RunMode.TestRun) // 出板完成
                {
                    this.machine.OutputTime.Stop();

                    entiy.ConveryStop();
                    this.CurState = State.Exit;
                }
            }
        }

        public override void OnExit(TrackStateMachine stateMachine)
        {
            base.OnExit(stateMachine);
            stateMachine.CurStep = new TrackInputStep(TrackManager.Instance.TrackEntiy[stateMachine.Track], this.machine);
        }
    }
}
