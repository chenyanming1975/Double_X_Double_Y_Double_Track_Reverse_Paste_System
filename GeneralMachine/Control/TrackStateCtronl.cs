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
using GeneralMachine.Track;
using GeneralMachine.Flow;

namespace GeneralMachine
{
    public partial class TrackStateCtronl : UserControl
    {
        public TrackStateCtronl()
        {
            InitializeComponent();

            this.cb_ManualTrackMode.Items.Clear();
            foreach (FlowInOutMode mode in Enum.GetValues(typeof(FlowInOutMode)))
            {
                this.cb_ManualTrackMode.Items.Add(Enum.GetName(typeof(FlowInOutMode), mode));
            }

            this.cb_ManualTrackMode.SelectedIndex = 0;
        }

        private Config.Track track = Config.Track.ForntTrack;

        /// <summary>
        /// 头
        /// </summary>
        public string Title
        {
            get
            {
                return this.gTrack.Text;
            }

            set
            {
                this.gTrack.Text = value;
            }
        }

        public Config.Track CurTrack
        {
            get
            {
                return this.track;
            }
            set
            {
                this.track = value;
            }
        }

        private void bInput_Click(object sender, EventArgs e)
        {
            TrackManager.Instance.TrackEntiy[CurTrack].ManualInput((FlowInOutMode)this.cb_ManualTrackMode.SelectedIndex);
        }

        private void bOutput_Click(object sender, EventArgs e)
        {
            TrackManager.Instance.TrackEntiy[CurTrack].ManualOutput((FlowInOutMode)this.cb_ManualTrackMode.SelectedIndex);
        }

        private void TrackStateCtronl_Enter(object sender, EventArgs e)
        {
            cb_ManualTrackMode.SelectedIndex = (int)SystemConfig.Instance.Tracks[CurTrack].FlowInOutMode;
        }
    }
}
