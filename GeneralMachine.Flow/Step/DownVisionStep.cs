using GeneralMachine.Config;
using GeneralMachine.Flow.Tool;
using NationalInstruments.Vision;
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
    /// <summary>
    /// 飞行下视觉步骤
    /// </summary>
    public class DownVisionStep : FlowStep
    {
        public DownVisionStep(StateMachine machine, MachineEntiy entiy) : base(machine, entiy)
        {
            this.FlowName = "下视觉拍照";
            this.MoveParam.TrunAngle = machine.MachineEntiy.MachineConfig.TrunPasteAngle;
        }

        #region 流程参数准备
        public override void OnEnter()
        {
            this.MoveParam.XYPos = entiy.MachineConfig[Nozzle.Nz1].RotateCamPoint;
            // 设置曝光和打光
            Task.Factory.StartNew(() => {
                bool c1Light = false;
                bool c2Light = false;

                for (Nozzle nz = Nozzle.Nz1; nz <= Nozzle.Nz4; ++nz)
                {
                    if (this.machine.RunData.RUN_NzData[nz].State == NZ_State.Sucked)
                    {
                        if ((nz == Nozzle.Nz1 || nz == Nozzle.Nz2) && !c1Light)
                        {
                            VisionCalHelper.Instance.SetShutterAndLight(entiy.Module, Camera.Bottom1
                                              , this.machine.RunData.RUN_NzData[nz].SuckLabel);
                            c1Light = true;
                        }

                        if ((nz == Nozzle.Nz3 || nz == Nozzle.Nz4) && !c2Light)
                        {
                            VisionCalHelper.Instance.SetShutterAndLight(entiy.Module, Camera.Bottom2
                                              , this.machine.RunData.RUN_NzData[nz].SuckLabel);
                            c2Light = true;
                        }
                    }
                }
            });
          
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
            this.machine.CurStep = new CalMarkStep(this.machine, this.entiy);
            this.machine.CurStep.CurState = State.Enter;
        }

        private void CalNz(Nozzle nz, string funcName, VisionImage image)
        {
            if (this.machine.RunData.RUN_NzData[nz].State == NZ_State.Sucked)
            {
                // 移动完成开始拍照
                var item = new Tool.ResultItem();
                if (nz == Nozzle.Nz1 || nz == Nozzle.Nz2)
                    item.Camera = Camera.Bottom1;
                else
                    item.Camera = Camera.Bottom2;
                int offset = 0;
                if(nz == Nozzle.Nz2 || nz == Nozzle.Nz4)
                    offset = 800;

                item.Key = ResultKey.DownVision;
                item.ROI = this.machine.MachineEntiy.MachineConfig[nz].ViewRoi;
                item.NZIndex = nz;
                item.funcName = funcName;
                item.PCBIndex = this.machine.RunData.RUN_NzData[nz].PCBIndex;
                item.PCSIndex = this.machine.RunData.RUN_NzData[nz].PCSIndex;
                item.CaptruePos = this.entiy.MachineConfig[Nozzle.Nz1].RotateCamPoint;
                VisionCalHelper.Instance.VisionDetect(entiy.Module, item, image, offset);
            }
        }
        #endregion

        #region 流程动作
        public override void MoveXYRFinished(MoveParam param)
        {
            // 翻转到位延时
            Thread.Sleep(SystemConfig.Instance.General.DownCamDelay); // 头部晃动
            var image1 = CameraDefine.Instance.Camera[entiy.Module][Camera.Bottom1].Grab();
            var image2 = CameraDefine.Instance.Camera[entiy.Module][Camera.Bottom2].Grab();

            Task.Factory.StartNew(() =>
            {
                if (this.machine.RunData.RUN_NzData[Nozzle.Nz1].State == NZ_State.Sucked)
                    CalNz(Nozzle.Nz1, this.machine.RunData.RUN_NzData[Nozzle.Nz1].SuckLabel, image1);
                if (this.machine.RunData.RUN_NzData[Nozzle.Nz2].State == NZ_State.Sucked)
                    CalNz(Nozzle.Nz2, this.machine.RunData.RUN_NzData[Nozzle.Nz2].SuckLabel, image1);

                VisionCalHelper.Instance.ImageShow(this.entiy.Module, Camera.Bottom1, image1);
            });

            Task.Factory.StartNew(() =>
            {
                if (this.machine.RunData.RUN_NzData[Nozzle.Nz3].State == NZ_State.Sucked)
                    CalNz(Nozzle.Nz3, this.machine.RunData.RUN_NzData[Nozzle.Nz3].SuckLabel, image2);
                if (this.machine.RunData.RUN_NzData[Nozzle.Nz4].State == NZ_State.Sucked)
                    CalNz(Nozzle.Nz4, this.machine.RunData.RUN_NzData[Nozzle.Nz4].SuckLabel, image2);

                VisionCalHelper.Instance.ImageShow(this.entiy.Module, Camera.Bottom2, image2);
            });

            CameraDefine.Instance.CloseLight(this.entiy.Module, Camera.Bottom1);
            this.OnExit();
        }
        #endregion
    }
}
