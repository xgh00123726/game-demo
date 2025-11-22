using GameBase.Inventorys;
using UnityEngine;

namespace Instance
{
    public class Shop
    {
        internal bool isActive;
        internal bool isTargetNear;

        public int InstanceID { get; set; }
        public ShopInventory Inventory { get; set; }
        public GameObject Obj { get; set; }
        public float DetectRange { get; set; }
    }
}
