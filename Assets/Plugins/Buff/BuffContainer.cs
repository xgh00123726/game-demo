using System.Collections;
using System.Collections.Generic;

namespace GameBase.Buffs
{
    public class BuffContainer : IEnumerable<Buff>
    {
        private LinkedList<Buff> _buffs = new LinkedList<Buff>();

        public bool HasBuff(Buff buff)
        {
            return _buffs.Contains(buff);
        }

        public void AddBuff(Buff buff)
        {
            _buffs.AddLast(buff);
        }

        public void RemoveBuff(Buff buff)
        {
            _buffs.Remove(buff);
        }

        public void ClearBuff(Buff buff)
        {
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
