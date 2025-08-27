using GameBase.Tools;
using GameBase.UI;

namespace Instance.UI
{
    public class InventoryUI
    {
        public const int ITEM_NUM = 50;
        private static InventoryUI _instance = new();
        public static InventoryUI Instance => _instance;
        private InventoryPanel _panel;


        private InventoryUI()
        {
            _panel = InventoryPanel.Instance;
            for (int i = 0; i < ITEM_NUM; i++)
            {
                _panel.NewEntity<InventoryItem>();
            }
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
