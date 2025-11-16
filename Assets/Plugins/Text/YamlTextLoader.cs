using GameBase.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GameBase.Texts
{
    public class YamlTextLoader<T>
    {
        private Dictionary<string, T> _dataDict;

        public void LoadData(string fileName, string lang)
        {
            _dataDict = ReadData(fileName, lang);
            XLogger.Instance.IF(false).Log($"read {_dataDict.Count} item from file: {fileName}");
        }

        public T this[string name]
        {
            get
            {
                return GetData(name);
            }
        }

        public T GetData(string name)
        {
            if (!_dataDict.ContainsKey(name))
            {
                return default;
            }

            return _dataDict[name];
        }

        protected virtual Dictionary<string, T> ReadData(string fileName, string lang)
        {
            var fullPath = $"{Application.streamingAssetsPath}/Texts/{lang}/{fileName}";
            using var reader = File.OpenText(fullPath);
            if (reader == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"TextMgr get a invalid file:{fullPath}");
                return null;
            }

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            try
            {
                return deserializer.Deserialize<Dictionary<string, T>>(reader);
            }
            catch (Exception e)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"Read text in file:{fullPath}, exception:{e}");
            }
            

            return null;
        }
    }
}
