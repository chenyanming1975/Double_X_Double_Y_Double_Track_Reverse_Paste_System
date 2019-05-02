using System;
using System.Collections.Generic;
using System.Runtime.InteropServices; //For Marshal

namespace GeneralMachine.Motion
{
    using Advantech.Motion;
    using System.ComponentModel;
    using System.IO;

    /// <summary>
    /// 研华板卡
    /// </summary>
    public class Axis_Advantech
    {
        public Axis_Advantech()
        {
        }

        /// <summary>
        /// 以轴号初始化轴,轴号可以为0-3
        /// </summary>
        /// <param name="axisno"></param>
        public Axis_Advantech(CardNo cardNo, int AxisNo)
        {
            this.CardNO = cardNo;
            this.AxisNo = AxisNo;
        }

        #region 可配置参数--- 需要保存
        /// <summary>
        /// 卡号
        /// </summary>
        [DisplayName("卡号")]
        public CardNo CardNO
        {
            get;
            set;
        }

        /// <summary>
        /// 在该卡中的轴号
        /// </summary>
        [DisplayName("轴号")]
        public int AxisNo
        {
            get;
            set;
        }

        /// <summary>
        /// 脉冲比
        /// </summary>
        [DisplayName("脉冲比")]
        public double AxisRatio
        {
            get;
            set;
        } = 100;

        /// <summary>
        /// 反馈方式
        /// </summary>
        [DisplayName("反馈方式")]
        [Description("COMMAND:命令脉冲 ACT:编码器脉冲")]
        public AxisSource Source
        {
            get;
            set;
        } = AxisSource.COMMAND;

        /// <summary>
        /// 定位精度
        /// </summary>
        [DisplayName("到位精度(mm)")]
        [Description("到位精度越高，镇定时间越久")]
        public double MinDiff
        {
            get;
            set;
        } = 0.02;

        /// <summary>
        /// 回零方向
        /// </summary>
        [DisplayName("回零方向")]
        [Description("True:往正向回零 False:往负方向回零")]
        public bool HomeDirection
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 回零方式
        /// </summary>
        [DisplayName("回零方式")]
        [Description("详细见研华板卡说明书")]
        public HomeMode HomeMode
        {
            get;
            set;
        } = HomeMode.MODE1_Abs;
        #endregion

        #region 成员变量-- 临时变量不用保存
        //[轴状态]
        /// <summary>
        /// 轴状态
        /// </summary>
        public uint AxisSts = 0;
        /// <summary>
        /// 正极限状态
        /// </summary>
        public bool bPosLimit = false;
        /// <summary>
        /// 负极限状态
        /// </summary>
        public bool bNegLimit = false;
        /// <summary>
        /// 原点状态
        /// </summary>
        public bool bHome = false;
        /// <summary>
        /// 轴是否伺服报警
        /// </summary>
        public bool bAxisServoWarning = false;
        /// <summary>
        /// 轴是否伺服报警
        /// </summary>
        public bool bAxisServoOn = false;
        /// <summary>
        /// 轴是在运动
        /// </summary>
        public bool bAxisIsRunning = false;
        /// <summary>
        /// 轴目前是否在回原点动作中
        /// </summary>
        public bool bAxisIsHoming = false;
        /// <summary>
        /// 轴急停
        /// </summary>
        public bool bAxisEmgOn = false;
        /// <summary>
        /// 轴准备好
        /// </summary>
        public bool bAxisReady = true;
        //[每一轴的 IO]
        /// <summary>
        /// 轴输入
        /// </summary>
        public bool[] bArrIO_In = new bool[4];
        /// <summary>
        /// 轴输出
        /// </summary>
        public bool[] bArrIO_Out = new bool[4];
        /// <summary>
        /// 轴规划器位置
        /// </summary>
        public double AxisPrfPos = 0;
        /// <summary>
        /// 轴编码器位置
        /// </summary>
        public double AxisEncPos = 0;
        #endregion

