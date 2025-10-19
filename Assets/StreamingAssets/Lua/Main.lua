require("Creature")
require("UI")
require("Inventory")
require("Shop")
require("Buff")
require("Modify")
require("Text")
require("Spell")
require("Controller")

local M = {
    UpdateTick = 0,
    MousePosition = nil,
}

--- 初始化完成时调用，lua入口函数
function OnInitOK()
    Creature.Init()

    Inventory.Init()
    Inventory.GenInitItem()

    Shop.Init()
    Shop.GenShop(ShopData.Shop1)
    Shop.SetInteractiveTarget(Creature.Player)
    Shop.AttrSelectInventory = Shop.GenShopInventory(ShopData.AttrSelect.S1)

    UI.Inventory.Init()
    UI.Inventory.Panel:UpdatePanel(Inventory.Instance)

    UI.Shop.Init()

    UI.Attr.Init()
    UI.Attr.SetTarget(Creature.Player)

    UI.Equipment.Init()
    UI.Equipment.SetTarget(Creature.Player)

    UI.Buff.Init()
    UI.Buff.SetTarget(Creature.Player)

    UI.Spell.Init()
    UI.Spell.SetTarget(Creature.Player)

    UI.SpellActionModifier.Init()

    UI.AttrSelect.Init()

    Controller.Init()
    Controller.PlayerMove.SetTarget(Creature.Player)
    Controller.SpellCast.SetTarget(Creature.Player)
    Controller.PlayerEpicBar.SetTarget(Creature.Player)
    -- Controller.AutoCaster.Register(Creature.Player.spells[1])
    Controller.Inputs.RegisterKeyDownEvent(Controller.Enum.KeyFunction.ExtraInfo, function ()
        Creature.DrawCreatureBase(CreatureData.CreatureBase.Base1)
    end)
    Controller.Inputs.RegisterKeyDownEvent(Controller.Enum.KeyFunction.ToggleDrawColliderEnable, function ()
        local enable = not Controller.ColliderCreator.ActiveSelf
        Controller.ColliderCreator:SetActive(enable)
    end)

    Text.Init()

    Creature.CreateBase(CreatureData.CreatureBase.Base1)

    print("lua init ok")
end

local function MouseUpdate()
    M.MousePosition = CS.UnityEngine.Input.mousePosition
end

--- 每帧都会调用
function Update()
    MouseUpdate()

    M.UpdateTick = M.UpdateTick + 1
end

local function GetInventoryItemText( info )
    local text = "NNN"

    if (info.type == InventoryData.Enum.ItemType.Equipment) then
        text = Text.GetBuffText(info.typeID)
    elseif (info.type == InventoryData.Enum.ItemType.SpellActionModifier) then
        text = Text.GetSpellActionModifierText(info.typeID)
    end

    return text
end

local function GetShopItemText( info )
    local text = "NNN"

    if (info.type == ShopData.Enum.ItemType.InventoryItem) then
        local inventoryInfo = Inventory.DataBase[info.typeID]
        text = "物品\n"..GetInventoryItemText(inventoryInfo)
    elseif (info.type == ShopData.Enum.ItemType.Buff) then
        text = "Buff\n"..Text.GetBuffText(info.typeID)
    elseif (info.type == ShopData.Enum.ItemType.Modifier) then
        text = "属性加成\n"..Text.GetModifierText(info.typeID)
    end

    return text
end

Inventory.OnActive = function ()
    UI.Inventory.Panel:Show()
    UI.Inputs.LockCaller("rectDrawer")
end

Inventory.OnInActive = function ()
    UI.Inventory.Panel:Hide()
    UI.Inputs.UnlockCaller("rectDrawer")
end

Shop.OnShopActive = function ( shop )
    UI.Shop.Panel:UpdatePanel(shop.inventory)
    UI.Shop.Panel:Show()
end

Shop.OnShopInActive = function ( shop )
    UI.Shop.Panel:Hide()
end

Shop.OnTargetNearShop = function ( shop )
    UI.Shop.NearView:Show(shop)
end

Shop.OnTargetFarFromShop = function ( shop )
    UI.Shop.NearView:Hide()
    UI.Shop.Panel:Hide()
end

