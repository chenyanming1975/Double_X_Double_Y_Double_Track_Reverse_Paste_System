using GeneralMachine.Common;
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
    public partial class frm_MessageBox : Form
    {
        /// <summary>
        /// DialogResult Yes-GOON  No-Halt
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sMessageIn"></param>
        /// <param name="AlarmEN"></param>
        /// <param name="CancelEN"></param>
        public frm_MessageBox(string sMessageIn, bool AlarmEN,bool CancelEN)
        {
            InitializeComponent();
            tInfo.Text = sMessageIn;
            tInfo.Select(tInfo.TextLength, 0);
            bStopAlarm.Visible = AlarmEN;
            bHalt.Visible = CancelEN;
        }

        private void bStopAlarm_Click(object sender, EventArgs e)
        {
            
        }

        private void bHalt_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void bGOON_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void frm_MessageBox_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// OK Cancel
        /// </summary>
        /// <param name="str"></param>
        public static DialogResult ShowMessage_OKCancel(string str)
        {
            frm_MessageBox frmmess = new frm_MessageBox(str, false, true);
            return frmmess.ShowDialog();
        }

        /// <summary>
        /// 报警 OK 取消
        /// </summary>
        /// <param name="str"></param>
        public static DialogResult ShowMessage_Alarm_OKCancel(string str)
        {
            frm_MessageBox frmmess = new frm_MessageBox(str, true, true);
            return frmmess.ShowDialog();
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="str"></param>
        public static void ShowMessage(string str)
        {
            MsgHelper.Instance.WriteLog(MsgLevel.Warn, str);
            frm_MessageBox frmmess = new frm_MessageBox(str, false, false);
            frmmess.ShowDialog();
        }

    }
}
