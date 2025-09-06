using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Instance.Inventory
{
    public class InventoryShadowView
    {
        private static GameObject _shadowObj;
        private static Image _iconImage;

        private static InventoryViewItem storedItem;

        static InventoryShadowView()
        {
            _shadowObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(37));

            _shadowObj.transform.SetParent(RootCanvas.Instance.transform, false);

            _iconImage = _shadowObj.transform.Find("Icon").GetComponent<Image>();
            if (_iconImage == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }
        }

        public static void SetPosition(Vector3 position)
        {
            _shadowObj.SetActive(true);
            _shadowObj.transform.position = position;
        }

        public static void Hide()
        {
            _shadowObj.SetActive(false);
        }

        public static void StorePush(InventoryViewItem e)
        {
            storedItem = e;
            _iconImage.sprite = e.IconSprite;
        }

        public static InventoryViewItem StorePop()
        {
            return storedItem;
        }
    }
}
