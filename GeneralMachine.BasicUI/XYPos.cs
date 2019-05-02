using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Common;
using GeneralMachine.Flow;
using GeneralMachine.Config;
using GeneralMachine.Motion;

namespace GeneralMachine.BasicUI
{
    public partial class XYPos : UserControl
    {
        public XYPos()
        {
            InitializeComponent();
        }
        private Module module = 0;
        public void Set(Module module)
        {
            this.module = module;
        }

        public string TitleName
        {
            get
            {
                return this.title.Text;
            }
            set
            {
                this.title.Text = value;
            }
        }

        private PointF point = new PointF();

        public PointF Point
        {
            get
            {
                return this.point;
            }
            set
            {
                this.point = value;
                this.tX.Text = Point.X.ToString("f3");
                this.tY.Text = Point.Y.ToString("f3");
            }
         
        }

        private void bSet_Click(object sender, EventArgs e)
        {
            Point = SystemEntiy.Instance[module].XYPos;
        }

        private void bGo_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[module].XYGoPos(CommonHelper.GetPoint(this.tX.Text, tY.Text), Shceme.ManualNormal);
        }
    }
}
