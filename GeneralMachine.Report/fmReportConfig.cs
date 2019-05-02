using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Report
{
    public partial class fmReportConfig : Form
    {
        public fmReportConfig()
        {
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            ReportHelper.Instance.DayShfitTime = new TimeSpan((int)this.numDay.Value, 0, 0);
            ReportHelper.Instance.NightShfitTime = new TimeSpan((int)this.numNight.Value, 0, 0);
            ReportHelper.Instance.ReportPath = this.tPath.Text;
            ReportHelper.Save();
            this.Close();
        }

        private void bCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fmReportConfig_Load(object sender, EventArgs e)
        {
            this.numDay.Value = ReportHelper.Instance.DayShfitTime.Hours;
            this.numNight.Value = ReportHelper.Instance.NightShfitTime.Hours;
            this.tPath.Text = ReportHelper.Instance.ReportPath;
        }
    }
}
