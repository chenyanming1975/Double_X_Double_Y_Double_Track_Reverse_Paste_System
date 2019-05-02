using GeneralMachine.Config;
using GeneralMachine.Flow.Nodes;
using GeneralMachine.Flow.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Flow.Step
{
    /// <summary>
    /// 1.在所有标签贴完之后开始读取
    /// 2.或者 贴一个 读一个(防止另一个料干扰到这一个)
    /// </summary>
    public class ReadPcsCodeStep : FlowStep
    {
        public ReadPcsCodeStep(StateMachine machine,MachineEntiy entiy) : base(machine,entiy)
        {
            this.FlowName = "读Pcs码";
            this.MoveParam.TrunAngle = this.entiy.MachineConfig.TrunPasteAngle;
        }

        #region 内置参数
        public ReadCodeParam codeParam = null;
        public int PCBIndex = 0;
        public int CodeIndex = 0;
        #endregion

        public override void OnEnter()
        {
            if (this.machine.Program.EnableReadPCS && !this.machine.RunData.RUN_CamCodeIsDone
                && this.machine.CanOuput
                && SystemConfig.Instance.General.RunMode == RunMode.Normal)
                base.OnEnter();
            else
                this.OnExit();
        }

        public override void OnExit()
        {
            base.OnExit();
            this.machine.CurStep = new NozzleCheckStep(this.machine, this.entiy);
            this.machine.CurStep.CurState = State.Enter;
        }

        public override void CheckContinue()
        {
            if (PCBIndex < this.machine.Program.PasteInfos.Count)
            {
                if (CodeIndex < this.machine.Program.PasteInfos[PCBIndex].ReadPcsList.Count)
                {
                    this.codeParam = this.machine.Program.PasteInfos[PCBIndex].ReadPcsList[CodeIndex];

                    if (SystemConfig.Instance.General.RunMode == RunMode.Normal)
                        VisionCalHelper.Instance.SetShutterAndLight(entiy.Module, Camera.Top, this.codeParam.VisionName);

                    this.MoveParam.XYPos = this.codeParam.Pos;
                    base.CheckContinue();
                }
                else
                {
                    PCBIndex++;
                    CodeIndex = 0;
                }
            }
            else
                this.OnExit();
        }

        public override void MoveXYRFinished(MoveParam param)
        {
            // 拍照等待
            Thread.Sleep(SystemConfig.Instance.General.UpCamDelay);
            base.MoveXYRFinished(param);
        }

        public override void MoveZFinished(MoveParam param)
        {
            var item = new Tool.ResultItem();
            item.Camera = Camera.Top;
            item.funcName = this.codeParam.VisionName;
            item.CaptruePos = this.codeParam.Pos;
            var image = entiy.GrabImage(Camera.Top);
            item.Key = ResultKey.PCSCode;
            item.PCBIndex = PCBIndex;
            item.PCSIndex = CodeIndex;

            VisionCalHelper.Instance.VisionDetect(entiy.Module, item, image);
            VisionCalHelper.Instance.ImageShow(this.entiy.Module, Camera.Top, image);
            base.MoveZFinished(param);
        }
    }
}
