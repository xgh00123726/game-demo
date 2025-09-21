using GameBase.Resources;
using GameBase.UI;
using GameBase.Shops;
using UnityEngine;
using GameBase.Tools;
using System.Collections.Generic;

namespace Instance.UI.Shops
{
    public class ShopView : IShopView, IShopInteractive
    {
        public bool _isTrigRefresh = false;

        internal ShopViewPanel SP => ShopViewPanel.Instance;

        bool IShopInteractive.TrigRefresh => _isTrigRefresh;

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

        void IShopInteractive.OnTrigRefresh()
        {
            _isTrigRefresh = false;
        }

        void IShopView.SetItem(List<int> goodIDs)
        {
            SP.FillItem(goodIDs.Count);
            for (int i = 0; i < goodIDs.Count; ++i)
            {
                if (goodIDs[i] < 0)
                {
                    SP[i].HideIcon();
                }
                else
                {
                    SP[i].ShowIcon();
                    SP[i].SetIconSprite(goodIDs[i]);
                }
            }
        }

        void IShopView.Show()
        {
            SP.Show();
        }

        public void Refresh()
        {
            _isTrigRefresh = true;
        }
    }
}
