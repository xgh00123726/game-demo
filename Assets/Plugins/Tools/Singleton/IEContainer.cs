using System.Collections.Generic;

namespace GameBase.Tools
{
    public interface IEContainer<T> : IEnumerable<T>
    {
        void Add(T e);
        bool Remove(T e);   
        int Count { get; }
    }
}
