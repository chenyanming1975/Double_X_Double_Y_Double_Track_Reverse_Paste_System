using GeneralMachine.Vision;
using GeneralMachine.Definition;
using GeneralMachine.Motion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static GeneralMachine.Common.CommonHelper;
using NationalInstruments.Vision;

namespace GeneralMachine.Config
{
    /// <summary>
    /// 吸嘴相关配置
    /// </summary>
    [Serializable]
    public class NozzleConfig : ICloneable
    {
        #region 系统配置-可随时变更 ---------Warning Level - 3
        /// <summary>
        /// 安全高度
        /// </summary>
        public double SafeHeight
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 贴附高度
        /// </summary>
        public double PasteHeight
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 吸标高度
        /// </summary>
        public double XIHeight
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 抛料高度
        /// </summary>
        public double DropHeight
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// R轴初始角度
        /// </summary>
        public double RInit
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 正飞 滞后参数
        /// </summary>
        public double PosFlyOffset
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 反飞 滞后参数
        /// </summary>
        public double NegFlyOffset
        {
            get;
            set;
        } = 0;
        #endregion

        #region 校验相关-不可随时变更(变更回影响贴附精度)--------Warning Level - 1
        /// <summary>
        /// 在下视觉看来，Z轴的旋转中心--------Warning Level - 1
        /// </summary>
        public PointContour RotatePoint
        {
            get;
            set;
        } = new PointContour(0, 0);

        /// <summary>
        /// 贴附高度下的旋转中心图像位置
        /// </summary>
        public PointContour PasteRotatePt
        {
            get;
            set;
        } = new PointContour();

        /// <summary>
        /// 上视觉中心到吸嘴旋转中心距离--------Warning Level - 1
        /// </summary>
        public PointF NzToCam
        {
            get;
            set;
        } = new PointF(0, 0);

        /// <summary>
        /// 视觉检测区域
        /// </summary>
        public RectangleContour ViewRoi
        {
            get;
            set;
        } = new RectangleContour();

        /// <summary>
        /// 吸嘴旋转中心拍照位置
        /// </summary>
        public PointF RotateCamPoint
        {
            get;
            set;
        } = new PointF(0, 0);

        public PointF DownMarkPt
        {
            get;
            set;
        } = new PointF();

        public PointF UpMarkPt
        {
            get;
            set;
        } = new PointF();

        public PointF PastePt
        {
            get;
            set;
        } = new PointF();

        /// <summary>
        /// 吸嘴到取料相机中心距离
        /// </summary>
        public PointF NzToLabelDist { get; set; } = new PointF();

        /// <summary>
        /// 取料相机位置
        /// </summary>
        public PointF LabelCenterPos { get; set; } = new PointF();

        /// <summary>
        /// 吸嘴位置
        /// </summary>
        public PointF NzPos { get; set; } = new PointF();
        #endregion


        public object Clone()
        {
            return Common.CommonHelper.Copy(this as object);
        }
    }
}
