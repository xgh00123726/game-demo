using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class ShopViewItem : BaseViewItem
    {
        internal GameObject iconObject;
        internal Image iconImage;
        internal Sprite iconSprite;

        public int iconTextureID;

        public ShopViewItem()
        {
            ObjID = 40;
        }

        public Sprite IconSprite
        {
            get => iconSprite;
            set
            {
                iconSprite = value;
                iconImage.sprite = iconSprite;
            }
        }
    }
}
