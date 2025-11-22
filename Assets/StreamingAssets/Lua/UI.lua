require("UIData")

local UIUtil = CS.GameBase.UI.UIUtil
local AttrUIChanger = CS.Instance.AttrUIChanger
local InventoryUIInteractive = CS.Instance.InventoryUIInteractive.Instance
local ShopUIInteractive = CS.Instance.ShopUIInteractive.Instance
local EquipmentUIInteractive = CS.Instance.EquipmentUIInteractive.Instance
local BuffUIInteractive = CS.Instance.BuffUIInteractive.Instance
local SpellUIInteractive = CS.Instance.SpellUIInteractive.Instance
local SpellActionModifierInteractive = CS.Instance.SpellActionModifierInteractive.Instance
local Inputs = CS.GameBase.Tools.Inputs
local TextSys = CS.GameBase.UI.TextSys.Instance

local CommonDragViewController = CS.Instance.CommonDragViewController.Instance
local CommonDetailViewController = CS.Instance.CommonDetailViewController.Instance

local LCommonDragViewController = {
    --- @noarg
    AttachToMouse = function ()
        CommonDragViewController:AttachToMouse()
    end ,

    --- @noarg
    Stop = function ()
        CommonDragViewController:Stop()
    end
}

local LCommonDetailViewController = {
        --- @noarg
    AttachToMouse = function ()
        CommonDetailViewController:AttachToMouse()
    end ,

    --- @noarg
    Stop = function ()
        CommonDetailViewController:Stop()
    end,
}

local function CommonDragViewInit()
    if (LCommonDragViewController.DragView ~= nil) then
        return
    end

    local dragView = CS.Instance.CommonDragView(UIData.CommonDragView.PrefabName)
    CommonDragViewController:SetActive(true)
    CommonDragViewController.DragView = dragView
    LCommonDragViewController.DragView = dragView
end

local function CommonDetailViewInit()
    if (LCommonDetailViewController.DetailView ~= nil) then
        return
    end

    local detailView = CS.Instance.CommonDetailView(UIData.CommonDetailView.PrefabName)
    CommonDetailViewController:SetActive(true)
    CommonDetailViewController.DetailView = detailView
    LCommonDetailViewController.DetailView = detailView
end

local function SetLayout( panel, layoutData)
    local layout = CS.Instance.CommonLayout()
    layout.XInterval = layoutData.xInterval
    layout.YInterval = layoutData.yInterval
    layout.Width = layoutData.width
    layout.Height = layoutData.height
    layout.Align = layoutData.align
    panel.Layout = layout
end

local function AttrUIInit()
    local panel = CS.Instance.AttrViewPanel()

    AttrUIChanger.Instance:SetActive(true)

    SetLayout(panel, UIData.Attr.Layout)

    local showAttr = UIData.Attr.ShowAttr
    for i = 0, #showAttr - 1 do
        panel:SetAttrKey(showAttr[i], i)
    end

    panel:Show()

    UI.Attr.Panel = panel
end

local function InventoryUIInit()
    local panel = CS.Instance.InventoryViewPanel()

    SetLayout(panel, UIData.Inventory.Layout)

    InventoryUIInteractive:SetActive(true)
    InventoryUIInteractive.EnterDragTime = UIData.Inventory.EnterDragTime
    InventoryUIInteractive.EnterDetailTime = UIData.Inventory.EnterDetailTime
    
    panel.OnEnterDrag = UI.Inventory.OnEnterDrag
    panel.OnExitDrag = UI.Inventory.OnExitDrag

    --- 如果在main中赋值，会导致c#周期调用lua，不推荐赋值
    panel.OnDrag = UI.Inventory.OnDrag

    panel.OnEnterDetail = UI.Inventory.OnEnterDetail
    panel.OnExitDetail = UI.Inventory.OnExitDetail

    --- 如果在main中赋值，会导致c#周期调用lua，不推荐赋值
    panel.OnDetail = UI.Inventory.OnDetail

    CommonDragViewInit()
    CommonDetailViewInit()

    UI.Inventory.Panel = panel
end

local function ShopUIInit()
    local panel = CS.Instance.ShopViewPanel() 
    local nearView = CS.Instance.ShopNearView("Prefabs/UI/ShopNearView.prefab")

    SetLayout(panel, UIData.Shop.Layout)

    nearView:Hide()

    ShopUIInteractive:SetActive(true)
    ShopUIInteractive.EnterDetailTime = UIData.Shop.EnterDetailTime

    panel.OnEnterDetail = UI.Shop.OnEnterDetail
    panel.OnExitDetail = UI.Shop.OnExitDetail

    --- 如果在main中赋值，会导致c#周期调用lua，不推荐赋值
    panel.OnDetail = UI.Shop.OnDetail

    panel.OnPointerDown = UI.Shop.OnPointerDown

    CommonDetailViewInit()

    UI.Shop.Panel = panel
    UI.Shop.NearView = nearView
