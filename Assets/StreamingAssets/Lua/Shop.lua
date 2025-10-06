require("ShopData")

local ShopInteracitve = CS.Instance.ShopInteractive
local ShopDataBase = CS.Instance.ShopDataBase.Instance

local function Init()
    ShopInteracitve.Instance:SetActive(true)
    ShopInteracitve.ShopInteractiveDis = ShopData.InteracitiveDistance
    ShopInteracitve.OnShopActive = Shop.OnShopActive
    ShopInteracitve.OnShopInActive = Shop.OnShopInActive
    ShopInteracitve.OnTargetNearShop = Shop.OnTargetNearShop
    ShopInteracitve.OnTargetFarFromShop = Shop.OnTargetFarFromShop
end

local function GenShop( shopTable )
    local inventory = CS.GameBase.Inventorys.ShopInventory(shopTable.DataFile)
    inventory.Size = shopTable.GoodsNum
    inventory:Refresh()
    
    local obj = CS.GameBase.Resources.ResourcesLoader.InstantiateGameObject(shopTable.PrefabID)
    local pos = CS.UnityEngine.Vector3(shopTable.Position.x, shopTable.Position.y, shopTable.Position.z)
    obj.transform.position = pos
    obj.name = "shop1"
    
    local shop = CS.Instance.Shop()
    shop.inventory = inventory
    shop.obj = obj
    shop.detectRange = shopTable.DetectRange

    ShopInteracitve.RegisterShop(shop)

    return shop
end


Shop = {
    --- @noarg
    Init = Init,

    --- @arg1 shopTable : table
    GenShop = GenShop,

    --- data
    DataBase = ShopDataBase,

    --- @arg1 target : Creature
    SetInteractiveTarget = ShopInteracitve.SetTarget,

    --- @ret currentShop : Shop
    GetCurrentShop = ShopInteracitve.GetCurrentShop,
}