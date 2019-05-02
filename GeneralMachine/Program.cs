using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using GeneralMachine.Test;
using GeneralMachine.SpeedManager;
using GeneralMachine.SystemManager;
using GeneralMachine.IO;
using GeneralMachine.Axis;
using GeneralMachine.Config;
using GeneralMachine.Vision;
using GeneralMachine.Common;

namespace GeneralMachine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //全局异常捕捉
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MsgHelper.Instance.Intialize();

            Process[] tProcess = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (tProcess.Length > 1)
            {
                frm_MessageBox.ShowMessage("已有一个进程在运行!!!");
                Application.Exit();
            }
            else
            {
                Application.ApplicationExit += Application_ApplicationExit;

                // 创建系统所需文件夹
                PathDefine.CreatePath();
                MsgHelper.Instance.WriteLog(MsgLevel.Debug, "软件启动");
                Application.Run(new frm_Main());
            }
                
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            MsgHelper.Instance.WriteLog(MsgLevel.Debug, "软件退出");
        }

        //UI线程异常
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            frm_BugReport.ShowBug(e.Exception);
        }

        //多线程异常
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            frm_BugReport.ShowBug((Exception)e.ExceptionObject);
        }
    }
}
