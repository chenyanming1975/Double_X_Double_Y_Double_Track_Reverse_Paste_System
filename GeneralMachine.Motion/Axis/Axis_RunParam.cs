using GeneralMachine.Definition;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Motion
{
    public class Axis_RunParam: Axis_Advantech
    {
        public Axis_RunParam()
        {
        }

        /// <summary>
        /// 创建一个轴运行参数
        /// </summary>
        /// <param name="axis"></param>
        public Axis_RunParam(CardNo card, int axisNo):
            base(card, axisNo)
        {
        }

        #region 基本运动点-点， JOG, 定长运动
        /// <summary>
        /// Jog 运动
        /// </summary>
        /// <param name="speed">速度</param>
        /// <param name="dirct">方向</param>
        /// <returns></returns>
        public short Jog(HostarSpeed speed, bool dirct)
        {
            var newSpeed = speed.GetActSpeed(this.AxisRatio);
            return this.AxisMoveJog(newSpeed.StartSpeed, newSpeed.MaxSpeed, newSpeed.AccTime, newSpeed.DecTime, dirct);
        }

        /// <summary>
        /// 点-点运动
        /// </summary>
        /// <param name="pos">位置</param>
        /// <param name="speed">速度</param>
        /// <returns></returns>
        public short GoPos(double pos, HostarSpeed speed)
        {
            if (double.IsNaN(pos) || double.IsInfinity(pos))
                return -1;

            var sd = speed.GetActSpeed(this.AxisRatio);
            return this.AxisMoveTrap_Abs(pos * this.AxisRatio, sd.StartSpeed, sd.MaxSpeed, sd.AccTime, sd.DecTime);
        }

        /// <summary>
        /// 点-点运动  等待停止
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="pos"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short GoPosTillStop(int timeout, double pos, HostarSpeed speed)
        {
            if (double.IsNaN(pos) || double.IsInfinity(pos))
                return -1;

            Stopwatch a = new Stopwatch();
            short rtn = 0;
            var sd = speed.GetActSpeed(this.AxisRatio);
            this.AxisMoveTrap_Abs(pos * this.AxisRatio, sd.StartSpeed, sd.MaxSpeed, sd.AccTime, sd.DecTime);
            a.Start();

            while (!this.AxisReach(pos))
            {
                Thread.Sleep(1);

                if (a.ElapsedMilliseconds > timeout || MotionHelper.Instance.Emg)
                {
                    this.StopAxis();
                    this.ClearAxisSts();
                    return -1;
                }
            }

            return rtn;
        }
        
        /// <summary>
        /// 定长运动
        /// </summary>
        /// <param name="TrimDist">定长</param>
        /// <param name="speed">速度</param>
        /// <returns></returns>
        public short MoveTrim(double TrimDist, HostarSpeed speed)
        {
            if (double.IsNaN(TrimDist) || double.IsInfinity(TrimDist))
                return -1;

            this.GetAxisPos();
            return this.GoPos(this.Pos + TrimDist, speed);
        }

        /// <summary>
        /// 轴 停止运动
        /// </summary>
        /// <returns></returns>
        public short Stop(bool bEmg = false)
        {
            return bEmg ? this.Stop() : this.StopAxis();
        }
        #endregion

        #region 回原点
        private short GoHomeWithLimit(int timeout, HomeMode homeMode, bool direction, HostarSpeed goLimit, HostarSpeed goHome)
        {
            short rtn = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.StopAxis();
            this.GetAxisSts();
            #region 到轴的极限位置
            if (direction && this.bNegLimit || !direction && this.bPosLimit)
            {

            }
            else
            {
       
                rtn = this.Jog(goLimit, !direction); // 到极限
                while (direction && !this.bNegLimit || !direction && !this.bPosLimit)
                {
                    Thread.Sleep(1);
                    this.GetAxisSts();
                    if (stopwatch.ElapsedMilliseconds > timeout || MotionHelper.Instance.Emg)
                    {
                        stopwatch.Stop();
                        this.StopAxis();
                        this.ClearAxisSts();
                        return -1;
                    }
                }
            }

            this.StopAxis();
            Thread.Sleep(200);
            this.GetAxisSts();
            #endregion

            #region 开始回原点
            stopwatch.Reset();
            stopwatch.Start();
            var homeSpeed = goHome.GetActSpeed(this.AxisRatio);
            rtn += this.AxisGoHome(homeMode, direction, homeSpeed.StartSpeed, homeSpeed.MaxSpeed, homeSpeed.AccTime, homeSpeed.DecTime);
            Thread.Sleep(200);
            this.GetAxisSts();

            while (this.bAxisIsHoming || this.bAxisIsRunning)
            {
                Thread.Sleep(1);
                this.GetAxisSts();
                if (stopwatch.ElapsedMilliseconds > timeout || MotionHelper.Instance.Emg)
                {
                    stopwatch.Stop();
                    this.StopAxis();
                    this.ClearAxisSts();
                    return -1;
                }
            }

            this.StopAxis();
            this.ClearAxisSts();
            this.ZeroAxis();
            stopwatch.Stop();
            return rtn;
            #endregion
        }

        private short GoHomeWithInput(int timeout, HomeMode homeMode, bool direction, HostarSpeed goLimit, HostarSpeed goHome, IOInput limit)
        {
            short rtn = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.StopAxis();
            this.GetAxisSts();

            #region 到轴的极限位置
            if (!limit.GetIO())
            {
                rtn = this.Jog(goLimit, !direction); // 到极限
                while (!limit.GetIO())
                {
                    Thread.Sleep(1);
                    this.GetAxisSts();
                    if (stopwatch.ElapsedMilliseconds > timeout || MotionHelper.Instance.Emg)
                    {
                        stopwatch.Stop();
                        this.StopAxis();
                        this.ClearAxisSts();
                        return -1;
                    }
                }
            }

            this.StopAxis();
            #endregion
            Thread.Sleep(200);

            #region 开始回原点
            stopwatch.Reset();
            stopwatch.Start();
            var homeSpeed = goHome.GetActSpeed(this.AxisRatio);
            rtn += this.AxisGoHome(homeMode, direction, homeSpeed.StartSpeed, homeSpeed.MaxSpeed, homeSpeed.AccTime, homeSpeed.DecTime);
            Thread.Sleep(200);
            this.GetAxisSts();

            while (this.bAxisIsHoming || this.bAxisIsRunning)
            {
                Thread.Sleep(1);
                this.GetAxisSts();
                if (stopwatch.ElapsedMilliseconds > timeout || MotionHelper.Instance.Emg)
                {
                    stopwatch.Stop();
                    this.StopAxis();
                    this.ClearAxisSts();
                    return -1;
                }
            }

            this.StopAxis();
            this.ClearAxisSts();
            this.ZeroAxis();
            stopwatch.Stop();
            return rtn;
            #endregion
        }

        /// <summary>
        /// 使用极限回原点的方式
        /// </summary>
        /// <param name="homeMode"></param>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short GoHome(int timeout, HostarSpeed goLimit, HostarSpeed goHome, bool limit = true)
        {
            if (limit)
            {
                return this.GoHomeWithLimit(timeout, this.HomeMode, this.HomeDirection, goLimit, goHome);
            }
            else
            {
                #region 开始回原点
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var homeSpeed = goHome.GetActSpeed(this.AxisRatio);
                short rtn = this.AxisGoHome(this.HomeMode, this.HomeDirection, homeSpeed.StartSpeed, homeSpeed.MaxSpeed, homeSpeed.AccTime, homeSpeed.DecTime);
                Thread.Sleep(200);
                this.GetAxisSts();

                while (this.bAxisIsHoming || this.bAxisIsRunning)
                {
                    Thread.Sleep(1);
                    this.GetAxisSts();
                    if (stopwatch.ElapsedMilliseconds > timeout || MotionHelper.Instance.Emg)
                    {
                        stopwatch.Stop();
                        this.StopAxis();
                        this.ClearAxisSts();
                        return -1;
                    }
                }

                this.StopAxis();
                this.ClearAxisSts();
                this.ZeroAxis();
                stopwatch.Stop();
                return rtn;
                #endregion
            }
        }

        public short GoHome(int timeout, HostarSpeed goLimit, HostarSpeed goHome, IOInput limit, bool Wait = true)
        {
            return this.GoHomeWithInput(timeout, this.HomeMode, this.HomeDirection, goLimit, goHome, limit);
        }

        #endregion

        /// <summary>
        /// 脉冲比较 判断是否到位
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="CommandBack"></param>
        /// <returns></returns>
        public virtual bool AxisReach(double pos)
        {
            this.GetAxisSts();

            if (Math.Abs(pos - this.Pos) <=  this.MinDiff && !this.bAxisIsRunning)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 当前位置
        /// </summary>
        [Browsable(false)]
        [JsonIgnore]
        public double Pos
        {
            get
            {
                if(this.Source == AxisSource.ACT)
                    return ActPos;
                else
                    return CmdPos;
            }
        }

        /// <summary>
        /// 实际坐标
        /// </summary>
        [Browsable(false)]
        [JsonIgnore]
        public double ActPos
        {
            get
            {
                this.GetAxisPos();
                return this.AxisEncPos / this.AxisRatio;
            }
        }

        /// <summary>
        /// 编码器坐标
        /// </summary>
        [Browsable(false)]
        [JsonIgnore]
        public double CmdPos
        {
            get
            {
                this.GetAxisPos();
                return this.AxisPrfPos / this.AxisRatio;
            }
        }
    }
}
