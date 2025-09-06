using System.Collections.Generic;

namespace GameBase.Inventorys
{
    public class InventoryModel<T_Item>
        where T_Item : struct, IModelItem
    {
        private struct InventoryItem
        {
            public T_Item item;
            public bool exist;
            public InventoryItem(T_Item item, bool exist)
            {
                this.item = item;
                this.exist = exist;
            }
        }

        private List<InventoryItem> _items = new();
        private List<int> _nullIndex = new();

        public T_Item this[int i]
        {
            get => _items[i].item;
            set => _items[i] = new InventoryItem(value, true);
        }

        public bool HasItem(int position)
        {
            if (position >= _items.Count)
            {
                return false;
            }

            return _items[position].exist;
        }

        public int AddItem(T_Item item)
        {
            if (_nullIndex.Count > 0)
            {
                var i = _nullIndex[_nullIndex.Count - 1];
                _nullIndex.RemoveAt(_nullIndex.Count - 1);
                _items[i] = new InventoryItem(item, true);
                return i;
            }
            else
            {
                _items.Add(new InventoryItem(item, true));
                return _items.Count - 1;
            }
        }

        public void RemoveItem(int position)
        {
            if (!HasItem(position))
            {
                return;
            }

            _nullIndex.Add(position);
            // ½µÐòÅÅÁÐ
            _nullIndex.Sort((x, y) => -x.CompareTo(y));
            var item = _items[position];
            item.exist = false;
            _items[position] = item;
        }

        public void Swap(int p1, int p2)
        {
            (_items[p2], _items[p1]) = (_items[p1], _items[p2]);
        }
    }
}
