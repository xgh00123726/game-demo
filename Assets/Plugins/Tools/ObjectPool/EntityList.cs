using System.Collections;
using System.Collections.Generic;

namespace GameBase.Tools
{
    public class EntityList<T> : IEntityContainer<T>,
        IEnumerable<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();

        public bool Empty => _list.Count == 0;

        int IEntityContainer<T>.Count => _list.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_list).GetEnumerator();
        }

        void IEntityContainer<T>.Add(T e)
        {
            _list.AddLast(e);
        }

        T IEntityContainer<T>.Get()
        {
            return _list.First.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }

        void IEntityContainer<T>.Release(T e)
        {
            _list.Remove(e);
        }
    }
}
