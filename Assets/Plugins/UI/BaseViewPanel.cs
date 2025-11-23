using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class BaseViewPanel<T, T_Panel> : SealedEntitySys<T, T_Panel>
        where T : BaseViewItem, new()
        where T_Panel : BaseViewPanel<T, T_Panel>, new()
    {
        protected internal GameObject panel;
        protected ListContainer<T> _container;
        private Action<int> _OnPointerDown;
        private Action<int> _OnPointerRightDown;
        private Action<int> _OnPointerUp;
        private Action<int> _OnPointerEnter;
        private Action<int> _OnPointerExit;
        protected virtual string ItemPrefabName { get; } = "Prefabs/UI/InventoryItem";
        protected virtual string PanelPrefabName { get; } = "Prefabs/UI/InventoryPanel";
        protected virtual int Layer { get; } = 0;
        protected override bool UseDefaultContainer => false;
        public ILayout Layout { get; set; }
        public Action<int> OnEnterDrag { get; set; }
        public Action<int> OnExitDrag { get; set; }
        public Action<int> OnDrag {  get; set; }
        public Action<int> OnEnterDetail { get; set; }
        public Action<int> OnExitDetail { get; set; }
        public Action<int> OnDetail { get; set; }
        public Action<int> OnPointerDown
        {
            get => _OnPointerDown;
            set
            {
                _OnPointerDown = value;
                foreach (var e in Entities)
                {
                    e.UIScript.OnPointerDown = value;
                }
            }
        }
        public Action<int> OnPointerRightDown
        {
            get => _OnPointerRightDown;
            set
            {
                _OnPointerRightDown = value;
                foreach (var e in Entities)
                {
                    e.UIScript.OnPointerRightDown += value;
                }
            }
        }
        public Action<int> OnPointerUp
        {
            get => _OnPointerUp;
            set
            {
                _OnPointerUp = value;
                foreach (var e in Entities)
                {
                    e.UIScript.OnPointerUp += value;
                }
            }
        }
        public Action<int> OnPointerEnter
        {
            get => _OnPointerEnter;
            set
            {
                _OnPointerEnter = value;
                foreach (var e in Entities)
                {
                    e.UIScript.OnPointerEnter += value;
                }
            }
        }
        public Action<int> OnPointerExit
        {
            get => _OnPointerExit;
            set
            {
                _OnPointerExit = value;
                foreach (var e in Entities)
                {
                    e.UIScript.OnPointerExit += value;
                }
            }
        }

        public BaseViewPanel()
        {
            if (!UseDefaultContainer)
            {
                _container = new ListContainer<T>();
                _sys = new EntitySys<T>(_container)
                {
                    StartAction = EntityStart,
                    UpdateAction = UpdateEntity,
                };
            }

            panel = GameObject.Instantiate(ResourceMgr.Prefab.Get(PanelPrefabName));
            panel.transform.SetParent(RootCanvas.Instance.Layer(Layer), false);
            panel.SetActive(false);
        }

        protected override void OnGet(T e)
        {
            var obj = GameObject.Instantiate(ResourceMgr.Prefab.Get(ItemPrefabName));
            obj.name = ItemPrefabName + Entities.Count;
            obj.transform.SetParent(panel.transform, false);
            e.Obj = obj; 
            var triggerObj = obj.transform.Find("Trigger").gameObject;
            var ui = triggerObj.AddComponent<BaseUI>();
            e.UIScript = ui;
            var image = triggerObj.GetComponent<Image>();
            e.TriggerImage = new SuperImage(image);
            e.TriggerImage.SetHideColor(image.color);
            e.triggerObject = triggerObj;
            e.triggerRectTransform = e.triggerObject.GetComponent<RectTransform>();

            e.UIScript.OnPointerDown = _OnPointerDown;
            e.UIScript.OnPointerEnter = _OnPointerEnter;
            e.UIScript.OnPointerRightDown = _OnPointerRightDown;
            e.UIScript.OnPointerExit = _OnPointerExit;
            e.UIScript.OnPointerUp = _OnPointerUp;
            ViewMgr.RegisterView(e);
        }

        private void DragableUpdate(T e)
        {
            if (!e.InteractiveEnable)
            {
                return;
            }
            if (e.inDragState)
            {
                OnDrag?.Invoke(e.itemIndex);
            }
        }

        private void DetailbleUpdate(T e)
        {
            if (!e.InteractiveEnable)
            {
                return;
            }
            if (e.inDetailState)
            {
                OnDetail?.Invoke(e.itemIndex);
            }
        }

        private void LayoutUpdate(T e)
        {
            if (Layout == null)
            {
                XLogger.Instance.Log($"panel:{GetType()}, layout is null");
                return;
            }

            e.Obj.transform.localPosition = Layout.GetItemLocalPosition(e.itemIndex);
        }

        protected override void UpdateEntity(T e)
        {
            if (e.UIScript.isPointerOn)
            {
                e.UIScript.enterTime += Time.deltaTime;
            }
            if (e.UIScript.isPointerDown)
            {
                e.UIScript.pointerDownTime += Time.deltaTime;
            }
            if (!e.Obj.activeSelf)
            {
                e.UIScript.isPointerOn = false;
                e.UIScript.isPointerDown = false;
                e.UIScript.enterTime = 0;
                e.UIScript.pointerDownTime = 0;
            }
            e.itemIndex = CurrentIterateIndex;

            LayoutUpdate(e);
            DragableUpdate(e);
            DetailbleUpdate(e);
        }

        public virtual void SetLocalPosition(float x, float y)
        {
            panel.transform.localPosition = new Vector3(x, y, 0);
        }

        public virtual bool IsShow => panel.activeSelf;

        public void FillItem(int targetCount)
        {
            int count = Entities.Count;
            if (count >= targetCount)
            {
                return;
            }
            for (int i = 0; i < targetCount - count; i++)
            {
                NewEntity();
            }
        }

        public virtual T this[int index]
        {
            get
            {
                return _container[index];
            }
            set => _container[index] = value;
        }

        /// <summary>
        /// 尝试从panel中获取position位置的UI，和下标，并返回获取结果
        /// </summary>
        /// <param name="triggerPosition"></param>
        /// <param name="e"></param>
        /// <returns>找到后返回结果，否则为null</returns>
        public T GetItemFromTriggerPosition(Vector3 triggerPosition)
        {
            foreach (var e in Entities)
            {
                Rect r = e.RectTransform.rect;
                r.center = e.UIScript.transform.position;
                if (r.Contains(triggerPosition))
                {
                    return e;
                }
            }

            return null;
        }

        public void Swap(int p1, int p2)
        {
            var item1 = this[p1];
            var item2 = this[p2];
            item1.TriggerImage.Swap(item2.TriggerImage);
            (item1.InteractiveEnable, item2.InteractiveEnable) = (item2.InteractiveEnable, item1.InteractiveEnable);
        }

        public void EnterDragState(int index)
        {
            if (!this[index].InteractiveEnable)
            {
                return;
            }
            if (!this[index].inDragState)
            {
                OnEnterDrag?.Invoke(index);
            }
            this[index].inDragState = true;
        }

        public void ExitDragState(int index)
        {
            if (!this[index].InteractiveEnable)
            {
                return;
            }
            if (this[index].inDragState)
            {
                OnExitDrag?.Invoke(index);
            }
            this[index].inDragState = false;
        }

        public void EnterDetailState(int index)
        {
            if (!this[index].InteractiveEnable)
            {
                return;
            }
            if (!this[index].inDetailState)
            {
                OnEnterDetail?.Invoke(index);
            }
            this[index].inDetailState = true;
        }

        public void ExitDetailState(int index)
        {
            if (!this[index].InteractiveEnable)
            {
                return;
            }
            if (this[index].inDetailState)
            {
                OnExitDetail?.Invoke(index);
            }
            this[index].inDetailState = false;
        }

        public virtual void Show()
        {
            panel.SetActive(true);
        }

        public virtual void Hide()
        {
            panel.SetActive(false);
        }

        public void Toggle()
        {
            if (IsShow)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
}
