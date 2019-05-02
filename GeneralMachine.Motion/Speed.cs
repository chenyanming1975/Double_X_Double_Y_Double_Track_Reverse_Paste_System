using GeneralMachine.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Motion
{
    /// <summary>
    /// 鸿仕达速度定义
    /// Speed 单位： mm/s
    /// 加减速时间：ms
    /// </summary>
    [Serializable]
    public class HostarSpeed:ICloneable
    {
        /// <summary>
        /// 运动速度设置
        /// </summary>
        public HostarSpeed()
        {
        }

        public static implicit operator HostarSpeed(string iniValue)
        {
            return new HostarSpeed(iniValue);
        }

        public static implicit operator string(HostarSpeed speed)
        {
            return speed.ToString();
        }

        /// <summary>
        /// 根据脉冲比得到实际速度
        /// </summary>
        /// <param name="axisRatio"></param>
        /// <returns></returns>
        public HostarSpeed GetActSpeed(double axisRatio)
        {
            HostarSpeed speed = new HostarSpeed();
            speed.StartSpeed = this.StartSpeed * axisRatio;
            speed.MaxSpeed = this.MaxSpeed * axisRatio;
            speed.AccTime = (this.MaxSpeed * 1000 / this.AccTime) * axisRatio;
            speed.DecTime = (this.MaxSpeed * 1000 / this.DecTime) * axisRatio;
            return speed;
        }

        public HostarSpeed(string iniValue)
        {
            string[] values = iniValue.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (values != null && values.Length > 0)
            {
                StartSpeed = Convert.ToDouble(values[0]);

                if (values.Length >= 2)
                {
                    MaxSpeed = Convert.ToDouble(values[1]);
                }

                if (values.Length >= 3)
                {
                    AccTime = Convert.ToDouble(values[2]);
                }

                if (values.Length >= 4)
                {
                    DecTime = Convert.ToDouble(values[3]);
                }
            }
        }

        /// <summary>
        /// 初始速度  单位mm/s
        /// </summary>
        [Category("速度")]
        [DisplayName("初始速度")]
        [Description("初始速度-单位mm/s")]
        public double StartSpeed
        {
            get;
            set;
        } = 5;

        /// <summary>
        /// 最大速度  单位mm/s
        /// </summary>
        [CategoryAttribute("速度")]
        [DisplayName("最大速度")]
        [Description("最大速度-单位mm/s")]
        public double MaxSpeed
        {
            get;
            set;
        } = 50;

        /// <summary>
        /// 加速时间  ms
        /// </summary>
        [Category("速度")]
        [DisplayName("加速度")]
        [Description("加速度-单位mm/s")]
        public double AccTime
        {
            get;
            set;
        } = 100;

        /// <summary>
        /// 减速时间  ms
        /// </summary>
        [CategoryAttribute("速度")]
        [DisplayName("减速度")]
        [Description("减速度-单位mm/s")]
        public double DecTime
        {
            get;
            set;
        } = 100;

        public override string ToString()
        {
            return string.Format("{0:F3},{1:F3},{2:F3},{3:F3}", this.StartSpeed, this.MaxSpeed, this.AccTime, this.DecTime);
        }

        public object Clone()
        {
            HostarSpeed speed = new HostarSpeed();
            speed.StartSpeed = this.StartSpeed;
            speed.MaxSpeed = this.MaxSpeed;
            speed.AccTime = this.AccTime;
            speed.DecTime = this.DecTime;
            return speed ;
        }
    }
}
