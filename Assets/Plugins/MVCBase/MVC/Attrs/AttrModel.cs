using GameBase.Tools;
using System.Collections.Generic;

namespace Instance.UI.MVC
{
    public class AttrModel : AutoFillList<int>, IMVCModel<int>
    {
        public int Size
        {
            get => Count;
            set => Resize(value);
        }

        public int AddItem(int item)
        {
            Add(item);
            return Count - 1;
        }

        public int AddItem(int item, int index)
        {
            Add(item, index);
            return index;
        }

        public bool HasItem(int index)
        {
            return index < Count;
        }

        public bool RemoveItem(int index)
        {
            RemoveAt(index);
            return true;
        }

        public void Swap(int p1, int p2)
        {
            (this[p1], this[p2]) = (this[p2], this[p1]);
        }
    }
}
