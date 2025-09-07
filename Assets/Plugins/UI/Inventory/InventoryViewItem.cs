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

        public Color colorHide;
        public Color colorShow;
        public int iconTextureID;

        public Sprite IconSprite
        {
            get => iconSprite;
            set
            {
                iconSprite = value;
                iconImage.sprite = iconSprite;
            }
        }

        public void ShowColor()
        {
            iconImage.color = colorShow;
        }

        public void HideColor()
        {
            iconImage.color = colorHide;
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
            (colorShow, other.colorShow) = (other.colorShow, colorShow);
        }
    }
}
