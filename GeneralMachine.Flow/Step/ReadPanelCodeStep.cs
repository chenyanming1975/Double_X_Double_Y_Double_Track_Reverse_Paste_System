using GeneralMachine.Config;
using GeneralMachine.Flow.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Flow.Step
{
    /// <summary>
    /// 读取Panel Code 步骤
    /// </summary>
    public class ReadPanelCodeStep: FlowStep
    {
        public ReadPanelCodeStep(StateMachine machine,MachineEntiy entiy) : base(machine,entiy)
        {
            this.FlowName = "读Panel码";
            this.MoveParam.TrunAngle = machine.MachineEntiy.MachineConfig.TrunPasteAngle;
        }

        public override void CheckContinue()
        {
            this.MoveParam.XYPos = this.machine.Program.ReadPanel.Pos;
            base.CheckContinue();
        }

        public override void MoveXYRFinished(MoveParam param)
        {
            base.MoveXYRFinished(param);
            // 需要即时计算
            // 进行Panel 绑定或者 BadMark 对应
        }

        public override void OnEnter()
        {
            // 判断
            // 1.是否启用
            // 2.是否已经做过
            // 3.程式是否为空
            if (!this.machine.RunData.RUN_CamPanelIsDone && this.machine.Program.EnableReadPanel
                && this.machine.Program.ReadPanel != null
                && SystemConfig.Instance.General.RunMode == RunMode.Normal)
                base.OnEnter();
            else
                this.OnExit();
        }

        public override void OnExit()
        {
            base.OnExit();
            this.machine.RunData.RUN_CamPanelIsDone = true;
            this.machine.CurStep = new CalMarkStep(this.machine, this.entiy);
            this.machine.CurStep.CurState = State.Enter;
        }
    }
}
