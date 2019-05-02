using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Common;
using GeneralMachine.Config;

namespace GeneralMachine.Press
{
    /// <summary>
    /// 压力感应器
    /// </summary>
    public class PressSensor
    {
        #region 需要保存变量
        public string SensorIP
        {
            get;
            set;
        } = "192.168.1.30";

        public int SensorProt
        {
            get;
            set;
        } = 8088;

        /// <summary>
        /// 压力报警记录保存行数
        /// </summary>
        public int RecrodRowCount
        {
            get;
            set;
        } = 50;

        /// <summary>
        /// 固有压力
        /// </summary>
        public double[] NozzlePress = new double[4];

        /// <summary>
        /// 累计超压报警
        /// </summary>
        public int AlarmCount
        {
            get;
            set;
        } = 5;

        /// <summary>
        /// 报警g数
        /// </summary>
        public double AlarmLimit
        {
            get;
            set;
        } = 400;

        public int[] ZChannel = { 0, 1, 2, 3 };
        #endregion
        #region 中间变量
        [JsonIgnore]
        public Socket Socket = null;

        /// <summary>
        /// 实时压力
        /// </summary>
        [JsonIgnore]
        public double[] CurPress = new double[4];

        /// <summary>
        /// 贴附过程中压力
        /// </summary>
        [JsonIgnore]
        public double[] PastePress = new double[4];

        [JsonIgnore]
        private object sendLock = new object();

        [JsonIgnore]
        public bool NeedConnected = true;

        /// <summary>
        /// 是否处于标定阶段
        /// </summary>
        [JsonIgnore]
        public bool IsCailb = false;

        /// <summary>
        /// 是否开始采集压力
        /// </summary>
        [JsonIgnore]
        public bool GrabPressValue = false;

