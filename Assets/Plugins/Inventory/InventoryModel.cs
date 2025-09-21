using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace GameBase.Inventorys
{
    /// <summary>
    /// <list type="bullet">
    /// <item>常规背包模型</item>
    /// <item>背包中可以留有空位</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T_Item"></typeparam>
    public class InventoryModel<T_Item>
        where T_Item : new()
    {
        protected class CInventoryItem
        {
            public T_Item item;
            public bool exist;
            public CInventoryItem(T_Item item, bool exist)
            {
                this.item = item;
                this.exist = exist;
            }
        }

        protected List<CInventoryItem> _items = new();
        protected SortedIntList _nullIndexes = new((x, y) => (y - x));

        public virtual int Size
        {
            get => _items.Count;
            set
            {
                var size = _items.Count;
                if (value < size)
                {
                    return;
                }

                for (int i = 0; i < value - size; ++i)
                {
                    _items.Add(new CInventoryItem(new T_Item(), false));
                    _nullIndexes.Push(i);
                }
            }
        }

        public int Count => _items.Count - _nullIndexes.Count;

        public T_Item this[int i]
        {
            get => _items[i].item;
            set
            {
                _items[i].item = value;
                _items[i].exist = true;
            }
        }


        public bool HasItem(int position)
        {
            if (position >= Size)
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
            if (index >= Size)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"inventory model out of range: i{index}, max size:{Size}");
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

        public virtual bool RemoveItem(int position)
        {
            if (!HasItem(position))
            {
                return false;
            }

            _nullIndexes.Push(position);
            _items[position].exist = false;

            return true;
        }

        public virtual void Swap(int p1, int p2)
        {
            if (p1 >= Size || p2 >= Size)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid pos:{p1}, {p2}");
                return;
            }

            (_items[p2], _items[p1]) = (_items[p1], _items[p2]);
            if (HasItem(p1) && !HasItem(p2))
            {
                _nullIndexes.Remove(p2);
                _nullIndexes.Push(p1);
            }
            else if (HasItem(p2) && !HasItem(p1))
            {
                _nullIndexes.Remove(p1);
                _nullIndexes.Push(p2);
            }
        }

        public void SortItems()
        {
            int p1 = 0; // p1指针指向当前需要被填充的地址
            int p2 = 0; // p2指针指向当前寻找到的需要用于填充的地址
            bool p1Ready = false;
            bool p2Ready = false;
            // 规则：
            // p1指针不断向前移动，碰到空地址就停下，并标记p1已经ready
            // p2从p1指针处开始移动（首次），碰到非空地址就停下，并标记p2已经ready
            // 当p1和p2都ready的时候，将p2复制到p1，并标记p2为空
            // 当p2遍历到_size时，算法结束，并重新计算nullIndex

            if (_nullIndexes.Count == 0)
            {
                // 空下标列表是空的，代表model是满的，不进行排序
                return;
            }

            // 第一次需要手动计算p1和p2
            for (; p1 < Size; ++p1)
            {
                if (!HasItem(p1))
                {
                    break;
                }
            }

            if (p1 >= Size)
            {
                // p1超过size，则代表model是全的，不需要sort，但是上面已经判断过了，
                // 如果这个分支仍然可以进入，则代表程序有bug
                XLogger.Instance.Level(XLogger.LogLevel.Fatal)
                            .Log("your program has bug，please fixed");
                return;
            }

            p1Ready = true;
            p2 = p1;
            for (; p2 < Size; ++p2)
            {
                if (p1Ready && p2Ready)
                {
                    _items[p1] = _items[p2];
                    _items[p2].exist = false;
                }
                else if (!p1Ready)
                {
                    for (; p1 < Size; ++p1)
                    {
                        if (!HasItem(p1))
                        {
                            p1Ready = true;
                            break;
                        }
                    }
                }
                else if (!p2Ready)
                {
                    for (; p2 < Size; ++p2)
                    {
                        if (HasItem(p2))
                        {
                            p2Ready = true;
                            break;
                        }
                    }
                    if (p2 == Size)
                    {
                        // p2到顶，循环结束
                        break;
                    }
                }
            }

            // 重新设置p1
            for (; p1 < Size; ++p1)
            {
                _nullIndexes.Push(p1);
            }
        }
    }
}
