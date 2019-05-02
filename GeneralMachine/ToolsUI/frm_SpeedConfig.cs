using GeneralMachine.Config;
using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.SpeedManager
{
    public partial class frm_SpeedConfig : Form
    {
        public frm_SpeedConfig()
        {
            InitializeComponent();
            foreach(Shceme sh in Enum.GetValues(typeof(Shceme)))
            {
                this.cb_Select.Items.Add(Common.CommonHelper.GetEnumDescription(typeof(Shceme), sh));
            }

            foreach (OtherShceme sh in Enum.GetValues(typeof(OtherShceme)))
            {
                this.cb_SelectOther.Items.Add(Common.CommonHelper.GetEnumDescription(typeof(OtherShceme), sh));
            }

            this.moduleRadio1.ModuleChange += (sender, module) =>
            {
                this.selectModule = module;
                this.config = SpeedDefine.Instance[module];
                UpdateToUI();
            };

            this.cb_Select.SelectedIndex = 0;
            this.cb_SelectOther.SelectedIndex = 0;
        }

        private Module selectModule = Module.Front;

        /// <summary>
        /// 当前选择的速度方案
        /// </summary>
        private Shceme selectShceme = Shceme.MaunalSlow;

        /// <summary>
        /// 当前选择的速度方案
        /// </summary>
        private OtherShceme selectOtherShceme = OtherShceme.UpFly;

        private SpeedConfig config = new SpeedConfig();

        /// <summary>
        /// 速度配置
        /// </summary>
        public SpeedConfig Config
        {
            get
            {
                return this.config;
            }

            set
            {
                if(value != null)
                {
                    config = value;
                    UpdateToUI();
                }
            }
        }

        public void UpdateToUI()
        {
            if(config != null)
            {
                this.xSpeed.HostarSpeed = this.config.Scheme[this.selectShceme][GeneralAxis.X];
                this.ySpeed.HostarSpeed = this.config.Scheme[this.selectShceme][GeneralAxis.Y];
                this.zSpeed.HostarSpeed = this.config.Scheme[this.selectShceme][GeneralAxis.Z];
                this.uSpeed.HostarSpeed = this.config.Scheme[this.selectShceme][GeneralAxis.U];
                this.trunSpeed.HostarSpeed = this.config.Scheme[this.selectShceme][GeneralAxis.TRUN];
            }
        }

        public bool UIToData()
        {
            try
            {
                if (config != null)
                {
                    this.config.Scheme[this.selectShceme][GeneralAxis.X] = this.xSpeed.HostarSpeed;
                    this.config.Scheme[this.selectShceme][GeneralAxis.Y] = this.ySpeed.HostarSpeed;
                    this.config.Scheme[this.selectShceme][GeneralAxis.Z] = this.zSpeed.HostarSpeed;
                    this.config.Scheme[this.selectShceme][GeneralAxis.U] = this.uSpeed.HostarSpeed;
                    this.config.Scheme[this.selectShceme][GeneralAxis.TRUN] = this.trunSpeed.HostarSpeed;
                    return true;
                }

            }
            catch { }
            return false;
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            UIToData();
            SpeedDefine.Instance[this.selectModule] = this.config;
            SpeedDefine.Save();
        }

        private void cb_Select_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectShceme = (Shceme)this.cb_Select.SelectedIndex;
            UpdateToUI();
        }

        private void cb_SelectOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectOtherShceme = (OtherShceme)this.cb_SelectOther.SelectedIndex;
            UpdateToUI();
        }

        private void frm_SpeedConfig_Load(object sender, EventArgs e)
        {
            this.config = SpeedDefine.Instance[this.selectModule];
            this.UpdateToUI();
        }

        #region 分段加速
        private void bAdd_Click(object sender, EventArgs e)
        {
        }

        private void bDelete_Click(object sender, EventArgs e)
        {

        }

        private void bUp_Click(object sender, EventArgs e)
        {

        }

        private void bDown_Click(object sender, EventArgs e)
        {

        }

        private void bSave_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
