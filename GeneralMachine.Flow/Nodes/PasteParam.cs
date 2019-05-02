using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Flow.Editer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Flow.Nodes
{
    [Serializable]
    public class PasteParam: NodeParamPt
    {
        public PasteParam() : base() {
            this.CanRecordROI = false;
        }

        public PasteParam(string name, NodeParam parent = null) : base(name, parent)
        {
            this.CanRecordROI = false;
        }

        [Category("贴附位")]
        [DisplayName("序号")]
        public int MapID
        {
            get;
            set;
        } = 0;

        [Category("贴附位")]
        [DisplayName("坐标")]
        [TypeConverter(typeof(PointFConverter))]
        public override PointF Pos
        {
            get;
            set;
        } = new PointF();

        /// <summary>
        /// 标签名称
        /// </summary>
        [Category("参数")]
        [DisplayName("使用Feeder")]
        public Feeder Feeder
        {
            get;
            set;
        } = Feeder.Left;

        [Category("贴附位")]
        [DisplayName("贴附角度")]
        public double PasteAngle
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 贴附层数
        /// </summary>
        [Category("参数")]
        [DisplayName("贴附层数")]
        public int PasteCell
        {
            get;
            set;
        } = 1;

        /// <summary>
        /// 是否可以贴附
        /// </summary>
        [Category("参数")]
        [DisplayName("是否贴附")]
        public bool CanPaste
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 偏移X
        /// </summary>
        [Category("贴附位")]
        [DisplayName("贴附偏移X")]
        public double OffsetX
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 偏移Y
        /// </summary>
        [Category("贴附位")]
        [DisplayName("贴附偏移Y")]
        public double OffsetY
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// Badmark ID
        /// </summary>
        [Category("参数")]
        [DisplayName("Badmark流程ID")]
        [TypeConverter(typeof(BadmarkListCtrl))]
        public string BadmarkID
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// Code ID
        /// </summary>
        [Category("参数")]
        [DisplayName("读条码流程ID")]
        [TypeConverter(typeof(ReadCodeListCtrl))]
        public string BarCodeID
        {
            get;
            set;
        } = string.Empty;
    }
}
