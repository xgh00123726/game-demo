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

local function GenShop( dataTable )
    local inventory = CS.GameBase.Inventorys.ShopInventory(dataTable.DataFile)
    inventory.Size = dataTable.GoodsNum
    inventory:Refresh()

    local obj = CS.GameBase.Resources.ResourcesLoader.InstantiateGameObject(dataTable.PrefabName)
    local pos = CS.UnityEngine.Vector3(dataTable.Position.x, dataTable.Position.y, dataTable.Position.z)
    obj.transform.position = pos
    obj.name = dataTable.Name

    local shop = CS.Instance.Shop()
    shop.inventory = inventory
    shop.obj = obj
    shop.detectRange = dataTable.DetectRange

    ShopInteracitve.RegisterShop(shop)

    return shop
end

local function GenShopInventory( dataTable )
    local inventory = CS.GameBase.Inventorys.ShopInventory(dataTable.DataFile)
    inventory.Size = dataTable.GoodsNum
    inventory:Refresh()

    return inventory
end

Shop = {
    --- @noarg
    Init = Init,

    --- @arg1 dataTable : table
    GenShop = GenShop,

    --- data
    DataBase = ShopDataBase,

    --- @arg1 target : Creature
    SetInteractiveTarget = ShopInteracitve.SetTarget,

    --- @ret currentShop : Shop
    GetCurrentShop = ShopInteracitve.GetCurrentShop,

    --- @arg1 dataTable : table
    GenShopInventory = GenShopInventory,
}