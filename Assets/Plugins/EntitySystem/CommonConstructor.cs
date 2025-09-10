namespace GameBase.EntitySystem
{
    public class CommonConstructor<T> : IEConstructor<T>
        where T : new()
    {
        public CommonConstructor()
        {
            _pool.InstantiateFunc = () => new T();
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
