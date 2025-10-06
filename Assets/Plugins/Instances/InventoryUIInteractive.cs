using GameBase.EntitySystem;
using System;

namespace Instance
{
    public class InventoryUIInteractive : UIInteractive<InventoryUIInteractive, InventoryViewPanel, InventoryViewItem>
    {
        protected override InventoryViewPanel GetPanel()
        {
            return InventoryViewPanel.Instance;
        }
    }
}
