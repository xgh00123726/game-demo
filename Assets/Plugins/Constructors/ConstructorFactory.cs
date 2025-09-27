using GameBase.Tools;
using NReco.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

namespace Constructor
{
    public abstract class ConstructorFactory<T_EntityEnum, T_Entity, T_Factory> : Singleton<T_Factory>
        where T_Factory : ConstructorFactory<T_EntityEnum, T_Entity, T_Factory>, new()
        where T_EntityEnum : struct
    {
        public struct FactoryReflect
        {
            public int id;
            public T_EntityEnum type;
            public int typeID;
        }

        private Dictionary<T_EntityEnum, Func<int, T_Entity>> _constructorGetDict;
        protected abstract Dictionary<T_EntityEnum, Func<int, T_Entity>> GetConstructorGetDict();

        protected virtual string RelativePath => "";

        protected FactoryReflect[] _factoryReflects;

        protected ConstructorFactory()
        {
            _constructorGetDict = GetConstructorGetDict();
            Init();
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
            _factoryReflects = new FactoryReflect[int.Parse(csvReader[0])];
            for (int i = 0; i < _factoryReflects.Length; i++)
            {
                csvReader.Read();
                int id = int.Parse(csvReader[0]);
                Enum.TryParse(csvReader[1], out T_EntityEnum type);
                int entityID = int.Parse(csvReader[2]);

                _factoryReflects[i] = new FactoryReflect()
                {
                    id = id,
                    type = type,
                    typeID = entityID
                };
            }

            reader.Close();
        }

        public FactoryReflect GetInfo(int id)
        {
            return _factoryReflects[id];
        }

        public T_Entity Get(int id)
        {
            if (_factoryReflects == null)
            {
                return default;
            }

            var data = _factoryReflects[id];
            return Get(data.type, data.typeID);
        }

        public T_Entity Get(T_EntityEnum type, int id)
        {
            return _constructorGetDict[type](id);
        }
    }
}
