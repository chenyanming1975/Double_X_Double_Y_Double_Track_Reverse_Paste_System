using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Press
{
    public partial class PressRealCtrl : UserControl
    {
        public PressRealCtrl()
        {
            InitializeComponent();
            this.dGVPress.Rows.Add();
            this.dGVPress.Rows[0].Cells[0].Value = "实时压力";
            this.dGVPress.Rows.Add();
            this.dGVPress.Rows[1].Cells[0].Value = "贴附压力";
            this.dGVPress.Rows.Add();
            this.dGVPress.Rows[2].Cells[0].Value = "超压次数";
        }



        public void UpdatePress(bool pasted = false)
        {
            this.BeginInvoke(new Action(() => {
                if(pasted)
                {
                    //for(int i = 0; i < this.PressLabels.Count; ++i)
                    //{
                    //    double press = PressHelper.Instance.PastePress[i];
                    //    this.PressLabels[i].Text = $"Z{i + 1}:{press:N1}";
                    //    if (PressHelper.Instance.AlarmLimit > press)
                    //        this.PressLabels[i].BackColor = Color.Red;
                    //    else if(this.PressLabels[i].BackColor == Color.Red)
                    //    {
                    //        this.PressLabels[i].BackColor = Color.LightGreen;
                    //    }
                    //}
                }
                else
                {
                    //for (int i = 0; i < this.PressLabels.Count; ++i)
                    //{
                    //    double press = PressHelper.Instance.CurPress[i];
                    //    this.PressLabels[i].Text = $"Z{i + 1}:{press:N1}";

                    //    if(this.PressLabels[i].BackColor == Color.Red)
                    //    {
                    //        this.PressLabels[i].BackColor = Color.LightGreen;
                    //    }
                    //}
                }
            }));
        }
    }
}
