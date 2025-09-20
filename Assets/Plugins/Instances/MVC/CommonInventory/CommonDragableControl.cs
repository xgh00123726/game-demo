using GameBase.GCamera;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance.UI.MVC
{
    public class CommonDragableControl : MVCDragableControl<CommonInventoryViewItem>
    {
        public CommonInventoryController CC => CommonInventoryController.Instance;
        public EquipmentInventoryController EC => EquipmentInventoryController.Instance;
        public SpellActionModifierController SC => SpellActionModifierController.Instance;

        protected override void OnExitDrag(CommonInventoryViewItem dragedItem, int dragedIndex)
        {
            // 如果拖动的位置是装备栏
            if (EC.TryGetItemUI(Input.mousePosition, out var eEntity, out var eIndex))
            {
                eEntity.SwapIconSprite(dragedItem);
                eEntity.ShowIcon();


                if (CC.TryGetData(dragedIndex, out var data))
                {
                    EC.AddItem(data, eIndex);
                    CC.RemoveItem(dragedIndex);
                }
            }

            // 如果拖动终点是技能修饰器
            else if (SC.TryGetItemUI(Input.mousePosition, out var sEntity, out var sIndex))
            {
                sEntity.SwapIconSprite(dragedItem);
                sEntity.ShowIcon();


                if (CC.TryGetData(dragedIndex, out var data))
                {
                    SC.AddItem(data, sIndex);
                    CC.RemoveItem(dragedIndex);
                }
            }

            // 如果拖动终点是背包
            else if (CC.TryGetItemUI(Input.mousePosition, out var cEntity, out var cIndex))
            {
                CC.Swap(dragedIndex, cIndex);
                cEntity.ShowIcon();
            }
        }
    }
}
