using GameBase.UI;

namespace Instance
{
    public class ShopEnterExitControl : IEnterExitControl
    {
        private ShopController _controller;

        public ShopEnterExitControl(ShopController controller)
        {
            _controller = controller;
        }

        void IEnterExitControl.OnPointerDown(int i)
        {
            _controller.Remove(i);
            _controller.RefreshView(i);
        }

        void IEnterExitControl.OnPointerEnter(int i)
        {
            
        }

        void IEnterExitControl.OnPointerExit(int i)
        {
            
        }

        void IEnterExitControl.OnPointerRightDown(int i)
        {
            
        }
    }
}
