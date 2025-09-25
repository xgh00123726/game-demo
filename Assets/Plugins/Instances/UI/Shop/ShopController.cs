using GameBase.Resources;
using GameBase.UI;
using UnityEngine;
using GameBase.Tools;
using System.Collections.Generic;
using Instance.UI.MVC;
using GameBase.Inventorys;
using Instance;

namespace GameBase.UI
{
    public class ShopController : InventoryController<ShopViewItem, int>
    {
        protected new ShopViewPanel _view;
        protected new ShopInventory _model;
        protected GameObject _obj;

        public Vector3 Position => _obj.transform.position;
        public ShopController(ShopInventory model, GameObject shopObj) : base(ShopViewPanel.Instance, model)
        {
            _view = ShopViewPanel.Instance;
            _model = model;
            _obj = shopObj;
            ShopMgr.Register(shopObj, this);
        }

        protected override void SetViewItem(int data, ShopViewItem viewItem)
        {
            var info = _model.GetItemInfoFromShopDataIndex(data);

            if (info.iconTextureID < 0 && info.reflectType == ReflectType.InventoryItem)
            {
                viewItem.triggerImage.SetIcon(CommonInventoryDataBase.Get(info.reflectID).iconTextureID);
                viewItem.identifyImage.SetIcon(62);
            }
            else
            {
                viewItem.triggerImage.SetIcon(info.iconTextureID);
                viewItem.identifyImage.SetIcon(61);
            }
            viewItem.Value = info.price;
            viewItem.obj.SetActive(true);
            viewItem.triggerImage.SetColor(info.rarity);
            viewItem.triggerImage.Show();
        }
        

        public void Refresh()
        {
            _model.Refresh();
            RefreshView();
        }

    }
}