end

local function EquipmentUIInit()
    local panel = CS.Instance.EquipmentPanel()

    SetLayout(panel, UIData.Equipment.Layout)

    panel:FillItem(UIData.Equipment.ItemNum)

    EquipmentUIInteractive:SetActive(true)
    EquipmentUIInteractive.EnterDetailTime = UIData.Equipment.EnterDetailTime
    EquipmentUIInteractive.EnterDragTime = UIData.Equipment.EnterDragTime

    panel.OnEnterDrag = UI.Equipment.OnEnterDrag
    panel.OnExitDrag = UI.Equipment.OnExitDrag

    --- 如果在main中赋值，会导致c#周期调用lua，不推荐赋值
    panel.OnDrag = UI.Equipment.OnDrag

    panel.OnEnterDetail = UI.Equipment.OnEnterDetail
    panel.OnExitDetail = UI.Equipment.OnExitDetail

    --- 如果在main中赋值，会导致c#周期调用lua，不推荐赋值
    panel.OnDetail = UI.Equipment.OnDetail

    CommonDragViewInit()
    CommonDetailViewInit()

    panel:Show()

    UI.Equipment.Panel = panel
end

local function BuffUIInit()
    local panel = CS.Instance.BuffViewPanel()

    SetLayout(panel, UIData.Buff.Layout)

    BuffUIInteractive:SetActive(true)

    panel:Show()

    UI.Buff.Panel = panel
end

local function SpellUIInit()
    local panel = CS.Instance.SpellViewPanel()

    SetLayout(panel, UIData.Spell.Layout)

    panel.OnPointerDown = UI.Spell.OnPointerDown

    SpellUIInteractive:SetActive(true)

    panel:Show()

    UI.Spell.Panel = panel
end

local function SpellActionModifierUIInit()
    local panel = CS.Instance.SpellActionModifierViewPanel()

    SetLayout(panel, UIData.SpellActionModifier.Layout)

    panel.OnEnterDrag = UI.SpellActionModifier.OnEnterDrag
    panel.OnExitDrag = UI.SpellActionModifier.OnExitDrag

    SpellActionModifierInteractive:SetActive(true)

    UI.SpellActionModifier.Panel = panel
end

local function AttrSelectUIInit()
    local panel = CS.Instance.AttrSelectPanel("Prefabs/UI/AttrSelectPanel.prefab", "Prefabs/UI/AttrSelectItem.prefab")

    SetLayout(panel, UIData.AttrSelect.Layout)

    panel.OnPointerDown = UI.AttrSelect.OnPointerDown

    UI.AttrSelect.Panel = panel
end


UI = {
    CommonDragView = LCommonDragViewController,

    CommonDetailView = LCommonDetailViewController,

    Shop = {
        --- @nopara
        Init = ShopUIInit,

        --- table
        DetailViewController = LCommonDetailViewController,
    },

    Inventory = {
        --- @noarg
        Init = InventoryUIInit,

        --- table
        DragViewController = LCommonDragViewController,

        --- table
        DetailViewController = LCommonDetailViewController
    },

    Attr = {
        --- @noarg
        Init = AttrUIInit,

        --- @arg1 target : Creature
        SetTarget = AttrUIChanger.SetTarget,
    },

    Equipment = {
        --- @noarg
        Init = EquipmentUIInit,

        --- @arg1 target : Creature
        SetTarget = function ( c )
            EquipmentUIInteractive.Target = c
        end,

        --- @ret target : Creature
        GetTarget = function ()
            return EquipmentUIInteractive.Target
        end,

        --- table
        DragViewController = LCommonDragViewController,

        --- table
        DetailViewController = LCommonDetailViewController
    },

    Buff = {
        --- @noarg
        Init = BuffUIInit,

        --- @arg1 target : Creature
        SetTarget = function ( c )
            BuffUIInteractive.Target = c
        end,
    },

    Spell = {
        --- @norag
        Init = SpellUIInit,

        --- @arg1 target : Creature
        SetTarget = function ( c )
            SpellUIInteractive.Target = c
        end,
    },

    SpellActionModifier = {
        --- @norag
        Init = SpellActionModifierUIInit,

        --- @arg1 target : Spell
        SetTarget = function ( target )
            SpellActionModifierInteractive.Target = target
        end,

        --- table
        DragViewController = LCommonDragViewController,

        --- table
        DetailViewController = LCommonDetailViewController
    },

    AttrSelect = {
        --- @noarg
        Init = AttrSelectUIInit,
    },

    TextSys = TextSys,

    Inputs = Inputs,
}