        #region 板卡方法
        /// <summary>
        /// 得到轴状态
        /// </summary>
        /// <returns></returns>
        public short GetAxisSts()
        {
            ushort uCode = 0;
            short rtn = 0;
            lock (myobj)
            {
                rtn += (short)Motion.mAcm_AxGetMotionIO(AxisHandle[CardNO][AxisNo], ref AxisSts);
                #region 状态：伺服报警、原点、极限、急停、到位、回原点
                if ((AxisSts & (uint)Ax_Motion_IO.AX_MOTION_IO_ALM) > 0)//ALM
                {
                    bAxisServoWarning = true;
                }
                else
                {
                    bAxisServoWarning = false;
                }

                if ((AxisSts & (uint)Ax_Motion_IO.AX_MOTION_IO_ORG) > 0)//ORG
                {
                    bHome = true;
                }
                else
                {
                    bHome = false;
                }

                if ((AxisSts & (uint)Ax_Motion_IO.AX_MOTION_IO_LMTP) > 0)//+EL
                {
                    bPosLimit = true;
                }
                else
                {
                    bPosLimit = false;
                }

                if ((AxisSts & (uint)Ax_Motion_IO.AX_MOTION_IO_LMTN) > 0)//-EL
                {
                    bNegLimit = true;
                }
                else
                {
                    bNegLimit = false;
                }

                if ((AxisSts & (uint)Ax_Motion_IO.AX_MOTION_IO_EMG) > 0)//EMG
                {
                    bAxisEmgOn = true;
                }
                else
                {
                    bAxisEmgOn = false;
                }
                if ((AxisSts & (uint)Ax_Motion_IO.AX_MOTION_IO_INP) > 0)//INP
                {
                    bAxisIsRunning = false;
                }
                else
                {
                    bAxisIsRunning = true;
                }
                #endregion
                rtn += (short)Motion.mAcm_AxGetState(AxisHandle[CardNO][AxisNo], ref uCode);
                if (uCode == (ushort)AxisState.STA_AX_HOMING)
                {
                    bAxisIsHoming = true;
                }
                else
                {
                    bAxisIsHoming = false;
                }
                //if (uCode == (ushort)AxisState.STA_AX_READY)
                //{
                //    if (run == false)
                //    {
                //        bAxisIsRunning = false;
                //    }
                //    else
                //    {
                //        bAxisIsRunning = true;
                //    }
                //}
                //else
                //{
                //    bAxisIsRunning = true;
                //}
                rtn += (short)Motion.mAcm_AxGetCmdPosition(AxisHandle[CardNO][AxisNo], ref AxisPrfPos);
                rtn += (short)Motion.mAcm_AxGetActualPosition(AxisHandle[CardNO][AxisNo], ref AxisEncPos);
                rtn += ClearAxisSts();
                return rtn;
                //每次读取完轴状态，或者轴到达某个新状态后都需清除一次轴状态
            }
        }

        /// <summary>
        /// 复位所有输出-共4个
        /// </summary>
        /// <returns></returns>
        public short ResetAllIO_Out()
        {
            short rtn = 0;
            lock (myobj)
            {
                for (int i = 0; i < 4; i++)
                {
                    rtn = (short)Motion.mAcm_AxDoSetBit(AxisHandle[CardNO][AxisNo], (ushort)i, 0);
                    rtn += (short)Motion.mAcm_AxDoSetBit(AxisHandle[CardNO][AxisNo], (ushort)i, 0);
                    rtn += (short)Motion.mAcm_AxDoSetBit(AxisHandle[CardNO][AxisNo], (ushort)i, 0);
                    rtn += (short)Motion.mAcm_AxDoSetBit(AxisHandle[CardNO][AxisNo], (ushort)i, 0);
                }
                return rtn;
            }
        }

