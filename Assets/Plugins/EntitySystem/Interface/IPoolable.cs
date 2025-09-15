namespace GameBase.EntitySystem
{
    public interface IPoolable
    {
        void AfterGet();
        void BeforeRelease();
    }
}
