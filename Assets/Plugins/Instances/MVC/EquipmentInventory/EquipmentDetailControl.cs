//using GameBase.UI;
//using UnityEngine;
//using GameBase.UI.MVC;
//namespace Instance.UI.MVC
//{
//    public class EquipmentDetailControl : IDetailableControl<EquipmentViewItem>
//    {
//        protected IDetailableShadowView _detailableView = new DefaultDetailableShadowView();
//        bool IDetailableControl<EquipmentViewItem>.IsDetail(EquipmentViewItem e)
//        {
//            return e.uiScript.EnterTime > 0.2f;
//        }

//        void IDetailableControl<EquipmentViewItem>.OnDetail(EquipmentViewItem e)
//        {
//            _detailableView.UpdateSpell();
//        }

//        void IDetailableControl<EquipmentViewItem>.OnEnterDetail(EquipmentViewItem e)
//        {
//            _detailableView.SetPosition(Input.mousePosition);

//            if (EquipmentInventoryController.Instance.TryGetData(e.ItemIndex, out var data))
//            {
//                _detailableView.SetText(InventoryDataBase.GetText(data.ID));
//            }
//            else
//            {
//                _detailableView.SetText("NNN");
//            }
//        }

//        void IDetailableControl<EquipmentViewItem>.OnExitDetail(EquipmentViewItem e)
//        {
//            _detailableView.Hide();
//        }
//    }
//}
