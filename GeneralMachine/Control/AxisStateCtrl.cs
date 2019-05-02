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
using GeneralMachine.Flow;
using GeneralMachine.Motion;

namespace GeneralMachine
{
    /// <summary>
    /// 轴状态枚举
    /// </summary>
    public enum AxisState
    {
        实际坐标 = 0,
        规划坐标,
        脉冲差值,
        报警,
        极限,
        轴状态,
    }

    public partial class AxisStateCtrl : UserControl
    {
      
        public AxisStateCtrl()
        {
            InitializeComponent();

            foreach(AxisState axisState in Enum.GetValues(typeof(AxisState)))
            {
                this.dGVAxisState.Rows.Add();
                this.dGVAxisState.Rows[(int)axisState].Cells[0].Value = axisState.ToString();
            }

            Timer.Tick += UpdateInfo;
            Timer.Interval = 100;
        }

        /// <summary>
        /// 所属模组
        /// </summary>
        public Module Module { get; set; } = Module.Front;

        public Timer Timer = new Timer();

        private bool IsExpend = false;

        /// <summary>
        /// 展开
        /// </summary>
        private void Expend()
        {
            if(IsExpend)
            {
                this.Height -= 100;
            }
            else
            {
                this.Height += 100;
            }

            IsExpend = !IsExpend;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Expend();
        }

        private void UpdateInfo(object sender, EventArgs args)
        {
            if (SystemEntiy.Instance.Entiys.Count < 2)
                return;

            #region 更新各个轴的状态
            Axis_RunParam[] updateAxis = new Axis_RunParam[11];
            //var pos = SystemEntiy.Instance[this.Module].XYPos;

            updateAxis[0] = SystemEntiy.Instance[this.Module].MachineAxis.X;
            updateAxis[1] = SystemEntiy.Instance[this.Module].MachineAxis.Y;
            updateAxis[2] = SystemEntiy.Instance[this.Module].MachineAxis.Trun;
            updateAxis[3] = SystemEntiy.Instance[this.Module].MachineAxis.Z[0];
            updateAxis[4] = SystemEntiy.Instance[this.Module].MachineAxis.Z[1];
            updateAxis[5] = SystemEntiy.Instance[this.Module].MachineAxis.Z[2];
            updateAxis[6] = SystemEntiy.Instance[this.Module].MachineAxis.Z[3];
            updateAxis[7] = SystemEntiy.Instance[this.Module].MachineAxis.R[0];
            updateAxis[8] = SystemEntiy.Instance[this.Module].MachineAxis.R[1];
            updateAxis[9] = SystemEntiy.Instance[this.Module].MachineAxis.R[2];
            updateAxis[10] = SystemEntiy.Instance[this.Module].MachineAxis.R[3];

            for(int i = 0; i < updateAxis.Length; ++i)
            {
                updateAxis[i].GetAxisPos();
                updateAxis[i].GetAxisSts();

                this.dGVAxisState.Rows[(int)AxisState.实际坐标].Cells[i + 1].Value = (updateAxis[i].AxisEncPos / updateAxis[i].AxisRatio).ToString("f3");
                this.dGVAxisState.Rows[(int)AxisState.规划坐标].Cells[i + 1].Value = (updateAxis[i].AxisPrfPos / updateAxis[i].AxisRatio).ToString("f3");
                this.dGVAxisState.Rows[(int)AxisState.脉冲差值].Cells[i + 1].Value = (updateAxis[i].AxisEncPos - updateAxis[i].AxisPrfPos).ToString("f3");

                if (updateAxis[i].bAxisServoWarning)
                    this.dGVAxisState.Rows[(int)AxisState.报警].Cells[i + 1].Value = "报警";
                else
                    this.dGVAxisState.Rows[(int)AxisState.报警].Cells[i + 1].Value = "正常";

                if(updateAxis[i].bPosLimit)
                {
                    this.dGVAxisState.Rows[(int)AxisState.极限].Cells[i + 1].Value = "正极限";
                }
                else if(updateAxis[i].bNegLimit)
                {
                    this.dGVAxisState.Rows[(int)AxisState.极限].Cells[i + 1].Value = "负极限";
                }
                else
                {
                    this.dGVAxisState.Rows[(int)AxisState.极限].Cells[i + 1].Value = "正常";
                }

                if (updateAxis[i].bAxisIsRunning)
                {
                    this.dGVAxisState.Rows[(int)AxisState.轴状态].Cells[i + 1].Value = "运动中";
                }
                else if(updateAxis[i].bAxisIsHoming)
                {
                    this.dGVAxisState.Rows[(int)AxisState.轴状态].Cells[i + 1].Value = "回原点中";
                }
                else if(updateAxis[i].bAxisReady)
                {
                    this.dGVAxisState.Rows[(int)AxisState.轴状态].Cells[i + 1].Value = "停止";
                }
            }
            #endregion
        }

        public void StartMointor()
        {
            Timer.Start();
        }
    }
}
