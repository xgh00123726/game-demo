using GameBase.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GameBase.Texts
{
    public class YamlTextLoader<T> where T : IKeywordText
    {
        private Dictionary<string, T> _dataDict;

        internal string lang;
        internal string defaultLang;
        internal string fileName;

        public string Lang
        {
            get => lang;
            set
            {
                if (defaultLang == null && value != null)
                {
                    defaultLang = value;
                }

                if (lang == null)
                {
                    lang = value;
                }
                else
                {
                    if (lang != value)
                    {
                        lang = value;
                        ReloadData();
                    }
                }
            }
        }

        public void ReloadData()
        {
            LoadData(this.fileName);
        }

        public void LoadData(string fileName)
        {
            this.fileName = fileName;
            _dataDict = ReadData(fileName, lang);
            foreach (var val in _dataDict.Values)
            {
                val.ReplaceKeywords();
            }
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
            if (!File.Exists(fullPath))
            {
                fullPath = $"{Application.streamingAssetsPath}/Texts/{defaultLang}/{fileName}";
            }
            if (!File.Exists(fullPath))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"missing {fileName} in folder: {Application.streamingAssetsPath}/Texts/{defaultLang}");
                return null;
            }
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
