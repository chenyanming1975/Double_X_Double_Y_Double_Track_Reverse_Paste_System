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

namespace GeneralMachine.BasicUI
{
    public partial class ModuleRadio : UserControl
    {
        public ModuleRadio()
        {
            InitializeComponent();
            this.rModule1.Checked = true;
        }

        private Module module = Module.Front;

        public Module Module
        {
            get
            {
                return this.module;
            }

            set
            {
                this.module = value;
                ModuleChange?.Invoke(this, Module);
            }
        }

        public event EventHandler<Module> ModuleChange;

        private void rModule1_Click(object sender, EventArgs e)
        {
            this.rModule1.Checked = true;
            this.rModule2.Checked = false;
            this.rModule1.BackColor = Color.CornflowerBlue;
            this.rModule2.BackColor = Color.Transparent;
            Module = Module.Front;
        }

        private void rModule2_Click(object sender, EventArgs e)
        {
            this.rModule1.Checked = false;
            this.rModule2.Checked = true;
            this.rModule1.BackColor = Color.Transparent;
            this.rModule2.BackColor = Color.CornflowerBlue;
            Module = Module.After;
        }
    }
}
