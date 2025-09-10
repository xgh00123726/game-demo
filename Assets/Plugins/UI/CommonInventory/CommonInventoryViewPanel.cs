using GameBase.Infos;
using UnityEngine;

namespace GameBase.UI
{
    public class CommonInventoryViewPanel : InventoryViewPanel<CommonInventoryViewItem, CommonInventoryViewPanel>
    {
        protected float panelXOffset = 0f;
        protected float panelYOffset = 0f;

        public float panelXMoveSpeed = 100f;
        public float panelYMoveSpeed = 100f;
        public float panelXOffsetTarget = 0f;
        public float panelYOffsetTarget = 0f;

        internal override float ItemWidth => UIPanelConfig.Float.Inventory_itemWidth;
        internal override float ItemHeight => UIPanelConfig.Float.Inventory_itemHeight;
        internal override float XInterval => UIPanelConfig.Float.Inventory_xInterval;
        internal override float YInterval => UIPanelConfig.Float.Inventory_yInterval;
        internal override float MaxPanelWidth => UIPanelConfig.Float.Inventory_maxPanelWidth;
        internal override float PanelX => UIPanelConfig.Float.Inventory_panelX + panelXOffset;
        internal override float PanelY => UIPanelConfig.Float.Inventory_panelY + panelYOffset;
        internal override int PanelObjID => UIPanelConfig.Int.Inventory_panelObjID;
        internal override int ShapeTexureID => UIPanelConfig.Int.Inventory_shapeTexureID;
        internal override int ContourTexureID => UIPanelConfig.Int.Inventory_contourTexureID;
        internal override int ItemAlign => UIPanelConfig.Int.Inventory_itemAlign;
        protected override Vector3 GetItemLocalPosition(int index)
        {
            GetItemNumXYStyle(index, out int itemPerLine, out int x, out int y);

            float vx = 0f;
            if (ItemAlign == (int)Align.Left)
            {
                vx = (ItemWidth + XInterval) * x;
            }
            else if (ItemAlign == (int)Align.Center)
            {
                vx = (ItemWidth + XInterval) * x;
                float remainWidth = itemPerLine * (ItemWidth + XInterval);
                vx -= remainWidth / 2;
            }
            float vy = (ItemHeight + YInterval) * y;


            return new Vector3(vx, PanelY - vy, 0);
        }
        protected override void Update()
        {
            base.Update();

            var delta = panelXOffset - panelXOffsetTarget;
            var xMoveDis = panelXMoveSpeed * Time.deltaTime;
            if (delta > xMoveDis)
            {
                panelXOffset -= xMoveDis;
            }
            else if (-delta > xMoveDis)
            {
                panelXOffset += xMoveDis;
            }
            else
            {
                panelXOffset = panelXOffsetTarget;
            }
        }
    }
}
