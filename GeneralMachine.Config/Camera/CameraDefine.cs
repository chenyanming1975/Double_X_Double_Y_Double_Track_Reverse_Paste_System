using GeneralMachine.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    public struct LightParam
    {
        public bool bRed;
        public bool bGreen;
        public bool bBlue;
        public int R_Value;
        public int G_Value;
        public int B_Value;
    }

    /// <summary>
    /// 该机器相机设备集合
    /// </summary>
    public class CameraDefine : SingletionProvider<CameraDefine>
    {
        public void Init()
        {
            if (CameraList.Count == 0)
            {
                foreach (Module module in Enum.GetValues(typeof(Module)))
                {
                    CameraList.Add(module, new Dictionary<Camera, CameraConfig>());
                    foreach (Camera camera in Enum.GetValues(typeof(Camera)))
                    {
                        CameraList[module].Add(camera, new CameraConfig());
                        if (CameraList[module][camera].Mat2D.Count == 0)
                        {
                            if (camera == Config.Camera.Top || camera == Config.Camera.Label)
                                CameraList[module][camera].Mat2D.Add(new Vision.HalCali());
                            else
                            {
                                CameraList[module][camera].Mat2D.Add(new Vision.HalCali());
                                CameraList[module][camera].Mat2D.Add(new Vision.HalCali());
                            }
                        }
                    }
                }
            }
        }

        public Dictionary<Module, Dictionary<Camera, CameraConfig>> CameraList = new Dictionary<Module, Dictionary<Camera, CameraConfig>>();

        [JsonIgnore]
        public Dictionary<Module, Dictionary<Camera, CameraEntiy>> Camera = new Dictionary<Module, Dictionary<Camera, CameraEntiy>>();

        [JsonIgnore]
        /// <summary>
        /// 光源列表
        /// </summary>
        public Dictionary<string, SerialPort> LightPort = new Dictionary<string, SerialPort>();

        [JsonIgnore]
        public Dictionary<Module, Dictionary<Camera, LightParam>> LightPlan = new Dictionary<Module, Dictionary<Camera, LightParam>>();

        /// <summary>
        /// 初始化相机
        /// </summary>
        /// <returns></returns>
        public string CameraConnected()
        {
            string msg = string.Empty;
            LightPlan.Clear();
            foreach (Module module in Enum.GetValues(typeof(Module)))
            {
                foreach (Camera camera in Enum.GetValues(typeof(Camera)))
                {
                    CameraEntiy entiy = new CameraEntiy();

                    try
                    {
                        if(CameraList[module][camera].Mat2D.Count == 0)
                        {
                            if (camera == Config.Camera.Top || camera == Config.Camera.Label)
                                CameraList[module][camera].Mat2D.Add(new Vision.HalCali());
                            else
                            {
                                CameraList[module][camera].Mat2D.Add(new Vision.HalCali());
                                CameraList[module][camera].Mat2D.Add(new Vision.HalCali());
                            }
                        }

                        entiy.Connectted(CameraList[module][camera], module, camera);
                    }

                    catch (Exception ex) { msg += (ex.Message + "\n"); }

                    if (!Camera.ContainsKey(module))
                    {
                        Camera.Add(module, new Dictionary<Config.Camera, CameraEntiy>());
                        LightPlan.Add(module, new Dictionary<Config.Camera, LightParam>());
                    }

                    if (!Camera[module].ContainsKey(camera))
                    {
                        Camera[module].Add(camera, entiy);
                        LightPlan[module].Add(camera, new LightParam());
                    }

                    this.OpenLightDev(module, camera);
                }
            }

            return msg;
        }

        public void CameraDisConnected()
        {
            foreach (Module module in Enum.GetValues(typeof(Module)))
            {
                foreach (Camera camera in Enum.GetValues(typeof(Camera)))
                {
                    this.CloseLightDev(module, camera);
                }
            }
        }


        #region 光源相关

        /// <summary>
        /// 打开光源设备
        /// </summary>
        public void OpenLightDev(Module module, Camera camera)
        {
          
            if (!LightPort.ContainsKey(this.CameraList[module][camera].ProtName))
            {
                try
                {
                    SerialPort port = new SerialPort(this.CameraList[module][camera].ProtName, this.CameraList[module][camera].BaudRate);
                    port.Open();
                    if (port.IsOpen)
                    {
                        LightPort.Add(this.CameraList[module][camera].ProtName, port);
                    }

                    this.OpreatorChannel(module, camera, true);
                }
                catch { }
            }
        }

        /// <summary>
        /// 光闭光源设备
        /// </summary>
        public void CloseLightDev(Module module, Camera camera)
        {
            if (LightPort.ContainsKey(this.CameraList[module][camera].ProtName))
            {
                try
                {
                    this.OpreatorChannel(module, camera, false);
                    LightPort[this.CameraList[module][camera].ProtName].Close();
                    LightPort[this.CameraList[module][camera].ProtName].Dispose();
                    LightPort.Remove(this.CameraList[module][camera].ProtName);
                }
                catch { }
            }
        }

        /// <summary>
        /// 激活通道
        /// </summary>
        /// <param name="module"></param>
        /// <param name="camera"></param>
        /// <param name="open"></param>
        private void OpreatorChannel(Module module, Camera camera, bool open = true)
        {
            for (byte i = 0x01; i <= 0x04; ++i)
            {
                byte[] senByte = new byte[8];
                senByte[0] = 0x2;
                senByte[1] = 0x4D;
                senByte[2] = 0x30;
                senByte[3] = 0x30;
                senByte[4] = (byte)(0x30 + i);
                if (open)
                    senByte[5] = 0x31;
                else
                    senByte[5] = 0x30;

                senByte[6] = 0x0D;
                senByte[7] = 0x00;
                this.LightPort[this.CameraList[module][camera].ProtName].Write(senByte, 0, senByte.Length);
            }
        }

        /// <summary>
        /// 设置通道值
        /// </summary>
        /// <param name="module"></param>
        /// <param name="camera"></param>
        /// <param name="channel"></param>
        /// <param name="value"></param>
        private void SetChannelValue(Module module, Camera camera, LightChannel channel, int value)
        {
            byte value1 = (byte)(value / 100);
            byte value2 = (byte)(value % 100 / 10);
            byte value3 = (byte)(value % 10);
            byte[] senByte = new byte[10];
            senByte[0] = 0x02;
            senByte[1] = 0x4C;
            senByte[2] = 0x30;
            senByte[3] = 0x30;
            senByte[4] = (byte)(0x30 + channel);
            senByte[5] = value1;//Byte.Parse(Encoding.GetEncoding("Unicode")(new char[] { char.Parse(BAI.ToString()) })[0].ToString());
            senByte[6] = value2;//Byte.Parse(Encoding.GetEncoding("Unicode").GetBytes(new char[] { char.Parse(SHI.ToString()) })[0].ToString());
            senByte[7] = value3;//Byte.Parse(Encoding.GetEncoding("Unicode").GetBytes(new char[] { char.Parse(GE.ToString()) })[0].ToString());
            senByte[8] = 0x0D;
            senByte[9] = 0x00;
            this.LightPort[this.CameraList[module][camera].ProtName].Write(senByte, 0, senByte.Length);
        }

        public void CloseLight(Module module, Camera camera)
        {
            if (camera == Config.Camera.Top)
            {
                IODefine.Instance.MachineIO[module].UpLight.SetIO(false);
            }
            else if (camera == Config.Camera.Bottom1 || camera == Config.Camera.Bottom2)
            {
                IODefine.Instance.MachineIO[module].DownLightRed.SetIO(false);
                IODefine.Instance.MachineIO[module].DownLightGreen.SetIO(false);
                IODefine.Instance.MachineIO[module].DownLightBlue.SetIO(false);
            }
        }

        /// <summary>
        /// 对外接口
        /// </summary>
        /// <param name="module"></param>
        /// <param name="camera"></param>
        /// <param name="Param"></param>
        public void Light(Module module, Camera camera, LightParam Param)
        {
            if (!this.LightPort.ContainsKey(this.CameraList[module][camera].ProtName))
            {
                return;
            }

            if (camera == Config.Camera.Top)
            {
                IODefine.Instance.MachineIO[module].UpLight.SetIO(Param.bRed);
            }
            else if (camera == Config.Camera.Bottom1 || camera == Config.Camera.Bottom2)
            {
                IODefine.Instance.MachineIO[module].DownLightRed.SetIO(Param.bRed);
                IODefine.Instance.MachineIO[module].DownLightGreen.SetIO(Param.bGreen);
                IODefine.Instance.MachineIO[module].DownLightBlue.SetIO(Param.bBlue);
            }

            if (Param.bRed)
            {
                this.SetChannelValue(module, camera, this.CameraList[module][camera].RedChannel, Param.R_Value);
                Thread.Sleep(30);
            }
            if (Param.bGreen)
            {
                this.SetChannelValue(module, camera, this.CameraList[module][camera].GreenChannel, Param.G_Value);
                Thread.Sleep(30);
            }
            if (Param.bBlue)
            {
                this.SetChannelValue(module, camera, this.CameraList[module][camera].BlueChannel, Param.B_Value);
            }
        }
        #endregion

        public static void Save()
        {
            SerializableHelper<CameraDefine> helper = new SerializableHelper<CameraDefine>(CameraDefine.Instance);
            helper.JsonSerialize(PathDefine.sPathCamera + "Camera.json");
        }

        public static bool Load()
        {
            if(!File.Exists(PathDefine.sPathCamera + "Camera.json"))
            {
                CameraDefine.Instance.Init();
                return true;
            }

            SerializableHelper<CameraDefine> helper = new SerializableHelper<CameraDefine>();
            var temp = helper.DeJsonSerialize(PathDefine.sPathCamera+"Camera.json");
            if (temp != null)
                CameraDefine.Instance = temp;
            else
                return false;
            return true;
        }
    }
}
