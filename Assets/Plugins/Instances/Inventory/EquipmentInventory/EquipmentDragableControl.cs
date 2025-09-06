using GameBase.UI;
using UnityEngine;

namespace Instance.Inventory
{
    public class EquipmentDragableControl : IDragableControl<GameBase.UI.EquipmentViewItem>
    {
        public float dragJugTime = 0.1f;

        bool IDragableControl<GameBase.UI.EquipmentViewItem>.IsDrag(GameBase.UI.EquipmentViewItem e)
        {
            return e.Obj.PointerDownTime > dragJugTime;
        }

        void IDragableControl<GameBase.UI.EquipmentViewItem>.OnDrag(GameBase.UI.EquipmentViewItem e)
        {
            InventoryShadowView.SetPosition(Input.mousePosition);
        }

        void IDragableControl<GameBase.UI.EquipmentViewItem>.OnEnterDrag(GameBase.UI.EquipmentViewItem e)
        {
            e.HideIcon();
            InventoryShadowView.StorePush(e);
        }

        void IDragableControl<GameBase.UI.EquipmentViewItem>.OnExitDrag(GameBase.UI.EquipmentViewItem e)
        {
            InventoryShadowView.Hide();
            if (EquipmentInventoryController.Instance.TryGetItemUI(Input.mousePosition, out var ee, out var ie))
            {
                InventoryShadowView.StorePop().SwapIconSprite(ee);
                ee.ShowIcon();
            }

            if (CommonInventoryController.Instance.TryGetItemUI(Input.mousePosition, out var ec, out var ic))
            {
                InventoryShadowView.StorePop().SwapIconSprite(ec);
                ec.ShowIcon();
            }
            e.ShowIcon();
        }
    }
}
