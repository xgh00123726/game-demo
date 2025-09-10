using GameBase.EntitySystem;
using UnityEngine;

namespace GameBase.UI
{
    public abstract class BaseViewItem : IUEntity<BaseUI>
    {
        internal RectTransform rectTransform;
        internal bool lastDrag;
        internal bool lastDetail;
        internal bool lastClicked;
        internal int itemIndex;

        public int ItemIndex => itemIndex;
        public RectTransform RectTransform => rectTransform;
        public BaseUI Obj { get; set; }
        public int ObjID { get; set; }
        public int InstanceID { get; set; }
    }
}
