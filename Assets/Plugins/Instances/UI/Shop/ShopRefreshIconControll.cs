using GameBase.UI;

namespace Instance
{
    public class ShopRefreshIconControll : IEnterExitControl
    {
        private ShopController _controller;

        public ShopRefreshIconControll(ShopController controller)
        {
            _controller = controller;
        }
        void IEnterExitControl.OnPointerDown(int i)
        {
            _controller.Refresh();
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
