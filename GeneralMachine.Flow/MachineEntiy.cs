using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Motion;
using NationalInstruments.Vision;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Flow
{
    /// <summary>
    /// 回零事件
    /// </summary>
    public class HomeEventArgs:EventArgs
    {
        public HomeEventArgs()
        {
        }

        public HomeEventArgs(int rate, string msg)
        {
            this.ProgressRate = rate;
            this.Msg = msg;
        }

        public int ProgressRate = 0;
        public string Msg = string.Empty;
    }

    /// <summary>
    /// 机器模组实例
    /// </summary>
    public class MachineEntiy
    {
        public MachineEntiy(Module module)
        {
            this.Module = module;
        }

        #region 基本配置
        public Module Module
        {
            get;
            set;
        } = Module.Front;

        public MachineAxis MachineAxis
        {
            get
            {
                return AxisDefine.Instance.MachineAxis[this.Module];
            }
        }

        public MachineIO MachineIO
        {
            get
            {
                return IODefine.Instance.MachineIO[this.Module];
            }
        }

        public MachineConfig MachineConfig
        {
            get
            {
                return SystemConfig.Instance.Machines[this.Module];
            }
        }
        #endregion

        #region XY轴操作
        /// <summary>
        /// 获得XY坐标经过调整后的坐标
        /// </summary>
        public PointF XYPos
        {
            get
            {
                return XYPosAxis;
            }
        }

        public PointF XYPosAxis
        {
            get
            {
                return new PointF((float)MachineAxis.X.CmdPos, (float)MachineAxis.Y.CmdPos);
            }
        }

        public bool XYReach(PointF point)
        {
            return this.MachineAxis.X.AxisReach(point.X) && this.MachineAxis.Y.AxisReach(point.Y);
        }

        public PointF MachinePtToActPt(PointF machinePt)
        {
            PointContour actPt = new PointContour();
            actPt.X = machinePt.X * HardwareOrgHelper.Instance[this.Module].XRate;
            actPt.Y = machinePt.Y + machinePt.X * HardwareOrgHelper.Instance[this.Module].YRate;
            actPt = HardwareOrgHelper.Instance[this.Module].ToReal(actPt);
            return new PointF((float)actPt.X, (float)actPt.Y);
        }

        public PointF ActPtToMachinePt(PointF actPt)
        {
            PointContour machinePt = new PointContour();
            PointContour Pt = new PointContour(actPt.X, actPt.Y);
            Pt = HardwareOrgHelper.Instance[this.Module].ToMachine(Pt);
            machinePt.X = Pt.X / HardwareOrgHelper.Instance[this.Module].XRate;
            machinePt.Y = Pt.Y - machinePt.X * HardwareOrgHelper.Instance[this.Module].YRate;
            return new PointF((float)machinePt.X, (float)machinePt.Y);
        }

        public PointF GetPasteOffset(Nozzle nz, PointF upPt, PointF pastePt)
        {
            PointF realPt = pastePt;
            double p1, p2, p3, p4, os, os2;
            if (HardwareOrgHelper.Instance.HardWare[this.Module].XPoly != null)
            {
                p1 = HardwareOrgHelper.Instance.HardWare[this.Module].XPoly.Evaluate(upPt.X);
                p2 = HardwareOrgHelper.Instance.HardWare[this.Module].XPoly.Evaluate(this.MachineConfig[nz].UpMarkPt.X);
                os = p2 - p1;

                p3 = HardwareOrgHelper.Instance.HardWare[this.Module].XPoly.Evaluate(pastePt.X);
                p4 = HardwareOrgHelper.Instance.HardWare[this.Module].XPoly.Evaluate(this.MachineConfig[nz].PastePt.X);
                os2 = p3 - p4;
                realPt.X += (float)(os + os2);
            }

            if(HardwareOrgHelper.Instance.HardWare[this.Module].YPoly != null)
            {
                p1 = HardwareOrgHelper.Instance.HardWare[this.Module].YPoly.Evaluate(upPt.Y);
                p2 = HardwareOrgHelper.Instance.HardWare[this.Module].YPoly.Evaluate(this.MachineConfig[nz].UpMarkPt.Y);
                os = p2 - p1;

                p3 = HardwareOrgHelper.Instance.HardWare[this.Module].YPoly.Evaluate(pastePt.Y);
                p4 = HardwareOrgHelper.Instance.HardWare[this.Module].YPoly.Evaluate(this.MachineConfig[nz].PastePt.Y);
                os2 = p3 - p4;
                realPt.Y += (float)(os + os2);
            }

            return realPt;
        }

        /// <summary>
        /// 贴附点微调--根据机械误差补偿
        /// </summary>
        /// <param name="pastePt">贴附位置</param>
        /// <param name="markPt">mark点</param>
        /// <param name="nz">吸嘴</param>
        /// <returns>微调后坐标</returns>
        public PointF PastePtReject(PointF pastePt, PointF markPt, Nozzle nz)
        {
            return pastePt;
        }

        /// <summary>
        /// 安全检查
        /// </summary>
        /// <param name="shceme"></param>
        /// <returns></returns>
        public bool SafeCheck(Shceme shceme = Shceme.ManualNormal)
        {
            // 如果不在安全高度
            if (!this.ZReachSafe)
            {
                this.ZGoSafe(shceme);
                return false;
            }

            // 判断离谁最近,到最近的位置
            double trunPos = this.MachineAxis.Trun.Pos;
            double d1 = Math.Abs(trunPos - this.MachineConfig.TrunPasteAngle);
            double d2 = Math.Abs(trunPos - this.MachineConfig.TrunSuckAngle);

            if (d1 < d2)
            {
                if (d1 > 1)
                {
                    this.TurnGoPaste(shceme);
                    return false;
                }
            }
            else
            {
                if (d2 > 1)
                {
                    this.TurnGoSuck(shceme);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 软极限检测
        /// </summary>
        /// <param name="wrold"></param>
        /// <returns></returns>
        public bool LimitCheck(PointF wrold)
        {
            return this.MachineConfig.XLimit.InSafe(wrold.X) && this.MachineConfig.YLimit.InSafe(wrold.Y);
        }

        /// <summary>
        /// XY到指定区域
        /// </summary>
        /// <param name="wrold"></param>
        /// <param name="shceme"></param>
        /// <returns></returns>
        public short XYGoPos(PointF wrold, Shceme shceme = Shceme.ManualNormal)
        {
            PointF curPos = this.XYPos;
            if (SafeCheck(shceme) && LimitCheck(wrold))
            {

                short rtn = 0;
                if (curPos.X != wrold.X)
                {
                    var sdX = SpeedDefine.Instance[Module][curPos.X, wrold.X, shceme, GeneralAxis.X];
                    rtn += this.MachineAxis.X.GoPos(wrold.X, sdX);
                }

                float Y = wrold.Y;
                if (curPos.Y != wrold.Y && SystemEntiy.Instance.CanMoveY(this.Module, ref Y))
                {
                    wrold.Y = Y;
                    var sdY = SpeedDefine.Instance[Module][curPos.Y, wrold.Y, shceme, GeneralAxis.Y];
                    rtn += this.MachineAxis.Y.GoPos(wrold.Y, sdY);
                }

                return rtn;
            }

            return -1;          
        }
        public short XGoPos(double x, Shceme shceme = Shceme.ManualNormal)
        {
            PointF curPos = this.XYPos;
            curPos.X = (float)x;
            return this.XYGoPos(curPos, shceme);
        }

        public short YGoPos(double y, Shceme shceme = Shceme.ManualNormal)
        {
            PointF curPos = this.XYPos;
            curPos.Y = (float)y;
            return this.XYGoPos(curPos, shceme);
        }

        public short XYGoPosUI(PointF xyPos, Shceme sheme = Shceme.ManualNormal)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            short rtn = this.XYGoPos(xyPos);
            while (!this.XYReach(xyPos) && sw.ElapsedMilliseconds < 5000)
                CommonHelper.DoEvent(50);
            if (sw.ElapsedMilliseconds > 5000)
                rtn += -1;
            return rtn;
        }
        /// <summary>
        /// X Jog运动
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="shceme"></param>
        /// <returns></returns>
        public short XJog(bool dir, Shceme shceme = Shceme.ManualNormal)
        {
            if (SafeCheck(shceme))
            {
                return this.MachineAxis.X.Jog(SpeedDefine.Instance[Module][shceme, GeneralAxis.X], dir);
            }
            else
                return -1;
        }

        /// <summary>
        /// Y Jog运动
        /// </summary>
        /// <param name="nozzle"></param>
        /// <param name="pos"></param>
        /// <param name="shceme"></param>
        /// <returns></returns>
        public short YJog(bool dir, Shceme shceme = Shceme.ManualNormal)
        {
            if (SafeCheck(shceme))
            {
                return this.MachineAxis.Y.Jog(SpeedDefine.Instance[Module][shceme, GeneralAxis.Y], dir);
            }
            else
                return -1;
        }

        /// <summary>
        /// XY运动到指定位置，直到到位
        /// </summary>
        /// <param name="wrold"></param>
        /// <param name="shceme"></param>
        /// <returns></returns>
        public short XYGoPosTillStop(PointF wrold, Shceme shceme = Shceme.ManualNormal)
        {
            if (this.SafeCheck(shceme))
            {
                Task<short> task = Task<short>.Factory.StartNew(() => {
                    Stopwatch watch = new Stopwatch();
                    watch.Start(); 
                    short rtn = this.XYGoPos(wrold, shceme);
                    while(rtn == 0 && !this.XYReach(wrold))
                    {
                        CommonHelper.DoEvent(50);
                        if (watch.ElapsedMilliseconds > ConstDefine.iActionTimeout)
                        {
                            rtn = -1;
                        }
                    }
                    return rtn;
                });

                task.Wait();
                return task.Result;
            }

            return -1;
        }

        public void StopAllAxis(bool emg = true)
        {
            MachineAxis.X.Stop(emg);
            MachineAxis.Y.Stop(emg);
            MachineAxis.Z[0].Stop(emg);
            MachineAxis.Z[2].Stop(emg);
            MachineAxis.R[0].Stop(emg);
            MachineAxis.R[1].Stop(emg);
            MachineAxis.Trun.Stop(emg);
        }

        /// <summary>
        /// XY到安全位置
        /// </summary>
        /// <param name="shceme"></param>
        public void XYGoSafePt(Shceme shceme = Shceme.ManualNormal)
        {
            if(this.ZGoSafeTillStop(Shceme.ManualNormal) == 0)
            {
                this.XYGoPosTillStop(this.MachineConfig.ReadyPoint, shceme);
            }
        }
        #endregion

        #region Z轴操作
        public bool ZReachSafe
        {
            get
            {
                foreach(Nozzle nozzle in Enum.GetValues(typeof(Nozzle)))
                {
                    if (!this.MachineAxis.Z[(int)nozzle].AxisReach(MachineConfig.NozzleMap[nozzle].SafeHeight))
                        return false;
                }
                return true;
            }
        }

        public short ZGoSafe(Shceme shceme = Shceme.ManualNormal)
        {
            short rtn = 0;
            foreach(Nozzle nozzle in Enum.GetValues(typeof(Nozzle)))
            {
                if(nozzle == Nozzle.Nz1 || nozzle == Nozzle.Nz4)
                    rtn += this.ZGoSafe(nozzle, shceme);
            }
            return rtn;
        }

        public short ZJog(Nozzle nozzle, bool dir, Shceme shceme = Shceme.ManualNormal)
        {
            return this.MachineAxis.Z[(int)nozzle].Jog(SpeedDefine.Instance[Module][shceme, GeneralAxis.Z], dir);
        }

        public short ZGoPos(Nozzle nozzle, double pos,Shceme shceme = Shceme.ManualNormal)
        {
            short rtn = 0;
            double curPos = this.MachineAxis.Z[(int)nozzle].Pos;
            rtn += this.MachineAxis.Z[(int)nozzle].GoPos(pos, SpeedDefine.Instance[Module][curPos, pos, shceme, GeneralAxis.Z]);
            return rtn;
        }

        public short ZGoPaste(Nozzle nozzle, Shceme shceme = Shceme.ManualNormal, double pos = 0)
        {
            return this.ZGoPos(nozzle, (MachineConfig.NozzleMap[nozzle].PasteHeight+pos), shceme);
        }
        public short ZGoSuck(Nozzle nozzle, Shceme shceme = Shceme.ManualNormal, double pos = 0)
        {
            return this.ZGoPos(nozzle, (MachineConfig.NozzleMap[nozzle].XIHeight + pos), shceme);
        }

        public short ZGoThrow(Nozzle nozzle, Shceme shceme = Shceme.ManualNormal, double pos = 0)
        {
            return this.ZGoPos(nozzle, (MachineConfig.NozzleMap[nozzle].DropHeight + pos), shceme);
        }

        public short ZGoSafe(Nozzle nozzle, Shceme shceme = Shceme.ManualNormal)
        {
            return this.ZGoPos(nozzle, MachineConfig.NozzleMap[nozzle].SafeHeight, shceme);
        }

        public short ZGoPosTillStop(Nozzle nozzle, double pos, Shceme shceme = Shceme.ManualNormal)
        {
            short rtn = 0;
            double curPos = this.MachineAxis.Z[(int)nozzle].Pos;
            rtn += this.MachineAxis.Z[(int)nozzle].GoPosTillStop(ConstDefine.iActionTimeout, pos, SpeedDefine.Instance[Module][curPos, pos, shceme, GeneralAxis.Z]);
            return rtn;
        }

        public short ZGoSafeTillStop(Shceme shceme = Shceme.ManualNormal)
        {
            List<Task<short>> tasks = new List<Task<short>>();
            foreach (Nozzle nozzle in Enum.GetValues(typeof(Nozzle)))
            {
                tasks.Add(Task<short>.Factory.StartNew(() => {
                     return this.ZGoPosTillStop(nozzle, this.MachineConfig[nozzle].SafeHeight, shceme);
                }));
            }

            Task.WaitAll(tasks.ToArray());
            short rtn = 0;
            for(int i = 0; i < tasks.Count; ++i)
            {
                rtn += tasks[i].Result;
            }

            return rtn;
        }

        /// <summary>
        /// 吸嘴位转相机位
        /// </summary>
        /// <param name="nz"></param>
        /// <param name="nzPt"></param>
        /// <returns></returns>
        public PointF NzToLabel(Nozzle nz, PointF nzPt)
        {
            PointF label = new PointF();
            label.X = nzPt.X + this.MachineConfig[nz].NzToLabelDist.X;
            label.Y = nzPt.Y + this.MachineConfig[nz].NzToLabelDist.Y;
            return label;
        }

        /// <summary>
        /// 相机位转吸嘴位
        /// </summary>
        /// <param name="nz"></param>
        /// <param name="labelPt"></param>
        /// <returns></returns>
        public PointF LabelToNz(Nozzle nz, PointF labelPt)
        {
            PointF nzPt = new PointF();
            nzPt.X = labelPt.X - this.MachineConfig[nz].NzToLabelDist.X;
            nzPt.Y = labelPt.Y - this.MachineConfig[nz].NzToLabelDist.Y;
            return nzPt;
        }
        #endregion

        #region U轴相关操作
        public bool UReach(double[] angle)
        {
            bool reach = true;
            foreach (Nozzle nozzle in Enum.GetValues(typeof(Nozzle)))
            {
                reach &= this.MachineAxis.R[(int)nozzle].AxisReach(angle[(int)nozzle]);
            }

            return reach;
        }

        public short RGoAngle(double angle, Nozzle nozzle, Shceme shceme = Shceme.ManualNormal)
        {
            short rtn = 0;
            rtn += this.MachineAxis.R[(int)nozzle].GoPos(angle, SpeedDefine.Instance[Module][shceme, GeneralAxis.U]);
            return rtn;
        }

        /// <summary>
        /// R轴到初始化角度(慎用)
        /// </summary>
        /// <param name="nozzle"></param>
        /// <param name="shceme"></param>
        /// <returns></returns>
        public short RGoInit(Nozzle nozzle, Shceme shceme = Shceme.ManualNormal)
        {
            short rtn = 0;
            Axis_RunParam r = MachineAxis.R[(int)nozzle];
            rtn += r.GoPosTillStop(ConstDefine.iActionTimeout, this.MachineConfig.NozzleMap[nozzle].RInit, SpeedDefine.Instance[Module][shceme, GeneralAxis.U]);
            r.Stop();
            Thread.Sleep(100);
            r.ClearAxisSts();
            r.ZeroAxis();
            return rtn;
        }

        public short RGoAngleTillStop(double angle, Nozzle nozzle, Shceme shceme = Shceme.ManualNormal)
        {
            short rtn = 0;
            double curPos = this.MachineAxis.R[(int)nozzle].Pos;
            rtn += this.MachineAxis.R[(int)nozzle].GoPosTillStop(ConstDefine.iActionTimeout, angle, SpeedDefine.Instance[Module][curPos, angle, shceme, GeneralAxis.U]);
            return rtn;
        }

        public short RJog(Nozzle nozzle, bool dir,Shceme shceme = Shceme.ManualNormal)
        {
            return this.MachineAxis.R[(int)nozzle].Jog(SpeedDefine.Instance[Module][shceme, GeneralAxis.U], dir);
        }
        #endregion

        #region 翻转轴相关操作

        // Turn 轴在安全位置
        public bool TurnReachSafe
        {
            get
            {
                return this.MachineAxis.Trun.AxisReach(this.MachineConfig.TrunPasteAngle) || this.MachineAxis.Trun.AxisReach(this.MachineConfig.TrunSuckAngle);
            }
        }

        public bool TurnReachPaste
        {
            get
            {
                return this.MachineAxis.Trun.AxisReach(this.MachineConfig.TrunPasteAngle);
            }
        }

        public bool TurnReachSuck
        {
            get
            {
                return this.MachineAxis.Trun.AxisReach(this.MachineConfig.TrunSuckAngle);
            }
        }

        public short TurnGoPos(double pos, Shceme shceme = Shceme.ManualNormal)
        {
            if (this.ZReachSafe)
                return this.MachineAxis.Trun.GoPos(pos, SpeedDefine.Instance[Module][shceme, GeneralAxis.TRUN]);
            else
            {
                this.ZGoSafe();
                return -1;
            }
        }

        public short TurnJog(bool dir, Shceme shceme = Shceme.ManualNormal)
        {
            if (this.ZReachSafe)
                return this.MachineAxis.Trun.Jog(SpeedDefine.Instance[Module][shceme, GeneralAxis.TRUN], dir);
            else
            {
                this.ZGoSafe();
                return -1;
            }
        }

        public short TurnGoPaste(Shceme shceme = Shceme.ManualNormal)
        {
            return this.TurnGoPos(this.MachineConfig.TrunPasteAngle, shceme);
        }

        public short TurnGoSuck(Shceme shceme = Shceme.ManualNormal)
        {
            return this.TurnGoPos(this.MachineConfig.TrunSuckAngle, shceme);
        }
        #endregion

        #region 校验结果获取相关
        /// <summary>
        /// 获得实际坐标
        /// </summary>
        /// <param name="camera">输入-相机</param>
        /// <param name="capturePt">输入-拍照位置</param>
        /// <param name="imagePt">输入-图像坐标</param>
        /// <param name="wroldPt">输出-实际坐标</param>
        /// <returns></returns>
        public bool WroldPt(Camera camera,PointF capturePt, PointContour imagePt, out PointF wroldPt)
        {
            wroldPt = new PointF();

            try
            {
                PointF pt1 = CameraDefine.Instance.CameraList[this.Module][camera].Mat2D[0].Pixel2World(imagePt);
                wroldPt.X = capturePt.X - (pt1.X - CameraDefine.Instance.CameraList[this.Module][camera].Mat2D[0].CliabCenter.X);
                wroldPt.Y = capturePt.Y - (pt1.Y - CameraDefine.Instance.CameraList[this.Module][camera].Mat2D[0].CliabCenter.Y);
                return true;
            }
            catch { return false; }
            //return 
        }

        /// <summary>
        /// 根据吸嘴获得实际坐标
        /// </summary>
        /// <param name="nz">输入-吸嘴</param>
        /// <param name="capturePt"></param>
        /// <param name="imagePt"></param>
        /// <param name="wroldPt"></param>
        /// <returns></returns>
        public bool WroldPt(Nozzle nz, PointF capturePt, PointContour imagePt, out PointF wroldPt)
        {
            Camera camera = Camera.Bottom1;
            if (nz == Nozzle.Nz3 || nz == Nozzle.Nz4)
            {
                camera = Camera.Bottom2;
            }
            int i = 0;
            if (nz == Nozzle.Nz2 || nz == Nozzle.Nz4)
            {
                i = 1;
            }

            wroldPt = new PointF();

            try
            {
                PointF pt1 = CameraDefine.Instance.CameraList[this.Module][camera].Mat2D[i].Pixel2World(imagePt);
                wroldPt = pt1;
                wroldPt.X = capturePt.X - (pt1.X - CameraDefine.Instance.CameraList[this.Module][camera].Mat2D[i].CliabCenter.X);
                wroldPt.Y = capturePt.Y - (pt1.Y - CameraDefine.Instance.CameraList[this.Module][camera].Mat2D[i].CliabCenter.Y);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 相机 转 吸嘴坐标
        /// </summary>
        /// <param name="camPt"></param>
        /// <returns></returns>
        public PointF CamToNozzle(Nozzle nozzle,PointF camPt)
        {
            PointF cur = this.XYPos;
            cur.X = camPt.X - this.MachineConfig[nozzle].NzToCam.X;
            cur.Y = camPt.Y - this.MachineConfig[nozzle].NzToCam.Y;
            return cur;
        }

        public PointF NozzleToCam(Nozzle nozzle,PointF nzPt)
        {
            PointF cur = this.XYPos;
            cur.X = nzPt.X + this.MachineConfig[nozzle].NzToCam.X;
            cur.Y = nzPt.Y + this.MachineConfig[nozzle].NzToCam.Y;
            return cur;
        }

        /// <summary>
        /// 求旋转中心过后的点
        /// </summary>
        /// <param name="nz"></param>
        /// <param name="imagePt"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public PointF RotatePtDown(Nozzle nz,PointContour imagePt, double angle)
        {
            var rotatedPt = Vision.VisionHelper.PtRotate(imagePt, this.MachineConfig[nz].RotatePoint, angle);

            PointF wroldPt = new PointF();
            if(!this.WroldPt(nz, this.MachineConfig[nz].RotateCamPoint, rotatedPt, out wroldPt))
            {
                return this.MachineConfig[nz].RotateCamPoint;
            }

            return wroldPt;
        }

        public VisionImage GrabImage(Camera camera)
        {
            return CameraDefine.Instance.Camera[this.Module][camera].Grab();
        }

        public VisionImage SnapImage(Camera camera)
        {
            return CameraDefine.Instance.Camera[this.Module][camera].Snap();
        }
        #endregion

        #region 回原点操作
        /// <summary>
        /// 机器开始回零点
        /// </summary>
        /// <returns></returns>
        public Task<short> MachineHome()
        {
            return Task<short>.Factory.StartNew(() => {
                #region 回零
                this.MachineIO.ResetBtnLight.SetIO(true);
                Thread.Sleep(1000);
                HomeEvent?.Invoke(this, new HomeEventArgs(30, "机器开始回零...."));
                MachineIO.TrunServoOn.SetIO(true);
                HomeEvent?.Invoke(this, new HomeEventArgs(40, "Z轴开始回零")); ;

                short rtn = 0;
                Task<short> task1 = Task<short>.Factory.StartNew(() =>
                {
                    return this.MachineAxis.Z[0].GoHome(ConstDefine.iHomeTime, SpeedDefine.Instance[Module][Shceme.GoLimit, GeneralAxis.Z],
             SpeedDefine.Instance[Module][Shceme.Home, GeneralAxis.Z], this.MachineIO.Limit[0]);
                });

                Task<short> task2 = Task<short>.Factory.StartNew(() =>
                {
                     return this.MachineAxis.Z[2].GoHome(ConstDefine.iHomeTime, SpeedDefine.Instance[Module][Shceme.GoLimit, GeneralAxis.Z],
                    SpeedDefine.Instance[Module][Shceme.Home, GeneralAxis.Z], this.MachineIO.Limit[1]);
                });

                // U轴回原点
                Task<short>[] tasks = new Task<short>[4];
                for (int i = 0; i < 4; ++i)
                {
                    int nz = i;
                    tasks[i] = Task<short>.Factory.StartNew(() =>
                    {
                        return this.MachineAxis.R[nz].GoHome(ConstDefine.iHomeTime, SpeedDefine.Instance[Module][Shceme.GoLimit, GeneralAxis.U],
                        SpeedDefine.Instance[Module][Shceme.Home, GeneralAxis.U], false);
                    });
                }

                task1.Wait();
                task2.Wait();

                HomeEvent?.Invoke(this, new HomeEventArgs(45, "Z轴回零完成,到安全高度中...")); ;
                Thread.Sleep(200);
                this.MachineAxis.Z[0].ZeroAxis();
                this.MachineAxis.Z[2].ZeroAxis();
                Thread.Sleep(100);
                if (task1.Result != 0)
                {
                    MsgHelper.Instance.WriteLog(MsgLevel.Error, "Z1轴 回原点失败,请检查,Z1轴感应器");
                    return task1.Result;
                }
                else if (task2.Result != 0)
                {
                    MsgHelper.Instance.WriteLog(MsgLevel.Error, "Z3轴 回原点失败,请检查,Z2轴感应器");
                    return task2.Result;
                }
                else
                {
                    MsgHelper.Instance.WriteLog(MsgLevel.Info, "Z轴 回原点成功");
                }

                Thread.Sleep(500);
                // Z轴到安全高度
                rtn += this.ZGoSafeTillStop(Shceme.ManualNormal);
                if (rtn != 0)
                {
                    HomeEvent?.Invoke(this, new HomeEventArgs(100, "Z轴到安全高度失败"));
                    return rtn;
                }
                else
                    HomeEvent?.Invoke(this, new HomeEventArgs(40, "Z轴到安全高度成功"));

                // X轴回原点
                rtn += MachineAxis.X.GoHome(ConstDefine.iHomeTime, SpeedDefine.Instance[Module][Shceme.GoLimit, GeneralAxis.X],
                SpeedDefine.Instance[Module][Shceme.Home, GeneralAxis.X]);
                if (rtn != 0)
                {
                    HomeEvent?.Invoke(this, new HomeEventArgs(100, "X轴回原点失败"));
                    return rtn;
                }
                else
                    HomeEvent?.Invoke(this, new HomeEventArgs(50, "X轴回原点成功"));

                // 翻转轴回原点
                rtn += MachineAxis.Trun.GoHome(ConstDefine.iHomeTime, SpeedDefine.Instance[Module][Shceme.GoLimit, GeneralAxis.TRUN],
               SpeedDefine.Instance[Module][Shceme.Home, GeneralAxis.TRUN]);
                if (rtn != 0)
                {
                    HomeEvent?.Invoke(this, new HomeEventArgs(100, "翻转轴回原点失败"));
                    return rtn;
                }
                else
                    HomeEvent?.Invoke(this, new HomeEventArgs(60, "翻转轴回原点成功"));

                // 翻转轴到水平位置
                rtn += MachineAxis.Trun.GoPosTillStop(ConstDefine.iHomeTime, MachineConfig.TrunSuckAngle, SpeedDefine.Instance[Module][Shceme.ManualNormal, GeneralAxis.TRUN]);
                if (rtn != 0)
                {
                    HomeEvent?.Invoke(this, new HomeEventArgs(100, "翻转轴到水平位置失败"));
                    return rtn;
                }
                else
                    HomeEvent?.Invoke(this, new HomeEventArgs(70, "翻转轴到水平位置成功"));

                // Y轴回原点
                rtn += MachineAxis.Y.GoHome(ConstDefine.iHomeTime, SpeedDefine.Instance[Module][Shceme.GoLimit, GeneralAxis.Y],
                    SpeedDefine.Instance[Module][Shceme.Home, GeneralAxis.Y]);
                if (rtn != 0)
                {
                    HomeEvent?.Invoke(this, new HomeEventArgs(100, "Y轴回原点失败"));
                    return rtn;
                }
                else
                    HomeEvent?.Invoke(this, new HomeEventArgs(80, "Y轴到回原点成功"));

                if (!Task.WaitAll(tasks, 2000))
                {
                    HomeEvent?.Invoke(this, new HomeEventArgs(100, "U轴回原点失败"));
                    return -1;
                }
                else
                    HomeEvent?.Invoke(this, new HomeEventArgs(90, "U轴回原点成功"));

                HomeEvent?.Invoke(this, new HomeEventArgs(100, "机器回零成功"));
                return rtn;
                #endregion
            });
        }
        #endregion

        #region 机器事件
        public event EventHandler<HomeEventArgs> HomeEvent;
        #endregion

        #region 机器IO点监控
        private Stopwatch watchBtn = new Stopwatch();
        public void MachineIOMointor(object sender, EventArgs args)
        {
            return;
            if(SystemEntiy.Instance.FlowMachine[this.Module].Pasued)
            {
                if (!this.MachineIO.SafeDoor.GetIO())
                {
                    SafeDoorOpen?.Invoke(true);// 蜂鸣器提示
                    this.StopAllAxis(false);
                }
                else
                {
                    SafeDoorOpen?.Invoke(false);// 蜂鸣器提示
                }

                #region 按钮操作
                if (this.MachineIO.StartBtn.GetIO())
                {
                    if(!watchBtn.IsRunning)
                    {
                        watchBtn.Restart();
                    }

                    if(watchBtn.ElapsedMilliseconds > 500)
                    {
                        ModuleStart.Invoke(this.Module);
                        Thread.Sleep(100);
                        watchBtn.Stop();
                        watchBtn.Reset();
                    }
                }
                else if(this.MachineIO.ResetBtn.GetIO())
                {
                    if (!watchBtn.IsRunning)
                    {
                        watchBtn.Restart();
                    }

                    if (watchBtn.ElapsedMilliseconds > 500)
                    {
                        ModuleReset.Invoke(this.Module);
                        Thread.Sleep(100);
                        watchBtn.Stop();
                        watchBtn.Reset();
                    }
                }
                else
                {
                    watchBtn.Stop();
                    watchBtn.Reset();
                }
                #endregion
            }
            else
            {
                if(this.MachineIO.StopBtn.GetIO())
                {
                    ModuleStop.Invoke(this.Module);
                }
            }
        }

        public static event Action<bool> SafeDoorOpen;

        public static event Action<Module> ModuleStart;

        public static event Action<Module> ModuleReset;

        public static event Action<Module> ModuleStop;
        #endregion
    }
}
