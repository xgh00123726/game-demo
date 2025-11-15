local lastC = nil
local lastSpellClickIndex = -1

local spellActionModifierDragValid = false

UI.Spell.OnPointerDown = function ( index )
    local c = Creature.GetCurrentSelect()
    local mPanel = UI.SpellActionModifier.Panel

    if (not Spell.IsModifyable(c:GetSpell(index))) then
        return
    end

    mPanel:UpdatePanel(c:GetSpell(index))

    if (lastC ~= c or lastSpellClickIndex ~= index) then
        mPanel:Show()
    else
        mPanel:Toggle()
    end

    lastC = c
    lastSpellClickIndex = index
    UI.Spell.LastClickIndex = index
end

UI.SpellActionModifier.OnEnterDrag = function ( index )
    local spellAction = Creature.GetCurrentSelect():GetSpell(UI.Spell.LastClickIndex).action
    if (spellAction.GetID ~= nil) then
        local id = spellAction:GetID(index)
        if (id == -1) then
            spellActionModifierDragValid = false
            return
        else
            spellActionModifierDragValid = true
        end
    end

    local controller = UI.SpellActionModifier.DragViewController
    local view = controller.DragView
    local dragImage = view.triggerImage
    local dragedItemImage = UI.SpellActionModifier.Panel[index].triggerImage

    UI.Inputs.LockCaller("rectDrawer")
    dragImage:Copy(dragedItemImage)
    dragedItemImage:Hide()
    dragedItemImage:HideColor()
    view:Show()
    controller.AttachToMouse()
end

UI.SpellActionModifier.OnExitDrag = function ( index )
    if (not spellActionModifierDragValid) then
        return
    end

    local controller = UI.SpellActionModifier.DragViewController
    local view = controller.DragView
    local mPanel = UI.SpellActionModifier.Panel
    local dragedItem = mPanel[index]
    local dragedItemImage = dragedItem.triggerImage

    view:Hide()
    controller.Stop()
    dragedItemImage:Show()
    dragedItemImage:ShowColor()

    local c = Creature.GetCurrentSelect()
    local spell = c:GetSpell(UI.Spell.LastClickIndex)
    local spellAction = spell.action
    if (spellAction.GetID == nil) then
        return
    end

    local mouseItem = mPanel:GetItemFromTriggerPosition(M.MousePosition)
    local dragEnd = false

    if (not dragEnd and mouseItem ~= nil) then
    --- 如果拖动终点还是技能修饰器，则交换
        local mouseIndex = mouseItem.ItemIndex
        spellAction:Swap(index, mouseIndex)
        mPanel:UpdatePanel(spell)
        dragEnd = true
    else
        mouseItem = UI.Inventory.Panel:GetItemFromTriggerPosition(M.MousePosition)
    end

    if (not dragEnd and mouseItem ~= nil) then
    --- 如果拖动终点是仓库
        local mouseIndex = mouseItem.ItemIndex
        SwapInventoryAndSAM(c, UI.Spell.LastClickIndex, index, mouseIndex)
        mPanel:UpdatePanel(spell)
        UI.Inventory.Panel:UpdatePanel(Inventory.Instance)
        dragEnd = true
    end
end