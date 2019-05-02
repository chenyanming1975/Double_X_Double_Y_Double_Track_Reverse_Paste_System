using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Motion;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.IO
{
    public partial class frm_IOState : Form
    {
        public frm_IOState()
        {
            InitializeComponent();
            foreach(CardNo cardNo in Enum.GetValues(typeof(CardNo)))
            {
                this.comboBox1.Items.Add(CommonHelper.GetEnumDescription(typeof(CardNo), cardNo));
            }

            this.comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CardNo t = (CardNo)this.comboBox1.SelectedIndex;
            this.ioStateControl1.SetInput(IODefine.Instance.Inputs[t]);
            this.ioStateControl1.SetOutput(IODefine.Instance.Outputs[t]);
        }

        private void bWrite_Click(object sender, EventArgs e)
        {
            string s = Interaction.InputBox("请输入权限密码进行该操作", "提示", "*****", -1, -1);  //-1表示在屏幕的中间
            if (s == "admin")
            {
                this.ioStateControl1.WriteText();
                IODefine.Save();
                this.bWrite.BackColor = Color.LightGreen;
                MessageBox.Show("操作成功!!!");
            }
            else
                MessageBox.Show("操作失败!!!");
        }
    }
}
