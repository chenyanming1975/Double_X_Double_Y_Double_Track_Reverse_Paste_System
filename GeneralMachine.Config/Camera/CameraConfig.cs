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
using System.IO.Ports;
using NationalInstruments.Vision;
using System.Collections.ObjectModel;
using NationalInstruments.Vision.Analysis;

namespace GeneralMachine.Config
{
    /// <summary>
    /// 相机相关配置
    /// </summary>
    [Serializable]
    public class CameraConfig : ICloneable
    {
        #region 基本设置
        /// <summary>
        /// 设备名称
        /// </summary>
        [Category("基本设置")]
        [Description("在NI中找到对应相机记录名称")]
        [DisplayName("相机名称")]
        public string Name { get; set; } = "Camera";

        /// <summary>
        /// 相机 AOI
        /// </summary>
        [Category("基本设置")]
        [Description("在NI中找到对应相机尺寸输入")]
        [DisplayName("相机视野")]
        public Rectangle AOI
        {
            get;
            set;
        } = new Rectangle(0,0,2592, 1944);

        /// <summary>
        /// 默认增益
        /// </summary>
        [Category("基本设置")]
        [DisplayName("默认增益")]
        [Description("值越大图片越亮，但噪点也越多")]
        public int DefaultGain
        {
            get;
            set;
        } = 6;

        /// <summary>
        /// 默认曝光
        /// </summary>
        [Category("基本设置")]
        [Description("值越大图片越亮，但帧数变慢，过大影响飞拍")]
        [DisplayName("默认曝光")]
        public int DefaultExp
        {
            get;
            set;
        } = 200;

        /// <summary>
        /// 对应光源COM口
        /// </summary>
        [Category("光源设置")]
        [DisplayName("串口号")]
        public string ProtName
        {
            get;
            set;
        } = "COM1";

        /// <summary>
        /// </summary>
        [Category("光源设置")]
        [DisplayName("波特率")]
        public int BaudRate
        {
            get;
            set;
        } = 9600;

        /// <summary>
        /// 红色通道
        /// </summary>
        [Category("光源设置")]
        [Description("不启用选择NoChannel")]
        [DisplayName("红光通道")]
        public LightChannel RedChannel
        {
            get;
            set;
        } = LightChannel.Channel1;

        /// <summary>
        /// 绿色通道
        /// </summary>
        [Category("光源设置")]
        [Description("不启用选择NoChannel")]
        [DisplayName("绿光通道")]
        public LightChannel GreenChannel
        {
            get;
            set;
        } = LightChannel.Channel2;

        /// <summary>
        /// 蓝色通道
        /// </summary>
        [Category("光源设置")]
        [Description("不启用选择NoChannel")]
        [DisplayName("蓝光通道")]
        public LightChannel BlueChannel
        {
            get;
            set;
        } = LightChannel.Channel3;
        #endregion

        public object Clone()
        {
            return Common.CommonHelper.Copy(this as object);
        }


        #region 相机检验相关
        /// <summary>
        /// 相机9点标定结果
        /// </summary>
        [Browsable(false)]
        public List<HalCali> Mat2D = new List<HalCali>();
        #endregion
    }
}
