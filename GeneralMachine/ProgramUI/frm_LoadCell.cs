using GeneralMachine.Vision;
using GeneralMachine.Config;
using GeneralMachine.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace GeneralMachine
{
    public partial class frm_LoadCell : UserControl
    {
        public frm_LoadCell()
        {
            InitializeComponent();
        }

        private Module module = Module.Front;
        private Nozzle nz = Nozzle.Nz1;

        private frm_Main frm_Main = null;
        private ToolStripStatusLabel[] ZPress = new ToolStripStatusLabel[4];

        private void frm_LoadCell_Load(object sender, EventArgs e)
        {
            this.tBaseZ.Text = "-1";
            this.dGV_Press.Rows.Add(50);
            Common.CommonHelper.AddRowHeader(this.dGV_Press);
        }

        private void tSetZ_Click(object sender, EventArgs e)
        {
        }

        private void bTest1_Click(object sender, EventArgs e)
        {
            this.TestZPress(0);
        }

        private void bTest2_Click(object sender, EventArgs e)
        {
            this.TestZPress(1);
        }

        private void bTest3_Click(object sender, EventArgs e)
        {
            this.TestZPress(2);
        }

        private void bTest4_Click(object sender, EventArgs e)
        {
            this.TestZPress(3);
        }

        private void bTest5_Click(object sender, EventArgs e)
        {
            this.TestZPress(4);
        }

        private void bGetK_Click(object sender, EventArgs e)
        {
            this.ChartPaneFront.GraphPane.CurveList.Clear();
            double[] press1 = new double[50];
            double[] press2 = new double[50];

            double[] avgPress1 = new double[5];
            double[] avgPress2 = new double[5];

            for (int i = 0; i < 50; i ++)
            {
                press1[i] = double.Parse(this.dGV_Press.Rows[i].Cells[1].Value.ToString());
                press2[i] = double.Parse(this.dGV_Press.Rows[i].Cells[2].Value.ToString());
            }

            LineItem curve = this.ChartPaneFront.GraphPane.AddCurve("", press1, press2, Color.Black, SymbolType.Circle);
            curve.Line.IsVisible = false;
            curve.Symbol.Size = 15;
            curve.Symbol.Fill = new Fill(Color.Yellow);
            this.ChartPaneFront.AxisChange();
            this.ChartPaneFront.Invalidate();

            double K = 0;
            double D = 0;
            double R = 0;

            if(VisionHelper.FitLine(press1, press2, out K, out D, out R))
            {
                this.tK.Text = K.ToString("F3");
                this.tD.Text = D.ToString("F3");
                MessageBox.Show($"相关性: {R}");

                for (int i = 0; i < 50; i++)
                {
                    this.dGV_Press.Rows[i].Cells[3].Value = (K * press1[i] + D).ToString("F3");
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double k = double.Parse(this.tK.Text);
                double d = double.Parse(this.tD.Text);

                double value = double.Parse(this.textBox4.Text);
                this.lPressResult.Text = (value * k + d).ToString("f3");
            }
            catch { }
        }

        private void TestZPress(int index)
        {
            try
            {
                double baseZ = double.Parse(this.tBaseZ.Text);
                //VelMode[] vel = SystemEntiy.Instance.Machines[(int)module].GetVel(Definition.SpeedMode.Auto_Fast);
                double zPos = baseZ - index * 0.5;
                int startIndex = index*10;

                Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 10; ++i)
                    {
                        //GeneralSystem.Instance.Machines[(int)module].ZMap[(int)nz].GoSafePos(vel);
                        Thread.Sleep(1000);
                        //GeneralSystem.Instance.Machines[(int)module].ZMap[(int)nz].GoPosTillStop(30000, zPos, vel);
                        Thread.Sleep(3000);
                        double press = double.Parse(ZPress[(int)nz + (int)module * 2].Text);
                        this.dGV_Press.Rows[startIndex+i].Cells[0].Value = index.ToString();
                        this.dGV_Press.Rows[startIndex + i].Cells[1].Value = press.ToString();
                        this.dGV_Press.Rows[startIndex + i].Cells[2].Value = "0";
                    }
                });
            }
            catch { }
        }
    }
}
