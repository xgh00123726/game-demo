using GameBase.EntitySystem;
using GameBase.Inventorys;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance.UI.MVC
{
    public abstract class MVController<T_ModelItem, T_ViewItem, T_View, T_Controller> : Singleton<T_Controller>, IBaseSys
        where T_ViewItem : BaseViewItem, new()
        where T_View : BaseViewPanel<T_ViewItem, T_View>, new()
        where T_Controller : MVController<T_ModelItem, T_ViewItem, T_View, T_Controller>, new()
    {
        public MVController()
        {
            ShadowMono.CreateShadowMono(this);
        }

        protected abstract IInventoryModel<T_ModelItem> Model { get; }
        protected abstract T_View View { get; }
        protected abstract IDataBase<T_ModelItem> DataBase { get; }

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
            var viewItem = View.FillGet(index);
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
            var viewItem = View.FillGet(index);
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
        /// 尝试获取index位置的物品数据
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
        /// 尝试获取database中index位置的物品数据
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool TryGetDataFromDataBase(int index, out T_ModelItem data)
        {
            if (index >= DataBase.Count)
            {
                data = default;
                return false;
            }
            else
            {
                data = DataBase.Read(index);
                return true;
            }
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
        /// 从数据库中添加物品到背包
        /// <list type="bullet">
        /// <item><param name="dataBaseIndex"><paramref name="dataBaseIndex"/>物品在数据库中的位置</param></item>
        /// <item><param name="index"><paramref name="index"/>需要添加到背包的位置</param></item>
        /// </list></summary>
        public int AddItem(int dataBaseIndex, int index)
        {
            if (TryGetDataFromDataBase(dataBaseIndex, out T_ModelItem item))
            {
                return AddItem(item, index);
            }
            return -1;
        }

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

        public T_ModelItem GetModelItem(int index)
        {
            return Model[index];
        }

        public T_ViewItem GetViewItem(int index)
        {
            return View[index];
        }

        public virtual void Show()
        {
            View.Show();
        }

        public virtual void Hide()
        {
            View.Hide();
        }

        public virtual void Toggle()
        {
            View.Toggle();
        }

        protected virtual void Update() { }

        void IBaseSys.Update()
        {
            
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
    }
}
