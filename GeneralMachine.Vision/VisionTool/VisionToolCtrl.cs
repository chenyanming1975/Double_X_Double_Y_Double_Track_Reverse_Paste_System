using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.Vision.Analysis;
using NationalInstruments.Vision;
using System.Diagnostics;
using GeneralMachine.Common;

namespace GeneralMachine.Vision
{
    public partial class VisionToolCtrl : UserControl
    {
        public static readonly string sPathVision = "D://程式//视觉方法//";
        /// <summary>
        /// 获取视觉列表
        /// </summary>
        /// <returns></returns>
        public static string[] GetVisionList()
        {
            return Common.CommonHelper.GetDirectoryName(sPathVision).ToArray();
        }

        public VisionToolCtrl()
        {
            InitializeComponent();
            this.imageSet.ShowToolbar = true;
            this.imageSet.ToolsShown = NationalInstruments.Vision.WindowsForms.ViewerTools.All;
            this.imageSet.ZoomToFit = true;
        }

        private VisionFlow flow = null;
        public VisionFlow Flow
        {
            get
            {
                return this.flow;
            }

            set
            {
                if(value != null)
                {
                    this.detectBindingSource.Clear();
                    this.flow = value;
                    this.detectBindingSource.DataSource = flow.Detects;
                }
            }
        }

        private void bHandleImage_Click(object sender, EventArgs e)
        {
    
        }

        private void bHandle_Click(object sender, EventArgs e)
        {
            //foreach(var flow in Flow.Detects)
            //{
            //    flow.Detected(this.imageSet.Image);
            //}
        }

       
        #region 参数预览
        private int index = 0;
        private void bPrev_Click(object sender, EventArgs e)
        {
            if (this.dGV_VisionUnit.SelectedRows.Count == 1)
            {
                int rowIndex = this.dGV_VisionUnit.SelectedRows[0].Index;
                if (index > 0)
                {
                    index--;
                    if (index == 0)
                    {
                        this.propertyGrid1.SelectedObject = this.detectBindingSource[rowIndex];
                    }
                    else
                    {
                        this.propertyGrid1.SelectedObject = (this.detectBindingSource[rowIndex] as Detect).OptionList[index - 1];
                    }
                }
            }
        }

        private void bNext_Click(object sender, EventArgs e)
        {
            if (this.dGV_VisionUnit.SelectedRows.Count == 1)
            {
                int rowIndex = this.dGV_VisionUnit.SelectedRows[0].Index;
                var detect = this.detectBindingSource[rowIndex] as Detect;
                if (index < detect.OptionList.Count)
                {
                    this.propertyGrid1.SelectedObject = detect.OptionList[index++];
                }
            }

        }
        #endregion


        private void imageSet_Resize(object sender, EventArgs e)
        {
            this.imageSet.ShowToolbar = true;
            this.imageSet.ToolsShown = NationalInstruments.Vision.WindowsForms.ViewerTools.All;
        }

        #region 程式加载和读取
        private void toolNewProgram_Click(object sender, EventArgs e)
        {
            fmCreateVisionProgram fm = new fmCreateVisionProgram();
            if (fm.ShowDialog() == DialogResult.OK)
            {
                this.Flow = new VisionFlow();
                this.Flow.FlowName = fm.VisionProgram;
                ChangedPresstion(true);
            }
        }

        public void ChangedPresstion(bool enable = false)
        {
            this.dGV_VisionUnit.Enabled = enable;
            this.toolVisionTools.Enabled = enable;
            this.toolCurStep.Enabled = enable;
            this.toolSelect.Enabled = enable;
            this.toolHandler.Enabled = enable;
            this.toolFlowHandler.Enabled = enable;
            this.toolLearnTemp.Enabled = enable;
            this.toolStepDelete.Enabled = enable;
            this.toolInitPos.Enabled = enable;
        }

        public void ReadToUI()
        {
            this.bRed.Checked = this.flow.RedUsed;
            this.bGreen.Checked = this.flow.GreenUsed;
            this.bBlue.Checked = this.flow.BlueUsed;
            this.numRed.Value = this.flow.RedValue;
            this.numGreen.Value = this.flow.GreenValue;
            this.numBlue.Value = this.flow.BlueValue;
            this.numExp.Value = this.flow.Exprouse;
        }

        public void SaveToProgram()
        {
            this.flow.RedUsed = this.bRed.Checked;
            this.flow.GreenUsed = this.bGreen.Checked;
            this.flow.BlueUsed = this.bBlue.Checked;
            this.flow.RedValue = (int)this.numRed.Value;
            this.flow.GreenValue = (int)this.numGreen.Value;
            this.flow.BlueValue = (int)this.numBlue.Value;
            this.flow.Exprouse = (int)this.numExp.Value;
        }

