using GameBase.UI;

namespace Instance.MVC
{
    public class CommonFixedDetailableControl : IEnterExistControl<CommonInventoryViewItem>
    {
        void IEnterExistControl<CommonInventoryViewItem>.OnPointerDown(CommonInventoryViewItem e)
        {
            CommonFixedDetailableShadowView.Instance.SetText("hello");
            CommonFixedDetailableShadowView.Instance.Show();
        }

        void IEnterExistControl<CommonInventoryViewItem>.OnPointerEnter(CommonInventoryViewItem e)
        {
        }

        void IEnterExistControl<CommonInventoryViewItem>.OnPointerExit(CommonInventoryViewItem e)
        {
        }

        void IEnterExistControl<CommonInventoryViewItem>.OnPointerRightDown(CommonInventoryViewItem e)
        {
            
        }
    }
}
