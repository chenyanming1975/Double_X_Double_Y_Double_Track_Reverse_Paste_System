using GeneralMachine.Vision;
using GeneralMachine.Definition;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralMachine.Flow;
using GeneralMachine.Config;

namespace GeneralMachine
{
    public partial class frm_DryRun : UserControl
    {
        public frm_DryRun(Object obj)
        {
            InitializeComponent();
            frm_Main = (frm_Main)obj;
            this.moduleRadio1.ModuleChange += (sender,module) => {
                TestPos.Clear();
                this.listBox1.Items.Clear();
                if (this.TestTask != null && !this.TestTask.IsCompleted)
                {
                    SystemEntiy.Instance[this.Module].StopAllAxis();
                    this.IsTest = false;
                }

                this.Module = module;
            };
        }

        public frm_Main frm_Main = null;
        public Module Module = Module.Front;
        public bool IsTest = false;

        public frm_DryRun()
        {
            InitializeComponent();
        }

        private void bLearnMove_Click(object sender, EventArgs e)
        {

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {

        }

        private void bRUN_Click(object sender, EventArgs e)
        {
            var shceme = (Motion.Shceme)(this.cb_SelectSpeed.SelectedIndex + 3);
            if(!this.IsTest)
            {
                this.TestTask = Task.Factory.StartNew(() =>
                {
                    this.TestRun(shceme);
                });
            }
        }

        private void btnStatisTest_Click(object sender, EventArgs e)
        {

        }

        private void btnDynamicTest_Click(object sender, EventArgs e)
        {

        }

        private List<PointF> TestPos = new List<PointF>();

        private void bAddPos_Click(object sender, EventArgs e)
        {
            var pt = SystemEntiy.Instance[this.Module].XYPos;
            this.listBox1.Items.Add($"X:{pt.X},Y:{pt.Y}");
            TestPos.Add(pt);
        }

        private void bDeletePos_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.RemoveAt(this.listBox1.SelectedIndex);
            this.TestPos.RemoveAt(this.listBox1.SelectedIndex);
        }

        private void bClearAll_Click(object sender, EventArgs e)
        {
            TestPos.Clear();
            this.listBox1.Items.Clear();
        }

        private Task TestTask = null;

        private void TestRun(Motion.Shceme speed)
        {
            this.IsTest = true;
            for (int i = 0; i < this.numTestCycle.Value;i++)
            {
                foreach (PointF pt in this.TestPos)
                {
                    SystemEntiy.Instance[this.Module].XYGoPosTillStop(pt, speed);
                }

                if(!this.IsTest)
                {
                    this.IsTest = false;
                    return;
                }

                Thread.Sleep((int)this.numTestDelay.Value);
            }
            this.IsTest = false;
        }

        private void bMove_Click(object sender, EventArgs e)
        {
            SystemEntiy.Instance[this.Module].XYGoPosTillStop(this.TestPos[this.listBox1.SelectedIndex]);
        }

        private void bTestZ_Click(object sender, EventArgs e)
        {
            if (!this.IsTest)
            {
                int cycle = (int)this.numCycle.Value;
                int delay = (int)this.numDelay.Value;
                Nozzle nz = (Nozzle)this.cbSelectNz.SelectedIndex;
                var shceme = (Motion.Shceme)(this.cbSelectSp2.SelectedIndex + 3);
                Module module = this.moduleRadio1.Module;

                this.TestTask = Task.Factory.StartNew(() =>
                {
                    for(int i = 0;i < cycle;++i)
                    {
                        SystemEntiy.Instance[module].ZGoPosTillStop(nz, SystemConfig.Instance.Machines[module][nz].PasteHeight, shceme);
                        Thread.Sleep(delay);
                        SystemEntiy.Instance[module].ZGoSafeTillStop(shceme);
                        Thread.Sleep(delay);
                    }
                });
            }
        }

        private void moduleRadio1_ModuleChange(object sender, Module e)
        {
            this.Module = e;
        }
        //public HObject Hroi;
        //public HObject imageSet;
        //public Vision.RectangleRegion roi;
        //public Vision.MatchResults[] results;
        //public int Module = 0;

        //public HTuple Learn_ModelID1 = null;
        //public HObject CountTrans1 = null;
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SpeedMode mode = SpeedMode.Manual_Slow;
        //        if (this.comboBox2.SelectedIndex == 0)
        //        {
        //            mode = SpeedMode.Auto_Fast;
        //        }
        //        else if (this.comboBox2.SelectedIndex == 1)
        //        {
        //            mode = SpeedMode.Auto_Normal;
        //        }
        //        else
        //        {
        //            mode = SpeedMode.Auto_Slow;
        //        }

