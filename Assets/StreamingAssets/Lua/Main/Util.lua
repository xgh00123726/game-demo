function GetInventoryItemText( info )
    local text = "NNN"

    if (info.type == InventoryData.Enum.ItemType.Equipment) then
        local name = Equipment.GetName(info.reflectedID)
        text = Text.GetEquipmentText(name).detail
    elseif (info.type == InventoryData.Enum.ItemType.SpellActionModifier) then
        text = Text.GetSpellActionModifierText(info.reflectedID)
    end

    return text
end

function GetShopItemText( info )
    local text = "NNN"

    if (info.type == ShopData.Enum.ItemType.InventoryItem) then
        local inventoryInfo = Inventory.DataBase[info.reflectedID]
        text = "物品\n"..GetInventoryItemText(inventoryInfo)
    elseif (info.type == ShopData.Enum.ItemType.Buff) then
        text = "Buff\n"..Text.GetBuffText(info.reflectedID)
    elseif (info.type == ShopData.Enum.ItemType.Modifier) then
        text = "属性加成\n"..Text.GetModifierText(info.reflectedID)
    end

    return text
end

--- @arg1 target : Creature
--- @arg2 equipIndex : int
--- @arg3 inventoryIndex : int
--- @ret isEquipOK : bool
function EquipInventoryItem( c, equipIndex, inventoryIndex)
    if (c == nil) then
        return false
    end
    local info = Inventory.Instance[inventoryIndex]
    return c:TryEquipInventoryItem(info, equipIndex)
end

--- @arg1 target : Creature
--- @arg2 equipIndex : int
--- @arg3 inventoryIndex : int
--- @ret isEquipOK : bool
--- @comment 将对象身上的物品和仓库中调换
function SwapInventoryAndEquipments(c, equipIndex, inventoryIndex)
    -- 没有选中任何对象，不进行操作
    if (c == nil) then
        return false
    end

    local inventory = Inventory.Instance
    local hasItem = inventory:HasItem(inventoryIndex)
    local hasEquipItem = false
    if (hasItem) then
        hasEquipItem = inventory[inventoryIndex].type == InventoryData.Enum.ItemType.Equipment
    end
    local equipment = c:GetEquipment(equipIndex)
    local hasEquipment = equipment ~= nil

    local oldEquipmentInfo = InventoryData.Struct.DataType()
    if (hasEquipment) then
        oldEquipmentInfo.type = InventoryData.Enum.ItemType.Equipment
        oldEquipmentInfo.reflectedID = equipment.id
    end
    local newEquipmentInfo = inventory[inventoryIndex]

    if (hasEquipItem and hasEquipment) then
    -- 如果背包里和装备位置都有物品，则交换
        inventory[inventoryIndex] = oldEquipmentInfo
        c:RemoveEquipment(equipIndex)
        c:AddEquipmentByID(newEquipmentInfo.reflectedID, equipIndex)
    elseif (not hasEquipItem and not hasEquipment) then
    -- 如果都没有物品，啥都不干
        return false
    elseif (not hasItem) then
    -- 如果仓库没有物品，则卸下装备
        inventory[inventoryIndex] = oldEquipmentInfo
        c:RemoveEquipment(equipIndex)
    elseif (not hasEquipment) then
    -- 如果装备栏没有物品，则装备仓库上的物品
        inventory:Remove(inventoryIndex)
        c:AddEquipmentByID(newEquipmentInfo.reflectedID, equipIndex)
    end
    return true
end

--- @arg1 c : Creature
--- @arg2 spellIndex : int
--- @arg3 modifierIndex : int
--- @arg4 inventoryIndex : int
--- @ret isAddOK : bool
--- @comment 交换技能修饰器和仓库中的物品
function SwapInventoryAndSAM(c, spellIndex, modifierIndex, inventoryIndex)
    -- 没有选中任何对象，不进行操作
    if (c == nil) then
        return false
    end

    -- 如果没有该技能，则失败
    local spell = c:GetSpell(spellIndex)
    if (spell == nil) then
        return false
    end

    -- 如果技能不是可修饰技能，则失败Z
    local spellAction = spell.action
    if (spellAction.GetID == nil) then
        return false
    end

    local inventory = Inventory.Instance
    local itemData = inventory[inventoryIndex]
    local inventoryHasItem = inventory:HasItem(inventoryIndex)
    local inventoryHasModifier = false
    if (inventoryHasItem) then
        inventoryHasModifier = inventory[inventoryIndex].type == InventoryData.Enum.ItemType.SpellActionModifier
    end
    local modifierID = spellAction:GetID(modifierIndex)
    local newModifierInfo = InventoryData.Struct.DataType()
    newModifierInfo.type = InventoryData.Enum.ItemType.SpellActionModifier
    newModifierInfo.reflectedID = modifierID

    if (modifierID ~= -1 and inventoryHasModifier) then
    -- 如果两边都不是空的，则交换        
        inventory[inventoryIndex] = newModifierInfo
        spellAction:RemoveModifier(modifierIndex)
        spellAction:AddModifier(itemData.reflectedID, modifierIndex)
    elseif (modifierID == -1) then
        if inventoryHasModifier then
            -- 如果技能修饰器是空的，则装备物品栏上的技能修饰器
            spellAction:AddModifier(itemData.reflectedID, modifierIndex)
            inventory:Remove(inventoryIndex)
        else
            -- 两边都是空的就啥都不干
            return false
        end
    elseif (modifierID ~= -1 and not inventoryHasItem) then
    -- 如果物品栏是空的，就卸下技能装饰器
        spellAction:RemoveModifier(modifierIndex)
        inventory[inventoryIndex] = newModifierInfo
    end

    return true
end