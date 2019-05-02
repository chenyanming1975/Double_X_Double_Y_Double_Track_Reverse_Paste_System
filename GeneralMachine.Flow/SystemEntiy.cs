using GeneralMachine.Config;
using GeneralMachine.Definition;
using GeneralMachine.Motion;
using GeneralMachine.Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow
{
    public class SystemErrorEventArg : EventArgs
    {
        public string Error = string.Empty;
    }

    /// <summary>
    /// 系统
    /// </summary>
    public class SystemEntiy : Common.SingletionProvider<SystemEntiy>
    {
        public SystemEntiy()
        {
        }

        public Dictionary<Module, MachineEntiy> Entiys = new Dictionary<Module, MachineEntiy>();
        public Dictionary<Module, StateMachine> FlowMachine = new Dictionary<Module, StateMachine>();

        /// <summary>
        /// 获得对应模组的信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public MachineEntiy this[Module module]
        {
            get
            {
                return Entiys[module];
            }
        }

        /// <summary>
        /// 返回对应模组使用的轨道
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public TrackEntiy GetTrack(Module module)
        {
            return TrackManager.Instance.TrackEntiy[(Config.Track)module];
        }


        public Dictionary<Module, float> YPlanPos = new Dictionary<Module, float>
        {
            { Module.Front, 0},
            { Module.After, 0},
        };

        /// <summary>
        /// 哪一个模组正在贴附区域  -1:无轴进入 0:前模组进入 1:后模组进入
        /// </summary>
        public int ModuleInWork = -1;

        public object YLock = new object();

        /// <summary>
        /// 改模组Y是否可以移动到该位置
        /// </summary>
        /// <param name="module"></param>
        /// <param name="YPos"></param>
        /// <returns></returns>
        public bool CanMoveY(Module module, ref float YPos)
        {
            Module Other = (Module)(((int)module + 1) % 2);
            if (this.FlowMachine[module].Pasued)
            {
                #region 手动移动时
                double otherY = this.Entiys[Other].XYPos.Y;
                if ((otherY + YPos) <= SystemConfig.Instance.General.YMaxSafe)
                    return true;
                else
                    return false;
                #endregion
            }
            else
            {
                #region 自动移动时
                lock (YLock)
                {
                    if (YPos < this.Entiys[module].MachineConfig.YWorkRange.LowerLimit) // 小于工作区域
                    {
                        if (this.ModuleInWork == (int)module)
                            ModuleInWork = -1;
                        YPlanPos[module] = YPos;
                        return true;
                    }
                    else if (YPos > this.Entiys[module].MachineConfig.YWorkRange.UpperLimit) // 大于工作区域
                    {
                        return false;
                    }
                    else // 在工作区域内
                    {
                        YPlanPos[module] = YPos;

                        if (this.ModuleInWork == -1) // 工作区域内是否无轴了
                            this.ModuleInWork = (int)module;

                        if (this.FlowMachine[Other].Pasued) // 暂停的那个轴位主动轴
                        {
                            this.ModuleInWork = (int)Other;
                            YPlanPos[Other] = this.Entiys[Other].XYPos.Y;
                        }

                        if ((YPos + YPlanPos[Other]) <= SystemConfig.Instance.General.YMaxSafe) // 可以直接去
                        {
                            YPlanPos[module] = YPos; // 等待从动轴避让完成再动
                            return true;
                        }
                        else
                        {
                            if (this.ModuleInWork == (int)module) // 是否时主动轴
                            {
                                YPlanPos[module] = YPos;
                                return false;
                            }
                            else
                            {
                                YPos = SystemConfig.Instance.General.YMaxSafe - YPlanPos[Other] - 5;
                                if (YPos < this.Entiys[module].MachineConfig.YLimit.LowerLimit) // 避让不能超过Y轴软极限
                                    YPos = (float)this.Entiys[module].MachineConfig.YLimit.LowerLimit;
                                YPlanPos[module] = YPos;

                                return true;
                            }
                        }
                    }
                }
                #endregion
            }
        }


        #region 负责加载程式，优化路径
        #endregion

        #region 负责加载配置文件
        public int SystemInit()
        {
            try
            {
                if (!SystemConfig.Load())
                {
                    throw new Exception("系统配置文件");
                }
                if (!SpeedDefine.Load())
                {
                    throw new Exception("速度配置文件");
                }

                if (!CameraDefine.Load())
                {
                    throw new Exception("相机配置文件");
                }

                if(!IODefine.Load())
                {
                    throw new Exception("IO配置文件");
                }

                if (!FeederDefine.Load())
                {
                    throw new Exception("Feeder配置文件");
                }

                if (!HardwareOrgHelper.Load())
                {
                    throw new Exception("机械校验文件");
                }

                if (!AxisDefine.Load())
                {
                    throw new Exception("轴卡配置文件");
                }
            }
            catch (Exception ex)
            {
                return Error.Invoke(new SystemErrorEventArg { Error = $"加载参数失败:[{ex.Message}]!!!" });
            }

            bool rtn = MotionHelper.Instance.InitCard(PathDefine.sPathCard);
            if (!rtn)
            {
                return Error.Invoke(new SystemErrorEventArg { Error = "轴卡初始化失败!!!" });
            }

            Task<string> result = Task<string>.Factory.StartNew(() =>
            {
                return CameraDefine.Instance.CameraConnected();
            });

            Axis_Advantech.DisableCamDO();
            MotionHelper.Instance.ResetAllOuput();
            MotionHelper.Instance.StartMointorIO();
            Track.TrackManager.Instance.TrackInit();
            Thread.Sleep(100);

            if (MotionHelper.Instance.bServoWarning)
            {
                return Error.Invoke(new SystemErrorEventArg { Error = "伺服报警,请重新上电再复位!!!" });
            }

            if (MotionHelper.Instance.bEmg)
            {
                return Error.Invoke(new SystemErrorEventArg { Error = "急停按下,请打开急停重新上电再复位!!!" });
            }

            // 初始化实例

            for(Module module = Module.Front; module <= Module.After; ++module)
            {
                Entiys.Add(module, new MachineEntiy(module));
                this.FlowMachine.Add(module, new StateMachine(module));
                Module index = module;
                Task.Factory.StartNew(() => {
                    IODefine.Instance.MachineIO[index].Init();
                    IODefine.Instance.TrackIO[(Config.Track)(index)].Init();
                    if(index == Module.Front)
                        IODefine.Instance.OtherIO.Init();
                });
            }


            result.Wait();
            if(result.Result != string.Empty)
            {
                return Error.Invoke(new SystemErrorEventArg { Error = result.Result });
            }
            else
            {
                MotionHelper.Instance.MachineIOMointor.Tick += Entiys[Module.Front].MachineIOMointor;
                MotionHelper.Instance.MachineIOMointor.Tick += Entiys[Module.After].MachineIOMointor;
            }

            return 0;
        }

        public void SystemHome(Form fm)
        {
            //MK:0426初始化屏蔽01
            //this.WorkStatus = WorkStatus.UnReset;
            this.WorkStatus = WorkStatus.FrontReseted;
            this.WorkStatus = WorkStatus.AfterReseted;

            //MK:0426初始化屏蔽02
            //this.SystemHomeAsync(Module.Front);
            //this.SystemHomeAsync(Module.After);
            //fm.ShowDialog();
        }

        public async void SystemHomeAsync(Module module)
        {
            this.Entiys[module].MachineIO.ResetBtnLight.SetIO(true);
            this.Entiys[module].MachineIO.StopBtnLight.SetIO(false);
            this.Entiys[module].MachineIO.StartBtnLight.SetIO(false);

            await Entiys[module].MachineHome();

            this.Entiys[module].MachineIO.ResetBtnLight.SetIO(false);
            this.Entiys[module].MachineIO.StopBtnLight.SetIO(true);

            if (module == Module.Front)
                this.WorkStatus |= WorkStatus.FrontReseted;
            else
                this.WorkStatus |= WorkStatus.AfterReseted;
            HomeFinished?.Invoke(this, module);
        }

        public void SystemExit()
        {
            MotionHelper.Instance.bSystemExit = true;
            TrackManager.Instance.TrackExit(Config.Track.ForntTrack);
            TrackManager.Instance.TrackExit(Config.Track.AfterTrack);
            Thread.Sleep(100);
            MotionHelper.Instance.ResetAllOuput();
            Thread.Sleep(100);
            Axis_Advantech.CardClose();
        }
        #endregion

        #region 系统当前状态
        public WorkStatus WorkStatus
        {
            get;
            set;
        } = WorkStatus.UnReset;

        #endregion

        #region 事件

        public Func<SystemErrorEventArg,int> Error;

        /// <summary>
        /// 开始回原点
        /// </summary>
        public event EventHandler<Module> StartGoHome;

        /// <summary>
        /// 回原点完成
        /// </summary>
        public event EventHandler<Module> HomeFinished;
        #endregion
    }

}
