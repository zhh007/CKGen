using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CKGen.Base
{
    public class SettingStore
    {
        private static SettingStore instance = new SettingStore();

        public static SettingStore Instance
        {
            get
            {
                return instance;
            }
        }

        public void Save<T>(T setting, string filename)
        {
            string xmlStr = Serialize<T>(setting);
            string filePath = Path.Combine(Environment.CurrentDirectory, "Settings\\" + filename);
            using (var sw = new StreamWriter(File.Open(filePath, FileMode.OpenOrCreate), System.Text.Encoding.UTF8))
            {
                sw.Write(xmlStr);
            }
        }

        public T Get<T>(string filename)
        {
            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, "Settings\\" + filename);
                if (System.IO.File.Exists(filePath))
                {
                    string xmlStr = System.IO.File.ReadAllText(filePath);
                    return Deserialize<T>(xmlStr);
                }
            }
            catch (Exception)
            {
                
            }
            return default(T);
        }

        private T Deserialize<T>(string xmlStr)
        {
            byte[] buff = Encoding.Unicode.GetBytes(xmlStr);
            MemoryStream ms = new MemoryStream(buff);
            XmlAttributeOverrides overrides = new XmlAttributeOverrides();
            XmlSerializer x = new XmlSerializer(typeof(T), overrides);

            return (T)x.Deserialize(ms);
        }

        private string Serialize<T>(T value)
        {
            XmlAttributeOverrides overrides = new XmlAttributeOverrides();
            XmlSerializer s = new XmlSerializer(typeof(T), overrides);
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            s.Serialize(writer, value);
            writer.Close();

            return sb.ToString();
        }
    }
}
