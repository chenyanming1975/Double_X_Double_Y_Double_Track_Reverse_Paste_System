//-----------------------------------------------------------------------
// <copyright file="SerializableHelper.cs" company="鸿仕达智能科技有限公司">
// Copyright (C)2013-2018 鸿仕达智能科技有限公司 . All Rights Reserved.
// </copyright>
// <author>Sunlike</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace GeneralMachine.Common
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// class SerializableHelper{T} Defination
    /// </summary>
    /// <typeparam name="T"> Type to be serialized </typeparam>
    public class SerializableHelper<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableHelper{T}"/> class.
        /// </summary>
        /// <param name="instance">实例</param>
        public SerializableHelper(T instance)
        {
            this.instance = instance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableHelper{T}"/> class.
        /// </summary>
        public SerializableHelper()
        {
        }

        /// <summary>
        /// 实例
        /// </summary>
        private T instance;

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="path">存储路径</param>
        /// <returns>操作是否成功</returns>
        public bool XMLSerialize(string path)
        {
            bool result = false;
            try
            {
                PathHelper.CreatePath(path);
                using (TextWriter fileStream = new StreamWriter(path, false))
                {
                    fileStream.Flush();
                    XmlSerializer xmlSerialize = new XmlSerializer(typeof(T));
                    xmlSerialize.Serialize(fileStream, this.instance);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MsgHelper.Instance.WriteLog(MsgLevel.Error, $"XMLSerialize:Exception-{ex.ToString()}");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="path">存储路径</param>
        /// <returns>对象</returns>
        public T DeXMLSerialize(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (TextReader fileStream = new StreamReader(path))
                    {
                        XmlSerializer xmlSerialize = new XmlSerializer(typeof(T));
                        this.instance = (T)xmlSerialize.Deserialize(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgHelper.Instance.WriteLog(MsgLevel.Error, $"DeXMLSerialize:Exception-{ex.ToString()}");
            }

            return this.instance;
        }

        public string StringSerialize()
        {
            string reuslt = string.Empty;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlSerializer xmlSerialize = new XmlSerializer(typeof(T));
                    xmlSerialize.Serialize(ms, this.instance);
                    byte[] content = ms.ToArray();
                    reuslt = Convert.ToBase64String(content);
                }
            }
            catch (Exception ex)
            {
                MsgHelper.Instance.WriteLog(MsgLevel.Error, $"StringSerialize:Exception-{ex.ToString()}");
                reuslt = string.Empty;
            }

            return reuslt;
        }

        public T DeStringSerialize(string content)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] datas = Convert.FromBase64String(content);
                    ms.Write(datas, 0, datas.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    XmlSerializer xmlSerialize = new XmlSerializer(typeof(T));
                    this.instance = (T)xmlSerialize.Deserialize(ms);
                }
            }
            catch (Exception ex)
            {
                MsgHelper.Instance.WriteLog(MsgLevel.Error, $"DeStringSerialize:Exception-{ex.ToString()}");
            }

            return this.instance;
        }

        /// <summary>
        /// Json序列化
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool JsonSerialize(string path)
        {
            bool result = false;
            try
            {
                PathHelper.CreatePath(path);
                using (TextWriter fileStream = new StreamWriter(path, false))
                {
                    fileStream.Flush();
                    JsonSerializer jsonSerialize = new JsonSerializer();
                    //序列化添加缩进
                    jsonSerialize.Formatting = Formatting.Indented;
                    //序列化时成接口或继承类
                    jsonSerialize.TypeNameHandling = TypeNameHandling.All;
                    jsonSerialize.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                    jsonSerialize.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    //空值处理
                    jsonSerialize.NullValueHandling = NullValueHandling.Ignore;

                    jsonSerialize.Serialize(fileStream, this.instance);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MsgHelper.Instance.WriteLog(MsgLevel.Error, $"JsonSerialize:Exception-{ex.ToString()}");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public T DeJsonSerialize(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (TextReader reader = new StreamReader(path))
                    {
                        JsonSerializer jsonSerialize = new JsonSerializer
                        {
                            //序列化时成接口或继承类
                            TypeNameHandling = TypeNameHandling.All,
                            DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat,
                            DateFormatString = "yyyy-MM-dd HH:mm:ss",
                            //空值处理
                            NullValueHandling = NullValueHandling.Ignore
                        };
                        this.instance = (T)jsonSerialize.Deserialize(reader, typeof(T));
                    }
                }
            }
            catch (Exception ex)
            {
                MsgHelper.Instance.WriteLog(MsgLevel.Error, $"DeJsonSerialize:Exception-{ex.ToString()}");
            }

            return this.instance;
        }
    }
}