        /// <summary>
        /// 得到所有IO点的输出状态-共4个
        /// </summary>
        /// <returns></returns>
        public short GetIO_Out()
        {
            short rtn = 0;
            byte IO_Out = 0;
            lock (myobj)
            {
                for (ushort i = 0; i < 4; i++)
                {
                    rtn += (short)Motion.mAcm_AxDoGetBit(AxisHandle[CardNO][AxisNo], (ushort)(4 + i), ref IO_Out);
                    if (IO_Out == 1)
                    {
                        bArrIO_Out[i] = true;
                    }
                    else
                    {
                        bArrIO_Out[i] = false;
                    }
                }
                return rtn;
            }

        }

        /// <summary>
        /// 得到所有IO点的输入状态-共4个
        /// </summary>
        /// <returns></returns>
        public short GetIO_IN()
        {
            short rtn = 0;
            byte IO_IN = 0;
            lock (myobj)
            {
                for (ushort i = 0; i < 4; i++)
                {
                    rtn += (short)Motion.mAcm_AxDiGetBit(AxisHandle[CardNO][AxisNo], i, ref IO_IN);
                    if (IO_IN == 1)
                    {
                        bArrIO_In[i] = true;
                    }
                    else
                    {
                        bArrIO_In[i] = false;
                    }
                }
                return rtn;
            }
        }

        /// <summary>
        /// 复位输出点
        /// </summary>
        /// <param name="bit4_7">复位输出点4到7</param>
        /// <returns></returns>
        public short ResetIO_OUT(ushort bit4_7)
        {
            lock (myobj)
            {
                short rtn = (short)Motion.mAcm_AxDoSetBit(AxisHandle[CardNO][AxisNo], (ushort)(bit4_7), 0);
                return rtn;
            }
        }

        /// <summary>
        /// 设置输出点
        /// </summary>
        /// <param name="bit4_7">设置输出点4到7</param>
        /// <returns></returns>
        public short SetIO_OUT(ushort bit4_7)
        {
            lock (myobj)
            {
                short rtn = (short)Motion.mAcm_AxDoSetBit(AxisHandle[CardNO][AxisNo], (ushort)(bit4_7), 1);
                return rtn;
            }
        }

        /// <summary>
        /// 得到轴规划位
        /// </summary>
        /// <returns></returns>
        public short GetAxisPos()
        {
            lock (myobj)
            {
                short rtn = (short)Motion.mAcm_AxGetCmdPosition(AxisHandle[CardNO][AxisNo], ref AxisPrfPos);
                rtn += (short)Motion.mAcm_AxGetActualPosition(AxisHandle[CardNO][AxisNo], ref AxisEncPos);
                return rtn;
            }
        }

        /// <summary>
        /// 轴Jog运动
        /// </summary>
        /// <param name="LowVel">最低速度</param>
        /// <param name="HighVel">最高速度</param>
        /// <param name="acc">加速度</param>
        /// <param name="dec">减速度</param>
        /// <param name="isDirPositive">正负方向</param>
        /// <returns></returns>
        public short AxisMoveJog(double LowVel, double HighVel, double acc, double dec, bool direction)
        {
            lock (myobj)
            {
                short rtn = AxisSetValue(LowVel, HighVel, acc, dec);
                rtn += (short)Motion.mAcm_AxMoveVel(AxisHandle[CardNO][AxisNo], (ushort)(direction ? 0 : 1));
                return rtn;
            }
        }

