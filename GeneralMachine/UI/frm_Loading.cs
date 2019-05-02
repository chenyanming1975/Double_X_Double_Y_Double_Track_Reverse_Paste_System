using GeneralMachine.Config;
using GeneralMachine.Flow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine
{
    public partial class frm_Loading : Form
    {
        public frm_Loading()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            SystemEntiy.Instance[Config.Module.Front].HomeEvent -= FrontMachineHome;
            SystemEntiy.Instance[Config.Module.After].HomeEvent -= AfterMachineHome;
            SystemEntiy.Instance.HomeFinished -= HomeFinished;
        }

        private void pHostarLog_Click(object sender, EventArgs e)
        {

        }

        private void frm_Loading_Load(object sender, EventArgs e)
        {
            SystemEntiy.Instance[Config.Module.Front].HomeEvent += FrontMachineHome;
            SystemEntiy.Instance[Config.Module.After].HomeEvent += AfterMachineHome;
            SystemEntiy.Instance.HomeFinished += HomeFinished;
            this.bConfim.Enabled = false;
        }

        protected void HomeFinished(object sender, Module module)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (module == Config.Module.Front)
                    listFront.Items.Insert(listFront.Items.Count, "前模组复位完成");
                else
                    listAfter.Items.Insert(listAfter.Items.Count, "后模组复位完成");
            }));

            if (SystemEntiy.Instance.WorkStatus == Definition.WorkStatus.Ready)
            {
                this.bConfim.Visible = true;
                this.bConfim.Enabled = true;
            }
        } 

        private void FrontMachineHome(object sender, HomeEventArgs eventArgs)
        {
            this.BeginInvoke(new Action(() => {
                listFront.Items.Insert(listFront.Items.Count, eventArgs.Msg);
                pBar_Front.Value = (int)eventArgs.ProgressRate;
            }));
        }

        private void AfterMachineHome(object sender, HomeEventArgs eventArgs)
        {
            this.BeginInvoke(new Action(() => {
                listAfter.Items.Insert(listAfter.Items.Count, eventArgs.Msg);
                pBar_After.Value = (int)eventArgs.ProgressRate;
            }));
        }
    }
}
