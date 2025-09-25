using GameBase.Resources;
using GameBase.UI;
using UnityEngine;

namespace Instance.UI.Shops
{
    public class ShopNearView
    {
        internal GameObject nearViewObj;

        public ShopNearView(int nearViewID = 39)
        {
            nearViewObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(nearViewID));
            nearViewObj.transform.SetParent(WorldCanvs.Instance.transform, false);
        }

        public void Show(Vector3 positon)
        {
            nearViewObj.SetActive(true);
            nearViewObj.transform.position = positon;
        }

        public void Hide()
        {
            nearViewObj.SetActive(false);
        }
    }
}
