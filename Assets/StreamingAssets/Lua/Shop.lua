require("ShopData")

local ShopUtil = CS.LuaUtil.ShopUtil

local function Init()
    ShopUtil.Init()
end

local function GenInitShop()
    local data = ShopData.Shop1

    local shopID = ShopUtil.Gen(data.DataFile, data.PrefabID, data.Position.x, data.Position.z)
    ShopUtil.SetGoodsNum(shopID, data.GoodsNum)
    ShopUtil.RefreshShop(shopID)
end

local function GetShopNearestCreature( creatureID )
    return ShopUtil.GetShopNearestCreature(creatureID, ShopData.ShopDetectRange)
end

Shop = {
    Enum = {
        --- item type of shop
        ItemType = CS.GameBase.Inventorys.ShopItemType,
    },
    --- @nopara
    Init = Init,

    --- @nopara
    GenInitShop = GenInitShop,

    --- @arg1 shopID : int 
    --- @arg2 itemPosition : int
    --- @ret itemInfo : Shop.Enum.ItemType
    GetItemInfo = ShopUtil.GetItemInfo,

    --- @arg1 shopID : int
    --- @arg2 index : int
    RemoveItem = ShopUtil.RemoveShopItem,

    --- @arg1 shopID : int
    RefreshShop = ShopUtil.RefreshShop,

    --- @arg1 creatureID : int
    --- @arg2 rangeLimit : float
    --- @ret shopID : int
    GetShopNearestCreature = GetShopNearestCreature,
}