        /// <summary>
        /// 设置轴速度
        /// </summary>
        /// <param name="dVelLow">最低速度</param>
        /// <param name="dVelHigh">最高速度</param>
        /// <param name="dAcc">加速度</param>
        /// <param name="dDec">减速度</param>
        /// <returns></returns>
        public short AxisSetValue(double dVelLow, double dVelHigh, double dAcc, double dDec) // 設定速度,加速度範圍
        {
            lock (myobj)
            {
                short rtn = (short)Motion.mAcm_SetProperty(AxisHandle[CardNO][AxisNo], (uint)PropertyID.PAR_AxVelLow, ref dVelLow, (uint)Marshal.SizeOf(typeof(double)));
                rtn += (short)Motion.mAcm_SetProperty(AxisHandle[CardNO][AxisNo], (uint)PropertyID.PAR_AxVelHigh, ref dVelHigh, (uint)Marshal.SizeOf(typeof(double)));
                rtn += (short)Motion.mAcm_SetProperty(AxisHandle[CardNO][AxisNo], (uint)PropertyID.PAR_AxAcc, ref dAcc, (uint)Marshal.SizeOf(typeof(double)));
                rtn += (short)Motion.mAcm_SetProperty(AxisHandle[CardNO][AxisNo], (uint)PropertyID.PAR_AxDec, ref dDec, (uint)Marshal.SizeOf(typeof(double)));
                return rtn;
            }
        }

        /// <summary>
        /// 在运动中改变轴速度
        /// </summary>
        /// <param name="NewVel">新的速度</param>
        /// <param name="NewAcc">新的加速度</param>
        /// <param name="NewDec">新的减速度</param>
        /// <returns></returns>
        public short AxisChangeValue(double NewVel, double NewAcc, double NewDec) // 設定速度,加速度範圍
        {
            lock (myobj)
            {
                short rtn = (short)Motion.mAcm_AxChangeVel(AxisHandle[CardNO][AxisNo], NewVel);
                //return (short)Motion.mAcm_AxChangeVelEx(AxisHandle[CardNO][AxisNo], NewVel, NewAcc, NewDec);
                return rtn;
            }
        }

        /// <summary>
        /// 轴回零
        /// </summary>
        /// <param name="uHomeMode">回零模式</param>
        /// <param name="IsDirP">是否正向</param>
        /// <param name="LowVel">最低速度</param>
        /// <param name="HighVel">最高速度</param>
        /// <param name="acc">加速度</param>
        /// <param name="dec">减速度</param>
        /// <returns></returns>
        public short AxisGoHome(HomeMode uHomeMode, bool direction, double LowVel, double HighVel, double acc, double dec)
        {
            lock (myobj)
            {
                short rtn = AxisSetValue(LowVel, HighVel, acc, dec);
                rtn += (short)Motion.mAcm_AxHome(AxisHandle[CardNO][AxisNo], (ushort)uHomeMode, (ushort)(direction ? 0 : 1));
                return rtn;
            }
        }

        /// <summary>
        /// 轴使能
        /// </summary>
        /// <returns></returns>
        public short SetAxisServoOn()
        {
            lock (myobj)
            {
                return (short)Motion.mAcm_AxSetSvOn(AxisHandle[CardNO][AxisNo], 1);
            }
        }

        /// <summary>
        /// 轴去使能
        /// </summary>
        /// <returns></returns>
        public short SetAxisServoOff()
        {
            lock (myobj)
            {
                return (short)Motion.mAcm_AxSetSvOn(AxisHandle[CardNO][AxisNo], 0);
            }
        }

        /// <summary>
        /// 设置最大速度、加速度、减速度
        /// </summary>
        /// <param name="dMaxVel">最大速度</param>
        /// <param name="dMaxAcc">加速度</param>
        /// <param name="dMaxDec">减速度</param>
        /// <returns></returns>
        public short SetAxisMaxRange(double dMaxVel, double dMaxAcc, double dMaxDec) // 設定最大速度,加速度範圍
        {
            lock (myobj)
            {
                // Max Vel
                short rtn = (short)Motion.mAcm_SetProperty(AxisHandle[CardNO][AxisNo], (uint)PropertyID.CFG_AxMaxVel, ref dMaxVel, (uint)Marshal.SizeOf(typeof(double)));
                // Max Acc
                rtn += (short)Motion.mAcm_SetProperty(AxisHandle[CardNO][AxisNo], (uint)PropertyID.CFG_AxMaxAcc, ref dMaxAcc, (uint)Marshal.SizeOf(typeof(double)));
                // Max Dec
                rtn += (short)Motion.mAcm_SetProperty(AxisHandle[CardNO][AxisNo], (uint)PropertyID.CFG_AxMaxDec, ref dMaxDec, (uint)Marshal.SizeOf(typeof(double)));
                return rtn;
            }
        }

