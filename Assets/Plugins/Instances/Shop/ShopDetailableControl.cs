using GameBase.Shops;
using GameBase.Tools;
using GameBase.UI;
using Instance.UI.MVC;
using UnityEngine;

namespace Instance.UI.Shops
{
    public class ShopDetailableControl : IDetailableControl<BaseViewItem>
    {
        private IDetailableShadowView _shadowView = new DefaultDetailableShadowView();
        internal ShopModel model;
        public ShopDetailableControl(ShopModel model)
        {
            this.model = model;
        }


        bool IDetailableControl<BaseViewItem>.IsDetail(BaseViewItem e)
        {
            return e.Obj.EnterTime > 0.2f && model.GetGoodID(e.ItemIndex) >= 0;
        }

        void IDetailableControl<BaseViewItem>.OnDetail(BaseViewItem e)
        {
            _shadowView.SetPosition(Input.mousePosition);
        }

        void IDetailableControl<BaseViewItem>.OnEnterDetail(BaseViewItem e)
        {
            _shadowView.Show();

            var goodID = model.GetGoodID(e.ItemIndex);
            var info = ShopItemInfos.Get(goodID);

            if (info.reflectType == ReflectType.InventoryItem)
            {
                _shadowView.SetText("装备\n" + InventoryDataBase.GetText(info.reflectID));
            }
            else if (info.reflectType == ReflectType.Buff)
            {
                _shadowView.SetText("成长\n" + InventoryDataBase.GetText(info.reflectID));
            }
            else
            {
                _shadowView.Hide();
            }
        }

        void IDetailableControl<BaseViewItem>.OnExitDetail(BaseViewItem e)
        {
            _shadowView.Hide();
        }
    }
}
