using GameBase.Resources;
using GameBase.UI;
using GameBase.Shops;
using UnityEngine;

namespace Instance.Shops
{
    public class ShopView : IShopView, IShopInteractive
    {
        internal ShopViewPanel SP => ShopViewPanel.Instance;

        int IShopView.GoodNums
        {
            get => SP.Constructor.Count;
            set
            {
                var count = SP.Constructor.Count;
                if (value > count)
                {
                    for (int i = 0; i < value - count; i++)
                    {
                        SP.NewEntity();
                    }
                }
                if (value < count)
                {
                    for (int i = 0; i < count -  value; i++)
                    {
                        SP.PopLast();
                    }
                }
            }
        }

        bool IShopInteractive.TrigRefresh => false;

        int IShopInteractive.CurrentPurchase => -1;

        void IShopView.Hide()
        {
            SP.panel.SetActive(false);
        }

        void IShopView.Show()
        {
            SP.panel.SetActive(true);
        }
    }
}
