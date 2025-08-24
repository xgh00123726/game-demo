using GameBase.Tools;
using NReco.Csv;
using System;
using System.IO;
using UnityEngine;

namespace Constructor
{
    public abstract class BaseConstructor<T_Data, T_Entity, T_Constructor>
        where T_Data : struct
        where T_Entity : class, new()
        where T_Constructor : BaseConstructor<T_Data, T_Entity, T_Constructor>, new()
    {
        private T_Data[] _datas;
        private static T_Constructor _instance;
        public static T_Constructor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T_Constructor();
                    _instance.Init();
                    Command.Register($"{typeof(T_Constructor).FullName}-init", _instance.Init);
                }
                return _instance;
            }
        }
        protected abstract void Parse(CsvReader line, ref T_Data data);
        protected abstract string RelativePath { get; }
        protected abstract void Set(T_Entity e, in T_Data data);
        protected abstract T_Entity Get(Action<T_Entity> Init);


        public T_Entity Get(int index)
        {
            if (index >= _datas.Length || index < 0)
            {
                XLogger.Instance.Log($"index out off array:{index}");
            }
            return Get((T_Entity e) =>
            {
                Set(e, in _datas[index]);
            });
        }

        private void Init()
        {
            if (RelativePath == null || RelativePath.Length == 0 || RelativePath == "")
            {
                return;
            }
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/ConstructorData/{RelativePath}");
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            _datas = new T_Data[int.Parse(csvReader[0])];
            for (int i = 0; i < _datas.Length; i++)
            {
                csvReader.Read();
                int index = int.Parse(csvReader[0]);
                Parse(csvReader, ref _datas[index]);
            }

            reader.Close();
        }
    }
}
