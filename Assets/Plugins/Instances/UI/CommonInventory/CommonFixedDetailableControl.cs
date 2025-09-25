using GameBase.UI;

namespace Instance
{
    public class CommonFixedDetailableControl : IEnterExitControl
    {
        void IEnterExitControl.OnPointerDown(int i)
        {
            CommonFixedDetailableShadowView.Instance.Show();
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
