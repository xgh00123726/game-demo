using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance.Inventory
{
    public class CommonInventoryController
    {
        public const int ITEM_NUM = 50;
        public const float PANEL_OFFSET_HIDE_SPEED = 6000;
        public const float PANEL_OFFSET_HIDE_X = 1920;

        private InventoryViewPanel _panel;
        private CommonInventoryModel _inventoryModel = new();
        private static CommonInventoryController _instance = new();
        private LinearNonReleaseEntityContainer<InventoryViewItem> _container = new();
        private bool _showFlag = false;

        private CommonInventoryController()
        {
            _panel = InventoryViewPanel.Instance;
            _panel.Container = _container;
            _panel.DragableControl = new CommonDragableControl();
            _panel.DetailableControl = new CommonDetailControl();
            _panel.panel.SetActive(false);
            for (int i = 0; i < ITEM_NUM; i++)
            {
                var e = _panel.NewEntity();
            }
        }

        public static CommonInventoryController Instance => _instance;

        public bool TryGetDataOfView(InventoryViewItem item, out CommonItemData data)
        {
            var index = _container.IndexOf(item);
            var ret = HasItemData(index);

            if (ret)
            {
                data = _inventoryModel[index];
            }
            else
            {
                data = default;
            }

            return ret;
        }

        public void SetIconSprite(int index, int iconID)
        {
            var texture = ResourcesLoader.GetTexture2D(iconID);
            _container[index].IconSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        public int TryGetItemUI(Vector3 position, out InventoryViewItem e)
        {
            return _panel.TryGetItem(position, out e);
        }

        public bool HasItemData(int index)
        {
            return _inventoryModel.HasItem(index);
        }

        public CommonItemData GetItemData(int index)
        {
            return _inventoryModel[index];
        }

        public InventoryViewItem GetItemUI(int index)
        {
            return _container[index];
        }

        public void AddItem(CommonItemData item)
        {
            var index = _inventoryModel.AddItem(item);
            SetIconSprite(index, item.iconTextureID);
        }

        public void RemoveItem(int position)
        {
            _inventoryModel.RemoveItem(position);
        }

        public void SwapItem(int p1, int p2)
        {
            _inventoryModel.Swap(p1, p2);
        }

        public void Show()
        {
            _panel.panel.SetActive(true);
            _showFlag = true;
            _panel.panelXOffsetTarget = 0;
        }
        public void Hide()
        {
            _showFlag = false;
            _panel.panelXOffsetTarget = PANEL_OFFSET_HIDE_X;
            _panel.panelXMoveSpeed = PANEL_OFFSET_HIDE_SPEED;
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
    }
}
