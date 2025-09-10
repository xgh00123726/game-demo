using System;
using System.Collections.Generic;
using GameBase.Tools;

namespace Constructor
{
    public abstract class ConstructorFactory<T_EntityEnum, T_Entity, T_Factory> : Signleton<T_Factory>
        where T_Factory : ConstructorFactory<T_EntityEnum, T_Entity, T_Factory>, new()
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
