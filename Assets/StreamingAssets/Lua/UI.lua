require("UIData")

local AttrUIChanger = CS.Instance.AttrUIChanger
local InventoryUIInteractive = CS.Instance.InventoryUIInteractive
local ShopUIInteractive = CS.Instance.ShopUIInteractive
local EquipmentUIInteractive = CS.Instance.EquipmentUIInteractive
local BuffUIInteractive = CS.Instance.BuffUIInteractive
local SpellUIInteractive = CS.Instance.SpellUIInteractive
local SpellActionModifierInteractive = CS.Instance.SpellActionModifierInteractive
local TextSys = CS.GameBase.UI.TextSys.Instance

local CommonDragViewController = CS.Instance.CommonDragViewController
local CommonDetailViewController = CS.Instance.CommonDetailViewController

local LCommonDragViewController = {
    --- @noarg
    AttachToMouse = CommonDragViewController.AttachToMouse,

    --- @noarg
    Stop = CommonDragViewController.Stop,
}

local LCommonDetailViewController = {
        --- @noarg
    AttachToMouse = CommonDetailViewController.AttachToMouse,

    --- @noarg
    Stop = CommonDetailViewController.Stop,
}

local function CommonDragViewInit()
    if (LCommonDragViewController.DragView ~= nil) then
        return
    end

    local dragView = CS.Instance.CommonDragView(UIData.CommonDragView.PrefabID)
    CommonDragViewController.Instance:SetActive(true)
    CommonDragViewController.dragView = dragView
    LCommonDragViewController.DragView = dragView
end

local function CommonDetailViewInit()
    if (LCommonDetailViewController.DetailView ~= nil) then
        return
    end

    local detailView = CS.Instance.CommonDetailView(UIData.CommonDetailView.PrefabID)
    CommonDetailViewController.Instance:SetActive(true)
    CommonDetailViewController.detailView = detailView
    LCommonDetailViewController.DetailView = detailView
end

local function SetLayout( panel, layoutData)
    local layout = CS.Instance.CommonLayout()
    layout.xInterval = layoutData.xInterval
    layout.yInterval = layoutData.yInterval
    layout.width = layoutData.width
    layout.height = layoutData.height
    layout.align = layoutData.align
    panel.layout = layout
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

    InventoryUIInteractive.Instance:SetActive(true)
    InventoryUIInteractive.enterDragTime = UIData.Inventory.EnterDragTime
    InventoryUIInteractive.enterDetailTime = UIData.Inventory.EnterDetailTime
    
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
    local nearView = CS.Instance.ShopNearView(39)

    SetLayout(panel, UIData.Shop.Layout)

    nearView:Hide()

    ShopUIInteractive.Instance:SetActive(true)
    ShopUIInteractive.enterDetailTime = UIData.Shop.EnterDetailTime

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

    EquipmentUIInteractive.Instance:SetActive(true)
    EquipmentUIInteractive.enterDetailTime = UIData.Equipment.EnterDetailTime
    EquipmentUIInteractive.enterDragTime = UIData.Equipment.EnterDragTime

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

    BuffUIInteractive.Instance:SetActive(true)

    panel:Show()

    UI.Buff.Panel = panel
end

local function SpellUIInit()
    local panel = CS.Instance.SpellViewPanel()

    SetLayout(panel, UIData.Spell.Layout)

    SpellUIInteractive.Instance:SetActive(true)

    panel:Show()

    UI.Spell.Panel = panel
end

local function SpellActionModifierUIInit()
    local panel = CS.Instance.SpellActionModifierViewPanel()

    SetLayout(panel, UIData.SpellActionModifier.Layout)

    SpellActionModifierInteractive.Instance:SetActive(true)

    UI.SpellActionModifier.Panel = panel
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
        SetTarget = EquipmentUIInteractive.SetTarget,
    },

    Buff = {
        --- @noarg
        Init = BuffUIInit,

        --- @arg1 target : Creature
        SetTarget = BuffUIInteractive.SetTarget,
    },

    Spell = {
        --- @norag
        Init = SpellUIInit,

        --- @arg1 target : Creature
        SetTarget = SpellUIInteractive.SetTarget,
    },

    SpellActionModifier = {
        --- @norag
        Init = SpellActionModifierUIInit,

        --- @arg1 target : Spell
        SetTarget = SpellActionModifierInteractive.SetTarget,
    },

    TextSys = TextSys,
}