        //        Axis_RunParam axis = null;
        //        if (this.comboBox1.SelectedIndex == 0) //X
        //        {
        //            axis = GeneralMachine.VisionSystem.Instance.Machines[Module].X;
        //        }
        //        else if (this.comboBox1.SelectedIndex == 1) //y
        //        {
        //            axis = GeneralMachine.VisionSystem.Instance.Machines[Module].Y;
        //        }
        //        else if (this.comboBox1.SelectedIndex == 2) //z1
        //        {
        //            axis = GeneralMachine.VisionSystem.Instance.Machines[Module].ZMap[0];
        //        }
        //        else if (this.comboBox1.SelectedIndex == 3) //z2
        //        {
        //            axis = GeneralMachine.VisionSystem.Instance.Machines[Module].ZMap[1];
        //        }
        //        else if (this.comboBox1.SelectedIndex == 4) // U
        //        {
        //            axis = GeneralMachine.VisionSystem.Instance.Machines[Module].RMap[0];
        //        }

        //        for (int i = 0; i < 1; ++i)
        //        {
        //            axis.GoPosTillStop(300000, double.Parse(this.textBox1.Text), GeneralMachine.VisionSystem.Instance.Machines[module].GetVel(mode));
        //            axis.GoPosTillStop(300000, double.Parse(this.textBox2.Text), GeneralMachine.VisionSystem.Instance.Machines[module].GetVel(mode));
        //        }
        //    }
        //    catch { }

        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (this.comboBox1.SelectedIndex == 0) //X
        //    {
        //        this.textBox1.Text = GeneralMachine.VisionSystem.Instance.Machines[Module].X.Pos.ToString("f3");
        //    }
        //    else if (this.comboBox1.SelectedIndex == 1) //y
        //    {
        //        this.textBox1.Text = GeneralMachine.VisionSystem.Instance.Machines[Module].Y.Pos.ToString("f3");
        //    }
        //    else if (this.comboBox1.SelectedIndex == 2) //z1
        //    {
        //        this.textBox1.Text = GeneralMachine.VisionSystem.Instance.Machines[Module].ZMap[0].Pos.ToString("f3");
        //    }
        //    else if (this.comboBox1.SelectedIndex == 3) //z2
        //    {
        //        this.textBox1.Text = GeneralMachine.VisionSystem.Instance.Machines[Module].ZMap[1].Pos.ToString("f3");
        //    }
        //    else if (this.comboBox1.SelectedIndex == 4) // U
        //    {
        //        this.textBox1.Text = GeneralMachine.VisionSystem.Instance.Machines[Module].RMap[0].Pos.ToString("f3");
        //    }
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    if (this.comboBox1.SelectedIndex == 0) //X
        //    {
        //        this.textBox2.Text = GeneralMachine.VisionSystem.Instance.Machines[Module].X.Pos.ToString("f3");
        //    }
        //    else if (this.comboBox1.SelectedIndex == 1) //y
        //    {
        //        this.textBox2.Text = GeneralMachine.VisionSystem.Instance.Machines[Module].Y.Pos.ToString("f3");
        //    }
        //    else if (this.comboBox1.SelectedIndex == 2) //z1
        //    {
        //        this.textBox2.Text = GeneralMachine.VisionSystem.Instance.Machines[Module].ZMap[0].Pos.ToString("f3");
        //    }
        //    else if (this.comboBox1.SelectedIndex == 3) //z2
        //    {
        //        this.textBox2.Text = GeneralMachine.VisionSystem.Instance.Machines[Module].ZMap[1].Pos.ToString("f3");
        //    }
        //    else if (this.comboBox1.SelectedIndex == 4) // U
        //    {
        //        this.textBox2.Text = GeneralMachine.VisionSystem.Instance.Machines[Module].RMap[0].Pos.ToString("f3");
        //    }
        //}

