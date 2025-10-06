using GameBase.EntitySystem;

namespace Instance
{
    public class ShopUIInteractive : UIInteractive<ShopUIInteractive, ShopViewPanel, ShopViewItem>
    {
        protected override ShopViewPanel GetPanel()
        {
            return ShopViewPanel.Instance;
        }
    }
}
