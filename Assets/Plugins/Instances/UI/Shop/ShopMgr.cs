using GameBase.Math;
using GameBase.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Instance
{
    public class ShopMgr
    {
        private static Dictionary<GameObject, ShopController> _shopObjs = new();

        public static void Register(GameObject obj, ShopController controller)
        {
            _shopObjs.Add(obj, controller);
        }

        public static ShopController NearestShop(Vector3 postion, float rangeLimit = 2)
        {
            ShopController ret = null;
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
    }
}
