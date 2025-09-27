using GameBase.Inventorys;
using GameBase.UI;

namespace Instance
{
    public class ShopRefreshIconControll : IEnterExitControl
    {
        private ShopInventory _model;
        private ShopViewPanel _viewPanel;

        public ShopRefreshIconControll(ShopInventory model, ShopViewPanel viewPanel)
        {
            _model = model;
            _viewPanel = viewPanel;
        }
        void IEnterExitControl.OnPointerDown(int i)
        {
            _model.Refresh();
            _viewPanel.UpdateInventory(_model);
        }

        void IEnterExitControl.OnPointerEnter(int i)
        {
            
        }

        void IEnterExitControl.OnPointerExit(int i)
        {
            
        }

        void IEnterExitControl.OnPointerRightDown(int i)
        {
            
        }
    }
}
