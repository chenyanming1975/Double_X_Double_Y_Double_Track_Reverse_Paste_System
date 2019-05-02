using NationalInstruments.Vision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Vision
{
    /// <summary>
    /// 视觉方法返回值结果
    /// </summary>
    public enum ResultType
    {
        NULL = 0x0000,
        XY = 0x0001,
        Angle = 0x0010,
        Area = 0x0100,
        Init = 0x1000,
        Code = 0x10000,
    }

    public class Detect
    {
        public static string Defatlu = "H0000";
        public static int unitId = 0;

        public Detect()
        {
            unitId++;
            this.UnitID = $"H{unitId.ToString("#0000")}";
        }

        /// <summary>
        /// 初始化变量
        /// </summary>
        public virtual void InitOption()
        {
        }

        /// <summary>
        /// 返回值类型
        /// </summary>
        [Browsable(false)]
        public ResultType Type
        {
            get;
            set;
        } = ResultType.NULL;

        /// <summary>
        /// 流程名称
        /// </summary>
        [Category("算子")]
        [DisplayName("算子名称")]
        [ReadOnly(true)]
        public string Name
        {
            get;
            set;
        } = "视觉";

        /// <summary>
        /// 单元名(唯一)
        /// </summary>
        [Category("算子")]
        [DisplayName("步骤ID")]
        [ReadOnly(true)]
        public string UnitID { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Category("算子")]
        [DisplayName("算子描述")]
        [ReadOnly(true)]
        public string Remark { get; set; }


        /// <summary>
        /// 参数列表
        /// </summary>
        [Browsable(false)]
        public List<IOptions> OptionList = new List<IOptions>();

        /// <summary>
        /// ROI
        /// </summary>
        [Browsable(false)]
        public Shape ROI = null;

        public double AdjuestAngle(double angle)
        {
            if(angle > 180)
            {
                angle -= 360;
            }
            else if(angle < -180)
            {
                angle += 360;
            }

            return angle;
        }

        public virtual bool Lerarn(VisionImage image, Roi roi)
        {
            return false;
        }

        /// <summary>
        /// 视觉计算
        /// </summary>
        /// <param name="image">图像计算</param>
        /// <param name="parent">流程</param>
        /// <param name="newRoi">新的ROI</param>
        /// <returns></returns>
        public virtual VisionResult Detected(VisionImage image, Dictionary<string, VisionResult> Result = null, VisionFlow parent = null,Shape newRoi = null)
        {
            return new VisionResult();
        }

        public void AddVisionResc(VisionResult rtn, string desr)
        {
            rtn.VisionDesr.Add($"[{this.UnitID}]---{desr}");
        }
    }
}
