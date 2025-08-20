using GameBase.Tools;
using NReco.Csv;
using System.IO;
using UnityEngine;

namespace GameBase.EntitySystem
{
    public abstract class BaseConstructor<T_Data, T_Entity, T_Container, T_EntitySys, T_Constructor>
        where T_Data : struct
        where T_Entity : class, IEntity, new()
        where T_Container : IEContainer, new()
        where T_Constructor : BaseConstructor<T_Data, T_Entity, T_Container, T_EntitySys, T_Constructor>, new()
        where T_EntitySys: SimplestEntitySys<T_Entity, T_Container,  T_EntitySys>, new()
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
                }
                return _instance;
            }
        }
        protected abstract void Parse(CsvReader line, ref T_Data data);
        protected abstract string RelativePath { get; }
        protected abstract T_EntitySys SysInstance { get; }
        protected abstract void Set(T_Entity e, in T_Data data);
        public T_Entity Get(int index)
        {
            if (index >= _datas.Length || index < 0)
            {
                XLogger.Instance.Log($"index out off array:{index}");
            }
            return SysInstance.NewEntity((T_Entity e) =>
            {
                Set(e, in _datas[index]);
            });
        }

        private void Init()
        {
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/ConstructorData/{RelativePath}");
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            _datas = new T_Data[int.Parse(csvReader[0])];
            while (csvReader.Read())
            {
                int index = int.Parse(csvReader[0]);
                Parse(csvReader, ref _datas[index]);
            }

            reader.Close();
        }
    }
}
