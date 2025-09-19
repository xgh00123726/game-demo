namespace GameBase.EntitySystem
{
    public interface IUEntity<T>
    {
        public T Obj { get; set; }
        public int ObjID { get; set; }
    }
}
