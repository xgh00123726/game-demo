using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using Instance.Inventory;
using UnityEngine;
using UnityEngine.UIElements;

namespace Instance.Inventory
{
    public abstract class InventoryController<T_ModelItem, T_ViewItem, T_View, T_Controller>
        where T_ModelItem : struct, IModelItem
        where T_ViewItem : InventoryViewItem, new()
        where T_View : InventoryViewPanel<T_ViewItem, T_View>, new()
        where T_Controller : InventoryController<T_ModelItem, T_ViewItem, T_View, T_Controller>, new()
    {
        private static T_Controller _controller = new();
        public static T_Controller Instance =>_controller;

        protected abstract InventoryModel<T_ModelItem> Model { get; }
        protected abstract T_View View { get; }
        protected abstract IDataBase<T_ModelItem> DataBase { get; }

        protected abstract void SetIcon(T_ModelItem modelData, T_ViewItem viewItem);
        protected virtual void OnSwap(int p1, int p2) { }

        private void SetColor(int index)
        {
            if (Model.HasItem(index))
            {
                View[index].ShowColor();
            }
            else
            {
                View[index].HideColor();
            }
        }

        public int IndexOfView(T_ViewItem vitem)
        {
            return View.IndexOf(vitem);
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

        public void AddItem(T_ModelItem item)
        {
            var index = Model.AddItem(item);
            var viewItem = View[index];
            SetIcon(item, viewItem);
            SetColor(index);
        }

        /// <summary>
        /// 从数据库中添加物品到背包
        /// <list type="bullet">
        /// <item><param name="dataBaseIndex"><paramref name="dataBaseIndex"/>物品在数据库中的位置</param></item>
        /// <item><param name="index"><paramref name="index"/>需要添加到背包的位置</param></item>
        /// </list></summary>
        public void AddItem(int dataBaseIndex, int index)
        {
            if (TryGetDataFromDataBase(dataBaseIndex, out T_ModelItem item))
            {
                AddItem(item, index);
            }
        }

        public virtual void AddItem(T_ModelItem item, int index)
        {
            Model.AddItem(item, index);
            var viewItem = View[index];
            SetIcon(item, viewItem);
            SetColor(index);
        }

        public virtual void RemoveItem(int position)
        {
            Model.RemoveItem(position);
            SetColor(position);
        }

        public T_ModelItem GetItemData(int index)
        {
            return Model[index];
        }

        public T_ViewItem GetItemUI(int index)
        {
            return View[index];
        }

        public virtual void Swap(int p1, int p2)
        {
            Model.Swap(p1, p2);
            View.Swap(p1, p2);
            OnSwap(p1, p2);
            SetColor(p1);
            SetColor(p2);
        }
    }
}
