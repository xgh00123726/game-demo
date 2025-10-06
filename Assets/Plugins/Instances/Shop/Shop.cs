using GameBase.Inventorys;
using UnityEngine;

namespace Instance
{
    public class Shop
    {
        internal bool isActive;
        internal bool isTargetNear;

        public int instanceID;
        public ShopInventory inventory;
        public GameObject obj;
        public float detectRange;
    }
}
