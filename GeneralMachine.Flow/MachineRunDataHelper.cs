using GeneralMachine.Config;
using GeneralMachine.Flow.Nodes;
using GeneralMachine.Flow.Step;
using GeneralMachine.Flow.Tool;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralMachine.Flow
{
    /// <summary>
    /// 吸嘴状态
    /// </summary>
    public enum NZ_State
    {
        /// <summary>
        /// 禁止使用
        /// </summary>
        ForbidUse,

        /// <summary>
        /// 没有被使用,可以使用
        /// </summary>
        NoUsed,

        /// <summary>
        /// 已吸取
        /// </summary>
        Sucked,

        /// <summary>
        /// 下视觉成功
        /// </summary>
        DownSuccessed,

        /// <summary>
        /// 下视觉失败
        /// </summary>
        DownFailed,

        /// <summary>
        /// 已贴附
        /// </summary>
        Pasted,
    }

    /// <summary>
    /// 运动过程中吸嘴的运行状态
    /// </summary>
    public class NZ_RunData
    {
        public NZ_State State = NZ_State.NoUsed;

        /// <summary>
        /// 小板号
        /// </summary>
        public int PCBIndex = -1;

        /// <summary>
        /// 穴位号
        /// </summary>
        public int PCSIndex = -1;

        /// <summary>
        /// 吸取料来自Feeder
        /// </summary>
        public Feeder SuckFeeder = Feeder.Left;

        /// <summary>
        /// 吸取起来的标签名称
        /// </summary>
        public string SuckLabel = string.Empty;

        /// <summary>
        /// 视觉状态
        /// </summary>
        public ResultItem DownVision = null;

        /// <summary>
        /// 贴附压力
        /// </summary>
        public double PastePress = 0;

        /// <summary>
        /// 吸取压力
        /// </summary>
        public double SuckPress = 0;

        /// <summary>
        /// 是否计算过贴附点
        /// </summary>
        public bool IsCalPastePt = false;

        /// <summary>
        /// 视觉计算结果
        /// </summary>
        public ResultItem Item = null;

        /// <summary>
        /// 真实贴附位
        /// </summary>
        public PointF RealPt = new PointF();

        /// <summary>
        /// 真实贴附角度
        /// </summary>
        public double RealAngle = 0;

        /// <summary>
        /// 重置参数
        /// </summary>
        public void Reset()
        {
            State = NZ_State.ForbidUse;
            PCBIndex = -1;
            PCSIndex = -1;
            SuckFeeder = Feeder.Left;
            RealPt = new PointF();
            RealAngle = 0;
            Item = null;
            IsCalPastePt = false;
            PastePress = 0;
            SuckPress = 0;
            SuckLabel = string.Empty;
        }
    }

    /// <summary>
    /// 每个pcs的状态
    /// </summary>
    public class Pcs_RunData
    {
        /// <summary>
        /// 上视觉补偿后坐标
        /// </summary>
        public PointF UpPastePt { get; set; } = new PointF();

        /// <summary>
        /// 贴附坐标
        /// </summary>
        public PointF RealPastePt { get; set; } = new PointF();

        /// <summary>
        /// 贴附吸嘴
        /// </summary>
        public Nozzle PasteNozzle { get; set; } = Nozzle.Nz1;

        /// <summary>
        /// 贴附信息标志
        /// -2: 已吸标需要贴标
        /// -1: 已抛料需要补料
        /// 0: 需要贴附
        /// 2: 已贴附
        /// 3: BadmarkFail
        /// 4: MarkFail
        /// </summary>
        public int iPasteState { get; set; } = 0;

        /// <summary>
        /// 上视觉补偿角度
        /// </summary>
        public double Angle = 0;

        /// <summary>
        /// 贴附压力
        /// </summary>
        public double PressValue = 0;

        /// <summary>
        /// 贴附时间
        /// </summary>
        public DateTime PasteTime = new DateTime();

        /// <summary>
        /// pcs条码
        /// </summary>
        public string PcsCode = string.Empty;

        /// <summary>
        /// 标签名称
        /// </summary>
        public string LabelName = string.Empty;

        /// <summary>
        /// 来自哪个Feeder
        /// </summary>
        public Feeder SuckFeeder = Feeder.Left;

        /// <summary>
        /// 当前料卷的第几个料
        /// </summary>
        public int IndexInLabels = -1;

        /// <summary>
        /// 来自feeder第几个光纤位置的料
        /// </summary>
        public int IndexInFeeder = -1;
    }

    /// <summary>
    /// mark点运行状态
    /// </summary>
    public class Mark_RunData
    {
        public bool Mark1IsCaled = false;
        public bool Mark2IsCaled = false;
        public PointF Mark1;
        public PointF Mark2;
        public double UpAngle = 0;
        public bool MarkSuccess = false;
    }

    /// <summary>
    /// 每个大板上的状态
    /// </summary>
    public class Board_RunData
    {
        public Pcs_RunData[] PcsData = null;
        public Mark_RunData MarkData = null;
        public Vision.VisionResult[] BadmarkData = null;
        public Vision.VisionResult[] CodeData = null;
        public Pcs_RunData this[int pcsIndex]
        {
            get
            {
                return PcsData[pcsIndex];
            }
        }
        public int PCSCount
        {
            get
            {
                return PcsData.Length;
            }
        }
        /// <summary>
        /// 设置点位
        /// </summary>
        /// <param name="list"></param>
        public void SetPos(PointF[] list)
        {
            for(int i =0; i< list.Length;++i)
            {
                this.PcsData[i].UpPastePt = list[i];
            }
        }
    }

    /// <summary>
    /// 机器运行数据处理中心
    /// 协助状态机对流程进行处理
    /// </summary>
    public class MachineRunDataHelper
    {
        public MachineRunDataHelper(Module module)
        {
            this.module = module;
        }

        /// <summary>
        /// 初始化运行时变量
        /// </summary>
        /// <param name="multiInfo"></param>
        public void Restet(MultiPasteInfo multiInfo)
        {
            RUN_CamMarkIsDone = false;
            RUN_CamBadmarkIsDone = false;
            RUN_CamPanelIsDone = false;
            RUN_CamCodeIsDone = false;

            RUN_NzData = new ConcurrentDictionary<Nozzle, NZ_RunData>();
            RUN_PCBData = new List<Board_RunData>();

            for (Nozzle nz = Nozzle.Nz1; nz <= Nozzle.Nz4; ++nz)
            {
                RUN_NzData.TryAdd(nz, new NZ_RunData());
            }

            for (int i = 0; i < multiInfo.PasteInfos.Count; ++i)
            {
                RUN_PCBData.Add(new Board_RunData());
                RUN_PCBData[i].MarkData = new Mark_RunData();
                RUN_PCBData[i].PcsData = new Pcs_RunData[multiInfo.PasteInfos[i].PasteList.Count];

                for (int j = 0; j < multiInfo.PasteInfos[i].PasteList.Count;++j)
                {
                    RUN_PCBData[i].PcsData[j] = new Pcs_RunData();
                    if (!multiInfo.PasteInfos[i].PasteList[j].CanPaste)
                        RUN_PCBData[i].PcsData[j].iPasteState = 3;
                    RUN_PCBData[i][j].UpPastePt = multiInfo.PasteInfos[i].PasteList[j].Pos;
                }
            }

            ChangePasteRegion?.Invoke(this.module);
            UpdateChart?.Invoke(this.module);
        }

        #region 固定不变变量
        private Module module = Module.Front;
        public MachineEntiy MachineEntiy
        {
            get
            {
                return SystemEntiy.Instance[this.module];
            }
        }
        #endregion

        #region 流程中间变量

        /// <summary>
        /// Panel Code 视觉结果
        /// </summary>
        public ResultItem RUN_PanelCode = null;

        /// <summary>
        /// 吸嘴状态
        /// </summary>
        public ConcurrentDictionary<Nozzle, NZ_RunData> RUN_NzData = new ConcurrentDictionary<Nozzle, NZ_RunData>();

        /// <summary>
        /// 
        /// </summary>
        private List<Board_RunData> RUN_PCBData = new List<Board_RunData>();

        /// <summary>
        /// 返回对应小板
        /// </summary>
        /// <param name="pcbIndex"></param>
        /// <returns></returns>
        public Board_RunData this[int pcbIndex]
        {
            get
            {
                return RUN_PCBData[pcbIndex];
            }
        }

        /// <summary>
        /// 大板数
        /// </summary>
        public int BoardCount
        {
            get
            {
                return RUN_PCBData.Count;
            }
        }

        /// <summary>
        /// 贴附信息标志
        /// -2: 已吸标需要贴标
        /// -1: 已抛料需要补料
        /// 0: 需要贴附
        /// 2: 已贴附
        /// 3: BadmarkFail
        /// 4: MarkFail
        /// </summary>
        //public ConcurrentDictionary<int, int[]> iPasteState = new ConcurrentDictionary<int, int[]>();

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="pcbIndex"></param>
        /// <param name="pcsIndex"></param>
        /// <param name="flag"></param>
        public void SetPasteState(int pcbIndex, int pcsIndex, int flag)
        {
            this.RUN_PCBData[pcbIndex].PcsData[pcsIndex].iPasteState = flag;

            UpdateChart?.Invoke(this.module);
        }

        /// <summary>
        /// 运行过程中,存储机台mark点计算出来的贴附点坐标
        /// </summary>
        //public ConcurrentDictionary<int, PointF[]> RUN_PasteList = new ConcurrentDictionary<int, PointF[]>();

        /// <summary>
        /// 运行过程中,存储机台计算出来mark点的坐标
        /// </summary>
        //public ConcurrentDictionary<int, Mark_RunData> RUN_MarkList = new ConcurrentDictionary<int, Mark_RunData>();

        /// <summary>
        /// 在该流程中各Feeder成功贴附次数 用于判断各Feeder分配
        /// </summary>
        //public ConcurrentDictionary<Feeder, int> RUN_FeederSuckCount = new ConcurrentDictionary<Feeder, int>();
        #endregion

        #region 流程步骤标志变量
        /// <summary>
        /// 该板中Badmark点步骤是否已经执行
        /// </summary>
        public bool RUN_CamMarkIsDone { get; set; } = false;
        /// <summary>
        /// 该板中Mark点步骤是否已经执行
        /// </summary>
        public bool RUN_CamBadmarkIsDone { get; set; } = false;
        /// <summary>
        /// 该板中PanelCode步骤是否已经执行
        /// </summary>
        public bool RUN_CamPanelIsDone { get; set; } = false;

        /// <summary>
        /// 该板中Code点步骤是否已经执行
        /// </summary>
        public bool RUN_CamCodeIsDone { get; set; } = false;
        #endregion

        public static event Action<Module> UpdateChart;
        public static event Action<Module> ChangePasteRegion;
    }
}
