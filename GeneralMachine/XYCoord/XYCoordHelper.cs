using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.XYCoord
{
    /// <summary>
    /// 机台坐标补正
    /// 
    /// 纪律机台在各个位置的偏移量
    /// 根据数学公式，选择性的进行 
    /// 1.线性拟合
    /// 2.抛物线拟合
    /// 3.多项式拟合
    /// 
    /// 
    /// 
    /// </summary>
    public class XYCoordHelper:Common.SingletionProvider<XYCoordHelper>
    {
        /// <summary>
        /// X 轴偏移项
        /// </summary>
        public List<AxisOffsetItem> Axis_X
        {
            get;
            set;
        }= new List<AxisOffsetItem>();

        /// <summary>
        /// Y轴偏移项
        /// </summary>
        public List<AxisOffsetItem> Axis_Y
        {
            get;
            set;
        }= new List<AxisOffsetItem>();

        /// <summary>
        /// XY坐标系夹角
        /// </summary>
        public double XYCroodAngle
        {
            get;
            set;
        } = 90;

        /// <summary>
        /// 根据机台坐标 得到 真实坐标
        /// </summary>
        /// <param name="machinePos"></param>
        /// <returns></returns>
        public PointF  GetAct(PointF machinePos)
        {
            return machinePos;
        }

        /// <summary>
        /// 根据真实坐标 反推 机台坐标
        /// </summary>
        /// <param name="actPos"></param>
        /// <returns></returns>
        public PointF GetMachine(PointF actPos)
        {
            
            return actPos;
        }
    }
}
