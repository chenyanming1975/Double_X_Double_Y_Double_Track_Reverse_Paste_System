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
using GeneralMachine.Motion;
using GeneralMachine.Flow;

namespace GeneralMachine.BasicUI
{
    public partial class ZUConfigCtrl : UserControl
    {
        public ZUConfigCtrl()
        {
            InitializeComponent();
            UpdateUI();
        }

        public Module Module
        {
            get;
            set;
        } = Module.Front;

        public Nozzle Nozzle
        {
            get;
            set;
        } = Nozzle.Nz1;
        /// <summary>
        /// 吸嘴配置
        /// </summary>
        private NozzleConfig nozzleConfig = new NozzleConfig();

        /// <summary>
        /// 刷新控件
        /// </summary>
        public void UpdateUI()
        {
            this.tZSafeHeight.Text = nozzleConfig.SafeHeight.ToString("f3");
            this.tPasteHeight.Text = nozzleConfig.PasteHeight.ToString("f3");
            this.tXIHeight.Text = nozzleConfig.XIHeight.ToString("f3");
            this.tDropHeight.Text = nozzleConfig.DropHeight.ToString("f3");
            this.tRInit.Text = nozzleConfig.RInit.ToString("f3");
        }

        /// <summary>
        /// 吸嘴配置
        /// </summary>
        public NozzleConfig NozzleConfig
        {
            get
            {
                return nozzleConfig;
            }

            set
            {
                if(value !=  null)
                {
                    nozzleConfig = value;
                    UpdateUI();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.NozzleConfig.SafeHeight = SystemEntiy.Instance[Module].MachineAxis.Z[(int)Nozzle].Pos;
            UpdateUI();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.NozzleConfig.PasteHeight = SystemEntiy.Instance[Module].MachineAxis.Z[(int)Nozzle].Pos;
            UpdateUI();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.NozzleConfig.XIHeight = SystemEntiy.Instance[Module].MachineAxis.Z[(int)Nozzle].Pos;
            UpdateUI();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.NozzleConfig.DropHeight = SystemEntiy.Instance[Module].MachineAxis.Z[(int)Nozzle].Pos;
            UpdateUI();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.NozzleConfig.RInit = SystemEntiy.Instance[Module].MachineAxis.R[(int)Nozzle].Pos;
            UpdateUI();
        }
    }
}
