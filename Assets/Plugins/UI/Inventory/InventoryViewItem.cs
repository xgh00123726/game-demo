using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class InventoryViewItem : BaseViewItem
    {
        internal GameObject iconObject;
        internal Image iconImage;
        internal Sprite iconSprite;

        public InventoryViewItem()
        {
            ObjID = 34;
        }

        internal override int IconTexureID => 0;

        public Sprite IconSprite
        {
            get => iconSprite;
            set
            {
                iconSprite = value;
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

        public void SwapIconSprite(InventoryViewItem other)
        {
            (iconSprite, other.iconSprite) = (other.iconSprite, iconSprite);
        }
    }
}
