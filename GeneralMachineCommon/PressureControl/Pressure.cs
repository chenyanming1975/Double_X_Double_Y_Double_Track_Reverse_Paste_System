using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.PressureControl
{
    public class Pressure
    {
        /// <summary>
        /// false-不启用压力反馈 true-启用压力反馈
        /// </summary>
        public static bool bPressureEN = false;//
        public static string sPressureDes = "";//压力传感器描述
        public static short iPressureIndexLeft = 0;//左Feeder压力反馈轴卡通道序列号
        public static short iPressureIndexRight = 0;//右Feeder压力反馈轴卡通道序列号
        public static double dPressureCaliWorldLeft1 = 0;//左Feeder压力线性校验实际数值1
        public static double dPressureCaliWorldLeft2 = 0;//左Feeder压力线性校验实际数值2
        public static double dPressureCaliValueLeft1 = 0;//左Feeder压力线性校验反馈数值1
        public static double dPressureCaliValueLeft2 = 0;//左Feeder压力线性校验反馈数值2
        public static double dPressureCaliWorldRight1 = 0;//右Feeder压力线性校验实际数值1
        public static double dPressureCaliWorldRight2 = 0;//右Feeder压力线性校验实际数值2
        public static double dPressureCaliValueRight1 = 0;//右Feeder压力线性校验反馈数值1
        public static double dPressureCaliValueRight2 = 0;//右Feeder压力线性校验反馈数值2


        /// <summary>
        /// UserId
        /// </summary>
        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }






    }
}
