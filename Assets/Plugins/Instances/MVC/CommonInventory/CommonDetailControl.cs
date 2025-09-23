using GameBase.UI;
using UnityEngine;
using GameBase.UI.MVC;
namespace Instance.UI.MVC
{
    public class CommonDetailControl : IDetailableControl<CommonInventoryViewItem>
    {
        protected DetailableShadowView<BaseViewItem> ShadowView => PublicDetailableShadowView.Instance;

        bool IDetailableControl<CommonInventoryViewItem>.IsDetail(CommonInventoryViewItem e)
        {
            return e.Obj.EnterTime > 0.2f && CommonInventoryController.Instance.HasItem(e.ItemIndex);
        }

        void IDetailableControl<CommonInventoryViewItem>.OnDetail(CommonInventoryViewItem e)
        {
            ShadowView.Show();
        }

        void IDetailableControl<CommonInventoryViewItem>.OnEnterDetail(CommonInventoryViewItem e)
        {
            ShadowView.SetPosition(Input.mousePosition);

            if (CommonInventoryController.Instance.TryGetData(e.ItemIndex, out var data))
            {
                ShadowView.SetText(InventoryDataBase.GetText(data.ID));
            }
            else
            {
                ShadowView.SetText($"index:{e.ItemIndex}");
            }
        }

        void IDetailableControl<CommonInventoryViewItem>.OnExitDetail(CommonInventoryViewItem e)
        {
            ShadowView.Hide();
        }
    }
}
