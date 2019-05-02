using GeneralMachine.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Track
{
    public class TrackEntiy
    {
        public TrackEntiy()
        {
            Config = new TrackConfig();
            TrackIO = new TrackIO();
        }

        public TrackEntiy(TrackConfig config, TrackIO state)
        {
            this.TrackIO = state;
            this.Config = config;
        }

        #region 基本配置
        public TrackConfig Config = null;
        public TrackIO TrackIO = null;
        #endregion

        #region 外部属性
        /// <summary>
        /// 进入ByPass
        /// </summary>
        public bool ByPass
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 是否进行测试
        /// </summary>
        public bool IsTest
        {
            get;
            set;
        }

        /// <summary>
        /// 贴装完成
        /// </summary>
        public bool PastedOK
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 进板到位完成
        /// </summary>
        public bool ReachOK
        {
            get;
            set;
        } = false;
        #endregion

        #region 对外函数
        /// <summary>
        /// 轨道运行
        /// </summary>
        public void TrackRun()
        {

        }

        /// <summary>
        /// 轨道暂停
        /// </summary>
        public void TrackPause()
        {

        }

        /// <summary>
        /// 手动进板
        /// </summary>
        public void ManualInput(FlowInOutMode flowMode)
        {
            Task.Factory.StartNew(() => {
                this.StopMove(true);
                this.CarryMove(false);
                Thread.Sleep(500);

                // 进板
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                ConveryRun(flowMode, true);
                while(!this.TrackReach() && stopwatch.ElapsedMilliseconds < ConstDefine.iInputTime)
                {
                    Thread.Sleep(10);
                }

                ConveryStop();
                if(this.TrackReach())
                {
                    this.StopMove(false);
                    this.CarryMove(true);
                }
            });
        }

        /// <summary>
        /// 手动出板
        /// </summary>
        public void ManualOutput(FlowInOutMode flowMode)
        {
            Task.Factory.StartNew(() =>
            {
                this.StopMove(false);
                this.CarryMove(false);
                Thread.Sleep(500);

                // 出板
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                ConveryRun(flowMode, false);
                while (!this.TrackOutput(flowMode) && stopwatch.ElapsedMilliseconds < ConstDefine.iInputTime)
                {
                    Thread.Sleep(10);
                }

                ConveryStop();
            });
        } 

        #region IO操作
        /// <summary>
        /// 传送带运动
        /// </summary>
        /// <param name="dir">true 正转/false 反转</param>
        public void ConveryRun(bool dir)
        {
            if (dir)
            {
                this.TrackIO.IO_Reverse.SetIO(false);
                this.TrackIO.IO_Positive.SetIO(true);
            }
            else
            {
                this.TrackIO.IO_Reverse.SetIO(true);
                this.TrackIO.IO_Positive.SetIO(true);
            }
        }

        public void ConveryRun(FlowInOutMode mode, bool Input)
        {
            switch(mode)
            {
                case FlowInOutMode.左进右出:
                        this.ConveryRun(true);
                    break;
                case FlowInOutMode.左进左出:
                        this.ConveryRun(Input);
                    break;
                case FlowInOutMode.右进左出:
                        this.ConveryRun(false);
                    break;
                case FlowInOutMode.右进右出:
                    this.ConveryRun(!Input);
                    break;
            }
        }
        /// <summary>
        /// 传送带正转
        /// </summary>
        public void ConveryStop()
        {
            this.TrackIO.IO_Positive.SetIO(false);
            this.TrackIO.IO_Reverse.SetIO(false);
        }

        /// <summary>
        /// 夹板气缸移动
        /// </summary>
        /// <param name="dir">true 动点/false 原点</param>
        public void CarryMove(bool dir)
        {
            TrackIO.IO_CarryMove.SetIO(dir);
            TrackIO.IO_Carry.SetIO(!dir);
        }

        /// <summary>
        /// 阻挡气缸移动
        /// </summary>
        /// <param name="dir">true 动点/false 原点</param>
        public void StopMove(bool dir)
        {
            TrackIO.IO_StopMove.SetIO(dir);
            TrackIO.IO_Stop.SetIO(!dir);
        }
        #endregion

        /// <summary>
        /// 获得到位信号
        /// </summary>
        /// <returns></returns>
        public bool TrackReach()
        {
            return this.TrackIO.IO_TrackReach.GetIO();
        }

        /// <summary>
        /// 获得 进板信号
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public bool TrackInput(FlowInOutMode mode)
        {
            switch(mode)
            {
                case FlowInOutMode.左进左出:
                case FlowInOutMode.左进右出:
                    return this.TrackIO.IO_TrackIn.GetIO();
                case FlowInOutMode.右进左出:
                case FlowInOutMode.右进右出:
                    return this.TrackIO.IO_TrackOut.GetIO();
            }
            return false;
        }

        /// <summary>
        /// 获得 出板信号
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public bool TrackOutput(FlowInOutMode mode)
        {
            switch (mode)
            {
                case FlowInOutMode.右进右出:
                case FlowInOutMode.左进右出:
                    return this.TrackIO.IO_TrackOut.GetIO();
                case FlowInOutMode.右进左出:
                case FlowInOutMode.左进左出:
                    return this.TrackIO.IO_TrackIn.GetIO();
            }
            return false;
        }
        #endregion
    }
}
