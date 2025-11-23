UI.EpicBonusSelect.OnPointerDown = function ( index )
    local inventory = Shop.EpicBonusSelectInventory
    local dataBase = Shop.DataBase
    local info = dataBase[inventory[index]]
    local player = Creature.Player
    local panel = UI.EpicBonusSelect.Panel

    if (info.type == ShopData.Enum.ItemType.Modifier) then
        -- player:AddModifier(info.typeID)
    end

    panel:Hide()
end