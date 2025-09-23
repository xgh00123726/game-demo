using GameBase.Buffs;
using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.UI;
using Instance.Buffs;
using UnityEngine;
using GameBase.UI.MVC;

namespace Instance.UI.MVC
{
    public class EquipmentInventoryController : InventoryController<InventoryData,
        EquipmentViewItem,
        EquipmentViewPanel,
        EquipmentInventoryController>
    {
        private EquipmentInventoryModel _inventoryModel = new();

        public EquipmentInventoryController()
        {
            View.DragableControl = new EquipmentDragableControl();
            View.DetailableControl = new EquipmentDetailControl();

            Size = 6;
        }

        protected override IMVCModel<InventoryData> Model => _inventoryModel;
        protected override EquipmentViewPanel View => EquipmentViewPanel.Instance;

        public void SetOwner<T_Owner>(T_Owner owner) where T_Owner : IBuffOwner
        {
            _inventoryModel.owner = owner;
        }
    }
}
