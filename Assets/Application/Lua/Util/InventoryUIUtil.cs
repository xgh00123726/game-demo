using GameBase.Inventorys;
using GameBase.LifeTime;
using GameBase.Tools;
using GameBase.UI;
using Instance;

namespace LuaUtil
{
    public static class InventoryUIUtil
    {
        private static InventoryViewPanel _panel;
        private static DynInventory<InventoryData> _inventory;
        private static DefaultFixedDetailableView _inventoryFixedDetailableShadowView;

        public static void Init()
        {
            _panel = new InventoryViewPanel();
            _inventory = InventoryUtil.Inventory;
            _inventoryFixedDetailableShadowView = new DefaultFixedDetailableView();

            _panel.detailableControl = new InventoryDetailControl(_inventory, _panel, new DefaultDetailableView());
            _panel.dragableControl = new InventoryDragableControl(_inventory, _panel);
            _panel.FixedDetailableShadowView = _inventoryFixedDetailableShadowView;
        }

        public static void SetLayout(float xInterval, float yInterval, float width, float height, int align)
        {
            _panel.layout = new DefaultLayout()
            {
                xInterval = xInterval,
                yInterval = yInterval,
                width = width,
                height = height,
                align = (AlignType)align
            };
        }

        public static UICmd GetInputCmd()
        {
            if (Inputs.GetKeyDown(KeyFunction.ToggleAttrPanel, "inventory"))
            {
                return UICmd.Toggle;
            }

            return UICmd.None;
        }

        public static void TogglePanel()
        {
            _panel.Toggle();
            if (_panel.IsShow)
            {
                Inputs.LockOthers("inventory");
            }
            else
            {
                Inputs.ReleaseAll();
                SpellActionModifierViewPanel.Instance?.Hide();
            }
        }

        public static void UpdateItem(int index)
        {
            _panel.UpdateItem(_inventory, index);
        }

        public static void UpdatePanel()
        {
            _panel.UpdatePanel(_inventory);
        }
    }
}
