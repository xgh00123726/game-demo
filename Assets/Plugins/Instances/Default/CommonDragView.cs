using GameBase.Resources;
using GameBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Instance
{
    public class CommonDragView
    {
        public GameObject obj;
        public SuperImage triggerImage;

        public CommonDragView(string prefabName = "Prefabs/UI/InventoryShadowItem")
        {
            obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(prefabName));

            obj.transform.SetParent(RootCanvas.Instance.Layer(1), false);

            var image = obj.transform.Find("Trigger").GetComponent<Image>();

            triggerImage = new SuperImage(image);
            triggerImage.SetHideColor(image.color);

            Hide();
        }

        public void SetPosition(Vector3 position)
        {
            obj.transform.position = position;
        }

        public void Show()
        {
            obj.SetActive(true);
        }

        public void Hide()
        {
            obj.SetActive(false);
        }
    }
}
