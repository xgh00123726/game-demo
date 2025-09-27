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
        private Dictionary<T_EntityEnum, Func<int, T_Entity>> _constructorGetDict;
        protected abstract Dictionary<T_EntityEnum, Func<int, T_Entity>> GetConstructorGetDict();

        protected ConstructorFactory()
        {
            _constructorGetDict = GetConstructorGetDict();
        }

        public T_Entity Get(T_EntityEnum type, int id)
        {
            return _constructorGetDict[type](id);
        }
    }
}
