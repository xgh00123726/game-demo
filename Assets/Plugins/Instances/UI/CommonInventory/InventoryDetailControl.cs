using GameBase.Inventorys;
using GameBase.Texts;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance
{
    public class InventoryDetailControl : IDetailableControl
    {
        private CommonInventory<InventoryData> _model;
        private InventoryViewPanel _viewPanel;
        private DefaultDetailableView _detailableView;

        public InventoryDetailControl(CommonInventory<InventoryData> inventory, InventoryViewPanel viewPanel, DefaultDetailableView detailableView)
        {
            _model = inventory;
            _viewPanel = viewPanel;
            _detailableView = detailableView;
        }

        bool IDetailableControl.IsDetail(int i)
        {
            var e = _viewPanel[i];
            return e.uiScript.EnterTime > 0.2f && _model.HasItem(i);
        }

        void IDetailableControl.OnDetail(int i)
        {
            _detailableView.Show();
        }

        void IDetailableControl.OnEnterDetail(int i)
        {
            _detailableView.SetPosition(Input.mousePosition);

            if (_model.HasItem(i))
            {
                var info = _model[i];

                if (info.type == InventoryType.Equipment)
                {
                    _detailableView.SetText(TextMgr.GetBuffText(info.id));
                }
                else if (info.type == InventoryType.SpellActionModifier)
                {
                    _detailableView.SetText(TextMgr.GetSpellActionModifierText(info.id));
                }
                else
                {
                    _detailableView.SetText("NNN");
                }
            }
            else
            {
                _detailableView.SetText($"index:{i}");
            }
        }

        void IDetailableControl.OnExitDetail(int i)
        {
            _detailableView.Hide();
        }
    }
}
