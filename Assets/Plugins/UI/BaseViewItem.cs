using GameBase.EntitySystem;
using UnityEngine;

namespace GameBase.UI
{
    public abstract class BaseViewItem : IUEntity<BaseUI>
    {
        internal RectTransform rectTransform;
        internal Texture2D iconTexture;
        internal bool lastDrag;

        public Texture2D IconTexture => iconTexture;
        public RectTransform RectTransform => rectTransform;
        internal abstract int IconTexureID { get; }
        public BaseUI Obj { get; set; }
        public int ObjID { get; set; }
        public int InstanceID { get; set; }
    }
}
