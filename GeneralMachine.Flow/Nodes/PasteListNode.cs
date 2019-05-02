using GeneralMachine.Flow.Editer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Nodes
{

    [Serializable]
    public class PasteListNode : NodeParam
    {
        public PasteListNode() : base() { }

        public PasteListNode(NodeParam parent):base("贴附列表", parent)
        {
        }

        public override ContextMenu Menu(ProgramEditCtrl ctrl)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("添加贴附位", ctrl.AddPaste));
            menu.MenuItems.Add(new MenuItem("清空",ctrl.ClearNode));
            menu.MenuItems.Add(new MenuItem("整体调整", (sender, e)=> { ctrl.AdjustPasteList(this); }));
            return menu;
        }
    }
}
