using System;
using System.Linq;

namespace GeneralMachine.Common
{
    public class SerialPortEntity
    {
        public SerialPortEntity(string iniValue)
        {
            this.SetEntity(iniValue);
        }

        public SerialPortEntity()
        {
        }

        public static implicit operator SerialPortEntity(string iniValue)
        {
            return new SerialPortEntity(iniValue);
        }

        public static implicit operator string (SerialPortEntity speed)
        {
            return speed.ToString();
        }

        private string portName = string.Empty;

        private int baudRate = 9600;

        private int stopBit = 1;

        private int dataBit = 8;

        private int correctBit = 0;

        private bool enableDTR = false;

        private bool enableRTS = false;

        public string PortName
        {
            get
            {
                return portName;
            }

            set
            {
                portName = value;
            }
        }

        public int BaudRate
        {
            get
            {
                return baudRate;
            }

            set
            {
                baudRate = value;
            }
        }

        public int StopBit
        {
            get
            {
                return stopBit;
            }

            set
            {
                stopBit = value;
            }
        }

        public int DataBit
        {
            get
            {
                return dataBit;
            }

            set
            {
                dataBit = value;
            }
        }

        public int CorrectBit
        {
            get
            {
                return correctBit;
            }

            set
            {
                correctBit = value;
            }
        }

        public bool EnableDTR
        {
            get
            {
                return enableDTR;
            }

            set
            {
                enableDTR = value;
            }
        }

        public bool EnableRTS
        {
            get
            {
                return enableRTS;
            }

            set
            {
                enableRTS = value;
            }
        }

        public override string ToString()
        {
            return $"{this.PortName},{this.BaudRate},{this.StopBit},{this.DataBit},{this.CorrectBit},{this.EnableDTR}, {this.EnableDTR}";
        }

        private void SetEntity(string iniValue)
        {
            string[] dataValue = iniValue.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (dataValue != null && dataValue.Count() >= 7)
            {
                this.portName = dataValue[0];
                int.TryParse(dataValue[1], out this.baudRate);
                int.TryParse(dataValue[2], out this.stopBit);
                int.TryParse(dataValue[3], out this.dataBit);
                int.TryParse(dataValue[4], out this.correctBit);
                bool.TryParse(dataValue[5], out this.enableDTR);
                bool.TryParse(dataValue[6], out this.enableRTS);
            }
        }
    }
}