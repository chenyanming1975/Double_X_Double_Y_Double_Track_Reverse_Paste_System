using GeneralMachine.Flow.Editer;
using NationalInstruments.Vision;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Nodes
{
    /// <summary>
    /// 节点参数
    /// BadMark-1-2
    /// Mark-1-1
    /// PanelCode-1
    /// Paste-1-1
    /// </summary>
    [Serializable]
    public class NodeParam : ICloneable
    {
        public NodeParam()
        {
        }

        public NodeParam(string name, NodeParam parent = null)
        {
            this.FlowName = name;

            if (parent != null)
            {
                parent.Nodes.Add(this);
            }
        }

        #region 属性
        /// <summary>
        /// 程式节点名称
        /// </summary>
        [ReadOnly(true)]
        [Category("流程")]
        [DisplayName("流程名称")]
        public string FlowName
        {
            get;
            set;
        } = string.Empty;

        [ReadOnly(true)]
        [Category("流程")]
        [DisplayName("流程ID")]
        public string ID
        {
            get;
            set;
        } = string.Empty;

        //[JsonIgnore]
        //[Browsable(false)]
        //[NonSerialized]
        //public NodeParam Parent
        //{
        //    get;
        //    set;
        //} = null;

        [Browsable(false)]
        public List<NodeParam> Nodes = new List<NodeParam>();
        #endregion

        public virtual ContextMenu Menu(ProgramEditCtrl ctrl)
        {
            return null;
        }

        public virtual string Text()
        {
            return $"[{this.FlowName}] ID:{this.ID}";
        }

        public object Clone()
        {
            return Common.CommonHelper.Copy(this);
        }
    }

    [Serializable]
    public class NodeParamPt : NodeParam
    {
        public NodeParamPt():base() { }

        public NodeParamPt(string name, NodeParam parent) : base(name, parent)
        {
        }

        /// <summary>
        /// 拍照位
        /// </summary>
        public virtual PointF Pos
        {
            get;
            set;
        } = new PointF();

        /// <summary>
        /// 侦测区域
        /// </summary>
        [Browsable(false)]
        public virtual RectangleContour ROI
        {
            get;
            set;
        } = null;

        /// <summary>
        /// 侦测区域
        /// </summary>
        [Browsable(false)]
        public virtual string VisionName
        {
            get;
            set;
        } = string.Empty;

        [JsonIgnore]
        [Browsable(false)]
        public bool CanExpand = true;

        [JsonIgnore]
        [Browsable(false)]
        public bool CanRecordXY = true;

        [JsonIgnore]
        [Browsable(false)]
        public bool CanRecordROI = true;

   
        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public override ContextMenu Menu(ProgramEditCtrl ctrl)
        {
            ContextMenu menu = new ContextMenu();
            if (CanExpand)
                menu.MenuItems.Add(new MenuItem("扩展", ctrl.Expand));

            menu.MenuItems.Add(new MenuItem("到点", ctrl.GoPos));
            if (CanRecordXY)
                menu.MenuItems.Add(new MenuItem("修改XY", ctrl.ChangeXY));

            if(CanRecordROI)
                menu.MenuItems.Add(new MenuItem("记录ROI", ctrl.RecrodROI));

            return menu;
        }
    }
}
