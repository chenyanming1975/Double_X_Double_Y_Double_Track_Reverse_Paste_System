using GeneralMachine.Config;
using NationalInstruments.Vision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.Flow.Editer
{
    public partial class frmPasteReCheck : Form
    {
        public frmPasteReCheck(Module module)
        {
            InitializeComponent();

            if(SystemEntiy.Instance.FlowMachine.ContainsKey(module)
                && SystemEntiy.Instance.FlowMachine[module].Program != null)
            {
                this.Module = module;
                this.RunData = SystemEntiy.Instance.FlowMachine[module].RunData;
                string desr = Common.CommonHelper.GetEnumDescription(module.GetType(), module);
                this.Flow = ProgramFlow.Load($"{PathDefine.sPathProgram}{desr}//{SystemEntiy.Instance.FlowMachine[module].Program.PasteName}.json");
                CameraDefine.Instance.Light(this.Module, Camera.Top, new LightParam() { bRed = true, R_Value = 80 });
            }
        }

        private Module Module = Module.Front;
        private MachineRunDataHelper RunData = null;
        private ProgramFlow Flow = null;
        public bool CanShow
        {
            get
            {
                return this.Flow != null && this.RunData != null;
            }
        }
        private void frmPasteReCheck_Load(object sender, EventArgs e)
        {
            if(this.RunData == null || this.Flow == null)
                return;

            this.dGVMark.Rows.Clear();
            this.dGVMark.Rows.Add(this.RunData.BoardCount);
            for(int pcbIndex = 0; pcbIndex < this.RunData.BoardCount; ++pcbIndex)
            {
                this.dGVMark.Rows[pcbIndex].Cells[0].Value = pcbIndex+1;
                this.dGVMark.Rows[pcbIndex].Cells[1].Value = $"{this.RunData[pcbIndex].MarkData.Mark1.X:N2},{this.RunData[pcbIndex].MarkData.Mark1.Y:N2}";
                this.dGVMark.Rows[pcbIndex].Cells[2].Value = $"{this.RunData[pcbIndex].MarkData.Mark2.X:N2},{this.RunData[pcbIndex].MarkData.Mark2.Y:N2}";

                for (int pcsIndex = 0; pcsIndex < this.RunData[pcbIndex].PCSCount; ++pcsIndex)
                {
                    int rowIndex = this.dGVPaste.Rows.Add();
                    this.dGVPaste.Rows[rowIndex].Cells[0].Value = pcbIndex + 1;
                    this.dGVPaste.Rows[rowIndex].Cells[1].Value = pcsIndex + 1;
                    this.dGVPaste.Rows[rowIndex].Cells[2].Value = $"{this.RunData[pcbIndex][pcsIndex].UpPastePt.X:N2},{this.RunData[pcbIndex][pcsIndex].UpPastePt.Y:N2}";
                    this.dGVPaste.Rows[rowIndex].Cells[3].Value = this.RunData[pcbIndex][pcsIndex].PasteNozzle.ToString();
                    this.dGVPaste.Rows[rowIndex].Cells[4].Value = this.RunData[pcbIndex][pcsIndex].SuckFeeder.ToString();
                }
            }
        }

        private void bGoMark1_Click(object sender, EventArgs e)
        {
            if(this.dGVMark.SelectedRows.Count > 0)
            {
                int pcbIndex = this.dGVMark.SelectedRows[0].Index;
                SystemEntiy.Instance[this.Module].XYGoPos(this.RunData[pcbIndex].MarkData.Mark1);
            }
        }

        private void bGoMark2_Click(object sender, EventArgs e)
        {
            if (this.dGVMark.SelectedRows.Count > 0)
            {
                int pcbIndex = this.dGVMark.SelectedRows[0].Index;
                SystemEntiy.Instance[this.Module].XYGoPos(this.RunData[pcbIndex].MarkData.Mark2);
            }
        }

        private void bGoPaste_Click(object sender, EventArgs e)
        {
            if (this.dGVMark.SelectedRows.Count > 0)
            {
                int rowIndex = this.dGVMark.SelectedRows[0].Index;
                int pcbIndex = int.Parse(this.dGVPaste.Rows[rowIndex].Cells[0].Value.ToString())-1;
                int pcsIndex = int.Parse(this.dGVPaste.Rows[rowIndex].Cells[1].Value.ToString())-1;
                SystemEntiy.Instance[this.Module].XYGoPos(this.RunData[pcbIndex][pcsIndex].UpPastePt);
                this.curPcbIndex = pcbIndex;
                this.curPcsIndex = pcsIndex;
            }
        }

        private void bUpGrab_Click(object sender, EventArgs e)
        {
            var image = SystemEntiy.Instance[this.Module].SnapImage(Camera.Top);
            CommonAlgorithms.Copy(image, this.imageSet.Image);
            image?.Dispose();
        }

        private int curPcbIndex = 0;
        private int curPcsIndex = 0;
        private void bPrev_Click(object sender, EventArgs e)
        {
            curPcsIndex--;
            if (curPcsIndex < 0)
            {
                curPcsIndex = 0;
                curPcbIndex--;
                if (curPcbIndex < 0)
                    curPcbIndex = 0;
            }

            this.GoTo();
        }

        private void GoTo()
        {
            SystemEntiy.Instance[this.Module].XYGoPosTillStop(this.RunData[curPcbIndex][curPcsIndex].UpPastePt);
            Thread.Sleep(200);
            var image = SystemEntiy.Instance[this.Module].SnapImage(Camera.Top);
            CommonAlgorithms.Copy(image, this.imageSet.Image);
            image?.Dispose();
            this.lCur.Text = $"当前第 [{curPcbIndex + 1}] 板第 [{curPcsIndex + 1}] 个";
            if (bShowCross)
            {
                this.imageSet.Image.Overlays.Default.AddLine(new LineContour(new PointContour(this.imageSet.Image.Width / 2, 0),
                           new PointContour(this.imageSet.Image.Width / 2, this.imageSet.Image.Height)), Rgb32Value.RedColor);
                this.imageSet.Image.Overlays.Default.AddLine(new LineContour(new PointContour(0, this.imageSet.Image.Height / 2),
                new PointContour(this.imageSet.Image.Width, this.imageSet.Image.Height / 2)), Rgb32Value.RedColor);
            }

            this.bSetToSelect.BackColor = Color.Red;
        }
        private void bNext_Click(object sender, EventArgs e)
        {
            curPcsIndex++;
            if (curPcsIndex >= this.RunData[curPcbIndex].PCSCount)
            {
                curPcsIndex = 0;
                curPcbIndex++;
                if (curPcbIndex >= this.RunData.BoardCount)
                    curPcbIndex--;
            }

            this.GoTo();
        }

        private bool IsGetPoint = false;
        private PointContour GetPt = new PointContour();
        private void bGetPaste_Click(object sender, EventArgs e)
        {
            IsGetPoint = true;
        }

        private void imageSet_ImageMouseMove(object sender, NationalInstruments.Vision.WindowsForms.ImageMouseEventArgs e)
        {
            this.imageSet.Image.Overlays.Default.Clear();
            double angle = (double)this.numericUpDown1.Value;

            if (bShowCross)
            {
                PointContour center = new PointContour(this.imageSet.Image.Width / 2, this.imageSet.Image.Height / 2);
                this.showCross(angle, center, Rgb32Value.RedColor);
            }

            if (IsGetPoint)
            {
                this.showCross(angle, e.Point, Rgb32Value.YellowColor);
            }
        }

        public void showCross(double angle, PointContour center, Rgb32Value colcor)
        {
            double rate = angle / 180 * Math.PI;
            LineContour line1 = new LineContour(new PointContour(center.X - 2000 * Math.Sin(rate), center.Y - 2000 * Math.Cos(rate)),
              new PointContour(center.X + 2000 * Math.Sin(rate), center.Y + 2000 * Math.Cos(rate)));

            LineContour line2 = new LineContour(new PointContour(center.X + 2000 * Math.Cos(rate), center.Y - 2000 * Math.Sin(rate)),
           new PointContour(center.X - 2000 * Math.Cos(rate), center.Y + 2000 * Math.Sin(rate)));
            this.imageSet.Image.Overlays.Default.AddLine(line1, colcor);
            this.imageSet.Image.Overlays.Default.AddLine(line2, colcor);
        }

        private void imageSet_Resize(object sender, EventArgs e)
        {
            this.imageSet.ToolsShown = NationalInstruments.Vision.WindowsForms.ViewerTools.All;
            this.imageSet.ShowToolbar = true;
        }

        private bool bShowCross = true;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bShowCross = this.checkBox1.Checked;
        }

        private void imageSet_ImageMouseDown(object sender, NationalInstruments.Vision.WindowsForms.ImageMouseEventArgs e)
        {
            if (IsGetPoint)
            {
                this.GetPt = e.Point;
                double angle = (double)this.numericUpDown1.Value;
                double rate = angle / 180 * Math.PI;

                var cur = SystemEntiy.Instance[this.Module].XYPos;
                PointF wrold = new PointF();
                SystemEntiy.Instance[this.Module].WroldPt(Camera.Top, cur, e.Point, out wrold);
                this.tOffsetX.Text = (cur.X - wrold.X).ToString("f3");
                this.tOffsetY.Text = (cur.Y - wrold.Y).ToString("f3");

                IsGetPoint = false;
            }
        }

        private void bSetToNz1_Click(object sender, EventArgs e)
        {
            this.SetToNz(Nozzle.Nz1, this.bSetToNz1);
        }

        private void SetToNz(Nozzle nz, Button bt)
        {
            if (MessageBox.Show($"是否将该补偿值应用到吸嘴{(int)nz + 1}?Y/N", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var offset = this.Flow.NzOffset[nz];
                offset.X += float.Parse(this.tOffsetX.Text);
                offset.Y += float.Parse(this.tOffsetY.Text);
                this.Flow.NzOffset[nz] = offset;
                bt.BackColor = Color.LightGreen;
            }
        }

        private void bSetToNz2_Click(object sender, EventArgs e)
        {
            this.SetToNz(Nozzle.Nz2, this.bSetToNz2);

        }

        private void bSetToNz3_Click(object sender, EventArgs e)
        {
            this.SetToNz(Nozzle.Nz3, this.bSetToNz3);

        }

        private void bSetToNz4_Click(object sender, EventArgs e)
        {
            this.SetToNz(Nozzle.Nz4, this.bSetToNz4);
        }

        private void bSetToSelect_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"是否将应用该补偿值?Y/N", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    var node = this.Flow.PCB.Nodes[curPcbIndex].Nodes[0].Nodes[curPcsIndex] as Nodes.PasteParam;
                    if (node != null)
                    {
                        node.OffsetX += double.Parse(this.tOffsetX.Text);
                        node.OffsetY += double.Parse(this.tOffsetY.Text);
                        this.bSetToSelect.BackColor = Color.LightGreen;
                    }
                }
                catch { }
            }
        }

        private void bUpdateToFlow_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"是否保存程式? Y/N", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if(ProgramFlow.Save(this.Flow))
                {
                    MessageBox.Show("检验保存成功,请清理重新导入程式!!!");
                }
            }
        }

        private void bFull_Click(object sender, EventArgs e)
        {
            imageSet.ZoomToFit = true;
        }

        private void dGVPaste_SelectionChanged(object sender, EventArgs e)
        {
            if(this.dGVPaste.SelectedRows.Count > 0)
            {
                try {
                    this.curPcbIndex = int.Parse(this.dGVPaste.Rows[this.dGVPaste.SelectedRows[0].Index].Cells[0].Value.ToString()) - 1;
                    this.curPcsIndex = int.Parse(this.dGVPaste.Rows[this.dGVPaste.SelectedRows[0].Index].Cells[1].Value.ToString()) - 1;
                    this.lCur.Text = $"当前第 [{curPcbIndex + 1}] 板第 [{curPcsIndex + 1}] 个";
                }
                catch { }
            }
        }
    }
}
