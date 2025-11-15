using GameBase.Creatures;
using GameBase.Inventorys;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.UI;
using Instance;
using XLua;

namespace LuaUtil
{
    [LuaCallCSharp]
    public static class Utils
    {
        public static DynInventory<InventoryData> NewInstance()
        {
            return new DynInventory<InventoryData>();
        }
        public static bool TryEquipInventoryItem(this Creature c, InventoryData data, int index)
        {
            if (data.type == InventoryItemType.Equipment)
            {
                return c.AddEquipment(data.reflectedID, index);
            }

            return false;
        }
    }
}
