using GameBase.Resources;
using GameBase.UI;
using UnityEngine;

namespace Instance
{
    public class ShopNearView
    {
        internal GameObject nearViewObj;

        public ShopNearView(string prefabName = "Prefabs/UI/ShopNearView.prefab")
        {
            nearViewObj = GameObject.Instantiate(ResourceMgr.Prefab.Get(prefabName));
            nearViewObj.transform.SetParent(WorldCanvs.Instance.transform, false);
        }

        public void Show(Shop shop)
        {
            nearViewObj.SetActive(true);
            nearViewObj.transform.position = shop.Obj.transform.position;
        }

        public void Hide()
        {
            nearViewObj.SetActive(false);
        }
    }
}
