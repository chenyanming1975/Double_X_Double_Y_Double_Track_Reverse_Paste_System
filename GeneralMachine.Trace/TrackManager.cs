using System;
using System.Collections.Generic;
using GeneralMachine.Config;

namespace GeneralMachine.Track
{
    public class TrackManager:Common.SingletionProvider<TrackManager>
    {
        public Dictionary<Config.Track, TrackEntiy> TrackEntiy = new Dictionary<Config.Track, TrackEntiy>();

        public Dictionary<Config.Track, TrackStateMachine> TrackMachine = new Dictionary<Config.Track, TrackStateMachine>();

        /// <summary>
        /// 轨道初始化
        /// </summary>
        public void TrackInit()
        {
            if(TrackEntiy.Count == 0)
            {
                foreach(Config.Track track in Enum.GetValues(typeof(Config.Track)))
                {
                    TrackEntiy.Add(track, new TrackEntiy(SystemConfig.Instance.Tracks[track], IODefine.Instance.TrackIO[track]));
                }
            }

            if (TrackMachine.Count == 0)
            {
                foreach (Config.Track track in Enum.GetValues(typeof(Config.Track)))
                {
                    var machine = new TrackStateMachine(track);
                    machine.Init();
                    TrackMachine.Add(track, machine);
                }
            }
        }

        #region 对外接口
        public void TrackStart(Config.Track track)
        {
            TrackMachine[track].StartTrack();
        }

        public void TrackReset(Config.Track track)
        {
            TrackMachine[track].Init();
        }

        /// <summary>
        /// 轨道暂停
        /// </summary>
        public void TrackPasue(Config.Track track)
        {
            TrackMachine[track].PauseTrack();
        }

        public void TrackExit(Config.Track track)
        {
            TrackMachine[track].bSystemExit = true;
        }
        #endregion
    }
}
