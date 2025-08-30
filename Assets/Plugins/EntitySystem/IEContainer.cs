namespace GameBase.EntitySystem
{
    public interface IEContainer<T>
    {
        T GetEntity();

        void ReleaseEntity(T e);

        int Count { get; }
    }
}
