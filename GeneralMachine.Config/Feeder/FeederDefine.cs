using GeneralMachine.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Config
{
    public class FeederDefine:Common.SingletionProvider<FeederDefine>
    {
        public void Init()
        {
            for (Module module = Module.Front; module <= Module.After; ++module)
            {
                this.InstallFeederName.Add(module, new Dictionary<Feeder, string>());
                this.InstallFeeder.Add(module, new Dictionary<Feeder, FeederConfig>());

                for (Feeder feeder = Feeder.Left; feeder <= Feeder.Right; ++feeder)
                {
                    this.InstallFeederName[module].Add(feeder, string.Empty);
                }
            }
        }

        /// <summary>
        /// 安装的Feeder名称
        /// </summary>
        public Dictionary<Module, Dictionary<Feeder,string>> InstallFeederName = new Dictionary<Module, Dictionary<Feeder, string>>();

        /// <summary>
        /// 安装好的feeder配置
        /// </summary>
        [JsonIgnore]
        public Dictionary<Module, Dictionary<Feeder, FeederConfig>> InstallFeeder = new Dictionary<Module, Dictionary<Feeder, FeederConfig>>();

        /// <summary>
        /// 对外接口
        /// </summary>
        /// <param name="module"></param>
        /// <param name="feeder"></param>
        /// <returns></returns>
        public FeederConfig this[Module module, Feeder feeder]
        {
            get
            {
                if (this.InstallFeeder[module].ContainsKey(feeder))
                    return InstallFeeder[module][feeder];
                else
                    return null;
            }

            set
            {
                if (!InstallFeeder.ContainsKey(module))
                    InstallFeeder.Add(module, new Dictionary<Feeder, FeederConfig>());

                if(!InstallFeeder[module].ContainsKey(feeder))
                    InstallFeeder[module].Add(feeder, value);
                else
                    InstallFeeder[module][feeder] = value;
            }
        }

        /// <summary>
        /// 获得对应模组的对应信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public List<string> GetFeederList(Module module)
        {
            List<string> feederList = new List<string>();

            try
            {
                DirectoryInfo info = new DirectoryInfo(PathDefine.sPathFeeder + $"{CommonHelper.GetEnumDescription(typeof(Module), module)}\\");
                var files = info.GetFiles("*.json");
                foreach (FileInfo fl in files)
                {
                    feederList.Add(fl.Name.Replace(".json", ""));
                }
            }
            catch { }

            return feederList;
        }

        /// <summary>
        /// 根据名称以及模组获得对应信息
        /// </summary>
        /// <param name="module"></param>
        /// <param name="feederName"></param>
        /// <returns></returns>
        public FeederConfig GetFeederConfig(Module module, string feederName)
        {
            FeederConfig config = new FeederConfig();
            SerializableHelper<FeederConfig> helper = new SerializableHelper<FeederConfig>(config);
            return helper.DeJsonSerialize(PathDefine.sPathFeeder + $"{CommonHelper.GetEnumDescription(typeof(Module), module)}//{feederName}.json");
        }

        /// <summary>
        /// 保存Feeder信息,并自动更新到Feeder 上
        /// </summary>
        /// <param name="feederConfig"></param>
        public void SaveFeederConfig(FeederConfig feederConfig)
        {
            SerializableHelper<FeederConfig> helper = new SerializableHelper<FeederConfig>(feederConfig);
            helper.JsonSerialize(PathDefine.sPathFeeder + $"{CommonHelper.GetEnumDescription(typeof(Module),feederConfig.Module)}//{feederConfig.FeederName}.json");
        }

        public void RemoveFeederConfig(FeederConfig feederConfig)
        {
            File.Delete(PathDefine.sPathFeeder + $"{CommonHelper.GetEnumDescription(typeof(Module), feederConfig.Module)}//{feederConfig.FeederName}.json");
        }

        public bool FeederExit(Module module, string name)
        {
            return File.Exists(PathDefine.sPathFeeder + $"{CommonHelper.GetEnumDescription(typeof(Module), module)}//{name}.json");
        }

        public static void Save()
        {
            SerializableHelper<FeederDefine> helper = new SerializableHelper<FeederDefine>(FeederDefine.Instance);
            helper.JsonSerialize(PathDefine.sPathConfigure + "Feeder.json");
        }

        public static bool Load()
        {
            if (!File.Exists(PathDefine.sPathConfigure + "Feeder.json"))
            {
                FeederDefine.Instance.Init();
                return true;
            }
            SerializableHelper<FeederDefine> helper = new SerializableHelper<FeederDefine>();
            var temp = helper.DeJsonSerialize(PathDefine.sPathConfigure + "Feeder.json");
            if (temp != null)
                FeederDefine.Instance = temp;
            else
                return false;

            for(Module module = Module.Front; module<= Module.After;++module)
            {
                for(Feeder feeder = Feeder.Left; feeder <= Feeder.Right;++feeder)
                {
                    if (FeederDefine.Instance.InstallFeederName[module][feeder] != string.Empty)
                    {
                        FeederDefine.Instance[module, feeder] = FeederDefine.Instance.GetFeederConfig(module,
                            FeederDefine.Instance.InstallFeederName[module][feeder]);
                    }
                }
            }
            return true;
        }
    }
}
