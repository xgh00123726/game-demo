using System;
using System.Collections.Generic;

namespace Constructor
{
    public abstract class ConstructorFactory<T_EntityEnum, T_Entity, T_Factory>
        where T_Factory : ConstructorFactory<T_EntityEnum, T_Entity, T_Factory>, new()
    {
        private Dictionary<T_EntityEnum, Func<int, T_Entity>> _constructorGetDict;
        protected abstract Dictionary<T_EntityEnum, Func<int, T_Entity>> ConstructorGetDict { get; }

        private static T_Factory _instance;
        public static T_Factory Instance
        {
            get
            {
                _instance ??= new T_Factory();
                _instance._constructorGetDict = _instance.ConstructorGetDict;
                return _instance;
            }
        }

        public T_Entity Get(T_EntityEnum type, int id)
        {
            return _constructorGetDict[type](id);
        }
    }
}
