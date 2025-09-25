using GameBase.UI;
using UnityEngine;

namespace Instance
{
    public class CommonDetailControl : IDetailableControl
    {
        private CommonInventoryController _CC;
        private CommonInventoryViewPanel _viewPanel;
        private DefaultDetailableShadowView _shadowView = new();
        protected DefaultDetailableShadowView ShadowView => _shadowView;

        public CommonDetailControl(CommonInventoryController CC, CommonInventoryViewPanel viewPanel)
        {
            _CC = CC;
            _viewPanel = viewPanel;
        }

        bool IDetailableControl.IsDetail(int i)
        {
            var e = _viewPanel[i];
            return e.uiScript.EnterTime > 0.2f && _CC.HasItem(e.ItemIndex) && _CC.IsShow;
        }

        void IDetailableControl.OnDetail(int i)
        {
            ShadowView.Show();
        }

        void IDetailableControl.OnEnterDetail(int i)
        {
            ShadowView.SetPosition(Input.mousePosition);

            if (_CC.HasItem(i))
            {
                ShadowView.SetText(CommonInventoryDataBase.GetText(_CC[i].dItem.ID));
            }
            else
            {
                ShadowView.SetText($"index:{i}");
            }
        }

        void IDetailableControl.OnExitDetail(int i)
        {
            ShadowView.Hide();
        }
    }
}
