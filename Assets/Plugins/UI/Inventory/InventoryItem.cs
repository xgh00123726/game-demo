using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class InventoryItem : BasePanelItem
    {
        internal GameObject iconObject;
        internal int iconImageID;
        internal Image iconImage;

        public Sprite iconSprite;

        public InventoryItem()
        {
            ObjID = 34;
        }

        internal override int IconTexureID => 0;
        
        
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

        public Sprite IconSprite => iconSprite;

        public void HideIcon()
        {
            iconImage.sprite = null;
        }

        public void ShowIcon()
        {
            iconImage.sprite = iconSprite;
        }

        public void SwapIconSprite(InventoryItem other)
        {
            (iconSprite, other.iconSprite) = (other.iconSprite, iconSprite);
        }
    }
}
