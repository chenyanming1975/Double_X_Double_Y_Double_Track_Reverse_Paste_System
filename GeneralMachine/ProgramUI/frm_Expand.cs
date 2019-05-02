using GeneralMachine.Common;
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

namespace GeneralMachine
{
    public partial class frm_Expand : Form
    {
        private static frm_Expand instance = null;

        public static frm_Expand Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new frm_Expand();

                return instance;
            }
        }

        private frm_Expand()
        {
            InitializeComponent();
            this.orgPos.Set(Module);
            this.xPos.Set(Module);
            this.yPos.Set(Module);
            this.orgPos.Point = Variable.ExpandOrg;
            this.xPos.Point = Variable.ExpandX;
            this.yPos.Point = Variable.ExpandY;
            this.tN.Text = Variable.Column.ToString();
            this.tM.Text = Variable.Row.ToString();
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
                this.xPos.Set(value);
                this.yPos.Set(value);
                this.orgPos.Set(value);
            }
        }
        private short row = 0;
        private short column = 0;
   
        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.row = short.Parse(tM.Text);
                this.column = short.Parse(tN.Text);
                Common.Variable.Column = column;
                Common.Variable.Row = row;
                Common.Variable.IsExpand = true;
                Common.Variable.ExpandOrg = this.orgPos.Point;
                Common.Variable.ExpandX = this.xPos.Point;
                Common.Variable.ExpandY = this.yPos.Point;
                this.Close();
            }
            catch
            {
            }
        }

        private void frm_Expand_Load(object sender, EventArgs e)
        {
        }
    }
}
