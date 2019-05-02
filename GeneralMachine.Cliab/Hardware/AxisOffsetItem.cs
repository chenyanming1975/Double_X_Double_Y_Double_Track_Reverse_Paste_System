using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Config;
using GeneralMachine.Flow;
using GeneralMachine.Common;
using GeneralMachine.Motion;
using System.Threading;
using GeneralMachine.Vision;
using NationalInstruments.Vision;
using MathNet.Numerics;

namespace GeneralMachine.Cliab
{
    public partial class AxisOffsetItem : UserControl
    {
        public AxisOffsetItem()
        {
            InitializeComponent();
        }

        private Module module = Module.Front;
        public GeneralAxis Aixs = GeneralAxis.X;
        public Module Module
        {
            get { return this.module; }
            set
            {
                this.module = value;
                try
                {
                    if (this.Aixs == GeneralAxis.X)
                    {
                        x = HardwareOrgHelper.Instance[this.module].X;
                        rx = HardwareOrgHelper.Instance[this.module].RX;
                        poly = HardwareOrgHelper.Instance[this.module].XPoly;
                    }
                    else
                    {
                        x = HardwareOrgHelper.Instance[this.module].Y;
                        rx = HardwareOrgHelper.Instance[this.module].RY;
                        poly = HardwareOrgHelper.Instance[this.module].YPoly;
                    }
                }
                catch { }
            }
        }

        public double[] rx = null;
        public double[] x = null;

        public string Tilte
        {
            get
            {
                return this.gAxis.Text;
            }
            set
            {
                this.gAxis.Text = value;
            }
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if (Array == null)
            {
                MessageBox.Show("请先生成量测参数!!!");
                return;
            }

            if (this.cbVisionList.Text == string.Empty)
            {
                MessageBox.Show("请选择量测算法!!!");
                return;
            }

            Roi roi = GetRoi?.Invoke();
            if (roi.Count == 0 || roi[0].Shape.GetType() != typeof(RectangleContour))
            {
                MessageBox.Show("请正确绘制ROI!!!");
                return;
            }

            rx = new double[Array.Length];
            x = new double[Array.Length];

            double angle = 0;
            double space = (double)this.numSpace.Value;
            string func = this.cbVisionList.Text;

            if (Aixs == GeneralAxis.X)
            {
                angle = MathHelper.GetAngle(Array[0], Array.Last()
                    , Array[0], new PointF(Array[0].X + 10, Array[0].Y));
            }
            else
            {
                angle = MathHelper.GetAngle(Array[0], Array.Last()
                  , Array[0], new PointF(Array[0].X, Array[0].Y - 10));
            }

            angle = Math.Abs(angle);
            if(angle > 90)
            {
                angle = 180 - angle;
            }

            double rate = Math.Cos(angle / 180 * Math.PI);
            for (int i = 0; i < Array.Length; ++i)
            {
                SystemEntiy.Instance[this.module].XYGoPosTillStop(Array[i]);
                GoAndCheck(func, roi);
                GoAndCheck(func, roi);
                PointF xy = SystemEntiy.Instance[this.module].XYPos;

                double val = this.Aixs == GeneralAxis.X ? xy.X : xy.Y;

                // 再计算
                if (i == 0)
                {
                    x[0] = val;
                }
                else
                {
                    x[i] = x[0] + (val - x[0]) * rate;
                    rx[i] = x[i] - (x[0] + space * i * rate);
                    this.dAxis.Rows[i].Cells[1].Value = (rx[i]).ToString("f3");
                }
            }
        }

        private void GoAndCheck(string func, Roi roi)
        {
            CommonHelper.DoEvent(500);
            VisionDetect?.Invoke(this.module, func);
        }

        private Polynomial poly = null;
        private void bFit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Aixs == GeneralAxis.X)
                {
                    HardwareOrgHelper.Instance[this.module].X = x;
                    HardwareOrgHelper.Instance[this.module].RX = rx;
                    HardwareOrgHelper.Instance[this.module].XPoly = new Polynomial();
                }
                else
                {
                    HardwareOrgHelper.Instance[this.module].Y = x;
                    HardwareOrgHelper.Instance[this.module].RY = rx;
                    HardwareOrgHelper.Instance[this.module].YPoly = new Polynomial();
                }
                HardwareOrgHelper.Save();

                poly = Polynomial.Fit(x, rx, (int)this.numPow.Value);
                poly.VariableName = $"{this.Aixs}";
                this.argList.Items.Clear();
                for (int i = 0; i < poly.Coefficients.Length; ++i)
                {
                    this.argList.Items.Add(poly.Coefficients[i].ToString());
                }
                this.bEnable.Enabled = true;
            }
            catch { }
        }

        private void bEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Aixs == GeneralAxis.X)
                {
                    HardwareOrgHelper.Instance[this.module].X = x;
                    HardwareOrgHelper.Instance[this.module].RX = rx;
                    HardwareOrgHelper.Instance[this.module].XPoly = poly;
                }
                else
                {
                    HardwareOrgHelper.Instance[this.module].Y = x;
                    HardwareOrgHelper.Instance[this.module].RY = rx;
                    HardwareOrgHelper.Instance[this.module].YPoly = poly;
                }
                HardwareOrgHelper.Save();
                this.bEnable.Enabled = false;
            }
            catch { }
        }

        private PointF start = new PointF();
        private PointF end = new PointF();
        private PointF[] Array = null;
        private void bSetStart_Click(object sender, EventArgs e)
        {
            this.start = SystemEntiy.Instance[this.Module].XYPos;
        }

        private void bSetEnd_Click(object sender, EventArgs e)
        {
            this.end = SystemEntiy.Instance[this.Module].XYPos;
        }

        private void bConfig_Click(object sender, EventArgs e)
        {
            Array = MathHelper.Expand2AllPoints(this.start, this.start, this.end, this.start, (short)this.numPoint.Value, 1);
            this.dAxis.Rows.Clear();
            this.dAxis.Rows.Add(Array.Length);
            for (int i = 0; i < Array.Length; ++i)
            {
                this.dAxis.Rows[i].Cells[0].Value = $"{Array[i].X:n2},{Array[i].Y:n2}";
                this.dAxis.Rows[i].Cells[1].Value = 0;
            }
        }

        private void bGoSelect_Click(object sender, EventArgs e)
        {
            if (this.dAxis.SelectedRows.Count > 0 && Array != null)
            {
                SystemEntiy.Instance[this.module].XYGoPos(Array[this.dAxis.SelectedRows[0].Index]);
            }
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            this.cbVisionList.Items.Clear();
            var test = VisionToolCtrl.GetVisionList().ToArray();
            this.cbVisionList.Items.AddRange(test);
        }

        public static event Func<Roi> GetRoi;
        public static event Action<Module, string> VisionDetect;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double v = double.Parse(this.textBox1.Text);
                double s = poly.Evaluate(v);
                this.textBox2.Text = s.ToString("f3");
            }
            catch { }
        }

        private void AxisOffsetItem_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Aixs == GeneralAxis.X)
                {
                    x = HardwareOrgHelper.Instance[this.module].X;
                    rx = HardwareOrgHelper.Instance[this.module].RX;
                    poly = HardwareOrgHelper.Instance[this.module].XPoly;
                }
                else
                {
                    x = HardwareOrgHelper.Instance[this.module].Y;
                    rx = HardwareOrgHelper.Instance[this.module].RY;
                    poly = HardwareOrgHelper.Instance[this.module].YPoly;
                }
            }
            catch { }
        }
    }
}
