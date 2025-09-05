using GameBase.Infos;
using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class PassivePanel : BaseViewPanel<PassiveItem, PassivePanel>
    {

        public static float itemWidth = 100f;
        public static float itemHeight = 100f;
        public static float itemInterval = 5f;
        public static float maxPanelWidth = 1000f;

        internal override int PanelObjID => UIPanelConfig.Int.Passive_panelObjID;
        internal override int ShapeTexureID => UIPanelConfig.Int.Passive_shapeTexureID;
        internal override int ContourTexureID => UIPanelConfig.Int.Passive_contourTexureID;
        internal override float ItemWidth => UIPanelConfig.Float.Passive_itemWidth;
        internal override float ItemHeight => UIPanelConfig.Float.Passive_itemHeight;
        internal override float XInterval => UIPanelConfig.Float.Passive_xInterval;
        internal override float YInterval => UIPanelConfig.Float.Passive_yInterval;
        internal override float PanelX => UIPanelConfig.Float.Passive_panelX;
        internal override float PanelY => UIPanelConfig.Float.Passive_panelY;
        internal override float MaxPanelWidth => UIPanelConfig.Float.Passive_maxPanelWidth;
        internal override int ItemAlign => UIPanelConfig.Int.Passive_itemAlign;

        protected override void GetItemNumXYStyle(int index, out int itemPerLine, out int x, out int y)
        {
            var maxItemPerLine = (int)Mathf.Floor(MaxPanelWidth / (ItemWidth + XInterval));

            int halfY = index / (maxItemPerLine + 1);
            int reMainX = index % (maxItemPerLine + 1);

            itemPerLine = reMainX == 0 ? 1 : maxItemPerLine;
            y = halfY * 2 + (reMainX == 0 ? 0 : 1);
            x = reMainX == 0 ? 0 : reMainX - 1;
        }

        protected override BaseUI InstantiateObj(PassiveItem e)
        {
            var obj = base.InstantiateObj(e);

            return obj;
        }
    }
}