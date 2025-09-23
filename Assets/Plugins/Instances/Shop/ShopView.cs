using GameBase.Resources;
using GameBase.UI;
using GameBase.Shops;
using UnityEngine;
using GameBase.Tools;
using System.Collections.Generic;
using Instance.UI.MVC;

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
                    SP[i].HideColor();
                }
                else
                {
                    var info = ShopItemInfos.Get(goodIDs[i]);
                    if (info.reflectType == ReflectType.InventoryItem)
                    {
                        if (info.iconTextureID >= 0)
                        {
                            SP[i].SetIconSprite(info.iconTextureID);
                        }
                        else
                        {
                            var inventoryDataID = info.reflectID;
                            var iconTextureID = InventoryDataBase.Get(inventoryDataID).iconTextureID;
                            SP[i].SetIconSprite(iconTextureID);
                        }

                        SP[i].SetIconColor(ViewConfig.GetColor(info.rarity));
                    }
                    else if (info.reflectType == ReflectType.Buff)
                    {
                        SP[i].SetIconSprite(info.iconTextureID);

                        SP[i].SetIconColor(ViewConfig.GetColor(info.rarity));
                    }
                    else
                    {
                        SP[i].SetIconSprite(0);

                        SP[i].HideColor();
                    }
                    SP[i].ShowIcon();
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
