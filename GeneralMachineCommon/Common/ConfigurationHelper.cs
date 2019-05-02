//-----------------------------------------------------------------------
// <copyright file="ConfigurationHelper.cs" company="鸿仕达智能科技有限公司">
// Copyright (C)2013-2018 鸿仕达智能科技有限公司 . All Rights Reserved.
// </copyright>
// <author>Sunlike</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace GeneralMachine.Common
{
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// class ConfigurationHelper Defination
    /// </summary>
    public class ConfigurationHelper
    {
        /// <summary>
        /// 保存appSetting
        /// </summary>
        /// <param name="key">appSetting的KEY值</param>
        /// <param name="value">appSetting的Value值</param>
        public static void SetAppSetting(string key, string value)
        {
            // 创建配置文件对象
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[key] != null)
            {
                // 修改
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                // 添加
                AppSettingsSection ass = (AppSettingsSection)config.GetSection("appSettings");
                ass.Settings.Add(key, value);
            }

            config.AppSettings.SectionInformation.ForceSave = true;

            // 保存修改
            config.Save(ConfigurationSaveMode.Minimal);

            // 强制重新载入配置文件的连接配置节
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 获得Appsetting的值
        /// </summary>
        /// <param name="key">appSetting的KEY值</param>
        /// <returns>appSetting的Value值</returns>
        public static string GetAppSetting(string key)
        {
            string result = string.Empty;
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                result = ConfigurationManager.AppSettings[key];
            }

            return result;
        }
    }
}