using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GeneralMachine
{
    public partial class FrmRoles : Form
    {
        private int flag = 0;//1:添加;2:修改
        public FrmRoles()
        {
            InitializeComponent();
        }

        //清空树
        private void ClearTreeView()
        {
            //一级目录
            for (int i = 0; i < tvModdleInfo.Nodes.Count; i++)
            {
                tvModdleInfo.Nodes[i].Checked = false;
                for (int J = 0; J < tvModdleInfo.Nodes.Count; J++)
                {

                }
                //二级目录
                for (int j = 0; j < tvModdleInfo.Nodes[i].Nodes.Count; j++)
                {
                    tvModdleInfo.Nodes[i].Nodes[j].Checked = false;
                    //三级目录
                    for (int k = 0; k < tvModdleInfo.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        tvModdleInfo.Nodes[i].Nodes[j].Nodes[k].Checked = false;
                    }
                }
            }
        }

        /// <summary>
        /// 绑定角色
        /// </summary>
        private void BindRole()
        {
            //加载角色信息
            lbRole.DataSource = Roles.BindRoles().Tables["RolesTB"];
            lbRole.DisplayMember = "Role_Name";
            lbRole.ValueMember = "Role_ID";
        }

        private void FrmRoles_Load(object sender, EventArgs e)
        {
            //加载角色
            BindRole();

            //加载模块列表
            //加载一级模块
            SqlParameter[] param ={
                new SqlParameter("@ModuleLen",SqlDbType.Char,4)
            };
            param[0].Value = 1;//一级模块
            DataTable dt = Roles.BindModule(param);
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Name = dt.Rows[i]["Module_ID"].ToString();
                tn.Text = dt.Rows[i]["Module_Name"].ToString();
                tvModdleInfo.Nodes.Add(tn);
            }
            //加载二级模块
            param[0].Value = 2;
            dt = Roles.BindModule(param);
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Name = dt.Rows[i]["Module_ID"].ToString();
                tn.Text = dt.Rows[i]["Module_Name"].ToString();
                tvModdleInfo.Nodes[0].Nodes.Add(tn);
            }
            //加载三级模块
            param[0].Value = 3;
            dt = Roles.BindModule(param);
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Name = dt.Rows[i]["Module_ID"].ToString();
                tn.Text = dt.Rows[i]["Module_Name"].ToString();
                for (int j = 0; j < tvModdleInfo.Nodes[0].Nodes.Count; j++)
                {
                    if (-1 != tn.Name.IndexOf(tvModdleInfo.Nodes[0].Nodes[j].Name))
                        tvModdleInfo.Nodes[0].Nodes[j].Nodes.Add(tn);
                }
            }
            tvModdleInfo.ExpandAll();//展开所有节点
        }

        private void btnRoleAdd_Click(object sender, EventArgs e)
        {
            //添加
            btnRoleAdd.Enabled = false;
            btnOK.Enabled = true;
            panRole.Enabled = true;
            //标记添加
            flag = 1;
            tvModdleInfo.Enabled = true;
            ClearTreeView();
            tbRoleName.Focus();
        }

        private void lbRole_Click(object sender, EventArgs e)
        {

            SqlParameter[] param ={
                new SqlParameter("@Role_ID",SqlDbType.Char,4)
            };
            param[0].Value = lbRole.SelectedValue;
            string role = Roles.GetRole(param);
            //一级目录
            for (int i = 0; i < tvModdleInfo.Nodes.Count; i++)
            {
                tvModdleInfo.Nodes[i].Checked = false;
                if (-1 != role.IndexOf(tvModdleInfo.Nodes[i].Name))
                    tvModdleInfo.Nodes[i].Checked=true;
                //二级目录
                for (int j = 0; j < tvModdleInfo.Nodes[i].Nodes.Count; j++)
                {
                    tvModdleInfo.Nodes[i].Nodes[j].Checked = false;
                    if (-1 != role.IndexOf(tvModdleInfo.Nodes[i].Nodes[j].Name))
                        tvModdleInfo.Nodes[i].Nodes[j].Checked = true;
                    //三级目录
                    for (int k = 0; k < tvModdleInfo.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        tvModdleInfo.Nodes[i].Nodes[j].Nodes[k].Checked = false;
                        if (-1 != role.IndexOf(tvModdleInfo.Nodes[i].Nodes[j].Nodes[k].Name))
                            tvModdleInfo.Nodes[i].Nodes[j].Nodes[k].Checked = true;
                    }
                }
            }

            tvModdleInfo.Enabled = false;
            btnRoleModify.Enabled = true;
            btnRoleDelete.Enabled = true;
            string[] data = new string[2];
            Roles.GetRole(ref data,param);
            tbRoleName.Text = data[0];
            tbRemark.Text = data[1];
            
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            btnRoleAdd.Enabled = true;
            btnRoleModify.Enabled = false;
            btnRoleDelete.Enabled = false;
            btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tvModdleInfo.Nodes[0].Checked == false)
            {
                MessageBox.Show("请选择权限");
                return;
            }

            SqlParameter[] param ={
                new SqlParameter("@Role_ID",SqlDbType.Char,4),
                new SqlParameter("@Role_Name",SqlDbType.VarChar,50),
                new SqlParameter("@Remark",SqlDbType.NVarChar,300)
            };
            param[0].Value = Roles.GetLastRoleID();
            param[1].Value = tbRoleName.Text.Trim();
            param[2].Value = tbRemark.Text.Trim();
            //角色权限信息
            SqlParameter[] param1 ={
                new SqlParameter("@Module_ID",SqlDbType.VarChar,50),
                new SqlParameter("@Role_ID",SqlDbType.Char,4)
            };
            //一级目录
            string role = "";
            for (int i = 0; i < tvModdleInfo.Nodes.Count; i++)
            {
                if (tvModdleInfo.Nodes[i].Checked)
                    role = role + "0|";
                //二级目录
                for (int j = 0; j < tvModdleInfo.Nodes[i].Nodes.Count; j++)
                {
                    if (tvModdleInfo.Nodes[i].Nodes[j].Checked)
                        role = role + tvModdleInfo.Nodes[i].Nodes[j].Name + "|";
                    //三级目录
                    for (int k = 0; k < tvModdleInfo.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        if (tvModdleInfo.Nodes[i].Nodes[j].Nodes[k].Checked)
                            role = role + tvModdleInfo.Nodes[i].Nodes[j].Nodes[k].Name + "|";
                    }
                }
            }
            param1[0].Value = role;
            param1[1].Value = param[0].Value;
            //操作
            switch(flag)
            {
                case 1://添加
                    if (Roles.InsertRole(param)&&Roles.InsertRoleControl(param1))
                        MessageBox.Show("添加角色成功！");
                    else
                        MessageBox.Show("添加角色失败！");
                    break;
                case 2://修改
                    param[0].Value = param1[1].Value = lbRole.SelectedValue;
                    if (Roles.UpdateRole(param) && Roles.UpdateRoleControl(param1))
                        MessageBox.Show("修改角色成功！");
                    else
                        MessageBox.Show("修改角色失败！");
                    break;
                default:
                    break;
            }
            BindRole();
            btnCencel_Click(sender, e);
        }

        private void btnRoleModify_Click(object sender, EventArgs e)
        {
            //修改
            tvModdleInfo.Enabled = true;
            btnOK.Enabled = true;
            panRole.Enabled = true;
            //标记为修改
            flag = 2;
        }

        private void btnRoleDelete_Click(object sender, EventArgs e)
        {
            //删除角色
            if (DialogResult.Yes == MessageBox.Show("确定删除该角色？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                SqlParameter[] param ={
                    new SqlParameter("@Role_ID",SqlDbType.Char,4)
                };
                param[0].Value = lbRole.SelectedValue;
                if (Roles.DeleteRole(param))
                    MessageBox.Show("删除角色成功！");
                else
                    MessageBox.Show("删除角色失败！");
                BindRole();
            }
        }
    }
}