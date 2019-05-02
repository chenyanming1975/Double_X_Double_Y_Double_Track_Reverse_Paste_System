using GeneralMachine.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeneralMachine
{
   
    public partial class frm_BugReport : Form
    {
        #region 全局变量
        Exception _bugInfo;
        #endregion
 
        #region 构造函数
        /// <summary>
        /// Bug发送窗口
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        public frm_BugReport(Exception bugInfo)
        {
            InitializeComponent();
            _bugInfo = bugInfo;
            this.txtBugInfo.Text = bugInfo.Message;
            lblErrorCode.Text = Guid.NewGuid().ToString();
        }
 
        /// <summary>
        /// Bug发送窗口
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        /// <param name="errorCode">错误号</param>
        public frm_BugReport(Exception bugInfo, string errorCode)
        {
            InitializeComponent();
            _bugInfo = bugInfo;
            this.txtBugInfo.Text = bugInfo.ToString();
            lblErrorCode.Text = errorCode;
        }
        #endregion
 
        #region 公开静态方法
        /// <summary>
        /// 提示Bug
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        /// <param name="errorCode">错误号</param>
        public static void ShowBug(Exception bugInfo, string errorCode)
        {
            MsgHelper.Instance.WriteLog(MsgLevel.Error, bugInfo.ToString());
            new frm_BugReport(bugInfo, errorCode).ShowDialog();
        }
 
        /// <summary>
        /// 提示Bug
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        public static void ShowBug(Exception bugInfo)
        {
            ShowBug(bugInfo, Guid.NewGuid().ToString());
        }
        #endregion
 
        private void btnDetailsInfo_Click(object sender, EventArgs e)
        {
            //GeneralMachine.VisionFunction.ShowMessage("异常详细信息：" + _bugInfo.Message + "\r\n跟踪：" + _bugInfo.StackTrace);
        }

        private void frm_BugReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Common.CommonHelper.PutInLog();
        }
    }
}
