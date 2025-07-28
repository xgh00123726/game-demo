namespace GameBase.Tools
{
    public interface IEntity<T>
    {
        T Obj { get; set; }
        int ID { get; }
    }
}
