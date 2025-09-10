using GameBase.GCamera;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance.MVC
{
    public class CommonDragableControl : InventoryDragableControl<CommonInventoryViewItem>
    {
        public CommonInventoryController CC => CommonInventoryController.Instance;
        public EquipmentInventoryController EC => EquipmentInventoryController.Instance;

        protected override int GetDragedItemIndex(CommonInventoryViewItem e)
        {
            return CC.IndexOfView(e);
        }

        protected override void OnExitDrag(CommonInventoryViewItem dragedItem, int dragedIndex)
        {
            // 如果拖动的位置是装备栏
            if (EC.TryGetItemUI(Input.mousePosition, out var ee, out var ie))
            {
                ee.SwapIconSprite(dragedItem);
                ee.ShowIcon();


                if (CC.TryGetData(dragedIndex, out var data))
                {
                    EC.AddItem(data, ie);
                    CC.RemoveItem(dragedIndex);
                }
            }

            // 如果拖动终点是背包
            if (CC.TryGetItemUI(Input.mousePosition, out var ec, out var ic))
            {
                CC.Swap(dragedIndex, ic);
                ec.ShowIcon();
            }
        }
    }
}
