using GameBase.Buffs;
using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.UI;
using Instance.Buffs;
using UnityEngine;

namespace Instance.MVC
{
    public class EquipmentInventoryController : InventoryController<InventoryData,
        EquipmentViewItem,
        EquipmentViewPanel,
        EquipmentInventoryController>
    {
        public IBuffOwner owner;

        private EquipmentInventoryModel _inventoryModel = new();

        public EquipmentInventoryController()
        {
            View.DragableControl = new EquipmentDragableControl();
            View.DetailableControl = new EquipmentDetailControl();

            Size = 6;
        }

        protected override IInventoryModel<InventoryData> Model => _inventoryModel;
        protected override EquipmentViewPanel View => EquipmentViewPanel.Instance;
        protected override IDataBase<InventoryData> DataBase => CommonDataBase.Instance;

        public override int AddItem(InventoryData item, int index)
        {
            base.AddItem(item, index);
            owner.RegisterBuff(_inventoryModel.GetBuff(index));
            return index;
        }

        public override void RemoveItem(int position)
        {
            owner.RemoveBuff(_inventoryModel.GetBuff(position));
            base.RemoveItem(position);
        }
    }
}
