using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Vision.Analysis;

namespace GeneralMachine.Vision
{
    public class DetectEdgeOptions:IOptions
    {
        [Category("直线参数")]
        [Description("设置用于查找直线边缘的处理模式 平均处理  中位处理")]
        [DisplayName("列处理方式")]
        public ColumnProcessingMode ColumnProcessingMode { get; set; } = ColumnProcessingMode.Average;

        [Category("直线参数")]
        [Description("默认 ZeroOrder")]
        [DisplayName("插值法")]
        public InterpolationMethod InterpolationType { get; set; } = InterpolationMethod.ZeroOrder;

        [Category("直线参数")]
        [Description("设置边缘检测内核的大小")]
        [DisplayName("内核尺寸")]
        public uint KernelSize { get; set; } = 7;

        [Category("直线参数")]
        [Description("默认75")]
        [DisplayName("最小边缘强度")]
        public double MinimumThreshold { get; set; } = 75;

        [Category("直线参数")]
        [Description("默认ALL,rising  上边  Falling 下边")]
        [DisplayName("边缘极性搜索方式")]
        public EdgePolaritySearchMode Polarity { get; set; } = EdgePolaritySearchMode.All;

        [Category("直线参数")]
        [Description("获取或设置垂直于搜索方向的平均像素数,计算沿搜索区域各点的边缘轮廓强度")]
        [DisplayName("宽度")]
        public uint Width { get; set; } = 3;

        public EdgeOptions Options
        {
            get
            {
                var op = new EdgeOptions(this.Polarity, this.MinimumThreshold, this.InterpolationType);
                op.KernelSize = this.KernelSize;
                op.ColumnProcessingMode = this.ColumnProcessingMode;
                op.Width = this.Width;
                return op;
            }
        }
    }
}
