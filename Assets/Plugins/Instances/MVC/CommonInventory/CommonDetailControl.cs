using GameBase.UI;
using UnityEngine;
namespace Instance.MVC
{
    public class CommonDetailControl : IDetailableControl<CommonInventoryViewItem>
    {
        protected DetailableShadowView<CommonInventoryViewItem> ShadowView => CommonDetailShadowView.Instance;

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

            var index = e.ItemIndex;
            if (index >= 0 && CommonInventoryController.Instance.TryGetData(index, out var data))
            {
                ShadowView.SetText($"index:{index}\nbuff id:{data.buffID}\ntexture id:{data.IconTextureID}");
            }
            else
            {
                ShadowView.SetText($"this pos has no item");
            }
        }

        void IDetailableControl<CommonInventoryViewItem>.OnExitDetail(CommonInventoryViewItem e)
        {
            ShadowView.Hide();
        }
    }
}
