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

        internal override float PanelX => UIPanelConfig.Float.Inventory_panelX;

        internal override float PanelY => UIPanelConfig.Float.Inventory_panelY;

        internal override int PanelObjID => UIPanelConfig.Int.Inventory_panelObjID;

        internal override int ShapeTexureID => UIPanelConfig.Int.Inventory_shapeTexureID;

        internal override int ContourTexureID => UIPanelConfig.Int.Inventory_contourTexureID;

        internal override int ItemAlign => UIPanelConfig.Int.Inventory_itemAlign;

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

            ui.OnPointerEnter = e.OnPointerEnter;
            ui.OnPointerExit = e.OnPointerExist;

            obj.transform.SetParent(panel.transform, false);

            e.iconImage = obj.transform.Find("Icon").GetComponent<Image>();
            if (e.iconImage == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            return ui;
        }
    }
}
