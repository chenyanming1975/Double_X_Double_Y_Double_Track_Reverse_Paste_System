using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    #region 其他IO
    /// <summary>
    /// 其他与模组不关联的整机IO
    /// </summary>
    public class OtherIO
    {
        /// <summary>
        /// 通过反射获得IO,然后设置每个的状态
        /// </summary>
        public void Init()
        {
            IODefine.Init(this);
        }

        /// <summary>
        /// LED灯
        /// </summary>
        [Category("机器灯")]
        [DisplayName("LED灯")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput LED
        {
            get;
            set;
        } = new IOOutput();

        [Category("机器灯")]
        [DisplayName("运行灯")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput StartLight
        {
            get;
            set;
        } = new IOOutput();

        [Category("机器灯")]
        [DisplayName("暂停灯")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput PasueLight
        {
            get;
            set;
        } = new IOOutput();

        [Category("机器灯")]
        [DisplayName("复位灯")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput ResetLight
        {
            get;
            set;
        } = new IOOutput();

        [Category("机器按钮")]
        [DisplayName("开始按钮")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput StartBtn
        {
            get;
            set;
        } = new IOInput();

        [Category("机器按钮")]
        [DisplayName("停止按钮")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput PauseBtn
        {
            get;
            set;
        } = new IOInput();

        [Category("机器按钮")]
        [DisplayName("复位按钮")]
        [TypeConverter(typeof(IOInputUIEditor))]
        public IOInput ResetBtn
        {
            get;
            set;
        } = new IOInput();

        [Category("状态灯")]
        [DisplayName("红灯")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput Red
        {
            get;
            set;
        } = new IOOutput();

        [Category("状态灯")]
        [DisplayName("绿灯")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput Green
        {
            get;
            set;
        } = new IOOutput();

        [Category("状态灯")]
        [DisplayName("黄灯")]
        [TypeConverter(typeof(IOOutputUIEditor))]
        public IOOutput Yellow
        {
            get;
            set;
        } = new IOOutput();
    }
    #endregion

}
