namespace GameBase.EntitySystem
{
    public interface IBaseSys
    {
        void Update();

        void FixedUpdate();

        int GetEntityCount();

        int GetReleasedCount();

        int GetActiveCount();
    }
}
