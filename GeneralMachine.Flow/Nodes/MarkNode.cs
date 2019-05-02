using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Nodes
{
    public class MarkNode:FlowNodePt
    {
        public MarkNode(string name):base()
        {
            this.FlowName = name;
        }

        /// <summary>
        /// Mark 点拍照位
        /// </summary>
        public PointF CamMark = new PointF();


        /// <summary>
        /// 照Mark点视觉名称
        /// </summary>
        public string MarkVisionName = string.Empty;
    }
}
