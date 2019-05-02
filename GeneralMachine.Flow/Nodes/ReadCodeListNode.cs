using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Flow.Editer;
using System.ComponentModel;

namespace GeneralMachine.Flow.Nodes
{
    [Serializable]
    public class ReadCodeListNode:NodeParam
    {
        public ReadCodeListNode() : base() { }
        public ReadCodeListNode(NodeParam parent):base("读码列表", parent)
        {
        }

        public override ContextMenu Menu(ProgramEditCtrl ctrl)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("添加条码位", ctrl.AddReadCode));
            menu.MenuItems.Add(new MenuItem("清空",ctrl.ClearNode));
            return menu;
        }
    }
}
