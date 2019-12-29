namespace d4160.DataPersistence
{
    using System;
    using System.IO;
    using global::OdinSerializer;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public class OdinDataSerializer : DataSerializerBase
    {
        protected DataFormat m_dataFormat;

        public OdinDataSerializer(DataFormat dataFormat = DataFormat.JSON)
        {
            m_dataFormat = dataFormat;

            isBinarySerializer = m_dataFormat == DataFormat.JSON;
            serializerName = "Odin";
        }

        public override T Deserialize<T>(Stream stream, bool encrypted = false)
        {
            T result = default(T);

            StreamReader sr = new StreamReader ( stream );

            var bytes = System.Text.Encoding.UTF8.GetBytes(sr.ReadToEnd());

            result = SerializationUtility.DeserializeValue<T>(bytes, m_dataFormat);

            sr.Dispose ();
            return result;
        }

        public override object Deserialize(string data, Type t, bool encrypted = false)
        {
            // if encrypted decrypt first

            var bytes = System.Text.Encoding.UTF8.GetBytes(data);
            return SerializationUtility.DeserializeValue<object>(bytes, m_dataFormat);
        }

        public override T Deserialize<T>(byte[] data, bool encrypted = false)
        {
            // if encrypted decrypt first

            return SerializationUtility.DeserializeValue<T>(data, m_dataFormat);
        }

        public override T Deserialize<T>(string data, bool encrypted = false)
        {
            // if encrypted decrypt first

            var bytes = System.Text.Encoding.UTF8.GetBytes(data);
            return SerializationUtility.DeserializeValue<T>(bytes, m_dataFormat);
        }

        public override object Serialize(object data)
        {
            var bytes = SerializationUtility.SerializeValue(data, m_dataFormat);

            return bytes;
        }

        public override string Serialize(object data, bool encrypted = false)
        {
            var bytes = Serialize(data);

            // Encrypt if needed before send

            return System.Text.Encoding.UTF8.GetString(bytes as byte[]);
        }

        public override void Serialize<T>(T data, Stream stream, bool encrypted = false)
        {
            StreamWriter sw = new StreamWriter ( stream );
            var json = Serialize(data, encrypted);

            sw.Write(json);
            sw.Dispose ();
        }

        public override string Serialize<T>(T data, bool encrypted = false)
        {
            var bytes = SerializationUtility.SerializeValue(data, m_dataFormat);

            // Encrypt if needed before send

            return System.Text.Encoding.UTF8.GetString(bytes as byte[]);
        }
    }
}