using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Instance.Inventory
{
    public class InventoryShadowItem
    {
        private static Image _iconImage;

        private static InventoryViewItem storedItem;

        static InventoryShadowItem()
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(37));

            obj.transform.SetParent(RootCanvas.Instance.transform, false);

            _iconImage = obj.transform.Find("Icon").GetComponent<Image>();
            if (_iconImage == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }
        }

        public static void SetPosition(Vector3 position)
        {
            _iconImage.gameObject.SetActive(true);
            _iconImage.transform.position = position;
        }

        public static void StoreItem(InventoryViewItem e)
        {
            storedItem = e;
            _iconImage.sprite = e.IconSprite;
        }

        public static InventoryViewItem StorePop()
        {
            return storedItem;
        }

        public static void Hide()
        {
            _iconImage.gameObject.SetActive(false);
        }
    }
}
