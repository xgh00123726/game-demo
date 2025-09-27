//using GameBase.UI;
//using UnityEngine;
//using GameBase.UI.MVC;

//namespace Instance.UI.MVC
//{
//    public class EquipmentDragableControl : MVCDragableControl<EquipmentViewItem>
//    {
//        public CommonInventoryController CC => CommonInventoryController.Instance;
//        public EquipmentInventoryController EC => EquipmentInventoryController.Instance;

//        protected override void OnExitDrag(EquipmentViewItem dragedItem, int dragedIndex)
//        {
//            if (EC.TryGetItemUI(Input.mousePosition, out var eEntity, out var eIndex))
//            {
//                EC.Swap(dragedIndex, eIndex);
//                eEntity.ShowTriggerIcon();
//            }

//            if (CC.TryGetItemUI(Input.mousePosition, out var cEntity, out var cIndex))
//            {
//                cEntity.SwapTriggerIconSprite(dragedItem);
//                cEntity.ShowTriggerIcon();

//                if (EC.TryGetData(dragedIndex, out var data))
//                {
//                    CC.Add(data, cIndex);
//                    EC.Remove(dragedIndex);
//                }
//            }
//        }
//    }
//}
