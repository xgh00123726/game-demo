using GameBase.Tools;

namespace GameBase.Inventorys
{
    /// <summary>
    /// 可以动态扩容的仓库模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynInventory<T> : CommonInventory<T>
        where T : new()
    {
        public override int Add(T item)
        {
            if (_nullIndexes.Count > 0)
            {
                var index = _nullIndexes.Pop();
                this[index] = item;
                return index;
            }
            else
            {
                _items.Add(new CInventoryItem(item, true));
                return _items.Count - 1;
            }
        }

        public override int Add(T item, int index)
        {
            if (index >= Size)
            {
                Size = index + 1;
                this[index] = item;

                return index;
            }
            else
            {
                if (_items[index].exist)
                {
                    XLogger.Instance.Level(XLogger.LogLevel.Warning)
                        .Log($"index:{index} of inventory:{this} has aready exist");
                }
                else
                {
                    this[index] = item;
                    _nullIndexes.Remove(index);
                }

                return index;
            }
        }
    }
}
