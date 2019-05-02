using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using GeneralMachine.Config;

namespace GeneralMachine.Press
{
    public partial class PressChartCtrl : UserControl
    {
        public PressChartCtrl()
        {
            InitializeComponent();
        }

        private Dictionary<Nozzle, List<double>> PressList = new Dictionary<Nozzle, List<double>>()
        {
            { Nozzle.Nz1, new List<double>()},
            { Nozzle.Nz2, new List<double>()},
            { Nozzle.Nz3, new List<double>()},
            { Nozzle.Nz4, new List<double>()},
        };

        private Dictionary<Nozzle, Color> NzColor = new Dictionary<Nozzle, Color>
        {
            { Nozzle.Nz1, Color.Red},
            { Nozzle.Nz2, Color.Yellow},
            { Nozzle.Nz3, Color.Black},
            { Nozzle.Nz4, Color.Blue},
        };

        public void AddPress(Nozzle nozzle,double value)
        {
            PressList[nozzle].Add(value);

            var curve = zedPress.GraphPane.CurveList[(int)nozzle] as LineItem;
            if (curve == null) return;
            IPointListEdit list = curve.Points as IPointListEdit;
            if (list == null)
            {
                return;
            }
            list.Add(list.Count, value);

            this.zedPress.AxisChange();
            this.zedPress.Refresh();
        }

        private void PressChartCtrl_Load(object sender, EventArgs e)
        {

            GraphPane myPane = zedPress.GraphPane;
            myPane.Title.Text = "贴附压力实时曲线";
            myPane.XAxis.Title.Text = "贴附次数";
            myPane.YAxis.Title.Text = "贴附压力(g)";
            for(Nozzle nz = Nozzle.Nz1; nz <= Nozzle.Nz4;++nz)
            {
                this.zedPress.GraphPane.AddCurve($"吸嘴{nz}",new PointPairList(), NzColor[nz], SymbolType.None);
            }
        }
    }
}
