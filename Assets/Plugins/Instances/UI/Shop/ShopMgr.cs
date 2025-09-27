using GameBase.Inventorys;
using GameBase.Math;
using GameBase.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Instance
{
    public class ShopMgr
    {
        private static Dictionary<GameObject, Shop> _shopObjs = new();

        public static void Register(Shop shop)
        {
            _shopObjs.Add(shop.obj, shop);
        }

        public static Shop NearestShop(Vector3 postion, float rangeLimit = 2)
        {
            Shop ret = null;
            float minDis = rangeLimit;
            foreach (var kvp in _shopObjs)
            {
                var shopPos = kvp.Key.transform.position;

                if (GMath.GameDistance(postion, shopPos) <= minDis)
                {
                    ret = kvp.Value;
                }
            }

            return ret;
        }

        public static Shop New(ShopInventory model, GameObject obj)
        {
            var shop = new Shop()
            {
                inventory = model,
                obj = obj,
            };
            Register(shop);
            return shop;
        }
    }
}
