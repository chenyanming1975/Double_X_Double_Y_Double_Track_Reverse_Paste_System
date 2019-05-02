using GeneralMachine.Common;
using GeneralMachine.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GeneralMachine.Report
{
    public enum StatisticType
    {
        /// <summary>
        /// 生产中
        /// </summary>
        Producte,
        /// <summary>
        /// 报警中
        /// </summary>
        DT,
        /// <summary>
        /// 等待生产中
        /// </summary>
        WaitProducte,
    }

    /// <summary>
    /// 报表模块
    /// 1. 启用数据库记录
    /// 2. 可按时间，分类查找
    /// 3. 可将 报表 导出成Excel 文件
    /// </summary>
    public class ReportHelper : Common.SingletionProvider<ReportHelper>
    {
        #region 机台实时状态统计功能
        /// <summary>
        /// 当天统计项
        /// </summary>
        [JsonIgnore]
        private Dictionary<Module, MachineStatistic> TodayReport = new Dictionary<Module, MachineStatistic>();

        [JsonIgnore]
        public MachineStatistic this[Module module]
        {
            get
            {
                if (!this.TodayReport.ContainsKey(module))
                    this.TodayReport.Add(module, new MachineStatistic(DateTime.Now));

                return TodayReport[module];

            }

            set
            {
                if (!this.TodayReport.ContainsKey(module))
                    this.TodayReport.Add(module, value);
                else
                    TodayReport[module] = value;
            }
        }
        #endregion

        #region 定时清零功能 只统计不查询
        /// <summary>
        /// 白天换班时间
        /// </summary>
        public TimeSpan DayShfitTime { get; set; } = new TimeSpan(8, 00, 0);

        /// <summary>
        /// 夜班换班时间
        /// </summary>
        public TimeSpan NightShfitTime { get; set; } = new TimeSpan(20, 00, 0);

        /// <summary>
        /// 上次清零时间
        /// </summary>
        public DateTime LastClearTime { get; set; } = new DateTime();

        /// <summary>
        /// 前模组统计存储路径
        /// </summary>
        public string ReportPath { get; set; } = @"D:/统计/";

        /// <summary>
        /// 检查是否换班 并 存储统计信息
        /// </summary>
        /// <param name="time"></param>
        public void ChangeShfit(DateTime time)
        {
            bool isDay = false;
            if (time.Hour >= ReportHelper.Instance.DayShfitTime.Hours &&
               time.Hour < ReportHelper.Instance.NightShfitTime.Hours)
            {
                isDay = true;
            }

            if (ReportHelper.Instance[Module.Front].IsDay != isDay)
            {
                SaveReport(Module.Front, time.AddHours(-1), ReportHelper.Instance[Module.Front]);
                ReportHelper.Instance[Module.Front] = new MachineStatistic(time);
            }

            if (ReportHelper.Instance[Module.After].IsDay != isDay)
            {
                SaveReport(Module.After, time.AddHours(-1), ReportHelper.Instance[Module.After]);
                ReportHelper.Instance[Module.After] = new MachineStatistic(time);
            }

            SaveReport(Module.Front, time, ReportHelper.Instance[Module.Front]);
            SaveReport(Module.After, time, ReportHelper.Instance[Module.After]);
        }
        #endregion

        #region 统计配置信息读取
        private static void SaveReport(Module module,DateTime time, MachineStatistic report)
        {
            bool isCurDay = true;
            bool isDay = true;
            if(time.Hour < ReportHelper.Instance.DayShfitTime.Hours) // 上一天的夜班
            {
                isDay = false;
                isCurDay = false;
            }
            else if(time.Hour >= ReportHelper.Instance.DayShfitTime.Hours &&
                time.Hour< ReportHelper.Instance.NightShfitTime.Hours)
            {
            }
            else if(time.Hour >= ReportHelper.Instance.NightShfitTime.Hours)
            {
                isDay = false;
            }

            string ss = isDay ? "白班" : "夜班";
            var curDay = time;
            if(!isCurDay)
            {
                curDay = time.AddDays(-1);
            }

            //CreatePath(curDay);
            var sP = $"{ReportHelper.Instance.ReportPath}{curDay.ToString("yyyy-MM-dd")}\\{module}\\{ss}.txt";
            Common.SerializableHelper<MachineStatistic> helper = new Common.SerializableHelper<MachineStatistic>(report);
            helper.JsonSerialize(sP);
        }

        private static MachineStatistic LoadReport(Module module, DateTime time)
        {
            bool isCurDay = true;
            bool isDay = true;
            if (time.Hour < ReportHelper.Instance.DayShfitTime.Hours) // 上一天的夜班
            {
                isDay = false;
                isCurDay = false;
            }
            else if (time.Hour >= ReportHelper.Instance.DayShfitTime.Hours &&
                time.Hour < ReportHelper.Instance.NightShfitTime.Hours)
            {
            }
            else if (time.Hour >= ReportHelper.Instance.NightShfitTime.Hours)
            {
                isDay = false;
            }

            string ss = isDay ? "白班" : "夜班";
            var curDay = time;
            if (!isCurDay)
            {
                curDay = time.AddDays(-1);
            }

            var sP = $"{ReportHelper.Instance.ReportPath}{curDay.ToString("yyyy-MM-dd")}\\{module}\\{ss}.txt";
            if (File.Exists(sP))
            {
                SerializableHelper<MachineStatistic> szHelper = new SerializableHelper<MachineStatistic>();
                var rep = szHelper.DeJsonSerialize(sP);
                if (rep != null)
                    return rep;
                else
                    return new MachineStatistic(DateTime.Now);
            }

            return new MachineStatistic(DateTime.Now);
        }
        public static void Save()
        {
            Common.SerializableHelper<ReportHelper> helper = new Common.SerializableHelper<ReportHelper>(ReportHelper.Instance);
            helper.JsonSerialize(PathDefine.sPathReport + "Report.json");
        }
        public static void CreatePath(DateTime time)
        {
            PathHelper.CreatePath(ReportHelper.Instance.ReportPath);
            PathHelper.CreatePath($"{ReportHelper.Instance.ReportPath}{time.ToString("yyyy-MM-dd")}");
            PathHelper.CreatePath($"{ReportHelper.Instance.ReportPath}{time.ToString("yyyy-MM-dd")}\\{Module.Front}");
            PathHelper.CreatePath($"{ReportHelper.Instance.ReportPath}{time.ToString("yyyy-MM-dd")}\\{Module.After}");
        }
        /// <summary>
        ///  只有程序加载的第一次可以使用
        /// </summary>
        public static void Load()
        {
            SerializableHelper<ReportHelper> helper = new SerializableHelper<ReportHelper>();
            var temp = helper.DeJsonSerialize(PathDefine.sPathReport + "Report.json");

            CreatePath(DateTime.Now);

            if (temp != null)
            {
                ReportHelper.Instance = temp;
            }
            ReportHelper.Instance[Module.Front] = LoadReport(Module.Front, DateTime.Now);
            ReportHelper.Instance[Module.After] = LoadReport(Module.After, DateTime.Now);
        }
        #endregion

        /// <summary>
        /// 机器实时状态  写入到CSV文件
        /// </summary>
        public class MachineStatistic
        {
            public MachineStatistic(DateTime time)
            {
                if (time.Hour >= ReportHelper.Instance.DayShfitTime.Hours && time.Hour < ReportHelper.Instance.NightShfitTime.Hours)
                    IsDay = true;
                else
                    IsDay = false;
            }

            public bool IsDay { get; set; } = true;

            #region 统计项
            /// <summary>
            /// 生产大板数
            /// </summary>
            public ulong PCBCount { get; set; } = 0;

            /// <summary>
            /// 生产小板数
            /// </summary>
            public ulong PCSCount { get; set; } = 0;

            /// <summary>
            /// 抛料率
            /// </summary>
            public double DropRate { get; set; } = 0;

            /// <summary>
            /// 报警数
            /// </summary>
            public ulong AlarmCount { get; set; } = 0;

            /// <summary>
            /// 吸嘴抛料数
            /// </summary>
            private Dictionary<Nozzle, ulong> ZDropCount { get; set; } = new Dictionary<Nozzle, ulong>();

            /// <summary>
            /// 抛料率
            /// </summary>
            /// <param name=""></param>
            /// <returns></returns>
            [JsonIgnore]
            public ulong this[Nozzle nz]
            {
                get
                {
                    if (ZDropCount.ContainsKey(nz))
                        return ZDropCount[nz];
                    else
                        return 0;
                }

                set
                {
                    if (!ZDropCount.ContainsKey(nz))
                        ZDropCount.Add(nz, value);
                    else
                        ZDropCount[nz] = value;
                }
            }

            /// <summary>
            /// 生产时间
            /// </summary>
            public TimeSpan ProductTime = new TimeSpan();

            /// <summary>
            /// DT时间
            /// </summary>
            public TimeSpan DTTime = new TimeSpan();

            /// <summary>
            /// 待产时间
            /// </summary>
            public TimeSpan WaitProductTime = new TimeSpan();
            #endregion

            #region 计时功能
            /// <summary>
            /// 当前计时状态
            /// </summary>
            public StatisticType CurType = StatisticType.WaitProducte;

            /// <summary>
            /// 开始计时时间
            /// </summary>
            public DateTime StartTime = DateTime.Now;

            /// <summary>
            /// 开始计时
            /// </summary>
            /// <param name="type"></param>
            public void StartTiming(StatisticType type, string msg, bool ime = false)
            {
                if (CurType != type || ime)
                {
                    var now = DateTime.Now;
                    var span = now - StartTime;
                    if (CurType == StatisticType.Producte)
                    {
                        this.ProductTime += span;
                        // csv [line-2] [开始时间] - [结束时间] -- [msg]
                    }
                    else if (CurType == StatisticType.DT)
                    {
                        this.DTTime += span;
                    }
                    else if (CurType == StatisticType.WaitProducte)
                    {
                        this.WaitProductTime += span;
                    }

                    CurType = type;
                    StartTime = now;
                    if (type == StatisticType.Producte)
                    {
                        // csv [line-1] [开始时间] - [] -- [msg]
                    }
                    else if (type == StatisticType.DT)
                    {
                    }
                    else if (type == StatisticType.WaitProducte)
                    {
                    }
                }
            }
            #endregion
        }
    }
}
