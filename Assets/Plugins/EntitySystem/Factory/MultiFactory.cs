using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace GameBase.EntitySystem
{
    public abstract class MultiFactory<T_Entity, T_Factory> : Singleton<T_Factory>
        where T_Factory : MultiFactory<T_Entity, T_Factory>, new()
    {
        private List<Func<string, T_Entity>> _entityGetters = new();

        public void Register(Func<string, T_Entity> getter)
        {
            _entityGetters.Add(getter);
        }

        public T_Entity Get(string key)
        {
            foreach (var getter in _entityGetters)
            {
                if (getter == null)
                {
                    continue;
                }
                var val = getter(key);
                if (val != null)
                {
                    return val;
                }
                else
                {
                    continue;
                }
            }

            return default;
        }
    }
}
