using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneralMachine.Common.CommonHelper;

namespace GeneralMachine.Config
{
    /// <summary>
    /// 图片保存模式
    /// </summary>
    public enum ImageSaveMode
    {
        保存所有,
        保存OK,
        保存NG,
        不保存,
    }

    /// <summary>
    /// 模组
    /// </summary>
    public enum Module
    {
        [EnumDescription("前模组")]
        Front = 0,
        [EnumDescription("后模组")]
        After = 1,
    }

    public enum Track
    {
        [EnumDescription("前轨道")]
        ForntTrack = 0,
        [EnumDescription("后轨道")]
        AfterTrack = 1,
    }

    /// <summary>
    /// 相机定义
    /// </summary>
    public enum Camera
    {
        [EnumDescription("上相机")]
        Top = 0,
        [EnumDescription("下相机1-2")]
        Bottom1,
        [EnumDescription("下相机3-4")]
        Bottom2,
        [EnumDescription("取料相机")]
        Label,
    }

    /// <summary>
    /// 吸嘴定义
    /// </summary>
    public enum Nozzle
    {
        [EnumDescription("吸嘴1")]
        Nz1 = 0,
        [EnumDescription("吸嘴2")]
        Nz2 = 1,
        [EnumDescription("吸嘴3")]
        Nz3 = 2,
        [EnumDescription("吸嘴4")]
        Nz4 = 3,
    }

    /// <summary>
    /// Feeder定义
    /// </summary>
    public enum Feeder
    {
        [EnumDescription("左Feeder")]
        Left = 0,
        [EnumDescription("右Feeder")]
        Right = 1,
    }

    /// <summary>
    /// BadMark模式
    /// </summary>
    public enum BadMarkMode
    {
        /// <summary>
        /// 从系统取得
        /// </summary>
        [EnumDescription("MES获取")]
        FromSystem = 0,

        /// <summary>
        ///  自扫描
        /// </summary>
        [EnumDescription("相机获取")]
        FromScan = 1,

        /// <summary>
        ///  取消全部BadMark
        /// </summary>
        [EnumDescription("屏蔽Badmark")]
        CancelAllBadMark = 2,
    }

    /// <summary>
    /// 轨道进出板模式
    /// </summary>
    public enum FlowInOutMode
    {
        /// <summary>
        /// 左进右出
        /// </summary>
        [EnumDescription("左进右出")]
        左进右出 = 0,

        /// <summary>
        ///  右进左出
        /// </summary>
        [EnumDescription("右进左出")]
        右进左出 = 1,

        /// <summary>
        ///  左进左出
        /// </summary>
        [EnumDescription("左进左出")]
        左进左出 = 2,

        /// <summary>
        ///  右进右出
        /// </summary>
        [EnumDescription("右进右出")]
        右进右出 = 3,
    }

    /// <summary>
    /// 光源通道设置
    /// </summary>
    public enum LightChannel
    {
        NoChannel = 0x00,
        Channel1 = 0x01,
        Channel2 = 0x02,
        Channel3 = 0x03,
        Channel4 = 0x04,
    }

    public static class PathDefine
    {
        public static void CreatePath()
        {
            Common.CommonHelper.CreatePath(sPathConfigure);
            Common.CommonHelper.CreatePath(sPathCard);
            Common.CommonHelper.CreatePath(sPathCamera);
            Common.CommonHelper.CreatePath(sPathAxis);
            Common.CommonHelper.CreatePath(sPathIO);

            Common.CommonHelper.CreatePath(sPathHostar);
            Common.CommonHelper.CreatePath(sPathProgram);
            Common.CommonHelper.CreatePath(sPathVision);
            Common.CommonHelper.CreatePath(sPathFeeder);

            Common.CommonHelper.CreatePath(sPath_PicSave);

            Common.CommonHelper.CreatePath(sLogPath);
            Common.CommonHelper.CreatePath(sUILogPath);
            Common.CommonHelper.CreatePath(sGeneralLogPath);
            Common.CommonHelper.CreatePath(sAlarmLogPath);
            Common.CommonHelper.CreatePath(sPathHardware);

        }

        #region 软件运行的配置文件
        public static readonly string sPathApplication = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string sPathConfigure= sPathApplication+"\\Configrue\\";
        public static readonly string sPathCard = sPathApplication + "\\Configrue\\Card\\";
        public static readonly string sPathCamera = sPathApplication + "\\Configrue\\Camera\\";
        public static readonly string sPathAxis = sPathApplication + "\\Configrue\\Axis\\";
        public static readonly string sPathIO = sPathApplication + "\\Configrue\\IO\\";
        public static readonly string sPressConfigure = sPathApplication + "\\Configrue\\Press\\";
        public static readonly string sPathHardware = sPathApplication + "\\Configrue\\Hardware\\";
        public static readonly string sPathReport = sPathApplication + "\\Configrue\\Report\\";

        #endregion

        #region 程式存储路径
        public static readonly string sPathHostar = "D:\\程式";

        public static readonly string sPathProgram = "D:\\程式\\贴附信息\\";

        public static readonly string sPathVision = "D:\\程式\\视觉方法\\";

        public static readonly string sPathFeeder = "D:\\程式\\Feeder\\";

        public static readonly string sPathLabel = "D:\\程式\\物料\\";

        /// <summary>
        /// 贴附临时区域，用于存储当前贴附的信息，便于机器异常停止时重贴
        /// </summary>
        public static readonly string sPathPasteTemp = "D:\\程式\\PasteTemp\\";
        #endregion

        public static readonly string sPath_PicSave = "D:\\图片\\";//保存图片 路径

        #region 日志存储路径
        public static readonly string sLogPath = "D:\\日志\\";//log 路径
        public static readonly string sUILogPath = sLogPath + "操作\\";//log 路径
        public static readonly string sGeneralLogPath = sLogPath+"流程\\";//log 路径
        public static readonly string sAlarmLogPath = sLogPath+"报警\\";//log 路径
        #endregion
    }

    /// <summary>
    /// 定值
    /// </summary>
    public static class ConstDefine
    {
        public static readonly int iHomeTime = 15000;

        public static readonly int iWaitInputTime = 60000;

        public static readonly int iInputTime = 10000;

        public static readonly int iOutputTime = 10000;

        public static readonly int iActionTimeout = 15000;
    }
}
