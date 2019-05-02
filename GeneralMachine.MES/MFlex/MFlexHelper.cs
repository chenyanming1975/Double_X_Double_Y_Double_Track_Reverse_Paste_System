using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Vision;
using MFlexEquipmentProject;
using Model;
using Newtonsoft.Json;

namespace GeneralMachine.MES
{
    public class MFlexHelper:SingletionProvider<MFlexHelper>
    {
        public bool EnableMES
        {
            get;
            set;
        } = false;

        public string CodeFunc
        {
            get;
            set;
        } = string.Empty;

        public string LineNo
        {
            get;
            set;
        } = "SMT-MIC-001";

        public string MachineName
        {
            get;
            set;
        } = "SMT-MIC-001";

        /// <summary>
        /// 区域
        /// </summary>
        public string WorkArea
        {
            get; set;
        } = "PSAA/PSAB";

        public string OperName
        {
            get;
            set;
        } = "3300780";

        /// <summary>
        /// 厂区类型
        /// </summary>
        public string Site
        {
            get;
            set;
        } = "SMT";

        /// <summary>
        /// 程式名
        /// </summary>
        public string ProgramName
        {
            get;
            set;
        } = string.Empty;

        [JsonIgnore]
        public VisionFlow CodeBean
        {
            get;
            set;
        } = new VisionFlow();

        /// <summary>
        /// 设备物理地址
        /// </summary>
        public string Mac
        {
            get;
            set;
        } = string.Empty;

        [JsonIgnore]
        private MFlexEquipmentProject.EquipmentImplement mflexSystem = new MFlexEquipmentProject.EquipmentImplement();

        public Tuple<bool, string> CheckBaseInfo(string code,out Dictionary<int,bool> badMarkList)
        {
            badMarkList = new Dictionary<int, bool>();
            Tuple<bool, string> result = null;
            badMarkList.Clear();

            Task<Dictionary<int, bool>> uploadTask = Task.Factory.StartNew<Dictionary<int, bool>>(() =>
            {
                MainInfo mainInfo = new MainInfo();
                mainInfo.Panel = code;
                mainInfo.Resource = this.LineNo;
                mainInfo.Machine = this.MachineName;
                mainInfo.WorkArea = this.WorkArea;
                mainInfo.TestType = "PSA";
                mainInfo.OperatorName = this.OperName;
                mainInfo.OperatorType = "Pcs";
                mainInfo.TrackType = "S";
                mainInfo.Site = this.Site;
                mainInfo.Mac = this.Mac;
                mainInfo.ProgramName = this.ProgramName;

                Dictionary<int, bool> model = new Dictionary<int, bool>();
                string[] serverResult = mflexSystem.CheckBaseInfo(mainInfo);
                bool isOK = serverResult[0] == "0";
                if (isOK)
                {
                    string badString = serverResult[1].Replace("[", "").Replace("]", "");
                    string[] badList = badString.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < badList.Length; ++i)
                    {
                        model[i + 1] = (badList[i].ToUpper() == "OK");
                    }
                }

                result = new Tuple<bool, string>(isOK, serverResult[1]);
                return model;
            });

            if (!uploadTask.Wait(500))
            {
                result = new Tuple<bool, string>(false, "获取超时，连接超时时间为:[" + 500 + "]ms");
            }
            else
            {
                if (uploadTask.Result != null)
                {
                    badMarkList = uploadTask.Result;
                }
            }

            return result;
        }

        public static void Save()
        {
            Common.SerializableHelper<MFlexHelper> helper = new Common.SerializableHelper<MFlexHelper>(MFlexHelper.Instance);
            helper.JsonSerialize(PathDefine.sPathConfigure + "MFlex.json");
            if (MFlexHelper.Instance.CodeFunc != string.Empty)
            {
                MFlexHelper.Instance.CodeBean = VisionFlow.Load(VisionToolCtrl.sPathVision+MFlexHelper.Instance.CodeFunc);
            }
        }

        public static bool Load()
        {
            if(File.Exists(PathDefine.sPathConfigure + "MFlex.json"))
            {
                Common.SerializableHelper<MFlexHelper> helper = new Common.SerializableHelper<MFlexHelper>();
                var temp = helper.DeJsonSerialize(PathDefine.sPathConfigure + "MFlex.json");
                if(temp != null)
                {
                    MFlexHelper.Instance = temp;
                    if (MFlexHelper.Instance.CodeFunc != string.Empty)
                    {
                        MFlexHelper.Instance.CodeBean = VisionFlow.Load(VisionToolCtrl.sPathVision+MFlexHelper.Instance.CodeFunc);
                    }
                    return true;
                }
            }

            return false;
        }
    }
}
