using GeneralMachine.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Flow.Step
{
    public class MoveParam
    {
        public MoveParam()
        {
            ZPos = new double[4];
            RPos = new double[4];
            NzUsed = new bool[4];
        }

        /// <summary>
        /// XY位置
        /// </summary>
        public PointF XYPos = new PointF();

        /// <summary>
        /// Z轴位置
        /// </summary>
        public double[] ZPos = new double[4] { 0,0,0,0};

        /// <summary>
        /// 角度位置
        /// </summary>
        public double[] RPos = new double[4] { 0,0,0,0};

        /// <summary>
        /// 使用那个Z轴
        /// </summary>
        public bool[] NzUsed = new bool[4]{ false, false,false,false};

        /// <summary>
        /// 是否需要移动Z
        /// </summary>
        public bool MoveZ = false;

        /// <summary>
        /// 是否需要移动R
        /// </summary>
        public bool MoveR = false;

        /// <summary>
        /// 反转轴角度
        /// </summary>
        public double TrunAngle = 0;


        #region 贴附信息
        public int PastePCBIndex = 0;

        public int PastePCSIndex = 0;

        public Feeder Feeder = Feeder.Left;

        public string LabelName = string.Empty;

        public Nozzle Nozzle = Nozzle.Nz1;
        #endregion
    }

    public class PasteAndSuckParam:MoveParam
    {

    }
}
