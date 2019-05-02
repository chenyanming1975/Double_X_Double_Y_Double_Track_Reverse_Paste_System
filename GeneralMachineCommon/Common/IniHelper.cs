using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace GeneralMachine.Common
{
    public abstract class IniHelper
    {
        public virtual string GetFileName()
        {
            return string.Empty;
        }

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateProfileString(string lpSectionName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileInt")]
        private static extern int GetPrivateProfileInt(string lpSectionName, string lpKeyName, int lpDefault, string lpFileName);

        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern int WritePrivateProfileString(string lpSectionName, string lpKeyName, string lpString, string lpFileName);

        public int GetPrivateProfileString(string lpSectionName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString)
        {
            return GetPrivateProfileString(lpSectionName, lpKeyName, lpDefault, lpReturnedString, 255, GetFileName());
        }

        public int GetPrivateProfileInt(string lpSectionName, string lpKeyName, int lpDefault)
        {
            return GetPrivateProfileInt(lpSectionName, lpKeyName, lpDefault, GetFileName());
        }

        public int WritePrivateProfileString(string lpSectionName, string lpKeyName, string lpString)
        {
            return WritePrivateProfileString(lpSectionName, lpKeyName, lpString, GetFileName());
        }

        [NonSerialized]
        [System.Xml.Serialization.XmlIgnore]
        public Action<PropertyInfo, string> SetUserDefineValueAction = null;

        public void Read()
        {
            if (File.Exists(GetFileName()))
            {
                foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
                {
                    if (propertyInfo.CanWrite)
                    {
                        StringBuilder strValue1 = new StringBuilder(1024);
                        string strValue;
                        string sectionName = "System";
                        INIAttribute atttribute = propertyInfo.GetCustomAttribute(typeof(INIAttribute)) as INIAttribute;
                        if (atttribute != null && !string.IsNullOrEmpty(atttribute.SectionName))
                        {
                            sectionName = atttribute.SectionName;
                        }

                        GetPrivateProfileString(sectionName, propertyInfo.Name, "0", strValue1);
                        strValue = strValue1.ToString();
                        object objN = propertyInfo.GetValue(this, null);
                        if (objN is int)
                        {
                            propertyInfo.SetValue(this, Convert.ToInt32(strValue), null);
                        }
                        else if (objN is double)
                        {
                            propertyInfo.SetValue(this, Convert.ToDouble(strValue), null);
                        }
                        else if (objN is string)
                        {
                            propertyInfo.SetValue(this, strValue, null);
                        }
                        else if (objN is bool)
                        {
                            propertyInfo.SetValue(this, bool.TrueString == strValue, null);
                        }
                        else
                        {
                            this.SetUserDefineValueAction?.Invoke(propertyInfo, strValue);
                        }
                    }
                }
            }
        }

        public void Save()
        {
            if (!File.Exists(GetFileName()))
            {
                PathHelper.CreatePath(GetFileName());
                File.CreateText(GetFileName());
            }

            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    string sectionName = "System";
                    INIAttribute atttribute = propertyInfo.GetCustomAttribute(typeof(INIAttribute)) as INIAttribute;
                    if (atttribute != null && !string.IsNullOrEmpty(atttribute.SectionName))
                    {
                        sectionName = atttribute.SectionName;
                    }

                    this.WritePrivateProfileString(sectionName, propertyInfo.Name, propertyInfo.GetValue(this, null).ToString());
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class INIAttribute : Attribute
    {
        public INIAttribute(string name)
        {
            this.SectionName = name;
        }

        public string SectionName { get; set; }
    }
}