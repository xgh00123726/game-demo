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

        public static InventoryItem storedItem;

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

        public static void SetIconSprite(InventoryItem other)
        {
            _iconImage.sprite = other.IconSprite;
        }

        public static void Hide()
        {
            _iconImage.gameObject.SetActive(false);
        }
    }
}
