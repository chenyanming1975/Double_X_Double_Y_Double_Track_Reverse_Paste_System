using System;
using System.Drawing;
using GeneralMachine.Common;
using static GeneralMachine.Common.CommonHelper;
// 所有结构体 枚举 定义

namespace GeneralMachine.Definition
{
    /// <summary>
    /// 极限范围
    /// </summary>
    [Serializable]
    public class LimitRange:ICloneable
    {
        /// <summary>
        /// 下极限
        /// </summary>
        public double LowerLimit
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 上极限
        /// </summary>
        public double UpperLimit
        {
            get;
            set;
        } = 0;

        public bool InSafe(double pos)
        {
            if (UpperLimit <= LowerLimit)
                return true;
            if (Math.Abs(UpperLimit - LowerLimit) < 10)
                return true;

            return pos >= LowerLimit && pos <= UpperLimit;
        }
        public object Clone()
        {
            return CommonHelper.Copy(this);
        }
    }

    /// <summary>
    /// XYZ点位
    /// </summary>
    public class ThrowPoint
    {
        public double Z { get; set; } = 0;
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public int Delay_Throw { get; set; } = 50;//抛料延时
    }

    /// <summary>
    /// 光源厂家
    /// </summary>
    public enum VendorName : short
    {
        /// <summary>
        /// 汇林
        /// </summary>
        [EnumDescription("汇林")]
        HuiLin = 0,
        /// <summary>
        ///  OPT
        /// </summary>
        [EnumDescription("OPT")]
        OPT = 1,
    }

    /// <summary>
    /// 机器运行状态
    /// </summary>
    public enum WorkStatus
    {
        /// <summary>
        /// 未复位
        /// </summary>
        UnReset = 0x00,

        /// <summary>
        ///  前模组复位
        /// </summary>
        FrontReseted = 0x01,


        /// <summary>
        ///  后模组复位
        /// </summary>
        AfterReseted = 0x10,

        /// <summary>
        /// 机器停止处于就绪状态
        /// </summary>
        Ready = 0x11,

        /// <summary>
        /// 暂停
        /// </summary>
        Pause = 0x100,

        /// <summary>
        /// 警报中
        /// </summary>
        Alarm = 0x1000,
    }

    /// <summary>
    /// 登录密码状态
    /// </summary>
    public enum PassWordStatus
    {
        /// <summary>
        /// 未登录
        /// </summary>
        UnLog = 0,

        /// <summary>
        ///  密码错误
        /// </summary>
        Visitor = 1,

        /// <summary>
        /// 管理员
        /// </summary>
        Manager = 2,

        /// <summary>
        /// 工程师
        /// </summary>
        Engineer = 3,

        /// <summary>
        /// 工程师
        /// </summary>
        Technician = 4,

        /// <summary>
        /// 操作员
        /// </summary>
        Operator = 5,
    }

    /// <summary>
    /// 相机侦测结果返回结构体
    /// </summary>
    public struct CamReturn
    {
        /// <summary>
        /// OK 结果
        /// </summary>
        public bool IsOK;
        /// <summary>
        /// OK 结果累计 计算0-未计算 1-计算OK 2-计算NG
        /// </summary>
        public int OKStatus;
        /// <summary>
        /// 视觉计算结果-点（世界坐标）
        /// </summary>
        public PointF Worldpoint;
        /// <summary>
        /// 视觉计算结果-角度
        /// </summary>
        public double Angle;
    }

}
