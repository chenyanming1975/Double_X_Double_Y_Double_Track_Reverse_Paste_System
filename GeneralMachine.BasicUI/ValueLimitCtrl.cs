using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Definition;
using GeneralMachine.Motion;
namespace GeneralMachine.BasicUI
{
    public partial class ValueLimitCtrl : UserControl
    {
        public ValueLimitCtrl()
        {
            InitializeComponent();
            UpdateToUI();
        }

        /// <summary>
        /// 
        /// </summary>
        private LimitRange range = new LimitRange();

        /// <summary>
        /// 设置对应的轴
        /// </summary>
        public Axis_RunParam Axis
        {
            get;
            set;
        } = null;

        public string Title
        {
            get
            {
                return this.groupBox1.Text;
            }

            set
            {
                this.groupBox1.Text = value;
            }
        }

        public LimitRange Range
        {
            get
            {
                return this.range;
            }

            set
            {
                if(value != null)
                {
                    this.range = value;
                    UpdateToUI();
                }
            }
        }

        public void UpdateToUI()
        {
            this.tUp.Text = this.Range.UpperLimit.ToString("f3");
            this.tLow.Text = this.Range.LowerLimit.ToString("f3");
        }

        private void bSetLowLimt_Click(object sender, EventArgs e)
        {
            if(this.Axis != null)
            {
                this.range.LowerLimit = this.Axis.Pos;
                UpdateToUI();
            }
        }

        private void bSetUpLimit_Click(object sender, EventArgs e)
        {
            if (this.Axis != null)
            {
                this.range.UpperLimit = this.Axis.Pos;
                UpdateToUI();
            }
        }
    }
}
