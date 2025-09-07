using GameBase.Buffs;
using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.UI;
using Instance.Buffs;
using UnityEngine;

namespace Instance.Inventory
{
    public class EquipmentInventoryController : InventoryController<CommonItemData,
        EquipmentViewItem,
        EquipmentViewPanel,
        EquipmentInventoryController>
    {
        public const int ITEM_NUM = 6;
        public IBuffOwner owner;

        private EquipmentInventoryModel _inventoryModel = new(ITEM_NUM);

        public EquipmentInventoryController()
        {
            View.DragableControl = new EquipmentDragableControl();
            View.DetailableControl = new EquipmentDetailControl();
            for (int i = 0; i < ITEM_NUM; i++)
            {
                var e = View.NewEntity();
            }
        }

        protected override InventoryModel<CommonItemData> Model => _inventoryModel;
        protected override EquipmentViewPanel View => EquipmentViewPanel.Instance;
        protected override IDataBase<CommonItemData> DataBase => CommonDataBase.Instance;

        public override void AddItem(CommonItemData item, int index)
        {
            base.AddItem(item, index);
            owner.RegisterBuff(_inventoryModel.GetBuff(index));
        }

        public override void RemoveItem(int position)
        {
            owner.RemoveBuff(_inventoryModel.GetBuff(position));
            base.RemoveItem(position);
        }

        protected override void SetIcon(CommonItemData modelData, EquipmentViewItem viewItem)
        {
            var texture = ResourcesLoader.GetTexture2D(modelData.iconTextureID);
            viewItem.IconSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}
