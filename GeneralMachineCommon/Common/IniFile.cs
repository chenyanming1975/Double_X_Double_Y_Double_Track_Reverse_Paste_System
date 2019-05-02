using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using General;
using GeneralMachine.Definition;

namespace GeneralMachine.Common
{
    public class IniFile
    {
        public string Path;

        public IniFile(string path)
        {
            this.Path = path;
        }
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string
            section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// 写INI文件
        public void IniWriteValue(string section, string key, string iValue)
        {
            WritePrivateProfileString(section, key, iValue, this.Path);
        }
        /// 读取INI文件
        public string IniReadValue(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            return temp.ToString();
        }
        public void IniWriteNumber(string section, string key, int nValue)
        {
            string strtowrt;
            strtowrt = nValue.ToString();
            WritePrivateProfileString(section, key, strtowrt, this.Path);
        }
        public void IniWriteNumber(string section, string key, double dValue)
        {
            string strtowrt;
            strtowrt = dValue.ToString();
            WritePrivateProfileString(section, key, strtowrt, this.Path);
        }
        public double IniReadNum(string section, string key)
        {
            double rtn = 0;
            StringBuilder temp = new StringBuilder(255);
            string strres;
            int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            strres = temp.ToString();
            if (strres == "")
            { strres = "0"; }

            try
            {
                rtn = Convert.ToDouble(strres);
            }
            catch
            {
                //Function.function.PutInLog("读取" + Path + "中参数" + section + ":" + key + "出错", false);
            }
            return rtn;
        }
        public int IniReadInt(string section, string key)
        {
            int rtn = 0;
            StringBuilder temp = new StringBuilder(255);
            string strres;
            int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            strres = temp.ToString();
            if (strres == "")
            {
                strres = "0";
            }
            try
            {
                rtn = Convert.ToInt32(strres);
            }
            catch
            {
                //Function.function.PutInLog("读取" + Path + "中参数" + section + ":" + key + "出错", false);
            }
            return rtn;
        }
        public void IniWriteInt(string section, string key, int iValue)
        {
            string strtowrt = iValue.ToString();
            WritePrivateProfileString(section, key, strtowrt, this.Path);
        }
        public void IniWriteShort(string section, string key, short iValue)
        {
            string strtowrt = iValue.ToString();
            WritePrivateProfileString(section, key, strtowrt, this.Path);
        }
        public short IniReadShort(string section, string key)
        {
            short rtn = 0;
            StringBuilder temp = new StringBuilder(255);
            string strres;
            GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            strres = temp.ToString();
            try
            {
                rtn = short.Parse(strres);
            }
            catch
            {
                //Function.function.PutInLog("读取" + Path + "中参数" + section + ":" + key + "出错", false);
            }
            return rtn;
        }
        public void IniWriteBool(string section, string key, bool bValue)
        {
            string strtowrt;
            if(bValue)
            {
                strtowrt = "1";
            }
            else
            {
                strtowrt = "0";
            }
            WritePrivateProfileString(section, key, strtowrt, this.Path);
        }
        public bool IniReadBool(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            string strres;
            int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            strres = temp.ToString();
            if (strres == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void IniWritePoint(string section, string key, PointF point)
        {
            string strX = point.X.ToString("F3");
            string strY = point.Y.ToString("F3");
            WritePrivateProfileString(section, key, strX + "," + strY, this.Path);
        }
        public PointF IniReadPoint(string section, string key)
        {
            PointF POINT = new PointF(0, 0);
            StringBuilder temp = new StringBuilder(255);
            string strres,strX,strY;
            GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            strres = temp.ToString();
            try
            {
                strX = strres.Split(new char[] { ',' })[0];
                strY = strres.Split(new char[] { ',' })[1];
                POINT.X = float.Parse(strX);
                POINT.Y = float.Parse(strY);
            }
            catch
            {
                return POINT;
            }
            return POINT;
        }

        public void IniWriteThrowPoint(string section, string key, ThrowPoint throwPoint)
        {
            string strX = throwPoint.X.ToString("F3");
            string strY = throwPoint.Y.ToString("F3");
            string strZ = throwPoint.Z.ToString("F3");
            string strD = throwPoint.Delay_Throw.ToString("F3");
            WritePrivateProfileString(section, key, strX + "," + strY + "," + strZ + "," + strD, this.Path);
        }

        public ThrowPoint IniReadThrowPoint(string section, string key)
        {
            ThrowPoint throwPoint = new ThrowPoint();
            StringBuilder temp = new StringBuilder(255);
            string strres, strX, strY, strZ, strD;
            GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            strres = temp.ToString();
            try
            {
                strX = strres.Split(new char[] { ',' })[0];
                strY = strres.Split(new char[] { ',' })[1];
                strZ = strres.Split(new char[] { ',' })[2];
                strD = strres.Split(new char[] { ',' })[3];
                throwPoint.X = double.Parse(strX);
                throwPoint.Y = double.Parse(strY);
                throwPoint.Z = double.Parse(strZ);
                throwPoint.Delay_Throw = int.Parse(strD);
            }
            catch
            {
                //Function.function.PutInLog("读取" + Path + "中参数" + section + ":" + key + "出错", false);
                return throwPoint;
            }
            return throwPoint;
        }






        public void IniWriteCamResolution(string section, string key, Vision.RectangleRegion camResolution)
        {
            string strTopLeftX = camResolution.TopLeftX.ToString();
            string strTopLeftY = camResolution.TopLeftY.ToString();
            string strWidth = camResolution.Width.ToString();
            string strHeight = camResolution.Height.ToString();
            WritePrivateProfileString(section, key, strTopLeftX + "," + strTopLeftY + "," + strWidth + "," + strHeight, this.Path);
        }
        public Vision.RectangleRegion IniReadCamResolution(string section, string key)
        {
            Vision.RectangleRegion camResolution = new Vision.RectangleRegion(0, 0, 1, 1);
            StringBuilder temp = new StringBuilder(255);
            string strres, strTopLeftX, strTopLeftY, strWidth, strHeight;
            GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            strres = temp.ToString();
            try
            {
                strTopLeftX = strres.Split(new char[] { ',' })[0];
                strTopLeftY = strres.Split(new char[] { ',' })[1];
                strWidth = strres.Split(new char[] { ',' })[2];
                strHeight = strres.Split(new char[] { ',' })[3];
                camResolution.TopLeftX = short.Parse(strTopLeftX);
                camResolution.TopLeftY = short.Parse(strTopLeftY);
                camResolution.Width = short.Parse(strWidth);
                camResolution.Height = short.Parse(strHeight);
            }
            catch
            {
                //Function.function.PutInLog("读取" + Path + "中参数" + section + ":" + key + "出错", false);
                return camResolution;
            }
            return camResolution;
        }
    }
}
