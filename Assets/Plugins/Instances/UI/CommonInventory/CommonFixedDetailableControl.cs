using GameBase.UI;

namespace Instance
{
    public class CommonFixedDetailableControl : IEnterExitControl
    {
        private DefaultFixedDetailableView _fixedDetailShadowView;

        public CommonFixedDetailableControl(DefaultFixedDetailableView fixedDetailShadowView)
        {
            _fixedDetailShadowView = fixedDetailShadowView;
        }

        void IEnterExitControl.OnPointerDown(int i)
        {
            _fixedDetailShadowView.Show();
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
