using GameBase.LifeTime;
using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using Instance;
using Instance.UI.Shops;
using System;
using UnityEngine;

namespace LuaUtil
{
    public static class ShopUIUtil
    {
        private static ShopNearView _nearView;
        private static ShopViewPanel _panel;
        private static ShopDetailableControl _detailableControl;

        public static void Init()
        {
            _panel = new ShopViewPanel();
            _nearView = new ShopNearView();
            _detailableControl = new ShopDetailableControl(ShopUtil.CurrentShop?.inventory, _panel, new DefaultDetailableView());
            _panel.detailableControl = _detailableControl;

            LuaMain.Table.Get<string, Action<int>>("OnShopItemClicked", out var OnPointerDown);
            _panel.OnPointerDown = OnPointerDown;
        }

        public static void UpdatePanel(int shopID)
        {
            var shop = ShopMgr.GetShop(shopID);
            if (shop == null)
            {
                return;
            }

            _detailableControl._inventory = shop.inventory;
            _panel.UpdatePanel(shop.inventory);
        }

        public static void UpdateItem(int shopID, int updateIndex)
        {
            var shop = ShopMgr.GetShop(shopID);
            if (shop == null)
            {
                return;
            }

            _detailableControl._inventory = shop.inventory;
            _panel.UpdateItem(shop.inventory, updateIndex);
        }

        public static void ShowPanel()
        {
            _panel.Show();
        }

        public static void HidePanel()
        {
            _panel.Hide();
        }

        public static void ShowIndicator(int shopID)
        {
            _nearView.Show(ShopMgr.GetShop(shopID).obj.transform.position);
        }

        public static void HideIndicator()
        {
            _nearView.Hide();
        }

        public static void SetLayout(float xInterval, float yInterval, float width, float height, int align)
        {
            _panel.layout = new DefaultLayout()
            {
                xInterval = xInterval,
                yInterval = yInterval,
                width = width,
                height = height,
                align = (AlignType)align
            };
        }

        public static UICmd GetInputCmd()
        {
            if (!_panel.IsShow && Inputs.GetKeyDown(KeyFunction.OpenShop, "shopping"))
            {
                return UICmd.Show;
            }

            if (_panel.IsShow && Inputs.GetKeyDown(KeyFunction.CloseShop, "shopping"))
            {
                return UICmd.Hide;
            }

            if (Inputs.GetKeyDown(KeyFunction.Cancel, "shopping"))
            {
                return UICmd.Hide;
            }

            return UICmd.None;
        }
    }
}
