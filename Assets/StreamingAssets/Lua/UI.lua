require("UIData")

local InventoryUIUtil = CS.LuaUtil.InventoryUIUtil
local AttrUIUtil = CS.LuaUtil.AttrUIUtil
local ShopUIUtil = CS.LuaUtil.ShopUIUtil

local function AttrUIInit()
    AttrUIUtil.Init()

    local layout = UIData.Attr.Layout
    AttrUIUtil.SetLayout(
        layout.xInterval,
        layout.yInterval,
        layout.width,
        layout.height,
        layout.align
    )

    local showAttr = UIData.Attr.ShowAttr
    for i = 1, #showAttr do
        AttrUIUtil.SetAttrKey(showAttr[i], i - 1)
    end
end

local function InventoryUIInit()
    InventoryUIUtil.Init()

    local layout = UIData.Inventory.Layout
    InventoryUIUtil.SetLayout(
        layout.xInterval,
        layout.yInterval,
        layout.width,
        layout.height,
        layout.align
    )

    InventoryUIUtil.UpdatePanel()
end

local function ShopUIInit()
    ShopUIUtil.Init()

    local layout = UIData.Shop.Layout
    ShopUIUtil.SetLayout(
        layout.xInterval,
        layout.yInterval,
        layout.width,
        layout.height,
        layout.align
    )
end

UI = {
    Enum = {
        --- user input type
        Cmd = CS.LuaUtil.UICmd,
    },
    --- @nopara
    AttrUIInit = AttrUIInit,
    --- @nopara
    InventoryUIInit = InventoryUIInit,
    --- @nopara
    ShopUIInit = ShopUIInit,
    
    Shop = {
        --- @nopara
        ShowPanel = ShopUIUtil.ShowPanel,

        --- @nopara
        HidePanel = ShopUIUtil.HidePanel,

        --- @int shopID
        ShowIndicator = ShopUIUtil.ShowIndicator,

        --- @nopara
        HideIndicator = ShopUIUtil.HideIndicator,

        --- @int shopID
        UpdatePanel = ShopUIUtil.UpdatePanel,

        --- @arg1 shopID : int
        --- @arg2 shopItemIndex : int
        UpdateItem = ShopUIUtil.UpdateItem,

        --- @ret cmd : UI.Enum.Cmd
        GetInputCmd = ShopUIUtil.GetInputCmd,
    },
    Inventory = {
        --- @nopara
        TogglePanel = InventoryUIUtil.TogglePanel,

        --- @nopara
        UpdatePanel = InventoryUIUtil.UpdatePanel,

        --- @arg1 index : int
        UpdateItem = InventoryUIUtil.UpdateItem,

        --- @ret cmd : UI.Enum.Cmd
        GetInputCmd = InventoryUIUtil.GetInputCmd,
    },
    Attr = {
        --- @arg1 creatureID : int
        SetValue = AttrUIUtil.SetValue,
    }
}