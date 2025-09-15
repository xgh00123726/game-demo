using System.Collections.Generic;

namespace GameBase.EntitySystem
{
    public class LinkListContainer<T> : LinkedList<T>, IEContainer<T>
    {
        public void Add(T e)
        {
            AddLast(e);
        }
    }
}
