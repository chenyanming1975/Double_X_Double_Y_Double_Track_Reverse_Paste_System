using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneralMachine.Common.CommonHelper;

namespace GeneralMachine.Motion
{
    /// <summary>
    /// 标准轴
    /// </summary>
    public enum GeneralAxis
    {
        [EnumDescription("X轴")]
        X = 0,
        [EnumDescription("Y轴")]
        Y = 1,
        [EnumDescription("Z轴")]
        Z = 2,
        [EnumDescription("U轴")]
        U = 3,
        [EnumDescription("翻转轴")]
        TRUN = 4,
    }

    /// <summary>
    /// 速度方案
    /// </summary>
    public enum Shceme
    {
        [EnumDescription("手动-慢速")]
        MaunalSlow = 0,
        [EnumDescription("手动-中速")]
        ManualNormal = 1,
        [EnumDescription("手动-快速")]
        ManualFast = 2,
        [EnumDescription("自动-慢速")]
        AutoSlow = 3,
        [EnumDescription("自动-中速")]
        AutoNormal = 4,
        [EnumDescription("自动-快速")]
        AutoFast = 5,
        [EnumDescription("回零速度")]
        Home = 6,
        [EnumDescription("到极限速度")]
        GoLimit = 7,
    }

    /// <summary>
    /// 其他速度方案
    /// </summary>
    public enum OtherShceme
    {
        [EnumDescription("上飞拍")]
        UpFly,
        [EnumDescription("下飞拍")]
        DownFly,
        [EnumDescription("轨道慢速")]
        ConverySlow,
        [EnumDescription("轨道快速")]
        ConveryNormal,
    }

    /// <summary>
    /// 分段加速项
    /// </summary>
    [Serializable]
    public class SegmentItem
    {
        public double Start
        {
            get;
            set;
        } = 0;

        public double End
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 分段加速比例 100%
        /// </summary>
        public double VelRate
        {
            get;
            set;
        } = 1;
    }

    /// <summary>
    /// 反馈脉冲
    /// </summary>
    public enum AxisSource
    {
        /// <summary>
        /// 命令脉冲
        /// </summary>
        [EnumDescription("命令脉冲")]
        COMMAND,

        /// <summary>
        /// 编码器脉冲
        /// </summary>
        [EnumDescription("编码器脉冲")]
        ACT,
    }

    /// <summary>
    /// 卡号
    /// </summary>
    public enum CardNo
    {
        [EnumDescription("模块1-A卡")]
        [EnumValueAttribue(0)]
        A = 0,
        [EnumValueAttribue(0)]
        [EnumDescription("模块1-B卡")]
        B = 1,
        [EnumValueAttribue(1)]
        [EnumDescription("模块2-C卡")]
        C = 2,
        [EnumValueAttribue(1)]
        [EnumDescription("模块2-D卡")]
        D = 3,
        [EnumValueAttribue(2)]
        [EnumDescription("E卡")]
        E = 4,
    }

    /// <summary>
    /// 输入点序号
    /// </summary>
    public enum InputNo
    {
        IN1 = 0,
        IN2 = 1,
        IN4 = 2,
        IN5 = 3,
    }

    /// <summary>
    /// 输出点序号
    /// </summary>
    public enum OutputNo
    {
        [EnumValueAttribue(4)]
        OUT4 = 0,
        [EnumValueAttribue(5)]
        OUT5 = 1,
        [EnumValueAttribue(6)]
        OUT6 = 2,
        [EnumValueAttribue(7)]
        OUT7 = 3,
    }

    /// <summary>
    /// 轴状态
    /// </summary>
    public enum AxisState
    {
        STA_AX_DISABLE = 0,
        STA_AX_READY = 1,
        STA_AX_STOPPING = 2,
        STA_AX_ERROR_STOP = 3,
        STA_AX_HOMING = 4,
        STA_AX_PTP_MOT = 5,
        STA_AX_CONTI_MOT = 6,
        STA_AX_SYNC_MOT = 7,
        STA_AX_EXT_JOG = 8,
        STA_AX_EXT_MPG = 9,
        STA_AX_PAUSE = 10,
        STA_AX_BUSY = 11,
    }

    /// <summary>
    /// 回零模式
    /// </summary>
    public enum HomeMode
    {
        MODE1_Abs = 0,
        MODE2_Lmt = 1,
        MODE3_Ref = 2,
        MODE4_Abs_Ref = 3,
        MODE5_Abs_NegRef = 4,
        MODE6_Lmt_Ref = 5,
        MODE7_AbsSearch = 6,
        MODE8_LmtSearch = 7,
        MODE9_AbsSearch_Ref = 8,
        MODE10_AbsSearch_NegRef = 9,
        MODE11_LmtSearch_Ref = 10,
        MODE12_AbsSearchReFind = 11,
        MODE13_LmtSearchReFind = 12,
        MODE14_AbsSearchReFind_Ref = 13,
        MODE15_AbsSearchReFind_NegRef = 14,
        MODE16_LmtSearchReFind_Ref = 15,
    }

    public enum CFG_AX_Property
    {
        CFG_Ax_ID = 501,
        CFG_AxCamDOEnable = CFG_Ax_ID + 121,
    }

    /// <summary>
    /// 马达状态-左右限位 原点 到位 急停
    /// </summary>
    public enum Ax_Motion_IO
    {
        AX_MOTION_IO_RDY = 1,
        AX_MOTION_IO_ALM = 2,
        AX_MOTION_IO_LMTP = 4,
        AX_MOTION_IO_LMTN = 8,
        AX_MOTION_IO_ORG = 16,
        AX_MOTION_IO_DIR = 32,
        AX_MOTION_IO_EMG = 64,
        AX_MOTION_IO_PCS = 128,
        AX_MOTION_IO_ERC = 256,
        AX_MOTION_IO_EZ = 512,
        AX_MOTION_IO_CLR = 1024,
        AX_MOTION_IO_LTC = 2048,
        AX_MOTION_IO_SD = 4096,
        AX_MOTION_IO_INP = 8192,
        AX_MOTION_IO_SVON = 16384,
        AX_MOTION_IO_ALRM = 32768,
        AX_MOTION_IO_SLMTP = 65536,
        AX_MOTION_IO_SLMTN = 131072,
        AX_MOTION_IO_CMP = 262144,
        AX_MOTION_IO_CAMDO = 524288,
    }
}
