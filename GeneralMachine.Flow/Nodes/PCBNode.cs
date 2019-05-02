using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Nodes
{
    public class PCBNode:FlowNodePt
    {
        public PCBNode():base()
        {
            this.FlowName = "主程式";
            this.ContextMenu = new ContextMenu();
            this.ContextMenu.MenuItems.Add(new MenuItem("添加小板", OnAddPCS));
            this.ContextMenu.MenuItems.Add(new MenuItem("添加大Panel码", OnAddReadCode));
        }

        public string PCBName
        {
            get;
            set;
        } = "A11";

        public void OnAddPCS(object sender, EventArgs args)
        {
            this.Nodes.Add(new PCSNode());
        }

        public void OnAddReadCode(object sender, EventArgs args)
        {
            this.Nodes.Add(new ReadCodeNode());
        }
    }
}
