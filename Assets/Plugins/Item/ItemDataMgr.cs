using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameBase.Tools;

namespace GameBase.Items
{
    public class ItemDataMgr
    {
        private static int _maxItemNum = 65535;
        private static List<ItemData> _items = new();

        private static void RegisterItems<T>(List<ItemData> existItems, List<T> items, int maxItemNum, ItemDataType type) where T : ItemData
        {
            foreach (var item in items)
            {
                if (item.ID > maxItemNum)
                {
                    XLogger.Instance.Level(XLogger.LogLevel.Error)
                        .Log($"invalid item id:{item.ID} of type:{item.GetType()}, item id must lessequal than {maxItemNum}");
                }
                if (item.ID >= existItems.Count)
                {
                    var lastCount = existItems.Count;
                    var newCount = item.ID + 1;
                    for (int i = lastCount; i < newCount; i++)
                    {
                        existItems.Add(null);
                    }
                    existItems[item.ID] = item;
                    existItems[item.ID].Type = type;
                }
            }
        }

        public static List<int> Check()
        {
            var ret = new List<int>();
            for (int i = 0; i < _items.Count; ++i)
            {
                if (_items[i] == null)
                {
                    ret.Add(i);
                }
            }

            return ret;
        }

        public static void RegisterType<T_Data, T_Register>(T_Register registerInstance) where T_Data : ItemData
            where T_Register : IItemDataRegister<T_Data>
        {
            if (Enum.TryParse(typeof(T_Data).Name, out ItemDataType type))
            {
                RegisterItems(_items, registerInstance.GetRegisteredItems(), _maxItemNum, type);
            }
            else
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid item type of: {typeof(T_Data).Name}");
            }
        }

        public static ItemData Get(int id)
        {
            if (id >= _items.Count)
            {
                return null;
            }

            return _items[id];
        }

        public static T Get<T>(int id) where T : ItemData
        {
            if (id >= _items.Count)
            {
                return null;
            }

            var item = _items[id];
            if (item is T e)
            {
                return e;
            }

            return null;
        }
    }
}
