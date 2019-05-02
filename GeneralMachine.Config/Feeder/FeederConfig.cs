using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{

    public class FeederConfig : ICloneable
    {
        public FeederConfig()
        {
        }

        /// <summary>
        /// Feeder 所属模组
        /// </summary>
        [ReadOnly(true)]
        [Category("硬件属性")]
        [DisplayName("Feeder所属模组")]
        public Module Module
        {
            get;
            set;
        } = Module.Front;

        /// <summary>
        /// Feeder配置名称
        /// </summary>
        [ReadOnly(true)]
        [Category("硬件属性")]
        [DisplayName("Feeder名称")]
        public string FeederName
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// Feeder类型
        /// </summary>
        [Category("硬件属性")]
        [Description("左Feeder 还是 右Feeder")]
        [DisplayName("Feeder位置")]
        public Feeder Feeder
        {
            get;
            set;
        } = Feeder.Left;

        /// <summary>
        /// 物料名称
        /// </summary>
        [Category("下视觉")]
        [Description("从下视觉列表中选取")]
        [DisplayName("物料下视觉方法")]
        [TypeConverter(typeof(Vision.VisionPropertyCtrl))]
        public string LabelName
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// Feeder 剩余物料
        /// </summary>
        [Category("物料管控")]
        [DisplayName("物料总数")]
        public int LabelTotalNum
        {
            get;
            set;
        } = 9999;

        /// <summary>
        /// Feeder 上总使用物料
        /// </summary>
        [Category("物料管控")]
        [DisplayName("剩余数量")]
        public int LabelUseNum
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// Feeder 上剩余多少物料开始报警
        /// </summary>
        [Category("物料管控")]
        [DisplayName("报警阈值")]
        public int LabelWarnNum
        {
            get;
            set;
        } = 50;

        /// <summary>
        /// 当前吸标次数
        /// </summary>
        [Browsable(false)]
        public int SuckIndex
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 吸标延时
        /// </summary>
        [Category("延时")]
        [DisplayName("吸料延时")]
        public int SuckDelay
        {
            get;
            set;
        } = 50;

        /// <summary>
        /// 保压延时
        /// </summary>
        [Category("延时")]
        [DisplayName("保压延时")]
        public int PutDelay
        {
            get;
            set;
        } = 50;

        /// <summary>
        /// 
        /// </summary>
        [Category("延时")]
        [DisplayName("Feeder出料延时")]
        public int NewFeederDelay
        {
            get;
            set;
        } = 500;

        /// <summary>
        /// 料
        /// </summary>
        [Browsable(false)]
        public List<FeederLabelInfo> Labels = new List<FeederLabelInfo>();

        /// <summary>
        /// 新出一排料
        /// </summary>
        [Browsable(false)]
        [JsonIgnore]
        public bool NewOut = false;

        public bool GetSuckPos(ref PointF suck, bool isTest = false)
        {
            if (SuckIndex >= this.Labels.Count)
            {
                SuckIndex = 0;
                NewOut = true;
            }

            if (IODefine.Instance.MachineIO[this.Module].LabelReach[this.Labels[this.SuckIndex].LabelSensorIndex].GetIO()
                || isTest)
            {
                suck = this.Labels[this.SuckIndex].GetPos();
                SuckIndex++;
                return true;
            }

            SuckIndex++;
            return false;
        }


        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
