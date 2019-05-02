using GeneralMachine.Flow.Editer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Nodes
{
    [Serializable]
    public class PCSParam:NodeParam
    {
        public PCSParam() : base() { }
        public PCSParam(string name, NodeParam parent) : base(name, parent)
        { }

        /// <summary>
        /// 基板角度
        /// </summary>
        [Category("参数")]
        [DisplayName("基准角度")]
        public double BaseAngle
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 获得PCS里的参数
        /// </summary>
        /// <param name="markList"></param>
        /// <param name="pasteList"></param>
        /// <param name="badmarkList"></param>
        /// <param name="codeList"></param>
        public void GetParamList(out List<MarkParam> markList, out PasteListNode pasteList, out BadmarkListNode badmarkList, out ReadCodeListNode codeList)
        {
            // 遍历树改变所有 点位坐标
            markList = new List<MarkParam>();
            pasteList = null;
            badmarkList = null;
            codeList = null;
            foreach (NodeParam node in this.Nodes)
            {
                if (node.GetType() == typeof(PasteListNode))
                {
                    pasteList = node as PasteListNode;
                }
                else if (node.GetType() == typeof(BadmarkListNode))
                {
                    badmarkList = node as BadmarkListNode;
                }
                else if (node.GetType() == typeof(ReadCodeListNode))
                {
                    codeList = node as ReadCodeListNode;
                }
                else if (node.GetType() == typeof(MarkParam))
                {
                    markList.Add(node as MarkParam);
                }
            }
        }

        public override ContextMenu Menu(ProgramEditCtrl ctrl)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("添加Mark点", ctrl.AddMark));
            menu.MenuItems.Add(new MenuItem("扩展", ctrl.Expand));
            menu.MenuItems.Add(new MenuItem("Mark点修正", ctrl.AdjustXY));
           
            return menu;
        }
    }
}
