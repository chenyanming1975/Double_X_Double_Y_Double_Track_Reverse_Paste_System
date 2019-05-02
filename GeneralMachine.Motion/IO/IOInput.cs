using GeneralMachine.Common;
using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneralMachine.Common.CommonHelper;

namespace GeneralMachine.Motion
{

    [Serializable]
    public class IOInput : IOProt,ICloneable
    {
        /// <summary>
        /// 输入点IO 序号
        /// </summary>
        [DisplayName("点位")]
        public InputNo InputNo
        {
            get;
            set;
        } = InputNo.IN1;

        public bool GetIO()
        {
            return MotionHelper.Instance[this.CardNo, this.AxisNo, this.InputNo];
        }

        public override string ToString()
        {
            return $"(卡号={this.CardNo},轴号={this.AxisNo},点位={this.InputNo})";
        }

        public object Clone()
        {
            return CommonHelper.Copy(this);
        }
    }
}
