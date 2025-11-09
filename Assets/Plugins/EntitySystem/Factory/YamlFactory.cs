using GameBase.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GameBase.EntitySystem
{
    public abstract class YamlFactory<T_YamlData, T_Entity, T_Factory> : Singleton<T_Factory>
        where T_Factory : YamlFactory<T_YamlData, T_Entity, T_Factory>, new()
    {
        private Dictionary<string, T_YamlData> _dataDict;

        protected abstract string YamlFolder { get; }

        public YamlFactory()
        {
            _dataDict = GetData();
        }

        private void ReadYamlData(DirectoryInfo dir, IDeserializer deserializer, Dictionary<string, T_YamlData> dict)
        {
            if (dir == null || !dir.Exists)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid dir:{dir.FullName}");
                return;
            }
            foreach (FileInfo f in dir.GetFiles())
            {
                if (f.Name.EndsWith(".yaml") || f.Name.EndsWith(".yml"))
                {
                    using var reader = File.OpenText(f.FullName);
                    var secs = f.Name.Split('.');
                    if (secs != null && secs.Length > 0)
                    {
                        var name = secs[0];
                        try
                        {
                            dict[name] = deserializer.Deserialize<T_YamlData>(reader);
                        }
                        catch (Exception e)
                        {
                            XLogger.Instance.Level(XLogger.LogLevel.Error)
                                .Log(e);
                        }
                    }
                }
            }

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                ReadYamlData(subDir, deserializer, dict);
            }
        }

        protected virtual Dictionary<string, T_YamlData> GetData()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            Dictionary<string, T_YamlData> ret;
            if (YamlFolder == null || YamlFolder.Length == 0)
            {
                ret = null;
            }
            else
            {
                ret = new();
                ReadYamlData(new DirectoryInfo(YamlFolder), deserializer, ret);
            }

            return ret;
        }
        protected abstract T_Entity GetEntity(T_YamlData data);

        public T_Entity GetFromData(T_YamlData data)
        {
            return GetEntity(data);
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
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"Factory:{GetType().Name} has no key: {key}");
                return default;
            }
            T_YamlData data = _dataDict[key];
            if (data == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"Factory:{GetType().Name} has null value which key is: {key}");
            }

            return GetEntity(data);
        }
    }
}
