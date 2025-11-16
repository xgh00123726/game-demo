require("EquipmentData")

local EquipmentUIInteractive = CS.Instance.EquipmentUIInteractive.Instance
local EquipmentFactory = CS.GameBase.Equipments.EquipmentFactory.Instance

local function GetCurrentEquipmentInventory()
    return EquipmentUIInteractive.target.equipments
end

local function GetName( id )
    return EquipmentFactory:GetName(id)
end

Equipment = {
    ---@norag
    GetCurrentEquipmentInventory = GetCurrentEquipmentInventory,

    ---@arg1 id : int
    GetName = GetName,
}