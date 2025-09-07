using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using System;
using UnityEditor;
using UnityEngine;

namespace Instance.Inventory
{
    public class CommonInventoryController : InventoryController<CommonItemData,
        CommonInventoryModel,
        CommonInventoryViewItem,
        CommonInventoryViewPanel,
        CommonInventoryController>
    {
        public const int ITEM_NUM = 50;
        public const float PANEL_OFFSET_HIDE_SPEED = 6000;
        public const float PANEL_OFFSET_HIDE_X = 1920;


        private CommonInventoryModel _inventoryModel = new()
        {
            Size = ITEM_NUM
        };
        protected override CommonInventoryModel Model => _inventoryModel;
        protected override CommonInventoryViewPanel View => CommonInventoryViewPanel.Instance;
        private bool _showFlag = false;

        public CommonInventoryController()
        {
            View.DragableControl = new CommonDragableControl();
            View.DetailableControl = new CommonDetailControl();
            View.panel.SetActive(false);
            for (int i = 0; i < ITEM_NUM; i++)
            {
                var e = View.NewEntity();
            }
        }

        public void Show()
        {
            View.panel.SetActive(true);
            _showFlag = true;
            View.panelXOffsetTarget = 0;
        }
        public void Hide()
        {
            _showFlag = false;
            View.panelXOffsetTarget = PANEL_OFFSET_HIDE_X;
            View.panelXMoveSpeed = PANEL_OFFSET_HIDE_SPEED;
        }
        public void Toggle()
        {
            if (_showFlag)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        protected override void SetIcon(CommonItemData modelData, CommonInventoryViewItem viewItem)
        {
            var texture = ResourcesLoader.GetTexture2D(modelData.iconTextureID);
            viewItem.IconSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}
