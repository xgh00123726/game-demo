using GameBase.EntitySystem;
using GameBase.Inventorys;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace GameBase.UI.MVC
{
    public abstract class MVController<T_ModelItem, T_ViewItem, T_View, T_Controller> : Singleton<T_Controller>, IBaseSys
        where T_ViewItem : BaseViewItem, new()
        where T_View : BaseViewPanel<T_ViewItem>
        where T_Controller : MVController<T_ModelItem, T_ViewItem, T_View, T_Controller>, new()
    {
        public MVController()
        {
            ShadowMono.CreateShadowMono(this);
        }

        public abstract IMVCModel<T_ModelItem> Model { get; }
        public abstract T_View View { get; }

        protected abstract void SetItem(T_ModelItem modelData, T_ViewItem viewItem);

        protected virtual void SetNullItem(T_ModelItem modelData, T_ViewItem viewItem) { }

        public int Size
        {
            get => Model.Size;
            set
            {
                Model.Size = value;
                View.FillItem(value);
            }
        }

        /// <summary>
        /// 向controller中添加一个物品，被添加的位置总是第一个空位
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual int AddItem(T_ModelItem item)
        {
            var index = Model.AddItem(item);
            View.FillItem(index + 1);
            var viewItem = View[index];
            SetItem(item, viewItem);
            return index;
        }

        /// <summary>
        /// 向指定位置中添加物品
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual int AddItem(T_ModelItem item, int index)
        {
            Model.AddItem(item, index);
            View.FillItem(index + 1);
            var viewItem = View[index];
            SetItem(item, viewItem);
            return index;
        }

        /// <summary>
        /// 移除指定位置的物品
        /// </summary>
        /// <param name="position"></param>
        public virtual void RemoveItem(int position)
        {
            Model.RemoveItem(position);
        }

        /// <summary>
        /// 尝试从model中获取index位置的物品数据
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool TryGetData(int index, out T_ModelItem data)
        {
            var ret = Model.HasItem(index);

            if (ret)
            {
                data = Model[index];
            }
            else
            {
                data = default;
            }

            return ret;
        }

        /// <summary>
        /// 尝试获取position位置（屏幕空间）的UI和位置
        /// </summary>
        /// <param name="position"></param>
        /// <param name="e"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool TryGetItemUI(Vector3 position, out T_ViewItem e, out int index)
        {
            return View.TryGetItem(position, out e, out index);
        }

        /// <summary>
        /// 强制刷新View
        /// </summary>
        public virtual void ForceRefreshView()
        {
            for (int i = 0; i < Model.Size; i++)
            {
                if (Model.HasItem(i))
                {
                    SetItem(Model[i], View[i]);
                }
                else
                {
                    SetNullItem(Model[i], View[i]);
                }
            }
        }

        /// <summary>
        /// 获取index处的model数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T_ModelItem GetModelItem(int index)
        {
            return Model[index];
        }

        public bool HasItem(int index)
        {
            return Model.HasItem(index);
        }

        /// <summary>
        /// 获取index处view数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T_ViewItem GetViewItem(int index)
        {
            return View[index];
        }

        /// <summary>
        /// 显示视图
        /// </summary>
        public virtual void Show()
        {
            View.Show();
        }

        /// <summary>
        /// 隐藏视图
        /// </summary>
        public virtual void Hide()
        {
            View.Hide();
        }

        /// <summary>
        /// 切换视图可见性
        /// </summary>
        public virtual void Toggle()
        {
            View.Toggle();
        }

        protected virtual void Update() { }

        void IBaseSys.Update()
        {
            Update();
        }

        int IBaseSys.GetEntityCount()
        {
            return Model.Size;
        }

        int IBaseSys.GetReleasedCount()
        {
            return 0;
        }

        int IBaseSys.GetActiveCount()
        {
            return View.Entities.Count;
        }

        void IBaseSys.FixedUpdate()
        {
            
        }
    }
}
