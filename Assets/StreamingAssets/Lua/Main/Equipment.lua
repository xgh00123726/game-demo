local equipmentDragValid = false

UI.Equipment.OnEnterDrag = function ( index )
    if (Creature.GetCurrentSelect():GetEquipment(index) == nil) then
        equipmentDragValid = false
        return
    else
        equipmentDragValid = true
    end

    local controller = UI.Equipment.DragViewController
    local view = controller.DragView
    local dragImage = view.triggerImage
    local dragedItemImage = UI.Equipment.Panel[index].triggerImage

    UI.Inputs.LockCaller("rectDrawer")

    dragImage:Copy(dragedItemImage)

    dragedItemImage:Hide()
    dragedItemImage:HideColor()

    view:Show()
    controller.AttachToMouse()
end

UI.Equipment.OnExitDrag = function ( index )
    if (not equipmentDragValid) then
        return
    end

    local controller = UI.Equipment.DragViewController
    local view = controller.DragView
    local equipmentPanel = UI.Equipment.Panel
    local dragedItem = equipmentPanel[index]
    local dragedItemImage = dragedItem.triggerImage
    local c = Creature.GetCurrentSelect()
    local equipments = c.equipments

    view:Hide()
    controller.Stop()

    dragedItemImage:Show()
    dragedItemImage:ShowColor()

    local mouseItem = UI.Equipment.Panel:GetItemFromTriggerPosition(M.MousePosition)
    local dragEnd = false

    if (not dragEnd and mouseItem ~= nil) then
    -- 如果拖动终点是装备栏，则交换
        local mouseIndex = mouseItem.ItemIndex
        equipments:Swap(index, mouseIndex)
        equipmentPanel:UpdatePanel(c)
        dragEnd = true
        UI.Inputs.UnlockCaller("rectDrawer")
    else
        mouseItem = UI.Inventory.Panel:GetItemFromTriggerPosition(M.MousePosition)
    end

    if (not dragEnd and mouseItem ~= nil) then
    -- 如果拖动终点是背包，则与背包交互艳
        local mouseIndex = mouseItem.ItemIndex
        SwapInventoryAndEquipments(c, index, mouseIndex)
        equipmentPanel:UpdatePanel(c)
        UI.Inventory.Panel:UpdateItem(Inventory.Instance, mouseIndex)
        dragEnd = true
    end

    if (not dragEnd) then
        UI.Inputs.UnlockCaller("rectDrawer")
    end
end

UI.Equipment.OnDrag = nil