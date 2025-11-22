using GameBase.Resources;
using GameBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Instance
{
    public class CommonDragView
    {
        public GameObject Obj { get; set; }
        public SuperImage TriggerImage { get; set; }

        public CommonDragView(string prefabName = "Prefabs/UI/InventoryShadowItem")
        {
            Obj = GameObject.Instantiate(ResourceMgr.Prefab.Get(prefabName));

            Obj.transform.SetParent(RootCanvas.Instance.Layer(1), false);

            var image = Obj.transform.Find("Trigger").GetComponent<Image>();

            TriggerImage = new SuperImage(image);
            TriggerImage.SetHideColor(image.color);

            Hide();
        }

        public void SetPosition(Vector3 position)
        {
            Obj.transform.position = position;
        }

        public void Show()
        {
            Obj.SetActive(true);
        }

        public void Hide()
        {
            Obj.SetActive(false);
        }
    }
}
