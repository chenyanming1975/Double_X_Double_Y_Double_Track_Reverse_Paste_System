using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;

namespace GeneralMachine.Common
{
    /// <summary>
    /// 可以序列化的颜色
    /// </summary>
    public class SerializableColor
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SerializableColor()
        {
            table["Color"] = Color.Black;
        }

        /// <summary>
        /// 指定颜色的构造函数
        /// </summary>
        /// <param name="color">颜色</param>
        public SerializableColor(Color color)
        {
            table["Color"] = Color.Black;
            this.Color = color;
        }

        /// <summary>
        /// 存储颜色
        /// </summary>
        private Hashtable table = new Hashtable(2);

        [Browsable(true)]
        [XmlIgnore()]
        public Color Color
        {
            get
            {
                return (Color)table["Color"];
            }
            set
            {
                table["Color"] = value;
            }
        }

        [Browsable(false)]
        [XmlElement("Color")]
        public string HostrColor
        {
            get
            {
                return SerializableColor.Serialize(this.Color);
            }

            set
            {
                this.Color = SerializableColor.DeSerialize(value);
            }
        }

        #region Helper

        /// <summary>
        /// 颜色类型
        /// </summary>
        protected enum ColorFormat
        {
            NamedColor,
            ARGBColor,
        }

        /// <summary>
        /// 转化为string 类型
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>格式化的颜色字符串 </returns>
        protected static string Serialize(Color color)
        {
            string result = string.Empty;
            if (color.IsNamedColor)
            {
                result = $"{ColorFormat.NamedColor}:{color.Name}";
            }
            else
            {
                result = $"{ColorFormat.ARGBColor}:{color.A}:{color.B}:{color.G}:{color.R}";
            }

            return result;
        }

        /// <summary>
        /// 从格式化字符串转化为颜色
        /// </summary>
        /// <param name="colorString">格式化的颜色字符串</param>
        /// <returns>颜色</returns>
        public static Color DeSerialize(string colorString)
        {
            Color result = Color.Empty;
            string[] pieces = colorString.Split(new char[] { ':' });
            if (pieces.Count() >= 1)
            {
                ColorFormat format = (ColorFormat)Enum.Parse(typeof(ColorFormat), pieces[0]);
                switch (format)
                {
                    case ColorFormat.NamedColor:
                        if (pieces.Count() >= 2)
                        {
                            result = Color.FromName(pieces[1]);
                        }

                        break;

                    case ColorFormat.ARGBColor:
                        if (pieces.Count() >= 5)
                        {
                            byte a = 0, g = 0, b = 0, r = 0;
                            a = byte.Parse(pieces[1]);
                            b = byte.Parse(pieces[2]);
                            g = byte.Parse(pieces[3]);
                            r = byte.Parse(pieces[4]);
                            result = Color.FromArgb(a, r, g, b);
                        }

                        break;
                }
            }

            return result;
        }

        #endregion Helper
    }
}