namespace GameBase.Tools
{
    public interface IBaseSys
    {
        void Awake();

        void Update();

        int GetEntityCount();

        int GetReleasedCount();

        int GetActiveCount();
    }
}
