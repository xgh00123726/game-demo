using GameBase.EntitySystem;
using GameBase.UI;

namespace Instance
{
    public class CommonDragViewController : SingletonInstance<CommonDragViewController>
    {
        private bool _isAttachToMouse = false;
        public CommonDragView dragView;

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
            if (dragView == null)
            {
                return;
            }

            if (_isAttachToMouse) 
            {
                dragView.SetPosition(UnityEngine.Input.mousePosition);
            }
        }
    }
}
