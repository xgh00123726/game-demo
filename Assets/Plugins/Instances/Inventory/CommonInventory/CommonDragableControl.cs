using GameBase.GCamera;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance.Inventory
{
    public class CommonDragableControl : IDragableControl<CommonInventoryViewItem>
    {
        public float dragJugTime = 0.1f;

        bool IDragableControl<CommonInventoryViewItem>.IsDrag(CommonInventoryViewItem e)
        {
            return e.Obj.PointerDownTime > dragJugTime;
        }

        void IDragableControl<CommonInventoryViewItem>.OnDrag(CommonInventoryViewItem e)
        {
            // 拖动时shadow位置跟随鼠标变化
            InventoryShadowView.SetPosition(Input.mousePosition);
        }

        void IDragableControl<CommonInventoryViewItem>.OnEnterDrag(CommonInventoryViewItem e)
        {
            // 被拖拽的图标隐藏
            e.HideIcon();

            // shadow储存被拖拽的图标，模拟图标被拖走
            InventoryShadowView.StorePush(e);
        }

        void IDragableControl<CommonInventoryViewItem>.OnExitDrag(CommonInventoryViewItem e)
        {
            InventoryShadowView.Hide();

            // 如果拖动的位置是装备栏
            if(EquipmentInventoryController.Instance.TryGetItemUI(Input.mousePosition, out var ee, out var ie))
            {
                InventoryShadowView.StorePop().SwapIconSprite(ee);
                ee.ShowIcon();

                if (CommonInventoryController.Instance.TryGetDataOfView(e, out var data))
                {
                    var eb = Constructor.Buffs.Factory.Instance.Get(Constructor.Buffs.Type.Common, data.buffID);
                    eb.uiStyle = GameBase.Buffs.UIStyle.None;
                    eb.durationSet = 9999;
                    EquipmentInventoryController.Instance.owner.RegisterBuff(eb);
                }
            }

            // 如果拖动终点是背包
            if (CommonInventoryController.Instance.TryGetItemUI(Input.mousePosition, out var ec, out var ic))
            {
                InventoryShadowView.StorePop().SwapIconSprite(ec);
                ec.ShowIcon();
            }

            // 结束拖动后将原图标显示
            // 如果结束拖动后的位置是一个有效位置，则图标会互换，原图标会被替换成拖拽重点的图标
            // 如果结束拖动后的位置是无效位置，则什么都不会发生，原图标重新显示
            e.ShowIcon();
        }
    }
}
