using GameBase.Inventorys;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance.MVC
{
    public abstract class MVController<T_ModelItem, T_ViewItem, T_View, T_Controller> : Singleton<T_Controller>
        where T_ViewItem : BaseViewItem, new()
        where T_View : BaseViewPanel<T_ViewItem, T_View>, new()
        where T_Controller : MVController<T_ModelItem, T_ViewItem, T_View, T_Controller>, new()
    {
        protected abstract IInventoryModel<T_ModelItem> Model { get; }
        protected abstract T_View View { get; }
        protected abstract IDataBase<T_ModelItem> DataBase { get; }

        protected abstract void SetIcon(T_ModelItem modelData, T_ViewItem viewItem);

        public virtual int AddItem(T_ModelItem item)
        {
            var index = Model.AddItem(item);
            var viewItem = View[index];
            SetIcon(item, viewItem);
            return index;
        }

        public virtual int AddItem(T_ModelItem item, int index)
        {
            Model.AddItem(item, index);
            var viewItem = View[index];
            SetIcon(item, viewItem);
            return index;
        }

        public virtual void RemoveItem(int position)
        {
            Model.RemoveItem(position);
        }

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
    }
}
