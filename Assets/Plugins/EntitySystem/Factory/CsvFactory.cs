using GameBase.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using YamlDotNet.Serialization;

namespace GameBase.EntitySystem
{
    public abstract class CsvFactory<T_Data, T_Entity, T_Factory> : Singleton<T_Factory>
        where T_Factory : CsvFactory<T_Data, T_Entity, T_Factory>, new()
    {
        public const string TEMPLATE_NAME = "Template.csv";

        protected T_Data[] _datas;
        protected Dictionary<string, T_Data> _dict;
        protected abstract string Folder { get; }
        protected virtual string FileName { get; } = null;
        protected virtual string TemplatePath { get; }
        public CsvFactory()
        {
            _datas = ReadData();
            if (_datas != null)
            {
                for (int i = 0; i < _datas.Length; i++)
                {
                    OnInitCsvData(ref _datas[i]);
                }

                FieldInfo[] fieldInfos = typeof(T_Data).GetFields(BindingFlags.Public | BindingFlags.Instance);
                bool hasName = false;
                FieldInfo nameField = null;
                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    if (fieldInfo.FieldType == typeof(string) && fieldInfo.Name == "name")
                    {
                        hasName = true;
                        nameField = fieldInfo;
                        break;
                    }
                }

                if (hasName)
                {
                    for (int i = 0; i < _datas.Length; i++)
                    {
                        SetName(nameField.GetValue(_datas[i]).ToString(), ref _datas[i]);
                    }
                }
            }
        }

        protected virtual void OnInitCsvData(ref T_Data data) { }
        protected virtual void SetName(string name, ref T_Data data)
        {
            if (data is INamedData nd)
            {
                nd.Name = name;
            }
        }

        protected virtual T_Data[] ReadCsvData(string path)
        {
            return CsvReaderExtend.Read<T_Data>(path).ToArray();
        }

        private T_Data[] ReadData()
        {
            if (Folder == null)
            {
                return null;
            }
            else
            {
                var fileName = FileName;
                if (fileName == null)
                {
                    fileName = $"{typeof(T_Data).Name}.csv";
                }
                return ReadCsvData($"{Folder}/{fileName}");
            }
        }

        protected abstract T_Entity GetEntity(T_Data data);

        public T_Entity GetFromData(T_Data data)
        {
            return GetEntity(data);
        }

        public T_Data GetData(int key)
        {
            if (_datas == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"Factory:{GetType().Name} has not yaml dict");
                return default;
            }
            if (key > _datas.Length)
            {
                return default;
            }

            return _datas[key];
        }

        public T_Entity Get(int key)
        {
            if (_datas == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"Factory:{GetType().Name} has not yaml dict");
                return default;
            }
            if (key > _datas.Length)
            {
                return default;
            }
            T_Data data = _datas[key];
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
            if (data == null)
            {
                data = General.CreateNotNullInstance<T_Data>();
            }
            var datas = new T_Data[2]
            {
                data,
                data,
            };
            CsvWriterExtend.Write(datas, path);

            XLogger.Instance.Log($"success to write template file to: {path}");
        }
    }
}
