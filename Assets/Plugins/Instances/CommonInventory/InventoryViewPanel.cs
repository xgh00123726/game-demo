using GameBase.Config;
using GameBase.Equipments;
using GameBase.Inventorys;
using GameBase.Items;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance
{
    public class InventoryViewPanel : BaseViewPanel<InventoryViewItem, InventoryViewPanel>
    {
        protected bool _showFlag = false;
        protected float initX;
        protected float initY;

        protected float panelXOffset = 0f;
        protected float panelYOffset = 0f;

        public float panelXMoveSpeed = 100f;
        public float panelYMoveSpeed = 100f;
        public float panelXOffsetTarget = 0f;
        public float panelYOffsetTarget = 0f;

        protected override string PanelPrefabName => "Prefabs/UI/InventoryPanel";
        protected override string ItemPrefabName => "Prefabs/UI/InventoryItem";

        public override bool IsShow => _showFlag;

        public InventoryViewPanel()
        {
            SetLocalPosition(panel.transform.localPosition.x, panel.transform.localPosition.y);
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

            panel.transform.localPosition = new Vector3(initX + panelXOffset, initY + panelYOffset, 0f);
        }

        public override void SetLocalPosition(float x, float y)
        {
            panel.transform.localPosition = new Vector3(x, y, 0);
            initX = x;
            initY = y;
        }

        public override void Show()
        {
            _showFlag = true;
            panel.SetActive(true);
            panelXOffsetTarget = 0f;
        }

        public override void Hide()
        {
            _showFlag = false;
            panelXOffsetTarget = InventoryConfig.Float.InventoryPanelHideOffsetX;
            panelXMoveSpeed = InventoryConfig.Float.InventoryPanelHideSpeed;
        }

        public void UpdatePanel(DynInventory<ItemData> model)
        {
            FillItem(model.Size);
            for (int i = 0; i < model.Size; ++i)
            {
                UpdateItem(model, i);
            }
        }

        public void UpdateItem(DynInventory<ItemData> model, int index)
        {
            var viewItem = this[index];

            if (model.HasItem(index))
            {
                var item = model[index];

                string textureName = null;
                int rarity = -1;

                if (item is EquipmentData equip)
                {
                    textureName = equip.TextureName;
                    rarity = equip.Rarity;
                }

                viewItem.TriggerImage.SetIcon(textureName);
                viewItem.TriggerImage.SetColor(rarity);
                viewItem.TriggerImage.Show();
                viewItem.InteractiveEnable = true;
            }
            else
            {
                viewItem.TriggerImage.SetIcon(null);
                viewItem.TriggerImage.Hide();
                viewItem.TriggerImage.HideColor();
                viewItem.InteractiveEnable = false;
            }
        }
    }
}
