using GameBase.EntitySystem;
using GameBase.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public abstract class BaseViewItem : IUEntity<BaseUI>
    {
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

        public int ItemIndex => itemIndex;
        public RectTransform RectTransform => rectTransform;
        public BaseUI Obj { get; set; }
        public int ObjID { get; set; }
        public int InstanceID { get; set; }

        public void SetIconSprite(int iconTextureID)
        {
            if (this.iconTextureID == iconTextureID)
            {
                return;
            }
            var texture = ResourcesLoader.GetTexture2D(iconTextureID);
            IconSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        public void SetIconSprite(Sprite iconSprite)
        {
            this.iconSprite = iconSprite;
            iconImage.sprite = iconSprite;
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

        public void ShowColor()
        {
            iconImage.color = colorShow;
        }

        public void HideColor()
        {
            iconImage.color = colorHide;
        }
    }
}
