using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class InventoryItem : DetailableBaseItem
    {
        public float dragableJugTime = 1f;
        public RectTransform rectTransform;
        internal GameObject iconObject;
        internal int iconImageID = -1;
        public Sprite iconSprite;
        internal Image iconImage;
        internal bool lastDrag;
        public IDragable<InventoryItem> dragable;

        public int IconImageID
        {
            set
            {
                iconImageID = value;
                var texture = Resources.ResourcesLoader.GetTexture2D(value);
                iconSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                iconImage.sprite = iconSprite;
            }
        }

        public void HideIcon()
        {
            iconImage.sprite = null;
        }

        public void ShowIcon()
        {
            iconImage.sprite = iconSprite;
        }

        public void SwapIconImage(InventoryItem other)
        {
            if (iconImageID == other.iconImageID)
            {
                return;
            }
            var t = other.iconImage;
            other.iconImage = iconImage;
            iconImage = t;
        }

        internal override int IconTexureID => -1;

        public InventoryItem()
        {
            ObjID = 34;
        }
    }
}
