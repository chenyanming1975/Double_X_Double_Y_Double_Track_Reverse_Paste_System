using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Config;
using GeneralMachine.Flow.Nodes;
using Microsoft.VisualBasic;
using System.IO;
using GeneralMachine.Common;
using System.Diagnostics;
using NationalInstruments.Vision;
using GeneralMachine.Vision;
using System.Threading;

namespace GeneralMachine.Flow.Editer
{
    public partial class ProgramEditCtrl : UserControl
    {
        public ProgramEditCtrl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 所选模组
        /// </summary>
        public Module Module
        {
            get;
            set;
        } = Module.Front;

        public ProgramFlow Flow = new ProgramFlow("主流程");

        private void ProgramEditCtrl_Load(object sender, EventArgs e)
        {
            this.programTree.ExpandAll();
            this.programTree.Nodes.Add(Flow.ProgramName);
            this.programTree.Nodes[0].ContextMenu = Flow.PCB.Menu(this);
            this.programTree.Nodes[0].Tag = Flow.PCB;
        }

        #region 流程树操作
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.programTree.SelectedNode == null)
                return;

            var node = this.programTree.SelectedNode.Tag as NodeParam;
            ProgramFlow.SelectFlow = this.Flow;
            this.proGridStep.SelectedObject = node;
            var arr = node.ID.Split('-');

            if (arr.Length > 0 && !string.IsNullOrEmpty(arr[0]))
            {
                var board = arr[0].Replace("Board","").Replace("Panel","").Replace("PL", "").Replace("BL", "").Replace("CL", "").Replace("P", "").Replace("B", "").Replace("C", "").Replace("M","");
                ProgramFlow.SelectBoard = int.Parse(board);
            }

