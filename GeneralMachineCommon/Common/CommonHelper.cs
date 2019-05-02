using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using GetHardDiskInfoDll;
using System.Runtime.Serialization;
using System.Reflection;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace GeneralMachine.Common
{
    public static class CommonHelper
    {
        /// <summary>
        /// 计算软件时间
        /// </summary>
        public static TimeSpan OnlineTime
        {
            get
            {
                return DateTime.Now - Process.GetCurrentProcess().StartTime;
            }
        }

        /// <summary>
        /// 界面不阻塞等待
        /// </summary>
        /// <param name="ms"></param>
        public static void DoEvent(int ms)
        {
            Stopwatch wa = new Stopwatch();
            wa.Restart();
            while (wa.ElapsedMilliseconds < ms)
            {
                Application.DoEvents();
                Thread.Sleep(33);
            }
        }

        /// <summary>
        /// 根据字符串获得坐标值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static PointF GetPoint(string x, string y)
        {
            PointF pointF = new PointF();
            float ptX, ptY;
            if(float.TryParse(x, out ptX))
            {
                pointF.X = ptX;
            }

            if (float.TryParse(y, out ptY))
            {
                pointF.Y = ptY;
            }

            return pointF;
        }

        /// <summary>
        /// 拷贝对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object Copy(object obj)
        {
            object cloneobj = null;
            using (Stream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                cloneobj = formatter.Deserialize(ms);
                ms.Close();
            }
            return cloneobj;
        }

        /// <summary>
        /// 枚举注释的自定义属性类
        /// </summary>
        public class EnumDescriptionAttribute : Attribute
        {
            private string m_strDescription;
            public EnumDescriptionAttribute(string strPrinterName)
            {
                m_strDescription = strPrinterName;
            }

            public string Description
            {
                get { return m_strDescription; }
            }
        }

        public class EnumValueAttribue: Attribute
        {
            private int m_Value;
            public EnumValueAttribue(int value)
            {
                this.m_Value = value;
            }

            public int  Value
            {
                get { return m_Value; }
            }
        }

        public static string GetEnumDescription(Type type, object value)
        {
            FieldInfo fieldInfo = type.GetField(value.ToString());
            object[] attribArray = fieldInfo.GetCustomAttributes(false);
            foreach(var obj in attribArray)
            {
                if(obj.GetType() == typeof(EnumDescriptionAttribute))
                    return ((EnumDescriptionAttribute)obj).Description;
            }

            return string.Empty;
        }

        public static int GetEnumValue(Type type, object value)
        {
            FieldInfo fieldInfo = type.GetField(value.ToString());
            object[] attribArray = fieldInfo.GetCustomAttributes(false);

            foreach (var obj in attribArray)
            {
                if (obj.GetType() == typeof(EnumValueAttribue))
                    return ((EnumValueAttribue)obj).Value;
            }
            return 0;
        }

        /// <summary>
        /// 创建文件夹路径
        /// </summary>
        /// <param name="path"></param>
        public static void CreatePath(string path)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// 删除文件夹及其文件
        /// </summary>
        /// <param name="sourcePath">文件夹路径</param>
        public static void DeleteDirectory(String sourcePath)
        {
            DirectoryInfo info = new DirectoryInfo(sourcePath);
            foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
            {
                if (fsi is System.IO.FileInfo)          //如果是文件，复制文件 
                    File.Delete(fsi.FullName);
                else                                    //如果是文件夹，新建文件夹，递归 
                {
                    DeleteDirectory(fsi.FullName);
                    try
                    {
                        Directory.Delete(fsi.FullName);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            try
            {
                Directory.Delete(sourcePath);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Copy 文件
        /// </summary>
        /// <param name="sourcePath">源文件</param>
        /// <param name="destinationPath"></param>
        public static void CopyDirectory(String sourcePath, String destinationPath)
        {
            DirectoryInfo info = new DirectoryInfo(sourcePath);
            Directory.CreateDirectory(destinationPath);
            foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
            {
                String destName = Path.Combine(destinationPath, fsi.Name);

                if (fsi is System.IO.FileInfo)          //如果是文件，复制文件 
                    File.Copy(fsi.FullName, destName);
                else                                    //如果是文件夹，新建文件夹，递归 
                {
                    Directory.CreateDirectory(destName);
                    CopyDirectory(fsi.FullName, destName);
                }
            }
        }

        /// <summary>
        /// 获取路径下的文件路径
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static List<string> GetFilePath(string directory, string match)
        {
            DirectoryInfo root = new DirectoryInfo(directory);
            FileInfo[] files = root.GetFiles(match);
            List<string> FilePath = new List<string>();
            foreach(FileInfo file in files)
            {
                FilePath.Add(file.FullName);
            }
            return FilePath;
        }

        /// <summary>
        /// 获取路径下的文件名
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static List<string> GetFileName(string directory, string match)
        {
            List<string> FilePath = new List<string>();

            try
            {
                DirectoryInfo root = new DirectoryInfo(directory);
                FileInfo[] files = root.GetFiles(match);
                foreach (FileInfo file in files)
                {
                    FilePath.Add(file.Name.Replace(file.Extension, ""));
                }

            }
            catch { }
            
            return FilePath;
        }


        /// <summary>
        /// 获得该路径下所有目录的名称
        /// </summary>
        /// <param name="parentPath">当前目录</param>
        /// <returns></returns>
        public static List<string> GetDirectoryName(string parentPath)
        {
            List<string> dirList = new List<string>();

            try
            {
                DirectoryInfo root = new DirectoryInfo(parentPath);
                var dir = root.GetDirectories();
                foreach (var DIR in dir)
                    dirList.Add(DIR.Name);
            }
            catch { }
            
            return dirList;
        }

        /// <summary>
        /// 拷贝oldlab的文件到newlab下面
        /// </summary>
        /// <param name="sourcePath">lab文件所在目录(@"~\labs\oldlab")</param>
        /// <param name="savePath">保存的目标目录(@"~\labs\newlab")</param>
        /// <returns>返回:true-拷贝成功;false:拷贝失败</returns>
        public static bool CopyOldLabFilesToNewLab(string sourcePath, string savePath)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            #region //拷贝labs文件夹到savePath下
            try
            {
                string[] labDirs = Directory.GetDirectories(sourcePath);//目录
                string[] labFiles = Directory.GetFiles(sourcePath);//文件
                if (labFiles.Length > 0)
                {
                    for (int i = 0; i < labFiles.Length; i++)
                    {
                        if (Path.GetFileName(labFiles[i]) != ".lab")//排除.lab文件
                        {
                            File.Copy(sourcePath + "\\" + Path.GetFileName(labFiles[i]), savePath + "\\" + Path.GetFileName(labFiles[i]), true);
                        }
                    }
                }
                if (labDirs.Length > 0)
                {
                    for (int j = 0; j < labDirs.Length; j++)
                    {
                        Directory.GetDirectories(sourcePath + "\\" + Path.GetFileName(labDirs[j]));

                        //递归调用
                        CopyOldLabFilesToNewLab(sourcePath + "\\" + Path.GetFileName(labDirs[j]), savePath + "\\" + Path.GetFileName(labDirs[j]));
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            #endregion
            return true;
        }

        /// <summary>
        /// 记录LOG
        /// </summary>
        /// <param name="li"></param>
        /// <returns></returns>
        public static short PutInLog(string Path, Color color, string li, DataGridView dgv)//记录Log 信息
        {
            string str = DateTime.Now.Year.ToString("0000") + "/"
                + DateTime.Now.Month.ToString("00") + "/"
                + DateTime.Now.Day.ToString("00") + " "
                + DateTime.Now.Hour.ToString("00") + ":"
                + DateTime.Now.Minute.ToString("00") + ":"
                + DateTime.Now.Second.ToString("00") + ":"
                + DateTime.Now.Millisecond.ToString("000");
            string logname = DateTime.Now.Year.ToString("0000")
                + DateTime.Now.Month.ToString("00")
                + DateTime.Now.Day.ToString("00");
            try
            {
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                StreamWriter sw = File.AppendText(Path + logname + ".txt");
                sw.Write(str + " " + li + "\r\n");
                sw.Close();

                if(dgv != null)
                {
                    if (dgv.Rows.Count > 2000)
                    {
                        DelDGV(dgv);
                    }
                    dgv.Rows.Insert(0);
                    dgv.Rows[0].Cells[0].Value = str;
                    dgv.Rows[0].Cells[1].Value = li;
                    dgv.Rows[0].DefaultCellStyle.BackColor = color;
                }
            }
            catch
            { }
            return 0;
        }

        /// <summary>
        /// 获取当前时间 例如：20180228_153920_300
        /// </summary>
        /// <returns>获取当前时间 20180228_153920_300</returns>
        public static string GetDataTime()
        {
            string DataTime =
            DateTime.Now.Year.ToString("0000")
                + DateTime.Now.Month.ToString("00")
                + DateTime.Now.Day.ToString("00") + "_"
                + DateTime.Now.Hour.ToString("00")
                + DateTime.Now.Minute.ToString("00")
                + DateTime.Now.Second.ToString("00") + "_"
                + DateTime.Now.Millisecond.ToString();
            return DataTime;
        }

        /// <summary>
        /// 表格添加行号
        /// </summary>
        /// <param name="a"></param>
        public static void AddRowHeader(DataGridView a)
        {
            //添加行号
            for (int i = 0; i < a.Rows.Count - 1; i++)
            {
                int j = i + 1;
                a.Rows[i].HeaderCell.Value = j.ToString();
            }
        }

        /// <summary>
        /// 删除表格数据
        /// </summary>
        /// <param name="a"></param>
        public static void DelDGV(DataGridView a)
        {
            while (a.RowCount > 1)//删除表格数据
            {
                a.Rows.Remove(a.Rows[0]);
            }
        }

        /// <summary>
        /// 初始化表格数据
        /// </summary>
        /// <param name="a">DataGridView</param>
        public static void InitDGV(DataGridView a,float fontSize)
        {
            a.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", (float)fontSize);
            a.RowHeadersDefaultCellStyle.Font = new Font("Tahoma", (float)fontSize);
            a.DefaultCellStyle.Font = new Font("Tahoma", (float)fontSize);
            a.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            a.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            a.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            a.RowHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            a.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            a.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            a.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            a.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllHeaders;
            a.Rows[0].Selected = true;//默认第一行选中
            for (int i = 0; i < a.Columns.Count; i++)
                a.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        /// <summary>
        /// 获取磁盘剩余空间
        /// </summary>
        /// <returns></returns>
        private static short GetDiskInfo(string CDEF)
        {
            long FreeSpace = 0;
            try
            {
                FreeSpace = GetHardDiskInfo.GetHardDiskFreeSpace(CDEF);
            }
            catch
            {
            }
            if (FreeSpace <= 2)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 获得MD5码
        /// </summary>
        /// <param name="code">字符串</param>
        /// <returns>返回MD5码</returns>
        public static string GetMD5Code(string code)
        {
            MD5 md5 = MD5.Create();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(code);
            return Convert.ToBase64String(fromData);
        }

        /// <summary>
        /// 将MenuStrip控件中的信息添加到TreeView控件中
        /// </summary>
        /// <param name="treeV">TreeView控件</param>
        /// <param name="MenuS">MenuStrip控件</param>
        public static void GetMenu(TreeView treeV, MenuStrip MenuS)
        {
            for (int i = 0; i < MenuS.Items.Count; i++) //遍历MenuStrip组件中的一级菜单项
            {
                //将一级菜单项的名称添加到TreeView组件的根节点中，并设置当前节点的子节点newNode1
                TreeNode newNode1 = treeV.Nodes.Add(MenuS.Items[i].Text);
                //将当前菜单项的所有相关信息存入到ToolStripDropDownItem对象中
                ToolStripDropDownItem newmenu = (ToolStripDropDownItem)MenuS.Items[i];
                //判断当前菜单项中是否有二级菜单项
                if (newmenu.HasDropDownItems && newmenu.DropDownItems.Count > 0)
                    for (int j = 0; j < newmenu.DropDownItems.Count; j++) //遍历二级菜单项
                    {
                        //将二级菜单名称添加到TreeView组件的子节点newNode1中，并设置当前节点的子节点newNode2
                        TreeNode newNode2 = newNode1.Nodes.Add(newmenu.DropDownItems[j].Text);
                        //将当前菜单项的所有相关信息存入到ToolStripDropDownItem对象中
                        ToolStripDropDownItem newmenu2 = (ToolStripDropDownItem)newmenu.DropDownItems[j];
                        //判断二级菜单项中是否有三级菜单项
                        if (newmenu2.HasDropDownItems && newmenu2.DropDownItems.Count > 0)
                            for (int p = 0; p < newmenu2.DropDownItems.Count; p++) //遍历三级菜单项
                                                                                   //将三级菜单名称添加到TreeView组件的子节点newNode2中
                                newNode2.Nodes.Add(newmenu2.DropDownItems[p].Text);
                    }
            }
        }

        /// <summary>
        /// 获得权限
        /// </summary>
        /// <param name="tv"></param>
        /// <returns></returns>
        public static int GetPermession(TreeView tv)
        {
            double Sum = 0;
            int index = -1;

            for (int i = 0; i < tv.Nodes.Count; i++)
            {
                index++;
                if (tv.Nodes[i].Checked)
                {
                    Sum += Math.Pow(2, index);
                }
                if (tv.Nodes[i].Nodes.Count > 0)
                {
                    for (int j = 0; j < tv.Nodes[i].Nodes.Count; j++)
                    {
                        index++;
                        if (tv.Nodes[i].Nodes[j].Checked)
                        {
                            Sum += Math.Pow(2, index);
                        }
                        if (tv.Nodes[i].Nodes[j].Nodes.Count > 0)
                        {
                            for (int k = 0; k < tv.Nodes[i].Nodes[j].Nodes.Count; k++)
                            {
                                index++;
                                if (tv.Nodes[i].Nodes[j].Nodes[k].Checked)
                                {
                                    Sum += Math.Pow(2, index);
                                }
                            }
                        }
                    }
                }
            }

            return (int)Sum;
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="Permission"></param>
        public static void SetPermession(TreeView tv, int Permission)
        {
            int index = -1;

            for (int i = 0; i < tv.Nodes.Count; i++)
            {
                index++;
                if ((Permission >> index & 1) == 1)
                {
                    tv.Nodes[i].Checked = true;
                }
                else
                {
                    tv.Nodes[i].Checked = false;
                }
                if (tv.Nodes[i].Nodes.Count > 0)
                {
                    for (int j = 0; j < tv.Nodes[i].Nodes.Count; j++)
                    {
                        index++;
                        if ((Permission >> index & 1) == 1)
                        {
                            tv.Nodes[i].Nodes[j].Checked = true;
                        }
                        else
                        {
                            tv.Nodes[i].Nodes[j].Checked = false;
                        }
                        if (tv.Nodes[i].Nodes[j].Nodes.Count > 0)
                        {
                            for (int k = 0; k < tv.Nodes[i].Nodes[j].Nodes.Count; k++)
                            {
                                index++;
                                if ((Permission >> index & 1) == 1)
                                {
                                    tv.Nodes[i].Nodes[j].Nodes[k].Checked = true;
                                }
                                else
                                {
                                    tv.Nodes[i].Nodes[j].Nodes[k].Checked = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="Permission"></param>
        public static void SetPermession(MenuStrip tv, int Permission)
        {
            int index = -1;

            for (int i = 0; i < tv.Items.Count; i++)
            {
                index++;
                if (((Permission >> index) & 1) == 1)
                {
                    tv.Items[i].Enabled = true;
                }
                else
                {
                    tv.Items[i].Enabled = false;
                }
                ToolStripDropDownItem newmenu = (ToolStripDropDownItem)tv.Items[i];
                if (newmenu.HasDropDownItems && newmenu.DropDownItems.Count > 0)
                {
                    for (int j = 0; j < newmenu.DropDownItems.Count; j++)
                    {
                        index++;
                        if (((Permission >> index) & 1) == 1)
                        {
                            newmenu.DropDownItems[j].Enabled = true;
                        }
                        else
                        {
                            newmenu.DropDownItems[j].Enabled = false;
                        }
                        ToolStripDropDownItem newmenu2 = (ToolStripDropDownItem)newmenu.DropDownItems[j];
                        if (newmenu2.HasDropDownItems && newmenu2.DropDownItems.Count > 0)
                        {
                            for (int k = 0; k < newmenu2.DropDownItems.Count; k++)
                            {
                                index++;
                                if (((Permission >> index) & 1) == 1)
                                {
                                    newmenu2.DropDownItems[k].Enabled = true;
                                }
                                else
                                {
                                    newmenu2.DropDownItems[k].Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static string GetNowTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetToday()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static bool InputCheck()
        {
            string s = Interaction.InputBox("请输入权限密码进行该操作", "提示", "*****", -1, -1);  //-1表示在屏幕的中间
            if (s == "admin")
                return true;
            else
            {
                MessageBox.Show("解锁失败,无权限!!!");
                return false;
            }
        }
    }
}
