using System;
using System.Collections.Generic;

namespace GameBase.Tools
{
    public class SimpleEntityContainer : IEContainer
    {
        private Dictionary<Type, BaseObjectPool<IEntity>> _pools = new();

        public int Count
        {
            get
            {
                int sum = 0;
                foreach (var pool in _pools.Values)
                {
                    sum += pool.Count;
                }
                return sum;
            }
        }

        T_Entity IEContainer.GetEntity<T_Entity>()
        {
            var type = typeof(T_Entity);

            return _pools[type].Get() as T_Entity;
        }

        void IEContainer.RegisterType<T_Entity>()
        {
            var type = typeof(T_Entity);
            if (!_pools.ContainsKey(type))
            {
                var pool = new BaseObjectPool<IEntity>();
                pool.InstantiateFunc = static () => new T_Entity();
                _pools.Add(type, pool);
            }
        }

        void IEContainer.ReleaseEntity(IEntity e)
        {
            _pools[e.GetType()].Release(e);
        }
    }
}
