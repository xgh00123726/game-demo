using GameBase.Buffs;
using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.UI;
using Instance.Buffs;
using UnityEngine;

namespace Instance.Inventory
{
    public class EquipmentInventoryController
    {
        public const int ITEM_NUM = 6;
        public IBuffOwner owner;

        private static EquipmentInventoryController _instance = new();
        private EquipmentInventoryModel _inventory = new();
        private EquipmentViewPanel _panel;
        private LinearNonReleaseEntityContainer<EquipmentViewItem> _container = new();

        private EquipmentInventoryController()
        {
            _panel = EquipmentViewPanel.Instance;
            _panel.Container = _container;
            _panel.Dragable = new EquipmentDragable();
            for (int i = 0; i < ITEM_NUM; i++)
            {
                var e = _panel.NewEntity();
            }
        }

        public static EquipmentInventoryController Instance => _instance;

        public void SetIconSprite(int index, int iconID)
        {
            var texture = ResourcesLoader.GetTexture2D(iconID);
            _container[index].IconSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        public int TryGetItemUI(Vector3 position, out EquipmentViewItem e)
        {
            return _panel.TryGetItem(position, out e);
        }

        public void AddItem(CommonItemData item)
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
    }
}
