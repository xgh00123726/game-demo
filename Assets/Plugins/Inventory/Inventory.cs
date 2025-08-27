using System.Collections.Generic;

namespace GameBase.Inventorys
{
    public class Inventory<T_Item, T_DataBase>
        where T_Item : struct, IItem
        where T_DataBase : IDataBase<T_Item>
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
        private int _count = 0;

        public bool HasItem(int position)
        {
            return position < _count;
        }

        public void AddItem(T_Item item)
        {
            _items.Add(new InventoryItem(item, true));
            _count++;
        }

        public void RemoveItem(int position)
        {
            if (!HasItem(position))
            {
                return;
            }

            var item = _items[position];
            item.exist = false;
            _items[position] = item;
            _count--;
        }

        public void Swap(int p1, int p2)
        {
            var t = _items[p1];
            _items[p1] = _items[p2];
            _items[p2] = t;
        }

        public void Sort()
        {
            _items.Sort();
        }
    }
}
