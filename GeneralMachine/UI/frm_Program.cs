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
using WeifenLuo.WinFormsUI.Docking;

namespace GeneralMachine
{
    public partial class frm_Program : DockContent
    {
        public frm_Main frm_Main = null;
        public string ProgramName = "";


        public frm_Program(Object obj,string propath)
        {
            InitializeComponent();
            frm_Main = (frm_Main)obj;
            this.Text += ":" + propath;
            this.ProgramName = propath.Substring(propath.LastIndexOf("\\") + 1);
            
            Graph_Program.GraphPane.Title.Text = this.Text;
        }
        public frm_Program()
        {
            InitializeComponent();
        }

        private void frm_Program_Load(object sender, EventArgs e)
        {
            try
            {
                if(frm_Main.frm_Wizard_Program == null || frm_Main.frm_Wizard_Program.IsDisposed)
                {
                    frm_Main.frm_Wizard_Program = new frm_Wizard_Program(frm_Main);
                }

                frm_Main.frm_Wizard_Program.TopLevel = false;
                pProgram.Controls.Clear();//移除所有控件
                pProgram.Controls.Add(frm_Main.frm_Wizard_Program);
                frm_Main.frm_Wizard_Program.Dock = DockStyle.Fill;
                frm_Main.frm_Wizard_Program.Show();
            }
            catch(Exception ex) { Debug.WriteLine(ex.Message); }
        }
    }
}