        /// <summary>
        /// 是否开始采集
        /// </summary>
        [JsonIgnore]
        public bool IsStarted = false;
        #endregion
        #region 网络连接管理
        /// <summary>
        /// 重连接
        /// </summary>
        /// <returns></returns>
        public bool ReConnected()
        {
            try
            {
                lock (this.sendLock)
                {
                    IPAddress iPAddress = IPAddress.Parse(this.SensorIP);
                    IPEndPoint iPEnd = new IPEndPoint(iPAddress, this.SensorProt);
                    this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    this.Socket.ReceiveTimeout = 200;
                    this.Socket.Connect(iPEnd);
                    Thread.Sleep(100);
                    return true;
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnected()
        {
            if (this.Socket != null && this.Socket.Connected)
            {
                lock (this.sendLock)
                {
                    try
                    {
                        this.Socket.Disconnect(false);
                        Thread.Sleep(100);
                        this.Socket.Dispose();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            if (this.Socket == null)
            {
                new Thread(ThreadRecive).Start();
            }
        }
        #endregion
        #region 压力传感器通讯
        /// <summary>
        /// 查询1-12通道的 测量值
        /// </summary>
        public void SendGet()
        {
            if (this.Socket != null && this.Socket.Connected)
            {
                try
                {
                    // CRC 校验需要改
                    byte[] sendByte = new byte[8];
                    sendByte[0] = 0X01;
                    sendByte[1] = 0X03;
                    sendByte[2] = 0X00;
                    sendByte[3] = 0X00;
                    sendByte[4] = 0X00;
                    sendByte[5] = 0X18;
                    sendByte[6] = 0X45;
                    sendByte[7] = 0XC0;

                    lock (this.sendLock)
                    {
                        this.Socket.Send(sendByte);
                    }
                }
                catch { }
            }
        }

        public void SendZero(int channel)
        {
            if (this.Socket == null || !this.Socket.Connected)
                return;

            byte[] sendByte = new byte[9];
            sendByte[0] = 0X01;
            sendByte[1] = 0X10;
            sendByte[2] = 0X00;
            sendByte[3] = 0X18;
            sendByte[4] = 0X00;
            sendByte[5] = 0X18;

            for (int i = 0; i < 12; i += 4)
            {
                if (channel == i)
                {
                    sendByte[i] = 00;
                    sendByte[i + 1] = 00;
                    sendByte[i + 2] = 00;
                    sendByte[i + 3] = 01;
                }
                else
                {
                    sendByte[i] = 00;
                    sendByte[i + 1] = 00;
                    sendByte[i + 2] = 00;
                    sendByte[i + 3] = 00;
                }
            }

            sendByte[54] = 0X50;
            sendByte[55] = 0X5C;

            lock (this.sendLock)
            {
                this.Socket.Send(sendByte);
            }
        }

        public void SendZeroAll()
        {
            if (this.Socket == null || !this.Socket.Connected)
                return;

            byte[] sendByte = new byte[9];
            sendByte[0] = 0X01;
            sendByte[1] = 0X11;
            sendByte[2] = 0X00;
            sendByte[3] = 0XC8;
            sendByte[4] = 0X00;
            sendByte[5] = 0X01;
            sendByte[6] = 0X02;
            sendByte[7] = 0XB6;
            sendByte[8] = 0XB0;
            lock (this.sendLock)
            {
                this.Socket.Send(sendByte);
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 获得 压力值并解析
        /// </summary>
        /// <returns></returns>
        public void ParseValue()
        {
            try
            {
                byte[] recBytes = new byte[256];
                int bytes = this.Socket.Receive(recBytes, recBytes.Length, 0);
                if (recBytes[0] == 0x01 && recBytes[1] == 0x03)
                {
                    int count = 0;
                    double[] Temp = new double[12];
                    for (int i = 3; i < 19; i += 4)
                    {
                        int r3 = 0;
                        int r4 = 0;
                        int IsPos = 1;
                        if (recBytes[i] == 0xFF) // 负数
                        {
                            IsPos = -1;
                            recBytes[i] ^= 0xFF;
                            recBytes[i + 1] ^= 0xFF;
                            recBytes[i + 2] ^= 0xFF;
                            recBytes[i + 3] ^= 0xFF;
                            recBytes[i + 3] += 0x01;
                        }

                        r3 = recBytes[i + 2];
                        r4 = recBytes[i + 3];

                        Temp[count] = IsPos * (r3 * 255 + r4) / 10.0;
                        count++;
                    }

                    for (int i = 0; i < 4; ++i)
                    {
                        this.CurPress[i] = Temp[this.ZChannel[i]];

                        if (this.GrabPressValue)
                        {
                            if ((this.CurPress[i] - this.PastePress[i]) > 0.2)
                            {
                                this.PastePress[i] = this.CurPress[i];
                            }
                        }
                        else if (IsCailb)
                        {
                            if ((this.CurPress[i] - this.PastePress[i]) > 0.2)
                            {
                                this.PastePress[i] = this.CurPress[i];
                            }
                        }
                        else
                            this.PastePress[i] = this.CurPress[i];
                    }
                }
            }
            catch
            {
                this.DisConnected();
            }
        }

        public void ClearPastePress(int zIndex)
        {
            this.PastePress[zIndex] = 0;
        }

        public void ClearAllPastePress()
        {
            for (int i = 0; i < 4; ++i)
            {
                this.ClearPastePress(i);
            }
        }
        public void ThreadRecive()
        {
            while (this.IsStarted)
            {
                Thread.Sleep(15);

                if (this.NeedConnected && (this.Socket == null || !this.Socket.Connected))
                {
                    this.DisConnected();
                    this.ReConnected();
                }

                this.SendGet();
                ParseValue();
            }
        }
        #endregion
    }


    /// <summary>
    /// 压力传感器模块
    /// </summary>
    public class PressHelper:SingletionProvider<PressHelper>
    {
        /// <summary>
        /// 压力传感器
        /// </summary>
        public Dictionary<Module, PressSensor> Sensors = new Dictionary<Module, PressSensor>();

        #region 配置导入和导出
        public static void Load()
        {
            SerializableHelper<PressHelper> helper = new SerializableHelper<PressHelper>();
            PressHelper.Instance = helper.DeJsonSerialize(PathDefine.sPressConfigure + "PressConfig.json");
            if (PressHelper.Instance == null)
                PressHelper.Instance = new PressHelper();

            if(PressHelper.Instance.Sensors.Count == 0)
            {
                PressHelper.Instance.Sensors.Add(Module.Front, new PressSensor());
                PressHelper.Instance.Sensors.Add(Module.After, new PressSensor());
            }
        }

        public static void Save()
        {
            SerializableHelper<PressHelper> helper = new SerializableHelper<PressHelper>(PressHelper.Instance);
            helper.JsonSerialize(PathDefine.sPressConfigure + "PressConfig.json");
        }
        #endregion
    }
}
