using GameBase.Infos;

namespace GameBase.UI
{
    public class InventoryPanel : BasePanel<InventoryItem, InventoryPanel>
    {
        internal override float ItemWidth => UIPanelConfig.Float.Inventory_itemWidth;

        internal override float ItemHeight => UIPanelConfig.Float.Inventory_itemHeight;

        internal override float XInterval => UIPanelConfig.Float.Inventory_xInterval;

        internal override float YInterval => UIPanelConfig.Float.Inventory_yInterval;

        internal override float MaxPanelWidth => UIPanelConfig.Float.Inventory_maxPanelWidth;

        internal override float PanelX => UIPanelConfig.Float.Inventory_panelX;

        internal override float PanelY => UIPanelConfig.Float.Inventory_panelY;

        internal override int PanelObjID => UIPanelConfig.Int.Inventory_panelObjID;

        internal override int ShapeTexureID => UIPanelConfig.Int.Inventory_shapeTexureID;

        internal override int ContourTexureID => UIPanelConfig.Int.Inventory_contourTexureID;

        internal override int ItemAlign => UIPanelConfig.Int.Inventory_itemAlign;
    }
}
