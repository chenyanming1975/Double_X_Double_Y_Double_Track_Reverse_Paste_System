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
using GeneralMachine.Common;

namespace GeneralMachine.BasicUI
{
    public partial class CalibRatio : UserControl
    {
        public CalibRatio()
        {
            InitializeComponent();

            this.AddRatio("Mark相机", 0);
            this.AddRatio("吸嘴1", 1);
            this.AddRatio("吸嘴2", 2);
            this.AddRatio("吸嘴3", 3);
            this.AddRatio("吸嘴4", 4);
            this.AddRatio("Label相机", 5);
            (this.Controls[0] as RadioButton).BackColor = Color.CornflowerBlue;
            (this.Controls[0] as RadioButton).Checked = true;
        }

        private void AddRatio(string name, int index)
        {
            RadioButton rad = new RadioButton();
            rad.Text = name;
            rad.Font = this.Font;
            rad.BackColor = Color.Transparent;
            rad.Checked = false;
            rad.Tag = index;
            rad.Dock = DockStyle.Left;
            rad.Click += Rad_Click;
            this.Controls.Add(rad);
        }

        private void Rad_Click(object sender, EventArgs e)
        {
            var tt = (sender as RadioButton);
            tt.BackColor = Color.CornflowerBlue;
            tt.Checked = true;
            int index = (int)tt.Tag;
            switch (index)
            {
                case 0:
                    this.SelectCam = Camera.Top;
                    break;
                case 1:
                    this.SelectCam = Camera.Bottom1;
                    this.SelectNz = Nozzle.Nz1;
                    break;
                case 2:
                    this.SelectCam = Camera.Bottom1;
                    this.SelectNz = Nozzle.Nz2;
                    break;
                case 3:
                    this.SelectCam = Camera.Bottom2;
                    this.SelectNz = Nozzle.Nz3;
                    break;
                case 4:
                    this.SelectCam = Camera.Bottom2;
                    this.SelectNz = Nozzle.Nz4;
                    break;
                case 5:
                    this.SelectCam = Camera.Label;
                    break;
            }

            foreach (Control btn in this.Controls)
            {
                if(btn.Tag != tt.Tag)
                {
                    btn.BackColor = Color.Transparent;
                    (btn as RadioButton).Checked = false;
                }
            }
        }

        public Camera SelectCam = Camera.Top;

        public Nozzle SelectNz = Nozzle.Nz1;

    }
}
