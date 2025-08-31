using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class InventoryItem : DetailableBaseItem
    {
        internal int iconImageID = -1;
        internal Image iconImage;

        public int IconImageID
        {
            set
            {
                iconImageID = value;
                var texture = Resources.ResourcesLoader.GetTexture2D(value);
                XLogger.Instance.Log(texture);
                XLogger.Instance.Log(iconImage);
                iconImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
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
