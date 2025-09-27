using GameBase.Config;
using GameBase.Infos;
using GameBase.Inventorys;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance
{
    public class CommonInventoryViewPanel : BaseViewPanel<CommonInventoryViewItem>
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

        private static CommonInventoryViewPanel _instance = new CommonInventoryViewPanel();
        public static CommonInventoryViewPanel Instance => _instance;

        public override bool IsShow => _showFlag;

        private DefaultFixedDetailableView _fixedDetailableShadowView;

        public DefaultFixedDetailableView FixedDetailableShadowView
        {
            set
            {
                _fixedDetailableShadowView = value;
                value.shadowObj.transform.SetParent(panel.transform, true);
            }
        }

        public CommonInventoryViewPanel(int prefabID = 35,
            int defaultObjID = 34) : base(
            prefabID,
            defaultObjID)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("error");
            }
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
            _fixedDetailableShadowView.Show();
            panel.SetActive(true);
            panelXOffsetTarget = 0f;
        }

        public override void Hide()
        {
            _showFlag = false;
            panelXOffsetTarget = InventoryConfig.Float.InventoryPanelHideOffsetX;
            panelXMoveSpeed = InventoryConfig.Float.InventoryPanelHideSpeed;
            _fixedDetailableShadowView.Hide();
        }

        public void UpdateInventory(DynInventory<CommonInventoryData> inventory)
        {
            FillItem(inventory.Size);
            for (int i = 0; i < inventory.Size; ++i)
            {
                UpdateInventoryItem(inventory, i);
            }
        }

        public void UpdateInventoryItem(DynInventory<CommonInventoryData> inventory, int index)
        {
            var viewItem = this[index];

            if (inventory.HasItem(index))
            {
                var data = inventory[index]; 

                viewItem.triggerImage.SetIcon(data.iconTextureID);
                viewItem.triggerImage.SetColor(data.rarity);
                viewItem.triggerImage.Show();
            }
            else
            {
                viewItem.triggerImage.SetIcon(-1);
                viewItem.triggerImage.Hide();
                viewItem.triggerImage.HideColor();
            }

        }
    }
}
