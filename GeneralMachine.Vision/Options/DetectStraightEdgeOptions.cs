using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Vision.Analysis;
namespace GeneralMachine.Vision
{
    public class DetectStraightEdgeOptions: IOptions
    {
        #region 搜索角度
        [Category("搜索角度")]
        [Description("默认false")]
        [DisplayName("角度范围")]
        public double AngleRange { get; set; } = 90;

        [Category("搜索角度")]
        [Description("设置期望找到直线边缘的角度")]
        [DisplayName("期望角度")]
        public double Orientation { get; set; } = 0;

        [Category("搜索角度")]
        [Description("设置直线边缘的预期角度精度")]
        [DisplayName("角度精度")]
        public double AngleTolerance { get; set; } = 1;
        #endregion

        #region 搜索算法
        [Category("算法")]
        [Description("设置基于hough的方法中使用的迭代次数")]
        [DisplayName("Hough算法迭代次数")]
        public uint HoughIterations { get; set; } = 5;

        [Category("算法")]
        [Description("设置作为搜索数量百分比的最小点数需要包含在检测到的直线边缘中的直线")]
        [DisplayName("最小覆盖比例")]
        public double MinimumCoverage { get; set; } = 25;

        [Category("算法")]
        [Description("设置所使用的边缘点的最小信噪比以适应直线边缘")]
        [DisplayName("最小噪点")]
        public double MinimumSignalToNoiseRatio { get; set; } = 0;
        #endregion

        #region 边缘搜索参数
        [Category("搜索边缘")]
        [Description("边缘最小分数")]
        [DisplayName("最小分数")]
        public double MinScore { get; set; } = 500;

        [Category("搜索边缘")]
        [Description("边缘最大分数")]
        [DisplayName("最大分数")]
        public double MaxScore { get; set; } = 1000;

        [Category("搜索边缘")]
        [Description("FirstRakeEdges 第一个点 BestHoughLine 最坚固点 BestRakeEdges 最佳点 BestProjectionEdge 最强点")]
        [DisplayName("搜索模式")]
        public StraightEdgeSearchMode SearchMode { get; set; } = StraightEdgeSearchMode.FirstRakeEdges;

        [Category("搜索边缘")]
        [Description("FirstRakeEdges 第一个点 BestHoughLine 最坚固点 BestRakeEdges 最佳点 BestProjectionEdge 最强点")]
        [DisplayName("搜索线间隔")]
        public uint StepSize { get; set; } = 3;
        #endregion

        public StraightEdgeOptions Options
        {
            get
            {
                var op = new StraightEdgeOptions(this.SearchMode, 1);
                op.HoughIterations = this.HoughIterations;
                op.AngleRange = this.AngleRange;
                op.AngleTolerance = this.AngleTolerance;
                op.ScoreRange = new NationalInstruments.Vision.Range(this.MinScore, this.MaxScore);
                op.Orientation = this.Orientation;
                op.MinimumCoverage = this.MinimumCoverage;
                op.MinimumSignalToNoiseRatio = this.MinimumSignalToNoiseRatio;
                op.StepSize = this.StepSize;
                return op;
            }
        }
    }
}