        /// <summary>
        /// 点到点运动-相对
        /// </summary>
        /// <param name="dist">相对目前位置的距离</param>
        /// <param name="LowVel">最低速度</param>
        /// <param name="HighVel">最高速度</param>
        /// <param name="acc">加速度</param>
        /// <param name="dec">减速度</param>
        /// <returns></returns>
        public short AxisMoveTrap_Rel(double dist, double LowVel, double HighVel, double acc, double dec)
        {
            lock (myobj)
            {
                short rtn = AxisSetValue(LowVel, HighVel, acc, dec);
                rtn = (short)Motion.mAcm_AxMoveRel(AxisHandle[CardNO][AxisNo], dist);
                return rtn;
            }
        }

        /// <summary>
        /// 点到点运动-绝对
        /// </summary>
        /// <param name="dist">绝对的位置点</param>
        /// <param name="LowVel">最低速度</param>
        /// <param name="HighVel">最高速度</param>
        /// <param name="acc">加速度</param>
        /// <param name="dec">减速度</param>
        /// <returns></returns>
        public short AxisMoveTrap_Abs(double dist, double LowVel, double HighVel, double acc, double dec)
        {
            lock (myobj)
            {
                short rtn = AxisSetValue(LowVel, HighVel, acc, dec);
                rtn = (short)Motion.mAcm_AxMoveAbs(AxisHandle[CardNO][AxisNo], dist);
                return rtn;
            }
        }

        /// <summary>
        /// 立即停止轴运动
        /// </summary>
        /// <returns></returns>
        public short StopAxis()
        {
            lock (myobj)
            {
                short rtn = (short)Motion.mAcm_AxStopEmg(AxisHandle[CardNO][AxisNo]);
                return rtn;
            }
        }

        /// <summary>
        /// 清除轴状态
        /// </summary>
        /// <returns></returns>
        public short ClearAxisSts()
        {
            lock (myobj)
            {
                short rtn = (short)Motion.mAcm_AxResetError(AxisHandle[CardNO][AxisNo]);//清除当前轴状态
                return rtn;
            }
        }

        /// <summary>
        /// 清除轴位置
        /// </summary>
        /// <returns></returns>
        public short ZeroAxis()
        {
            lock (myobj)
            {
                short rtn = (short)Motion.mAcm_AxSetCmdPosition(AxisHandle[CardNO][AxisNo], 0);
                rtn += (short)Motion.mAcm_AxSetActualPosition(AxisHandle[CardNO][AxisNo], 0);
                return rtn;
            }
        }

        /// <summary>
        /// 获得目前的比较输出的数据
        /// </summary>
        /// <param name="rCurCmpData">比较输出数据</param>
        /// <returns></returns>
        public short GetCompareCurData(ref double rCurCmpData)
        {
            lock (myobj)
            {
                return (short)Motion.mAcm_AxGetCmpData(AxisHandle[CardNO][AxisNo], ref rCurCmpData);
            }
        }

