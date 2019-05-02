using GeneralMachine.Config;
using GeneralMachine.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Flow.Step
{
    public class RejectLabelStep : FlowStep
    {
        public RejectLabelStep(StateMachine machine, MachineEntiy entiy) : base(machine, entiy)
        {
            this.FlowName = "抛料";
            this.MoveParam.TrunAngle = this.entiy.MachineConfig.TrunSuckAngle;
        }

        #region 内置参数
        public List<Nozzle> RejectList = new List<Nozzle>();
        #endregion

        #region 参数准备
        public override void OnEnter()
        {
            for (Nozzle nz = Nozzle.Nz1; nz <= Nozzle.Nz4; ++nz)
            {
                switch (this.machine.RunData.RUN_NzData[nz].State)
                {
                    case NZ_State.Sucked:
                    case NZ_State.DownSuccessed:
                    case NZ_State.DownFailed:
                        this.RejectList.Add(nz);
                        this.MoveParam.NzUsed[(int)nz] = true;
                        this.MoveParam.ZPos[(int)nz] = this.entiy.MachineConfig[nz].DropHeight;
                        this.MoveParam.MoveR = true;
                        this.MoveParam.MoveZ = true;
                        this.MoveParam.XYPos = this.entiy.MachineConfig.DropPoint;
                        break;
                    case NZ_State.Pasted:
                        this.machine.RunData.RUN_NzData[nz].State = NZ_State.NoUsed;
                        break;
                }
            }

            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();

            // 可以出板
            if(this.machine.CanOuput)
                this.machine.RunData.Restet(this.machine.Program);

            this.machine.CurStep = new SuckLabelStep(this.machine, this.entiy);
            this.machine.CurStep.CurState = State.Enter;
        }

        public override void CheckContinue()
        {
            if (this.RejectList.Count > 0)
            {
                this.MoveParam.XYPos = this.entiy.MachineConfig.DropPoint;
                base.CheckContinue();
            }
            else
                this.OnExit();
        }
        #endregion

        #region 流程动作
        public override void MoveXYRFinished(MoveParam param)
        {
            foreach(Nozzle nozzle in this.RejectList)
            {
                this.entiy.MachineIO.VaccumSuck[(int)nozzle].SetIO(false);
                this.entiy.MachineIO.VaccumPO[(int)nozzle].SetIO(true);
            }

            base.MoveXYRFinished(param);
        }

        public override void MoveZFinished(MoveParam param)
        {
            Thread.Sleep(this.entiy.MachineConfig.DropDelay);

            foreach (Nozzle nozzle in this.RejectList)
            {
                this.entiy.MachineIO.VaccumPO[(int)nozzle].SetIO(false);
                int pcbIndex = this.machine.RunData.RUN_NzData[nozzle].PCBIndex;
                int pcsIndex = this.machine.RunData.RUN_NzData[nozzle].PCSIndex;

                this.machine.RunData.SetPasteState(pcbIndex,pcsIndex, -1);
                this.machine.RunData.RUN_NzData[nozzle].State = NZ_State.NoUsed;
                ReportHelper.Instance[this.machine.Module][nozzle] += 1;
            }

            RejectList.Clear();
            base.MoveZFinished(param);
        }
        #endregion
    }
}
