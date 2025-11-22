using GameBase.EntitySystem;
using GameBase.UI;

namespace Instance
{
    public class CommonDetailViewController : SingletonInstance<CommonDetailViewController>
    {
        private bool _isAttachToMouse = false;
        public CommonDetailView DetailView { get; set; }

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
            if (DetailView == null)
            {
                return;
            }

            if (_isAttachToMouse)
            {
                DetailView.SetPosition(UnityEngine.Input.mousePosition);
            }
        }
    }
}