        ///// <summary>
        ///// 更新图像信息
        ///// </summary>
        ///// <returns></returns>
        //private bool UpdateImage_Roi()
        //{
        //    try
        //    {
        //        Hroi?.Dispose();
        //        imageSet?.Dispose();
        //        frm_Main.frm_Camera.imageSet.Image.BorderWidth = 0;
        //        imageSet = frm_Main.Image(frm_Main.frm_Camera.imageSet.Image);
        //        roi = frm_Main.CROI(frm_Main.frm_Camera.imageSet.Roi);
        //        Hroi = frm_Main.HROI(frm_Main.frm_Camera.imageSet.Roi);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //private void bLearnMove_Click(object sender, EventArgs e)
        //{
        //    if (UpdateImage_Roi())
        //    {
        //        if (!Vision.CreatNCCTemplate(imageSet, Hroi, out Learn_ModelID1))
        //        {
        //            MessageBox.Show("模板学习失败!");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("获得图像失败!");
        //    }
        //}

        //private void btnStaticTest_Click(object sender, EventArgs e)
        //{
        //    int count = (int)ndCount.Value;
        //    int module = this.rB_Module1.Checked ? 0 : 1;
        //    bool leftNozzle = this.rbLeftNozzle.Checked;
        //    bool isUpVision = this.radioUpVision.Checked;
        //    Task.Factory.StartNew(()=>
        //    {
        //        this.Invoke(new Action(()=>
        //        {
        //            this.Enabled = false;
        //        }));
        //        List<PointF> resultPoint = new List<PointF>();
        //        for (int i = 0; i < count; i++)
        //        {
        //            PointF pt = new PointF();
        //            if (isUpVision)
        //            {
        //                GeneralMachine.VisionSystem.Instance.Machines[module]._session_Up.Snap(frm_Main.frm_Camera.imageSet.Image);
        //            }
        //            else
        //            {
        //                GeneralMachine.VisionSystem.Instance.Machines[module]._session_Down.Snap(frm_Main.frm_Camera.imageSet.Image);
        //            }

        //            if (UpdateImage_Roi())
        //            {
        //                bool rtn = false;
        //                rtn = Vision.FindNccTemplate(imageSet, this.roi, Learn_ModelID1, 1, 0.4, 0, 360, out results);

        //                if(rtn && results!= null && results.Count()>0)
        //                {
        //                    pt = results[0].XYCoord;
        //                    resultPoint.Add(pt);

        //                    PointF worldPt = new PointF();
        //                    if (isUpVision)
        //                    {
        //                        GeneralMachine.VisionSystem.Instance.Machines[module].MyHalCali_UP.Pixel2World(pt, out worldPt);
        //                        worldPt.X = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.X + GeneralMachine.VisionSystem.Instance.Machines[module].WorldCenterPoint_Up.X - worldPt.X;
        //                        worldPt.Y = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.Y + GeneralMachine.VisionSystem.Instance.Machines[module].WorldCenterPoint_Up.Y - worldPt.Y;
        //                    }
        //                    else
        //                    {
        //                        GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].MyHalCali.Pixel2World(pt, out worldPt);
        //                        worldPt.X = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.X + GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].WorldCenterPoint.X - worldPt.X;
        //                        worldPt.Y = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.Y + GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].WorldCenterPoint.Y - worldPt.Y;
        //                    }

        //                    this.Invoke(new Action(() =>
        //                    {
        //                        this.tbRealYPos.Text = worldPt.X.ToString("F5");
        //                        this.tbRealXPos.Text = worldPt.Y.ToString("F5");
        //                    }));
        //                }//end if
        //            }//end if

        //        }// end for

        //        string up = isUpVision ? "上" : "下";
        //        string modelName = module ==0 ? "前" : "后";
        //        string nozzle = leftNozzle ? "左" : "右";

        //        string path = string.Empty;
        //        if (isUpVision)
        //        {
        //            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + modelName + up + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
        //        }
        //        else
        //        {
        //            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + modelName + up + nozzle + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
        //        }

        //        using (TextWriter tw = new StreamWriter(path, false))
        //        {
        //            string message = string.Empty;
        //            tw.WriteLine("序号,机器X坐标,机器Y坐标,相机像素X坐标,相机像素Y坐标");
        //            for (int i = 0; i < resultPoint.Count;++i)
        //            {
        //                PointF pt = resultPoint[i];
        //                PointF worldPt = new PointF();
        //                if (isUpVision)
        //                {
        //                    GeneralMachine.VisionSystem.Instance.Machines[module].MyHalCali_UP.Pixel2World(pt, out worldPt);
        //                    worldPt.X = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.X + GeneralMachine.VisionSystem.Instance.Machines[module].WorldCenterPoint_Up.X - worldPt.X;
        //                    worldPt.Y = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.Y + GeneralMachine.VisionSystem.Instance.Machines[module].WorldCenterPoint_Up.Y - worldPt.Y;
        //                }
        //                else
        //                {
        //                    GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].MyHalCali.Pixel2World(pt, out worldPt);
        //                    worldPt.X = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.X + GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].WorldCenterPoint.X - worldPt.X;
        //                    worldPt.Y = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.Y + GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].WorldCenterPoint.Y - worldPt.Y;
        //                }

