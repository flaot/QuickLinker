using QFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickLinker.Utils
{
    public interface IJsonSerializeUtility : IUtility
    {
        /// <summary> Json反序列化文件为类 </summary>
        T JsonDeserializeByFile<T>(string path);
        /// <summary> Json反序列化为类(内存数据) </summary>
        T JsonDeserializeByMem<T>(byte[] bytes);
        /// <summary> Json反序列化为类(文本数据) </summary>
        T JsonDeserializeByStr<T>(string str);
        /// <summary> obj序列化成Json文件 </summary>
        bool JsonSerializeToFile(string path, object obj);
        /// <summary> obj序列化成Json文本 </summary>
        string JsonSerializeToStr(object obj);
    }
    internal class JsonSerializeUtility : IJsonSerializeUtility
    {
        private JsonSerializerOptions _options;

        public JsonSerializerOptions JsonSerializerOpt
        {
            get {
                if (_options == null)
                { 
                    _options = new JsonSerializerOptions();
                    _options.IncludeFields = true;
                    _options.WriteIndented = true;
                }
                return _options;
            }
        }

        public T JsonDeserializeByFile<T>(string path)
        {
            T result = default(T);
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string value = streamReader.ReadToEnd();
                        result = JsonSerializer.Deserialize<T>(value, JsonSerializerOpt);
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
        public T JsonDeserializeByMem<T>(byte[] bytes)
        {
            T result = default(T);
            try
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string value = streamReader.ReadToEnd();
                        result = JsonSerializer.Deserialize<T>(value, JsonSerializerOpt);
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
        public T JsonDeserializeByStr<T>(string str)
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
                        result = JsonSerializer.Deserialize<T>(value, JsonSerializerOpt);
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
        public bool JsonSerializeToFile(string path, object obj)
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
                    {
                        string value = JsonSerializer.Serialize(obj, JsonSerializerOpt);
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
        public string JsonSerializeToStr(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj, JsonSerializerOpt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("此类无法转换成Json " + obj.GetType()?.ToString() + "," + ex);
            }
            return string.Empty;
        }
    }
}
