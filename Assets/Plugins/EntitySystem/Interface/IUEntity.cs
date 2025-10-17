namespace GameBase.EntitySystem
{
    public interface IUEntity<T>
    {
        public T obj { get; set; }
        public int ObjID { get; set; }
    }
}
