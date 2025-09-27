using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class DefaultDragableView
    {
        public static GameObject shadowObj;
        public static SuperImage iconImage;

        static DefaultDragableView()
        {
            shadowObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(37));

            shadowObj.transform.SetParent(RootCanvas.Instance.Layer(1), false);

            var image = shadowObj.transform.Find("Trigger").GetComponent<Image>();

            iconImage = new SuperImage(image);
            iconImage.SetHideColor(image.color);
        }

        public static void SetPosition(Vector3 position)
        {
            shadowObj.SetActive(true);
            shadowObj.transform.position = position;
        }

        public static void Hide()
        {
            shadowObj.SetActive(false);
        }
    }
}
