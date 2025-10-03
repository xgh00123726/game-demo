using GameBase.Inventorys;
using GameBase.Tools;
using GameBase.UI;
using Instance;

namespace LuaUtil
{
    public static class InventoryUtil
    {
        private static DynInventory<InventoryData> _inventory;

        public static DynInventory<InventoryData> Inventory => _inventory;

        public static void Init()
        {
            _inventory = new DynInventory<InventoryData>();
        }

        public static void SetSize(int size)
        {
            _inventory.Size = size;
        }

        public static int AddItemFromDataBase(int dataBaseIndex)
        {
            return _inventory.Add(InventoryDataBase.Instance[dataBaseIndex]);
        }
    }
}
