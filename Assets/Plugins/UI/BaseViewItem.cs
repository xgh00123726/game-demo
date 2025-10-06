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

        public bool interactiveEnable = true;
        public int objID;
        public GameObject obj;
        public int iconTextureID;
        public SuperImage triggerImage;
        public BaseUI uiScript;

        public int ItemIndex => itemIndex;
        public RectTransform RectTransform => triggerRectTransform;
    }
}
