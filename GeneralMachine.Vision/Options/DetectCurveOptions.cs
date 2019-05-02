using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Vision.Analysis;

namespace GeneralMachine.Vision
{
    /// <summary>
    /// 搜索轮廓参数
    /// </summary>
    public class DetectCurveOptions: IOptions
    {
        [Category("搜索轮廓矩形内切割线")]
        [Description("将此值设置为小于您想要的对象的最小宽度")]
        [DisplayName("切割线宽度")]
        public int ColumnStepSize { get; set; } = 15;


        [Category("搜索轮廓矩形内切割线")]
        [Description("将此值设置为小于您想要的对象的最小长度")]
        [DisplayName("切割线长度")]
        public int RowStepSize { get; set; } = 15;

        [Category("搜索轮廓")]
        [Description("NormalImage ")]
        [DisplayName("搜索轮廓模式")]
        public ExtractionMode ExtractionMode { get; set; } = ExtractionMode.NormalImage;

        [Category("搜索轮廓")]
        [Description("Fine 细边或窄边 Normal 普通边缘 ContourTracing 最佳(耗时多)")]
        [DisplayName("搜索轮廓筛选器")]
        public EdgeFilterSize FilterSize { get; set; } = EdgeFilterSize.Fine;

        [Category("搜索轮廓")]
        [Description("获取或设置曲线的端点之间的最大间距(以像素为单位)")]
        [DisplayName("最大端点间隙")]
        public int MaximumEndPointGap { get; set; } = 10;

        [Category("搜索轮廓")]
        [Description("获取或设置方法将提取的最小曲线的长度(以像素为单位)。此属性忽略长度小于最小像素的曲线。")]
        [DisplayName("最小曲线长度")]
        public int MinimumLength { get; set; } = 25;

        [Category("搜索轮廓")]
        [Description("设置该方法是否仅识别图像中的闭合曲线")]
        [DisplayName("识别闭合曲线")]
        public bool OnlyClosed { get; set; } = false;

        [Category("搜索轮廓")]
        [Description("是否启用亚像素精度")]
        [DisplayName("启用亚像素精度")]
        public bool SubpixelAccuracy { get; set; } = false;

        [Category("搜索轮廓")]
        [Description("0 黑 255白")]
        [DisplayName("边缘对比度")]
        public int Threshold { get; set; } = 75;

        /// <summary>
        /// NI 参数
        /// </summary>
        public CurveOptions Options
        {
            get
            {
                return new CurveOptions(this.OnlyClosed, this.ExtractionMode, this.FilterSize, this.SubpixelAccuracy, this.Threshold, this.RowStepSize, this.ColumnStepSize, this.MaximumEndPointGap, this.MinimumLength);
            }
        }
    }
}