        //                this.Invoke(new Action(() =>
        //                {
        //                    this.tbRealYPos.Text = worldPt.X.ToString("F5");
        //                    this.tbRealXPos.Text = worldPt.Y.ToString("F5");
        //                }));

        //                message = string.Format("[{0:F0}],{1:F5},{2:F5},{3:F5},{4:F5}", i +1, worldPt.Y, worldPt.X, pt.Y, pt.X);
        //                tw.WriteLine(message);
        //            }
        //        }

        //        this.Invoke(new Action(() =>
        //        {
        //            this.Enabled = true;
        //        }));
        //    });

        //}

        //private void btnDynamicTest_Click(object sender, EventArgs e)
        //{
        //    int count = (int)ndCount.Value;
        //    int module = this.rB_Module1.Checked ? 0 : 1;
        //    bool leftNozzle = this.rbLeftNozzle.Checked;
        //    bool isUpVision = this.radioUpVision.Checked;
        //    int delay = (int)this.ndDelay.Value;
        //    PointF startPos = new PointF();
        //    PointF endPos = new PointF();

        //    float doubleValue = 0;
        //    if (!float.TryParse(this.tbStartXPos.Text, out doubleValue))
        //    {
        //        return;
        //    }

        //    startPos.X = doubleValue;

        //    if (!float.TryParse(this.tbStartYPos.Text, out doubleValue))
        //    {
        //        return;
        //    }

        //    startPos.Y = doubleValue;

        //    if (!float.TryParse(this.tbEndXPos.Text, out doubleValue))
        //    {
        //        return;
        //    }

        //    endPos.X = doubleValue;


        //    if (!float.TryParse(this.tbEndYPos.Text, out doubleValue))
        //    {
        //        return;
        //    }

        //    GeneralMachine.VisionSystem.Instance.Machines[module].ZGoSafeHeight(SpeedMode.Manual_Normal);
        //    while (!GeneralMachine.VisionSystem.Instance.Machines[module].IsSafeHeight)
        //    {
        //        Thread.Sleep(10);
        //    }

        //    endPos.Y = doubleValue;

        //    Task.Factory.StartNew(() =>
        //    {
        //        this.Invoke(new Action(() =>
        //        {
        //            this.Enabled = false;
        //        }));
        //        List<PointF> resultPoint = new List<PointF>();
        //        List<PointF> currentPoint = new List<PointF>();
        //        List<long> resultTime = new List<long>();

        //        Stopwatch sw = new Stopwatch();
        //        for (int i = 0; i < count; i++)
        //        {
        //            sw.Restart();
        //            GeneralMachine.VisionSystem.Instance.Machines[module].XYGoPos(startPos, SpeedMode.Auto_Fast);

        //            while (!GeneralMachine.VisionSystem.Instance.Machines[module].XYReach(startPos))
        //            {
        //                Thread.Sleep(1);
        //            }

        //            long time = sw.ElapsedMilliseconds;
        //            Thread.Sleep(delay);
        //            PointF pt = new PointF();
        //            if (isUpVision)
        //            {
        //                GeneralMachine.VisionSystem.Instance.Machines[module]._session_Up.Snap(frm_Main.frm_Camera.imageSet.Image);

        //            }
        //            else
        //            {
        //                GeneralMachine.VisionSystem.Instance.Machines[module]._session_Down.Snap(frm_Main.frm_Camera.imageSet.Image);
        //            }


        //            if (UpdateImage_Roi())
        //            {
        //                bool rtn = false;
        //                rtn = Vision.FindNccTemplate(imageSet, this.roi, Learn_ModelID1, 1, 0.4, 0, 360, out results);

        //                if (rtn && results != null && results.Count() > 0)
        //                {
        //                    PointF curp = new PointF((float)GeneralMachine.VisionSystem.Instance.Machines[module].X.Pos, (float)GeneralMachine.VisionSystem.Instance.Machines[module].Y.Pos);
        //                    currentPoint.Add(curp);

