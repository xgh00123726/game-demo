using GameBase.Infos;
using GameBase.UI;

public class SpellActionModifierViewPanel : InventoryViewPanel<SpellActionModifierViewItem, SpellActionModifierViewPanel>
{
    internal override float ItemWidth => UIPanelConfig.Float.SpellActionModifier_itemWidth;
    internal override float ItemHeight => UIPanelConfig.Float.SpellActionModifier_itemHeight;
    internal override float XInterval => UIPanelConfig.Float.SpellActionModifier_xInterval;
    internal override float YInterval => UIPanelConfig.Float.SpellActionModifier_yInterval;
    internal override float MaxPanelWidth => UIPanelConfig.Float.SpellActionModifier_maxPanelWidth;
    internal override float PanelX => UIPanelConfig.Float.SpellActionModifier_panelX;
    internal override float PanelY => UIPanelConfig.Float.SpellActionModifier_panelY;
    internal override int PanelObjID => UIPanelConfig.Int.SpellActionModifier_panelObjID;
    internal override int ShapeTexureID => UIPanelConfig.Int.SpellActionModifier_shapeTexureID;
    internal override int ContourTexureID => UIPanelConfig.Int.SpellActionModifier_contourTexureID;
    internal override int ItemAlign => UIPanelConfig.Int.SpellActionModifier_itemAlign;
}
