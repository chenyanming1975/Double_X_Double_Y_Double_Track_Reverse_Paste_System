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
        private int flag = 0;//1:���;2:�޸�
        public FrmRoles()
        {
            InitializeComponent();
        }

        //�����
        private void ClearTreeView()
        {
            //һ��Ŀ¼
            for (int i = 0; i < tvModdleInfo.Nodes.Count; i++)
            {
                tvModdleInfo.Nodes[i].Checked = false;
                for (int J = 0; J < tvModdleInfo.Nodes.Count; J++)
                {

                }
                //����Ŀ¼
                for (int j = 0; j < tvModdleInfo.Nodes[i].Nodes.Count; j++)
                {
                    tvModdleInfo.Nodes[i].Nodes[j].Checked = false;
                    //����Ŀ¼
                    for (int k = 0; k < tvModdleInfo.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        tvModdleInfo.Nodes[i].Nodes[j].Nodes[k].Checked = false;
                    }
                }
            }
        }

        /// <summary>
        /// �󶨽�ɫ
        /// </summary>
        private void BindRole()
        {
            //���ؽ�ɫ��Ϣ
            lbRole.DataSource = Roles.BindRoles().Tables["RolesTB"];
            lbRole.DisplayMember = "Role_Name";
            lbRole.ValueMember = "Role_ID";
        }

        private void FrmRoles_Load(object sender, EventArgs e)
        {
            //���ؽ�ɫ
            BindRole();

            //����ģ���б�
            //����һ��ģ��
            SqlParameter[] param ={
                new SqlParameter("@ModuleLen",SqlDbType.Char,4)
            };
            param[0].Value = 1;//һ��ģ��
            DataTable dt = Roles.BindModule(param);
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Name = dt.Rows[i]["Module_ID"].ToString();
                tn.Text = dt.Rows[i]["Module_Name"].ToString();
                tvModdleInfo.Nodes.Add(tn);
            }
            //���ض���ģ��
            param[0].Value = 2;
            dt = Roles.BindModule(param);
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Name = dt.Rows[i]["Module_ID"].ToString();
                tn.Text = dt.Rows[i]["Module_Name"].ToString();
                tvModdleInfo.Nodes[0].Nodes.Add(tn);
            }
            //��������ģ��
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
            tvModdleInfo.ExpandAll();//չ�����нڵ�
        }

        private void btnRoleAdd_Click(object sender, EventArgs e)
        {
            //���
            btnRoleAdd.Enabled = false;
            btnOK.Enabled = true;
            panRole.Enabled = true;
            //������
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
            //һ��Ŀ¼
            for (int i = 0; i < tvModdleInfo.Nodes.Count; i++)
            {
                tvModdleInfo.Nodes[i].Checked = false;
                if (-1 != role.IndexOf(tvModdleInfo.Nodes[i].Name))
                    tvModdleInfo.Nodes[i].Checked=true;
                //����Ŀ¼
                for (int j = 0; j < tvModdleInfo.Nodes[i].Nodes.Count; j++)
                {
                    tvModdleInfo.Nodes[i].Nodes[j].Checked = false;
                    if (-1 != role.IndexOf(tvModdleInfo.Nodes[i].Nodes[j].Name))
                        tvModdleInfo.Nodes[i].Nodes[j].Checked = true;
                    //����Ŀ¼
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
                MessageBox.Show("��ѡ��Ȩ��");
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
            //��ɫȨ����Ϣ
            SqlParameter[] param1 ={
                new SqlParameter("@Module_ID",SqlDbType.VarChar,50),
                new SqlParameter("@Role_ID",SqlDbType.Char,4)
            };
            //һ��Ŀ¼
            string role = "";
            for (int i = 0; i < tvModdleInfo.Nodes.Count; i++)
            {
                if (tvModdleInfo.Nodes[i].Checked)
                    role = role + "0|";
                //����Ŀ¼
                for (int j = 0; j < tvModdleInfo.Nodes[i].Nodes.Count; j++)
                {
                    if (tvModdleInfo.Nodes[i].Nodes[j].Checked)
                        role = role + tvModdleInfo.Nodes[i].Nodes[j].Name + "|";
                    //����Ŀ¼
                    for (int k = 0; k < tvModdleInfo.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        if (tvModdleInfo.Nodes[i].Nodes[j].Nodes[k].Checked)
                            role = role + tvModdleInfo.Nodes[i].Nodes[j].Nodes[k].Name + "|";
                    }
                }
            }
            param1[0].Value = role;
            param1[1].Value = param[0].Value;
            //����
            switch(flag)
            {
                case 1://���
                    if (Roles.InsertRole(param)&&Roles.InsertRoleControl(param1))
                        MessageBox.Show("��ӽ�ɫ�ɹ���");
                    else
                        MessageBox.Show("��ӽ�ɫʧ�ܣ�");
                    break;
                case 2://�޸�
                    param[0].Value = param1[1].Value = lbRole.SelectedValue;
                    if (Roles.UpdateRole(param) && Roles.UpdateRoleControl(param1))
                        MessageBox.Show("�޸Ľ�ɫ�ɹ���");
                    else
                        MessageBox.Show("�޸Ľ�ɫʧ�ܣ�");
                    break;
                default:
                    break;
            }
            BindRole();
            btnCencel_Click(sender, e);
        }

        private void btnRoleModify_Click(object sender, EventArgs e)
        {
            //�޸�
            tvModdleInfo.Enabled = true;
            btnOK.Enabled = true;
            panRole.Enabled = true;
            //���Ϊ�޸�
            flag = 2;
        }

        private void btnRoleDelete_Click(object sender, EventArgs e)
        {
            //ɾ����ɫ
            if (DialogResult.Yes == MessageBox.Show("ȷ��ɾ���ý�ɫ��", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                SqlParameter[] param ={
                    new SqlParameter("@Role_ID",SqlDbType.Char,4)
                };
                param[0].Value = lbRole.SelectedValue;
                if (Roles.DeleteRole(param))
                    MessageBox.Show("ɾ����ɫ�ɹ���");
                else
                    MessageBox.Show("ɾ����ɫʧ�ܣ�");
                BindRole();
            }
        }
    }
}