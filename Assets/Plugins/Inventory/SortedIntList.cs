using System;
using System.Collections;
using System.Collections.Generic;

namespace GameBase.Inventorys
{
    public class SortedIntList : IEnumerable<int>
    {
        private Comparison<int> _comparison;
        private List<int> _list = new();

        public SortedIntList(Comparison<int> comparison)
        {
            _comparison = comparison;
        }

        public int Count => _list.Count;

        public void Push(int value)
        {
            _list.Add(value);
            _list.Sort(_comparison);
        }

        public int Pop()
        {
            int ret = _list[_list.Count - 1];
            _list.RemoveAt(_list.Count - 1);
            return ret;
        }

        public void Remove(int value)
        {
            _list.Remove(value);
            _list.Sort(_comparison);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return ((IEnumerable<int>)_list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }
    }
}
