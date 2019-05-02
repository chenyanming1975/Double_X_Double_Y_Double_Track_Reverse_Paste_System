using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    #region 轨道IO
    /// <summary>
    /// 与轨道相关IO
    /// </summary>
    public class TrackIO
    {
        /// <summary>
        /// 通过反射获得IO,然后设置每个的状态
        /// </summary>
        public void Init()
        {
            IODefine.Init(this);
        }

        #region 轨道感应器
        /// <summary>
        /// 进板感应器
        /// </summary>
        [Category("轨道感应器")]
        [DisplayName("进板")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput IO_TrackIn
        {
            get;
            set;
        } = new IOInput();

        /// <summary>
        /// 到位感应器
        /// </summary>
        [Category("轨道感应器")]
        [DisplayName("到位")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput IO_TrackReach
        {
            get;
            set;
        } = new IOInput();

        /// <summary>
        /// 出板感应器
        /// </summary>
        [Category("轨道感应器")]
        [DisplayName("出板")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput IO_TrackOut
        {
            get;
            set;
        } = new IOInput();
        #endregion

        #region 气缸
        /// <summary>
        /// 阻挡预原点感应器
        /// </summary>
        [Category("轨道气缸")]
        [DisplayName("阻挡原点感应器")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput IO_StopOrg
        {
            get;
            set;
        } = new IOInput();

        /// <summary>
        /// 夹板原点感应器
        /// </summary>
        [Category("轨道气缸")]
        [DisplayName("顶升原点感应器")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput IO_CarryOrg
        {
            get;
            set;
        } = new IOInput();

        /// <summary>
        /// 夹板动点感应器
        /// </summary>
        [Category("轨道气缸")]
        [DisplayName("顶升动点感应器")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput IO_CarryDone
        {
            get;
            set;
        } = new IOInput();

        /// <summary>
        /// 阻挡气缸
        /// </summary>
        [Category("轨道气缸")]
        [DisplayName("阻挡原点")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput IO_Stop
        {
            get;
            set;
        } = new IOOutput();

        /// <summary>
        /// 阻挡气缸
        /// </summary>
        [Category("轨道气缸")]
        [DisplayName("阻挡动点")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput IO_StopMove
        {
            get;
            set;
        } = new IOOutput();

        /// <summary>
        /// 夹板气缸
        /// </summary>
        [Category("夹板气缸")]
        [DisplayName("夹板原点")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput IO_Carry
        {
            get;
            set;
        } = new IOOutput();

        /// <summary>
        /// 夹板气缸
        /// </summary>
        [Category("夹板气缸")]
        [DisplayName("夹板动点")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput IO_CarryMove
        {
            get;
            set;
        } = new IOOutput();
        #endregion

        #region 轨道信号
        /// <summary>
        /// 前流水线准备信息
        /// </summary>
        [Category("轨道信号")]
        [DisplayName("前流水线准备信号")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput IO_PreLineReady
        {
            get;
            set;
        } = new IOInput();

        /// <summary>
        /// 后流水线请求
        /// </summary>
        [Category("轨道信号")]
        [DisplayName("后流水线要板信号")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput IO_AfterLineRequest
        {
            get;
            set;
        } = new IOInput();

        /// <summary>
        /// 向前要板
        /// </summary>
        [Category("轨道信号")]
        [DisplayName("向前要板")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput IO_RequestInput
        {
            get;
            set;
        } = new IOOutput();

        /// <summary>
        /// 告后有板
        /// </summary>
        [Category("轨道信号")]
        [DisplayName("告后有板")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput IO_InformExit
        {
            get;
            set;
        } = new IOOutput();

        #endregion

        #region 轨道运动
        /// <summary>
        /// 正向运动
        /// </summary>
        [Category("轨道运动")]
        [DisplayName("正转")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput IO_Positive
        {
            get;
            set;
        } = new IOOutput();

        /// <summary>
        /// 反向运动
        /// </summary>
        [Category("轨道运动")]
        [DisplayName("反转")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput IO_Reverse
        {
            get;
            set;
        } = new IOOutput();
        #endregion

        #region 其他
        /// <summary>
        /// 轨道吹气
        /// </summary>
        [Category("其他")]
        [DisplayName("轨道吹气")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput TrackAir
        {
            get;
            set;
        } = new IOOutput();
        #endregion
    }
    #endregion
}
