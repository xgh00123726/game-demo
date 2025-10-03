require("Creature")
require("UI")
require("Inventory")
require("Player")
require("Shop")
require("Buff")
require("Modify")

local M = {
    PlayerID = -1,
    CurrentShopID = -1,
    IsShopOpen = false
}

--- 初始化完成时调用，lua入口函数
function OnInitOK()
    M.PlayerID = Player.GetPlayerID()

    Inventory.Init()
    Inventory.GenInitItem()

    Creature.GenInitEnemy()

    Shop.Init()
    Shop.GenInitShop()

    UI.AttrUIInit()
    UI.InventoryUIInit()
    UI.ShopUIInit()

    print("lua init ok")
end

local function ShopUpdate()
    local nearstShopID = Shop.GetShopNearestCreature(M.PlayerID)
    M.CurrentShopID = nearstShopID
    if (nearstShopID < 0) then
        UI.Shop.HidePanel()
        UI.Shop.HideIndicator()
    else
        UI.Shop.ShowIndicator(nearstShopID)

        local inputCmd = UI.Shop.GetInputCmd()

        if (inputCmd == UI.Enum.Cmd.Show) then
            Shop.RefreshShop()
            UI.Shop.UpdatePanel(nearstShopID)
            UI.Shop.ShowPanel()
        elseif (inputCmd == UI.Enum.Cmd.Hide) then
            UI.Shop.HidePanel()
        end
    end
end

local function InventoryUpdate()
    local inputCmd = UI.Inventory.GetInputCmd()

    if (inputCmd == UI.Enum.Cmd.Toggle) then
        UI.Inventory.TogglePanel()
    end
end

local function AttrUpdate()
    UI.Attr.SetValue(M.PlayerID)
end

function OnShopItemClicked( index )
    local shopID = M.CurrentShopID

    if (shopID < 0) then
        return
    else
        local info = Shop.GetItemInfo(shopID, index)
        if (info.type == Shop.Enum.ItemType.Buff) then
            Buff.AddBuffToCreature(info.id, M.PlayerID, 5)
        elseif (info.type == Shop.Enum.ItemType.Modifier) then
            Modify.AddModifier(info.id, M.PlayerID)
        elseif (info.type == Shop.Enum.ItemType.InventoryItem) then
            local pos = Inventory.AddItemFromDataBase(info.id)
            UI.Inventory.UpdateItem(pos)
        end

        Shop.RemoveItem(shopID, index)
        UI.Shop.UpdateItem(shopID, index)
    end
end

--- 每帧都会调用
function Update()
    ShopUpdate()
    InventoryUpdate()
    AttrUpdate()
end

