using GeneralMachine.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    public enum RunMode
    {
        /// <summary>
        /// 空跑模式
        /// </summary>
        TestRun,

        /// <summary>
        /// 吸标测试
        /// </summary>
        SuckTest,

        /// <summary>
        /// 正常模式
        /// </summary>
        Normal,
    }
    [Serializable]
    public class GeneralConfig:ICloneable
    {
        #region 通用配置
        /// <summary>
        /// 上视觉拍照延时
        /// </summary>
        [Category("延时(ms)")]
        [DisplayName("上视觉拍照")]
        public int UpCamDelay
        {
            get;
            set;
        } = 100;

        /// <summary>
        /// 下视觉拍照延时
        /// </summary>
        [Category("延时(ms)")]
        [DisplayName("下视觉拍照")]
        public int DownCamDelay
        {
            get;
            set;
        } = 100;

        /// <summary>
        /// 动作超时 15s
        /// </summary>
        [Category("超时(ms)")]
        [DisplayName("执行超时时间")]
        public int FlowStepTime
        {
            get;
            set;
        } = 15000;

        /// <summary>
        /// Feeder出标超时
        /// </summary>
        [Category("超时(ms)")]
        [DisplayName("出标超时时间")]
        public int FeederOutTime
        {
            get;
            set;
        } = 5000;

        /// <summary>
        /// 顶升延时
        /// </summary>
        [Category("延时(ms)")]
        [DisplayName("气缸夹紧前延时")]
        public int ReachBeforeDelay
        {
            get;
            set;
        } = 200;

        /// <summary>
        /// 顶升后延时
        /// </summary>
        [Category("延时(ms)")]
        [DisplayName("气缸夹紧后延时")]
        public int ReachAfterDelay
        {
            get;
            set;
        } = 200;

        /// <summary>
        /// 连续吸标报警
        /// </summary>
        [Category("报警")]
        [DisplayName("连续吸标报警")]
        public int ContinuousSuckAlarm
        {
            get;
            set;
        } = 3;

        /// <summary>
        /// 连续抛料报警
        /// </summary>
        [Category("报警")]
        [DisplayName("连续抛料报警")]
        public int ContinuousDropAlarm
        {
            get;
            set;
        } = 10;

        /// <summary>
        /// 启用空跑流程
        /// </summary>
        [Category("通用")]
        [DisplayName("启用空跑测试")]
        public bool EnableDryRun
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 启用吸标测试
        /// </summary>
        [Category("通用")]
        [DisplayName("启用吸标测试")]
        public bool EnableSuckTest
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 安全间隙
        /// </summary>
        [Category("通用")]
        [DisplayName("安全间距")]
        public float SafeSpace
        {
            get;
            set;
        } = 20;

        [Category("图像保存")]
        [DisplayName("保存模式")]
        public ImageSaveMode ImageSaveMode
        {
            get;
            set;
        } = ImageSaveMode.不保存;

        [Category("图像保存")]
        [Description("选择图片存储路径")]
        [DisplayName("存储路径")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string ImagePath
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// Y轴最大安全距离
        /// </summary>
        [Category("安全设置")]
        [DisplayName("Y轴最大安全距离")]
        public float YMaxSafe
        {
            get;
            set;
        } = 500.0f;

        /// <summary>
        /// 正常模式
        /// </summary>
        public RunMode RunMode = RunMode.Normal;


        /// <summary>
        /// 吸嘴回拍测试
        /// </summary>
        [Category("通用")]
        [DisplayName("启用吸嘴回拍检测")]
        public bool EnableNozzleCheck
        {
            get;
            set;
        } = true;

        public object Clone()
        {
            return Common.CommonHelper.Copy(this);
        }
        #endregion
    }
}
