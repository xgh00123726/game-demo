using UnityEngine;

namespace GameBase.UI
{
    public abstract class DefaultDragableControl<T> : IDragableControl
        where T : BaseViewItem, new()
    {
        public float dragJugTime = 0.1f;

        private BaseViewPanel<T> _viewPanel;
        private T _dragedItem;
        private int _dragedIndex;
        protected abstract void OnExitDrag(T dragedItem, int dragedIndex);

        public DefaultDragableControl(BaseViewPanel<T> viewPanel)
        {
            _viewPanel = viewPanel;
        }

        bool IDragableControl.IsDrag(int i)
        {
            return _viewPanel[i].uiScript.PointerDownTime > dragJugTime;
        }

        void IDragableControl.OnDrag()
        {
            // 拖动时shadow位置跟随鼠标变化
            DefaultDragableShadowView.SetPosition(Input.mousePosition);
        }

        void IDragableControl.OnEnterDrag(int i)
        {
            var e = _viewPanel[i];

            // 被拖拽的图标隐藏
            _dragedItem = e;
            _dragedItem.triggerImage.Hide();

            // shadow储存被拖拽的图标，模拟图标被拖走
            DefaultDragableShadowView.CopyIcon(e);
            _dragedIndex = e.ItemIndex;
        }

        void IDragableControl.OnExitDrag()
        {
            OnExitDrag(_dragedItem, _dragedIndex);

            DefaultDragableShadowView.Hide();
            // 结束拖动后将原图标显示
            // 如果结束拖动后的位置是一个有效位置，则图标会互换，原图标会被替换成拖拽重点的图标
            // 如果结束拖动后的位置是无效位置，则什么都不会发生，原图标重新显示
            _dragedItem.triggerImage.Show();
        }
    }
}
