using GeneralMachine.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Track
{
    public abstract class TrackStep
    {
        /// <summary>
        /// 流程中的状态
        /// </summary>
        public enum State
        {
            Enter,// 进入
            DoAction, // 执行
            WaitFinished, // 等待完成
            Finished, //完成
            Exit, // 退出
            Pause, // 暂停
        }

        public TrackStep(TrackEntiy entiy,TrackStateMachine machine)
        {
            this.entiy = entiy;
            this.machine = machine;
        }

        #region 保存步骤内参数
        /// <summary>
        /// 当前状态
        /// </summary>
        public State CurState
        {
            get;
            set;
        } = State.Enter;

        /// <summary>
        /// 上一次状态
        /// </summary>
        public State LastState = State.Enter;
        private Stopwatch stopwatch = new Stopwatch();

        public TrackEntiy entiy = null;
        public TrackStateMachine machine = null;

        /// <summary>
        /// 花费时间
        /// </summary>
        public long Elapsed
        {
            get
            {
                return stopwatch.ElapsedMilliseconds;
            }
        }
        #endregion

        #region 对外方法
        public void Handler(TrackStateMachine stateMachine)
        {
            Thread.Sleep(5);
            switch(CurState)
            {
                case State.Enter:
                    this.OnEntiy();
                    break;
                case State.DoAction:
                    this.DoAction();
                    break;
                case State.WaitFinished:
                    this.WaitFinished();
                    break;
                case State.Finished:
                    this.Finished();
                    break;
                case State.Exit:
                    this.OnExit(stateMachine);
                    break;
            }
        }

        /// <summary>
        /// 重新激活
        /// </summary>
        /// <returns></returns>
        public void ReActive()
        {
            stopwatch.Restart();
            this.CurState = this.LastState;
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pasue()
        {
            entiy.ConveryStop();
            stopwatch.Stop();
            this.LastState = CurState;
            CurState = State.Pause;
        }
        #endregion

        public virtual void OnEntiy()
        {
            stopwatch.Restart();
            this.CurState = State.DoAction;
        }

        public virtual void OnExit(TrackStateMachine stateMachine)
        {
            stopwatch.Stop();
        }

        #region 必须重写的方法
        public abstract void DoAction();

        public abstract void WaitFinished();

        public abstract void Finished();
        #endregion
    }
}
