namespace GameBase.EntitySystem
{
    public interface IEConstructor<T>
    {
        T GetEntity();

        void ReleaseEntity(T e);

        int Count { get; }
    }
}
