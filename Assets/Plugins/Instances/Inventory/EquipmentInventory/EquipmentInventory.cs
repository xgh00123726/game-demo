using GameBase.Buffs;
using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.UI;
using Instance.Buffs;
using UnityEngine;

namespace Instance.Inventory
{
    public class EquipmentInventory
    {
        public const int ITEM_NUM = 6;

        private static EquipmentInventory _instance = new();
        public static EquipmentInventory Instance => _instance;
        private Inventory<EquipmentItem, EquipmentDataBase> _inventory = new();
        private EquipmentPanel _panel;
        internal LinearNonReleaseEntityContainer<GameBase.UI.EquipmentItem> container = new();

        private EquipmentInventory()
        {
            _panel = EquipmentPanel.Instance;
            _panel.Container = container;
            _panel.Dragable = new EquipmentDragable();
            for (int i = 0; i < ITEM_NUM; i++)
            {
                var e = _panel.NewEntity();
            }
        }

        public void SetIconSprite(int index, int iconID)
        {
            var texture = ResourcesLoader.GetTexture2D(iconID);
            container[index].IconSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        public GameBase.UI.EquipmentItem GetItem(Vector3 position)
        {
            return _panel.GetItem(position);
        }

        public GameBase.UI.EquipmentItem this[int i]
        {
            get => container[i];
        }

        public void SetItem(Buff e, int position)
        {
            var ee = this[position];
            SetIconSprite(position, e.textureID);
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
    }
}
