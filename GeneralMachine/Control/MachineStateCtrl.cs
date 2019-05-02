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
using GeneralMachine.Track;
using GeneralMachine.Report;

namespace GeneralMachine.ProgramUI
{
    public partial class MachineStateCtrl : UserControl
    {
        public enum MachineStateItem
        {
            当前状态 = 0,
            生产程式,
            大板码,
            当前步序,
            步序耗时,
            生产板数,
            生产片数,
            进板时间,
            贴附时间,
            出板时间,
            生产时间,
            待产时间,
            报警时间,
            抛料率,
            报警率,
        }

        public MachineStateCtrl()
        {
            InitializeComponent();
            foreach (MachineStateItem item in Enum.GetValues(typeof(MachineStateItem)))
            {
                this.dGVMachineState.Rows.Add();
                this.dGVMachineState.Rows[(int)item].Cells[0].Value = item.ToString();
            }
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
        }

        private int saveCount = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            saveCount++;
            if (saveCount > 10)
            {
                ReportHelper.Instance.ChangeShfit(DateTime.Now);
                saveCount = 0;
            }
            try
            {
                for (Module module = Module.Front; module <= Module.After; ++module)
                {
                    this.dGVMachineState.Rows[(int)MachineStateItem.出板时间].Cells[(int)module + 1].Value = (TrackManager.Instance.TrackMachine[(Config.Track)module].OutputTime.ElapsedMilliseconds / 1000.0).ToString("f2");
                    this.dGVMachineState.Rows[(int)MachineStateItem.进板时间].Cells[(int)module + 1].Value = (TrackManager.Instance.TrackMachine[(Config.Track)module].InputTime.ElapsedMilliseconds / 1000.0).ToString("f2");
                    this.dGVMachineState.Rows[(int)MachineStateItem.贴附时间].Cells[(int)module + 1].Value = (SystemEntiy.Instance.FlowMachine[module].CT.ElapsedMilliseconds / 1000.0).ToString("f2");

                    this.dGVMachineState.Rows[(int)MachineStateItem.当前状态].Cells[(int)module + 1].Value = SystemEntiy.Instance.FlowMachine[module].Pasued ? "暂停":"运行";
                    if(SystemEntiy.Instance.FlowMachine[module].CurStep != null)
                        this.dGVMachineState.Rows[(int)MachineStateItem.当前步序].Cells[(int)module + 1].Value = SystemEntiy.Instance.FlowMachine[module].CurStep.FlowName;
                    else
                        this.dGVMachineState.Rows[(int)MachineStateItem.当前步序].Cells[(int)module + 1].Value = "无";

                    if(SystemEntiy.Instance.FlowMachine[module].RunData.RUN_PanelCode != null)
                        this.dGVMachineState.Rows[(int)MachineStateItem.大板码].Cells[(int)module + 1].Value = SystemEntiy.Instance.FlowMachine[module].RunData.RUN_PanelCode;

                    if (SystemEntiy.Instance.FlowMachine[module].Program!= null)
                        this.dGVMachineState.Rows[(int)MachineStateItem.生产程式].Cells[(int)module + 1].Value = SystemEntiy.Instance.FlowMachine[module].Program.PasteName;
                    else
                        this.dGVMachineState.Rows[(int)MachineStateItem.生产程式].Cells[(int)module + 1].Value = "未导入程序";

                    this.dGVMachineState.Rows[(int)MachineStateItem.生产板数].Cells[(int)module + 1].Value = ReportHelper.Instance[module].PCBCount;
                    this.dGVMachineState.Rows[(int)MachineStateItem.生产片数].Cells[(int)module + 1].Value = ReportHelper.Instance[module].PCSCount;

                 
                    if(ReportHelper.Instance[(Module)module].CurType != StatisticType.Producte)
                        this.dGVMachineState.Rows[(int)MachineStateItem.生产时间].Cells[(int)module + 1].Value = ReportHelper.Instance[module].ProductTime.ToString();
                    else
                        this.dGVMachineState.Rows[(int)MachineStateItem.生产时间].Cells[(int)module + 1].Value =
                            ((DateTime.Now - ReportHelper.Instance[module].StartTime) + ReportHelper.Instance[module].ProductTime).ToString();

                    if (ReportHelper.Instance[(Module)module].CurType != StatisticType.DT)
                        this.dGVMachineState.Rows[(int)MachineStateItem.报警时间].Cells[(int)module + 1].Value = ReportHelper.Instance[module].DTTime.ToString();
                    else
                        this.dGVMachineState.Rows[(int)MachineStateItem.报警时间].Cells[(int)module + 1].Value =
                           ((DateTime.Now - ReportHelper.Instance[module].StartTime) + ReportHelper.Instance[module].DTTime).ToString();

                    if (ReportHelper.Instance[(Module)module].CurType != StatisticType.WaitProducte)
                        this.dGVMachineState.Rows[(int)MachineStateItem.待产时间].Cells[(int)module + 1].Value = ReportHelper.Instance[module].WaitProductTime.ToString();
                    else
                        this.dGVMachineState.Rows[(int)MachineStateItem.待产时间].Cells[(int)module + 1].Value =
                           ((DateTime.Now - ReportHelper.Instance[module].StartTime) + ReportHelper.Instance[module].WaitProductTime).ToString();

                    ulong dropCount = 0;
                    for(Nozzle nz = Nozzle.Nz1; nz <= Nozzle.Nz4;++nz)
                    {
                        dropCount += ReportHelper.Instance[module][nz];
                    }

                    ulong sum = dropCount + ReportHelper.Instance[module].PCSCount;
                    if (sum != 0)
                        this.dGVMachineState.Rows[(int)MachineStateItem.抛料率].Cells[(int)module + 1].Value = $"{(dropCount * 100.0 / (double)sum):N2} %";
                    else
                        this.dGVMachineState.Rows[(int)MachineStateItem.抛料率].Cells[(int)module + 1].Value = $"0 %";

                    sum = ReportHelper.Instance[module].AlarmCount + ReportHelper.Instance[module].PCSCount;

                    if (ReportHelper.Instance[module].PCSCount != 0)
                        this.dGVMachineState.Rows[(int)MachineStateItem.报警率].Cells[(int)module + 1].Value = $"{ReportHelper.Instance[module].AlarmCount * 100.0 / ReportHelper.Instance[module].PCSCount} %";
                    else if (ReportHelper.Instance[module].AlarmCount > ReportHelper.Instance[module].PCSCount)
                    {
                        this.dGVMachineState.Rows[(int)MachineStateItem.报警率].Cells[(int)module + 1].Value = "100 %";
                    }
                    else
                    {
                        this.dGVMachineState.Rows[(int)MachineStateItem.报警率].Cells[(int)module + 1].Value = "0 %";
                    }
                }
            }
            catch { }
        }

        public Timer timer = new Timer();

        private void MachineStateCtrl_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}
