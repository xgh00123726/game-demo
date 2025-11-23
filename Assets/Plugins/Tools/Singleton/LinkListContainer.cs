using System.Collections.Generic;

namespace GameBase.Tools
{
    public class LinkListContainer<T> : LinkedList<T>, IEContainer<T>
    {
        public void Add(T e)
        {
            AddLast(e);
        }
    }
}
