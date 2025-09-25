using GameBase.UI;
using GameBase.UI.MVC;

namespace Instance.UI.MVC
{
    public abstract class InventoryController<T_ModelItem, T_ViewItem, T_View, T_Controller> : MVController<
        T_ModelItem,
        T_ViewItem,
        T_View,
        T_Controller>
        where T_ModelItem : CommonInventoryData
        where T_ViewItem : BaseViewItem, new()
        where T_View : BaseViewPanel<T_ViewItem>
        where T_Controller : InventoryController<T_ModelItem, T_ViewItem, T_View, T_Controller>, new()
    {
        protected virtual void OnSwap(int p1, int p2) { }

        protected override void SetItem(T_ModelItem modelData, T_ViewItem viewItem)
        {
            viewItem.triggerImage.SetIcon(modelData.iconTextureID);
            viewItem.triggerImage.SetColor(modelData.rarity); 
            viewItem.triggerImage.Show();
        }

        protected override void SetNullItem(T_ModelItem modelData, T_ViewItem viewItem)
        {
            viewItem.triggerImage.SetIcon(-1);
            viewItem.triggerImage.Hide();
            viewItem.triggerImage.HideColor();
        }

        private void SetColor(int index)
        {
            if (Model.HasItem(index))
            {
                View[index].triggerImage.SetColor(Model[index].rarity);
            }
            else
            {
                View[index].triggerImage.HideColor();
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
            View.SwapIconSprite(p1, p2);
            OnSwap(p1, p2);
            SetColor(p1);
            SetColor(p2);
        }
    }
}
