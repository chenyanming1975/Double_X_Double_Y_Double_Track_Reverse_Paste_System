using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Nodes
{
    public class PCSNode:FlowNode
    {
        public PCSNode()
        {
            this.FlowName = "小板";
            this.Nodes.Add(new BadMarkListNode());
            this.Nodes.Add(new PasteListNode());
            this.Nodes.Add(new ReadCodeListNode());

            this.ContextMenu = new ContextMenu();
            this.ContextMenu.MenuItems.Add(new MenuItem("添加Mark-1点", OnAddMark1));
            this.ContextMenu.MenuItems.Add(new MenuItem("添加Mark-2点", OnAddMark2));
            this.ContextMenu.MenuItems.Add(new MenuItem("扩展", OnExpand));
        }

        public void OnAddMark1(object sender, EventArgs args)
        {
            this.Add(new MarkNode("Mark-1点"));
        }

        public void OnAddMark2(object sender, EventArgs args)
        {
            this.Add(new MarkNode("Mark-2点"));
        }

        public void OnExpand(object sender, EventArgs args)
        {
        }
    }
}