        /// <summary>
        /// 设置比较输出数据
        /// </summary>
        /// <param name="EN">使用与否</param>
        /// <param name="CompareMethodIndex">0-greater 1-smaller</param>
        /// <param name="CompareSourceIndex">0-command 1-encode</param>
        /// <param name="ComparePulseLogicIndex">0-Low 1-High</param>
        /// <param name="ComparePulseWidthIndex">脉冲宽度</param>
        /// <param name="ComparePulseEX">脉冲宽度</param>
        /// <returns></returns>
        public short SetComapreData(uint EN, uint CompareMethodIndex, uint CompareSourceIndex, uint ComparePulseLogicIndex, uint ComparePulseWidthIndex, uint ComparePulseEX)
        {
            lock (myobj)
            {
                short rtn = (short)Motion.mAcm_SetU32Property(AxisHandle[CardNO][AxisNo], (uint)PropertyID.CFG_AxCmpSrc, CompareSourceIndex);//0-command 1-encode
                rtn += (short)Motion.mAcm_SetU32Property(AxisHandle[CardNO][AxisNo], (uint)PropertyID.CFG_AxCmpMethod, CompareMethodIndex);//0-greater 1-smaller
                rtn += (short)Motion.mAcm_SetU32Property(AxisHandle[CardNO][AxisNo], (uint)PropertyID.CFG_AxCmpPulseLogic, ComparePulseLogicIndex);//0-Low 1-High
                rtn += (short)Motion.mAcm_SetU32Property(AxisHandle[CardNO][AxisNo], (uint)PropertyID.CFG_AxCmpPulseWidth, 5);//0-5 1-10
                rtn += (short)Motion.mAcm_SetU32Property(AxisHandle[CardNO][AxisNo], (uint)PropertyID.CFG_AxCmpPulseWidthEx, 1000);//ComparePulseEX);//0-5 1-10
                rtn += (short)Motion.mAcm_SetU32Property(AxisHandle[CardNO][AxisNo], (uint)PropertyID.CFG_AxCmpEnable, EN);//0-disable 1-enable
                return rtn;
            }
        }

        /// <summary>
        /// 设置比较输出表
        /// </summary>
        /// <param name="TableArray">比较输出队列</param>
        /// <param name="TablePointsCount">比较输出点数</param>
        /// <returns></returns>
        public short SetComapreTable(double[] TableArray, short TablePointsCount)
        {
            lock (myobj)
            {
                return (short)Motion.mAcm_AxSetCmpTable(AxisHandle[CardNO][AxisNo], TableArray, TablePointsCount);
            }
        }
        #endregion

        #region 静态参数
        /// <summary>
        /// 锁
        /// </summary>
        private static object myobj = new object();

        /// <summary>
        /// 设备列表
        /// </summary>
        private static DEV_LIST[] DeviceList = new DEV_LIST[Motion.MAX_DEVICES];//

        /// <summary>
        /// 设备个数
        /// </summary>
        private static uint uDeviceCount = 0;

        /// <summary>
        /// 板卡-设备句柄
        /// </summary>
        private static IntPtr[] CardHandle = null;

        /// <summary>
        /// 轴-设备句柄
        /// </summary>
        public static Dictionary<CardNo, IntPtr[]> AxisHandle = new Dictionary<CardNo, IntPtr[]>();
        #endregion

        #region 静态方法
        /// <summary>
        /// 禁止Cam 功能
        /// </summary>
        public static void DisableCamDO()
        {
            foreach (var list in AxisHandle.Values)
            {
                foreach (var axis in list)
                    Motion.mAcm_SetI32Property(axis, (uint)CFG_AX_Property.CFG_AxCamDOEnable, 0);
            }
        }

        public static List<string> GetCard()
        {
            List<string> CardName = new List<string>();
            Motion.mAcm_GetAvailableDevs(DeviceList, Motion.MAX_DEVICES, ref uDeviceCount);
            for (int i = 0; i < uDeviceCount; ++i)
            {
                CardName.Add(DeviceList[i].DeviceName);
            }

            return CardName;
        }

