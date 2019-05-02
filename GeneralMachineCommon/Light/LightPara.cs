using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Threading;
using GeneralMachine.Definition;
using System.Diagnostics;

namespace GeneralMachine.Light
{
    public class LightPara
    {
        /// <summary>
        /// 光源厂家
        /// </summary>
        
        public VendorName Vendor;//厂商序号
        public short COM_NO;//串口号
        public bool bRed;//红色光使用与否
        public bool bGreen;//绿色光使用与否
        public bool bBlue;//蓝色光使用与否
        public short dRedValue;//红色光亮度
        public short dGreenValue;//绿色光亮度
        public short dBlueValue;//蓝色光亮度
        public SerialPort Huilin_Light;//汇林光源
        public OPTControllerAPI OPT_Light;// = new OPTControllerAPI();//opt光源
        public int lRet;//OPT返回值

        private short OPTChannelTransform(short channel)
        {
            short aa = (short)((channel + 1) % 3);
            if (aa == 0)
            {
                aa = 3;
            }
            return aa;
        }

        /// <summary>
        /// 构造函数 传递 厂家、COM口、上下光源序号等信息
        /// </summary>
        /// <param name="厂家 汇林=0 OPT=1"></param>
        /// <param name="COM口"></param>
        /// <param name="上下光源序号"></param>
        public LightPara(VendorName vendor,short com_NO)
        {
            Vendor = (VendorName)vendor;
            if(vendor == VendorName.OPT)
            {
                OPT_Light = new OPTControllerAPI();//opt光源
            }
            COM_NO = com_NO;
        }

        /// <summary>
        /// 初始化光源
        /// </summary>
        /// <returns></returns>
        public short InitLight()
        {
            if (Vendor == VendorName.HuiLin)//Huilin
            {
                try
                {
                    Huilin_Light = new SerialPort("COM" + COM_NO.ToString(), 9600, Parity.None, 8);
                    if (!Huilin_Light.IsOpen)
                    {
                        Huilin_Light.Open();
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return 1;
                }
            }
            else//OPT
            {
                try
                {
                    lRet = OPT_Light.InitSerialPort("COM" + COM_NO.ToString());
                }
                catch
                {
                    lRet =1;
                }
                
                if(lRet !=0)
                {
                    return 1;
                }
            }
            return 0;
        }//

        /// <summary>
        /// 更新光源计划，以目前的打光方式进行打光
        /// </summary>
        public void UpdateValue(bool RedUse, bool GreenUse, bool BlueUse, bool UPUse,double RedValue, double GreenValue, double BlueValue, double UPValue)
        {
            bRed = RedUse;
            bGreen = GreenUse;
            bBlue = BlueUse;
            dRedValue = (short)RedValue;
            dGreenValue = (short)GreenValue;
            dBlueValue = (short)BlueValue;

            if (RedUse)
                Setchannelvalue(1, dRedValue);

            if (GreenUse)
                Setchannelvalue(2, dGreenValue);

            if (BlueUse)
                Setchannelvalue(3, dBlueValue);

            if (UPUse)
                Setchannelvalue(4, (short)UPValue);
        }

        /// <summary>
        /// 关闭光源
        /// </summary>
        public void Close()
        {
            try
            {
                Huilin_Light.Close();
            }
            catch { }
            try
            {
                OPT_Light.ReleaseSerialPort();
            }
            catch { }
        }

        /// <summary>
        /// 打开通道
        /// </summary>
        /// <param name="通道"></param>
        private void openchannel(short ChannelIndex)
        {
            if (Vendor == 0)//Huilin
            {
                Byte[] senByte = new Byte[8];
                senByte[0] = 0x2;
                senByte[1] = 0x4D;
                senByte[2] = 0x30;
                senByte[3] = 0x30;
                senByte[4] = (Byte)(0x30 + ChannelIndex);
                senByte[5] = 0x31;
                senByte[6] = 0x0D;
                senByte[7] = 0x00;
                Huilin_Light.Write(senByte, 0, 8);
            }
            else//OPT
            {
                OPT_Light.TurnOnChannel(OPTChannelTransform(ChannelIndex));
            }

        }
       
        /// <summary>
        /// 关闭通道
        /// </summary>
        /// <param name="ChannelIndex"></param>
        private void closechannel(short ChannelIndex)
        {
            if (Vendor == 0)//Huilin
            {
                Byte[] senByte = new Byte[8];
                senByte[0] = 0x2;
                senByte[1] = 0x4D;
                senByte[2] = 0x30;
                senByte[3] = 0x30;
                senByte[4] = (Byte)(0x30 + ChannelIndex);
                senByte[5] = 0x30;
                senByte[6] = 0x0D;
                senByte[7] = 0x00;
                Huilin_Light.Write(senByte, 0, 8);
            }
            else//OPT
            {
                OPT_Light.TurnOffChannel(OPTChannelTransform(ChannelIndex));
            }

        }

        /// <summary>
        /// 设置通道值
        /// </summary>
        /// <param name="通道"></param>
        /// <param name="通道值"></param>
        public void Setchannelvalue(short ChannelIndex, double ChannelValue)
        {
            if (Vendor == VendorName.HuiLin)//Huilin
            {
                short BAI = (short)Math.Floor(ChannelValue / 100);
                short SHI = (short)Math.Floor((ChannelValue - 100 * BAI) / 10);
                short GE = (short)Math.Floor((ChannelValue - 100 * BAI - 10 * SHI));
                Byte[] senByte = new Byte[10];
                senByte[0] = 0x02;
                senByte[1] = 0x4C;
                senByte[2] = 0x30;
                senByte[3] = 0x30;
                senByte[4] = (Byte)(0x30 + ChannelIndex);
                senByte[5] = Byte.Parse(Encoding.GetEncoding("Unicode").GetBytes(new char[] { char.Parse(BAI.ToString()) })[0].ToString());
                senByte[6] = Byte.Parse(Encoding.GetEncoding("Unicode").GetBytes(new char[] { char.Parse(SHI.ToString()) })[0].ToString());
                senByte[7] = Byte.Parse(Encoding.GetEncoding("Unicode").GetBytes(new char[] { char.Parse(GE.ToString()) })[0].ToString());
                senByte[8] = 0x0D;
                senByte[9] = 0x00;
                Huilin_Light.Write(senByte, 0, 10);
            }
            else//OPT
            {
                OPT_Light.SetIntensity(OPTChannelTransform(ChannelIndex), (int)ChannelValue);
            }
        }
    }//光源
}
