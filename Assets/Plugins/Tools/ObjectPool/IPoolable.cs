namespace GameBase.Tools
{
    public interface IPoolable
    {
        void AfterGet();
        void BeforeRelease();
    }
}
