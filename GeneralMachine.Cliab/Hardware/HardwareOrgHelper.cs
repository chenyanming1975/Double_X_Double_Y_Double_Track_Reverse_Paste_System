using GeneralMachine.Config;
using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Cliab
{
    /// <summary>
    /// 硬件机械原点
    /// 可以确定X,Y轴的 夹角
    /// 
    ///                  LeftTop                    RightTop     
    ///                    __________________________
    ///                   |                          |
    ///                   |                          |
    ///                   |                          |
    ///                   |                          |
    ///                   |                          |
    ///                   |                          |
    ///                   |                          |   
    ///                   |__________________________|
    ///                 Org(LeftBottom)              RightBottom
    ///                   
    /// 
    /// 
    /// 
    /// 
    /// </summary>
    public class HardwareOrgHelper:Common.SingletionProvider<HardwareOrgHelper>
    {
        #region 机械原点 和 XY水平度
        /// <summary>
        /// 左上角机械原点
        /// </summary>
        public PointF LeftTop
        {
            get;
            set;
        } = new PointF();

        /// <summary>
        /// 右上角机械原点
        /// </summary>
        public PointF RightTop
        {
            get;
            set;
        } = new PointF();

        /// <summary>
        /// 右下角机械原点
        /// </summary>
        public PointF RightBottom
        {
            get;
            set;
        } = new PointF();

        /// <summary>
        /// 左下角机械原点
        /// </summary>
        public PointF LeftBottom
        {
            get;
            set;
        } = new PointF();

        /// <summary>
        /// 机械偏移
        /// </summary>
        public PointF HardwareOffset
        {
            get;
            set;
        } = new PointF();

        /// <summary>
        /// XY夹角度数
        /// </summary>
        public double XYCroodAngle
        {
            get;
            set;
        }
        #endregion

        #region 机械误差
        public Dictionary<Module, Dictionary<GeneralAxis, List<AxisOffsetItem>>> AxisOffset = new Dictionary<Module, Dictionary<GeneralAxis, List<AxisOffsetItem>>>();
        #endregion

        #region 保存和存储
        public static void Load()
        {

        }

        public static void Save()
        {

        }
        #endregion
    }
}
