using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.SpreadSheet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static GeneralMachine.Common.CommonHelper;

namespace GeneralMachine.Common
{
    /// <summary>
    /// 错误类型
    /// </summary>
    public enum StringCode
    {
        [EnumDescription("机器运行")]
        MachineStart,
        [EnumDescription("机器暂停")]
        MachinePause,
        [EnumDescription("机器停止")]
        MachineStop,
        [EnumDescription("进板")]
        InputPCB,
        [EnumDescription("出板")]
        OutPCB,
        [EnumDescription("吸标")]
        SuckLabel,
        [EnumDescription("下视觉")]
        DownMark,
        [EnumDescription("飞拍")]
        FlyMark,
        [EnumDescription("点拍")]
        PointMark,
        [EnumDescription("贴标")]
        PasteLabel,
        [EnumDescription("流程准备")]
        FlowReady,
        [EnumDescription("伺服报警")]
        ServoWarning,
        [EnumDescription("到达硬极限")]
        LimitWarning,
        [EnumDescription("急停报警")]
        EmgWarning,
        [EnumDescription("Z轴回安全高度失败")]
        ZGoSafeError,
        [EnumDescription("到吸料高度超时")]
        ZGoSuckPosTimeout,
        [EnumDescription("出料超时")]
        FeederTimeout,
        [EnumDescription("下视觉报警超时")]
        DownPhotoTimeout,
        [EnumDescription("寻找吸标失败")]
        SuckLabelFindError,
        [EnumDescription("连续吸料失败报警")]
        SuckLabelFailed,
        [EnumDescription("没有找到吸标点")]
        NoFindSuckPos,
        [EnumDescription("没有找到下视觉点位")]
        NoFindDownVisionPos,
        [EnumDescription("没有找到上视觉点位")]
        NoFindUpVisionPos,
        [EnumDescription("没有找到贴标点位")]
        NoFindPastePos,
    }

    /// <summary>
    /// 消息等级
    /// </summary>
    public enum MsgLevel
    {
        /// <summary>
        /// 只输出到文件
        /// </summary>
        [EnumDescription("调试级")]
        Debug,

        /// <summary>
        /// 需要输出到UI界面
        /// </summary>
        [EnumDescription("信息级")]
        Info,

        /// <summary>
        /// 需要报警 - 提示不停止
        /// </summary>
        [EnumDescription("警告级")]
        Warn,

        /// <summary>
        /// 需要报警 - 提示单边停机
        /// </summary>
        [EnumDescription("错误级")]
        Error,

        /// <summary>
        /// 需要报警 - 提示双边停机
        /// </summary>
        [EnumDescription("致命级")]
        Fatal,
    }

    /// <summary>
    /// 语言
    /// </summary>
    public enum Language
    {
        简体中文,
        繁體中文,
        English,
    }

    /// <summary>
    /// 机器字符加载
    /// </summary>
    public class MsgHelper : SingletionProvider<MsgHelper>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public void Intialize()
        {
            this.InitializeLog();
            this.ErrLock[0] = new object();
            this.ErrLock[1] = new object();
            this.ErrLock[2] = new object();

            WriteLog(MsgLevel.Info, "机器启动...");
            WriteLog(MsgLevel.Info, "正在载入相关字符定义");

            this.Start();

            if (!File.Exists("./Configure/机器字符定义.xls"))
            {
                WriteLog(MsgLevel.Info, "字符定义载入失败,没有找到定义文件");
                return;
            }
            // 读取Excel进行设置ErrorCode的定义
            CompoundDocument doc = CompoundDocument.Open("./Configure/机器字符定义.xls");
            byte[] bookdata = doc.GetStreamData("Workbook");
            if (bookdata == null)
            {
                return;
            }
            using (var ms = new MemoryStream(bookdata))
            {
                Workbook book = WorkbookDecoder.Decode(ms);
                Worksheet sheet = book.Worksheets[0];

                int rowCount = sheet.Cells.LastRowIndex + 1;
                int colCount = sheet.Cells.LastColIndex + 1;
                for (int i = 0; i < colCount; ++i)
                {
                    DataColumn dt = new DataColumn(sheet.Cells[0, i].StringValue);
                    stringDB.Columns.Add(dt);
                }

                string[] rowValue = new string[colCount];
                for (int row = 1; row < rowCount; row++)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        rowValue[col] = sheet.Cells[row, col].StringValue;
                    }

