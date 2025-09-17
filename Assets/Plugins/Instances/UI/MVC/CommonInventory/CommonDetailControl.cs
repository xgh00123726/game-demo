using GameBase.UI;
using UnityEngine;
namespace Instance.UI.MVC
{
    public class CommonDetailControl : IDetailableControl<CommonInventoryViewItem>
    {
        protected DetailableShadowView<BaseViewItem> ShadowView => PublicDetailableShadowView.Instance;

        bool IDetailableControl<CommonInventoryViewItem>.IsDetail(CommonInventoryViewItem e)
        {
            return e.Obj.EnterTime > 0.2f;
        }

        void IDetailableControl<CommonInventoryViewItem>.OnDetail(CommonInventoryViewItem e)
        {
            ShadowView.SetPosition(Input.mousePosition);
        }

        void IDetailableControl<CommonInventoryViewItem>.OnEnterDetail(CommonInventoryViewItem e)
        {
            ShadowView.SetPosition(Input.mousePosition);

            ShadowView.SetText(InventoryDataBase.Instance.GetText(e.ItemIndex));
        }

        void IDetailableControl<CommonInventoryViewItem>.OnExitDetail(CommonInventoryViewItem e)
        {
            ShadowView.Hide();
        }
    }
}
