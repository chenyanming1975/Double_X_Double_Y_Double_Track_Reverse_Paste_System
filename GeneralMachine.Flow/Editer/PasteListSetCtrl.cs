using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Flow.Nodes;
using GeneralMachine.Config;

namespace GeneralMachine.Flow.Editer
{
    public partial class PasteListSetCtrl : Form
    {
        public PasteListSetCtrl()
        {
            InitializeComponent();
        }

        public PasteListSetCtrl(PasteListNode nodeList)
        {
            InitializeComponent();
            this.listNode = nodeList;
        }

        private PasteListNode listNode = null;

        private void pasteList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (listNode != null)
            {
                (listNode.Nodes[e.Index] as PasteParam).CanPaste = e.NewValue == CheckState.Checked;
            }
        }

        private void bAutoSetByX_Click(object sender, EventArgs e)
        {

        }

        private void bAutoSetByY_Click(object sender, EventArgs e)
        {

        }

        private void bAutoSetByBadmark_Click(object sender, EventArgs e)
        {

        }

        private void rbPasteAll_CheckedChanged(object sender, EventArgs e)
        {
            if(this.rbPasteAll.Checked)
            {
                for (int i = 0; i < this.pasteParamBindingSource.Count; ++i)
                {
                    (this.pasteParamBindingSource[i] as PasteParam).CanPaste = true;
                }
                this.dGVPaste.Refresh();
            }
        }

        private void rbPasteNULL_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbPasteNULL.Checked)
            {
                for (int i = 0; i < this.pasteParamBindingSource.Count; ++i)
                {
                    (this.pasteParamBindingSource[i] as PasteParam).CanPaste = false;
                }

                this.dGVPaste.Refresh();
            }
        }

        private void rbPasteLeft_CheckedChanged(object sender, EventArgs e)
        {
            if(rbPasteLeft.Checked)
            {
                for (int i = 0; i < this.pasteParamBindingSource.Count; ++i)
                {
                    var node = this.pasteParamBindingSource[i] as PasteParam;
                    if(node.Feeder == Config.Feeder.Left)
                    {
                        node.CanPaste = true;
                    }
                    else
                    {
                        node.CanPaste = false;
                    }
                }
                this.dGVPaste.Refresh();
            }
        }

        private void rbPasteRight_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPasteRight.Checked)
            {
                for (int i = 0; i < this.dGVPaste.Rows.Count; ++i)
                {
                    var node = this.pasteParamBindingSource[i] as PasteParam;
                    if (node.Feeder == Config.Feeder.Right)
                    {
                        node.CanPaste = true;
                    }
                    else
                    {
                        node.CanPaste = false;
                    }
                }
                this.dGVPaste.Refresh();
            }
        }

        private void PasteListSetCtrl_Load(object sender, EventArgs e)
        {
            try
            {
                List<PasteParam> list = new List<PasteParam>();
                if(listNode != null)
                {
                    for (int i = 0; i < listNode.Nodes.Count; ++i)
                    {
                        list.Add(listNode.Nodes[i] as PasteParam);
                    }
                    this.pasteParamBindingSource.DataSource = list;
                }
            }
            catch { }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bDeleteSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dGVPaste.SelectedRows.Count; ++i)
            {
                this.pasteParamBindingSource.RemoveAt(this.dGVPaste.SelectedRows[i].Index);
            }
            this.dGVPaste.Refresh();
        }

        private void bInOffset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dGVPaste.SelectedRows.Count; ++i)
            {
                var node = this.pasteParamBindingSource[this.dGVPaste.SelectedRows[i].Index] as PasteParam;
                PointF pt = node.Pos;
                pt.X += (float)this.numOffsetX.Value;
                pt.Y += (float)this.numOffsetY.Value;
                node.Pos = pt;
            }
            this.dGVPaste.Refresh();
        }

        private void bSetSelect_Click(object sender, EventArgs e)
        {
            for(int i =0; i < this.dGVPaste.SelectedRows.Count;++i) 
            {
                var node = this.pasteParamBindingSource[this.dGVPaste.SelectedRows[i].Index] as PasteParam;
                node.PasteAngle = (double)numAngle.Value;
                if(this.cbFeeder.SelectedIndex >= 0)
                    node.Feeder = (Feeder)this.cbFeeder.SelectedIndex;
            }

            this.dGVPaste.Refresh();
        }

        private void bAutoByX_Click(object sender, EventArgs e)
        {
            var list = this.pasteParamBindingSource.DataSource as List<PasteParam>;
            list.Sort((a, b) => {
                if (a.Pos.X >= b.Pos.X)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            });

            this.pasteParamBindingSource.DataSource = list;
            this.dGVPaste.Refresh();

            if (this.listNode != null)
            {
                this.listNode.Nodes.Clear();
                for (int i = 0; i < list.Count; ++i)
                {
                    listNode.Nodes.Add(list[i]);
                }
            }

            this.dGVPaste.Refresh();
        }

        private void bAutoByY_Click(object sender, EventArgs e)
        {
            var list = this.pasteParamBindingSource.DataSource as List<PasteParam>;
            list.Sort((a, b) => {
                if(a.Pos.Y <= b.Pos.Y)
                {
                    return 1;
                }else
                {
                    return -1;
                }
            });

            this.pasteParamBindingSource.DataSource = list;
            this.dGVPaste.Refresh();

            if(this.listNode != null)
            {
                this.listNode.Nodes.Clear();
                for(int i = 0; i < list.Count;++i)
                {
                    listNode.Nodes.Add(list[i]);
                }
            }
        }
    }
}