        /// <summary>
        /// 轴卡初始化，搜索使用后再加载配置文件 默认两张轴卡
        /// </summary>
        /// <param name="ConfigPath1">卡1配置文件路径</param>
        /// <param name="ConfigPath2">卡2配置文件路径</param>
        /// <returns></returns>
        public static short CardInit(string cardPath)
        {
            if (!Directory.Exists(cardPath))
            {
                return 1;
            }

            List<string> Paths = Common.CommonHelper.GetFilePath(cardPath, "*.cfg");

            short rtn;
            try
            {
                rtn = (short)Motion.mAcm_GetAvailableDevs(DeviceList, Motion.MAX_DEVICES, ref uDeviceCount);
            }
            catch
            {
                return 1;
            }
            if (uDeviceCount == 0)
            {
                return 1;
            }

            CardHandle = new IntPtr[uDeviceCount];
            for (int ii = 0; ii < uDeviceCount; ii++)
            {
                IntPtr pCardHandle = IntPtr.Zero;

                rtn += (short)Motion.mAcm_DevOpen(DeviceList[ii].DeviceNum, ref pCardHandle);
                if (DeviceList[ii].DeviceName.Contains("1285"))
                {
                    Motion.mAcm_DevOpen(DeviceList[ii].DeviceNum, ref pCardHandle);
                    CardHandle[ii] = pCardHandle;
                    CardNo a = (CardNo)(ii * 2);
                    CardNo b = (CardNo)(ii * 2 + 1);
                    AxisHandle.Add(a, new IntPtr[4]);
                    AxisHandle.Add(b, new IntPtr[4]);

                    for (ushort i = 0; i < 8; ++i)
                    {
                        IntPtr pAxisHandle = IntPtr.Zero;
                        rtn += (short)Motion.mAcm_AxOpen(pCardHandle, i, ref pAxisHandle);

                        if (i < 4)
                            AxisHandle[a][i] = pAxisHandle;
                        else
                            AxisHandle[b][i - 4] = pAxisHandle;
                    }

                    rtn += (short)Motion.mAcm_DevLoadConfig(pCardHandle, Paths[ii]);
                }
                else if (DeviceList[ii].DeviceName.Contains("1245"))
                {
                    Motion.mAcm_DevOpen(DeviceList[ii].DeviceNum, ref pCardHandle);
                    CardHandle[ii] = pCardHandle;
                    CardNo a = (CardNo)(ii * 2);
                    AxisHandle.Add(a, new IntPtr[4]);

                    for (ushort i = 0; i < 4; ++i)
                    {
                        IntPtr pAxisHandle = IntPtr.Zero;
                        rtn += (short)Motion.mAcm_AxOpen(pCardHandle, i, ref pAxisHandle);

                        AxisHandle[a][i] = pAxisHandle;
                    }
                    rtn += (short)Motion.mAcm_DevLoadConfig(pCardHandle, Paths[ii]);
                }

            }
            return rtn;
        }     //初始化轴卡-DONE

        /// <summary>
        /// 关闭轴卡
        /// </summary>
        /// <returns></returns>
        public static short CardClose()
        {
            try
            {
                short rtn = 0;
                UInt16[] usAxisState = new UInt16[32];

                #region 关闭每一个轴,卡
                foreach (CardNo cardNo in Enum.GetValues(typeof(CardNo)))
                {
                    for (int axisNo = 0; axisNo < 4; axisNo++)
                    {
                        ushort state = 0;
                        rtn += (short)Motion.mAcm_AxGetState(AxisHandle[cardNo][axisNo], ref state);
                        if (state == (ushort)AxisState.STA_AX_ERROR_STOP)
                        {
                            rtn += (short)Motion.mAcm_AxResetError(AxisHandle[cardNo][axisNo]);
                        }

                        rtn += (short)Motion.mAcm_AxStopDec(AxisHandle[cardNo][axisNo]);
                        rtn += (short)Motion.mAcm_AxClose(ref AxisHandle[cardNo][axisNo]);
                    }

                    rtn += (short)Motion.mAcm_DevClose(ref CardHandle[(int)(cardNo) / 2]);
                }
                #endregion

                return rtn;
            }
            catch { }

            return 0;
        }
        #endregion
    }
}
