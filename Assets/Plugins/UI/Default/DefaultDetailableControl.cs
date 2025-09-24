using UnityEngine;

namespace GameBase.UI
{
    public class DefaultDetailableControl<T> : IDetailableControl<T>
        where T : BaseViewItem
    {
        private DefaultDetailableShadowView _shadowView;
        public DefaultDetailableControl()
        {
            _shadowView = new();
        }

        protected virtual IDetailableShadowView ShadowView => _shadowView;

        bool IDetailableControl<T>.IsDetail(T e)
        {
            return e.Obj.EnterTime > 0.2f;
        }

        void IDetailableControl<T>.OnDetail(T e)
        {
            ShadowView.SetPosition(Input.mousePosition);
        }

        void IDetailableControl<T>.OnEnterDetail(T e)
        {
            ShadowView.SetPosition(Input.mousePosition);

            var index = e.ItemIndex;
            if (index >= 0)
            {
                ShadowView.SetText($"index:{index}");
            }
            else
            {
                ShadowView.SetText($"NNN");
            }
        }

        void IDetailableControl<T>.OnExitDetail(T e)
        {
            ShadowView.Hide();
        }
    }
}
