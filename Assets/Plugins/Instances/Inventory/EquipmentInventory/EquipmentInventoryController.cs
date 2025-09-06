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
        private EquipmentInventoryModel _inventoryModel = new(ITEM_NUM);
        private EquipmentViewPanel _panel;
        private LinearNonReleaseEntityContainer<EquipmentViewItem> _container = new();

        private EquipmentInventoryController()
        {
            _panel = EquipmentViewPanel.Instance;
            _panel.Container = _container;
            _panel.DragableControl = new EquipmentDragableControl();
            _panel.DetailableControl = new EquipmentDetailControl();
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

        public bool TryGetDataOfView(EquipmentViewItem item, out CommonItemData data)
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

        public bool HasItemData(int index)
        {
            return _inventoryModel.HasItem(index);
        }

        public CommonItemData GetItemData(int index)
        {
            return _inventoryModel[index];
        }

        public int TryGetItemUI(Vector3 position, out EquipmentViewItem e)
        {
            return _panel.TryGetItem(position, out e);
        }

        public Buff GetItemBuff(int index)
        {
            return _inventoryModel.GetBuff(index);
        }

        public void AddItem(CommonItemData item)
        {
            _inventoryModel.AddItem(item);
        }

        public void RemoveItem(int position)
        {
            _inventoryModel.RemoveItem(position);
        }

        public void SwapItem(int p1, int p2)
        {
            _inventoryModel.Swap(p1, p2);
        }
    }
}
