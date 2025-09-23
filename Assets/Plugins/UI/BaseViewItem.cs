using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public abstract class BaseViewItem : IUEntity<BaseUI>
    {
        internal ViewTag tag;

        internal GameObject iconObject;
        internal Image iconImage;
        internal Sprite iconSprite;
        internal RectTransform rectTransform;
        internal bool lastDrag;
        internal bool lastDetail;
        internal bool lastClicked;
        internal int itemIndex;
        internal int iconTextureID;

        public Color colorHide;
        public Color colorShow;

        public int IconTextureID => iconTextureID;
        public int ItemIndex => itemIndex;
        public RectTransform RectTransform => rectTransform;
        public BaseUI Obj { get; set; }
        public int ObjID { get; set; }

        public Sprite InstantiateSpriteFromIconTextureID(int iconTextureID)
        {
            var texture = ResourcesLoader.GetTexture2D(iconTextureID);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }


        public void SetIconSprite(int iconTextureID)
        {
            if (this.iconTextureID == iconTextureID)
            {
                return;
            }

            this.iconTextureID = iconTextureID;
            
            if (iconTextureID < 0)
            {
                iconSprite = null;
                return;
            }

            IconSprite = InstantiateSpriteFromIconTextureID(iconTextureID);
        }

        public void HideIcon()
        {
            iconImage.sprite = null;
        }

        public void ShowIcon()
        {
            iconImage.sprite = iconSprite;
        }

        public void SwapIconSprite(BaseViewItem other)
        {
            (iconSprite, other.iconSprite) = (other.iconSprite, iconSprite);
            (colorShow, other.colorShow) = (other.colorShow, colorShow);
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

        public void SetIconColor(int rarity)
        {
            iconImage.color = ViewConfig.GetColor(rarity);
        }

        public void SetIconColor(Color color)
        {
            iconImage.color = color;
        }

        public void HideColor()
        {
            iconImage.color = colorHide;
        }
    }
}
