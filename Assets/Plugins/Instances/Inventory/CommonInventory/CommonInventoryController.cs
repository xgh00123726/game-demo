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
        CommonInventoryViewItem,
        CommonInventoryViewPanel,
        CommonInventoryController>
    {
        private bool _showFlag = false;

        public const int ITEM_NUM = 50;
        public const float PANEL_OFFSET_HIDE_SPEED = 6000;
        public const float PANEL_OFFSET_HIDE_X = 1920;


        private CommonInventoryModel _inventoryModel = new()
        {
            Size = ITEM_NUM
        };
        protected override InventoryModel<CommonItemData> Model => _inventoryModel;
        protected override CommonInventoryViewPanel View => CommonInventoryViewPanel.Instance;
        protected override IDataBase<CommonItemData> DataBase => CommonDataBase.Instance;
        public bool IsShow => _showFlag;
        

        public CommonInventoryController()
        {
            View.DragableControl = new CommonDragableControl();
            View.DetailableControl = new CommonDetailControl();
            View.EnterExistControl = new CommonFixedDetailableControl();
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
            CommonFixedDetailableShadowView.Instance.Hide();
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
