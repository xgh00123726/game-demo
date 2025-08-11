using UnityEngine;
using GameBase.Tools;
using GameBase.Config;

namespace GameBase.UI
{
    public class EquipmentPanel : BasePanel<EquipmentItem, EquipmentPanel>
    {
        internal override float ItemWidth => UIPanelConfig.Float.Equipment_itemWidth;

        internal override float ItemHeight => UIPanelConfig.Float.Equipment_itemHeight;

        internal override float XInterval => UIPanelConfig.Float.Equipment_xInterval;

        internal override float YInterval => UIPanelConfig.Float.Equipment_yInterval;
        
        internal override float MaxPanelWidth => UIPanelConfig.Float.Equipment_maxPanelWidth;

        internal override float PanelX => UIPanelConfig.Float.Equipment_panelX;

        internal override float PanelY => UIPanelConfig.Float.Equipment_panelY;

        internal override int PanelObjID => UIPanelConfig.Int.Equipment_panelObjID;

        internal override int ShapeTexureID => UIPanelConfig.Int.Equipment_shapeTexureID;

        internal override int ContourTexureID => UIPanelConfig.Int.Equipment_contourTexureID;

        internal override int ItemAlign => UIPanelConfig.Int.Equipment_itemAlign;
    }
}
