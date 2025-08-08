using System.Collections;
using System.Collections.Generic;

namespace GameBase.Modify
{
    public class ModifyableContainer<T> : IEnumerable<Modifyable<T>>
    {
        private Dictionary<int, Modifyable<T>> _modifyables = new Dictionary<int, Modifyable<T>>();

        public bool Contains(int id)
        {
            return _modifyables.ContainsKey(id);
        }

        public Modifyable<T> this[int i]
        {
            get => _modifyables[i];
            set => _modifyables[i] = value;
        }

        public IEnumerator<Modifyable<T>> GetEnumerator()
        {
            return ((IEnumerable<Modifyable<T>>)_modifyables).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_modifyables).GetEnumerator();
        }
    }
}
