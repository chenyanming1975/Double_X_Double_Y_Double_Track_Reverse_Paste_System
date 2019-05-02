using GeneralMachine.Track;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Install
{
    public partial class frm_TestTrack : Form
    {
        public frm_TestTrack()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrackManager.Instance.TrackEntiy[Config.Track.AfterTrack].ManualInput(Config.FlowInOutMode.左进左出);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TrackManager.Instance.TrackEntiy[Config.Track.AfterTrack].ManualOutput(Config.FlowInOutMode.左进左出);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TrackManager.Instance.TrackEntiy[Config.Track.AfterTrack].Config.FlowInOutMode = Config.FlowInOutMode.左进左出;
            TrackManager.Instance.TrackEntiy[Config.Track.ForntTrack].Config.FlowInOutMode = Config.FlowInOutMode.左进左出;

            TrackManager.Instance.TrackStart(Config.Track.AfterTrack);
            TrackManager.Instance.TrackStart(Config.Track.ForntTrack);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TrackManager.Instance.TrackPasue(Config.Track.AfterTrack);
            TrackManager.Instance.TrackPasue(Config.Track.ForntTrack);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void frm_TestTrack_Load(object sender, EventArgs e)
        {
            TrackManager.Instance.TrackInit();
        }
    }
}
