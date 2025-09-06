using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public abstract class BaseViewPanel<T, T_Instance> : UObjEntitySys<T, BaseUI, T_Instance>
        where T : BaseViewItem, new()
        where T_Instance : BaseViewPanel<T, T_Instance>, new()
    {
        public enum Align
        {
            Left = 0, Right, Center
        }

        internal IDragableControl<T> dragableControl;
        internal IEnterExist<T> enterExist;
        internal ISwitchable<T> switchable;
        internal IDetailableControl<T> detailableControl;

        public IDragableControl<T> DragableControl
        {
            get => dragableControl;
            set => dragableControl = value;
        }

        public IEnterExist<T> EnterExist
        {
            get => enterExist;
            set => enterExist = value;
        }

        public ISwitchable<T> Switchable
        {
            get => switchable;
            set => switchable = value;
        }

        public IDetailableControl<T> DetailableControl
        {
            get => detailableControl;
            set => detailableControl = value;
        }

        public GameObject panel;
        private int itemIterIdx = 0;

        internal abstract float ItemWidth { get; }
        internal abstract float ItemHeight { get; }
        internal abstract float XInterval { get; }
        internal abstract float YInterval { get; }
        internal abstract float MaxPanelWidth { get; }
        internal abstract float PanelX { get; }
        internal abstract float PanelY { get; }
        internal abstract int PanelObjID {  get; }
        internal abstract int ShapeTexureID { get; }
        internal abstract int ContourTexureID { get; }
        internal abstract int ItemAlign { get; }
        internal int ItemIterIdx => itemIterIdx;

        protected virtual RectTransform GetRectTransform(T e)
        {
            return null;
        }

        protected virtual void GetItemNumXYStyle(int index, out int itemPerLine, out int x, out int y)
        {
            itemPerLine = (int)Mathf.Floor(MaxPanelWidth / (ItemWidth + XInterval));
            x = index % itemPerLine;
            y = index / itemPerLine;
        }

        protected virtual Vector3 GetItemLocalPosition(int index)
        {
            GetItemNumXYStyle(index, out int itemPerLine, out int x, out int y);

            float vx = 0f;
            if (ItemAlign == (int)Align.Left)
            {
                vx = (ItemWidth + XInterval) * x;
            }
            else if (ItemAlign == (int)Align.Center)
            {
                vx = (ItemWidth + XInterval) * x;
                float remainWidth = itemPerLine * (ItemWidth + XInterval);
                vx -= remainWidth / 2;
            }
            float vy = (ItemHeight + YInterval) * y;


            return new Vector3(vx, vy, 0);
        }

        protected override void AfterInstantiateEUObject(T e)
        {            
            e.Obj.gameObject.SetActive(true);
        }

        protected override void BeforeReleaseEUObject(T e)
        {
            e.Obj.gameObject.SetActive(false);
        }

        protected override BaseUI InstantiateObj(T e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));

            var ui = obj.AddComponent<BaseUI>();
            ui.enterAction = () => enterExist?.OnPointerEnter(e);
            ui.exitAction = () => enterExist?.OnPointerExit(e);

            e.Obj = ui;

            obj.transform.SetParent(panel.transform, false);

            e.rectTransform = GetRectTransform(e);

            return ui;
        }

        private void DragableUpdate(T e)
        {
            if (dragableControl == null) return;   

            var isDrag = dragableControl.IsDrag(e);
            if (isDrag && !e.lastDrag)
            {
                dragableControl.OnEnterDrag(e);
            }
            else if (!isDrag && e.lastDrag)
            {
                dragableControl.OnExitDrag(e);
            }

            if (isDrag)
            {
                dragableControl.OnDrag(e);
            }

            e.lastDrag = isDrag;
        }

        private void DetailbleUpdate(T e)
        {
            if (detailableControl == null) return;

            var isDetail = detailableControl.IsDetail(e);
            if (isDetail && !e.lastDetail)
            {
                detailableControl.OnEnterDetail(e);
            }
            else if (!isDetail && e.lastDetail)
            {
                detailableControl.OnExitDetail(e);
            }

            if (isDetail)
            {
                detailableControl.OnDetail(e);
            }

            e.lastDetail = isDetail;
        }

        protected override void UpdateEntity(T e)
        {
            panel.transform.localPosition = new Vector3(PanelX, PanelY, 0);
            e.Obj.transform.localPosition = GetItemLocalPosition(itemIterIdx);

            if (e.Obj.isPointerOn)
            {
                e.Obj.enterTime += Time.deltaTime;
            }
            if (e.Obj.isPointerDown)
            {
                e.Obj.pointerDownTime += Time.deltaTime;
            }

            DragableUpdate(e);
            DetailbleUpdate(e);

            if (++itemIterIdx >= _entities.Count)
            {
                itemIterIdx = 0;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            panel = GameObject.Instantiate(ResourcesLoader.GetPrefab(PanelObjID));
            panel.transform.SetParent(RootCanvas.Instance.transform, false);
        }

        /// <summary>
        /// 尝试从panel中获取position位置的UI，和下标，并返回获取结果
        /// </summary>
        /// <param name="position"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool TryGetItem(Vector3 position, out T e, out int index)
        {
            index = 0;
            foreach (var ie in _entities)
            {
                Rect r = ie.RectTransform.rect;
                r.center = ie.Obj.transform.position;
                if (r.Contains(position))
                {
                    e = ie;
                    return true;
                }
                index++;
            }

            e = null;
            return false;
        }
    }
}
