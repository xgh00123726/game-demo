using GameBase.Resources;
using GameBase.UI;
using GameBase.Shops;
using UnityEngine;
using GameBase.Tools;

namespace Instance.Shops
{
    public class NearView : INearView
    {
        internal GameObject nearViewObj;

        public NearView(int nearViewID = 39)
        {
            nearViewObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(nearViewID));
            nearViewObj.transform.SetParent(WorldCanvs.Instance.transform, false);
        }

        void INearView.Show(Vector3 positon)
        {
            nearViewObj.SetActive(true);
            nearViewObj.transform.position = positon;
        }

        void INearView.Hide()
        {
            nearViewObj.SetActive(false);
        }
    }
}
