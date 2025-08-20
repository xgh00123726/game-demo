namespace GameBase.EntitySystem
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
