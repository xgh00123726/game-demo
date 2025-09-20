using GameBase.Tools;

namespace GameBase.EntitySystem
{
    public class PoolConstructor<T> : IEConstructor<T>
        where T : new()
    {
        public PoolConstructor()
        {
            _pool.InstantiateFunc = static () => new T();
        }

        private BaseObjectPool<T> _pool = new();

        int IEConstructor<T>.Count => _pool.Count;

        T IEConstructor<T>.GetEntity()
        {
            return _pool.Get();
        }

        void IEConstructor<T>.ReleaseEntity(T e)
        {
            _pool.Release(e);
        }
    }
}
