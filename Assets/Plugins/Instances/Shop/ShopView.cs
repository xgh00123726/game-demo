using GameBase.Resources;
using GameBase.UI;
using GameBase.Shops;
using UnityEngine;
using GameBase.Tools;

namespace Instance.UI.Shops
{
    public class ShopView : IShopView, IShopInteractive
    {
        internal ShopViewPanel SP => ShopViewPanel.Instance;

        int IShopView.GoodNums
        {
            get => SP.Entities.Count;
            set
            {
                SP.FillItem(value);
            }
        }

        bool IShopInteractive.TrigRefresh => false;

        int IShopInteractive.CurrentPurchase
        {
            get
            {
                if(Inputs.GetKeyDown(KeyFunction.ShopPurchase) && SP.TryGetItem(Input.mousePosition, out var e, out var index))
                {
                    return index;
                }

                return -1;
            }
        }

        void IShopView.Hide()
        {
            SP.Hide();
        }

        void IShopView.Show()
        {
            SP.Show();
        }
    }
}
