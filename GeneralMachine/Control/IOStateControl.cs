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

namespace GeneralMachine.IO
{
    public partial class IOStateControl : UserControl
    {
        public IOStateControl()
        {
            InitializeComponent();
            this.Timer.Interval = 300;
            this.Timer.Tick += Timer_Tick;
        }

        private Timer Timer = new Timer();

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (Control control in this.tpOuput.Controls)
            {
                (control as BasicUI.BasicIOControl).RefreshStatus();
            }

            foreach (Control control in this.tpInput.Controls)
            {
                (control as BasicUI.BasicIOControl).RefreshStatus();
            }
        }

        public void SetInput(List<IOInput> input)
        {
            this.Timer.Stop();
            this.tpInput.Controls.Clear();
            foreach(IOInput item in input)
            {
                var ui = new BasicUI.BasicIOControl();
                ui.IOProt = item;
                ui.Dock = DockStyle.Fill;
                this.tpInput.Controls.Add(ui);
            }
            this.Timer.Start();
        }

        public void SetOutput(List<IOOutput> ouput)
        {
            this.Timer.Stop();
            this.tpOuput.Controls.Clear();
            foreach (IOOutput item in ouput)
            {
                var ui = new BasicUI.BasicIOControl();
                ui.IOProt = item;
                ui.Dock = DockStyle.Fill;
                this.tpOuput.Controls.Add(ui);
            }

            this.Timer.Start();
        }

        private void IOStateControl_Load(object sender, EventArgs e)
        {
        }


        private void IOStateControl_Leave(object sender, EventArgs e)
        {
        }

        public void WriteText()
        {
            foreach(Control control in this.tpOuput.Controls)
            {
                BasicUI.BasicIOControl ctrl = control as BasicUI.BasicIOControl;
                ctrl.WriteText();
            }

            foreach (Control control in this.tpInput.Controls)
            {
                BasicUI.BasicIOControl ctrl = control as BasicUI.BasicIOControl;
                ctrl.WriteText();
            }
        }
    }
}
