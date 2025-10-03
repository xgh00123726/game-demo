using GameBase.Buffs;
using GameBase.Inventorys;
using GameBase.UI;
using UnityEngine;

namespace Instance
{
    public class EquipmentDragableControl : DefaultDragableControl<EquipmentItem>
    {
        private new EquipmentPanel _viewPanel;
        private CommonInventory<Buff> _model;
        private InventoryViewPanel _commonViewPanel;

        public EquipmentDragableControl(EquipmentPanel viewPanel, CommonInventory<Buff> model,
            InventoryViewPanel commonInventoryViewPanel) : base(viewPanel)
        {
            _viewPanel = viewPanel;
            _model = model;
            _commonViewPanel = commonInventoryViewPanel;
        }

        protected override void OnExitDragOver(int dragedIndex, int dragedOverIndex)
        {
            _model.Swap(dragedIndex, dragedOverIndex);
            base.OnExitDragOver(dragedIndex, dragedOverIndex);
        }

        protected override void OnExitDragOut(int dragedIndex)
        {
            var commonItem = _commonViewPanel.GetItemFromTriggerPosition(Input.mousePosition);
            if (commonItem != null)
            {

            }
            else
            {
                base.OnExitDragOut(dragedIndex);
            }
        }
    }
}
