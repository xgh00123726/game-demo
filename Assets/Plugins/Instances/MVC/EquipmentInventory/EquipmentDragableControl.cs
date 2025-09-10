using GameBase.UI;
using UnityEngine;

namespace Instance.MVC
{
    public class EquipmentDragableControl : InventoryDragableControl<EquipmentViewItem>
    {
        public CommonInventoryController CC => CommonInventoryController.Instance;
        public EquipmentInventoryController EC => EquipmentInventoryController.Instance;

        protected override void OnExitDrag(EquipmentViewItem dragedItem, int dragedIndex)
        {
            if (EC.TryGetItemUI(Input.mousePosition, out var ee, out var ie))
            {
                EC.Swap(dragedIndex, ie);
                ee.ShowIcon();
            }

            if (CC.TryGetItemUI(Input.mousePosition, out var ec, out var ic))
            {
                ec.SwapIconSprite(dragedItem);
                ec.ShowIcon();

                if (EC.TryGetData(dragedIndex, out var data))
                {
                    CC.AddItem(data, ic);
                    EC.RemoveItem(dragedIndex);
                }
            }
        }
    }
}
