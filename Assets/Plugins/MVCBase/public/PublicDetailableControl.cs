using GameBase.UI;
using Instance.UI.MVC;
using UnityEngine;

namespace Instance.UI
{
    public class PublicDetailableControl : IDetailableControl<BaseViewItem>
    {
        protected DetailableShadowView<BaseViewItem> ShadowView => PublicDetailableShadowView.Instance;

        bool IDetailableControl<BaseViewItem>.IsDetail(BaseViewItem e)
        {
            return e.Obj.EnterTime > 0.2f;
        }

        void IDetailableControl<BaseViewItem>.OnDetail(BaseViewItem e)
        {
            ShadowView.SetPosition(Input.mousePosition);
        }

        void IDetailableControl<BaseViewItem>.OnEnterDetail(BaseViewItem e)
        {
            ShadowView.SetPosition(Input.mousePosition);

            var index = e.ItemIndex;
            if (index >= 0)
            {
                ShadowView.SetText($"index:{index}");
            }
            else
            {
                ShadowView.SetText($"this pos has no item");
            }
        }

        void IDetailableControl<BaseViewItem>.OnExitDetail(BaseViewItem e)
        {
            ShadowView.Hide();
        }
    }
}
