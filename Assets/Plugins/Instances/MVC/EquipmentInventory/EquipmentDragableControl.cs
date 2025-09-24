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
//                eEntity.ShowIcon();
//            }

//            if (CC.TryGetItemUI(Input.mousePosition, out var cEntity, out var cIndex))
//            {
//                cEntity.SwapIconSprite(dragedItem);
//                cEntity.ShowIcon();

//                if (EC.TryGetData(dragedIndex, out var data))
//                {
//                    CC.AddItem(data, cIndex);
//                    EC.RemoveItem(dragedIndex);
//                }
//            }
//        }
//    }
//}
