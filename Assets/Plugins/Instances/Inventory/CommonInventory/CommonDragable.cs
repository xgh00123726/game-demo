using GameBase.GCamera;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance.Inventory
{
    public class CommonDragable : IDragable<InventoryItem>
    {
        public float dragJugTime = 1f;

        bool IDragable<InventoryItem>.IsDrag(InventoryItem e)
        {
            return e.Obj.PointerDownTime > dragJugTime;
        }

        void IDragable<InventoryItem>.OnDrag(InventoryItem e)
        {
            CommonShadowItem.SetPosition(Input.mousePosition);
        }

        void IDragable<InventoryItem>.OnEnterDrag(InventoryItem e)
        {
            e.HideIcon();
            CommonShadowItem.SetImageIcon(e);
        }

        void IDragable<InventoryItem>.OnExitDrag(InventoryItem e)
        {
            CommonShadowItem.Hide();
            var item = CommonInventory.Instance.GetItem(Input.mousePosition);
            if (item == null)
            {
                
            }
            else
            {
                item.SwapIconImage(e);
            }
            e.ShowIcon();
        }
    }
}
