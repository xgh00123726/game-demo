using System;

namespace GameBase.Tools
{
    public interface IUEntity<T> : IEntity
    {
        public T Obj { get; set; }
        public int ObjID { get; set; }
        Action AfterInstantiateObj { get; set; }
    }
}
