using GeneralMachine.Config;
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

namespace GeneralMachine
{
    public partial class frm_Feeder : UserControl
    {
        public frm_Feeder()
        {
            InitializeComponent();

            this.lLeftUsed.ContextMenu = new ContextMenu();
            this.lLeftUsed.ContextMenu.MenuItems.Add(new MenuItem("卸载", (s,e)=> {
                this.toolUnInstall_Click(Feeder.Left);
            }));
            this.lRightUsed.ContextMenu = new ContextMenu();
            this.lRightUsed.ContextMenu.MenuItems.Add(new MenuItem("卸载", (s,e)=> {
                this.toolUnInstall_Click(Feeder.Right);
            }));
        }

        public Module Module = Module.Front;

        private void listFeeder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tAdd_Click(object sender, EventArgs e)
        {
            string name = Interaction.InputBox("请输入新创建的Feeder配置名称", "新建Feeder配置", "Feeder");
            if (name == string.Empty)
            {
                return;
            }
            else if (FeederDefine.Instance.FeederExit(Module, name))
            {
                MessageBox.Show("创建的Feeder 信息已经存在，请重新命名!!!");
                return;
            }


            FeederConfig config = new FeederConfig();
            config.Module = Module;
            config.FeederName = name;
            this.feederInfo.FeederConfig = config;
            this.feederInfo.Visible = true;
            FeederDefine.Instance.SaveFeederConfig(config);
            this.ReloadFeederList();
        }

        private void tDelete_Click(object sender, EventArgs e)
        {
            if (this.listFeeder.Items.Count > 0 && this.listFeeder.SelectedIndex != -1
           && this.listFeeder.SelectedIndex < this.listFeeder.Items.Count)
            {
                if (MessageBox.Show($"是否要删除--选中的[{this.listFeeder.Items[this.listFeeder.SelectedIndex].ToString()}] 信息!!!Y/N", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FeederDefine.Instance.RemoveFeederConfig(this.feederInfo.FeederConfig);
                    this.feederInfo.FeederConfig = null;
                    this.feederInfo.Visible = false;
                    this.ReloadFeederList();
                }
            }
        }

        private void tRefresh_Click(object sender, EventArgs e)
        {

        }

        private void frm_Feeder_Load(object sender, EventArgs e)
        {
            this.feederInfo.Visible = false;
            this.moduleRadio1.ModuleChange += (sd, module) =>
            {
                this.Module = module;
                this.feederInfo.Visible = false;
                this.ReloadFeederList();
            };
            this.moduleRadio1.Module = Module.Front;
        }

        private void ReloadFeederList()
        {
            this.listFeeder.Items.Clear();
            this.listFeeder.Items.AddRange(FeederDefine.Instance.GetFeederList(this.Module).ToArray());
        }

        private void listFeeder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listFeeder.Items.Count > 0 && this.listFeeder.SelectedIndex != -1
            && this.listFeeder.SelectedIndex < this.listFeeder.Items.Count)
            {
                this.feederInfo.FeederConfig = FeederDefine.Instance.GetFeederConfig(Module, this.listFeeder.Items[this.listFeeder.SelectedIndex].ToString());
            }
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            FeederDefine.Instance.SaveFeederConfig(this.feederInfo.FeederConfig);
            if(this.feederInfo.FeederConfig.FeederName
                == FeederDefine.Instance.InstallFeederName[this.feederInfo.FeederConfig.Module][this.feederInfo.FeederConfig.Feeder])
            {
                FeederDefine.Instance.InstallFeeder[this.Module][this.feederInfo.FeederConfig.Feeder] = this.feederInfo.FeederConfig;
                FeederDefine.Save();
            }
        }

        private void toolInstall_Click(object sender, EventArgs e)
        {
            if (this.listFeeder.SelectedItems.Count > 0 && this.listFeeder.SelectedIndex != -1
            && this.listFeeder.SelectedIndex < this.listFeeder.Items.Count)
            {
                var feder = FeederDefine.Instance.GetFeederConfig(Module, this.listFeeder.Items[this.listFeeder.SelectedIndex].ToString());

                if (MessageBox.Show($"是否安装[{feder.FeederName}] 到 [{feder.Feeder}] Y/N", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FeederDefine.Instance.InstallFeederName[this.Module][feder.Feeder] = feder.FeederName;
                    FeederDefine.Instance.InstallFeeder[this.Module][feder.Feeder] = feder;
                    this.lLeftUsed.Text = $"左Feeder正在使用:[{FeederDefine.Instance.InstallFeederName[this.Module][Feeder.Left]}]";
                    this.lRightUsed.Text = $"右Feeder正在使用:[{FeederDefine.Instance.InstallFeederName[this.Module][Feeder.Right]}]";
                    FeederDefine.Save();
                }
            }
        }

        private void moduleRadio1_ModuleChange(object sender, Module e)
        {
            this.Module = e;
            this.lLeftUsed.Text = $"左Feeder正在使用:[{FeederDefine.Instance.InstallFeederName[e][Feeder.Left]}]";
            this.lRightUsed.Text = $"右Feeder正在使用:[{FeederDefine.Instance.InstallFeederName[e][Feeder.Right]}]";
        }

        private void toolUnInstall_Click(Feeder feeder)
        {

            if (FeederDefine.Instance.InstallFeederName[this.Module][feeder] != string.Empty)
            {
                if (MessageBox.Show($"是否卸载当前使用的Feeder [{FeederDefine.Instance.InstallFeederName[this.Module][feeder]}] Y/N", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FeederDefine.Instance.InstallFeederName[this.Module][feeder] = string.Empty;
                    FeederDefine.Instance.InstallFeeder[this.Module].Remove(feeder);
                    this.lLeftUsed.Text = $"左Feeder正在使用:[{FeederDefine.Instance.InstallFeederName[this.Module][Feeder.Left]}]";
                    this.lRightUsed.Text = $"右Feeder正在使用:[{FeederDefine.Instance.InstallFeederName[this.Module][Feeder.Right]}]";
                    FeederDefine.Save();
                }
            }
        }
    }
}
