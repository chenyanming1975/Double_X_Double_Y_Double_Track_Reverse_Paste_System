using GeneralMachine.Common;
using GeneralMachine.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Track
{
    public class TrackInputStep:TrackStep
    {
        public TrackInputStep(TrackEntiy entiy, TrackStateMachine machine)
            :base(entiy, machine)
        {

        }

        public override void OnEntiy()
        {
            base.OnEntiy();
            entiy.CarryMove(false);
            entiy.StopMove(true);
            entiy.ConveryStop();
            // 进入Wait
        }

        public override void DoAction()
        {
            // 感应进板口
            if (entiy.TrackInput(entiy.Config.FlowInOutMode) || entiy.TrackReach()
                || SystemConfig.Instance.General.RunMode == RunMode.TestRun)
            {
                this.machine.InputTime.Restart();

                // 开始进板
                entiy.ConveryRun(entiy.Config.FlowInOutMode, true);
                this.CurState = State.WaitFinished;
                return;
            }

            if (this.Elapsed > ConstDefine.iWaitInputTime)
            {
                // 报警
            }
        }

        public override void WaitFinished()
        {
            // 进板口不再感应到信号
            if (entiy.TrackReach() || SystemConfig.Instance.General.RunMode == RunMode.TestRun)
            {
                if(SystemConfig.Instance.General.RunMode == RunMode.TestRun)
                    Thread.Sleep(1500);

                Thread.Sleep(entiy.Config.CarryBefore);
                entiy.CarryMove(true);
                entiy.ConveryStop();
                Thread.Sleep(entiy.Config.CarryOver);
                entiy.StopMove(false);
                this.CurState = State.Finished;
            }

            if (this.Elapsed > ConstDefine.iInputTime)
            {
                // 报警
                MsgHelper.Instance.AddMessage(MsgLevel.Warn, "轨道进板超时,请重新进板", (int)this.machine.Track);
            }
        }

        public override void Finished()
        {
            this.machine.InputTime.Stop();

            entiy.PastedOK = false;
            entiy.ReachOK = true;
            this.CurState = State.Exit;
        }

        public override void OnExit(TrackStateMachine stateMachine)
        {
            base.OnExit(stateMachine);
            stateMachine.CurStep = new TrackOutputStep(TrackManager.Instance.TrackEntiy[stateMachine.Track],this.machine);
        }
    }
}
