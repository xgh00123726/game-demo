namespace GameBase.Tools
{
    public interface IEntity<T>
    {
        public T Obj { get; set; }
        public int ID { get; }
    }
}
