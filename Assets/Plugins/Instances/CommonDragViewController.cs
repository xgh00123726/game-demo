using GameBase.EntitySystem;
using GameBase.UI;

namespace Instance
{
    public class CommonDragViewController : SingletonInstance<CommonDragViewController>
    {
        private bool _isAttachToMouse = false;
        public CommonDragView DragView { get; set; }

        public void AttachToMouse()
        {
            _isAttachToMouse = true;
        }

        public void Stop()
        {
            _isAttachToMouse = false;
        }

        protected override void Update()
        {
            if (DragView == null)
            {
                return;
            }

            if (_isAttachToMouse) 
            {
                DragView.SetPosition(UnityEngine.Input.mousePosition);
            }
        }
    }
}
