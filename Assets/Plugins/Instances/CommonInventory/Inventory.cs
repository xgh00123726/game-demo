using GameBase.Inventorys;
using GameBase.Items;

namespace Instance
{
    public class Inventory
    {
        private static DynInventory<int> _commonInventory = new();
        public static DynInventory<int> Common => _commonInventory;

        public static DynInventory<int> _serverInventory = new();
        public static DynInventory <int> Server => _serverInventory;
    }
}
