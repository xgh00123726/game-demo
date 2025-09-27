using GameBase.Inventorys;
using GameBase.UI;
using UnityEngine;

namespace Instance
{
    public class CommonDragableControl : DefaultDragableControl<CommonInventoryViewItem>
    {
        private DynInventory<CommonInventoryData> _inventory;
        private new CommonInventoryViewPanel _viewPanel;

        public CommonDragableControl(DynInventory<CommonInventoryData> inventory, CommonInventoryViewPanel viewPanel) : base(viewPanel) 
        {
            _inventory = inventory;
            _viewPanel = viewPanel;
        }
        //public EquipmentInventoryController EC => EquipmentInventoryController.Instance;
        //public SpellActionModifierController SC => SpellActionModifierController.Instance;

        protected override void OnExitDragOver(int dragedIndex, int dragedOverIndex)
        {
            _inventory.Swap(dragedIndex, dragedOverIndex);
            base.OnExitDragOver(dragedIndex, dragedOverIndex);
        }

        protected override void OnExitDragOut(int dragedIndex)
        {
            base.OnExitDragOut(dragedIndex);
        }

        //protected override void OnExitDrag(CommonInventoryViewItem dragedItem, int dragedIndex)
        //{
            // 如果拖动的位置是装备栏
            //if (EC.TryGetItemUI(Input.mousePosition, out var eEntity, out var eIndex))
            //{
            //    eEntity.SwapTriggerIconSprite(dragedItem);
            //    eEntity.ShowTriggerIcon();


            //    if (CC.TryGetData(dragedIndex, out var data))
            //    {
            //        EC.Add(data, eIndex);
            //        CC.Remove(dragedIndex);
            //    }
            //}

            // 如果拖动终点是技能修饰器
            //if (SC.TryGetItemUI(Input.mousePosition, out var sEntity, out var sIndex))
            //{
            //    sEntity.SwapTriggerIconSprite(dragedItem);
            //    sEntity.ShowTriggerIcon();

            //    if (_CC.HasItem(dragedIndex))
            //    {
            //        SC.Add(_CC[dragedIndex].dItem, sIndex);
            //        _CC.Remove(dragedIndex);
            //    }
            //}

            // 如果拖动终点是背包

        //}
    }
}
