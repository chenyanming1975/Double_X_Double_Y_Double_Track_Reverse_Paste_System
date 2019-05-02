using GeneralMachine.Config;
using GeneralMachine.Flow.Nodes;
using GeneralMachine.Flow.Tool;
using NationalInstruments.Vision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Flow.Step
{
    public class CalBadmarkStep : FlowStep
    {
        public CalBadmarkStep(StateMachine machine, MachineEntiy entiy) : base(machine,entiy)
        {
            this.FlowName = "Badmark拍照";
            this.MoveParam.TrunAngle = machine.MachineEntiy.MachineConfig.TrunPasteAngle;
        }

        #region 内置参数
        private BadmarkParam badmark = null;
        private int PCBIndex = 0;
        private int BadmarkIndex = 0;
        #endregion

        #region 流程确认
        public override void OnEnter()
        {
            if (!this.machine.RunData.RUN_CamBadmarkIsDone && this.machine.Program.EnableBadmark
                &&  SystemConfig.Instance.General.RunMode == RunMode.Normal)
            {
                base.OnEnter();
            }
            else
                this.OnExit();
        }

        public override void OnExit()
        {
            base.OnExit();
            this.machine.RunData.RUN_CamBadmarkIsDone = true;
            this.machine.CurStep = new PasteLabelStep(this.machine, this.entiy);
            this.machine.CurStep.CurState = State.Enter;
        }

        public override void CheckContinue()
        {
            if (PCBIndex < this.machine.Program.PasteInfos.Count)
            {
                if (BadmarkIndex < this.machine.Program.PasteInfos[PCBIndex].BadmarkList.Count)
                {
                    this.badmark = this.machine.Program.PasteInfos[PCBIndex].BadmarkList[BadmarkIndex];
                    VisionCalHelper.Instance.SetShutterAndLight(entiy.Module,Camera.Top, this.badmark.VisionName);
                    this.MoveParam.XYPos = this.badmark.Pos;
                    base.CheckContinue();
                }
                else
                {
                    PCBIndex++;
                    BadmarkIndex = 0;
                }
            }
            else
                this.OnExit();
        }
        #endregion

        #region 流程动作
        public override void MoveXYRFinished(MoveParam param)
        {
            // 拍照等待
            Thread.Sleep(SystemConfig.Instance.General.UpCamDelay);
            if (SystemConfig.Instance.General.RunMode == RunMode.TestRun)
            {
                this.BadmarkIndex++;
            }
            else
            {
                var item = new Tool.ResultItem();
                item.Camera = Camera.Top;
                item.funcName = this.badmark.VisionName;
                item.CaptruePos = this.badmark.Pos;
                VisionImage image = entiy.GrabImage(Camera.Top);
                item.Key = ResultKey.Badmark;
                item.PCBIndex = PCBIndex;
                item.PCSIndex = BadmarkIndex;
                VisionCalHelper.Instance.ImageShow(this.entiy.Module, Camera.Top, image);
                VisionCalHelper.Instance.VisionDetect(entiy.Module, item, image);
            }

            base.MoveXYRFinished(param);
        }
        #endregion
    }
}
