namespace GameBase.Tools
{
    public interface IEContainer
    {
        T_Entity GetEntity<T_Entity>() where T_Entity : class, IEntity, new();

        void RegisterType<T_Entity>() where T_Entity : class, IEntity, new();

        void ReleaseEntity(IEntity e);

        int Count { get; }
    }
}
