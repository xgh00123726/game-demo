using GameBase.UI;

namespace Instance.MVC
{
    public class CommonFixedDetailableControl : IEnterExitControl<CommonInventoryViewItem>
    {
        void IEnterExitControl<CommonInventoryViewItem>.OnPointerDown(CommonInventoryViewItem e)
        {
            CommonFixedDetailableShadowView.Instance.SetText($"hello\nlast spell panel click:{SpellViewPanel.Instance.LastClickedItemIndex}");
            CommonFixedDetailableShadowView.Instance.Show();
        }

        void IEnterExitControl<CommonInventoryViewItem>.OnPointerEnter(CommonInventoryViewItem e)
        {
        }

        void IEnterExitControl<CommonInventoryViewItem>.OnPointerExit(CommonInventoryViewItem e)
        {
        }

        void IEnterExitControl<CommonInventoryViewItem>.OnPointerRightDown(CommonInventoryViewItem e)
        {
            
        }
    }
}
