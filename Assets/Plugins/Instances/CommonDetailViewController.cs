using GameBase.EntitySystem;
using GameBase.UI;

namespace Instance
{
    public class CommonDetailViewController : SingletonInstance<CommonDetailViewController>
    {
        private bool _isAttachToMouse = false;
        public CommonDetailView detailView;

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
