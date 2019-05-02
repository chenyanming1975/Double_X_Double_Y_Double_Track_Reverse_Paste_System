using System;
using System.Diagnostics;
using System.IO.Ports;

namespace GeneralMachine.Common
{
    public class SerialPortHelper
    {
        public SerialPortHelper(SerialPortEntity entity)
        {
            if (entity == null)
            {
                this.portEntity = new SerialPortEntity();
            }
            else
            {
                this.PortEntity = entity;
            }
        }


        /// <summary>
        /// 串口工具配置路径
        /// </summary>
        private string configPath = string.Empty;

        /// <summary>
        /// 串口调试配置
        /// </summary>
        private SerialPortEntity portEntity = new SerialPortEntity();

        /// <summary>
        /// 串口调试配置
        /// </summary>
        private SerialPort serialPort = new SerialPort();

        /// <summary>
        /// 是否收到数据
        /// </summary>
        private bool isDataReceived = false;

        /// <summary>
        /// 串口调试配置
        /// </summary>
        public SerialPortEntity PortEntity
        {
            get
            {
                return this.portEntity;
            }

            set
            {
                this.portEntity = value;
                this.SetPortConfig();
            }
        }

        /// <summary>
        /// 是否收到数据
        /// </summary>
        public bool IsDataReceived
        {
            get
            {
                return this.isDataReceived;
            }

            set
            {
                this.isDataReceived = value;
            }
        }

        public bool IsOpen
        {
            get
            {
                return this.serialPort.IsOpen;
            }
        }

        /// <summary>
        /// 接收数据处理方法
        /// </summary>
        public Action<string> ReveviceDataHanlder = null;

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>是否发送成功</returns>
        public bool SendData(string data)
        {
            bool result = false;
            if (this.serialPort.IsOpen)
            {
                this.IsDataReceived = false;
                this.serialPort.Write(data);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>是否发送成功</returns>
        public bool SendData(byte[] data)
        {
            bool result = false;
            if (this.serialPort.IsOpen)
            {
                this.IsDataReceived = false;
                this.serialPort.Write(data, 0, data.Length);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 串口接收到数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.IsDataReceived = false;
            if (this.serialPort.IsOpen)
            {
                string data = this.serialPort.ReadExisting();
                if (this.ReveviceDataHanlder != null)
                {
                    this.ReveviceDataHanlder(data);
                }

                Debug.WriteLine($"-----Received{data}");
            }

            this.IsDataReceived = true;
        }

        /// <summary>
        /// 设置串口配置
        /// </summary>
        /// <param name="entity"></param>
        private void SetPortConfig()
        {
            if (this.PortEntity != null)
            {
                if (this.serialPort.IsOpen)
                {
                    this.serialPort.Close();
                }

                if (!string.IsNullOrEmpty(this.PortEntity.PortName))
                {
                    this.serialPort.PortName = this.PortEntity.PortName;
                    this.serialPort.BaudRate = this.PortEntity.BaudRate;
                    this.serialPort.StopBits = (StopBits)this.PortEntity.StopBit;
                    this.serialPort.DataBits = this.PortEntity.DataBit;
                    this.serialPort.Parity = (Parity)this.PortEntity.CorrectBit;
                    this.serialPort.DtrEnable = this.PortEntity.EnableDTR;
                    this.serialPort.RtsEnable = this.PortEntity.EnableRTS;
                }
            }
        }

        /// <summary>
        /// 重连串口
        /// </summary>
        /// <returns></returns>
        public bool ReConnection()
        {
            try
            {
                if (this.serialPort.IsOpen)
                {
                    this.serialPort.Close();
                }

                this.serialPort.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return this.serialPort.IsOpen;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public void SaveConfig()
        {
            SerializableHelper<SerialPortEntity> serialize = new SerializableHelper<SerialPortEntity>(this.PortEntity);
            serialize.XMLSerialize(this.configPath);
        }
    }
}