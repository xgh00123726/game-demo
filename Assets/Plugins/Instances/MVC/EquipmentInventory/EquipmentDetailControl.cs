using GameBase.UI;
using UnityEngine;
using GameBase.UI.MVC;
namespace Instance.UI.MVC
{
    public class EquipmentDetailControl : IDetailableControl<EquipmentViewItem>
    {
        protected DetailableShadowView<BaseViewItem> ShadowView => PublicDetailableShadowView.Instance;
        bool IDetailableControl<EquipmentViewItem>.IsDetail(EquipmentViewItem e)
        {
            return e.Obj.EnterTime > 0.2f;
        }

        void IDetailableControl<EquipmentViewItem>.OnDetail(EquipmentViewItem e)
        {
            ShadowView.SetPosition(Input.mousePosition);
        }

        void IDetailableControl<EquipmentViewItem>.OnEnterDetail(EquipmentViewItem e)
        {
            ShadowView.SetPosition(Input.mousePosition);

            ShadowView.SetText(InventoryDataBase.Instance.GetText(e.ItemIndex));
        }

        void IDetailableControl<EquipmentViewItem>.OnExitDetail(EquipmentViewItem e)
        {
            ShadowView.Hide();
        }
    }
}
