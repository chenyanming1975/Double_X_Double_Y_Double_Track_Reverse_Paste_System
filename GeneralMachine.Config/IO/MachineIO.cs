using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    #region 机器IO
    /// <summary>
    /// 与模组相关的IO
    /// </summary>
    public class MachineIO
    {
        /// <summary>
        /// 通过反射获得IO,然后设置每个的状态
        /// </summary>
        public void Init()
        {
            IODefine.Init(this);
        }

        #region Z轴IO
        /// <summary>
        /// 真空感应器
        /// </summary>
        [Category("吸嘴配置")]
        [DisplayName("真空感应")]
        public List<IOInput> VaccumCheck
        {
            get;
            set;
        } = new List<IOInput>();
        /// <summary>
        /// 真空吸
        /// </summary>
        [Category("吸嘴配置")]
        [DisplayName("吸真空")]
        public List<IOOutput> VaccumSuck
        {
            get;
            set;
        } = new List<IOOutput>();

        /// <summary>
        /// Z轴极限
        /// </summary>
        [Category("吸嘴配置")]
        [DisplayName("极限")]
        public List<IOInput> Limit
        {
            get;
            set;
        } = new List<IOInput>();

        /// <summary>
        /// 真空破
        /// </summary>
        [Category("吸嘴配置")]
        [DisplayName("破真空")]
        public List<IOOutput> VaccumPO
        {
            get;
            set;
        } = new List<IOOutput>();
        #endregion

        #region Feeder IO
        /// <summary>
        /// Feeder 存在
        /// </summary>
        [Category("Feeder配置")]
        [DisplayName("Feeder安装到位")]
        public List<IOInput> FeederExit
        {
            get;
            set;
        } = new List<IOInput>();

        /// <summary>
        /// 标签到位
        /// </summary>
        [Category("Feeder配置")]
        [DisplayName("标签到位")]
        public List<IOInput> LabelReach
        {
            get;
            set;
        } = new List<IOInput>();
        #endregion

        #region 光源
        [Category("光源")]
        [DisplayName("上光源-红")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput UpLight
        {
            get;
            set;
        } = new IOOutput();

        [Category("光源")]
        [DisplayName("下光源-红")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput DownLightRed
        {
            get;
            set;
        } = new IOOutput();

        [Category("光源")]
        [DisplayName("下光源-绿")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput DownLightGreen
        {
            get;
            set;
        } = new IOOutput();

        [Category("光源")]
        [DisplayName("下光源-蓝")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput DownLightBlue
        {
            get;
            set;
        } = new IOOutput();

        /// <summary>
        /// 相机外触发
        /// </summary>
        [Category("外触发")]
        [DisplayName("上/下相机触发")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput CCDTrriger
        {
            get;
            set;
        } = new IOOutput();

        #endregion

        #region 其他
        /// <summary>
        /// 安全门
        /// </summary>
        [Category("安全门")]
        [DisplayName("安全光栅")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput SafeDoor
        {
            get;
            set;
        } = new IOInput();

        /// <summary>
        /// 翻转轴刹车
        /// </summary>
        [Category("刹车")]
        [DisplayName("翻转轴刹车")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput TrunServoOn
        {
            get;
            set;
        } = new IOOutput();

        /// <summary>
        /// 启动灯
        /// </summary>
        [Category("操作按钮")]
        [DisplayName("启动灯")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput StartBtnLight
        {
            get;
            set;
        } = new IOOutput();


        /// <summary>
        /// 停止灯
        /// </summary>
        [Category("操作按钮")]
        [DisplayName("停止灯")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput StopBtnLight
        {
            get;
            set;
        } = new IOOutput();

        /// <summary>
        /// 复位灯
        /// </summary>
        [Category("操作按钮")]
        [DisplayName("复位灯")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput ResetBtnLight
        {
            get;
            set;
        } = new IOOutput();

        /// <summary>
        /// 启动按钮
        /// </summary>
        [Category("操作按钮")]
        [DisplayName("启动按钮")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput StartBtn
        {
            get;
            set;
        } = new IOInput();

        /// <summary>
        /// 启动按钮
        /// </summary>
        [Category("操作按钮")]
        [DisplayName("停止按钮")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput StopBtn
        {
            get;
            set;
        } = new IOInput();

        /// <summary>
        /// 启动按钮
        /// </summary>
        [Category("操作按钮")]
        [DisplayName("复位按钮")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput ResetBtn
        {
            get;
            set;
        } = new IOInput();
        #endregion
    }
    #endregion

}
