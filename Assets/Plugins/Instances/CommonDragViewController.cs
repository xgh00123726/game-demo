using GameBase.EntitySystem;
using GameBase.UI;

namespace Instance
{
    public class CommonDragViewController : SingletonInstance<CommonDragViewController>
    {
        private static bool _isAttachToMouse = false;
        public static CommonDragView dragView;

        public static void AttachToMouse()
        {
            _isAttachToMouse = true;
        }

        public static void Stop()
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
