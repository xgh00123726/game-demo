using GameBase.UI;

namespace Instance.UI.Shops
{
    public class ShopDetailableControl : IDetailableControl<ShopViewItem>
    {
        bool IDetailableControl<ShopViewItem>.IsDetail(ShopViewItem e)
        {
            return e.Obj.EnterTime > 0.2f;
        }

        void IDetailableControl<ShopViewItem>.OnDetail(ShopViewItem e)
        {
            
        }

        void IDetailableControl<ShopViewItem>.OnEnterDetail(ShopViewItem e)
        {
            
        }

        void IDetailableControl<ShopViewItem>.OnExitDetail(ShopViewItem e)
        {
            
        }
    }
}