            Type type = node.GetType();
            if (type == typeof(BadmarkParam)
                || type == typeof(MarkParam)
                || type == typeof(PasteParam)
                || type == typeof(ReadCodeParam)
                || type == typeof(PCBParam))
            {
                this.toolChangeXY.Visible = true;
                this.toolMoveTo.Visible = true;
            }
            else
            {
                this.toolChangeXY.Visible = false;
                this.toolMoveTo.Visible = false;
            }
        }
        public void AddPCS(object sender, EventArgs args)
        {
            var pcs = new PCSParam("小板", (this.programTree.SelectedNode.Tag as NodeParam));
            new PasteListNode(pcs);
            new BadmarkListNode(pcs);
            new ReadCodeListNode(pcs);
            this.RefreshTree();
        }

        public void RecrodROI(object sender, EventArgs args)
        {
            var roi = GetROI?.Invoke();
            if (roi.Count > 0 && roi[0].Shape.GetType() == typeof(RectangleContour))
            {
                var param = (this.programTree.SelectedNode.Tag as NodeParamPt);
                param.ROI = (RectangleContour)roi[0].Shape;
                this.proGridStep.Refresh();
            }
            else
            {
                MessageBox.Show("请正确设置ROI");
            }
        }

        public void AddMark(object sender, EventArgs args)
        {
            if(this.programTree.SelectedNode.Nodes.Count > 4)
            {
                MessageBox.Show("一个小板只允许添加两个Mark点!!!");
                return;
            }
         
            new MarkParam("Mark点", this.programTree.SelectedNode.Tag as NodeParam);
            this.RefreshTree();
        }
        public void AddPaste(object sender, EventArgs args)
        {
            new PasteParam("贴附位",this.programTree.SelectedNode.Tag as NodeParam);
            this.RefreshTree();
        }
        public void AddBadMark(object sender, EventArgs args)
        {
            new BadmarkParam("Badmark", this.programTree.SelectedNode.Tag as NodeParam);
            this.RefreshTree();

            //TreeAdd(new BadmarkParam("Badmark", this.programTree.SelectedNode.Tag as NodeParam));
        }
        public void AddReadCode(object sender, EventArgs args)
        {
            new ReadCodeParam("条码位", this.programTree.SelectedNode.Tag as NodeParam);
            this.RefreshTree();
        }

        public void ClearNode(object sender, EventArgs args)
        {
            var node = this.programTree.SelectedNode.Tag as NodeParam;
            if (MessageBox.Show($"是否清空点选中节点的数据{node.Nodes.Count}个!!!", "警告",MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                node.Nodes.Clear();
                this.RefreshTree();
            }
        }

        public void AdjustPasteList(PasteListNode LIST)
        {
            PasteListSetCtrl frm = new PasteListSetCtrl(LIST);
            frm.ShowDialog();
            this.RefreshTree();
        }


        #endregion

        #region 手动操作
        /// <summary>
        /// 扩展
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Expand(object sender, EventArgs args)
        {
            if(MessageBox.Show("是否对流程线 进行扩展 Y/N", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!Common.Variable.IsExpand)
                {
                    MessageBox.Show("请设置扩展数据!!!");
                    return;
                }

                Type type = this.programTree.SelectedNode.Tag.GetType();
                if (type == typeof(PCSParam))
                {
                    var pcs = this.programTree.SelectedNode.Tag as PCSParam;
                    PointF org = SystemEntiy.Instance[this.Module].MachinePtToActPt(Variable.ExpandOrg);
                    PointF ex = SystemEntiy.Instance[this.Module].MachinePtToActPt(Variable.ExpandX);
                    PointF ey = SystemEntiy.Instance[this.Module].MachinePtToActPt(Variable.ExpandY);
                    var list = MathHelper.Expand2AllPoints(org, org, ex, ey, Variable.Column, Variable.Row);
                    for(int i = 1; i < list.Length;++i)
                    {
                        this.Flow.PCB.Nodes.Add(pcs.Clone() as PCSParam);
                    }
                }
                else if (type == typeof(PasteParam) || type == typeof(ReadCodeParam) || type == typeof(BadmarkParam))
                {
                    var param = this.programTree.SelectedNode.Tag as NodeParamPt;
                    PointF org = SystemEntiy.Instance[this.Module].MachinePtToActPt(Variable.ExpandOrg);
                    PointF ex = SystemEntiy.Instance[this.Module].MachinePtToActPt(Variable.ExpandX);
                    PointF ey = SystemEntiy.Instance[this.Module].MachinePtToActPt(Variable.ExpandY);
                    PointF expend = SystemEntiy.Instance[this.Module].MachinePtToActPt(param.Pos);
                    var list = MathHelper.Expand2AllPoints(expend, org, ex, ey, Variable.Column, Variable.Row);
                    
                    for(int i = 1; i < list.Length; ++i)
                    {
                        var obj = param.Clone() as NodeParamPt;
                        obj.Pos = SystemEntiy.Instance[this.Module].ActPtToMachinePt(list[i]);
                        (this.programTree.SelectedNode.Parent.Tag as NodeParam).Nodes.Add(obj);
                    }
                }

                this.RefreshTree();
                Common.Variable.IsExpand = false;
            }
        }

        public void GoPos(object sender, EventArgs args)
        {
            try
            {
                var pt = (this.programTree.SelectedNode.Tag as NodeParamPt).Pos;
                SystemEntiy.Instance[this.Module].XYGoPos(pt);
            }
            catch (Exception)
            {
            }
        }

        public void ChangeXY(object sender, EventArgs args)
        {
            var node = this.programTree.SelectedNode.Tag as NodeParamPt;
            fmChangeXY fm = new fmChangeXY(this.Module, node.Pos);
            if (fm.ShowDialog() == DialogResult.OK)
            {
                node.Pos = fm.UpdatePt;
                this.proGridStep.Refresh();
            }
        }

        public void FindMark(object sender, EventArgs args)
        {
            var param = (this.programTree.SelectedNode.Tag as NodeParamPt);
            var rtn = DetectUI?.Invoke(param.VisionName, param.ROI, this.Module);
            if(rtn.State == Vision.VisionResultState.OK)
            {
                var cur = SystemEntiy.Instance[this.Module].XYPos;
                var pt = new PointF();
                SystemEntiy.Instance[this.Module].WroldPt(Camera.Top, cur, rtn.Point, out pt);
                SystemEntiy.Instance[this.Module].XYGoPos(pt);
            }
            else
            {
                MessageBox.Show("侦测失败!!");
            }
        }

        public void FindMarkAndChangeXY(object sender, EventArgs args)
        {
            if (MessageBox.Show("侦测Mark点并 修改Mark点坐标 Y/N", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    var param = (this.programTree.SelectedNode.Tag as MarkParam);
                    var rtn = DetectUI?.Invoke(param.VisionName, param.ROI, this.Module);
                    if (rtn.State == Vision.VisionResultState.OK)
                    {
                        var cur = SystemEntiy.Instance[this.Module].XYPos;
                        var pt = new PointF();
                        SystemEntiy.Instance[this.Module].WroldPt(Camera.Top, cur, rtn.Point, out pt);
                        SystemEntiy.Instance[this.Module].XYGoPos(pt);
                        param.Pos = pt;
                        param.IsDetected = true;
                        this.proGridStep.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("侦测失败!!");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void RecordFidMark(object sender, EventArgs args)
        {
            if (MessageBox.Show("是否修改FidMark点 Y/N", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                var param = this.programTree.SelectedNode.Tag as PCBParam;
                param.Pos = SystemEntiy.Instance[this.Module].XYPos;
                this.proGridStep.SelectedObject = param;

                // 遍历树改变所有 点位坐标
            }
        }

        public void AdjustXY(object sender, EventArgs args)
        {
            if (MessageBox.Show("是否进行Mark点 修正 Y/N", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var param = this.programTree.SelectedNode.Tag as PCSParam;

                #region Mark点重新校正
                // 遍历树改变所有 点位坐标
                List<MarkParam> markList = new List<MarkParam>();
                PasteListNode pasteList = null;
                BadmarkListNode badmarkList = null;
                ReadCodeListNode codeList = null;
                param.GetParamList(out markList, out pasteList, out badmarkList, out codeList);

                List<VisionResult> markResults = new List<VisionResult>();

                try
                {
                    for (int i = 0; i < markList.Count; ++i)
                    {
                        SystemEntiy.Instance[this.Module].XYGoPosUI(markList[i].Pos);
                        Thread.Sleep(500);
                        markResults.Add(DetectUI?.Invoke(markList[i].VisionName, markList[i].ROI, this.Module));
                    }

                    PointF[] pL = null;
                    PointF[] bL = null;
                    PointF[] cL = null;

                    if (pasteList != null && pasteList.Nodes.Count > 0)
                    {
                        pL = new PointF[pasteList.Nodes.Count];
                        for (int pi = 0; pi < pasteList.Nodes.Count; ++pi)
                            pL[pi] = (pasteList.Nodes[pi] as NodeParamPt).Pos;
                    }

                    if (badmarkList != null && badmarkList.Nodes.Count > 0)
                    {
                        bL = new PointF[badmarkList.Nodes.Count];
                        for (int pi = 0; pi < badmarkList.Nodes.Count; ++pi)
                            bL[pi] = (badmarkList.Nodes[pi] as NodeParamPt).Pos;
                    }

                    if (codeList != null && codeList.Nodes.Count > 0)
                    {
                        bL = new PointF[codeList.Nodes.Count];
                        for (int pi = 0; pi < codeList.Nodes.Count; ++pi)
                            bL[pi] = (codeList.Nodes[pi] as NodeParamPt).Pos;
                    }

                    if (markList.Count == 1 && markResults[0].State == VisionResultState.OK)
                    {
                        //MathHelper.TransformPointsForm1Mark1Angle()
                    }
                    else if (markList.Count == 2 
                        && markResults[0].State == markResults[1].State
                        && markResults[0].State == VisionResultState.OK)
                    {
                        PointF newMark1 = new PointF();
                        PointF newMark2 = new PointF();

                        SystemEntiy.Instance[this.Module].WroldPt(Camera.Top, markList[0].Pos, markResults[0].Point, out newMark1);
                        SystemEntiy.Instance[this.Module].WroldPt(Camera.Top, markList[1].Pos, markResults[1].Point, out newMark2);
                        double upAngle = 0;

                        if(pL != null)
                        {
                            pL = MathHelper.TransformPointsForm2Mark(pL, markList[0].Pos, markList[1].Pos, newMark1, newMark2, ref upAngle);
                            for (int pi = 0; pi < pL.Length; ++pi)
                            {
                                (pasteList.Nodes[pi] as NodeParamPt).Pos = pL[pi];
                            }
                        }

                        if (bL != null)
                        {
                            bL = MathHelper.TransformPointsForm2Mark(bL, markList[0].Pos, markList[1].Pos, newMark1, newMark2, ref upAngle);
                            for (int bi = 0; bi < bL.Length; ++bi)
                            {
                                (badmarkList.Nodes[bi] as NodeParamPt).Pos = bL[bi];
                            }
                        }


                        if (cL != null)
                        {
                            cL = MathHelper.TransformPointsForm2Mark(cL, markList[0].Pos, markList[1].Pos, newMark1, newMark2, ref upAngle);
                            for (int ci = 0; ci < bL.Length; ++ci)
                            {
                                (codeList.Nodes[ci] as NodeParamPt).Pos = cL[ci];
                            }
                        }

                        param.BaseAngle += upAngle;
                    }
                }
                catch { }
                #endregion
            }
        }
        #endregion

        #region 程式树操作
        private void programTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point click = new Point(e.X, e.Y);
                var node = this.programTree.GetNodeAt(click);
                if (node != null)
                {
                    this.programTree.SelectedNode = node;
                }
            }
        }

        private void bSaveProgram_Click(object sender, EventArgs e)
        {
            this.Flow.NzUsed.Clear();
            this.Flow.NzOffset.Clear();
            this.Flow.NzUOffset.Clear();
            this.Flow.NzUsed.Add(Nozzle.Nz1, this.bUsedNz1.Checked);
            this.Flow.NzUsed.Add(Nozzle.Nz2, this.bUsedNz2.Checked);
            this.Flow.NzUsed.Add(Nozzle.Nz3, this.bUsedNz3.Checked);
            this.Flow.NzUsed.Add(Nozzle.Nz4, this.bUsedNz4.Checked);
            this.Flow.NzOffset.Add(Nozzle.Nz1, new PointF((float)this.nz1OffsetX.Value,
                (float)this.nz1OffsetY.Value));
            this.Flow.NzOffset.Add(Nozzle.Nz2, new PointF((float)this.nz2OffsetX.Value,
                     (float)this.nz2OffsetY.Value));
            this.Flow.NzOffset.Add(Nozzle.Nz3, new PointF((float)this.nz3OffsetX.Value,
             (float)this.nz3OffsetY.Value));
            this.Flow.NzOffset.Add(Nozzle.Nz4, new PointF((float)this.nz4OffsetX.Value,
             (float)this.nz4OffsetY.Value));

            this.Flow.NzUOffset.Add(Nozzle.Nz1, (double)this.nz1ROffset.Value);
            this.Flow.NzUOffset.Add(Nozzle.Nz2, (double)this.nz2ROffset.Value);
            this.Flow.NzUOffset.Add(Nozzle.Nz3, (double)this.nz3ROffset.Value);
            this.Flow.NzUOffset.Add(Nozzle.Nz4, (double)this.nz4ROffset.Value);

            if (ProgramFlow.Save(this.Flow))
                MessageBox.Show("保存成功!!");
            else
                MessageBox.Show("保存失敗!!");
        }

        private void bLoadProgram_Click(object sender, EventArgs e)
        {
            CreateProgramCtrl fm = new CreateProgramCtrl(false);

            if (fm.ShowDialog() == DialogResult.OK)
            {
                string module = CommonHelper.GetEnumDescription(typeof(Module), fm.Module);

                this.Flow = ProgramFlow.Load($"{PathDefine.sPathProgram}{module}//{fm.ProgramName}.json");

                this.lModule.Text = module;
                this.lProgram.Text = fm.ProgramName;
                if (this.Flow != null)
                {
                    if(this.Flow.NzUsed.Count > 0 && this.Flow.NzOffset.Count > 0)
                    {
                        this.bUsedNz1.Checked = this.Flow.NzUsed[Nozzle.Nz1];
                        this.bUsedNz2.Checked = this.Flow.NzUsed[Nozzle.Nz2];
                        this.bUsedNz3.Checked = this.Flow.NzUsed[Nozzle.Nz3];
                        this.bUsedNz4.Checked = this.Flow.NzUsed[Nozzle.Nz4];
                        this.nz1OffsetX.Value = (decimal)this.Flow.NzOffset[Nozzle.Nz1].X;
                        this.nz1OffsetY.Value = (decimal)this.Flow.NzOffset[Nozzle.Nz1].Y;
                        this.nz2OffsetX.Value = (decimal)this.Flow.NzOffset[Nozzle.Nz2].X;
                        this.nz2OffsetY.Value = (decimal)this.Flow.NzOffset[Nozzle.Nz2].Y;
                        this.nz3OffsetX.Value = (decimal)this.Flow.NzOffset[Nozzle.Nz3].X;
                        this.nz3OffsetY.Value = (decimal)this.Flow.NzOffset[Nozzle.Nz3].Y;
                        this.nz4OffsetX.Value = (decimal)this.Flow.NzOffset[Nozzle.Nz4].X;
                        this.nz4OffsetY.Value = (decimal)this.Flow.NzOffset[Nozzle.Nz4].Y;
                    }

                    this.SetTree(null, this.Flow.PCB);
                    this.Module = this.Flow.Module;
                    this.programTree.ExpandAll();
                    this.programTree.Enabled = true;
                }
                else
                    this.programTree.Enabled = false;
            }
        }

        /// <summary>
        /// 刷新 树形控件
        /// </summary>
        /// <param name="tree">null:全部刷新,node 刷新改节点下的控件</param>
        /// <param name="node">父节点</param>
        private void SetTree(TreeNode tree, NodeParam node)
        {
            if (tree == null)
            {
                this.programTree.Nodes.Clear();
                this.programTree.Nodes.Add(new TreeNode());
                this.programTree.Nodes[0].ContextMenu = node.Menu(this);
                this.programTree.Nodes[0].Tag = node;
                this.programTree.Nodes[0].Text = node.Text();

                foreach (NodeParam param in node.Nodes)
                {
                    SetTree(this.programTree.Nodes[0], param);
                }
            }
            else
            {
                TreeNode tNode = new TreeNode(node.Text());
                tNode.Tag = node;
                tNode.ContextMenu = node.Menu(this);
                tNode.Text = node.Text();
                tree.Nodes.Add(tNode);
                foreach (NodeParam param in node.Nodes)
                {
                    SetTree(tNode, param);
                }
            }
        }
        private void RefreshTree()
        {
            this.Flow.UpdateFlowID();
            this.SetTree(null, this.Flow.PCB);
            this.programTree.ExpandAll();
        }

        private void bNewProgram_Click(object sender, EventArgs e)
        {
            CreateProgramCtrl ctrl = new CreateProgramCtrl();
            ctrl.ShowDialog();
            if(ctrl.DialogResult == DialogResult.OK)
            {
                string module = CommonHelper.GetEnumDescription(typeof(Module), ctrl.Module);
                if (ProgramFlow.Exist(ctrl.Module, ctrl.ProductName))
                {
                    MessageBox.Show($"{module}:贴附信息[{ctrl.ProgramName}]已存在!!");
                    return;
                }
                else if(ctrl.ProgramName == string.Empty)
                {
                    MessageBox.Show("请正确输入程序名!!!");
                    return;
                }

                this.lModule.Text = module;
                this.lProgram.Text = ctrl.ProgramName;
                this.Flow = new ProgramFlow(ctrl.ProgramName);
                this.Flow.Module = ctrl.Module;
                this.Module = ctrl.Module;
                this.programTree.Enabled = true;
                this.RefreshTree();
            }
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定删除改节点?", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Type type = this.programTree.SelectedNode.Tag.GetType();
                
                if (type  == typeof(PCBParam))
                {
                    MessageBox.Show("主程式禁止删除!!!");
                    return;
                }
                else if(type == typeof(BadmarkListNode))
                {
                    MessageBox.Show("Badmark列表禁止删除!!!");
                    return;
                }
                else if (type == typeof(PasteListNode))
                {
                    MessageBox.Show("贴附列表禁止删除!!!");
                    return;
                }
                else if (type == typeof(ReadCodeListNode))
                {
                    MessageBox.Show("读条码列表禁止删除!!!");
                    return;
                }
                var parent = this.programTree.SelectedNode.Parent.Tag as NodeParam;
                var node = this.programTree.SelectedNode.Tag as NodeParam;
                parent.Nodes.Remove(node);
                this.programTree.SelectedNode.Remove();
            }
        }
        #endregion

        private void toolMoveTo_Click(object sender, EventArgs e)
        {
            try
            {
                var node = this.programTree.SelectedNode.Tag as NodeParamPt;
                SystemEntiy.Instance[this.Module].XYGoPos(node.Pos);
            }
            catch { }
        }

        private NodeParam nodeParamCopy = null;
        private void programTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.programTree.SelectedNode == null)
                return;
            if (e.Control && e.KeyCode == Keys.C)
            {
                var node = this.programTree.SelectedNode.Tag as NodeParam;
                Type type = node.GetType();
                if (type == typeof(PCSParam)
                    || type == typeof(ReadCodeParam)
                    || type == typeof(BadmarkParam)
                    || type == typeof(PasteParam))
                {
                    nodeParamCopy = (this.programTree.SelectedNode.Tag as NodeParam).Clone() as NodeParam;
                    Debug.WriteLine("复制");
                }
                else
                    nodeParamCopy = null;
            }
            else if(e.Control && e.KeyCode  == Keys.V) // 拷贝
            {
                var parent = this.programTree.SelectedNode.Tag as NodeParam;
                if (parent == null || nodeParamCopy == null) return;
                Type type = parent.GetType();
                Type type2 = nodeParamCopy.GetType();
                if (type2 == typeof(PCSParam) && type == typeof(PCBParam))
                {
                    this.Flow.PCB.Nodes.Add(nodeParamCopy.Clone() as PCSParam);
                    this.RefreshTree();
                }
                else if (type2 == typeof(PasteParam) && type == typeof(PasteListNode))
                {
                    parent.Nodes.Add(nodeParamCopy.Clone() as PasteParam);
                    this.RefreshTree();
                }
                else if (type2 == typeof(BadmarkParam) && type == typeof(BadmarkListNode))
                {
                    parent.Nodes.Add(nodeParamCopy.Clone() as BadmarkParam);
                    this.RefreshTree();
                }
                else if (type2 == typeof(MarkParam) && type == typeof(ReadCodeListNode))
                {
                    parent.Nodes.Add(nodeParamCopy.Clone() as MarkParam);
                    this.RefreshTree();
                }
            }
        }

        private void toolDetect_Click(object sender, EventArgs e)
        {
            if (this.programTree.SelectedNode == null) return;
            var node = this.programTree.SelectedNode.Tag as NodeParamPt;

            DetectUI?.Invoke(node.VisionName, node.ROI, this.Module);
        }

        public static event Func<string, RectangleContour,Module, Vision.VisionResult> DetectUI;

        public static event Func<Roi> GetROI;
    }
}
