require("EquipmentData")

local EquipmentUIInteractive = CS.Instance.EquipmentUIInteractive.Instance

Equipment = {
    GetCurrentEquipmentInventory = function ()
        return EquipmentUIInteractive.target.equipments
    end
}