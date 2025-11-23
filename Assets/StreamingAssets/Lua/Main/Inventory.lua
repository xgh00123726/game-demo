Inventory.OnActive = function ()
    UI.Inventory.Panel:Show()
    UI.Inputs.LockCaller("rectDrawer")
end

Inventory.OnInActive = function ()
    UI.Inventory.Panel:Hide()
    UI.SpellActionModifier.Panel:Hide()
    UI.Inputs.UnlockCaller("rectDrawer")
end

UI.Inventory.OnEnterDrag = function ( index )
    local controller = UI.Inventory.DragViewController
    local view = controller.DragView
    local dragImage = view.TriggerImage
    local dragedItemImage = UI.Inventory.Panel[index].TriggerImage

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
    local dragedItemImage = dragedItem.TriggerImage
    local inventory = Inventory.Instance

    dragedItemImage:Show()
    dragedItemImage:ShowColor()

    -- 如果拖动终点还是背包，则交换
    local mouseItem = UI.Inventory.Panel:GetItemFromTriggerPosition(M.MousePosition)
    local dragEnd = false

    if (not dragEnd and mouseItem ~= nil) then
        local mouseIndex = mouseItem.ItemIndex
        inventory:Swap(index, mouseIndex)
        inventoryPanel:UpdateItem(inventory, index)
        inventoryPanel:UpdateItem(inventory, mouseIndex)
        dragEnd = true
    else
        --- 查询拖动终点是否是装备栏
        mouseItem = UI.Equipment.Panel:GetItemFromTriggerPosition(M.MousePosition)
    end

    -- 如果拖动终点是装备栏，则装备
    if (not dragEnd and mouseItem ~= nil) then
        local mouseIndex = mouseItem.ItemIndex
        local st = SwapInventoryAndEquipments(Creature.GetCurrentSelect(), mouseIndex, index)
        if (st) then
            inventoryPanel:UpdateItem(inventory, index)
        end
        dragEnd = true
    else
        --- 查询拖动终点是否是技能强化栏
        mouseItem = UI.SpellActionModifier.Panel:GetItemFromTriggerPosition(M.MousePosition)
    end

    if (not dragEnd and mouseItem ~= nil) then
        local mouseIndex = mouseItem.ItemIndex
        local c = Creature.GetCurrentSelect()
        local st = SwapInventoryAndSAM(c, UI.Spell.LastClickIndex, mouseIndex, index)

        if (st) then
            inventoryPanel:UpdateItem(inventory, index)
            UI.SpellActionModifier.Panel:UpdatePanel(c:GetSpell(UI.Spell.LastClickIndex))
        end
        dragEnd = true
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