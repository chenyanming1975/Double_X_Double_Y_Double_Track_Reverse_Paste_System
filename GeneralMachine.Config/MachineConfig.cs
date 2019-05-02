using GeneralMachine.Vision;
using GeneralMachine.Definition;
using GeneralMachine.Motion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static GeneralMachine.Common.CommonHelper;

namespace GeneralMachine.Config
{

    /// <summary>
    /// 模组参数
    /// </summary>
    [Serializable]
    public class MachineConfig : ICloneable
    {
        public MachineConfig()
        {
            foreach(Nozzle nozzle in Enum.GetValues(typeof(Nozzle)))
            {
                NozzleMap.Add(nozzle, new NozzleConfig());
            }
        }

        /// <summary>
        /// 吸嘴相关配置
        /// </summary>
        public Dictionary<Nozzle, NozzleConfig> NozzleMap = new Dictionary<Nozzle, NozzleConfig>();

        /// <summary>
        /// 清料位置
        /// </summary>
        public PointF ReadyPoint
        {
            get;
            set;
        } = new PointF();

        /// <summary>
        /// 抛料位
        /// </summary>
        public PointF DropPoint
        {
            get;
            set;
        } = new PointF();

        /// <summary>
        /// X轴极限
        /// </summary>
        public LimitRange XLimit
        {
            get;
            set;
        } = new LimitRange();

        /// <summary>
        /// Y极限
        /// </summary>
        public LimitRange YLimit
        {
            get;
            set;
        } = new LimitRange();

        public LimitRange YWorkRange
        {
            get;
            set;
        } = new LimitRange { LowerLimit = 100, UpperLimit = 300 };

        public LimitRange XWorkRange
        {
            get;
            set;
        } = new LimitRange { LowerLimit = 0, UpperLimit = 300 };

        /// <summary>
        /// 翻转轴贴标时角度
        /// </summary>
        public float TrunPasteAngle
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 翻转轴吸标角度
        /// </summary>
        public float TrunSuckAngle
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 抛料延时
        /// </summary>
        public int DropDelay
        {
            get;
            set;
        } = 200;

        /// <summary>
        /// 自动运行下的速度模式
        /// </summary>
        public Shceme AutoSpeedMode
        {
            get;
            set;
        } = Shceme.AutoSlow;

        /// <summary>
        /// 复制构造
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return Common.CommonHelper.Copy(this as object);
        }

        /// <summary>
        /// 获得 吸嘴配置
        /// </summary>
        /// <param name="nozzle"></param>
        /// <returns></returns>
        [JsonIgnore]
        public NozzleConfig this[Nozzle nozzle]
        {
            get
            {
                return NozzleMap[nozzle];
            }
        }
    }
}
