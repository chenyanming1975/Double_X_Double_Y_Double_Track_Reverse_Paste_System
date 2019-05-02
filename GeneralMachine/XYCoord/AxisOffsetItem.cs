using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.XYCoord
{
    /// <summary>
    /// 轴偏移项
    /// </summary>
    public class AxisOffsetItem
    {
        public double MachineAxisPos
        {
            get;
            set;
        } = 0;

        public double ActAxisPos
        {
            get;
            set;
        } = 0;

        public double Offset
        {
            get;
            set;
        }
    }
}
