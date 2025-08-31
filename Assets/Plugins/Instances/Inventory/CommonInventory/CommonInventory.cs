using GameBase.Inventorys;
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
        public static CommonInventory Instance => _instance;
        private Inventory<CommonItem, CommonDataBase> _inventory;
        private InventoryPanel _panel;
        internal LinearNonReleaseEntityContainer<InventoryItem> container = new();
        private bool _showFlag = false;

        private CommonInventory()
        {
            _panel = InventoryPanel.Instance;
            _panel.Container = container;
            _panel.panel.SetActive(false);
            for (int i = 0; i < ITEM_NUM; i++)
            {
                var e = _panel.NewEntity();
                e.dragable = new CommonDragable();
            }
            var go = new GameObject("CommonInventoryGizmos");
            var gizmos = go.AddComponent<InventoryGizmos>();
            gizmos.Init(_panel);
        }

        public InventoryItem GetItem(Vector3 position)
        {
            Vector2 mousePosition = new Vector2(position.x, position.y);
            foreach (var item in container)
            {
                var r = item.rectTransform.rect;
                r.center = item.Obj.transform.position;
                if (r.Contains(mousePosition))
                {
                    return item;
                }
            }
            return null;
        }

        public InventoryItem this[int i]
        {
            get => container[i];
        }

        public void AddItem(CommonItem item)
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
