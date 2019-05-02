using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Common;
using GeneralMachine.Config;

namespace GeneralMachine.BasicUI
{
    public partial class NozzleRadio : UserControl
    {
        public NozzleRadio()
        {
            InitializeComponent();
            bool check = false;
            foreach (Nozzle nz in Enum.GetValues(typeof(Nozzle)))
            {
                RadioButton rad = new RadioButton();
                rad.Text = CommonHelper.GetEnumDescription(typeof(Nozzle), nz);

                if (!check)
                {
                    rad.Checked = true;
                    rad.BackColor = Color.CornflowerBlue;
                    check = true;
                }
                else
                {
                    rad.BackColor = Color.Transparent;
                }

                rad.Tag = nz;
                rad.Click += Rad_Click; ;
                rad.Dock = DockStyle.Left;
                rad.Font = this.Font;
                this.Controls.Add(rad);
            }
        }

        private Nozzle selectNz = Nozzle.Nz1;

        public event EventHandler<Nozzle> NozzleChanged;

        public Nozzle SelectNz
        {
            get
            {
                return this.selectNz;
            }
            set
            {
                if(this.selectNz != value)
                {
                    this.selectNz = value;
                    this.NozzleChanged?.Invoke(this,this.selectNz);
                }
            }
        }

        private void Rad_Click(object sender, EventArgs e)
        {
            var tt = (sender as RadioButton);
            this.SelectNz = (Nozzle)tt.Tag;
            foreach (Control btn in this.Controls)
            {
                if (btn.GetType() == typeof(RadioButton))
                {
                    RadioButton t = btn as RadioButton;
                    if ((Nozzle)t.Tag == this.selectNz)
                    {
                        t.BackColor = Color.CornflowerBlue;
                        t.Checked = true;
                    }
                    else
                    {
                        t.BackColor = Color.Transparent;
                        t.Checked = false;
                    }
                }
            }
        }
    }
}