UI.Shop.OnEnterDetail = function ( index )
    local controller = UI.Shop.DetailViewController
    local view = controller.DetailView
    local shop = Shop.GetCurrentShop()
    local inventory = shop.inventory
    local dataBase = Shop.DataBase

    local info = dataBase[inventory[index]]
    local text = GetShopItemText(info)
    
    view:SetText(text)
    view:Show()

    controller.AttachToMouse()
end

UI.Shop.OnExitDetail = function ( index )
    local controller = UI.Shop.DetailViewController
    local view = controller.DetailView
    
    view:Hide()

    controller.Stop()
end

UI.Shop.OnDetail = nil

UI.Shop.OnPointerDown = function ( index )
    local shop = Shop.GetCurrentShop()
    local inventory = shop.inventory
    local dataBase = Shop.DataBase
    local info = dataBase[inventory[index]]
    local player = Creature.Player
    local panel = UI.Shop.Panel

    if (info.type == ShopData.Enum.ItemType.Buff) then
        local duration = 5
        player:AddBuff(info.typeID, duration)
    elseif (info.type == ShopData.Enum.ItemType.Modifier) then
        -- player:AddModifier(info.typeID)
    elseif (info.type == ShopData.Enum.ItemType.InventoryItem) then
        local itemData = Inventory.DataBase[info.typeID]
        local pos = Inventory.Instance:Add(itemData)
        UI.Inventory.Panel:UpdateItem(Inventory.Instance, pos)
    end

    inventory:Remove(index)
    panel:UpdateItem(inventory, index)
end

UI.Inventory.OnEnterDrag = function ( index )
    local controller = UI.Inventory.DragViewController
    local view = controller.DragView
    local dragImage = view.triggerImage
    local dragedItemImage = UI.Inventory.Panel[index].triggerImage

    dragImage:Copy(dragedItemImage)

    dragedItemImage:Hide()
    dragedItemImage:HideColor()

    view:Show()
    controller.AttachToMouse()
end

UI.Inventory.OnExitDrag = function ( index )
    local controller = UI.Inventory.DragViewController
    local view = controller.DragView
    local inventoryPanel = UI.Inventory.Panel
    local dragedItem = inventoryPanel[index]
    local dragedItemImage = dragedItem.triggerImage
    local inventory = Inventory.Instance

    dragedItemImage:Show()
    dragedItemImage:ShowColor()

    local mouseItem = UI.Inventory.Panel:GetItemFromTriggerPosition(M.MousePosition)
    local dragEnd = false

    if (not dragEnd and mouseItem ~= nil) then
        local mouseIndex = mouseItem.ItemIndex
        inventory:Swap(index, mouseIndex)
        inventoryPanel:UpdateItem(inventory, index)
        inventoryPanel:UpdateItem(inventory, mouseIndex)
        dragEnd = true
    else
        mouseItem = UI.Equipment.Panel:GetItemFromTriggerPosition(M.MousePosition)
    end

    if (not dragEnd and mouseItem ~= nil) then
        local mouseIndex = mouseItem.ItemIndex

        local player = Creature.Player
        local info = inventory[index]
        local st = player:AddEquipment(info.typeID, mouseIndex)

        if (st) then
            inventory:Remove(index)
            inventoryPanel:UpdateItem(inventory, index)
        else
            print("err")
        end
    end

    view:Hide()
    controller.Stop()
end

UI.Inventory.OnDrag = nil

UI.Inventory.OnEnterDetail = function ( index )
    local controller = UI.Inventory.DetailViewController
    local view = controller.DetailView

    local info = Inventory.Instance[index]
    local text = GetInventoryItemText(info)
    
    view:SetText(text)
    view:Show()

    controller.AttachToMouse()
end

UI.Inventory.OnExitDetail = function ( index )
    local controller = UI.Inventory.DetailViewController
    local view = controller.DetailView
    
    view:Hide()

    controller.Stop()
end

UI.Inventory.OnDetail = nil

UI.AttrSelect.OnPointerDown = function ( index )
    local inventory = Shop.AttrSelectInventory
    local dataBase = Shop.DataBase
    local info = dataBase[inventory[index]]
    local player = Creature.Player
    local panel = UI.AttrSelect.Panel

    if (info.type == ShopData.Enum.ItemType.Modifier) then
        -- player:AddModifier(info.typeID)
    end

    panel:Hide()
end