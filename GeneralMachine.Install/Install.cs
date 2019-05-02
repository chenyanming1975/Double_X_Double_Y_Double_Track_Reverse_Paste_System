using GeneralMachine.Axis;
using GeneralMachine.Config;
using GeneralMachine.Flow;
using GeneralMachine.IO;
using GeneralMachine.Motion;
using GeneralMachine.SpeedManager;
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

namespace GeneralMachine.Install
{
    public partial class Install : Form
    {
        public Install()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_CameraConfig frm_CameraConfig = new frm_CameraConfig();
            frm_CameraConfig.Show();
            this.button1.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_AxisConfig frm_AxisConfig = new frm_AxisConfig();
            frm_AxisConfig.Show();
            this.button2.BackColor = Color.Transparent;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_IOConfig frm_IOConfig = new frm_IOConfig();
            frm_IOConfig.Show();
            this.button3.BackColor = Color.Transparent;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frm_IOState frm_IOState = new frm_IOState();
            frm_IOState.Show();
            this.button4.BackColor = Color.Transparent;
        }

        private void Install_Load(object sender, EventArgs e)
        {
            bool rtn = MotionHelper.Instance.InitCard(PathDefine.sPathCard);
            if (!rtn)
            {
                MessageBox.Show("轴卡初始化失败");
            }

            SystemConfig.Load();
            MotionHelper.Instance.StartMointorIO();
            SpeedDefine.Load(); // 速度配置
            CameraDefine.Load(); // 相机配置
            IODefine.Load(); // IO配置
            AxisDefine.Load(); // 轴配置
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frm_SpeedConfig fm = new frm_SpeedConfig();
            fm.ShowDialog();
            this.button5.BackColor = Color.Transparent;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[Module.Front].MachineHome();
            SystemEntiy.Instance[Module.After].MachineHome();
        }

        private void Install_FormClosed(object sender, FormClosedEventArgs e)
        {
            MotionHelper.Instance.bSystemExit = true;
            Thread.Sleep(200);
            Axis_Advantech.CardClose();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frm_TestTrack frm = new frm_TestTrack();
            frm.Show();
        }
    }
}
