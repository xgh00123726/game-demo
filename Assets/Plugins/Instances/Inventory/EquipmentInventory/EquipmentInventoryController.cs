using GameBase.Buffs;
using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.UI;
using Instance.Buffs;
using UnityEngine;

namespace Instance.Inventory
{
    public class EquipmentInventoryController : InventoryController<CommonItemData,
        EquipmentInventoryModel,
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

        protected override EquipmentInventoryModel Model => _inventoryModel;
        protected override EquipmentViewPanel View => EquipmentViewPanel.Instance;

        public Buff GetItemBuff(int index)
        {
            return _inventoryModel.GetBuff(index);
        }
    }
}
