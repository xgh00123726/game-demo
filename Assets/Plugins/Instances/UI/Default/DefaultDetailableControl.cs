using UnityEngine;

namespace GameBase.UI
{
    public class DefaultDetailableControl<T> : IDetailableControl
        where T : BaseViewItem, new()
    {
        private BaseViewPanel<T> _viewPanel;
        private DefaultDetailableView _detailableView;
        public DefaultDetailableControl(BaseViewPanel<T> viewPanel, DefaultDetailableView detailableView)
        {
            _detailableView = detailableView;
            _viewPanel = viewPanel;
        }

        bool IDetailableControl.IsDetail(int i)
        {
            return _viewPanel[i].uiScript.EnterTime > 0.2f;
        }

        void IDetailableControl.OnDetail(int i)
        {
            _detailableView.SetPosition(Input.mousePosition);
        }

        void IDetailableControl.OnEnterDetail(int i)
        {
            _detailableView.SetPosition(Input.mousePosition);

            if (i >= 0)
            {
                _detailableView.SetText($"index:{i}");
            }
            else
            {
                _detailableView.SetText($"NNN");
            }
        }

        void IDetailableControl.OnExitDetail(int i)
        {
            _detailableView.Hide();
        }
    }
}