        //                    pt = results[0].XYCoord;
        //                    resultPoint.Add(pt);
        //                    resultTime.Add(time);
        //                    PointF worldPt = new PointF();
        //                    if (isUpVision)
        //                    {
        //                        GeneralMachine.VisionSystem.Instance.Machines[module].MyHalCali_UP.Pixel2World(pt, out worldPt);
        //                        worldPt.X = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.X + GeneralMachine.VisionSystem.Instance.Machines[module].WorldCenterPoint_Up.X - worldPt.X;
        //                        worldPt.Y = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.Y + GeneralMachine.VisionSystem.Instance.Machines[module].WorldCenterPoint_Up.Y - worldPt.Y;
        //                    }
        //                    else
        //                    {
        //                        GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].MyHalCali.Pixel2World(pt, out worldPt);
        //                        worldPt.X = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.X + GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].WorldCenterPoint.X - worldPt.X;
        //                        worldPt.Y = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.Y + GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].WorldCenterPoint.Y - worldPt.Y;
        //                    }

        //                    this.Invoke(new Action(() =>
        //                    {
        //                        this.tbRealXPos.Text = worldPt.X.ToString("F5");
        //                        this.tbRealYPos.Text = worldPt.Y.ToString("F5");
        //                    }));
        //                }//end if
        //            }//end if

        //            GeneralMachine.VisionSystem.Instance.Machines[module].XYGoPos(endPos, SpeedMode.Auto_Fast);

        //            while (!GeneralMachine.VisionSystem.Instance.Machines[module].XYReach(endPos))
        //            {
        //                Thread.Sleep(1);
        //            }

        //            Thread.Sleep(delay);
        //        }// end for

        //        string up = isUpVision ? "上" : "下";
        //        string modelName = module == 0 ? "前" : "后";
        //        string nozzle = leftNozzle ? "左" : "右";

        //        string path = string.Empty;
        //        if (isUpVision)
        //        {
        //            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + modelName + up + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
        //        }
        //        else
        //        {
        //            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + modelName + up + nozzle + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
        //        }
        //        using (TextWriter tw = new StreamWriter(path, false))
        //        {
        //            string message = string.Empty;
        //            tw.WriteLine("序号,机器X坐标,机器Y坐标,相机像素X坐标,相机像素Y坐标, 花费时间,X 距离, Y距离");
        //            for (int i = 0; i < resultPoint.Count; ++i)
        //            {
        //                PointF pt = resultPoint[i];
        //                PointF worldPt = new PointF();
        //                if (isUpVision)
        //                {
        //                    GeneralMachine.VisionSystem.Instance.Machines[module].MyHalCali_UP.Pixel2World (pt, out worldPt);
        //                    worldPt.X = currentPoint[i].X + GeneralMachine.VisionSystem.Instance.Machines[module].WorldCenterPoint_Up.X - worldPt.X;
        //                    worldPt.Y = currentPoint[i].Y + GeneralMachine.VisionSystem.Instance.Machines[module].WorldCenterPoint_Up.Y - worldPt.Y;
        //                }
        //                else
        //                {
        //                    GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].MyHalCali.Pixel2World(pt, out worldPt);
        //                    worldPt.X = currentPoint[i].X + GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].WorldCenterPoint.X - worldPt.X;
        //                    worldPt.Y = currentPoint[i].Y + GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].WorldCenterPoint.Y - worldPt.Y;
        //                }

        //                this.Invoke(new Action(() =>
        //                {
        //                    this.tbRealXPos.Text = worldPt.X.ToString("F5");
        //                    this.tbRealYPos.Text = worldPt.Y.ToString("F5");
        //                }));

        //                message = string.Format("[{0:F0}],{1:F5},{2:F5},{3:F5},{4:F5},{6:F5},{7:F5},{5},{8:F5},{9:F5}", i + 1, worldPt.Y, worldPt.X, pt.Y, pt.X, resultTime[i]
        //                    , currentPoint[i].X
        //                   , currentPoint[i].Y,
        //                 Math.Abs(endPos.X - startPos.X),
        //                 Math.Abs(endPos.Y - startPos.Y)

        //                    );
        //                tw.WriteLine(message);
        //            }
        //        }

        //        this.Invoke(new Action(() =>
        //        {
        //            this.Enabled = true;
        //        }));
        //    });
        //}

        //private void btnSetStart_Click(object sender, EventArgs e)
        //{
        //    int module = this.rB_Module1.Checked ? 0 : 1;
        //    this.tbStartXPos.Text = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.X.ToString("F4");
        //    this.tbStartYPos.Text = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.Y.ToString("F4");
        //}

