using GameBase.GCamera;
using GameBase.Tools;
using GameBase.UI;
using GameBase.UI.MVC;
using UnityEngine;

namespace Instance.UI.MVC
{
    public class CommonDragableControl : DefaultDragableControl<CommonInventoryViewItem>
    {
        private CommonInventoryController _CC;

        public CommonDragableControl(CommonInventoryController CC)
        {
            _CC = CC;
        }
        //public EquipmentInventoryController EC => EquipmentInventoryController.Instance;
        public SpellActionModifierController SC => SpellActionModifierController.Instance;

        protected override void OnExitDrag(CommonInventoryViewItem dragedItem, int dragedIndex)
        {
            // 如果拖动的位置是装备栏
            //if (EC.TryGetItemUI(Input.mousePosition, out var eEntity, out var eIndex))
            //{
            //    eEntity.SwapIconSprite(dragedItem);
            //    eEntity.ShowIcon();


            //    if (CC.TryGetData(dragedIndex, out var data))
            //    {
            //        EC.AddItem(data, eIndex);
            //        CC.RemoveItem(dragedIndex);
            //    }
            //}

            // 如果拖动终点是技能修饰器
            if (SC.TryGetItemUI(Input.mousePosition, out var sEntity, out var sIndex))
            {
                sEntity.SwapIconSprite(dragedItem);
                sEntity.ShowIcon();

                if (_CC.HasItem(dragedIndex))
                {
                    SC.AddItem(_CC[dragedIndex].dItem, sIndex);
                    _CC.Remove(dragedIndex);
                }
            }

            // 如果拖动终点是背包
            var cViewItem = _CC.GetView(Input.mousePosition);

            if (cViewItem != null)
            {
                _CC.Swap(dragedIndex, cViewItem.ItemIndex);
                cViewItem.ShowIcon();
            }
        }
    }
}
