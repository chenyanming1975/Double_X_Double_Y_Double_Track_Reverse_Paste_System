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
using GeneralMachine.Common;

namespace GeneralMachine.Flow.Editer
{
    public partial class CreateProgramCtrl : Form
    {
        public CreateProgramCtrl(bool isCreate = true)
        {
            InitializeComponent();
            this.isCreate = isCreate;
            this.tProgramName.Items.Clear();
            if (this.isCreate)
            {
                this.tProgramName.DropDownStyle = ComboBoxStyle.DropDown;
                this.bCreate.Text = "创建";
            }
            else
            {
                this.lSelectName.Text = "请选择导入的程式";
                this.lSelectMode.Text = "请选择导入的模组";

                this.tProgramName.DropDownStyle = ComboBoxStyle.DropDownList;
                this.bCreate.Text = "导入";
            }
        }

        public string ProgramName = string.Empty;
        public Module Module = Module.Front;
        private bool isCreate = true;

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            this.ProgramName = this.tProgramName.Text;

            if (string.IsNullOrEmpty(this.ProgramName))
                this.DialogResult = DialogResult.No;
            else
            {
                this.Module = (Module)this.tModule.SelectedIndex;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void tModule_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if(!isCreate)
            {
                this.tProgramName.Items.Clear();
                this.tProgramName.Items.AddRange(ProgramFlow.GetProgramList((Module)this.tModule.SelectedIndex));
            }
        }
    }
}
