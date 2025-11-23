using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class BaseViewItem
    {
        protected internal ViewTag tag = ViewTag.None;
        protected internal GameObject triggerObject;
        protected internal RectTransform triggerRectTransform;
        protected internal bool lastClicked;
        protected internal int itemIndex;
        protected internal bool inDragState;
        protected internal bool inDetailState;

        public bool InteractiveEnable { get; set; } = true;
        public GameObject Obj {  get; set; }
        public string IconTextureName { get; set; }
        public SuperImage TriggerImage { get; set; }
        public BaseUI UIScript { get; set; }
        public int ItemIndex => itemIndex;
        public RectTransform RectTransform => triggerRectTransform;
    }
}
