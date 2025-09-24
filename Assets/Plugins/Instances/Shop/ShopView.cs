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
        public BaseViewPanel<BaseViewItem> view;

        bool IShopInteractive.TrigRefresh => _isTrigRefresh;

        int IShopInteractive.CurrentPurchase
        {
            get
            {
                if(Inputs.GetKeyDown(KeyFunction.ShopPurchase) && view.TryGetItem(Input.mousePosition, out var e, out var index))
                {
                    return index;
                }

                return -1;
            }
        }

        void IShopView.Hide()
        {
            view.Hide();
        }

        void IShopInteractive.OnTrigRefresh()
        {
            _isTrigRefresh = false;
        }

        void IShopView.SetItem(List<int> goodIDs)
        {
            view.FillItem(goodIDs.Count);
            for (int i = 0; i < goodIDs.Count; ++i)
            {
                if (goodIDs[i] < 0)
                {
                    view[i].HideIcon();
                    view[i].HideColor();
                }
                else
                {
                    var info = ShopItemInfos.Get(goodIDs[i]);
                    if (info.reflectType == ReflectType.InventoryItem)
                    {
                        if (info.iconTextureID >= 0)
                        {
                            view[i].SetIconSprite(info.iconTextureID);
                        }
                        else
                        {
                            var inventoryDataID = info.reflectID;
                            var iconTextureID = InventoryDataBase.Get(inventoryDataID).iconTextureID;
                            view[i].SetIconSprite(iconTextureID);
                        }

                        view[i].SetIconColor(ViewConfig.GetColor(info.rarity));
                    }
                    else if (info.reflectType == ReflectType.Buff)
                    {
                        view[i].SetIconSprite(info.iconTextureID);

                        view[i].SetIconColor(ViewConfig.GetColor(info.rarity));
                    }
                    else
                    {
                        view[i].SetIconSprite(0);

                        view[i].HideColor();
                    }
                    view[i].ShowIcon();
                }
            }
        }

        void IShopView.Show()
        {
            view.Show();
        }

        public void Refresh()
        {
            _isTrigRefresh = true;
        }
    }
}
