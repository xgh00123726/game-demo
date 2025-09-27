using UnityEngine;

namespace GameBase.UI
{
    public abstract class DefaultDragableControl<T> : IDragableControl
        where T : BaseViewItem, new()
    {
        public float dragJugTime = 0.1f;

        protected BaseViewPanel<T> _viewPanel;
        private int _dragedIndex;
        private int _dragedOverIndex;

        public DefaultDragableControl(BaseViewPanel<T> viewPanel)
        {
            _viewPanel = viewPanel;
        }

        protected virtual bool IsExitDragOut()
        {
            var overItem = _viewPanel.GetItemFromTriggerPosition(Input.mousePosition);
            if (overItem == null)
            {
                return true;
            }
            else
            {
                _dragedOverIndex = overItem.ItemIndex;
                return false;
            }
        }
        protected virtual void OnExitDragOut(int dragedIndex)
        {
            var viewItem = _viewPanel[dragedIndex];

            viewItem.triggerImage.Show();
            viewItem.triggerImage.ShowColor();
        }
        protected virtual void OnExitDragOver(int dragedIndex, int dragedOverIndex)
        {
            // 结束拖动后将原图标显示
            // 如果结束拖动后的位置是一个有效位置，则图标会互换，原图标会被替换成拖拽重点的图标
            // 如果结束拖动后的位置是无效位置，则什么都不会发生，原图标重新显示
            _viewPanel[dragedIndex].triggerImage.ShowColor();
            _viewPanel.SwapIconSprite(dragedIndex, dragedOverIndex);
        }

        bool IDragableControl.IsDrag(int i)
        {
            return _viewPanel[i].uiScript.PointerDownTime > dragJugTime;
        }

        void IDragableControl.OnDrag()
        {
            // 拖动时shadow位置跟随鼠标变化
            DefaultDragableView.SetPosition(Input.mousePosition);
        }

        void IDragableControl.OnEnterDrag(int i)
        {
            var e = _viewPanel[i];

            // shadow储存被拖拽的图标，模拟图标被拖走
            DefaultDragableView.iconImage.Copy(e.triggerImage);

            // 被拖拽的图标隐藏
            e.triggerImage.Hide();
            e.triggerImage.HideColor();

            _dragedIndex = e.ItemIndex;
        }

        void IDragableControl.OnExitDrag()
        {
            DefaultDragableView.Hide();

            if (IsExitDragOut())
            {
                OnExitDragOut(_dragedIndex);
            }
            else
            {
                OnExitDragOver(_dragedIndex, _dragedOverIndex);
            }
        }
    }
}
