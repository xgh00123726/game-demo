namespace GameBase.Tools
{
    public interface IEntityContainer<T>
    {
        public T Get();
        public void Release(T e);

        public void Add(T e);

        public int Count { get; }

        public bool Empty { get; }
    }
}
