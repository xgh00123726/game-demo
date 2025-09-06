using UnityEngine;
using GameBase.Tools;
using GameBase.Infos;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class EquipmentViewPanel : InventoryViewPanel<EquipmentViewItem, EquipmentViewPanel>
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

        protected override RectTransform GetRectTransform(EquipmentViewItem e)
        {
            return e.Obj.transform.Find("Icon").GetComponent<RectTransform>();
        }

        protected override BaseUI InstantiateObj(EquipmentViewItem e)
        {
            var ui = base.InstantiateObj(e);

            e.iconImage = e.Obj.transform.Find("Icon").GetComponent<Image>();
            if (e.iconImage == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            return ui;
        }
    }
}
