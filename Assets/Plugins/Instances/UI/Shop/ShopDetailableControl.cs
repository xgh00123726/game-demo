using GameBase.Inventorys;
using GameBase.Tools;
using GameBase.UI;
using Instance.UI.MVC;
using UnityEngine;

namespace Instance.UI.Shops
{
    public class ShopDetailableControl : IDetailableControl
    {
        private IDetailableShadowView _shadowView = new DefaultDetailableShadowView();
        private ShopController _controller;
        private ShopViewPanel _viewPanel;
        internal ShopInventory model;
        public ShopDetailableControl(ShopInventory model, ShopController controller)
        {
            this.model = model;
            this._viewPanel = ShopViewPanel.Instance;
            _controller = controller;
        }


        bool IDetailableControl.IsDetail(int i)
        {
            var e = _viewPanel[i];
            return e.uiScript.EnterTime > 0.2f && model.HasItem(e.ItemIndex) && _controller.IsShow && e.obj.activeSelf;
        }

        void IDetailableControl.OnDetail(int i)
        {
            _shadowView.SetPosition(Input.mousePosition);
        }

        void IDetailableControl.OnEnterDetail(int i)
        {
            _shadowView.Show();

            var info = model.GetItemInfoFromShoppingPosition(i);

            if (info.reflectType == ReflectType.InventoryItem)
            {
                _shadowView.SetText("装备\n" + CommonInventoryDataBase.GetText(info.reflectID));
            }
            else if (info.reflectType == ReflectType.Buff)
            {
                _shadowView.SetText("成长\n" + CommonInventoryDataBase.GetText(info.reflectID));
            }
            else
            {
                _shadowView.Hide();
            }
        }

        void IDetailableControl.OnExitDetail(int i)
        {
            _shadowView.Hide();
        }
    }
}
