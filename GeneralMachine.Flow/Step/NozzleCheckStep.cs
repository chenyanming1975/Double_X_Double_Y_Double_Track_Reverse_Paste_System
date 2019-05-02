using GeneralMachine.Config;
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
    /// 吸嘴回拍主要检测吸嘴上是否有沾料
    /// </summary>
    public class NozzleCheckStep: FlowStep
    {
        public NozzleCheckStep(StateMachine machine, MachineEntiy entiy)
            :base(machine, entiy)
        {
            this.FlowName = "回拍";
            this.MoveParam.XYPos = machine.MachineEntiy.MachineConfig[Nozzle.Nz1].RotateCamPoint;
            this.MoveParam.MoveR = true;
            this.MoveParam.RPos = new double[4];
            this.MoveParam.MoveZ = false;
            this.MoveParam.TrunAngle = this.entiy.MachineConfig.TrunPasteAngle;
        }

        public override void OnEnter()
        {
            if (SystemConfig.Instance.General.EnableNozzleCheck
                && SystemConfig.Instance.General.RunMode == RunMode.Normal)
            {
                #region 设置曝光和打光
                if (SystemConfig.Instance.General.RunMode == RunMode.Normal)
                {
                    for (Nozzle nz = Nozzle.Nz1; nz <= Nozzle.Nz4; ++nz)
                    {
                        if (this.machine.RunData.RUN_NzData[nz].State == NZ_State.Pasted)
                        {
                            if (nz == Nozzle.Nz1 || nz == Nozzle.Nz2)
                                VisionCalHelper.Instance.SetShutterAndLight(entiy.Module, Camera.Bottom1
                                                      , this.machine.RunData.RUN_NzData[nz].SuckLabel);
                            else
                                VisionCalHelper.Instance.SetShutterAndLight(entiy.Module, Camera.Bottom2
                                                      , this.machine.RunData.RUN_NzData[nz].SuckLabel);
                            break;
                        }
                    }
                }
                #endregion
                base.OnEnter();
            }
            else
                this.OnExit();
        }

        public override void OnExit()
        {
            base.OnExit();
            this.machine.CurStep = new RejectLabelStep(this.machine, this.entiy);
            this.machine.CurStep.CurState = State.Enter;
        }

        public override void MoveXYRFinished(MoveParam param)
        {
            if (SystemConfig.Instance.General.RunMode == RunMode.TestRun) // 空跑
            {
                this.OnExit();
            }
            else
            {
                #region 拍照检测面积是否匹配
                //var image1 = CameraDefine.Instance.Camera[entiy.Module][Camera.Bottom1].Grab();
                //var image2 = CameraDefine.Instance.Camera[entiy.Module][Camera.Bottom2].Grab();

                //Task.Factory.StartNew(() =>
                //{
                //if (this.machine.RunData.RUN_NzData[Nozzle.Nz1].State == NZ_State.Sucked)
                //    CalNz(Nozzle.Nz1, this.machine.RunData.RUN_NzData[Nozzle.Nz1].SuckLabel, image1);
                //if (this.machine.RunData.RUN_NzData[Nozzle.Nz2].State == NZ_State.Sucked)
                //    CalNz(Nozzle.Nz2, this.machine.RunData.RUN_NzData[Nozzle.Nz2].SuckLabel, image1);

                //VisionCalHelper.Instance.ImageShow(this.entiy.Module, Camera.Bottom1, image1);
                //});

                //Task.Factory.StartNew(() =>
                //{
                //if (this.machine.RunData.RUN_NzData[Nozzle.Nz3].State == NZ_State.Sucked)
                //    CalNz(Nozzle.Nz3, this.machine.RunData.RUN_NzData[Nozzle.Nz3].SuckLabel, image2);
                //if (this.machine.RunData.RUN_NzData[Nozzle.Nz4].State == NZ_State.Sucked)
                //    CalNz(Nozzle.Nz4, this.machine.RunData.RUN_NzData[Nozzle.Nz4].SuckLabel, image2);

                //VisionCalHelper.Instance.ImageShow(this.entiy.Module, Camera.Bottom2, image2);
                //});

                //CameraDefine.Instance.CloseLight(this.entiy.Module, Camera.Bottom1);
                #endregion
                this.OnExit();
            }
            base.MoveXYRFinished(param);
        }
    }
}
