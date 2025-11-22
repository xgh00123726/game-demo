require("InventoryData")

local Utils = CS.LuaUtil.Utils
local ItemDataMgr = CS.GameBase.Items.ItemDataMgr
local InventoryInteractive = CS.Instance.InventoryInteractive
local InventoryDataBase = CS.Instance.InventoryDataBase.Instance

local function Init()
    -- 初始化背包
    local instance = Utils.NewItemInventory()
    -- 背包设置容量
    instance.Size = InventoryData.Size

    InventoryInteractive.Instance:SetActive(true)
    InventoryInteractive.OnActive = Inventory.OnActive
    InventoryInteractive.OnInActive = Inventory.OnInActive

    Inventory.Instance = instance
end

local function GenInitItem()
    local instance = Inventory.Instance
    -- 向背包中添加物品
    for i = 0, InventoryData.InitItemNum - 1 do
        local itemData = ItemDataMgr.Get(i)
        instance:Add(itemData)
    end
end

Inventory = {
    --- @noarg
    Init = Init,

    --- @noarg
    GenInitItem = GenInitItem,

    --- data
    DataBase = InventoryDataBase,

    --- @arg1 data : InventoryData
    --- @ret index : int 
    GetIndex = function ( data )
        return InventoryDataBase:GetIndex(data)
    end
}