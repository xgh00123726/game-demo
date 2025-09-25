using GameBase.Inventorys;
using GameBase.Tools;
using GameBase.UI;
using GameBase.Resources;
using UnityEngine;
using Instance;
using Instance.UI.Shops;

public partial class Player
{
    public int Gold { get; set; } = 100;

    private ShopNearView _shopNearView;
    private ShopViewPanel _shopView;

    //bool IShoper.OpenShop => Inputs.GetKeyDown(KeyFunction.OpenShop, "shopping");
    //bool IShoper.CloseShop => Inputs.GetKeyDown(KeyFunction.CloseShop, "shopping") || Inputs.GetKeyDown(KeyFunction.Cancel, "shopping");

    //void IShoper.OnPurchaseItem(int goodID)
    //{
    //    var info = ShopDataBase.Get(goodID);

    //    if (info.reflectType == ReflectType.InventoryItem)
    //    {
    //        inventoryController.Add(InventoryDataBase.Get(info.reflectID));
    //    }
    //    else if (info.reflectType == ReflectType.Buff)
    //    {
    //        Constructor.Buffs.Factory.Instance.Get(Constructor.Buffs.Type.Common,
    //            info.reflectID).AddTo(character);
    //    }
    //}

    /// <summary>
    /// 在玩家旁边生成一个初始商店
    /// </summary>
    private void GenerateInitShop()
    {
        _shopView = ShopViewPanel.Instance;
        _shopNearView = new ShopNearView();
        var shopModel = new ShopInventory();
        var shopObject = GameObject.Instantiate(ResourcesLoader.GetPrefab(1));
        var shopController = new ShopController(shopModel, shopObject);

        shopObject.transform.position = character.Position + new Vector3(3, 0, 0);

        shopModel.BindFile("CommonShop_1.csv");

        _shopView.detailableControl = new ShopDetailableControl(shopModel, shopController);
        _shopView.EnterExitControl = new ShopEnterExitControl(shopController);
        _shopView.SetRefreshIconController(new ShopRefreshIconControll(shopController));
        _shopView.layout = new DefaultLayout()
        {
            xInterval = 180
        };
        _shopView.SetLocalPosition(-300, 200);

        
        shopController.Size = 5;
        shopController.Refresh();
    }

    public void ShopUpdate()
    {
        var shop = ShopMgr.NearestShop(character.Position);
        if (shop != null)
        {
            _shopNearView.Show(shop.Position);

            if (!shop.IsShow && Inputs.GetKeyDown(KeyFunction.OpenShop, "shopping"))
            {
                shop.Show();
            }
            else if (shop.IsShow && Inputs.GetKeyDown(KeyFunction.CloseShop, "shopping") || Inputs.GetKeyDown(KeyFunction.Cancel, "shopping"))
            {
                shop.Hide();
            }
        }
        else
        {
            _shopNearView.Hide();
            _shopView.Hide();
        }
    }
}
