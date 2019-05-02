using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Nodes
{
    public class FlowNode:TreeNode
    {
        public FlowNode(NodeParam param, List<Tuple<string, EventHandler>> menu = null) :base()
        {
            this.Param = param;
            if(menu != null && menu.Count > 0)
            {
                this.ContextMenu = new ContextMenu();
                for(int i =0; i <menu.Count;++i)
                {
                    this.ContextMenu.MenuItems.Add(menu[i].Item1, menu[i].Item2);
                }
            }
        }

        private NodeParam m_Param = null;

        public NodeParam Param
        {
            get
            {
                return this.m_Param;
            }
            set
            {
                this.m_Param = value;
                this.PropertyChange();
            }
        }

        public virtual void PropertyChange()
        {
            this.Text = $"[{this.Param.FlowName}] ID:{this.Param.ID}";
        }

        public virtual void Add(FlowNode node)
        {
            this.Nodes.Add(node);
        }

        public virtual void Delete(FlowNode node)
        {
            this.Nodes.Remove(node);
        }
    }
}
