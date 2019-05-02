using GeneralMachine.Common;
using GeneralMachine.Vision;
using NationalInstruments.Vision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralMachine.Flow.Editer;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Nodes
{
    [Serializable]
    public class BadmarkParam : NodeParamPt
    {
        public BadmarkParam() : base() { }
        public BadmarkParam(string name, NodeParam parent = null) : base(name, parent)
        {
        }

        /// <summary>
        /// 侦测区域
        /// </summary>
        [Category("配置")]
        [Description("设置侦测区域")]
        [DisplayName("侦测区域")]
        [Browsable(true)]
        public override RectangleContour ROI
        {
            get;
            set;
        } = null;

        [Category("配置")]
        [DisplayName("坐标")]
        [TypeConverter(typeof(PointFConverter))]
        public override PointF Pos
        {
            get;
            set;
        } = new PointF();


        /// <summary>
        /// Badmark方法
        /// </summary>
        [Category("配置")]
        [DisplayName("视觉算法")]
        [TypeConverter(typeof(VisionPropertyCtrl))]
        [Browsable(true)]
        public override string VisionName
        {
            get;
            set;
        } = string.Empty;
    }
}
