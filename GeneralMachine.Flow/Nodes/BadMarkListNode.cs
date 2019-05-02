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
    public class BadmarkListNode:NodeParam
    {
        public BadmarkListNode() : base() { }
        public BadmarkListNode(NodeParam parent):base("Badmark列表", parent)
        {
        }

        public override ContextMenu Menu(ProgramEditCtrl ctrl)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("添加Badmark", ctrl.AddBadMark));
            menu.MenuItems.Add(new MenuItem("清空",ctrl.ClearNode));
            return menu;
        }
    }
}
