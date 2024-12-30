using QFramework;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace QuickLinker.Utils
{
    public class SerializeOpt : IUtility
    {
        public static T XmlDeserializeByFile<T>(string path) where T : class
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
        public static T XmlDeserializeByBytes<T>(byte[] bytes) where T : class
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
        public static T XmlDeserializeByStr<T>(string str)
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
        public static bool XmlSerializeByFile(string path, object obj)
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
        public static string XmlSerializeByStr(object obj)
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


        public static T JsonDeserializeByFile<T>(string path)
        {
            T result = default(T);
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string value = streamReader.ReadToEnd();
                        result = JsonSerializer.Deserialize<T>(value);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("此Json文件无法转成类: " + path + "," + ex);
            }
            return result;
        }
        public static T JsonDeserializeByBytes<T>(byte[] bytes)
        {
            T result = default(T);
            try
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string value = streamReader.ReadToEnd();
                        result = JsonSerializer.Deserialize<T>(value);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("此Json字节数组无法转成类: " + bytes.ToString() + "," + ex);
            }
            return result;
        }
        public static T JsonDeserializeByStr<T>(string str)
        {
            T result = default(T);
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(str);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string value = streamReader.ReadToEnd();
                        result = JsonSerializer.Deserialize<T>(value);
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
        public static bool JsonSerializeByFile(string path, object obj)
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
                    {
                        string value = JsonSerializer.Serialize(obj);
                        streamWriter.Write(value);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("此类无法转换成Json " + obj.GetType()?.ToString() + "," + ex);
            }
            return false;
        }
        public static string JsonSerializeByStr(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("此类无法转换成Json " + obj.GetType()?.ToString() + "," + ex);
            }
            return string.Empty;
        }
    }
}
