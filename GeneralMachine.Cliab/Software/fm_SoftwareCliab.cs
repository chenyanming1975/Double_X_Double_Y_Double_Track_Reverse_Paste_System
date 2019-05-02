using GeneralMachine.Vision;
using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Flow;
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
using NationalInstruments.Vision;
using GeneralMachine.Flow.Tool;

namespace GeneralMachine.Cliab
{
    public partial class fm_SoftwareCliab : UserControl
    {
        public fm_SoftwareCliab()
        {
            InitializeComponent();
            this.selectNz3.NozzleChanged += (sender, Nz) => {
                this.UpDownValueChanged();
            };

            this.selectNz2.NozzleChanged += (sender, Nz) => {
                ShowROI?.Invoke(this,SystemEntiy.Instance[this.module.Module].MachineConfig[Nz].ViewRoi);
                this.tDetectX.Text = "0";
                this.tDetectY.Text = "0";

                this.tRotateX.Text = SystemEntiy.Instance[this.module.Module].MachineConfig[Nz].RotatePoint.X.ToString("f3");
                this.tRotateY.Text = SystemEntiy.Instance[this.module.Module].MachineConfig[Nz].RotatePoint.Y.ToString("f3");
            };

            this.selectNz4.NozzleChanged += (sender, Nz) => {
                this.labelCenterPos.Point = SystemEntiy.Instance[this.module.Module].MachineConfig[Nz].LabelCenterPos;
                this.nzPos.Point = SystemEntiy.Instance[this.module.Module].MachineConfig[Nz].NzPos;
                this.tNzToLabelX.Text = SystemEntiy.Instance[this.module.Module].MachineConfig[Nz].NzToLabelDist.X.ToString("f3");
                this.tNzToLabelY.Text = SystemEntiy.Instance[this.module.Module].MachineConfig[Nz].NzToLabelDist.Y.ToString("f3");
            };

            this.module.ModuleChange += (sender, Module) =>
            {
                this.labelCenterPos.Set(Module);
                this.nzPos.Set(Module);

                this.selectNz2.SelectNz = Nozzle.Nz1;
                this.selectNz3.SelectNz = Nozzle.Nz1;
                this.selectNz4.SelectNz = Nozzle.Nz1;
            };
        }

        private void UpDownValueChanged()
        {
            this.tUpDownX.Text = SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz3.SelectNz].NzToCam.X.ToString("f3");
            this.tUpDownY.Text = SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz3.SelectNz].NzToCam.Y.ToString("f3");

