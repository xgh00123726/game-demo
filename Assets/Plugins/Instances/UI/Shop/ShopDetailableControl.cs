using GameBase.Inventorys;
using GameBase.Texts;
using GameBase.Tools;
using GameBase.UI;
using Instance.UI.MVC;
using System;
using UnityEngine;

namespace Instance.UI.Shops
{
    public class ShopDetailableControl : IDetailableControl
    {
        private DefaultDetailableView _detailableView;
        private ShopViewPanel _viewPanel;
        public ShopInventory _inventory;
        public ShopDetailableControl(ShopInventory inventory, ShopViewPanel viewPanel, DefaultDetailableView detailableView)
        {
            _inventory = inventory;
            _viewPanel = viewPanel;
            _detailableView = detailableView;
        }


        bool IDetailableControl.IsDetail(int i)
        {
            var e = _viewPanel[i];
            var _model = _inventory;
            return e.uiScript.EnterTime > 0.2f && _model.HasItem(e.ItemIndex) && _viewPanel.IsShow;
        }

        void IDetailableControl.OnDetail(int i)
        {
            _detailableView.SetPosition(Input.mousePosition);
        }

        void IDetailableControl.OnEnterDetail(int i)
        {
            _detailableView.Show();
            var _model = _inventory;
            var info = _model.GetItemInfoOfShoppingPosition(i);
            var goodID = _model[i];

            if (info.type == ShopItemType.InventoryItem)
            {
                var secondInfo = InventoryDataBase.Instance[info.id];
                if (secondInfo.type == InventoryType.Equipment)
                {
                    _detailableView.SetText("物品\n" + TextMgr.GetBuffText(info.id));
                }
                else if (secondInfo.type == InventoryType.SpellActionModifier)
                {
                    _detailableView.SetText("技能修饰器\n" + TextMgr.GetSpellActionModifierText(secondInfo.id));
                }
            }
            else if (info.type == ShopItemType.Buff)
            {
                _detailableView.SetText("Buff\n" + TextMgr.GetBuffText(info.id));
            }
            else if (info.type == ShopItemType.Modifier)
            {
                _detailableView.SetText("属性\n" + TextMgr.GetModifierText(info.id));
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
