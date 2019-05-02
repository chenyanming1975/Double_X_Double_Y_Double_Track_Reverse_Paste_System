using GeneralMachine.Config;
using GeneralMachine.Flow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.SystemManager
{
    public partial class fm_SystemSet : Form
    {
        public fm_SystemSet()
        {
            InitializeComponent();
        }

        private void cbDryRun_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbSuckTest_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void fm_SystemSet_Load(object sender, EventArgs e)
        {
            try
            {
                this.propertyGrid1.SelectedObject = SystemConfig.Instance.Tracks[Config.Track.ForntTrack].Clone() as TrackConfig; ;
                this.propertyGrid2.SelectedObject = SystemConfig.Instance.Tracks[Config.Track.AfterTrack].Clone() as TrackConfig; ;
                this.propertyGrid3.SelectedObject = SystemConfig.Instance.General.Clone() as GeneralConfig;
            }
            catch { }
        }

        private void bUpdateTrack_Click(object sender, EventArgs e)
        {
            try
            {
                SystemConfig.Instance.Tracks[Config.Track.ForntTrack] = this.propertyGrid1.SelectedObject as TrackConfig;
                SystemConfig.Instance.Tracks[Config.Track.AfterTrack] = this.propertyGrid2.SelectedObject as TrackConfig;
            }
            catch { }
        }

        private void bSaveToSys_Click(object sender, EventArgs e)
        {
            SystemConfig.Instance.General = this.propertyGrid3.SelectedObject as GeneralConfig;
            SystemConfig.Save();
        }

        private void bSetYSafe_Click(object sender, EventArgs e)
        {
            (this.propertyGrid3.SelectedObject as GeneralConfig).YMaxSafe = SystemEntiy.Instance[Module.Front].XYPos.Y + SystemEntiy.Instance[Module.After].XYPos.Y;
            this.propertyGrid3.Refresh();
        }
    }
}
