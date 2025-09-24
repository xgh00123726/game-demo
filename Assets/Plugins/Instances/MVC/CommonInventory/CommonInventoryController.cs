using GameBase.Config;
using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.UI;
using GameBase.UI.MVC;
using System.Xml.Linq;
using UnityEngine;

namespace Instance.UI.MVC
{
    public class CommonInventoryController : InventoryController<CommonInventoryViewItem, InventoryData>
    {
        private bool _showFlag = false;
        protected new CommonInventoryViewPanel _view;

        public CommonInventoryController(CommonInventoryViewPanel view,
            DynInventoryModel<InventoryData> model) : base(view, model)
        {
            _view = view;
            _view.SetLocalPosition(-600, 160);
        }

        public override bool IsShow => _showFlag;

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
