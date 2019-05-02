using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using GeneralMachine.Common;
using GeneralMachine.UserManager;

namespace GeneralMachine
{
    public partial class frm_Login : Form
    {
        public frm_Main frm_Main = null;
        public frm_Login(object obj)
        {
            InitializeComponent();
            frm_Main = (frm_Main)obj;
        }
        public frm_Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //帐号和密码不为空
            if (CheckText())
            {
                string ur;
                int permission;
                string msg = "";
                //判断用户登录是否成功
                string pwd = CommonHelper.GetMD5Code(tLoginPassword.Text);
                if (UserInfoHelper.IsLoginByLoginName(tLoginName.Text, pwd, out msg, out ur, out permission))
                {
                    Variable.sPermission_CurerentUserName = tLoginName.Text;
                    Variable.sPermission_CurerentRole = ur;
                    Variable.iPermission_CurerentUser = permission;
                }
                else
                {
                    tLoginPassword.Text = "";
                }
            }
        }

        private void btnNoIdLogin_Click(object sender, EventArgs e)
        {
            Variable.sPermission_CurerentUserName = "游客";
            Variable.sPermission_CurerentRole = "游客";
            Variable.iPermission_CurerentUser = (int)(Math.Pow(2, 30) - 1);
            //Variable.iPermission_CurerentUser = 1069547907;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnExist_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public void Bind()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private bool CheckText()
        {
            if (string.IsNullOrEmpty(tLoginName.Text))//帐号是否为空
            {
                return false;
            }
            if (string.IsNullOrEmpty(tLoginPassword.Text))//密码是否为空
            {
                return false;
            }
            return true;
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {
            btnLogin_Click(sender,e);
        }
    }
}
