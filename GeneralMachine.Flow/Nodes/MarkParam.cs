using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Flow.Editer;
using System.ComponentModel;
using GeneralMachine.Vision;
using NationalInstruments.Vision;

namespace GeneralMachine.Flow.Nodes
{
    public enum Mark
    {
        Mark点1,
        Mark点2,
    }

    [Serializable]
    public class MarkParam : NodeParamPt
    {
        public MarkParam() : base() {
            this.CanExpand = false;
            this.CanRecordXY = false;
        }

        public MarkParam(string name, NodeParam parent = null) : base(name, parent)
        {
            this.CanExpand = false;
            this.CanRecordXY = false;
        }

        [Category("Mark点")]
        [DisplayName("坐标")]
        public override PointF Pos
        {
            get;
            set;
        } = new PointF();

        /// <summary>
        /// Mark点序号
        /// </summary>
        [Category("Mark点")]
        [Description("小板内坐标以Mark点基准")]
        [DisplayName("Mark点序号")]
        public Mark MarkID
        {
            get;
            set;
        } = Mark.Mark点1;

        /// <summary>
        /// 侦测区域
        /// </summary>
        [Category("视觉")]
        [Description("侦测Mark点区域")]
        [DisplayName("侦测框")]
        [Browsable(true)]
        public override RectangleContour ROI
        {
            get;
            set;
        } = null;

        /// <summary>
        /// 上视觉拍照算法名称
        /// </summary>
        [Category("视觉")]
        [DisplayName("侦测Mark点视觉方法")]
        [TypeConverter(typeof(VisionPropertyCtrl))]
        [Browsable(true)]
        public override string VisionName
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// 上视觉是否侦测过
        /// </summary>
        [Category("Mark点")]
        [DisplayName("是否侦测过")]
        [ReadOnly(true)]
        public bool IsDetected
        {
            get;
            set;
        } = false;

        public override ContextMenu Menu(ProgramEditCtrl ctrl)
        {
            var meun = base.Menu(ctrl);
            meun.MenuItems.Add(new MenuItem("侦测", ctrl.FindMark));
            meun.MenuItems.Add(new MenuItem("侦测并修改XY", ctrl.FindMarkAndChangeXY));
            return meun;
        }
    }
}
