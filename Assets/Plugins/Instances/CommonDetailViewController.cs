using GameBase.EntitySystem;
using GameBase.UI;

namespace Instance
{
    public class CommonDetailViewController : SingletonInstance<CommonDetailViewController>
    {
        private static bool _isAttachToMouse = false;
        public static CommonDetailView detailView;

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
            if (detailView == null)
            {
                return;
            }

            if (_isAttachToMouse)
            {
                detailView.SetPosition(UnityEngine.Input.mousePosition);
            }
        }
    }
}