                    stringDB.Rows.Add(rowValue);
                }
            }
            WriteLog(MsgLevel.Info, "字符定义载入成功");
        }

        #region Log对象
        /// <summary>
        /// UI 操作保存日志
        /// </summary>
        private log4net.ILog basicLog = null;

        /// <summary>
        /// 错误信息的日志
        /// </summary>
        private log4net.ILog errLog = null;

        private void InitializeLog()
        {
            basicLog = log4net.LogManager.GetLogger("BasicLog");
            errLog = log4net.LogManager.GetLogger("ErrorLog");
        }

        /// <summary>
        /// Log写入
        /// </summary>
        /// <param name="logBean"></param>
        public void WriteLog(MsgLevel level, string message)
        {
            switch (level)
            {
                case MsgLevel.Debug:
                    basicLog?.Debug(message);
                    break;

                case MsgLevel.Info:
                    basicLog?.Info(message);
                    break;

                case MsgLevel.Warn:
                    basicLog?.Warn(message);
                    break;

                case MsgLevel.Error:
                    errLog?.Error(message);
                    break;

                case MsgLevel.Fatal:
                    errLog?.Fatal(message);
                    break;
            }
        }
        #endregion

        #region 字加载
        private DataTable stringDB = new DataTable();
        public Language Language = Language.简体中文;

        /// <summary>
        /// 获得字符定义
        /// </summary>
        /// <param name="define">定义</param>
        /// <returns></returns>
        private string GetResource(StringCode code,  Language language)
        {
            var item = from model in this.stringDB.AsEnumerable()
                       where model.Field<string>("StringCode") == code.ToString()
                       select model.Field<string>(language.ToString());
            string result = string.Empty;
            if (item.Count() > 0)
            {
                result = item.First();
            }
            else
            {
                result = CommonHelper.GetEnumDescription(code.GetType(), code);
            }

            return result;
        }
        #endregion

        #region 消息输出接口
        public void AddMessage(MsgLevel level,string debug, int module = -1)
        {
            HostarLogBean bean = new HostarLogBean(level, debug, module);
            this.msgQueue.Enqueue(bean);
        }

        public void AddMessage(MsgLevel level, StringCode code, int module  = - 1, params object[] param)
        {
            string str = this.GetResource(code, Language);
            if (param != null)
                str = string.Format(str, param);
            HostarLogBean bean = new HostarLogBean(level, str, module);

            if(bean.LogLevel >= MsgLevel.Warn)
            {
                lock(this.ErrLock)
                {
                    this.msgQueue.Enqueue(bean);
                }
            }
            else
                this.msgQueue.Enqueue(bean);
        }

        /// <summary>
        /// 消息队列
        /// </summary>
        private ConcurrentQueue<HostarLogBean> msgQueue = new ConcurrentQueue<HostarLogBean>();
        #endregion

        #region 输出线程

        public void Start()
        {
            if(isTerminal)
            {
                isTerminal = false;
                Task.Factory.StartNew(Run);
            }
        }

        public void Stop()
        {
            isTerminal = true;
        }

        private bool isTerminal = true;
        public object[] ErrLock = new object[3];

        private void Run()
        {
            while(!isTerminal)
            {
                if(this.msgQueue.IsEmpty)
                {
                    Thread.Sleep(5);
                }
                else
                {
                    HostarLogBean bean = null;
                    this.msgQueue.TryDequeue(out bean);
                    if(bean != null)
                    {
                        string log = bean.Message;
                        if (bean.Module == 0) { log = "[前模组]-" + bean.Message; }
                        else if (bean.Module == 1) { log = "[后模组]-" + bean.Message; }

                        switch (bean.LogLevel)
                        {
                            case MsgLevel.Debug:
                                basicLog?.Debug(log);
                                break;
                            case MsgLevel.Info:
                                basicLog?.Info(log);
                                InfoRaised?.Invoke(bean);
                                break;
                            case MsgLevel.Warn:
                                basicLog?.Warn(log);
                                ErrorRaised?.Invoke(bean);
                                break;
                            case MsgLevel.Error:
                                errLog?.Error(log);
                                ErrorRaised?.Invoke(bean);
                                break;
                            case MsgLevel.Fatal:
                                errLog?.Fatal(log);
                                ErrorRaised?.Invoke(bean);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        #endregion

        #region 事件
        public event Action<HostarLogBean> InfoRaised;
        public event Action<HostarLogBean> ErrorRaised;
        #endregion
    }

    /// <summary>
    /// 追踪一个声明周期的Log
    /// 适用于：追踪按钮事件等
    /// </summary>
    public class LogTraceLife : IDisposable
    {
        private string controlName = null;
        private string eventName = null;

        public LogTraceLife(string control, string eventName)
        {
            this.controlName = control;
            this.eventName = eventName;
            MsgHelper.Instance.AddMessage(MsgLevel.Debug, $"---Name:[{controlName}], Event:[{eventName}]----Begin----");
        }

        public void WriteLog(string message)
        {
            MsgHelper.Instance.AddMessage(MsgLevel.Debug, $"---Name:[{controlName}], Event:[{eventName}]----[{message}]----");
        }

        ~LogTraceLife()
        {
        }

        public void Dispose()
        {
            MsgHelper.Instance.AddMessage(MsgLevel.Debug, $"---Name:[{controlName}], Event:[{eventName}]----End----");
        }
    }

    public class HostarLogBean
    {
        public HostarLogBean()
        {
        }

        public HostarLogBean(MsgLevel level, string message,int module)
        {
            this.LogLevel = level;
            this.Message = message;
            this.Module = module;
        }

        public int Module { get; set; }

        public DateTime Time = DateTime.Now;

        public MsgLevel LogLevel
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public bool NeedReturn
        {
            get;
            set;
        }
    }
}