            upPt = SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz3.SelectNz].UpMarkPt;
            downPt = SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz3.SelectNz].DownMarkPt;
            pastePt = SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz3.SelectNz].PastePt;
        }

        #region Page1 九点标定
        /// <summary>
        /// 点间距
        /// </summary>
        private float xspace = 3.0f;
        private float yspace = 3.0f;


        private PointF orgPt = new PointF();
        private void bSetOrg_Click(object sender, EventArgs e)
        {
            orgPt = SystemEntiy.Instance[this.module.Module].XYPos;
        }

        private void bGoOrg_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[this.module.Module].XYGoPos(orgPt);
        }

        private void bLeanTemp_Click(object sender, EventArgs e)
        {
            fm_SoftwareCliab.LearnTemp?.Invoke();
        }

        private void bAutoCliab1_Click(object sender, EventArgs e)
        {
            if(fm_SoftwareCliab.FindTemp == null || fm_SoftwareCliab.FindTemp.GetInvocationList().Length <= 0)
            {
                MessageBox.Show("请打开相机轴控界面!!");
                return;
            }

            #region 九点标定
            Task.Factory.StartNew(() =>
            {
                float.TryParse(this.tXSpace.Text, out this.xspace);
                float.TryParse(this.tYSpace.Text, out this.yspace);

                List<PointF> WorldPt = new List<PointF>();
                PointF start = new PointF(orgPt.X - xspace, orgPt.Y - yspace);
                for (int i = 0; i < 3; ++i)
                    for (int j = 0; j < 3; ++j)
                        WorldPt.Add(new PointF(start.X + i * xspace, start.Y + j * yspace));

                // 拍照 标定 记录图像坐标
                List<PointContour> ImagePt = new List<PointContour>();
                var camera = CameraDefine.Instance.CameraList[this.module.Module][this.calibRatio1.SelectCam];
                var entiy = SystemEntiy.Instance[this.module.Module];
                for (int I = 0; I < WorldPt.Count; ++I)
                {
                    entiy.XYGoPosTillStop(WorldPt[I]);
                    // Grab Image and Calc
                    var result = fm_SoftwareCliab.FindTemp.Invoke(this.module.Module, this.calibRatio1.SelectCam);
                    if (result.State == VisionResultState.OK)
                    {
                        ImagePt.Add(result.Point);
                    }
                    else
                    {
                        MessageBox.Show("侦测失败!!");
                        return;
                    }
                }

                short rtn = 0;

                var image = GetImage?.Invoke();
                if (image == null)
                {
                    MessageBox.Show("获取图片失败!!!");
                    return;
                }

                if (this.calibRatio1.SelectCam == Camera.Top || this.calibRatio1.SelectCam == Camera.Label)
                    this.AddCliab(this.module.Module,this.calibRatio1.SelectCam, 0, image, WorldPt, ImagePt);
                else
                {
                    if (this.calibRatio1.SelectNz == Nozzle.Nz1 || this.calibRatio1.SelectNz == Nozzle.Nz3)
                        this.AddCliab(this.module.Module, this.calibRatio1.SelectCam, 0, image, WorldPt, ImagePt);
                    else
                        this.AddCliab(this.module.Module, this.calibRatio1.SelectCam, 1, image, WorldPt, ImagePt);
                }

                if (rtn == 0)
                {
                    CameraDefine.Save();
                    MessageBox.Show("相机标定成功!!");
                }
                else
                {
                    MessageBox.Show("相机标定成功!!");
                }
            });
            #endregion
        }

        private short AddCliab(Module module, Camera camera, int index, VisionImage image, List<PointF> WroldPt, List<PointContour> ImagePt)
        {
            CameraDefine.Instance.CameraList[module][camera].Mat2D[index].WorldLocs = WroldPt;
            CameraDefine.Instance.CameraList[module][camera].Mat2D[index].ImageLocs = ImagePt;
            return CameraDefine.Instance.CameraList[module][camera].Mat2D[index].Cam_Calibration(PathDefine.sPathCamera + $"{module}-{camera}-{index}.bmp", image);
        }

        #endregion

        #region Page2 吸嘴选择中心
        private void bMoveRotateCenter_Click(object sender, EventArgs e)
        {
            var module = this.module.Module;
            var nz = this.selectNz2.SelectNz;
            NozzleConfig config = SystemConfig.Instance.Machines[module][nz];
            PointF pt = new PointF();

            SystemEntiy.Instance[module].WroldPt(nz, config.RotateCamPoint, config.RotatePoint, out pt);
            SystemEntiy.Instance[module].XYGoPos(pt);
        }

        private void bCalRotateCenter_Click(object sender, EventArgs e)
        {
            if (fm_SoftwareCliab.FindTemp == null || fm_SoftwareCliab.FindTemp.GetInvocationList().Length <= 0)
            {
                MessageBox.Show("请打开相机轴控界面!!");
                return;
            }

            Task.Factory.StartNew(() => {
                var entiy = SystemEntiy.Instance[this.module.Module];
                PointF curPos = entiy.XYPos;
                Nozzle nz = this.selectNz2.SelectNz;
                PointContour circle = new PointContour();
                List<PointF> ImagePt = new List<PointF>();

                Camera camera = Camera.Bottom1;
                if(nz == Nozzle.Nz3 || nz == Nozzle.Nz4)
                {
                    camera = Camera.Bottom2;
                }

                // 12 次拟合
                for (int I = 0; I < 6; ++I)
                {
                    entiy.RGoAngleTillStop(I * 60, nz);
                    Thread.Sleep(200);

                    var result = fm_SoftwareCliab.FindTemp.Invoke(this.module.Module, camera);
                    if (result.State == VisionResultState.OK)
                    {
                        ImagePt.Add(new PointF((float)result.Point.X, (float)result.Point.Y));
                    }
                    else
                    {
                        MessageBox.Show("侦测失败!!");
                        return;
                    }
                }
                double x, y, r = 0;

                if(VisionHelper.FitCircle(ImagePt.ToArray(), out x, out y, out r))
                {
                    circle.X = x;
                    circle.Y = y;
                    this.BeginInvoke(new Action(() => {
                        this.tDetectX.Text = x.ToString("f3");
                        this.tDetectY.Text = y.ToString("f3");
                    }));
                    
                    MessageBox.Show("旋转中心检测成功!!");
                }
                else
                {
                    MessageBox.Show("拟合圆失败!!");
                }
            });
        }

        #endregion

        #region Page3 吸嘴 到 上视觉中心
        private PointF downPt;
        private PointF pastePt;
        private PointF upPt;

        private void bRecrodDown_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz3.SelectNz].DownMarkPt = SystemEntiy.Instance[this.module.Module].XYPos;
            //downPt = SystemEntiy.Instance[this.module.Module].XYPos;
        }

        private void bRecrodPaste_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz3.SelectNz].PastePt = SystemEntiy.Instance[this.module.Module].XYPos;
            //pastePt = SystemEntiy.Instance[this.module.Module].XYPos;
        }

        private void bRecrodUp_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz3.SelectNz].UpMarkPt = SystemEntiy.Instance[this.module.Module].XYPos;
            //upPt = SystemEntiy.Instance[this.module.Module].XYPos;
        }

        private void bCalcDist_Click(object sender, EventArgs e)
        {
            var config = SystemEntiy.Instance[this.module.Module].MachineConfig;
            PointF pt = new PointF();
            pt.X = config[this.selectNz3.SelectNz].PastePt.X - config[this.selectNz3.SelectNz].UpMarkPt.X;
            pt.Y = config[this.selectNz3.SelectNz].PastePt.Y - config[this.selectNz3.SelectNz].UpMarkPt.Y;

            config.NozzleMap[this.selectNz3.SelectNz].UpMarkPt = upPt;
            config.NozzleMap[this.selectNz3.SelectNz].DownMarkPt = downPt;
            config.NozzleMap[this.selectNz3.SelectNz].PastePt = pastePt;
            config.NozzleMap[this.selectNz3.SelectNz].NzToCam = pt;
            SystemConfig.Save();
            this.tUpDownX.Text = pt.X.ToString("f3");
            this.tUpDownY.Text = pt.Y.ToString("f3");
        }

        private void bCalDown_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(this.cbDownVision.Text))
            {
                VisionResult rtn = DetectLabel?.Invoke(this.cbDownVision.Text, this.module.Module, this.selectNz3.SelectNz);

                if(rtn.State == VisionResultState.OK)
                {
                    var entiy = SystemEntiy.Instance[this.module.Module];
                    var curPt = SystemEntiy.Instance[this.module.Module].XYPos;
                    //PointF wroldPt = new PointF();
                    //SystemEntiy.Instance[this.module.Module].WroldPt(this.selectNz3.SelectNz,
                    //    SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz3.SelectNz].RotateCamPoint,
                    //    rtn.Point, out wroldPt);
                    var wroldPt = SystemEntiy.Instance[this.module.Module].RotatePtDown(this.selectNz3.SelectNz, rtn.Point, -rtn.Angle);
                    entiy.XYGoPos(wroldPt);
                    entiy.RGoAngle(entiy.MachineAxis.R[(int)this.selectNz3.SelectNz].Pos - rtn.Angle, this.selectNz3.SelectNz);
                }
            }
        }

        private void bCalUp_Click(object sender, EventArgs e)
        {
            var entiy = SystemEntiy.Instance[this.module.Module];
            entiy.XYGoPos(entiy.NozzleToCam((Nozzle)selectNz3.SelectNz, entiy.XYPos));
        }


        private void bPasteTest_Click(object sender, EventArgs e)
        {
            if(this.cbDownVision.SelectedIndex < 0)
            {
                return;
            }

            var entiy = SystemEntiy.Instance[this.module.Module];
            Nozzle nz = this.selectNz3.SelectNz;
            // 记录要贴附的坐标
            PointF pasteXY = entiy.XYPos;
            double pasteAngle = (double)this.numPasteAngle.Value;

            // Z轴到安全高度
            entiy.ZGoSafeTillStop();
            // 到拍照位
            entiy.XYGoPosTillStop(entiy.MachineConfig[nz].RotateCamPoint);
            // R角度到0
            entiy.RGoAngleTillStop(0, nz);

            Thread.Sleep(50);

            // 拍照
            VisionResult rtn = DetectLabel?.Invoke(this.cbDownVision.Text, this.module.Module, this.selectNz3.SelectNz);
            if(rtn.State == VisionResultState.OK)
            {
                PointF result = entiy.RotatePtDown(nz, rtn.Point, -rtn.Angle+ pasteAngle);
                PointF offset = new PointF();
                offset.X = result.X - entiy.MachineConfig[nz].DownMarkPt.X;
                offset.Y = result.Y - entiy.MachineConfig[nz].DownMarkPt.Y;
                PointF realPt = new PointF();
                realPt.X = pasteXY.X + offset.X + entiy.MachineConfig[nz].NzToCam.X;
                realPt.Y = pasteXY.Y + offset.Y + entiy.MachineConfig[nz].NzToCam.Y;

                if (!this.cbCheck.Checked)
                {
                    realPt = entiy.GetPasteOffset(nz, pasteXY, realPt);
                
                }
                else
                {
                    this.upPt = pasteXY;
                    this.pastePt = realPt;
                }

                entiy.RGoAngle(-rtn.Angle+ pasteAngle, nz);
                entiy.XYGoPosTillStop(realPt);
                entiy.ZGoPaste(nz);
                Thread.Sleep(1000);
                entiy.MachineIO.VaccumSuck[(int)nz].SetIO(false);
                Thread.Sleep(100);
                entiy.MachineIO.VaccumPO[(int)nz].SetIO(true);
                Thread.Sleep(100);
                entiy.MachineIO.VaccumPO[(int)nz].SetIO(false);
                entiy.ZGoSafeTillStop();
                entiy.XYGoPosTillStop(pasteXY);
            }
            else
            {
                MessageBox.Show("Label 侦测失败!!");
            }
        }

        private void bGoDown_Click(object sender, EventArgs e)
        {
            var entiy = SystemEntiy.Instance[this.module.Module];
            entiy.XYGoPos(entiy.MachineConfig[this.selectNz3.SelectNz].DownMarkPt);
        }

        private void bGoPaste_Click(object sender, EventArgs e)
        {
            var entiy = SystemEntiy.Instance[this.module.Module];
            entiy.XYGoPos(entiy.MachineConfig[this.selectNz3.SelectNz].PastePt);
        }

        private void bGoUp_Click(object sender, EventArgs e)
        {
            var entiy = SystemEntiy.Instance[this.module.Module];
            entiy.XYGoPos(entiy.MachineConfig[this.selectNz3.SelectNz].UpMarkPt);
        }
        #endregion

        #region 委托
        /// <summary>
        /// 学习模板
        /// </summary>
        public static event Action LearnTemp;

        /// <summary>
        /// 寻找模板
        /// </summary>
        public static event Func<Module,Camera,VisionResult> FindTemp;

        /// <summary>
        /// 识别label
        /// </summary>
        public static event Func<string, Module, Nozzle, VisionResult> DetectLabel;

        /// <summary>
        /// 获取图片
        /// </summary>
        public static event Func<VisionImage> GetImage;

        /// <summary>
        /// 界面关闭事件
        /// </summary>
        public event EventHandler CloseEvent;
        #endregion

        private void wizardControl1_CancelButtonClick(object sender, EventArgs e)
        {
            this.CloseEvent?.Invoke(this, e);
        }


        private void startStep3_Enter(object sender, EventArgs e)
        {

        }

        private void bSetViewROI_Click(object sender, EventArgs e)
        {
            var roi = GetRoi?.Invoke();
            if (roi != null && roi.Count > 0 && roi[0].Shape.GetType() == typeof(RectangleContour))
            {
                SystemConfig.Instance.Machines[this.module.Module].NozzleMap[this.selectNz2.SelectNz].ViewRoi = roi[0].Shape.Clone() as RectangleContour;
                SystemConfig.Save();
            }
            else
            {
                MessageBox.Show("请正确绘制ROI");
            }
        }

        /// <summary>
        /// 获取ROI 事件
        /// </summary>
        public static event Func<Roi> GetRoi;

        /// <summary>
        /// 显示ROI
        /// </summary>
        public static event EventHandler<RectangleContour> ShowROI;

        private void bGoCamPos_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[this.module.Module].XYGoPos(
    SystemEntiy.Instance[this.module.Module].MachineConfig.NozzleMap[this.selectNz2.SelectNz].RotateCamPoint);
        }

        private void wizardControl1_NextButtonClick(WizardBase.WizardControl sender, WizardBase.WizardNextButtonClickEventArgs args)
        {
            this.cbDownVision.Items.Clear();

            var test = VisionToolCtrl.GetVisionList().ToArray();
            this.cbDownVision.Items.AddRange(test);
        }

        private void bUpdateRotate_Click(object sender, EventArgs e)
        {
            try
            {
                double x = double.Parse(this.tDetectX.Text);
                double y = double.Parse(this.tDetectY.Text);
                this.tRotateX.Text = this.tDetectX.Text;
                this.tRotateY.Text = this.tDetectY.Text;
                SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz2.SelectNz].RotatePoint = new PointContour(x,y);
                SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz2.SelectNz].RotateCamPoint = SystemEntiy.Instance[this.module.Module].XYPos;
                SystemConfig.Save();

                PointF captrue = SystemEntiy.Instance[this.module.Module].XYPos;

                for(Nozzle nz = Nozzle.Nz1; nz <= Nozzle.Nz4; ++nz)
                {
                    if(SystemEntiy.Instance[this.module.Module].MachineConfig[this.selectNz2.SelectNz].RotateCamPoint != captrue)
                    {
                        MessageBox.Show("吸嘴拍照位置不一致!!请确认");
                        return;
                    }
                }
            }
            catch { }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            PointF nzDist = new PointF();
            nzDist.X = this.labelCenterPos.Point.X - this.nzPos.Point.X;
            nzDist.Y = this.labelCenterPos.Point.Y - this.nzPos.Point.Y;
            SystemConfig.Instance.Machines[this.module.Module][this.selectNz4.SelectNz].NzToLabelDist = nzDist;

            SystemConfig.Instance.Machines[this.module.Module][this.selectNz4.SelectNz].LabelCenterPos =
                this.labelCenterPos.Point;
            SystemConfig.Instance.Machines[this.module.Module][this.selectNz4.SelectNz].NzPos =
               this.nzPos.Point;

            this.tNzToLabelY.Text = (this.labelCenterPos.Point.Y - this.nzPos.Point.Y).ToString("f3");
            this.tNzToLabelX.Text = (this.labelCenterPos.Point.X - this.nzPos.Point.X).ToString("f3");
            this.tNzToLabelY.Text = (this.labelCenterPos.Point.Y - this.nzPos.Point.Y).ToString("f3");
            SystemConfig.Save();
        }
        #region 指哪儿打哪儿校准
        private PointF pastePos = new PointF();
        private PointF needPastePos = new PointF();

        private void bSetPastePos_Click(object sender, EventArgs e)
        {
            pastePos = SystemEntiy.Instance[this.module.Module].XYPos;
        }

        private void bSetNeedPos_Click(object sender, EventArgs e)
        {
            needPastePos = SystemEntiy.Instance[this.module.Module].XYPos;
        }

        private void bSetOffset_Click(object sender, EventArgs e)
        {
            var nzConfig = SystemConfig.Instance.Machines[this.module.Module].NozzleMap[this.selectNz3.SelectNz];
            var pt = nzConfig.NzToCam;
            pt.X = pt.X + (needPastePos.X - pastePos.X);
            pt.Y = pt.Y + (needPastePos.Y - pastePos.Y);

            if (MessageBox.Show($"偏差X:{needPastePos.X - pastePos.X} Y:{needPastePos.Y - pastePos.Y} 是否修改 Y/N"
                , "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                nzConfig.NzToCam = pt;
                this.tUpDownX.Text = pt.X.ToString("f3");
                this.tUpDownY.Text = pt.Y.ToString("f3");
                SystemConfig.Instance.Machines[this.module.Module][this.selectNz3.SelectNz].UpMarkPt =
                    this.upPt;
                SystemConfig.Instance.Machines[this.module.Module][this.selectNz3.SelectNz].PastePt =
                  this.pastePt;
                SystemConfig.Save();
                needPastePos = new PointF();
                pastePos = new PointF();
            }
        }
        #endregion

        private void fm_SoftwareCliab_Load(object sender, EventArgs e)
        {
            this.module.Module = Module.Front;
        }
    }
}