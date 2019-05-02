using GeneralMachine.Vision;
using NationalInstruments.Vision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.MES
{
    public partial class MFlexMesControl : Form
    {
        public MFlexMesControl()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Dictionary<int, bool> badList = new Dictionary<int, bool>();
            var item = MFlexHelper.Instance.CheckBaseInfo(this.tbBarcode.Text, out badList);

            if (item.Item1)
            {
                this.lblResult.BackColor = Color.Transparent;
                this.lblResult.Text = item.Item2;
            }
            else
            {
                this.lblResult.Text = item.Item2;
                this.lblResult.BackColor = Color.LightCoral;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.UIToConfig();
        }

        private void MFlexMesControl_Load(object sender, EventArgs e)
        {
            this.ConfigToUI();
            this.cbCodeList.Items.AddRange(VisionToolCtrl.GetVisionList());
        }

        private void ConfigToUI()
        {
            MFlexHelper.Load();
            this.tbMachineName.Text = MFlexHelper.Instance.MachineName;
            this.tbMachineLine.Text = MFlexHelper.Instance.LineNo;
            this.tbMachineSite.Text = MFlexHelper.Instance.Site;
            this.tbOperatorName.Text = MFlexHelper.Instance.OperName;
            this.tbMachineSide.Text = MFlexHelper.Instance.WorkArea;
            this.cbEnable.Checked = MFlexHelper.Instance.EnableMES;
            this.cbCodeList.Text = MFlexHelper.Instance.CodeFunc;
            this.tbMac.Text = MFlexHelper.Instance.Mac;
            this.tbProgramName.Text = MFlexHelper.Instance.ProgramName;
        }

        private void UIToConfig()
        {
            MFlexHelper.Instance.MachineName = this.tbMachineName.Text;
            MFlexHelper.Instance.LineNo = this.tbMachineLine.Text;
            MFlexHelper.Instance.Site = this.tbMachineSite.Text;
            MFlexHelper.Instance.OperName = this.tbOperatorName.Text;
            MFlexHelper.Instance.WorkArea = this.tbMachineSide.Text;
            MFlexHelper.Instance.EnableMES = this.cbEnable.Checked;
            MFlexHelper.Instance.Mac = this.tbMac.Text;
            MFlexHelper.Instance.CodeFunc = this.cbCodeList.Text;
            MFlexHelper.Instance.ProgramName = this.tbProgramName.Text;
            MFlexHelper.Save();
        }

        private void bRead_Click(object sender, EventArgs e)
        {
            try
            {
               if(this.cbCodeList.Text != string.Empty)
                {
                    if(GetImage == null || GetRoi == null)
                    {
                        MessageBox.Show("请先打开相机界面!!!");
                           return;
                    }
                    using (VisionFlow flow = VisionFlow.Load(VisionToolCtrl.sPathVision + this.cbCodeList.Text))
                    {
                        var image = GetImage?.Invoke();
                        var roi = GetRoi?.Invoke();
                        if(roi == null||roi.Count ==0 || !(roi[0].Shape is RectangleContour))
                        {
                            MessageBox.Show("请在相机界面中绘制读取框!!!");
                            return;
                        }
                        var rtn = flow.Detect(image, roi[0].Shape);
                        this.tbBarcode.Text = rtn.BarCode;
                    }
                }
            }
            catch
            {
                MessageBox.Show("读取失败");
            }
        }

        public static Func<VisionImage> GetImage;
        public static Func<Roi> GetRoi;
    }
}
