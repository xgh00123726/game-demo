using GameBase.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GameBase.EntitySystem
{
    public class GenTemplateAttribute : Attribute
    {
        public GenTemplateAttribute(string path = null, string comment = null)
        {
            Path = path;
            Comment = comment;
        }
        public string Path { get; set; }
        public string Comment { get; set; }
    }
    public abstract class YamlFactory<T_Data, T_Entity, T_Factory> : Singleton<T_Factory>
        where T_Factory : YamlFactory<T_Data, T_Entity, T_Factory>, new()
    {
        public const string TEMPLATE_NAME = "Template.yaml";

        protected Dictionary<string, T_Data> _dataDict;

        protected abstract string Folder { get; }
        protected virtual string TemplatePath { get; }

        public YamlFactory()
        {
            _dataDict = ReadData();
        }

        protected virtual void OnInitYamlData(ref T_Data data) { }
        protected virtual void SetName(string name, ref T_Data data)
        {
            if (data is INamedData nd)
            {
                nd.Name = name;
            }
        }

        private void ReadYamlData(DirectoryInfo dir, IDeserializer deserializer, Dictionary<string, T_Data> dict)
        {
            if (dir == null || !dir.Exists)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid dir:{dir.FullName}");
                return;
            }
            foreach (FileInfo f in dir.GetFiles())
            {
                if (f.Name == TEMPLATE_NAME)
                {
                    continue;
                }
                if (f.Name.EndsWith(".yaml") || f.Name.EndsWith(".yml"))
                {
                    using var reader = File.OpenText(f.FullName);
                    var secs = f.Name.Split('.');
                    if (secs != null && secs.Length > 0)
                    {
                        var name = secs[0];
                        try
                        {
                            var yamlData = deserializer.Deserialize<T_Data>(reader);
                            dict[name] = yamlData;
                            SetName(name, ref yamlData);
                            OnInitYamlData(ref yamlData);
                        }
                        catch (Exception e)
                        {
                            XLogger.Instance.Level(XLogger.LogLevel.Error)
                                .Log($"error in file:{f.Name}, exception:{e}");
                        }
                    }
                }
            }

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                ReadYamlData(subDir, deserializer, dict);
            }
        }

        protected virtual Dictionary<string, T_Data> ReadData()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(NullNamingConvention.Instance)
                .Build();

            Dictionary<string, T_Data> ret;
            if (Folder == null || Folder.Length == 0)
            {
                ret = null;
            }
            else
            {
                ret = new();
                ReadYamlData(new DirectoryInfo(Folder), deserializer, ret);
            }

            return ret;
        }
        protected abstract T_Entity GetEntity(T_Data data);

        public T_Entity GetFromData(T_Data data)
        {
            return GetEntity(data);
        }

        public T_Data GetData(string key)
        {
            if (_dataDict == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"Factory:{GetType().Name} has not yaml dict");
                return default;
            }
            if (!_dataDict.ContainsKey(key))
            {
                return default;
            }

            return _dataDict[key];
        }

        public T_Entity Get(string key)
        {
            if (_dataDict == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"Factory:{GetType().Name} has not yaml dict");
                return default;
            }
            if (!_dataDict.ContainsKey(key))
            {
                return default;
            }
            T_Data data = _dataDict[key];
            if (data == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"Factory:{GetType().Name} has null value which key is: {key}");
            }

            return GetEntity(data);
        }

        protected virtual T_Data GetTemplateData() => default;

        public void GenerateTemplate()
        {
            Type type = GetType();
            GenTemplateAttribute attribute = type.GetCustomAttribute<GenTemplateAttribute>();
            string path = null;
            string comment = null;
            if (attribute != null)
            {
                path = attribute.Path;
                comment = attribute.Comment;
            }
            if (path == null)
            {
                if (Folder == null)
                {
                    return;
                }
                path = $"{Folder}/{TEMPLATE_NAME}";
            }
            var data = GetTemplateData();
            data ??= General.CreateNotNullInstance<T_Data>();
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(data);
            if (comment != null)
            {
                yaml = $"# {comment}\n# this is a template file, which will be ignored\n{yaml}";
            }
            File.WriteAllText(path, yaml);
            XLogger.Instance.Log($"success to write template file to: {path}");
        }
    }
}
