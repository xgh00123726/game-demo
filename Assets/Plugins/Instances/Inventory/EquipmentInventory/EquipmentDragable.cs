using GameBase.UI;
using UnityEngine;

namespace Instance.Inventory
{
    public class EquipmentDragable : IDragable<GameBase.UI.EquipmentItem>
    {
        public float dragJugTime = 0.1f;

        bool IDragable<GameBase.UI.EquipmentItem>.IsDrag(GameBase.UI.EquipmentItem e)
        {
            return e.Obj.PointerDownTime > dragJugTime;
        }

        void IDragable<GameBase.UI.EquipmentItem>.OnDrag(GameBase.UI.EquipmentItem e)
        {
            InventoryShadowItem.SetPosition(Input.mousePosition);
        }

        void IDragable<GameBase.UI.EquipmentItem>.OnEnterDrag(GameBase.UI.EquipmentItem e)
        {
            e.HideIcon();
            InventoryShadowItem.storedItem = e;
            InventoryShadowItem.SetIconSprite(e);
        }

        void IDragable<GameBase.UI.EquipmentItem>.OnExitDrag(GameBase.UI.EquipmentItem e)
        {
            InventoryShadowItem.Hide();
            var eitem = EquipmentInventory.Instance.GetItem(Input.mousePosition);
            if (eitem != null)
            {
                InventoryShadowItem.storedItem.SwapIconSprite(eitem);
                eitem.ShowIcon();
            }
            e.ShowIcon();

            var citem = CommonInventory.Instance.GetItem(Input.mousePosition);
            if (citem != null)
            {
                InventoryShadowItem.storedItem.SwapIconSprite(citem);
                citem.ShowIcon();
            }
            e.ShowIcon();
        }
    }
}
