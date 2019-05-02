using GeneralMachine.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    public class SpeedDefine:Common.SingletionProvider<SpeedDefine>
    {
        public static void Save()
        {
            SerializableHelper<SpeedDefine> helper = new SerializableHelper<SpeedDefine>(SpeedDefine.Instance);
            helper.JsonSerialize(PathDefine.sPathConfigure + "Speed.json");
        }

        public void Init()
        {
            if(this.SpeedConfig.Count == 0)
            {
                foreach (Module module in Enum.GetValues(typeof(Module)))
                {
                    SpeedConfig.Add(module, new Config.SpeedConfig());
                }
            }
        }

        public static bool Load()
        {
            if (!File.Exists(PathDefine.sPathConfigure + "Speed.json"))
            {
                SpeedDefine.Instance.Init();
                return true;
            }
            SerializableHelper<SpeedDefine> helper = new SerializableHelper<SpeedDefine>();
            var temp = helper.DeJsonSerialize(PathDefine.sPathConfigure + "Speed.json");
            if (temp != null)
                SpeedDefine.Instance = temp;
            else
                return false;
            return true;
        }

        public Dictionary<Module, SpeedConfig> SpeedConfig = new Dictionary<Module, SpeedConfig>();

        public SpeedConfig this[Module module]
        {
            get
            {
                return this.SpeedConfig[module];
            }

            set
            {
                this.SpeedConfig[module] = value;
            }
        }
    }
}
