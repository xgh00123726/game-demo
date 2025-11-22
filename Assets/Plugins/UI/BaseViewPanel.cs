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
    public class BaseViewPanel<T> : IBaseSys
        where T : BaseViewItem, new()
    {
        protected internal LinkedList<T> _entityNeedRegister = new LinkedList<T>();
        protected internal LinkedList<T> _entitiesNeedRemove = new LinkedList<T>();
        protected internal bool _inUpdating = false;

        protected internal string itemPrefabName = "Prefabs/UI/InventoryItem";
        protected internal ListContainer<T> container = new();
        protected internal GameObject panel;

        private Action<int> _OnPointerDown;
        private Action<int> _OnPointerRightDown;
        private Action<int> _OnPointerUp;
        private Action<int> _OnPointerEnter;
        private Action<int> _OnPointerExit;
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


        public BaseViewPanel(string prefabName,
            string itemPrefabName)
        {
            SingletonEntitySysInstance.CreateShadowMono(this); 

            panel = GameObject.Instantiate(ResourceMgr.Prefab.Get(prefabName));
            panel.transform.SetParent(RootCanvas.Instance.Layer(0), false);
            panel.SetActive(false);
            this.itemPrefabName = itemPrefabName;
        }

        public virtual IEContainer<T> Entities => container;

        protected virtual GameObject GetGameObject(string name)
        {
            return GameObject.Instantiate(ResourceMgr.Prefab.Get(name));
        }

        protected virtual BaseUI InstantiateObj(T e)
        {
            var obj = GetGameObject(e.PrefabName);
            e.Obj = obj;

            var triggerObj = obj.transform.Find("Trigger").gameObject;
            var ui = triggerObj.AddComponent<BaseUI>();

            var image = triggerObj.GetComponent<Image>();
            e.TriggerImage = new SuperImage(image);
            e.TriggerImage.SetHideColor(image.color);

            e.triggerObject = triggerObj;

            obj.transform.SetParent(panel.transform, false);

            e.triggerRectTransform = e.triggerObject.GetComponent<RectTransform>();

            e.UIScript = ui;

            return ui;
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
                return;
            }

            e.Obj.transform.localPosition = Layout.GetItemLocalPosition(e.itemIndex);
        }

        protected virtual void UpdateEntity(T e)
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

            LayoutUpdate(e);
            DragableUpdate(e);
            DetailbleUpdate(e);
        }

        protected virtual void Update()
        {
            int index = 0;
            foreach (var e in Entities)
            {
                e.itemIndex = index;
                e.UIScript.index = index;
                _inUpdating = true;
                UpdateEntity(e);
                index++;
            }
            foreach (var e in _entitiesNeedRemove)
            {
                Remove(e);
            }
            _entitiesNeedRemove.Clear();
        }

        public T NewEntity(string name = null)
        {
            var e = new T();
            if (name != null)
            {
                e.PrefabName = name;
            }
            else
            {
                e.PrefabName = itemPrefabName;
            }
            return NewEntity(e);
        }

        public virtual T NewEntity(T e)
        {
            Entities.Add(e);
            e.UIScript = InstantiateObj(e);
            e.UIScript.OnPointerDown = _OnPointerDown;
            e.UIScript.OnPointerEnter = _OnPointerEnter;
            e.UIScript.OnPointerRightDown = _OnPointerRightDown;
            e.UIScript.OnPointerExit = _OnPointerExit;
            e.UIScript.OnPointerUp = _OnPointerUp;
            ViewMgr.RegisterView(e);
            return e;
        }

        /// <summary>
        /// 将实体标记为删除
        /// </summary>
        public void RemoveEntity(T e)
        {
            if (e == null) return;

            if (_entitiesNeedRemove.Contains(e))
            {
                return;
            }

            if (!_inUpdating)
            {
                Remove(e);
            }
            else
            {
                _entitiesNeedRemove.AddLast(e);
            }
        }

        protected virtual void Remove(T e)
        {
            Entities.Remove(e);
            ViewMgr.RemoveView(e);
        }

        public virtual void SetLocalPosition(float x, float y)
        {
            panel.transform.localPosition = new Vector3(x, y, 0);
        }

        public virtual bool IsShow => panel.activeSelf;

        public void FillItem(int targetCount)
        {
            int count = container.Count;
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
            get => container[index];
            set => container[index] = value;
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

        public void AddChild(GameObject child)
        {
            child.transform.SetParent(panel.transform, false);
        }

        public Transform FindChild(string name)
        {
            return panel.transform.Find(name);
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

        public SuperImage GetItemImage(int index)
        {
            return this[index].TriggerImage;
        }

        void IBaseSys.Update()
        {
            Update();
        }

        void IBaseSys.FixedUpdate()
        {
            
        }

        int IBaseSys.GetEntityCount()
        {
            return Entities.Count;
        }

        int IBaseSys.GetReleasedCount()
        {
            return 0;
        }

        int IBaseSys.GetActiveCount()
        {
            return 0;
        }
    }
}
