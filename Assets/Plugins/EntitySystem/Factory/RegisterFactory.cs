using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace GameBase.EntitySystem
{
    public class RegisterFactory<T_Entity, T_Factory> : Singleton<T_Factory>
        where T_Factory : RegisterFactory<T_Entity, T_Factory>, new()
    {
        private Dictionary<string, Func<T_Entity>> _entityGetters = new();

        public T_Entity Get(string name)
        {
            if (_entityGetters.ContainsKey(name))
            {
                if (_entityGetters[name] == null)
                {
                    return default;
                }
                return _entityGetters[name].Invoke();
            }

            return default;
        }

        public void Register(string name, Func<T_Entity> getter)
        {
            _entityGetters[name] = getter;
        }
    }
}
