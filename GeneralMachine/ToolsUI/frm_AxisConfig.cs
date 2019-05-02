using GeneralMachine.Config;
using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Axis
{
    public partial class frm_AxisConfig : Form
    {
        public frm_AxisConfig()
        {
            InitializeComponent();
            axisTimer.Interval = 100;
            axisTimer.Tick += AxisTimer_Tick;
            axisTimer.Start();
        }

        private void AxisTimer_Tick(object sender, EventArgs e)
        {
            if(this.cb_CardList.SelectedIndex >= 0 && this.cbAxisNo.SelectedIndex >= 0)
            {
                int axisNo = this.cbAxisNo.SelectedIndex;
                bool nextCard = false;
                if(this.cbAxisNo.SelectedIndex > 3)
                {
                    axisNo -= 4;
                    nextCard = true;
                }

                CardNo card = (CardNo)(this.cb_CardList.SelectedIndex * 2);
                if (nextCard)
                {
                    card = (CardNo)(card + 1);
                }

                this.pLmtP.BackColor = MotionHelper.Instance.Cards[card][axisNo].bPosLimit ? Color.Red : Color.Green;
                this.pLmtN.BackColor = MotionHelper.Instance.Cards[card][axisNo].bNegLimit ? Color.Red : Color.Green;
                this.pOrg.BackColor = MotionHelper.Instance.Cards[card][axisNo].bHome ? Color.Red : Color.Green;
                this.pWarn.BackColor = MotionHelper.Instance.Cards[card][axisNo].bAxisServoWarning ? Color.Red : Color.Green;
                this.pEmg.BackColor = MotionHelper.Instance.Cards[card][axisNo].bAxisEmgOn ? Color.Red : Color.Green;
            }
        }

        private Timer axisTimer = new Timer();

        private void frm_AxisConfig_Load(object sender, EventArgs e)
        {
            this.cb_CardList.Items.AddRange(Axis_Advantech.GetCard().ToArray());
            this.propertyGrid1.SelectedObject = AxisDefine.Instance.MachineAxis[Module.Front];
            this.propertyGrid2.SelectedObject = AxisDefine.Instance.MachineAxis[Module.After];
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            AxisDefine.Save();
        }

        private void cb_CardList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.cb_CardList.SelectedIndex >= 0)
            {
                this.cbAxisNo.Items.Clear();

                if (this.cb_CardList.Text.Contains("1285"))
                {
                    for(int i = 0; i < 8; i++)
                    {
                        this.cbAxisNo.Items.Add($"轴{i + 1}");
                    }
                }
                else if(this.cb_CardList.Text.Contains("1245"))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        this.cbAxisNo.Items.Add($"轴{i + 1}");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.cb_CardList.SelectedIndex >= 0 && this.cbAxisNo.SelectedIndex >= 0)
            {
                int axisNo = this.cbAxisNo.SelectedIndex;
                bool nextCard = false;
                if (this.cbAxisNo.SelectedIndex > 3)
                {
                    axisNo -= 4;
                    nextCard = true;
                }

                CardNo card = (CardNo)(this.cb_CardList.SelectedIndex * 2);
                if (nextCard)
                {
                    card = (CardNo)(card + 1);
                }

                MotionHelper.Instance.Cards[card][axisNo].SetAxisServoOn();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.cb_CardList.SelectedIndex >= 0 && this.cbAxisNo.SelectedIndex >= 0)
            {
                int axisNo = this.cbAxisNo.SelectedIndex;
                bool nextCard = false;
                if (this.cbAxisNo.SelectedIndex > 3)
                {
                    axisNo -= 4;
                    nextCard = true;
                }

                CardNo card = (CardNo)(this.cb_CardList.SelectedIndex * 2);
                if (nextCard)
                {
                    card = (CardNo)(card + 1);
                }

                MotionHelper.Instance.Cards[card][axisNo].SetAxisServoOff();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.cb_CardList.SelectedIndex >= 0 && this.cbAxisNo.SelectedIndex >= 0)
            {
                int axisNo = this.cbAxisNo.SelectedIndex;
                bool nextCard = false;
                if (this.cbAxisNo.SelectedIndex > 3)
                {
                    axisNo -= 4;
                    nextCard = true;
                }

                CardNo card = (CardNo)(this.cb_CardList.SelectedIndex * 2);
                if (nextCard)
                {
                    card = (CardNo)(card + 1);
                }
            }
        }
    }
}
