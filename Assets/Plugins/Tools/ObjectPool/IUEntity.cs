namespace GameBase.Tools
{
    public interface IUEntity<T> : IEntity
    {
        public T Obj { get; set; }
        public int ObjID { get; }
    }
}
