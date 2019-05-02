using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Flow.Editer;
using System.ComponentModel;
using GeneralMachine.Common;
using GeneralMachine.Config;

namespace GeneralMachine.Flow.Nodes
{
    /// <summary>
    /// 贴附顺序
    /// </summary>
    public enum SuckSheme
    {
        左Feeder优先,
        右Feeder优先,
        左右Feeder均衡,
    }

    /// <summary>
    /// 补料策略
    /// </summary>
    public enum RepasteShceme
    {
        优先贴,
        优先补,
    }

    [Serializable]
    public class PCBParam:NodeParamPt
    {
        public PCBParam():base() {
            this.CanExpand = false;
            this.CanRecordXY = false;
            this.CanRecordROI = false;
        }

        public PCBParam(string name):base(name, null)
        {
            this.CanExpand = false;
            this.CanRecordXY = false;
            this.CanRecordROI = false;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Category("参数")]
        [DisplayName("启用读Badmark")]
        public bool EnableBadmark
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 是否启用
        /// </summary>
        [Category("参数")]
        [DisplayName("启用读小板条码")]
        public bool EnableReadPcsCode
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 是否启用
        /// </summary>
        [Category("参数")]
        [DisplayName("启用读大板条码")]
        public bool EnableReadPanelCode
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 轨道宽度
        /// </summary>
        [Category("轨道")]
        [Description("记录，方便换线调节宽度")]
        [DisplayName("轨道宽度")]
        public double TrackWidth
        {
            get;
            set;
        } = 0;

        [Category("程式基准点")]
        [Description("程式坐标都以改点扩展")]
        [DisplayName("FidMark")]
        [TypeConverter(typeof(PointFConverter))]
        public override PointF Pos
        {
            get;
            set;
        } = new PointF();

        [Category("策略")]
        [DisplayName("补料策略")]
        /// <summary>
        /// 补料策略
        /// </summary>
        public RepasteShceme RepasteShceme
        {
            get;
            set;
        } = RepasteShceme.优先贴;


        [Category("策略")]
        [DisplayName("吸标策略")]
        public SuckSheme SuckSheme
        {
            get;
            set;
        } = SuckSheme.左右Feeder均衡;

        public override ContextMenu Menu(ProgramEditCtrl ctrl)
        {
            ContextMenu menu = base.Menu(ctrl);
            menu.MenuItems.Add(new MenuItem("记录FidMark", ctrl.RecordFidMark));
            menu.MenuItems.Add(new MenuItem("添加小板", ctrl.AddPCS));
            menu.MenuItems.Add(new MenuItem("添加Panel码", ctrl.AddReadCode));
            return menu;
        }
    }
}
