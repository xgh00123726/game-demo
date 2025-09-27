using GameBase.Inventorys;
using GameBase.Texts;
using GameBase.Tools;
using GameBase.UI;
using Instance.UI.MVC;
using UnityEngine;

namespace Instance.UI.Shops
{
    public class ShopDetailableControl : IDetailableControl
    {
        private DefaultDetailableView _detailableView;
        private ShopViewPanel _viewPanel;
        internal ShopInventory _model;
        public ShopDetailableControl(ShopInventory model, ShopViewPanel viewPanel, DefaultDetailableView detailableView)
        {
            _model = model;
            _viewPanel = viewPanel;
            _detailableView = detailableView;
        }


        bool IDetailableControl.IsDetail(int i)
        {
            var e = _viewPanel[i];
            return e.uiScript.EnterTime > 0.2f && _model.HasItem(e.ItemIndex) && _viewPanel.IsShow;
        }

        void IDetailableControl.OnDetail(int i)
        {
            _detailableView.SetPosition(Input.mousePosition);
        }

        void IDetailableControl.OnEnterDetail(int i)
        {
            _detailableView.Show();

            var info = _model.GetItemInfoFromShoppingPosition(i);

            if (info.type == ShopItemType.InventoryItem)
            {
                _detailableView.SetText("装备\n" + TextMgr.GetBuffText(info.secondID));
            }
            else if (info.type == ShopItemType.Buff)
            {
                _detailableView.SetText("成长\n" + TextMgr.GetBuffText(info.secondID));
            }
            else
            {
                _detailableView.Hide();
            }
        }

        void IDetailableControl.OnExitDetail(int i)
        {
            _detailableView.Hide();
        }
    }
}
