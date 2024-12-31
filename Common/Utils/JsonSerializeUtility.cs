using QFramework;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

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
                    _options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                    _options.Converters.Add(new BindablePropertyConverter());
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
    internal class BindablePropertyConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
                return false;

            if (typeToConvert.GetGenericTypeDefinition() != typeof(BindableProperty<>))
                return false;

            return true;
        }

        public override JsonConverter CreateConverter(
            Type type,
            JsonSerializerOptions options)
        {
            Type[] typeArguments = type.GetGenericArguments();
            Type keyType = typeArguments[0];

            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(BindablePropertyJsonConverter<>).MakeGenericType(
                    [keyType]),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: [options],
                culture: null)!;

            return converter;
        }

        internal class BindablePropertyJsonConverter<T> : JsonConverter<BindableProperty<T>>
        {
            private JsonConverter<T> s_defaultConverter;
            public BindablePropertyJsonConverter(JsonSerializerOptions options)
            {
                s_defaultConverter = (JsonConverter<T>)options.GetConverter(typeof(T));
            }
            public override BindableProperty<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                BindableProperty<T> reault = new BindableProperty<T>();
                T t = s_defaultConverter.Read(ref reader, typeof(T), options);
                reault.SetValueWithoutEvent(t);
                return reault;
            }

            public override void Write(
                Utf8JsonWriter writer,
                BindableProperty<T> objectToWrite,
                JsonSerializerOptions options)
            {
                s_defaultConverter.Write(writer, objectToWrite.Value, options);
            }
        }
    }
}
