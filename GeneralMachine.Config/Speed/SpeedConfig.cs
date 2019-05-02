using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Motion;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneralMachine.Common.CommonHelper;

namespace GeneralMachine.Config
{

    /// <summary>
    /// 存储机台的速度配置
    /// </summary>
    public class SpeedConfig
    {
        public SpeedConfig()
        {
            foreach (Shceme shceme in Enum.GetValues(typeof(Shceme)))
            {
                this.SchemeName.Add(shceme, shceme.ToString());
                var dic = new Dictionary<GeneralAxis, HostarSpeed>();
                foreach (GeneralAxis axis in Enum.GetValues(typeof(GeneralAxis)))
                {
                    dic.Add(axis, new HostarSpeed());
                }
                this.Scheme.Add(shceme, dic);
            }

            foreach (OtherShceme shceme in Enum.GetValues(typeof(OtherShceme)))
            {
                this.OtherSchemeName.Add(shceme, shceme.ToString());
                this.OtherScheme.Add(shceme, new HostarSpeed());
            }
        }

        /// <summary>
        /// 其他速度方案名称
        /// </summary>
        public Dictionary<OtherShceme, string> OtherSchemeName = new Dictionary<OtherShceme, string>();

        /// <summary>
        /// 标准轴速度方案名称
        /// </summary>
        public Dictionary<Shceme, string> SchemeName = new Dictionary<Shceme, string>();

        /// <summary>
        /// 其他速度方案
        /// </summary>
        public Dictionary<OtherShceme, HostarSpeed> OtherScheme = new Dictionary<OtherShceme, HostarSpeed>();

        /// <summary>
        /// 标准轴的速度配置
        /// </summary>
        public Dictionary<Shceme, Dictionary<GeneralAxis, HostarSpeed>> Scheme = new Dictionary<Shceme, Dictionary<GeneralAxis, HostarSpeed>>();

        /// <summary>
        /// 获得速度
        /// </summary>
        /// <param name="shc"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        [JsonIgnore]
        public HostarSpeed this[Shceme shc, GeneralAxis axis]
        {
            get
            {
                return this.Scheme[shc][axis].Clone() as HostarSpeed;
            }
        }

        /// <summary>
        /// 获得分段减速后的速度
        /// </summary>
        /// <param name="curPos"></param>
        /// <param name="endPos"></param>
        /// <param name="shc"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        [JsonIgnore]
        public HostarSpeed this[double curPos, double endPos, Shceme shc, GeneralAxis axis]
        {
            get
            {
                return this.GetSpeed(curPos, endPos, this.Scheme[shc][axis]);
            }
        }

        /// <summary>
        /// 获得速度
        /// </summary>
        /// <param name="shc"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        [JsonIgnore]
        public HostarSpeed this[OtherShceme shc]
        {
            get
            {
                return this.OtherScheme[shc];
            }
        }

        /// <summary>
        /// 根据分段比例调节速度
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        private HostarSpeed GetSpeed(double start, double end, HostarSpeed speed)
        {
            double dist = Math.Abs(end - start);
            double rate = 1;
            if (dist < 10)
            {
                rate = 0.7;
            }
            else if (dist < 20)
            {
                rate = 0.8;
            }
            else if (dist < 40)
            {
                rate = 0.9;
            }

            HostarSpeed newSpeed = new HostarSpeed();
            newSpeed.StartSpeed = speed.StartSpeed * rate;
            newSpeed.MaxSpeed = speed.MaxSpeed * rate;
            newSpeed.AccTime = speed.AccTime;
            newSpeed.DecTime = speed.DecTime;
            return newSpeed;
        }
    }
}
