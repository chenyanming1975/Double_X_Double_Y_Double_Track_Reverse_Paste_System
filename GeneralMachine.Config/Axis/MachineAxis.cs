using GeneralMachine.Common;
using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    public class MachineAxis
    {
        /// <summary>
        /// X轴
        /// </summary>
        [Category("基本轴")]
        [DisplayName("X轴")]
        [TypeConverter(typeof(AxisUIEditor))]
        public Axis_RunParam X
        {
            get;
            set;
        } = new Axis_RunParam();

        /// <summary>
        /// Y轴
        /// </summary>
        [Category("基本轴")]
        [DisplayName("Y轴")]
        [TypeConverter(typeof(AxisUIEditor))]
        public Axis_RunParam Y
        {
            get;
            set;
        } = new Axis_RunParam();

        /// <summary>
        /// Z轴参数
        /// </summary>
        [Category("Z轴")]
        [DisplayName("Z轴")]
        //[TypeConverter(typeof(AxisUIEditor))]
        public List<Axis_RunParam> Z
        {
            get;
            set;
        } = new List<Axis_RunParam>();

        [Category("U轴")]
        [DisplayName("U轴")]
        //[TypeConverter(typeof(AxisUIEditor))]
        public List<Axis_RunParam> R
        {
            get;
            set;
        } = new List<Axis_RunParam>();

        [Category("基本轴")]
        [DisplayName("翻转轴")]
        [TypeConverter(typeof(AxisUIEditor))]
        public Axis_RunParam Trun
        {
            get;
            set;
        } = new Axis_RunParam();
    }

}
