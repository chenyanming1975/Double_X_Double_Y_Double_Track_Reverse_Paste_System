using NationalInstruments.Vision;
using NationalInstruments.Vision.Acquisition.Imaqdx;
using NationalInstruments.Vision.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Config
{
    public class CameraEntiy:IDisposable
    {

        public CameraEntiy()
        {
            UITimer.Interval = 17;
            UITimer.Tick += UITimer_Tick;
        }

        public CameraConfig CameraConfig = null;

        #region 相机的链接 和 设备获取
        /// <summary>
        /// 获得相机 设备列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCameraList()
        {
            List<string> CameraList = new List<string>();
            var cameraList = ImaqdxSystem.GetCameraInformation(true);
            foreach (var camera in cameraList)
            {
                CameraList.Add(camera.Name);
            }

            return CameraList;
        }

        /// <summary>
        /// 依据相机配置链接相机
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool Connectted(CameraConfig config, Module module, Camera cam)
        {
            try
            {
                this.CameraConfig = config;
                if(camera != null)
                {
                    UITimer.Stop();
                    camera.Acquisition.Unconfigure();
                    camera.Dispose();
                }

                camera = new ImaqdxSession(config.Name);
                this.Exposure = config.DefaultExp;
                this.Gain = config.DefaultGain;
                this.Timeout = 1000;
              
                for (int i = 0; i < config.Mat2D.Count; ++i)
                    config.Mat2D[i].LoadCalib(PathDefine.sPathCamera + $"{module}-{cam}-{i}.bmp");

                bOpen = true;
            }
            catch (Exception ex)
            {
                bOpen = false;
                throw new Exception($"相机[{config.Name}]初始化失败!!! 原因[{ex.StackTrace}]");
            }

            return bOpen;
        }

        /// <summary>
        /// 依据设备名称链接相机
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Connectted(string name)
        {
            try
            {
                if (camera != null)
                {
                    UITimer.Stop();
                    camera.Acquisition.Unconfigure();
                    camera.Dispose();
                }

                camera = new ImaqdxSession(name);
                bOpen = true;
            }
            catch (Exception ex)
            {
                bOpen = false;
            }

            return bOpen;
        }
        #endregion

        #region 相机固定属性
        /// <summary>
        /// 曝光
        /// </summary>
        public int Exposure
        {
            get
            {
                return int.Parse(camera.Attributes["CameraAttributes::AcquisitionControl::ExposureTime"].GetValue().ToString());
            }

            set
            {
                camera.Attributes["CameraAttributes::AcquisitionControl::ExposureTime"].SetValue(value);
            }
        }

        /// <summary>
        /// 增益
        /// </summary>
        public int Gain
        {
            get
            {
                return int.Parse(camera.Attributes["CameraAttributes::AnalogControl::Gain"].GetValue().ToString());
            }

            set
            {
                camera.Attributes["CameraAttributes::AnalogControl::Gain"].SetValue(value);
            }
        }

        /// <summary>
        /// 是否外触发
        /// </summary>
        public bool Trriger
        {
            get
            {
                return camera.Attributes["CameraAttributes::AcquisitionControl::TriggerMode"].GetValue().ToString() == "1";
            }

            set
            {
                if (value)
                {
                    camera.Attributes["CameraAttributes::AcquisitionControl::TriggerMode"].SetValue(1);
                    camera.Attributes["CameraAttributes::AcquisitionControl::TriggerSource"].SetValue(1);
                }
                else
                {
                    camera.Attributes["CameraAttributes::AcquisitionControl::TriggerMode"].SetValue(0);
                    camera.Attributes["CameraAttributes::AcquisitionControl::TriggerSource"].SetValue(0);
                }
            }
        }

        /// <summary>
        /// 取图等待时间
        /// </summary>
        public int Timeout
        {
            get
            {
                return int.Parse(this.camera.Attributes["AcquisitionAttributes::Timeout"].GetValue().ToString());
            }

            set
            {
                this.camera.Attributes["AcquisitionAttributes::Timeout"].SetValue(value);
            }
        }

        private ImaqdxSession camera = null;

        /// <summary>
        /// 相机是否打开
        /// </summary>
        public bool bOpen { get; set; } = false;

        /// <summary>
        /// 是否处于抓捕状态
        /// </summary>
        public bool IsGrab { get; set; } = false;

        /// <summary>
        /// 相机采图区域
        /// </summary>
        public Rectangle AOI
        {
            get
            {
                int width = (int)camera.Attributes["CameraAttributes::ImageFormatControl::Width"].GetValue();
                int height = (int)camera.Attributes["CameraAttributes::ImageFormatControl::Height"].GetValue();
                int left = (int)camera.Attributes["CameraAttributes::ImageFormatControl::OffsetX"].GetValue();
                int top = (int)camera.Attributes["CameraAttributes::ImageFormatControl::OffsetY"].GetValue();

                return new Rectangle(left, top, width, height);
            }

            set
            {
                camera.Attributes["CameraAttributes::ImageFormatControl::Width"].SetValue(value.Width);
                camera.Attributes["CameraAttributes::ImageFormatControl::Height"].SetValue(value.Height);
                camera.Attributes["CameraAttributes::ImageFormatControl::OffsetX"].SetValue(value.Left);
                camera.Attributes["CameraAttributes::ImageFormatControl::OffsetY"].SetValue(value.Top);
            }
        }
        #endregion

        #region UI采图
    
        private ImageViewer viewer = null;

        /// <summary>
        /// UI采图刷新定时器
        /// </summary>
        public System.Windows.Forms.Timer UITimer = new System.Windows.Forms.Timer();

        private void UITimer_Tick(object sender, EventArgs e)
        {
            if (bOpen && this.viewer != null)
            {
                try
                {
                    this.camera.Grab(this.viewer.Image, true);

                    if (ShowCross)
                    {
                        this.viewer.Image.Overlays.Default.AddLine(new LineContour(new PointContour(this.viewer.Image.Width / 2, 0),
                        new PointContour(this.viewer.Image.Width / 2, this.viewer.Image.Height)), Rgb32Value.RedColor);
                        this.viewer.Image.Overlays.Default.AddLine(new LineContour(new PointContour(0, this.viewer.Image.Height / 2),
                        new PointContour(this.viewer.Image.Width, this.viewer.Image.Height / 2)), Rgb32Value.RedColor);
                    }
                }
                catch { }
            }
            else
            {
                UITimer.Stop();
            }
        }

        public bool ShowCross = false;

        /// <summary>
        /// 开始UI采图
        /// </summary>
        /// <param name="imageViewer"></param>
        public void UIStartGrab(ImageViewer imageViewer = null)
        {
            if (bOpen && !this.IsGrab)
            {
                this.viewer = imageViewer;
                this.camera.Acquisition.Unconfigure();
                this.camera.ConfigureGrab();
                IsGrab = true;
                if (imageViewer != null)
                    UITimer.Start();
            }
        }

        /// <summary>
        /// UI采图停止
        /// </summary>
        public void UIStopGrab()
        {
            if (this.bOpen)
            {
                this.UITimer.Stop();
                Thread.Sleep(100);
                this.camera.Acquisition.Unconfigure();
                this.IsGrab = false;
            }
        }

        public void UISnap()
        {
            if(this.bOpen && !this.IsGrab && this.viewer != null)
            {
                this.camera.Snap(this.viewer.Image);
            }
        }
        #endregion

        #region 点拍采图
        public void StartGrab()
        {
            try
            {
                if (bOpen && !this.IsGrab)
                {
                    this.camera.Acquisition.Unconfigure();
                    this.camera.ConfigureGrab();
                    IsGrab = true;
                }
            }
            catch { }
        }

        public void StopGrab()
        {
            try
            {
                if (bOpen)
                {
                    this.UITimer.Stop();
                    this.camera.Acquisition.Unconfigure();
                    IsGrab = false;
                }
            }
            catch { }
        }

        public VisionImage Snap()
        {
            VisionImage image = new VisionImage();
            if (this.bOpen && !this.IsGrab)
            {
                this.camera.Snap(image);
            }

            return image;
        }

        public VisionImage Grab()
        {
            VisionImage image = new VisionImage();
            if (this.bOpen && this.IsGrab)
            {
                try
                {
                    this.camera.Grab(image, true);
                }
                catch { }
            }
            return image;
        }
        #endregion

        #region 飞拍采图
        public List<VisionImage> GrabList = new List<VisionImage>();

        public void ClearFlyImage()
        {
            foreach (VisionImage image in GrabList)
            {
                try
                {
                    image?.Dispose();
                }
                catch { }
            }
        }
        public void StartFlyGrab()
        {
            if(this.bOpen && !this.IsGrab)
            {
                this.ClearFlyImage();
                this.GrabList.Clear();
                this.Trriger = true;
                this.camera.Acquisition.Unconfigure();
                this.camera.ConfigureGrab();
                this.IsGrab = true;
                new Thread(GrabFlyImage).Start();
            }
        }

        public void StopFlyGrab()
        {
            if (this.bOpen)
            {
                this.IsGrab = false;
                this.Trriger = false;
                this.camera.Acquisition.Unconfigure();
            }
        }

        private void GrabFlyImage()
        {
            while(this.IsGrab && this.bOpen)
            {
                try
                {
                    VisionImage image = this.camera.Grab(null, true);
                    this.GrabList.Add(image);
                }
                catch { }
            }
        }
        #endregion

        public void Dispose()
        {
            this.bOpen = false;
            this.camera?.Acquisition.Unconfigure();
            this.camera?.Dispose();
        }
    }
}
