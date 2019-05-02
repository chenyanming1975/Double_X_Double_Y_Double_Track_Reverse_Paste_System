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

namespace GeneralMachine.BasicUI
{
    public partial class BasicIOControl : UserControl
    {
        public BasicIOControl()
        {
            InitializeComponent();
            this.picState.Tag = false;
        }

        public BasicIOControl(IOInput io)
        {
            InitializeComponent();
            this.IOProt = io;
            this.IsInput = true;
        }

        public BasicIOControl(IOOutput io)
        {
            InitializeComponent();
            this.IOProt = io;
        }

        public void WriteText()
        {
            this.IOProt.Text = this.tText.Text;
        }
        private IOProt prot = new IOInput();

        /// <summary>
        /// IO状态
        /// </summary>
        public IOProt IOProt
        {
            get
            {
                return this.prot;
            }
            set
            {
                if(value != null)
                {
                    this.IsInput = value.GetType() == typeof(IOInput);
                    this.prot = value;
                    this.labelMark.Text = this.prot.Name;
                    this.tText.Text = this.prot.Text;
                }
            }
        }

        /// <summary>
        /// 是否是输入点
        /// </summary>
        private bool IsInput = true;

        public void RefreshStatus()
        {
            if (this.IsInput)
            {
                if ((this.IOProt as IOInput).GetIO())
                    this.picState.Image = global::GeneralMachine.BasicUI.Properties.Resources.lamp_on;
                else
                    this.picState.Image = global::GeneralMachine.BasicUI.Properties.Resources.lamp_off;
            }
            else
            {
                bool state = (this.IOProt as IOOutput).GetIO();
                this.picState.Tag = state;
                if (state)
                    this.picState.Image = global::GeneralMachine.BasicUI.Properties.Resources.lamp_on;
                else
                    this.picState.Image = global::GeneralMachine.BasicUI.Properties.Resources.lamp_off;
            }
        }

        private void picState_Click(object sender, EventArgs e)
        {
            if (!this.IsInput)
            {
                bool last = (bool)this.picState.Tag;
                if (last)
                    this.picState.Image = global::GeneralMachine.BasicUI.Properties.Resources.lamp_off;
                else
                    this.picState.Image = global::GeneralMachine.BasicUI.Properties.Resources.lamp_on;
                if (!(this.IOProt as IOOutput).GetIO())
                {
                    (this.IOProt as IOOutput).SetIO(true);
                }
                else
                {
                    (this.IOProt as IOOutput).SetIO(false);
                }
                this.picState.Tag = !last;
            }
        }
    }
}