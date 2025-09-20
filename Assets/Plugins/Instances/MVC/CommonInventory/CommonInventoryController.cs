using GameBase.Config;
using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.UI;
using UnityEngine;

namespace Instance.UI.MVC
{
    public class CommonInventoryController : InventoryController<InventoryData,
        CommonInventoryViewItem,
        CommonInventoryViewPanel,
        CommonInventoryController>
    {
        private bool _showFlag = false;

        private CommonInventoryModel _inventoryModel = new();
        protected override IMVCModel<InventoryData> Model => _inventoryModel;
        protected override CommonInventoryViewPanel View => CommonInventoryViewPanel.Instance;
        protected override IDataBase<InventoryData> DataBase => InventoryDataBase.Instance;
        public bool IsShow => _showFlag;

        public CommonInventoryController()
        {
            View.DragableControl = new CommonDragableControl();
            View.DetailableControl = new CommonDetailControl();
            View.EnterExitControl = new CommonFixedDetailableControl();
            View.panel.SetActive(false);
            
            Size = InventoryConfig.Int.InventoryPageCapacity;
        }

        public override void Show()
        {
            _showFlag = true;
            View.panelXOffsetTarget = 0;
            View.panel.SetActive(true);
        }
        public override void Hide()
        {
            _showFlag = false;
            View.panelXOffsetTarget = InventoryConfig.Float.InventoryPanelHideOffsetX;
            View.panelXMoveSpeed = InventoryConfig.Float.InventoryPanelHideSpeed;
            CommonFixedDetailableShadowView.Instance.Hide();
        }
        public override void Toggle()
        {
            if (_showFlag)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
}
