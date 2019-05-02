using GeneralMachine.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    public class MylarLabel
    {
        public MylarLabel()
        {
         
        }

        public void Init()
        {
            for (int i = 0; i < 4; ++i)
                NzCanUsed[i] = true;
        }

        /// <summary>
        /// 物料名称
        /// </summary>
        [Category("物料")]
        [DisplayName("名称")]
        public string LabelName { get; set; } = string.Empty;

        /// <summary>
        /// 物料视觉名称
        /// </summary>
        [Category("视觉配置")]
        [DisplayName("视觉方法")]
        public string LabelVisionName { get; set; } = string.Empty;

        /// <summary>
        /// 吸嘴可使用贴附点 默认全部贴附
        /// </summary>
        [Category("吸嘴配置")]
        [DisplayName("吸嘴使用权限")]
        public bool[] NzCanUsed
        {
            get;
            set;
        } = new bool[4];

        /// <summary>
        /// 尺寸
        /// </summary>
        [Category("物料")]
        [DisplayName("尺寸")]
        public Size Szie { get; set; } = new Size();

        /// <summary>
        /// 厚度
        /// </summary>
        [Category("物料")]
        [DisplayName("厚度")]
        public double Height { get; set; } = 0;

        /// <summary>
        /// 保压时间
        /// </summary>
        [Category("运行参数")]
        [DisplayName("保压时间")]
        public int PasteDelay { get; set; } = 50;

        /// <summary>
        /// 吸标延时
        /// </summary>
        [Category("运行参数")]
        [DisplayName("吸标延时")]
        public int XIDelay { get; set; } = 50;

        /// <summary>
        /// 是否提前吸
        /// </summary>
        [Category("运行参数")]
        [DisplayName("提前吸")]
        public bool PreSuck { get; set; } = false;
    }

    public class LabelDefine:Common.SingletionProvider<LabelDefine>
    {
        public Dictionary<string, MylarLabel> LabelList = new Dictionary<string, MylarLabel>();

        public MylarLabel this[string name]
        {
            get
            {
                return this.LabelList[name];
            }
        }

        /// <summary>
        /// 安装Label
        /// </summary>
        public void InstallLable(string label)
        {
            if (label != string.Empty && LabelDefine.Exist(label))
            {
                var info = LabelDefine.Load(label);
                if (LabelList.ContainsKey(label))
                    LabelList[label] = info;
                else
                    LabelList.Add(label, info);
            }
        }

        public static void Save(MylarLabel label)
        {
            SerializableHelper<MylarLabel> helper = new SerializableHelper<MylarLabel>(label);
            helper.JsonSerialize(PathDefine.sPathLabel + $"{label.LabelName}.json");
        }

        public static bool Exist(string labelName)
        {
            return File.Exists(PathDefine.sPathLabel + $"{labelName}.json");
        }

        public static MylarLabel Load(string labelName)
        {
            SerializableHelper<MylarLabel> helper = new SerializableHelper<MylarLabel>();
            return helper.DeJsonSerialize(PathDefine.sPathLabel + $"{labelName}.json");
        }

        public static void LoadAll()
        {
            
        }
    }
}
