namespace GameBase.EntitySystem
{
    public interface IBaseSys
    {
        void Update();

        int GetEntityCount();

        int GetReleasedCount();

        int GetActiveCount();
    }
}
