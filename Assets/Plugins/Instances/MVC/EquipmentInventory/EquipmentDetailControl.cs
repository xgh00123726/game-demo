//using GameBase.UI;
//using UnityEngine;
//using GameBase.UI.MVC;
//namespace Instance.UI.MVC
//{
//    public class EquipmentDetailControl : IDetailableControl<EquipmentViewItem>
//    {
//        protected IDetailableShadowView _shadowView = new DefaultDetailableShadowView();
//        bool IDetailableControl<EquipmentViewItem>.IsDetail(EquipmentViewItem e)
//        {
//            return e.Obj.EnterTime > 0.2f;
//        }

//        void IDetailableControl<EquipmentViewItem>.OnDetail(EquipmentViewItem e)
//        {
//            _shadowView.Show();
//        }

//        void IDetailableControl<EquipmentViewItem>.OnEnterDetail(EquipmentViewItem e)
//        {
//            _shadowView.SetPosition(Input.mousePosition);

//            if (EquipmentInventoryController.Instance.TryGetData(e.ItemIndex, out var data))
//            {
//                _shadowView.SetText(InventoryDataBase.GetText(data.ID));
//            }
//            else
//            {
//                _shadowView.SetText("NNN");
//            }
//        }

//        void IDetailableControl<EquipmentViewItem>.OnExitDetail(EquipmentViewItem e)
//        {
//            _shadowView.Hide();
//        }
//    }
//}
