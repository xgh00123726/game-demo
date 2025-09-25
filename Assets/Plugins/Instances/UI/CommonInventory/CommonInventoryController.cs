using GameBase.Config;
using GameBase.Inventorys;
using GameBase.UI;

namespace Instance
{
    public class CommonInventoryController : InventoryController<CommonInventoryViewItem, CommonInventoryData>
    {
        private bool _showFlag = false;
        private new CommonInventoryViewPanel _view;

        public CommonInventoryController(DynInventory<CommonInventoryData> model) : base(CommonInventoryViewPanel.Instance, model)
        {
            _view = CommonInventoryViewPanel.Instance;
            _view.SetLocalPosition(-600, 160);
        }

        public override bool IsShow => _showFlag;

        protected override void SetViewItem(CommonInventoryData data, CommonInventoryViewItem viewItem)
        {
            viewItem.triggerImage.SetIcon(data.iconTextureID);
            viewItem.triggerImage.SetColor(data.rarity);
            viewItem.triggerImage.Show();
        }

        public override void Show()
        {
            _showFlag = true;
            _view.panelXOffsetTarget = 0;
            _view.Show();
        }
        public override void Hide()
        {
            _showFlag = false;
            _view.panelXOffsetTarget = InventoryConfig.Float.InventoryPanelHideOffsetX;
            _view.panelXMoveSpeed = InventoryConfig.Float.InventoryPanelHideSpeed;
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
