using System.Collections.Generic;

namespace GameBase.EntitySystem
{
    public interface IEContainer<T> : IEnumerable<T>
    {
        void Add(T e);
        bool Remove(T e);   
        int Count { get; }
    }
}
