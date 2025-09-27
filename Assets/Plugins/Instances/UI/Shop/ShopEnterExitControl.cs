using GameBase.Inventorys;
using GameBase.UI;

namespace Instance
{
    public class ShopEnterExitControl : IEnterExitControl
    {
        private ShopInventory _model;
        private ShopViewPanel _viewPanel;

        public ShopEnterExitControl(ShopInventory model, ShopViewPanel viewPanel)
        {
            _model = model;
            _viewPanel = viewPanel;
        }

        void IEnterExitControl.OnPointerDown(int i)
        {
            _model.Remove(i);
            _viewPanel.UpdateInventoryItem(_model, i);
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
