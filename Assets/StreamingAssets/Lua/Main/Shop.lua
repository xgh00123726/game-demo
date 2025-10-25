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
        player:AddBuff(info.reflectedID, duration)
    elseif (info.type == ShopData.Enum.ItemType.Modifier) then

    elseif (info.type == ShopData.Enum.ItemType.InventoryItem) then
        local itemData = Inventory.DataBase[info.reflectedID]
        local pos = Inventory.Instance:Add(itemData)
        UI.Inventory.Panel:UpdateItem(Inventory.Instance, pos)
    end

    inventory:Remove(index)
    panel:UpdateItem(inventory, index)
end