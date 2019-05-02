using GeneralMachine.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    /// <summary>
    /// 轨道配置
    /// </summary>
    [Serializable]
    public class TrackConfig : ICloneable
    {
        /// <summary>
        /// 轨道模式
        /// </summary>
        [Category("模式")]
        [DisplayName("自动-轨道方向")]
        public FlowInOutMode FlowInOutMode
        {
            get;
            set;
        } = FlowInOutMode.左进右出;

        /// <summary>
        /// 顶升前延时
        /// </summary>
        [Category("延时")]
        [DisplayName("夹紧前延时")]
        [Description("等待 ms再开始夹紧")]
        public int CarryBefore
        {
            get;
            set;
        } = 200;

        /// <summary>
        /// 夹紧后延时
        /// </summary>
        [Category("延时")]
        [DisplayName("夹紧后延时")]
        [Description("夹紧后等待 ms发送进板到位")]
        public int CarryOver
        {
            get;
            set;
        } = 200;

        /// <summary>
        /// 离线式|在线式
        /// </summary>
        [Category("模式")]
        [DisplayName("在线")]
        [Description("离线：进出板信号没有串接 false  在线：进出版信号已串接 true")]
        public bool OnLine
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 是否侦测后要板
        /// </summary>
        /// 
        [Category("模式")]
        [DisplayName("侦测后要板")]
        [Description("true：侦测  false：不侦测")]
        public bool MointorAfterRequeset
        {
            get;
            set;
        } = false;
        
        /// <summary>
        /// 出板等待时间
        /// </summary>
        [Category("延时")]
        [DisplayName("出板等待")]
        [Description("ms")]
        public int OutputDelay
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 进板等待时间
        /// </summary>
        [Category("延时")]
        [DisplayName("进板等待")]
        [Description("ms")]
        public int InputDelay
        {
            get;
            set;
        } = 0;

        public object Clone()
        {
            return CommonHelper.Copy(this);
        }
    }
}
