using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Flow.Nodes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneralMachine.Common.CommonHelper;

namespace GeneralMachine.Flow
{
    public class ProgramFlow:ICloneable
    {
        #region 公共方法
        public static string[] GetProgramList(Module module)
        {
            if (module == Module.Front)
                return Common.CommonHelper.GetFileName(PathDefine.sPathProgram + "前模组\\", "*.json").ToArray();
            else
                return Common.CommonHelper.GetFileName(PathDefine.sPathProgram + "后模组\\", "*.json").ToArray();
        }
        public static int SelectBoard = 0;
        public static ProgramFlow SelectFlow = null;

        /// <summary>
        /// 获得指定类型的节点
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetNodeList(Type type)
        {
            List<string> list = new List<string>();

            if(SelectFlow != null)
            {
                string ID = $"Board{SelectBoard}";
                foreach (NodeParam node in SelectFlow.PCB.Nodes)
                {
                    if (node.ID == ID)
                    {
                        foreach (NodeParam child in node.Nodes)
                        {
                            if (child.GetType() == type)
                            {
                                foreach (NodeParam re in child.Nodes)
                                {
                                    list.Add(re.ID);
                                }
                            }
                        }
                    }
                }
            }

            return list;
        }
        #endregion

        public ProgramFlow(string ProgramName)
        {
            this.ProgramName = ProgramName;
            this.PCB = new PCBParam(this.ProgramName);
        }

        #region 基本属性
        /// <summary>
        /// 主程式名称
        /// </summary>
        public string ProgramName = string.Empty;

        /// <summary>
        /// 属于模组
        /// </summary>
        public Module Module = Module.Front;

        /// <summary>
        /// 头结点
        /// </summary>
        public PCBParam PCB = null;
        #endregion

        #region 程式参数
        /// <summary>
        /// 吸嘴是否可用
        /// </summary>
        public Dictionary<Nozzle, bool> NzUsed = new Dictionary<Nozzle, bool>();

        /// <summary>
        /// 吸嘴整体偏移
        /// </summary>
        public Dictionary<Nozzle, PointF> NzOffset = new Dictionary<Nozzle, PointF>();

        /// <summary>
        /// 吸嘴角度补正
        /// </summary>
        public Dictionary<Nozzle, double> NzUOffset = new Dictionary<Nozzle, double>();
        #endregion

