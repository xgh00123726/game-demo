using GameBase.Inventorys;
using GameBase.Modify;
using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using Instance;
using Instance.UI.Shops;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace LuaUtil
{
    public static class ShopUtil
    {
        private static Shop _currShop;
        public static Shop CurrentShop => _currShop;

        private static void OnPointerDown(int i)
        {
            var inventory = _currShop.inventory;

            var _shopper = PlayerUtil.Player;
            var shopInfo = inventory.GetItemInfoOfShoppingPosition(i);
            if (shopInfo.type == ShopItemType.Buff)
            {
                _shopper.AddBuff(shopInfo.id);
            }
            else if (shopInfo.type == ShopItemType.InventoryItem)
            {
                InventoryUtil.Inventory.Add(InventoryDataBase.Instance[shopInfo.id]);
            }
            else if (shopInfo.type == ShopItemType.Modifier)
            {
                var modifyInfo = ModifierDataBase.Instance[shopInfo.id];
                var modifier = ModifyerSys.Instance.NewEntity();
                modifier.value = modifyInfo.value;
                modifier.type = modifyInfo.type1 | modifyInfo.type2;
                _shopper.Modifyables.ModifySet(modifyInfo.key, modifier);
            }

            inventory.Remove(i);
            //_panel.UpdateItem(inventory, i);
        }

        public static void Init()
        {
            
        }

        public static void RemoveShopItem(int shopID, int index)
        {
            var shop = ShopMgr.GetShop(shopID);
            if (shop == null)
            {
                return;
            }
            shop.inventory.Remove(index);
        }

        public static ShopItemInfo GetItemInfo(int shopID, int itemPosition)
        {
            var shop = ShopMgr.GetShop(shopID);
            if (shop == null)
            {
                return default;
            }
            else
            {
                return shop.inventory.GetItemInfoOfShoppingPosition(itemPosition);
            }
        }

        public static void RefreshShop(int shopID)
        {
            var shop = ShopMgr.GetShop(shopID);
            if (shop != null)
            {
                shop.inventory.Refresh();
            }
        }

        public static int GetShopNearestCreature(int creatureID, float rangeLimit)
        {
            var c = CreatureSys.Instance.GetCreature(creatureID);

            var s = ShopMgr.NearestShop(c.Position, rangeLimit);

            if (s == null)
            {
                return -1;
            }
            else
            {
                return s.instanceID;
            }
        }

        public static void SetGoodsNum(int shopID, int size)
        {
            var shop = ShopMgr.GetShop(shopID);
            shop.inventory.Size = size; 
        }

        public static int Gen(string dataFile, int prefabID, float x, float z)
        {
            var inventory = new ShopInventory(dataFile);

            var shopObject = GameObject.Instantiate(ResourcesLoader.GetPrefab(prefabID));
            shopObject.transform.position = new Vector3(x, -7, z);

            var shop = ShopMgr.New(inventory, shopObject);

            return 0;
        }
    }
}
