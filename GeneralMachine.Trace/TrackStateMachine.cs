using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Track
{
    /// <summary>
    /// 
    /// </summary>
    public class TrackStateMachine
    {
        public TrackStateMachine(Config.Track track)
        {
            this.Track = track;
        }

        public Config.Track Track
        {
            get;
            set;
        } = Config.Track.ForntTrack;

        /// <summary>
        /// 初始化轨道
        /// </summary>
        public void Init()
        {
            CurStep = new TrackInputStep(TrackManager.Instance.TrackEntiy[this.Track], this);
            CurStep.CurState = TrackStep.State.Pause;
        }

        public Task TrackTask = null;

        public void StartTrack()
        {
            if(TrackTask == null)
            {
                CurStep.CurState = TrackStep.State.Enter;
                TrackTask = Task.Factory.StartNew(() =>
                {
                    this.TrackRun();
                });
            }
            else
            {
                CurStep.ReActive();
            }
        }

        public void PauseTrack()
        {
            CurStep.Pasue();
        }

        public TrackStep CurStep
        {
            get;
            set;
        } = null;

        /// <summary>
        /// 是否退出
        /// </summary>
        public bool bSystemExit
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 状态机运行
        /// </summary>
        public void TrackRun()
        {
            while(!this.bSystemExit)
            {
                Thread.Sleep(20);

                CurStep?.Handler(this);
            }
        }

        public Stopwatch InputTime = new Stopwatch();
        public Stopwatch OutputTime = new Stopwatch();

    }
}
