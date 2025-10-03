require("InventoryData")

local InventoryUtil = CS.LuaUtil.InventoryUtil

local function Init()
    -- 初始化背包
    InventoryUtil.Init()
    -- 背包设置容量
    InventoryUtil.SetSize(InventoryData.Size)
end

local function GenInitItem()
    -- 向背包中添加27个物品
    for i = 1, InventoryData.InitItemNum do
        InventoryUtil.AddItemFromDataBase(i - 1)
    end
end

Inventory = {
    --- @nopara
    Init = Init,

    --- @nopara
    GenInitItem = GenInitItem,

    --- @arg1 dataBaseIndex : int
    --- @ret addPosition : int
    AddItemFromDataBase = InventoryUtil.AddItemFromDataBase
}