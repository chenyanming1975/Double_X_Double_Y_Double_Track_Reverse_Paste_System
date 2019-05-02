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

    /// <summary>
    /// IO点基本信息
    /// </summary>
    [Serializable]
    public class IOProt
    {
        [Browsable(false)]
        public string Name
        {
            get;
            set;
        } = string.Empty;

        [DisplayName("卡号")]
        public CardNo CardNo
        {
            get;
            set;
        } = CardNo.A;

        /// <summary>
        /// 轴号
        /// </summary>
        [DisplayName("轴号")]
        [Description("0-3号轴")]
        public int AxisNo
        {
            get;
            set;
        } = 0;

        [Browsable(false)]
        public string Text
        {
            get;
            set;
        } = string.Empty;
    }

}
