using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Nodes
{
    public class ReadCodeNode:FlowNodePt
    {
        public ReadCodeNode():base()
        {
            this.FlowName = "读码位";
            this.ContextMenu = new ContextMenu();
            this.ContextMenu.MenuItems.Add(new MenuItem("扩展", OnExpand));
        }

        private void OnExpand(object sender, EventArgs e)
        {
        }
    }
}
