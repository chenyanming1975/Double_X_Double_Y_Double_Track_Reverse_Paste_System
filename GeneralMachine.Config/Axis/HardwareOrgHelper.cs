using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Motion;
using MathNet.Numerics;
using NationalInstruments.Vision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
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
    public class HardwareItem
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
        } = 90;

        /// <summary>
        /// X轴倾斜率
        /// </summary>
        public double XRate
        {
            get;
            set;
        } = 1;

        /// <summary>
        /// Y轴倾斜率
        /// </summary>
        public double YRate
        {
            get;
            set;
        } = 0;

        public Polynomial XPoly = new Polynomial();

        public Polynomial YPoly = new Polynomial();

        public double[] X = null;
        public double[] RX = null;

        public double[] Y = null;
        public double[] RY = null;

        public PointContour ToReal(PointContour xy)
        {
            try
            {
                PointContour temp = new PointContour(xy.X, xy.Y);

                if (XPoly.Coefficients.Length > 0
                    && (xy.X >= X[0] - 20)
                    && (xy.X <= (X.Last() + 20)))
                {
                    temp.X -= XPoly.Evaluate(xy.X);
                }

                if (YPoly.Coefficients.Length > 0
                    && (xy.Y >= Y[0] - 20)
                    && (xy.Y <= Y.Last()+20))
                {
                    temp.Y -= YPoly.Evaluate(xy.Y);
                }

                return temp;
            }
            catch { }
            return xy;
        }

        public PointContour ToMachine(PointContour xy)
        {
            try
            {
                PointContour temp = new PointContour(xy.X, xy.Y);

                if (XPoly.Coefficients.Length > 0
                    && (xy.X >= X[0] - 20)
                   && (xy.X <= (X.Last() + 20)))
                {
                    temp.X += XPoly.Evaluate(xy.X);
                }

                if (YPoly.Coefficients.Length > 0
                    && (xy.Y >= Y[0] - 20)
                    && (xy.Y <= Y.Last() + 20))
                {
                    temp.Y += YPoly.Evaluate(xy.Y);
                }
                return temp;
            }
            catch { }
            return xy;
        }
        #endregion
    }
    public class HardwareOrgHelper:Common.SingletionProvider<HardwareOrgHelper>
    {
        public Dictionary<Module, HardwareItem> HardWare = new Dictionary<Module, HardwareItem>();
        public HardwareItem this[Module module]
        {
            get
            {
                return this.HardWare[module];
            }
        }

        public void Init()
        {
            HardWare.Add(Module.Front, new HardwareItem());
            HardWare.Add(Module.After, new HardwareItem());
        }

        #region 保存和存储
        public static bool Load()
        {
            if (!File.Exists(PathDefine.sPathHardware + "机械校验.json"))
            {
                HardwareOrgHelper.Instance.Init();
                return true;
            }

            SerializableHelper<HardwareOrgHelper> helper = new SerializableHelper<HardwareOrgHelper>();
            var temp = helper.DeJsonSerialize(PathDefine.sPathHardware + "机械校验.json");
            if (temp != null)
                HardwareOrgHelper.Instance = temp;
            else
                return false;
            return true;
        }

        public static void Save()
        {
            SerializableHelper<HardwareOrgHelper> helper = new SerializableHelper<HardwareOrgHelper>(HardwareOrgHelper.Instance);
            helper.JsonSerialize(PathDefine.sPathHardware + "机械校验.json");
        }
        #endregion
    }
}
