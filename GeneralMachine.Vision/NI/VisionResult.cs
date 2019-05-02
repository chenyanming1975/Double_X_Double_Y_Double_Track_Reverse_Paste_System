using NationalInstruments.Vision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Vision
{
    /// <summary>
    /// 计算状态
    /// </summary>
    public enum VisionResultState
    {
        /// <summary>
        /// 等待计算
        /// </summary>
        WaitCal,
        /// <summary>
        /// 计算成功
        /// </summary>
        OK,
        /// <summary>
        /// 计算失败
        /// </summary>
        NG,
    }

    public class VisionResult
    {
        public VisionResult()
        {
            //Point = null;
            //Angle = 0;
            //Area = 0;
            //State =  VisionResultState.WaitCal;
            //Line = null;
            //BarCode = string.Empty;
        }

        /// <summary>
        /// 输出点
        /// </summary>
        public PointContour Point = null;

        /// <summary>
        /// 输出角度
        /// </summary>
        public double Angle = 0;

        /// <summary>
        /// 面积
        /// </summary>
        public double Area = 0;

        /// <summary>
        /// 是否成功
        /// </summary>
        public VisionResultState State = VisionResultState.WaitCal;

        /// <summary>
        /// 输出线段
        /// </summary>
        public LineContour Line = null;

        /// <summary>
        /// 输出线段
        /// </summary>
        public string BarCode = string.Empty;

        /// <summary>
        /// 计算完成之后的解释
        /// </summary>
        public List<string> VisionDesr = new List<string>();
    }
}
