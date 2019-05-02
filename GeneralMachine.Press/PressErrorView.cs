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

namespace GeneralMachine.Press
{
    public partial class PressErrorView : UserControl
    {
        public PressErrorView()
        {
            InitializeComponent();
        }

        public void UpdateInfo(Module module,int nz, double press, int pastePCB, int pastePCS)
        {
            if(press > PressHelper.Instance.Sensors[module].AlarmLimit)
            {
                int rowIndex = this.dGVPressError.Rows.Add();
                this.dGVPressError.Rows[rowIndex].Cells[0].Value = Common.CommonHelper.GetNowTime();
                this.dGVPressError.Rows[rowIndex].Cells[1].Value = module == Module.Front ? "前模组":"后模组";
                this.dGVPressError.Rows[rowIndex].Cells[2].Value = $"{nz + 1}";
                this.dGVPressError.Rows[rowIndex].Cells[3].Value = $"{press:N1}";
                this.dGVPressError.Rows[rowIndex].Cells[4].Value = $"{pastePCB+1}-{pastePCS+1}";
            }
        }
    }
}
