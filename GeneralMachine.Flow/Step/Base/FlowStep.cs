using GeneralMachine.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralMachine.Common;
using GeneralMachine.Motion;
using System.Threading;
using System.Diagnostics;

namespace GeneralMachine.Flow.Step
{
    public abstract class FlowStep
    {
        public FlowStep(StateMachine machine,MachineEntiy entiy)
        {
            this.entiy = entiy;
            this.machine = machine;
        }

        public MachineEntiy entiy = null;
        public StateMachine machine = null;

        public string FlowName = string.Empty;

        /// <summary>
        /// 当前步骤的状态
        /// </summary>
        public enum State
        {
            Idle,
            Enter,
            CheckContinue, // 检查是否继续
            CheckTrun, // 检查是否需要翻转
            TrunGoXY, // 
            Trun, // 
            StartMoveXYR, // 开始移动XYR
            MoveXYR, // 移动XY
            MoveXYRFinished, // 移动XY是否完成
            MoveZ, // 
            MoveZFinished,
            ZMoveSafe, // Z轴回安全高度---->比做
            ZMoveSafeFinished,
            Exit,
        }

        /// <summary>
        /// 当前状态
        /// </summary>
        public State CurState
        {
            get;
            set;
        } = State.Idle;

        /// <summary>
        /// 上一次状态
        /// </summary>
        public State LastState
        {
            get;
            set;
        } = State.Idle;

        /// <summary>
        /// 步骤计时
        /// </summary>
        private Stopwatch sw = new Stopwatch();

        /// <summary>
        /// 移动参数
        /// </summary>
        public MoveParam MoveParam = new MoveParam();

        /// <summary>
        /// 处理
        /// </summary>
        public void Handler()
        {
            switch (CurState)
            {
                case State.Enter:
                    this.OnEnter();
                    break;
                case State.CheckTrun:
                    this.CheckTrun(this.MoveParam);
                    break;
                case State.CheckContinue:
                    this.CheckContinue();
                    break;
                case State.TrunGoXY:
                    this.TrunGoXY(this.MoveParam);
                    break;
                case State.Trun:
                    this.Trun(this.MoveParam);
                    break;
                case State.StartMoveXYR:
                    this.StartMoveXYR();
                    break;
                case State.MoveXYR:
                    this.MoveXYR(this.MoveParam);
                    break;
                case State.MoveXYRFinished:
                    this.MoveXYRFinished(this.MoveParam);
                    break;
                case State.MoveZ:
                    this.MoveZ(this.MoveParam);
                    break;
                case State.MoveZFinished:
                    this.MoveZFinished(this.MoveParam);
                    break;
                case State.ZMoveSafe:
                    this.ZGoSafe();
                    break;
                case State.ZMoveSafeFinished:
                    this.ZGoSafeFinished();
                    break;
                case State.Exit:
                    this.OnExit();
                    break;
            }
        }

        /// <summary>
        /// 判断终止条件
        /// </summary>
        public virtual void CheckContinue()
        {
            this.CurState = State.CheckTrun;
        }

        #region 不可继承，固定步骤
        /// <summary>
        /// 检查是否需要翻转
        /// </summary>
        /// <param name="param"></param>
        public void CheckTrun(MoveParam param)
        {
            if(entiy.MachineAxis.Trun.AxisReach(param.TrunAngle))
            {
                this.CurState = State.StartMoveXYR;
            }
            else
            {
                this.CurState = State.TrunGoXY;
            }
        }

