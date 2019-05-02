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
    /// <summary>
    /// 上相机计算
    /// 1. 计算mark点
    /// </summary>
    public class CalMarkStep : FlowStep
    {
        public CalMarkStep(StateMachine machine, MachineEntiy entiy) :base(machine,entiy)
        {
            this.FlowName = "Mark点拍照";
            this.MoveParam.TrunAngle = machine.MachineEntiy.MachineConfig.TrunPasteAngle;
        }

        #region 内置参数
        public MarkParam markParam = null;
        public int PCBIndex = 0;
        public int MarkIndex = 0;
        #endregion

        #region 流程确认
        public override void OnEnter()
        {
            if (!this.machine.RunData.RUN_CamMarkIsDone)
            {
                base.OnEnter();
            }
            else
            {
                this.OnExit();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            this.machine.RunData.RUN_CamMarkIsDone = true;
            this.machine.CurStep = new CalBadmarkStep(this.machine, this.entiy);
            this.machine.CurStep.CurState = State.Enter;
        }

        /// <summary>
        /// 检查条件是否满足
        /// </summary>
        public override void CheckContinue()
        {
            if (PCBIndex < this.machine.Program.PasteInfos.Count)
            {
                if (MarkIndex < this.machine.Program.PasteInfos[PCBIndex].MarkPtList.Count)
                {
                    this.markParam = this.machine.Program.PasteInfos[PCBIndex].MarkPtList[MarkIndex];

                    VisionCalHelper.Instance.SetShutterAndLight(entiy.Module, Camera.Top, this.markParam.VisionName);

                    this.MoveParam.XYPos = this.markParam.Pos;
                    base.CheckContinue();
                }
                else
                {
                    PCBIndex++;
                    MarkIndex = 0;
                }
            }
            else
            {
                this.machine.CT.Restart();
                this.OnExit();
            }
        }
        #endregion

        #region 流程动作
        /// <summary>
        /// 移动结束后拍照
        /// </summary>
        /// <param name="param"></param>
        public override void MoveXYRFinished(MoveParam param)
        {
            if (this.machine.PCBReach)
            {
                if(this.MarkIndex == 0 && this.PCBIndex == 0)
                {
                    Thread.Sleep(SystemConfig.Instance.General.ReachAfterDelay);
                }

                // 拍照等待
                Thread.Sleep(SystemConfig.Instance.General.UpCamDelay);
                var item = new Tool.ResultItem();
                item.Camera = Camera.Top;
                item.funcName = this.markParam.VisionName;
                item.CaptruePos = this.markParam.Pos;
                VisionImage image = entiy.GrabImage(Camera.Top);
                item.Key = ResultKey.Mark;
                item.PCBIndex = PCBIndex;
                item.PCSIndex = MarkIndex;
                item.Mark = this.machine.Program.PasteInfos[PCBIndex].MarkPtList[MarkIndex].MarkID;
                VisionCalHelper.Instance.VisionDetect(entiy.Module, item, image);
                VisionCalHelper.Instance.ImageShow(this.entiy.Module, Camera.Top, image);
                MarkIndex++;
                base.MoveXYRFinished(param);
            }
        }
        #endregion
    }
}
