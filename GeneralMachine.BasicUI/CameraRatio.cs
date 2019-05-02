using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using GeneralMachine.Common;
using GeneralMachine.Config;

namespace GeneralMachine.BasicUI
{
    public partial class CameraRatio : UserControl
    {
        public CameraRatio()
        {
            InitializeComponent();
            bool check = false;
            foreach(Camera cam in Enum.GetValues(typeof(Camera)))
            {
                RadioButton rad = new RadioButton();
                rad.Text = CommonHelper.GetEnumDescription(typeof(Camera), cam);

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

                rad.Tag = cam;
                rad.Click += Rad_Click;
                rad.Dock = DockStyle.Left;
                rad.Font = this.Font;
                this.Controls.Add(rad);
            }
        }

        private Camera selectCamera = Camera.Top;

        public event EventHandler<Camera> CameraChanged;
        public Camera SelectCamera
        {
            get
            {
                return this.selectCamera;
            }
            set
            {
                if (this.selectCamera != value)
                {
                    this.selectCamera = value;
                    CameraChanged?.Invoke(this,this.selectCamera);
                }
            }
        }
        private void Rad_Click(object sender, EventArgs e)
        {
            var tt = (sender as RadioButton);
            this.SelectCamera = (Camera)tt.Tag;
            foreach (Control btn in this.Controls)
            {
                if(btn.GetType() == typeof(RadioButton))
                {
                    RadioButton t = btn as RadioButton;
                    if((Camera)t.Tag == this.selectCamera)
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
