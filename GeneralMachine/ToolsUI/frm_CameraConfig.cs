using GeneralMachine.Config;
using NationalInstruments.Vision.WindowsForms;
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
    public partial class frm_CameraConfig : Form
    {
        public frm_CameraConfig()
        {
            InitializeComponent();
            this.moduleRadio1.ModuleChange += (sender, module) =>
            {
                this.module = module;
                this.propertyGrid1.SelectedObject = CameraDefine.Instance.CameraList[this.module][this.camera];
            };

            this.cameraRatio1.CameraChanged += (sender, camera) =>
            {
                this.camera = camera;
                this.propertyGrid1.SelectedObject = CameraDefine.Instance.CameraList[this.module][this.camera];
            };
        }

        private Module module = Module.Front;
        private Camera camera = Camera.Top;

        private void frm_CameraConfig_Load(object sender, EventArgs e)
        {
            this.cb_CamList.Items.AddRange(CameraEntiy.GetCameraList().ToArray());
            imageSet.ShowToolbar = true;
            imageSet.ToolsShown = (ViewerTools)((int)(ViewerTools.Rectangle) + (int)(ViewerTools.ZoomIn) + (int)(ViewerTools.ZoomOut) + (int)(ViewerTools.Selection) + (int)(ViewerTools.Pan) + (int)(ViewerTools.Line));
            imageSet.ZoomToFit = true;
            imageSet.Image.BorderWidth = 0;
            this.propertyGrid1.SelectedObject = CameraDefine.Instance.CameraList[this.module][camera];
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            CameraDefine.Save();
        }

        private CameraEntiy entiy = null;
        private void OpenCam_Click(object sender, EventArgs e)
        {
            if (entiy == null || !entiy.bOpen)
            {
                entiy = new CameraEntiy();

                entiy.Connectted(this.cb_CamList.Text);
            }

            if(!entiy.IsGrab && entiy.bOpen)
            {
                entiy.UIStartGrab(this.imageSet);
                this.bOpenCam.Text = "关闭";
            }
            else
            {
                entiy.StopGrab();
                entiy.Dispose();
                this.bOpenCam.Text = "实时";
            }
        }

        private void OpenRed_Click(object sender, EventArgs e)
        {
            Random a = new Random();
            CameraDefine.Instance.Light(this.module, this.camera, new LightParam { bRed = true, R_Value = a.Next(255) });
        }

        private void OpenGreen_Click(object sender, EventArgs e)
        {
            Random a = new Random();
            CameraDefine.Instance.Light(this.module, this.camera, new LightParam { bGreen = true, G_Value = a.Next(255) });
        }

        private void OpenBlue_Click(object sender, EventArgs e)
        {
            Random a = new Random();
            CameraDefine.Instance.Light(this.module, this.camera, new LightParam { bBlue = true, B_Value = a.Next(255) });
        }

        private void bLinkLight_Click(object sender, EventArgs e)
        {
            if (this.bLinkLight.Text.Contains("链接"))
            {
                CameraDefine.Instance.OpenLightDev(this.module, this.camera);
                this.bLinkLight.Text = "光闭光源";
            }
            else
            {
                CameraDefine.Instance.CloseLightDev(this.module, this.camera);
                this.bLinkLight.Text = "链接光源";
            }
        }
    }
}
