using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance.Inventory
{
    public class CommonInventory
    {
        public const int ITEM_NUM = 50;
        public const float PANEL_OFFSET_HIDE_SPEED = 6000;
        public const float PANEL_OFFSET_HIDE_X = 1920;

        private static CommonInventory _instance = new();
        private Inventory<EquipmentItem, CommonDataBase> _inventory = new();
        private InventoryPanel _panel;
        internal LinearNonReleaseEntityContainer<InventoryItem> container = new();
        private bool _showFlag = false;

        private CommonInventory()
        {
            _panel = InventoryPanel.Instance;
            _panel.Container = container;
            _panel.Dragable = new CommonDragable();
            _panel.panel.SetActive(false);
            for (int i = 0; i < ITEM_NUM; i++)
            {
                var e = _panel.NewEntity();
            }
        }

        public static CommonInventory Instance => _instance;

        public void SetIconSprite(int index, int iconID)
        {
            var texture = ResourcesLoader.GetTexture2D(iconID);
            container[index].IconSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        public InventoryItem GetItem(Vector3 position)
        {
            return _panel.GetItem(position);
        }

        public InventoryItem this[int i]
        {
            get => container[i];
        }

        public void AddItem(EquipmentItem item)
        {
            _inventory.AddItem(item);
        }

        public void RemoveItem(int position)
        {
            _inventory.RemoveItem(position);
        }

        public void SwapItem(int p1, int p2)
        {
            _inventory.Swap(p1, p2);
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
