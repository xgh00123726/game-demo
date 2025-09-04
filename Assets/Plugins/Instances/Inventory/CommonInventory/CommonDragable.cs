using GameBase.GCamera;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance.Inventory
{
    public class CommonDragable : IDragable<InventoryItem>
    {
        public float dragJugTime = 0.1f;

        bool IDragable<InventoryItem>.IsDrag(InventoryItem e)
        {
            return e.Obj.PointerDownTime > dragJugTime;
        }

        void IDragable<InventoryItem>.OnDrag(InventoryItem e)
        {
            InventoryShadowItem.SetPosition(Input.mousePosition);
        }

        void IDragable<InventoryItem>.OnEnterDrag(InventoryItem e)
        {
            e.HideIcon();
            InventoryShadowItem.storedItem = e;
            InventoryShadowItem.SetIconSprite(e);
        }

        void IDragable<InventoryItem>.OnExitDrag(InventoryItem e)
        {
            InventoryShadowItem.Hide();
            var eitem = EquipmentInventory.Instance.GetItem(Input.mousePosition, out int eidx);
            if (eitem != null)
            {
                InventoryShadowItem.storedItem.SwapIconSprite(eitem);
                var data = CommonInventory.Instance.DataOfUI(e);
                var eb = Constructor.Buffs.Factory.Instance.Get(Constructor.Buffs.Type.Common, data.buffID);
                eb.owner = EquipmentInventory.Instance.owner;
                eb.durationSet = 9999;
                eb.uiStyle = GameBase.Buffs.UIStyle.None;
                eitem.ShowIcon();
            }
            e.ShowIcon();

            var citem = CommonInventory.Instance.GetItemUI(Input.mousePosition, out int cidx);
            if (citem != null)
            {
                InventoryShadowItem.storedItem.SwapIconSprite(citem);
                citem.ShowIcon();
            }
            e.ShowIcon();
        }
    }
}