        /// <summary>
        /// 到翻转点
        /// </summary>
        public void TrunGoXY(MoveParam param)
        {
            if (entiy.XYReach(machine.MachineEntiy.MachineConfig[Nozzle.Nz1].RotateCamPoint))
            {
                this.CurState = State.Trun;
            }
            else
            {
                entiy.XYGoPos(machine.MachineEntiy.MachineConfig[Nozzle.Nz1].RotateCamPoint, entiy.MachineConfig.AutoSpeedMode);
                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// 翻转
        /// </summary>
        /// <param name="param"></param>
        public void Trun(MoveParam param)
        {
            if(entiy.MachineAxis.Trun.AxisReach(param.TrunAngle))
            {
                this.CurState = State.StartMoveXYR;
            }
            else
            {
                entiy.TurnGoPos(param.TrunAngle, entiy.MachineConfig.AutoSpeedMode);
                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// 移动XYR
        /// </summary>
        /// <param name="param"></param>
        public void MoveXYR(MoveParam param)
        {
            if (entiy.XYReach(param.XYPos) && entiy.UReach(param.RPos))
            {
                CurState = State.MoveXYRFinished;
            }
            else
            {
                entiy.XYGoPos(param.XYPos, entiy.MachineConfig.AutoSpeedMode);
                if (param.MoveR)
                {
                    for (int i = 0; i < param.RPos.Length; ++i)
                    {
                        this.entiy.RGoAngle(param.RPos[i], (Nozzle)i, entiy.MachineConfig.AutoSpeedMode);
                    }
                }
                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// 移动Z
        /// </summary>
        /// <param name="param"></param>
        public void MoveZ(MoveParam param)
        {
            if (!param.MoveZ)
            {
                this.CurState = State.MoveZFinished;
                return;
            }

            bool zReach = true;
            for (int nz = 0; nz < param.NzUsed.Length; ++nz)
            {
                if (param.NzUsed[nz])
                {
                    if (!entiy.MachineAxis.Z[nz].AxisReach(param.ZPos[nz]))
                    {
                        zReach = false;
                        entiy.ZGoPos((Nozzle)nz, param.ZPos[nz], entiy.MachineConfig.AutoSpeedMode);
                    }
                }
            }

            if (zReach)
            {
                this.CurState = State.MoveZFinished;
            }
            else
            {
                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// Z回安全高度
        /// </summary>
        public void ZGoSafe()
        {
            if (entiy.ZReachSafe)
            {
                this.CurState = State.ZMoveSafeFinished;
            }
            else
            {
                entiy.ZGoSafe(entiy.MachineConfig.AutoSpeedMode);
            }
        }
        #endregion

        #region 步骤异化
        /// <summary>
        /// 开始移动XYR
        /// </summary>
        public virtual void StartMoveXYR()
        {
             this.CurState = State.MoveXYR;
        }

        /// <summary>
        /// 移动XYR结束，开始移动Z
        /// </summary>
        /// <param name="param"></param>
        public virtual void MoveXYRFinished(MoveParam param)
        {
            this.CurState = State.MoveZ;
        }

        /// <summary>
        /// 移动Z完成,开始Z回安全高度
        /// </summary>
        /// <param name="param"></param>
        public virtual void MoveZFinished(MoveParam param)
        {
            this.CurState = State.ZMoveSafe;
        }

        /// <summary>
        /// Z回安全高度完成
        /// </summary>
        public virtual void ZGoSafeFinished()
        {
            this.CurState = State.CheckContinue;
        }

        public virtual void OnEnter()
        {
            MsgHelper.Instance.AddMessage(MsgLevel.Info, $"{this.FlowName} 动作开始", (int)this.entiy.Module);
            sw.Restart();
            this.CurState = State.CheckContinue;
        }

        public virtual void OnExit()
        {
            if (sw.IsRunning)
                MsgHelper.Instance.AddMessage(MsgLevel.Info, $"{this.FlowName} 完成耗时：{sw.ElapsedMilliseconds} ms", (int)this.entiy.Module);
            sw.Stop();
        }

        public virtual void ReStart()
        {
            if (this.CurState == State.Idle)
            {
                this.CurState = this.LastState;
                this.LastState = State.Idle;
            }
        }

        public virtual void Pause()
        {
            if (this.CurState != State.Idle)
            {
                this.LastState = this.CurState;
                this.CurState = State.Idle;
            }
        }
        #endregion
    }
}
