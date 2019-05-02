//-----------------------------------------------------------------------
// <copyright file="SerializableDictionary.cs" company="鸿仕达智能科技有限公司">
// Copyright (C)2013-2018 鸿仕达智能科技有限公司 . All Rights Reserved.
// </copyright>
// <author>Sunlike</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace GeneralMachine.Common
{
    using Newtonsoft.Json.Serialization;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// class SerializableDictionary Defination
    /// </summary>
    /// <typeparam name="TKey">Key Type</typeparam>
    /// <typeparam name="TValue">Value Type</typeparam>
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableDictionary{TKey, TValue}"/> class;
        /// </summary>
        public SerializableDictionary()
        {
        }

        /// <summary>
        /// Serialize stream
        /// </summary>
        /// <param name="write">XmlWriter</param>
        public void WriteXml(XmlWriter write)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (KeyValuePair<TKey, TValue> kv in this)
            {
                write.WriteStartElement("SerializableDictionary");
                write.WriteStartElement("key");
                keySerializer.Serialize(write, kv.Key);
                write.WriteEndElement();
                write.WriteStartElement("value");
                valueSerializer.Serialize(write, kv.Value);
                write.WriteEndElement();
                write.WriteEndElement();
            }
        }

        /// <summary>
        ///  DeSerialize stream
        /// </summary>
        /// <param name="reader">XmlReader</param>
        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("SerializableDictionary");
                reader.ReadStartElement("key");
                TKey tk = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                TValue vl = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadEndElement();
                this.Add(tk, vl);
                reader.MoveToContent();
            }

            reader.ReadEndElement();
        }

        /// <summary>
        ///  GetSchema
        /// </summary>
        /// <returns>XmlSchema</returns>
        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }
    }
}