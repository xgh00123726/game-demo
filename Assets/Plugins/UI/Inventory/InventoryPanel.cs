using GameBase.Infos;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class InventoryPanel : BasePanel<InventoryItem, InventoryPanel>
    {
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

        protected float panelXOffset = 0f;
        protected float panelYOffset = 0f;
        public float panelXMoveSpeed = 100f;
        public float panelYMoveSpeed = 100f;
        public float panelXOffsetTarget = 0f;
        public float panelYOffsetTarget = 0f;

        protected override void AfterInstantiateEUObject(InventoryItem e)
        {
            if (e.IconTexureID >= 0)
            {
                var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.IconTexureID));
            }

            e.AfterInstantiateUObjectDelegate?.Invoke(e);

            e.Obj.gameObject.SetActive(true);
        }

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

        protected override BaseUI InstantiateObj(InventoryItem e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));
            var ui = obj.AddComponent<BaseUI>();

            obj.transform.SetParent(panel.transform, false);

            e.iconObject = obj.transform.Find("Icon").gameObject;

            e.rectTransform = obj.transform.Find("Icon").GetComponent<RectTransform>();

            e.iconImage = obj.transform.Find("Icon").GetComponent<Image>();
            if (e.iconImage == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            return ui;
        }

        protected override void UpdateEntity(InventoryItem e)
        {
            base.UpdateEntity(e);

            if (e.dragable == null)
            {
                return;
            }
            var isDrag = e.dragable.IsDrag(e);
            if (isDrag && !e.lastDrag)
            {
                e.dragable.OnEnterDrag(e);
            }
            else if (!isDrag && e.lastDrag)
            {
                e.dragable.OnExitDrag(e);
            }

            if (isDrag)
            {
                e.dragable.OnDrag(e);
            }

            e.lastDrag = isDrag;
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
