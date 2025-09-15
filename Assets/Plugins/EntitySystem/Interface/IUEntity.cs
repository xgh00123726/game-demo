namespace GameBase.EntitySystem
{
    public interface IUEntity<T> : IEntity
    {
        public T Obj { get; set; }
        public int ObjID { get; set; }
    }
}
