using GeneralMachine.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Editer
{
    public partial class fmChangeXY : Form
    {
        public fmChangeXY(Module module, PointF pt)
        {
            InitializeComponent();
            UpdatePt = pt;
            this.UpdateXY();
            this.module = module;
         }

        private void UpdateXY()
        {
            this.tX.Text = UpdatePt.X.ToString("f3");
            this.tY.Text = UpdatePt.Y.ToString("f3");
        }
        public PointF UpdatePt = new PointF();
        private Module module = Module.Front;
        private void bUpdate_Click(object sender, EventArgs e)
        {
            this.UpdatePt.X += (float)this.numOffsetX.Value;
            this.UpdatePt.Y += (float)this.numOffsetY.Value;

            this.DialogResult = DialogResult.OK;
        }

        private void bSetCur_Click(object sender, EventArgs e)
        {
            UpdatePt = SystemEntiy.Instance[this.module].XYPos;
            this.UpdateXY();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
