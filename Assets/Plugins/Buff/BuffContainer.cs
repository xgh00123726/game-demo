using System;
using System.Collections;
using System.Collections.Generic;

namespace GameBase.Buffs
{
    public class BuffContainer : IEnumerable<Buff>
    {
        private LinkedList<Buff> _buffs = new LinkedList<Buff>();
        public Action<Buff> OnAddBuff;
        public Action<Buff> OnRemoveBuff;

        public bool HasBuff(Buff buff)
        {
            return _buffs.Contains(buff);
        }

        public void AddBuff(Buff buff)
        {
            OnAddBuff?.Invoke(buff);
            _buffs.AddLast(buff);
        }

        public void RemoveBuff(Buff buff)
        {
            OnRemoveBuff?.Invoke(buff);
            _buffs.Remove(buff);
        }

        public void ClearBuff(Buff buff)
        {
            foreach (var item in _buffs)
            {
                OnRemoveBuff?.Invoke(item);
            }
            _buffs.Clear();
        }


        public IEnumerator<Buff> GetEnumerator()
        {
            return ((IEnumerable<Buff>)_buffs).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_buffs).GetEnumerator();
        }
    }
}
