using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using Newtonsoft.Json.Linq;
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
        private bool _inUpdating = false;

        internal int defaultObjID = 34;
        internal ListContainer<T> container = new();
        internal GameObject panel;

        public ILayout layout;
        public IDetailableControl<T> detailableControl;
        public IEnterExitControl<T> enterExitControl;
        public IDragableControl<T> dragableControl;
        

        public BaseViewPanel(int prefabID,
            int defaultObjID)
        {
            ShadowMono.CreateShadowMono(this); 

            panel = GameObject.Instantiate(ResourcesLoader.GetPrefab(prefabID));
            panel.transform.SetParent(RootCanvas.Instance.Layer(0), false);
            panel.SetActive(false);

            this.defaultObjID = defaultObjID;
        }

        protected virtual RectTransform GetRectTransform(T e)
        {
            return e.iconImage.GetComponent<RectTransform>();
        }

        public virtual IEContainer<T> Entities => container;

        protected virtual BaseUI InstantiateObj(T e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));

            var ui = obj.AddComponent<BaseUI>();
            ui.enterAction = () => enterExitControl?.OnPointerEnter(e);
            ui.exitAction = () => enterExitControl?.OnPointerExit(e);
            ui.pointerDownAction = () => enterExitControl?.OnPointerDown(e);
            ui.pointerRightDownAction = () => enterExitControl?.OnPointerRightDown(e);

            var iconObj = obj.transform.Find("Icon");

            if (iconObj != null) 
            {
                e.iconImage = iconObj.GetComponent<Image>();
            }

            e.Obj = ui;

            obj.transform.SetParent(panel.transform, false);

            e.rectTransform = GetRectTransform(e);

            e.colorHide = e.iconImage.color;

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
                dragableControl.OnExitDrag();
            }

            if (isDrag)
            {
                dragableControl.OnDrag();
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

        private void EnterExitUpdate(T e)
        {
            e.lastClicked = e.Obj.isPointerDown;

            if (enterExitControl == null) return;
        }

        private void LayoutUpdate(T e)
        {
            if (layout == null)
            {
                return;
            }

            e.Obj.transform.localPosition = layout.GetItemLocalPosition(e.itemIndex);
        }

        protected virtual void UpdateEntity(T e)
        {
            if (e.Obj.isPointerOn)
            {
                e.Obj.enterTime += Time.deltaTime;
            }
            if (e.Obj.isPointerDown)
            {
                e.Obj.pointerDownTime += Time.deltaTime;
            }

            LayoutUpdate(e);
            DragableUpdate(e);
            DetailbleUpdate(e);
            EnterExitUpdate(e);
        }

        protected virtual void Update()
        {
            int index = 0;
            foreach (var e in Entities)
            {
                e.itemIndex = index;
                UpdateEntity(e);
                index++;
            }
        }

        public virtual T NewEntity(int objID = -1)
        {
            var e = new T();
            if (objID > 0)
            {
                e.ObjID = objID;
            }
            else
            {
                e.ObjID = defaultObjID;
            }
            return NewEntity(e);
        }

        public virtual T NewEntity(T e)
        {
            Entities.Add(e);
            e.Obj = InstantiateObj(e);
            ViewManager.RegisterView(e);
            return e;
        }

        ///// <summary>
        ///// 将实体标记为删除
        ///// </summary>
        //protected void RemoveEntity(T e)
        //{
        //    if (e == null) return;

        //    if (_entitiesNeedRemove.Contains(e))
        //    {
        //        return;
        //    }

        //    if (!_inUpdating)
        //    {
        //        Entities.Remove(e);
        //    }
        //    else
        //    {
        //        _entitiesNeedRemove.AddLast(e);
        //    }
        //}

        public virtual void RemoveEntity(T e)
        {
            Entities.Remove(e);
            ViewManager.RemoveView(e);
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
        /// <param name="position"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool TryGetItem(Vector3 position, out T e, out int index)
        {
            index = 0;
            foreach (var ie in Entities)
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

        public void SwapIconSprite(int p1, int p2)
        {
            this[p1].SwapIconSprite(this[p2]);
        }

        public void AddChild(GameObject child)
        {
            child.transform.SetParent(panel.transform, false);
        }

        public Transform FindChild(string name)
        {
            return panel.transform.Find(name);
        }

        public void Show()
        {
            panel.SetActive(true);
        }

        public void Hide()
        {
            panel.SetActive(false);
        }

        public void Toggle()
        {
            panel.SetActive(!panel.activeSelf);
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
