using GameBase.Creatures;
using GameBase.Inventorys;
using GameBase.Items;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.UI;
using Instance;
using System;
using XLua;

namespace LuaUtil
{
    [LuaCallCSharp]
    public static class Utils
    {
        public static DynInventory<ItemData> NewItemInventory()
        {
            return new DynInventory<ItemData>();
        }

        public static void RegisterStringActionArg(this Command command, string key, Action<string> action)
        {
            Command.Register(key, action);
        }
    }
}
