using GeneralMachine.Common;
using GeneralMachine.Motion;
using Newtonsoft.Json;
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
    /// <summary>
    /// 输出点
    /// </summary>
    [Serializable]
    public class IOOutput : IOProt
    {
        [DisplayName("点位")]
        public OutputNo OuputNo
        {
            get;
            set;
        } = OutputNo.OUT4;

        /// <summary>
        /// 默认开-关
        /// </summary>
        [DisplayName("默认开关")]
        public bool DefaultState
        {
            get;
            set;
        } = false;

        [JsonIgnore]
        [Browsable(false)]
        private bool isFrist = true;

        public void SetIO(bool value)
        {
            MotionHelper.Instance[this.CardNo, this.AxisNo, this.OuputNo] = value;
        }

        public bool GetIO()
        {
            return MotionHelper.Instance[this.CardNo, this.AxisNo, this.OuputNo];
        }

        public override string ToString()
        {
            return $"(卡号={this.CardNo},轴号={this.AxisNo},点位={this.OuputNo},默认开关={this.DefaultState})";
        }
    }
}
