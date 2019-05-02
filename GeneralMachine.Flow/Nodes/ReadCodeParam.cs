using GeneralMachine.Common;
using GeneralMachine.Flow.Editer;
using GeneralMachine.Vision;
using NationalInstruments.Vision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Nodes
{
    [Serializable]
    public class ReadCodeParam: NodeParamPt
    {
        public ReadCodeParam() : base() { }
        public ReadCodeParam(string name, NodeParam parent) :base(name, parent)
        {
        }

        /// <summary>
        /// 侦测区域
        /// </summary>
        [Category("配置")]
        [Description("设置读码区域")]
        [DisplayName("读码区域")]
        [Browsable(true)]
        public override RectangleContour ROI
        {
            get;
            set;
        } = null;

        /// <summary>
        /// 读条码方法
        /// </summary>
        [Category("配置")]
        [Description("从视觉库中选取方法")]
        [DisplayName("读码方案")]
        [TypeConverter(typeof(VisionPropertyCtrl))]
        [Browsable(true)]
        public override string VisionName
        {
            get;
            set;
        } = string.Empty;

        [Category("配置")]
        [DisplayName("坐标")]
        [TypeConverter(typeof(PointFConverter))]
        public override PointF Pos
        {
            get
            {
                return base.Pos;
            }

            set
            {
                base.Pos = value;
            }
        }
    }
}
