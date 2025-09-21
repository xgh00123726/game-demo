using GameBase.Shops;
using GameBase.UI;
using UnityEngine;

namespace Instance.UI.Shops
{
    public class ShopDetailableControl : IDetailableControl<ShopViewItem>
    {
        public ShopModel model;

        bool IDetailableControl<ShopViewItem>.IsDetail(ShopViewItem e)
        {
            return e.Obj.EnterTime > 0.2f;
        }

        void IDetailableControl<ShopViewItem>.OnDetail(ShopViewItem e)
        {
            ShopDetailShadowView.Instance.SetPosition(Input.mousePosition);
        }

        void IDetailableControl<ShopViewItem>.OnEnterDetail(ShopViewItem e)
        {
            ShopDetailShadowView.Instance.Show();
            ShopDetailShadowView.Instance.SetText(model.GetGoodID(e.ItemIndex).ToString());
        }

        void IDetailableControl<ShopViewItem>.OnExitDetail(ShopViewItem e)
        {
            ShopDetailShadowView.Instance.Hide();
        }
    }
}