        private void toolReadProgram_Click(object sender, EventArgs e)
        {
            fmCreateVisionProgram fm = new fmCreateVisionProgram(false);

            if (fm.ShowDialog(this) == DialogResult.OK)
            {
                var flow = VisionFlow.Load(sPathVision+ fm.VisionProgram);
                if (flow == null)
                {
                    MessageBox.Show($"导入视觉程式失败!!!");
                    ChangedPresstion(false);
                    return;
                }

                this.Flow = flow;
                ChangedPresstion(true);
                ReadToUI();
            }
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            fmVisionOutput fm = new fmVisionOutput(this.flow);
            if (fm.ShowDialog() == DialogResult.Yes)
            {
                this.SaveToProgram();

                if (this.Flow != null)
                {
                    if (VisionFlow.Save(sPathVision, this.Flow))
                        MessageBox.Show("保存成功!!");
                    else
                        MessageBox.Show("保存失败!!");
                }
            }
        }
        private void toolLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        imageSet.Image.ReadFile(dialog.FileName);
                    }
                    catch { }
                }
            }

        }
        #endregion

        #region 视觉单元
        private void toolImageOffset_Click(object sender, EventArgs e)
        {
            this.detectBindingSource.Add(new DetectOffset());
        }

        private void toolMeanImage_Click(object sender, EventArgs e)
        {
            //this.detectBindingSource.Add(new DetectOffset());
        }

        private void toolThread_Click(object sender, EventArgs e)
        {
            //this.Flow.Detects.Add(new DetectOffset());
        }

        private void toolFindLine_Click(object sender, EventArgs e)
        {
            var line = new DetectLine();
            line.InitOption();
            this.detectBindingSource.Add(line);
        }

        private void toolFindCircle_Click(object sender, EventArgs e)
        {
            var circle = new DetectCircle();
            circle.InitOption();
            this.detectBindingSource.Add(circle);
        }

        private void toolFindEillpe_Click(object sender, EventArgs e)
        {
        }

        private void toolGrayMatch_Click(object sender, EventArgs e)
        {
            var shape = new DetectPttern();
            shape.InitOption();
            this.detectBindingSource.Add(shape);
        }


        private void toolEdgeMatch_Click(object sender, EventArgs e)
        {
            var shape = new DetectShapeMatch();
            shape.InitOption();
            this.detectBindingSource.Add(shape);
        }

        private void toolShapMatch_Click(object sender, EventArgs e)
        {
            var shape = new DetectGeometric();
            shape.InitOption();
            this.detectBindingSource.Add(shape);
        }


        private void toolBaseCrood_Click(object sender, EventArgs e)
        {

        }

        private void toolOutputOffset_Click(object sender, EventArgs e)
        {
            var offset = new DetectAdjustOutput();
            this.detectBindingSource.Add(offset);
        }


        private void toolAreaCount_Click(object sender, EventArgs e)
        {
            var area = new DetectArea();
            this.detectBindingSource.Add(area);
        }
        #endregion

        private void toolHandler_Click(object sender, EventArgs e)
        {
            if (this.dGV_VisionUnit.SelectedRows.Count == 1)
            {
                Stopwatch wacth = new Stopwatch();
                wacth.Restart();
                this.imageSet.Image.Overlays.Default.Clear();
                var detect = this.detectBindingSource[this.dGV_VisionUnit.SelectedRows[0].Index] as Detect;

                if (this.imageSet.Roi.Count > 0)
                {
                    detect.ROI = this.imageSet.Roi[0].Shape.Clone();
                }

                var rtn = detect.Detected(this.imageSet.Image);
                wacth.Stop();
                this.lTime.Text = $"耗时:{wacth.ElapsedMilliseconds} ms";
                this.listVisionState.Items.Clear();
                this.listVisionState.Items.AddRange(rtn.VisionDesr.ToArray());
                if (rtn.State == VisionResultState.OK)
                {
                    this.lImageInfo.Text = "OK";
                    this.lImageInfo.BackColor = Color.LightGreen;
                }
                else
                {
                    this.lImageInfo.Text = "NG  ";
                    this.lImageInfo.BackColor = Color.Red;
                }
            }
        }

        private void toolFlowHandler_Click(object sender, EventArgs e)
        {
            if (this.dGV_VisionUnit.Rows.Count > 0)
            {
                Stopwatch wacth = new Stopwatch();
                wacth.Restart();
                this.imageSet.Image.Overlays.Default.Clear();
                var rtn = this.Flow.Detect(this.imageSet.Image);

                wacth.Stop();
                this.lTime.Text = $"耗时:{wacth.ElapsedMilliseconds} ms";
                this.listVisionState.Items.Clear();
                this.listVisionState.Items.AddRange(rtn.VisionDesr.ToArray());
                if (rtn.State == VisionResultState.OK)
                {
                    this.lImageInfo.Text = "OK";
                    this.lImageInfo.BackColor = Color.LightGreen;
                }
                else
                {
                    this.lImageInfo.Text = "NG  ";
                    this.lImageInfo.BackColor = Color.Red;
                }
            }
        }

        private void toolHandlerStop_Click(object sender, EventArgs e)
        {

        }

        private void toolFindCross_Click(object sender, EventArgs e)
        {
            var cross = new DetectCross();
            this.detectBindingSource.Add(cross);
        }

        private void dGV_VisionUnit_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dGV_VisionUnit.SelectedRows.Count == 1)
            {
                this.imageSet.Image.Overlays.Default.Clear();
                this.imageSet.Roi.Clear();

                var detect = this.detectBindingSource[this.dGV_VisionUnit.SelectedRows[0].Index] as Detect;

                if (detect.GetType() == typeof(DetectPttern) || 
                    detect.GetType() == typeof(DetectGeometric)
                    || detect.GetType() == typeof(DetectShapeMatch))
                {
                    this.toolLearnTemp.Visible = true;
                }
                else
                    this.toolLearnTemp.Visible = false;

                if((detect.Type & ResultType.Init) == ResultType.Init)
                {
                    this.toolInitPos.Visible = true;
                }
                else
                {
                    this.toolInitPos.Visible = false;
                }

                if (detect.ROI != null)
                    this.imageSet.Roi.Add(detect.ROI);

                this.propertyGrid1.SelectedObject = detect;
                this.toolCurStep.Text = detect.UnitID;
            }
        }

        private void toolFitLine_Click(object sender, EventArgs e)
        {
            var fit = new DetectFitLine();
            this.detectBindingSource.Add(fit);
        }

        private void toolFitCircle_Click(object sender, EventArgs e)
        {
            var fit = new DetectFitCicle();
            this.detectBindingSource.Add(fit);
        }

        private void bStroe_Click(object sender, EventArgs e)
        {
            var lightAndExp = GetLightAndExp?.Invoke();
            this.bRed.Checked = lightAndExp.Item1;
            this.bGreen.Checked = lightAndExp.Item2;
            this.bBlue.Checked = lightAndExp.Item3;
            this.numRed.Value = lightAndExp.Item4;
            this.numGreen.Value = lightAndExp.Item5;
            this.numBlue.Value = lightAndExp.Item6;
            this.numExp.Value = lightAndExp.Item7;
        }

        private void toolInputCamrea_Click(object sender, EventArgs e)
        {
            var img = GetImage?.Invoke();
            Algorithms.Copy(img, this.imageSet.Image);
        }

        /// <summary>
        /// 从相机获取图片资源
        /// </summary>
        public static event Func<VisionImage> GetImage;

        public static event Func<Tuple<bool, bool, bool, int, int, int, int>> GetLightAndExp;

        public static event EventHandler<Tuple<bool, bool, bool, int, int, int, int>> SetLightAndExp;

        private void toolStepDelete_Click(object sender, EventArgs e)
        {
            if (this.dGV_VisionUnit.SelectedRows.Count > 0)
            {
                int i = this.dGV_VisionUnit.SelectedRows[0].Index;
                this.detectBindingSource.RemoveAt(i);
            }
        }

        private void bUsed_Click(object sender, EventArgs e)
        {
            SetLightAndExp?.Invoke(this, new Tuple<bool, bool, bool, int, int, int, int>
                (this.flow.RedUsed,
                this.flow.GreenUsed,
                this.flow.BlueUsed,
                this.flow.RedValue,
                this.flow.GreenValue,
                this.flow.BlueValue,
                this.flow.Exprouse
                ));
        }

        private void toolLearnTemp_Click(object sender, EventArgs e)
        {
            if (this.dGV_VisionUnit.SelectedRows.Count == 1 && this.imageSet.Roi.Count > 0
                && this.imageSet.Roi[0].Shape.GetType() == typeof(RectangleContour))
            {
                var detect = this.detectBindingSource[this.dGV_VisionUnit.SelectedRows[0].Index] as Detect;
               
                if(detect.Lerarn(this.imageSet.Image, this.imageSet.Roi))
                {
                    MessageBox.Show("模板学习成功!!");
                }
                else
                    MessageBox.Show("模板学习失败!!");
            }
        }

        private void toolInitPos_Click(object sender, EventArgs e)
        {
            if (this.dGV_VisionUnit.SelectedRows.Count == 1)
            {
                Stopwatch wacth = new Stopwatch();
                wacth.Restart();
                this.imageSet.Image.Overlays.Default.Clear();
                var detect = this.detectBindingSource[this.dGV_VisionUnit.SelectedRows[0].Index] as Detect;

                if (this.imageSet.Roi.Count > 0)
                {
                    detect.ROI = this.imageSet.Roi[0].Shape.Clone();
                }

                var rtn = detect.Detected(this.imageSet.Image);
                wacth.Stop();

                this.lTime.Text = $"耗时:{wacth.ElapsedMilliseconds} ms";
                this.listVisionState.Items.Clear();
                this.listVisionState.Items.AddRange(rtn.VisionDesr.ToArray());
                if (rtn.State == VisionResultState.OK)
                {
                    this.flow.OrgPoint = rtn.Point;
                    this.flow.BaseAngle = rtn.Angle;

                    this.lImageInfo.Text = "OK";
                    this.lImageInfo.BackColor = Color.LightGreen;
                }
                else
                {
                    this.lImageInfo.Text = "NG";
                    this.lImageInfo.BackColor = Color.Red;
                }
            }
        }

        private void toolCode_Click(object sender, EventArgs e)
        {
            var code = new DetectCode();
            this.detectBindingSource.Add(code);
        }

        private void imageSet_Resize_1(object sender, EventArgs e)
        {

        }
    }
}
