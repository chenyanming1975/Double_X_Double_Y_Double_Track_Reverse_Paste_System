using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using GeneralMachine.Common;
using System.Threading;
using GeneralMachine.UserManager;

namespace GeneralMachine
{
    public partial class frm_UserManager : DockContent
    {
        private bool flag = false;
        private frm_Main frm_Main = null;
        List<UserInfo> urs;
   
        public frm_UserManager(frm_Main obj)
        {
            InitializeComponent();
            frm_Main = (frm_Main)obj;
        }

        private void UpdateUserInfo()
        {
            urs = UserInfoHelper.GetAllUsers();
            listView.Items.Clear();
            for (int i = 0; i < urs.Count; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Add(urs[i].UserId.ToString());
                li.SubItems.Add(urs[i].LoginUserName);
                li.SubItems.Add(urs[i].Role);
                li.SubItems.Add(urs[i].NoteName);
                this.listView.Items.Add(li);
            }
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_UserManager_Load(object sender, EventArgs e)
        {
            treeView.Nodes.Clear();
            CommonHelper.GetMenu(treeView, frm_Main.Menu_Main);
            UpdateUserInfo();
            if (urs.Count > 0)
            {
                listView.Items[0].Selected = true;
            }
            treeView.ExpandAll();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            try
            {
                UserInfo ur = new UserInfo();
                ur.Permission = "0";
                ur.LastLoginTime = DateTime.Now;
                ur.LoginUserName = "User";
                ur.Pwd = CommonHelper.GetMD5Code("666666");
                ur.NoteName = "Operator";
                ur.Role = "Operator";
                ur.UserId = urs.Count;
                urs.Add(ur);
                UserInfoHelper.AddUserInfo(ur);
                UpdateUserInfo();
            }
            catch (Exception)
            {
                msgDiv1.MsgDivShow("操作失败!", 1);
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView.SelectedItems.Count > 0)
                {
                    UserInfoHelper.DeleteUserInfo(urs[listView.SelectedItems[0].Index]);
                    this.listView.Items.Remove(listView.SelectedItems[0]);
                }
            }
            catch
            {
                msgDiv1.MsgDivShow("操作失败!", 1);
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckText() && this.listView.SelectedItems.Count > 0)
                {
                    UserInfo ur = new UserInfo();
                    ur.Permission = CommonHelper.GetPermession(treeView).ToString();
                    ur.LastLoginTime = DateTime.Now;
                    ur.LoginUserName = tLoginName.Text;
                    ur.Pwd = CommonHelper.GetMD5Code(tPassword.Text);
                    ur.NoteName = tNoteInfo.Text;
                    if (rB_Manager.Checked)
                    {
                        ur.Role = "Manager";
                    }
                    if (rB_Engineer.Checked)
                    {
                        ur.Role = "Engineer";
                    }
                    if (rB_Techinical.Checked)
                    {
                        ur.Role = "Technician";
                    }
                    if (rB_Operator.Checked)
                    {
                        ur.Role = "Operator";
                    }
                    ur.UserId =int.Parse(listView.SelectedItems[0].SubItems[1].Text);
                    urs.Add(ur);
                    listView.SelectedItems[0].SubItems[1].Text = ur.UserId.ToString();
                    listView.SelectedItems[0].SubItems[2].Text = ur.LoginUserName;
                    listView.SelectedItems[0].SubItems[3].Text = ur.Role;
                    listView.SelectedItems[0].SubItems[4].Text = ur.NoteName;
                    UserInfoHelper.UpdateUserInfo(ur);
                    UpdateUserInfo();
                }
            }
            catch
            {
                msgDiv1.MsgDivShow("操作失败!", 1);
            }
        }

      

