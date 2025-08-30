using System;
using System.Collections.Generic;

namespace GameBase.EntitySystem
{
    public class CommonEntityContainer<T> : IEContainer<T>
        where T : new()
    {
        public CommonEntityContainer()
        {
            _pool.InstantiateFunc = () => new T();
        }

        private BaseObjectPool<T> _pool = new();

        int IEContainer<T>.Count => _pool.Count;

        T IEContainer<T>.GetEntity()
        {
            return _pool.Get();
        }

        void IEContainer<T>.ReleaseEntity(T e)
        {
            _pool.Release(e);
        }
    }
}
