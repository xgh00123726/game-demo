using GameBase.Resources;
using GameBase.UI;
using UnityEngine;

namespace Instance
{
    public class ShopNearView
    {
        internal GameObject nearViewObj;

        public ShopNearView(int nearViewID = 39)
        {
            nearViewObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(nearViewID));
            nearViewObj.transform.SetParent(WorldCanvs.Instance.transform, false);
        }

        public void Show(Shop shop)
        {
            nearViewObj.SetActive(true);
            nearViewObj.transform.position = shop.obj.transform.position;
        }

        public void Hide()
        {
            nearViewObj.SetActive(false);
        }
    }
}
