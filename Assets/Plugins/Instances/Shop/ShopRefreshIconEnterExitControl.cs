using GameBase.Shops;
using GameBase.Tools;
using GameBase.UI;

namespace Instance.UI.Shops
{
    public class ShopRefreshIconEnterExitControl : IEnterExitControl<BaseUI>
    {
        public Shop shop;

        void IEnterExitControl<BaseUI>.OnPointerDown(BaseUI e)
        {
            if (shop.shopView is ShopView s)
            {
                s.Refresh();
            }
        }

        void IEnterExitControl<BaseUI>.OnPointerEnter(BaseUI e)
        {
            
        }

        void IEnterExitControl<BaseUI>.OnPointerExit(BaseUI e)
        {
            
        }

        void IEnterExitControl<BaseUI>.OnPointerRightDown(BaseUI e)
        {
            
        }
    }
}
