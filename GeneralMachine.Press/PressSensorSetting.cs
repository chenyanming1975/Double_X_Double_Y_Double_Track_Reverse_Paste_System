using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Press
{
    public partial class PressSensorSetting : Form
    {
        public PressSensorSetting()
        {
            InitializeComponent();
        }

        private void ConfigToUI()
        {
            this.nAlarmPress.Value = (decimal)PressHelper.Instance.Sensors[this.cbModule.Module].AlarmLimit;
            this.nAlarmTimes.Value = (decimal)PressHelper.Instance.Sensors[this.cbModule.Module].AlarmCount;
            this.tIP.Text = PressHelper.Instance.Sensors[this.cbModule.Module].SensorIP;
        }

        private void UIToConfig()
        {
            PressHelper.Instance.Sensors[this.cbModule.Module].AlarmLimit = (double)this.nAlarmPress.Value;
            PressHelper.Instance.Sensors[this.cbModule.Module].AlarmCount = (int)this.nAlarmTimes.Value;
            PressHelper.Instance.Sensors[this.cbModule.Module].SensorIP = this.tIP.Text;
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            PressHelper.Instance.Sensors[this.cbModule.Module].ReConnected();
        }

        private void bDisConnect_Click(object sender, EventArgs e)
        {
            PressHelper.Instance.Sensors[this.cbModule.Module].DisConnected();
        }

        private void bZero_Click(object sender, EventArgs e)
        {
            PressHelper.Instance.Sensors[this.cbModule.Module].SendZeroAll();
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            this.UIToConfig();
            PressHelper.Save();
        }

        private void bAuto_Click(object sender, EventArgs e)
        {

        }

        private void cbNz_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tFixedPress.Text = PressHelper.Instance.Sensors[this.cbModule.Module].NozzlePress[this.cbNz.SelectedIndex].ToString("f2");
        }

        private void cbModule_ModuleChange(object sender, Config.Module e)
        {
            this.ConfigToUI();
            this.tFixedPress.Text = PressHelper.Instance.Sensors[e].NozzlePress[this.cbNz.SelectedIndex].ToString("f2");
        }
    }
}
