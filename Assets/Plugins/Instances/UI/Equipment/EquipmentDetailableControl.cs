using GameBase.Buffs;
using GameBase.Inventorys;
using GameBase.Texts;
using GameBase.UI;
using UnityEngine;

namespace Instance
{
    public class EquipmentDetailableControl : IDetailableControl
    {
        private CommonInventory<Buff> _model;
        private EquipmentPanel _viewPanel;
        private DefaultDetailableView _detailableView;

        public EquipmentDetailableControl(CommonInventory<Buff> model, EquipmentPanel viewPanel, DefaultDetailableView detailableView)
        {
            _model = model;
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
                _detailableView.SetText(TextMgr.GetBuffText(_model[i].id));
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
