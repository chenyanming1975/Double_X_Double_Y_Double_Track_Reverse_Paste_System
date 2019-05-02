using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralMachine.Common;
using GeneralMachine.Definition;
using GeneralMachine.Light;
using System.Drawing;
using NationalInstruments.Vision.Acquisition.Imaqdx;
using GeneralMachine.Vision;
using HalconDotNet;
using System.IO;
using System.Diagnostics;
using GeneralMachine.Config;
using GeneralMachine.Motion;

namespace GeneralMachine
{
    public class Variable
    {
        //*******************************************************[路径]*******************************************************
        /// <summary>
        /// 当前用户 职位(角色)
        /// </summary>
        public static string sPermission_CurerentRole = "Hostar";
        /// <summary>
        /// 当前用户名称
        /// </summary>
        public static string sPermission_CurerentUserName = "管理员";
        /// <summary>
        /// 当前用户权限
        /// </summary>
        public static int iPermission_CurerentUser = 0;
    }
}
