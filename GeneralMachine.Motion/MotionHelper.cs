using GeneralMachine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Motion
{
    public class MotionHelper : SingletionProvider<MotionHelper>
    {
        public MotionHelper()
        {
            foreach(CardNo cardNo in Enum.GetValues(typeof(CardNo)))
            {
                Cards.Add(cardNo, new Dictionary<int, Axis_Advantech>());
                for(int i =0; i < 4;++i)
                {
                    Cards[cardNo].Add(i, new Axis_Advantech(cardNo,i));
                }
            }
        }
        /// <summary>
        /// 急停
        /// </summary>
        public bool bEmg = false;

        /// <summary>
        /// 伺服报警
        /// </summary>
        public bool bServoWarning = false;

        /// <summary>
        /// 是否已经发生报警
        /// </summary>
        public bool bHappendError = false;

        /// <summary>
        /// 是否存在报警
        /// </summary>
        public bool Emg
        {
            get
            {
                return this.bEmg;
            }
        }

        /// <summary>
        /// 系统是否退出
        /// </summary>
        public bool bSystemExit = false;

        public System.Windows.Forms.Timer MachineIOMointor = new System.Windows.Forms.Timer();

        /// <summary>
        /// 控制卡集合
        /// </summary>
        public Dictionary<CardNo, Dictionary<int,Axis_Advantech>> Cards = new Dictionary<CardNo, Dictionary<int,Axis_Advantech>>();

        #region 板卡系统 操作----初始化,释放所有IO
        /// <summary>
        /// 初始化轴卡
        /// </summary>
        /// <returns></returns>
        public bool InitCard(string path)
        {
            return Axis_Advantech.CardInit(path) == 0;
        }

        /// <summary>
        /// 卸载所有轴卡
        /// </summary>
        /// <returns></returns>
        public bool UninstallCard()
        {
            return Axis_Advantech.CardClose() == 0;
        }

        /// <summary>
        /// 释放所有IO
        /// </summary>
        /// <returns></returns>
        public void ResetAllOuput()
        {
            foreach(var card in Cards.Values)
            {
                foreach(var axis in card.Values)
                {
                    axis.ResetIO_OUT(4);
                    axis.ResetIO_OUT(5);
                    axis.ResetIO_OUT(6);
                    axis.ResetIO_OUT(7);
                }
            }
        }
        #endregion

        #region IO 操作
        /// <summary>
        /// 获得 IO 输入点状态
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="axisNo"></param>
        /// <param name="inputNo"></param>
        /// <returns></returns>
        public bool this[CardNo cardNo, int axisNo, InputNo inputNo]
        {
            get
            {
                return Cards[cardNo][axisNo].bArrIO_In[(int)inputNo];
            }
        }

        /// <summary>
        /// 获得IO 输出点状态 和 设置 输出 点状态
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="axisNo">轴号</param>
        /// <param name="outputNo">输出点</param>
        /// <returns></returns>
        public bool this[CardNo cardNo, int axisNo, OutputNo outputNo]
        {
            get
            {
                return Cards[cardNo][axisNo].bArrIO_Out[(int)outputNo];
            }

            set
            {
                if(value)
                    Cards[cardNo][axisNo].SetIO_OUT((ushort)CommonHelper.GetEnumValue(typeof(OutputNo),outputNo));
                else
                    Cards[cardNo][axisNo].ResetIO_OUT((ushort)CommonHelper.GetEnumValue(typeof(OutputNo), outputNo));
            }
        }

        /// <summary>
        /// 开始监控IO
        /// </summary>
        public void StartMointorIO()
        {
            var thd = new Thread(RefreshIO);
            thd.Start();
            MachineIOMointor.Interval = 33;
            MachineIOMointor.Start();
        }

        /// <summary>
        /// 刷新IO
        /// </summary>
        public void RefreshIO()
        {
            while(!this.bSystemExit)
            {
                Thread.Sleep(5);
                bool emgTemp = false;
                bool servoWarning = false;
                try
                {
                    foreach (var card in Cards.Values)
                    {
                        foreach (var axis in card.Values)
                        {
                            axis.GetAxisSts();
                            axis.GetIO_IN();
                            axis.GetIO_Out();

                            #region 检查是否有报警
                            emgTemp |= axis.bAxisEmgOn;
                            servoWarning |= axis.bAxisServoWarning;

                            if (emgTemp)
                            {
                                if (this.bHappendError)
                                {
                                    MsgHelper.Instance.AddMessage(MsgLevel.Fatal, "急停按钮按下!!!");
                                    this.bHappendError = true;
                                }
                            }
                            else if (servoWarning)
                            {
                                if (this.bHappendError)
                                {
                                    MsgHelper.Instance.AddMessage(MsgLevel.Error, $"卡号[{Enum.GetName(typeof(CardNo), axis.CardNO)}] 轴号[{axis.AxisNo} 伺服报警]");
                                    this.bHappendError = true;
                                }
                            }
                            else
                            {
                                this.bHappendError = false;
                            }
                            #endregion
                        }
                    }
                }
                catch { }
      

                this.bEmg = emgTemp;
                this.bServoWarning = servoWarning;
            }
        }
        #endregion 
    }
}
