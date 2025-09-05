using GameBase.UI;
using UnityEngine;

namespace Instance.Inventory
{
    public class EquipmentDragable : IDragable<GameBase.UI.EquipmentViewItem>
    {
        public float dragJugTime = 0.1f;

        bool IDragable<GameBase.UI.EquipmentViewItem>.IsDrag(GameBase.UI.EquipmentViewItem e)
        {
            return e.Obj.PointerDownTime > dragJugTime;
        }

        void IDragable<GameBase.UI.EquipmentViewItem>.OnDrag(GameBase.UI.EquipmentViewItem e)
        {
            InventoryShadowItem.SetPosition(Input.mousePosition);
        }

        void IDragable<GameBase.UI.EquipmentViewItem>.OnEnterDrag(GameBase.UI.EquipmentViewItem e)
        {
            e.HideIcon();
            InventoryShadowItem.StoreItem(e);
        }

        void IDragable<GameBase.UI.EquipmentViewItem>.OnExitDrag(GameBase.UI.EquipmentViewItem e)
        {
            InventoryShadowItem.Hide();
            if (EquipmentInventoryController.Instance.TryGetItemUI(Input.mousePosition, out var ee) != -1)
            {
                InventoryShadowItem.StorePop().SwapIconSprite(ee);
                ee.ShowIcon();
            }

            if (CommonInventoryController.Instance.TryGetItemUI(Input.mousePosition, out var ec) != -1)
            {
                InventoryShadowItem.StorePop().SwapIconSprite(ec);
                ec.ShowIcon();
            }
            e.ShowIcon();
        }
    }
}
