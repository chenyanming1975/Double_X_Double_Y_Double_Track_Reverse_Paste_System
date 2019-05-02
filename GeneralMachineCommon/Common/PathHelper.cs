//-----------------------------------------------------------------------
// <copyright file="PathHelper.cs" company="鸿仕达智能科技有限公司">
// Copyright (C)2013-2018 鸿仕达智能科技有限公司 . All Rights Reserved.
// </copyright>
// <author>Sunlike</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace GeneralMachine.Common
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// class PathHelper Defination
    /// </summary>
    public class PathHelper
    {
        /// <summary>
        /// Create Directory
        /// </summary>
        /// <param name="path">File Path or Directory </param>
        public static void CreatePath(string path, bool hiden = false)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string pathName = Path.GetDirectoryName(path);
                if (!Directory.Exists(pathName))
                {
                    Directory.CreateDirectory(pathName);
                }

                if (hiden)
                {
                    DirectoryInfo info = new DirectoryInfo(pathName);
                    if (info != null)
                    {
                        info.Attributes |= FileAttributes.Hidden;
                    }
                }
            }
        }

        /// <summary>
        /// 加载 目录信息
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>目录集合</returns>
        public static List<string> LoadDirectoryInfo(string path, bool fullPath = true)
        {
            List<string> result = new List<string>();
            if (Directory.Exists(path))
            {
                string[] datas = Directory.GetDirectories(path);
                if (datas != null)
                {
                    if (fullPath)
                    {
                        result.AddRange(datas);
                    }
                    else
                    {
                        foreach (var item in datas)
                        {
                            result.Add(Path.GetFileName(item));
                        }
                    }
                }
            }

            return result;
        }
    }
}