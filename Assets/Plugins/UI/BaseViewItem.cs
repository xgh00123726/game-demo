using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class BaseViewItem : ITodoView, ITodoTarget
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
        public SuperImage TriggerImage { get; set; }
        public BaseUI UIScript { get; set; }
        public int ItemIndex => itemIndex;
        public RectTransform RectTransform => triggerRectTransform;

        public bool IsShow
        {
            get => Obj.activeSelf;
            set => Obj.SetActive(value);
        }

        public Vector3 Position
        {
            get => Obj.transform.position;
            set => Obj.transform.position = value;
        }
    }
}
