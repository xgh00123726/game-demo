using GameBase.Infos;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class ShopViewPanel : BaseViewPanel<ShopViewItem, ShopViewPanel>
    {
        internal override float ItemWidth => UIPanelConfig.Float.Shop_itemWidth;
        internal override float ItemHeight => UIPanelConfig.Float.Shop_itemHeight;
        internal override float XInterval => UIPanelConfig.Float.Shop_xInterval;
        internal override float YInterval => UIPanelConfig.Float.Shop_yInterval;
        internal override float MaxPanelWidth => UIPanelConfig.Float.Shop_maxPanelWidth;
        internal override float PanelX => UIPanelConfig.Float.Shop_panelX;
        internal override float PanelY => UIPanelConfig.Float.Shop_panelY;
        internal override int PanelObjID => UIPanelConfig.Int.Shop_panelObjID;
        internal override int ShapeTexureID => UIPanelConfig.Int.Shop_shapeTexureID;
        internal override int ContourTexureID => UIPanelConfig.Int.Shop_contourTexureID;
        internal override int ItemAlign => UIPanelConfig.Int.Shop_itemAlign;
        protected override RectTransform GetRectTransform(ShopViewItem e)
        {
            return e.Obj.transform.Find("Icon").GetComponent<RectTransform>();
        }
        protected override BaseUI InstantiateObj(ShopViewItem e)
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