        #region 整体调整
        /// <summary>
        /// 更新FlowID
        /// </summary>
        public void UpdateFlowID()
        {
            int panelIndex = 0;
            int pcsIndex = 0;
            int badmarkIndex = 0;
            int markIndex = 0;
            int pasteIndex = 0;
            int codeIndex = 0;

            foreach (NodeParam node in this.PCB.Nodes)
            {
                if (node.GetType() == typeof(ReadCodeParam))
                {
                    panelIndex++;
                    node.ID = $"Panel{panelIndex}";
                }
                else if (node.GetType() == typeof(PCSParam))
                {
                    pcsIndex++;
                    node.ID = $"Board{pcsIndex}";
                    badmarkIndex = 0;
                    markIndex = 0;
                    pasteIndex = 0;
                    codeIndex = 0;

                    #region 读取小板信息
                    foreach (NodeParam childNode in node.Nodes)
                    {
                        if (childNode.GetType() == typeof(MarkParam))
                        {
                            markIndex++;
                            childNode.ID = $"M{pcsIndex}-{markIndex}";
                        }
                        else if (childNode.GetType() == typeof(BadmarkListNode))
                        {
                            childNode.ID = $"BL{pcsIndex}";
                            foreach (NodeParam badmark in childNode.Nodes)
                            {
                                badmarkIndex++;
                                badmark.ID = $"B{pcsIndex}-{badmarkIndex}";
                            }
                        }
                        else if (childNode.GetType() == typeof(PasteListNode))
                        {
                            childNode.ID = $"PL{pcsIndex}";
                            foreach (NodeParam paste in childNode.Nodes)
                            {
                                pasteIndex++;
                                paste.ID = $"P{pcsIndex}-{pasteIndex}";
                            }
                        }
                        else if (childNode.GetType() == typeof(ReadCodeListNode))
                        {
                            childNode.ID = $"CL{pcsIndex}";
                            foreach (NodeParam readcode in childNode.Nodes)
                            {
                                codeIndex++;
                                readcode.ID = $"C{pcsIndex}-{codeIndex}";
                            }
                        }
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 更新Mark点,修正PCS内的相对坐标
        /// </summary>
        /// <param name="mark1"></param>
        /// <param name="newMark1"></param>
        /// <param name="board"></param>
        public void UpdateMark(MarkParam mark1, PointF newMark1, PCSParam board)
        {

        }
        #endregion

        /// <summary>
        /// 转化为程式参数
        /// </summary>
        public MultiPasteInfo ConvertToInfo()
        {
            MultiPasteInfo info = new MultiPasteInfo();
            info.PasteName = this.PCB.FlowName;
            info.LocFidMark = this.PCB.Pos;
            info.EnableBadmark = this.PCB.EnableBadmark;
            info.EnableReadPanel = this.PCB.EnableReadPanelCode;
            info.EnableReadPCS = this.PCB.EnableReadPcsCode;
            info.NzUsed = this.NzUsed;
            info.NzOffset = this.NzOffset;
            info.NzUOffset = this.NzUOffset;

            foreach (NodeParam pcsNode in this.PCB.Nodes)
            {
                if (pcsNode.GetType() == typeof(ReadCodeParam))
                {
                    info.ReadPanel = pcsNode as ReadCodeParam;
                    if (!info.VisionList.Contains(info.ReadPanel.VisionName))
                        info.VisionList.Add(info.ReadPanel.VisionName);
                }
                else if (pcsNode.GetType() == typeof(PCSParam))
                {
                    var pasteInfo = new PasteInfo();
                    pasteInfo.BaseAngle = (pcsNode as PCSParam).BaseAngle;

                    #region 读取小板信息
                    foreach (NodeParam childNode in pcsNode.Nodes)
                    {
                        if (childNode.GetType() == typeof(MarkParam))
                        {
                            var mark = childNode as MarkParam;
                            pasteInfo.MarkPtList.Add(mark);
                            if (!info.VisionList.Contains(mark.VisionName))
                                info.VisionList.Add(mark.VisionName);
                        }
                        else if (childNode.GetType() == typeof(BadmarkListNode))
                        {
                            foreach (NodeParam badmark in childNode.Nodes)
                            {
                                var temp = badmark as BadmarkParam;
                                pasteInfo.BadmarkList.Add(temp);
                                if (!info.VisionList.Contains(temp.VisionName))
                                    info.VisionList.Add(temp.VisionName);
                            }
                        }
                        else if (childNode.GetType() == typeof(PasteListNode))
                        {
                            for (int pasteIndex = 0; pasteIndex < childNode.Nodes.Count; ++pasteIndex)
                            {
                                var temp = childNode.Nodes[pasteIndex] as PasteParam;
                                pasteInfo.PasteList.Add(temp);
                            }
                        }
                        else if (childNode.GetType() == typeof(ReadCodeListNode))
                        {
                            foreach (NodeParam readcode in childNode.Nodes)
                            {
                                var temp = readcode as ReadCodeParam;
                                pasteInfo.ReadPcsList.Add(temp);
                                if (!info.VisionList.Contains(temp.VisionName))
                                    info.VisionList.Add(temp.VisionName);
                            }
                        }
                    }
                    #endregion

                    info.PasteInfos.Add(pasteInfo);
                }
            }

            return info;
        }

        #region 保存加载 文件操作
        /// <summary>
        /// 是否已经存在
        /// </summary>
        /// <param name="module"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Exist(Module module, string name)
        {
            return File.Exists(PathDefine.sPathProgram + $"{CommonHelper.GetEnumDescription(module.GetType(), module)}\\" + name + ".json");
        }

        /// <summary>
        /// 加载程式
        /// </summary>
        /// <param name="name">程式名称</param>
        /// <returns></returns>
        public static ProgramFlow Load(string path)
        {
            Common.SerializableHelper<ProgramFlow> helper = new Common.SerializableHelper<ProgramFlow>();
            ProgramFlow flow = helper.DeJsonSerialize(path);
            return flow;
        }

        /// <summary>
        /// 保存程式
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        public static bool Save(ProgramFlow program)
        {
            Common.SerializableHelper<ProgramFlow> helper = new Common.SerializableHelper<ProgramFlow>(program);
            return helper.JsonSerialize(PathDefine.sPathProgram + $"{CommonHelper.GetEnumDescription(typeof(Module), program.Module)}\\{program.ProgramName}.json");
        }

        public object Clone()
        {
            return CommonHelper.Copy(this);
        }
        #endregion
    }

    //public class FeederData
    //{
    //    public string LabelName = string.Empty;

    //    public int SuckCount = 0;

    //    public string FeederName = string.Empty;
    //}

    /// <summary>
    /// 可保存到中转目录,当机器异常停止时，可启用重贴功能
    /// </summary>
    public class MultiPasteInfo
    {
        public string PasteName = string.Empty;

        public PointF LocFidMark = new PointF();

        /// <summary>
        /// 基板信息
        /// </summary>
        public List<PasteInfo> PasteInfos = new List<PasteInfo>();

        /// <summary>
        /// Panel码信息
        /// </summary>
        public ReadCodeParam ReadPanel = null;

        #region 基本属性
    
        /// <summary>
        /// 是否启用读Badmark
        /// </summary>
        public bool EnableBadmark = false;

        /// <summary>
        /// 是否启用读条码
        /// </summary>
        public bool EnableReadPCS = false;

        /// <summary>
        /// 是否启用读Panel
        /// </summary>
        public bool EnableReadPanel = false;

        /// <summary>
        /// 吸嘴是否启用
        /// </summary>
        public Dictionary<Nozzle, bool> NzUsed = new Dictionary<Nozzle, bool>();

        /// <summary>
        /// 吸嘴偏移
        /// </summary>
        public Dictionary<Nozzle, PointF> NzOffset = new Dictionary<Nozzle, PointF>();

        public Dictionary<Nozzle, double> NzUOffset = new Dictionary<Nozzle, double>();


        /// <summary>
        /// 补附策略
        /// </summary>
        public RepasteShceme RepasteShceme = RepasteShceme.优先贴;

        /// <summary>
        /// 贴附策略
        /// </summary>
        public SuckSheme PasteShceme = SuckSheme.左右Feeder均衡;

        /// <summary>
        /// 程序中包含的视觉库
        /// </summary>
        public List<string> VisionList = new List<string>();
        #endregion
    }

    public class PasteInfo
    {
        /// <summary>
        /// mark点信息
        /// </summary>
        public List<MarkParam> MarkPtList = new List<MarkParam>();

        /// <summary>
        /// badmark点信息
        /// </summary>
        public List<BadmarkParam> BadmarkList = new List<BadmarkParam>();

        /// <summary>
        /// 读码点信息
        /// </summary>
        public List<ReadCodeParam> ReadPcsList = new List<ReadCodeParam>();

        /// <summary>
        /// 贴附点信息
        /// </summary>
        public List<PasteParam> PasteList = new List<PasteParam>();

        /// <summary>
        /// 基准角度
        /// </summary>
        public double BaseAngle = 0;
    }
}
