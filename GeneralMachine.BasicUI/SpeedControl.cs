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
    public partial class SpeedControl : UserControl
    {
        public SpeedControl()
        {
            InitializeComponent();
        }

        private HostarSpeed hostarSpeed = new HostarSpeed();

        public double MaxStard
        {
            get;
            set;
        } = 100;

        public double MaxSpeed
        {
            get;
            set;
        } = 1500;

        public double MinAccTime
        {
            get;
            set;
        } = 100;

        public double MinDecTime
        {
            get;
            set;
        } = 100;

        public HostarSpeed HostarSpeed
        {
            get
            {
                this.UpdateHostarSpeed();
                return hostarSpeed;
            }

            set
            {
                hostarSpeed = value;
                this.SetHostarSpeed();
            }
        }

        private void SetHostarSpeed()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.SetHostarSpeed();
                }));
            }
            else
            {
                if (this.hostarSpeed == null)
                    this.hostarSpeed = new HostarSpeed();
                this.teStartSpeed.Text = this.hostarSpeed.StartSpeed.ToString();
                this.teMaxSpeed.Text = this.hostarSpeed.MaxSpeed.ToString();
                this.teAcc.Text = this.hostarSpeed.AccTime.ToString();
                this.teDec.Text = this.hostarSpeed.DecTime.ToString();
            }
        }

        private void UpdateHostarSpeed()
        {
            double acc = 0, dec = 0, maxSpeed = 0, startSpeed = 0;
            double.TryParse(this.teAcc.Text, out acc);
            double.TryParse(this.teDec.Text, out dec);
            double.TryParse(this.teMaxSpeed.Text, out maxSpeed);
            double.TryParse(this.teStartSpeed.Text, out startSpeed);

            if (startSpeed > this.MaxStard)
                startSpeed = this.MaxStard;
            if (maxSpeed > this.MaxSpeed)
                maxSpeed = this.MaxSpeed;
            if (acc < this.MinAccTime)
                acc = this.MinAccTime;
            if (dec < this.MinDecTime)
                dec = this.MinDecTime;

            this.hostarSpeed.AccTime = acc;
            this.hostarSpeed.DecTime = dec;
            this.hostarSpeed.StartSpeed = startSpeed;
            this.hostarSpeed.MaxSpeed = maxSpeed;
        }
    }
}