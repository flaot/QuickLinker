using QFramework;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System;

namespace QuickLinker.Utils
{
    public interface IXmlSerializeUtility : IUtility
    {
        /// <summary> 反序列化Xml文件为类 </summary>
        T XmlDeserializeByFile<T>(string path) where T : class;
        /// <summary> 反序列化Xml文件为类(内存数据) </summary>
        T XmlDeserializeByMem<T>(byte[] bytes) where T : class;
        /// <summary> Xml反序列化为类(文本数据) </summary>
        T XmlDeserializeByStr<T>(string str);
        /// <summary> obj序列化成xml文件 </summary>
        bool XmlSerializeToFile(string path, object obj);
        /// <summary> obj序列化成xml文本 </summary>
        string XmlSerializeToStr(object obj);
    }

    internal class XmlSerializeUtility : IXmlSerializeUtility
    {
        public T XmlDeserializeByFile<T>(string path) where T : class
        {
            T result = null;
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    result = (T)xmlSerializer.Deserialize(stream);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("此xml文件无法转成类: " + path + "," + ex);
            }
            return result;
        }
        public T XmlDeserializeByMem<T>(byte[] bytes) where T : class
        {
            T result = null;
            try
            {
                using (Stream stream = new MemoryStream(bytes))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    result = (T)xmlSerializer.Deserialize(stream);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("此xml字节数组无法转成二进制: " + bytes.ToString() + "," + ex);
            }
            return result;
        }
        public T XmlDeserializeByStr<T>(string str)
        {
            T result = default(T);
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(str);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                        result = (T)xmlSerializer.Deserialize(streamReader);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("此Json字节数组无法转成类: " + str.ToString() + "," + ex);
            }
            return result;
        }
        public bool XmlSerializeToFile(string path, object obj)
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter textWriter = new StreamWriter(stream, Encoding.UTF8))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                        xmlSerializer.Serialize(textWriter, obj);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("此类无法转换成xml " + obj.GetType()?.ToString() + "," + ex);
            }
            return false;
        }
        public string XmlSerializeToStr(object obj)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    using (StreamWriter textWriter = new StreamWriter(stream, Encoding.UTF8))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                        xmlSerializer.Serialize(textWriter, obj);
                    }
                    return stream.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("此类无法转换成xml " + obj.GetType()?.ToString() + "," + ex);
            }
            return string.Empty;
        }
    }
}
