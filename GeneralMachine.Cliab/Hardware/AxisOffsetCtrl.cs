using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Motion;
using GeneralMachine.Config;

namespace GeneralMachine.Cliab
{
    public partial class AxisOffsetCtrl : UserControl
    {
        public AxisOffsetCtrl(GeneralAxis axis)
        {
            InitializeComponent();
            this.Axis = axis;
        }

        private GeneralAxis axis = GeneralAxis.X;

        public GeneralAxis Axis
        {
            get { return this.Axis; }
            set
            {
                this.axis = value;
                this.gTitle.Text = $"{axis}轴机械误差校正";
            }
        }

        private Module module = Module.Front;

        public Module Module
        {
            get
            {
                return this.module;
            }

            set
            {
                this.module = value;
                this.dGVAxisOffset.Rows.Clear();

                // 导入数据
                foreach(AxisOffsetItem item in HardwareOrgHelper.Instance.AxisOffset[this.module][this.axis])
                {
                    int rowIndex = this.dGVAxisOffset.Rows.Add();
                    this.dGVAxisOffset.Rows[rowIndex].Cells[0].Value = item.MoveDist.ToString("f2");
                    this.dGVAxisOffset.Rows[rowIndex].Cells[1].Value = item.Offset.ToString("f2");
                }
            }
        }

        private void bUp_Click(object sender, EventArgs e)
        {
            if(this.dGVAxisOffset.SelectedRows.Count > 0 && this.dGVAxisOffset.SelectedRows[0].Index > 0)
            {
                int line = this.dGVAxisOffset.SelectedRows[0].Index;
                int preLine = this.dGVAxisOffset.SelectedRows[0].Index - 1;
                string temp1 = this.dGVAxisOffset.Rows[preLine].Cells[0].Value.ToString();
                string temp2 = this.dGVAxisOffset.Rows[preLine].Cells[1].Value.ToString();

                this.dGVAxisOffset.Rows[preLine].Cells[0].Value = this.dGVAxisOffset.Rows[line].Cells[0].Value;
                this.dGVAxisOffset.Rows[preLine].Cells[1].Value = this.dGVAxisOffset.Rows[line].Cells[1].Value;

                this.dGVAxisOffset.Rows[line].Cells[0].Value = temp1;
                this.dGVAxisOffset.Rows[line].Cells[1].Value = temp2;
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            int rowIndex = dGVAxisOffset.Rows.Add();
            this.dGVAxisOffset.Rows[rowIndex].Cells[0].Value = "0";
            this.dGVAxisOffset.Rows[rowIndex].Cells[1].Value = "0";
        }

        private void bDown_Click(object sender, EventArgs e)
        {
            if (this.dGVAxisOffset.SelectedRows.Count > 0 && this.dGVAxisOffset.SelectedRows[0].Index > 0)
            {
                int line = this.dGVAxisOffset.SelectedRows[0].Index;
                int preLine = this.dGVAxisOffset.SelectedRows[0].Index + 1;
                string temp1 = this.dGVAxisOffset.Rows[preLine].Cells[0].Value.ToString();
                string temp2 = this.dGVAxisOffset.Rows[preLine].Cells[1].Value.ToString();

                this.dGVAxisOffset.Rows[preLine].Cells[0].Value = this.dGVAxisOffset.Rows[line].Cells[0].Value;
                this.dGVAxisOffset.Rows[preLine].Cells[1].Value = this.dGVAxisOffset.Rows[line].Cells[1].Value;

                this.dGVAxisOffset.Rows[line].Cells[0].Value = temp1;
                this.dGVAxisOffset.Rows[line].Cells[1].Value = temp2;
            }
        }

        private void bCliab_Click(object sender, EventArgs e)
        {
            try
            {
                List<AxisOffsetItem> items = new List<AxisOffsetItem>();
                for (int i = 0; i < this.dGVAxisOffset.Rows.Count; ++i)
                {
                    var item = new AxisOffsetItem();
                    item.MoveDist = double.Parse(this.dGVAxisOffset.Rows[i].Cells[0].Value.ToString());
                    item.Offset = double.Parse(this.dGVAxisOffset.Rows[i].Cells[1].Value.ToString());
                }

                if (!HardwareOrgHelper.Instance.AxisOffset.ContainsKey(this.module))
                    HardwareOrgHelper.Instance.AxisOffset.Add(this.module, new Dictionary<GeneralAxis, List<AxisOffsetItem>>());

                if(!HardwareOrgHelper.Instance.AxisOffset[this.module].ContainsKey(this.axis))
                    HardwareOrgHelper.Instance.AxisOffset[this.module].Add(this.Axis, new List<AxisOffsetItem>());

                HardwareOrgHelper.Instance.AxisOffset[this.module][this.axis] = items;
                HardwareOrgHelper.Save();
                MessageBox.Show("标定成功!");
            }
            catch
            {
                MessageBox.Show("轴机械标定失败!!!");
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            this.dGVAxisOffset.Rows.Clear();
        }
    }
}
