using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Vision
{
    public partial class fmCreateVisionProgram : Form
    {
        public fmCreateVisionProgram(bool isCreate = true)
        {
            InitializeComponent();
            this.tProgramName.Items.Clear();

            if (isCreate)
            {
                this.tProgramName.DropDownStyle = ComboBoxStyle.DropDown;
                this.bCreate.Text = "创建";
            }
            else
            {
                this.tProgramName.DropDownStyle = ComboBoxStyle.DropDownList;
                this.tProgramName.Items.AddRange(VisionToolCtrl.GetVisionList());
                this.bCreate.Text = "导入";
            }
        }

        private bool isCreate = true;

        public string VisionProgram = string.Empty;

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(this.tProgramName.Text))
            {
                this.DialogResult = DialogResult.No;
            }
            else
            {
                this.VisionProgram = this.tProgramName.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
