using GameBase.UI;
using UnityEngine;

namespace Instance.Inventory
{
    public abstract class InventoryDragableControl<T> : IDragableControl<T>
        where T : InventoryViewItem
    {
        public float dragJugTime = 0.1f;

        private T _dragedItem;
        private int _dragedIndex;

        protected abstract int GetDragedItemIndex(T e);
        protected abstract void OnExitDrag(T dragedItem, int dragedIndex);

        bool IDragableControl<T>.IsDrag(T e)
        {
            return e.Obj.PointerDownTime > dragJugTime;
        }

        void IDragableControl<T>.OnDrag()
        {
            // 拖动时shadow位置跟随鼠标变化
            DragableShadowView.SetPosition(Input.mousePosition);
        }

        void IDragableControl<T>.OnEnterDrag(T e)
        {
            // 被拖拽的图标隐藏
            e.HideIcon();

            // shadow储存被拖拽的图标，模拟图标被拖走
            DragableShadowView.CopyIcon(e);

            _dragedItem = e;
            _dragedIndex = GetDragedItemIndex(e);
        }

        void IDragableControl<T>.OnExitDrag()
        {
            OnExitDrag(_dragedItem, _dragedIndex);

            DragableShadowView.Hide();
            // 结束拖动后将原图标显示
            // 如果结束拖动后的位置是一个有效位置，则图标会互换，原图标会被替换成拖拽重点的图标
            // 如果结束拖动后的位置是无效位置，则什么都不会发生，原图标重新显示
            _dragedItem.ShowIcon();
        }
    }
}
