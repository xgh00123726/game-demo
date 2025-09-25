using UnityEngine;

namespace GameBase.UI
{
    public class DefaultDetailableControl<T> : IDetailableControl
        where T : BaseViewItem, new()
    {
        private BaseViewPanel<T> _viewPanel;
        private DefaultDetailableShadowView _shadowView;
        public DefaultDetailableControl(BaseViewPanel<T> viewPanel)
        {
            _shadowView = new();
            _viewPanel = viewPanel;
        }

        protected virtual IDetailableShadowView ShadowView => _shadowView;

        bool IDetailableControl.IsDetail(int i)
        {
            return _viewPanel[i].uiScript.EnterTime > 0.2f;
        }

        void IDetailableControl.OnDetail(int i)
        {
            ShadowView.SetPosition(Input.mousePosition);
        }

        void IDetailableControl.OnEnterDetail(int i)
        {
            ShadowView.SetPosition(Input.mousePosition);

            if (i >= 0)
            {
                ShadowView.SetText($"index:{i}");
            }
            else
            {
                ShadowView.SetText($"NNN");
            }
        }

        void IDetailableControl.OnExitDetail(int i)
        {
            ShadowView.Hide();
        }
    }
}
