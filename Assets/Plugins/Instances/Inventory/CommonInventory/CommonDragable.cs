using GameBase.GCamera;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance.Inventory
{
    public class CommonDragable : IDragable<InventoryViewItem>
    {
        public float dragJugTime = 0.1f;

        bool IDragable<InventoryViewItem>.IsDrag(InventoryViewItem e)
        {
            return e.Obj.PointerDownTime > dragJugTime;
        }

        void IDragable<InventoryViewItem>.OnDrag(InventoryViewItem e)
        {
            // 拖动时shadow位置跟随鼠标变化
            InventoryShadowItem.SetPosition(Input.mousePosition);
        }

        void IDragable<InventoryViewItem>.OnEnterDrag(InventoryViewItem e)
        {
            // 被拖拽的图标隐藏
            e.HideIcon();

            // shadow储存被拖拽的图标，模拟图标被拖走
            InventoryShadowItem.StoreItem(e);
        }

        void IDragable<InventoryViewItem>.OnExitDrag(InventoryViewItem e)
        {
            InventoryShadowItem.Hide();
            if(EquipmentInventoryController.Instance.TryGetItemUI(Input.mousePosition, out var ee) != -1)
            {
                InventoryShadowItem.StorePop().SwapIconSprite(ee);
                var data = CommonInventoryController.Instance.DataOfUI(e);
                var eb = Constructor.Buffs.Factory.Instance.Get(Constructor.Buffs.Type.Common, data.buffID);

                eb.uiStyle = GameBase.Buffs.UIStyle.None;
                eb.durationSet = 9999;
                EquipmentInventoryController.Instance.owner.AddBuff(eb);
                ee.ShowIcon();
            }

            if (CommonInventoryController.Instance.TryGetItemUI(Input.mousePosition, out var ec) != -1)
            {
                InventoryShadowItem.StorePop().SwapIconSprite(ec);
                ec.ShowIcon();
            }

            // 结束拖动后将原图标显示
            // 如果结束拖动后的位置是一个有效位置，则图标会互换，原图标会被替换成拖拽重点的图标
            // 如果结束拖动后的位置是无效位置，则什么都不会发生，原图标重新显示
            e.ShowIcon();
        }
    }
}
