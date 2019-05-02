using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Motion;

namespace GeneralMachine.BasicUI
{
    public partial class VelWithLabel : UserControl
    {
        public VelWithLabel()
        {
            InitializeComponent();
        }

        public HostarSpeed Speed
        {
            get
            {
                return this.speedControl1.HostarSpeed;
            }

            set
            {
                this.speedControl1.HostarSpeed = value;
            }
        }
    }
}
