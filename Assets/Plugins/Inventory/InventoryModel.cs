using GameBase.Tools;
using System;
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
        private SortedIntList _nullIndexes = new((x, y) => (y - x));
        private int _size = 0;

        public int Size
        {
            get => _size;
            set
            {
                if (value < _size)
                {
                    return;
                }

                _items.Capacity = value;
                for (int i = 0; i < value - _size; ++i)
                {
                    _items.Add(new InventoryItem(default, false));
                    _nullIndexes.Push(i);
                }

                _size = value;
            }
        }

        public T_Item this[int i]
        {
            get => _items[i].item;
            private set => _items[i] = new InventoryItem(value, true);
        }

        public bool HasItem(int position)
        {
            if (position >= _size)
            {
                return false;
            }

            return _items[position].exist;
        }

        public virtual int AddItem(T_Item item)
        {
            if (_nullIndexes.Count > 0)
            {
                var index = _nullIndexes.Pop();
                this[index] = item;
                return index;
            }

            return -1;
        }

        public virtual int AddItem(T_Item item, int index)
        {
            if (index >= _size)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"inventory model out of range: i{index}, max size:{_size}");
                _items.Capacity = index + 1;

                return index;
            }
            else
            {
                if (_items[index].exist)
                {
                    XLogger.Instance.Log($"index:{index} of inventory:{this} has aready exist");
                }
                else
                {
                    this[index] = item;
                    _nullIndexes.Remove(index);
                }

                return index;
            }
        }

        public virtual void RemoveItem(int position)
        {
            if (!HasItem(position))
            {
                return;
            }

            _nullIndexes.Push(position);
            _items[position] = new InventoryItem(_items[position].item, false);
        }

        public virtual void Swap(int p1, int p2)
        {
            if (HasItem(p1) && HasItem(p2))
            {
                (_items[p2], _items[p1]) = (_items[p1], _items[p2]);
            }
            else if (HasItem(p1))
            {
                AddItem(_items[p1].item, p2);
                RemoveItem(p1);
            }
            else if (HasItem(p2))
            {
                AddItem(_items[p2].item, p1);
                RemoveItem(p2);
            }
        }
    }
}
