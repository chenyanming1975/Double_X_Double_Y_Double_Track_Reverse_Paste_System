using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Vision
{
    public partial class fmVisionOutput : Form
    {
        public fmVisionOutput()
        {
            InitializeComponent();
        }

        public fmVisionOutput(VisionFlow flow)
        {
            this.flow = flow;
            InitializeComponent();
            this.cb_X.Items.AddRange(flow.GetItem(ResultType.XY).ToArray());
            this.cb_Y.Items.AddRange(flow.GetItem(ResultType.XY).ToArray());
            this.cb_Angle.Items.AddRange(flow.GetItem(ResultType.Angle).ToArray());
            this.cb_Area.Items.AddRange(flow.GetItem(ResultType.Area).ToArray());
            this.cb_OrgCrood.Items.AddRange(flow.GetItem(ResultType.Init).ToArray());
            this.cb_Code.Items.AddRange(flow.GetItem(ResultType.Code).ToArray());
            this.cb_X.Text = flow.ResultX;
            this.cb_Y.Text = flow.ResultY;
            this.cb_Angle.Text = flow.Angle;
            this.cb_Area.Text = flow.Area;
            this.cb_OrgCrood.Text = flow.OrgCrood;
            this.cb_Code.Text = flow.Code;
        }

        private VisionFlow flow = null;

        private void bSave_Click(object sender, EventArgs e)
        {
            flow.ResultX = this.cb_X.Text;
            flow.ResultY = this.cb_Y.Text;
            flow.Angle = this.cb_Angle.Text;
            flow.Area = this.cb_Area.Text;
            flow.OrgCrood = this.cb_OrgCrood.Text;
            flow.Code = this.cb_Code.Text;

            if (flow.OrgPoint.X.Equals(new PointF(0,0)))
            {
                MessageBox.Show("请先进行初始化识别!!!");
                this.DialogResult = DialogResult.No;
                return;
            }

            this.DialogResult = DialogResult.Yes;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
