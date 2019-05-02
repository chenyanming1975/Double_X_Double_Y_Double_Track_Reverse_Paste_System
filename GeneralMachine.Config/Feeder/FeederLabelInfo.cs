using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    public class FeederLabelInfo
    {
        /// <summary>
        /// 是否启用吸取
        /// </summary>
        public bool EnableSuck
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 各位置对应的光纤位
        /// </summary>
        public int LabelSensorIndex
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 吸标位置X
        /// </summary>
        public float SuckX
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 吸标位置Y
        /// </summary>
        public float SuckY
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 物料在Feeder上的高度
        /// </summary>
        public double ZHeight
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 吸取角度
        /// </summary>
        public double SuckAngle
        {
            get;
            set;
        } = 0;

        public PointF GetPos()
        {
            return new PointF(this.SuckX, this.SuckY);
        }
    }
}
