using GameBase.Shops;
using GameBase.UI;
using Instance.UI.MVC;
using UnityEngine;

namespace Instance.UI.Shops
{
    public class ShopDetailableControl : IDetailableControl<ShopViewItem>
    {
        public ShopModel model;

        bool IDetailableControl<ShopViewItem>.IsDetail(ShopViewItem e)
        {
            return e.Obj.EnterTime > 0.2f && model.GetGoodID(e.ItemIndex) >= 0;
        }

        void IDetailableControl<ShopViewItem>.OnDetail(ShopViewItem e)
        {
            ShopDetailShadowView.Instance.SetPosition(Input.mousePosition);
        }

        void IDetailableControl<ShopViewItem>.OnEnterDetail(ShopViewItem e)
        {
            ShopDetailShadowView.Instance.Show();

            var goodID = model.GetGoodID(e.ItemIndex);
            var info = ShopItemInfos.Get(goodID);

            if (info.reflectType == ReflectType.InventoryItem)
            {
                ShopDetailShadowView.Instance.SetText("装备\n" + InventoryDataBase.GetText(info.reflectID));
            }
            else if (info.reflectType == ReflectType.Buff)
            {
                ShopDetailShadowView.Instance.SetText("成长\n" + InventoryDataBase.GetText(info.reflectID));
            }
            else
            {
                ShopDetailShadowView.Instance.Hide();
            }
        }

        void IDetailableControl<ShopViewItem>.OnExitDetail(ShopViewItem e)
        {
            ShopDetailShadowView.Instance.Hide();
        }
    }
}
