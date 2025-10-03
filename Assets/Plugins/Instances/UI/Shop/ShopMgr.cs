using GameBase.Inventorys;
using GameBase.Math;
using GameBase.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Instance
{
    public class ShopMgr
    {
        private static List<Shop> _shops = new();

        public static Shop GetShop(int id)
        {
            if (id >= _shops.Count)
            {
                return null;
            }

            return _shops[id];
        }

        public static Shop NearestShop(Vector3 postion, float rangeLimit = 2)
        {
            Shop ret = null;
            float minDis = rangeLimit;
            foreach (var shop in _shops)
            {
                var shopPos = shop.obj.transform.position;

                if (GMath.GameDistance(postion, shopPos) <= minDis)
                {
                    ret = shop;
                }
            }

            return ret;
        }

        public static Shop New(ShopInventory model, GameObject obj)
        {
            var shop = new Shop()
            {
                instanceID = _shops.Count,
                inventory = model,
                obj = obj,
            };
            _shops.Add(shop);
            return shop;
        }
    }
}