        //private void btnEndSet_Click(object sender, EventArgs e)
        //{
        //    int module = this.rB_Module1.Checked ? 0 : 1;
        //    this.tbEndXPos.Text = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.X.ToString("F4");
        //    this.tbEndYPos.Text = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.Y.ToString("F4");
        //}


        //private PointF templatePoint = new PointF();
        //private void btnCheck_Click(object sender, EventArgs e)
        //{
        //    int module = this.rB_Module1.Checked ? 0 : 1;
        //    bool leftNozzle = this.rbLeftNozzle.Checked;
        //    bool isUpVision = this.radioUpVision.Checked;
        //    PointF pt = new PointF();
        //    if (isUpVision)
        //    {
        //        GeneralMachine.VisionSystem.Instance.Machines[module]._session_Up.Snap(frm_Main.frm_Camera.imageSet.Image);
        //    }
        //    else
        //    {
        //        GeneralMachine.VisionSystem.Instance.Machines[module]._session_Down.Snap(frm_Main.frm_Camera.imageSet.Image);
        //    }

        //    if (UpdateImage_Roi())
        //    {
        //        bool rtn = false;
        //        rtn = Vision.FindNccTemplate(imageSet, this.roi, Learn_ModelID1, 1, 0.4, 0, 360, out results);

        //        if (rtn && results != null && results.Count() > 0)
        //        {
        //            pt = results[0].XYCoord;
        //            if (isUpVision)
        //            {
        //                GeneralMachine.VisionSystem.Instance.Machines[module].MyHalCali_UP.Pixel2World(pt, out templatePoint);
        //                templatePoint.X = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.X + GeneralMachine.VisionSystem.Instance.Machines[module].WorldCenterPoint_Up.X - templatePoint.X;
        //                templatePoint.Y = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.Y + GeneralMachine.VisionSystem.Instance.Machines[module].WorldCenterPoint_Up.Y - templatePoint.Y;
        //            }
        //            else
        //            {
        //                GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].MyHalCali.Pixel2World(pt, out templatePoint);
        //                templatePoint.X = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.X + GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].WorldCenterPoint.X - templatePoint.X;
        //                templatePoint.Y = GeneralMachine.VisionSystem.Instance.Machines[module].CurPos.Y + GeneralMachine.VisionSystem.Instance.Machines[module].ZMap[leftNozzle ? 0 : 1].WorldCenterPoint.Y - templatePoint.Y;
        //            }

        //            this.Invoke(new Action(() =>
        //            {
        //                this.tbRealXPos.Text = templatePoint.X.ToString("F5");
        //                this.tbRealYPos.Text = templatePoint.Y.ToString("F5");
        //            }));
        //        }//end if

        //    }
        //}

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    int module = this.rB_Module1.Checked ? 0 : 1;
        //    PointF pt = new PointF();
        //    float outValue = 0;
        //    if(float.TryParse(this.tbStartXPos.Text, out outValue))
        //    {
        //        pt.X = outValue;
        //        if (float.TryParse(this.tbStartYPos.Text, out outValue))
        //        {

        //            pt.Y = outValue;

        //            GeneralMachine.VisionSystem.Instance.Machines[module].ZGoSafeHeight(SpeedMode.Manual_Normal);
        //            while (!GeneralMachine.VisionSystem.Instance.Machines[module].IsSafeHeight)
        //            {
        //                Thread.Sleep(10);
        //            }

        //            GeneralMachine.VisionSystem.Instance.Machines[module].XYGoPos(pt, SpeedMode.Auto_Fast);
        //        }
        //    }

        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    int module = this.rB_Module1.Checked ? 0 : 1;
        //    PointF pt = new PointF();
        //    float outValue = 0;
        //    if (float.TryParse(this.tbEndXPos.Text, out outValue))
        //    {
        //        pt.X = outValue;
        //        if (float.TryParse(this.tbEndYPos.Text, out outValue))
        //        {

        //            pt.Y = outValue;

        //            GeneralMachine.VisionSystem.Instance.Machines[module].ZGoSafeHeight(SpeedMode.Manual_Normal);
        //            while (!GeneralMachine.VisionSystem.Instance.Machines[module].IsSafeHeight)
        //            {
        //                Thread.Sleep(10);
        //            }

        //            GeneralMachine.VisionSystem.Instance.Machines[module].XYGoPos(pt, SpeedMode.Auto_Fast);
        //        }
        //    }
        //}
    }
}
