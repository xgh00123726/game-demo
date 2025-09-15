using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance.MVC
{
    public abstract class InventoryController<T_ModelItem, T_ViewItem, T_View, T_Controller> : MVController<
        T_ModelItem,
        T_ViewItem,
        T_View,
        T_Controller>
        where T_ModelItem : IInventoryItem
        where T_ViewItem : InventoryViewItem, new()
        where T_View : InventoryViewPanel<T_ViewItem, T_View>, new()
        where T_Controller : InventoryController<T_ModelItem, T_ViewItem, T_View, T_Controller>, new()
    {
        protected virtual void OnSwap(int p1, int p2) { }

        protected override void SetIcon(T_ModelItem modelData, T_ViewItem viewItem)
        {
            viewItem.SetIconSprite(modelData.IconTextureID);
        }

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

        public override int AddItem(T_ModelItem item)
        {
            var index = base.AddItem(item);
            SetColor(index);
            return index;
        }

        public override int AddItem(T_ModelItem item, int index)
        {
            base.AddItem(item, index);
            SetColor(index);
            return index;
        }

        public override void RemoveItem(int position)
        {
            base.RemoveItem(position);
            SetColor(position);
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
