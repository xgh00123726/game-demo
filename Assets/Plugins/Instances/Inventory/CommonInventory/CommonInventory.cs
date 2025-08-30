using GameBase.Inventorys;
using GameBase.Tools;
using GameBase.UI;

namespace Instance.Inventory
{
    public class CommonInventory
    {
        public const int ITEM_NUM = 50;
        private static CommonInventory _instance = new();
        public static CommonInventory Instance => _instance;
        private Inventory<CommonItem, CommonDataBase> _inventory;
        private InventoryPanel _panel;


        private CommonInventory()
        {
            _panel = InventoryPanel.Instance;
            for (int i = 0; i < ITEM_NUM; i++)
            {
                _panel.NewEntity();
            }
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
        }
        public void Hide()
        {
            _panel.panel.SetActive(false);
        }
        public void Toggle()
        {
            _panel.panel.SetActive(!_panel.panel.activeSelf);
        }
    }
}