        /// <summary>
        /// 窗体输入框检测
        /// </summary>
        /// <returns></returns>
        private bool CheckText()
        {
            if (string.IsNullOrEmpty(tLoginName.Text))//帐号是否为空
            {
                msgDiv1.MsgDivShow("帐号不能为空", 1);
                return false;
            }
            if (string.IsNullOrEmpty(tPassword.Text))//密码是否为空
            {
                msgDiv1.MsgDivShow("密码不能为空", 1);
                return false;
            }
            if (string.IsNullOrEmpty(tPasswordAgain.Text))//密码是否为空
            {
                msgDiv1.MsgDivShow("密码确认不能为空", 1);
                return false;
            }
            if (string.IsNullOrEmpty(tNoteInfo.Text))//密码是否为空
            {
                msgDiv1.MsgDivShow("备注不能为空", 1);
                return false;
            }
            if (tPassword.Text != tPasswordAgain.Text)//密码是否相同
            {
                msgDiv1.MsgDivShow("密码与密码确认不相同", 1);
                return false;
            }
            return true;
        }
        
        private void UpdateUserInfo2UI()
        {
            if (this.listView.SelectedItems.Count > 0)
            {
                flag = true;
                int index = listView.SelectedItems[0].Index;
                CommonHelper.SetPermession(treeView, int.Parse(urs[index].Permission));
                switch (urs[index].Role)
                {
                    case "Manager":
                        rB_Manager.Checked = true;
                        break;
                    case "Engineer":
                        rB_Engineer.Checked = true;
                        break;
                    case "Technician":
                        rB_Techinical.Checked = true;
                        break;
                    case "Operator":
                        rB_Operator.Checked = true;
                        break;
                }
                tLoginName.Text = urs[index].LoginUserName;
                tNoteInfo.Text = urs[index].NoteName;
                tPassword.Text = urs[index].Pwd;
                tPasswordAgain.Text = urs[index].Pwd;
                flag = false;
            }
        }

        private void listView_Click(object sender, EventArgs e)
        {
            UpdateUserInfo2UI();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUserInfo2UI();
        }


        /// <summary>
        /// 权限联动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (flag)
            {
                return;
            }
            if (treeView.Tag == null || (bool)treeView.Tag == false)
            {
                treeView.Tag = true;
            }
            else
            {
                return;
            }
            //仅执行由鼠标勾选触发的事件，避免代码递归执行勾选，无限循环  
            if (e.Action != TreeViewAction.ByMouse)
                return;

            this.UpdateNodesCheckState(e.Node);
            treeView.Tag = false;
        }

        /// <summary>  
        /// 设置节点上下级状态  
        /// </summary>  
        /// <param name="node"></param>  
        private void UpdateNodesCheckState(TreeNode node)
        {
            //设置子节点，向下递归  
            SetChildNode(node);

            //设置父节点，向上递归  
            if (node.Checked)
            {
                //勾选  
                SetParentNodeChecked(node);
            }
            else
            {
                //未勾选  
                //若同级节点都未勾选，则父节点去除勾选。不断向上递归判断  
                SetParentNodeUnChecked(node);
            }
        }

        /// <summary>  
        /// 节点勾选，设置父节点选中。并向上递归设置选中  
        /// </summary>  
        /// <param name="node"></param>  
        private void SetParentNodeChecked(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Checked = true;
                SetParentNodeChecked(node.Parent);
            }
        }

        /// <summary>  
        /// 节点未勾选，设置上级节点状态  
        /// 判断同级节点状态，设置父节点  
        /// </summary>  
        /// <param name="node"></param>  
        private void SetParentNodeUnChecked(TreeNode node)
        {
            bool IaAllUnChecked = true;
            if (node.Parent != null)
            {
                for (int i = 0; i < node.Parent.Nodes.Count; i++)
                {
                    if (node.Parent.Nodes[i].Checked)
                    {
                        IaAllUnChecked = false;
                        break;
                    }
                }
            }

            //当前节点的所有同级节点均未勾选，则父节点设置状态未勾选。向上递归  
            if (IaAllUnChecked && node.Parent != null)
            {
                node.Parent.Checked = node.Checked;
                SetParentNodeUnChecked(node.Parent);
            }
        }
        /// <summary>  
        /// 父节点变化，其子节点同步变化，其所有子节点与父节点勾选状态保持一致。  
        /// </summary>  
        /// <param name="node"></param>  
        private void SetChildNode(TreeNode node)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                childNode.Checked = node.Checked;
                if (node.Nodes.Count > 0)
                    SetChildNode(childNode);
            }
        }



    }
}
