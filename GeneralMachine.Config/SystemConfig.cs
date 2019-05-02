using GeneralMachine.Common;
using GeneralMachine.Definition;
using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    /// <summary>
    /// 系统配置-保存通用配置---可以随时更改的参数
    /// </summary>
    public class SystemConfig: SingletionProvider<SystemConfig>
    {
        /// <summary>
        /// 机器内部参数
        /// </summary>
        public Dictionary<Module, MachineConfig> Machines = new Dictionary<Module, MachineConfig>
        {
            {Module.Front, new MachineConfig() },
            {Module.After, new MachineConfig() },
        };

        /// <summary>
        /// 轨道配置
        /// </summary>
        public Dictionary<Track, TrackConfig> Tracks = new Dictionary<Track, TrackConfig>
        {
            {Track.ForntTrack, new TrackConfig() },
            {Track.AfterTrack, new TrackConfig() },
        };

        /// <summary>
        /// 通用配置
        /// </summary>
        public GeneralConfig General
        {
            get;
            set;
        } = new GeneralConfig();

        public static void Save()
        {
            SerializableHelper<SystemConfig> helper = new SerializableHelper<SystemConfig>(SystemConfig.Instance);
            helper.JsonSerialize(PathDefine.sPathConfigure + "System.json");
        }

        public static bool Load()
        {
            if (!File.Exists(PathDefine.sPathConfigure + "System.json"))
            {
                SystemConfig.Instance = new SystemConfig();
                return true;
            }

            SerializableHelper<SystemConfig> helper = new SerializableHelper<SystemConfig>();
            var temp = helper.DeJsonSerialize(PathDefine.sPathConfigure + "System.json");
            if (temp != null)
                SystemConfig.Instance = temp;
            else
                return false;
            return true;
        }

    }
}
