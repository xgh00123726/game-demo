require("Creature")
require("UI")
require("Inventory")
require("Shop")
require("Buff")
require("Modify")
require("Text")
require("Spell")
require("Controller")
require("Equipment")
require("Item")

require("Main/Util")
require("Main/Inventory")
require("Main/Shop")
require("Main/AttrSelect")
require("Main/Equipment")
require("Main/Spell")

M = {
    UpdateTick = 0,
    MousePosition = nil,
}

--- 初始化完成时调用，lua入口函数
function OnInitOK()
    Creature.Init()

    Inventory.Init()
    Inventory.GenInitItem()

    Shop.Init()
    Shop.GenShop(ShopData.Shop1)
    Shop.SetInteractiveTarget(Creature.Player)
    Shop.AttrSelectInventory = Shop.GenShopInventory(ShopData.AttrSelect.S1)

    UI.Inventory.Init()
    UI.Inventory.Panel:UpdatePanel(Inventory.Instance)

    UI.Shop.Init()

    UI.Attr.Init()
    UI.Attr.SetTarget(Creature.Player)

    UI.Equipment.Init()
    UI.Equipment.SetTarget(Creature.Player)

    UI.Buff.Init()
    UI.Buff.SetTarget(Creature.Player)

    UI.Spell.Init()
    UI.Spell.SetTarget(Creature.Player)

    UI.SpellActionModifier.Init()

    UI.AttrSelect.Init()

    Controller.Init()
    Controller.PlayerMove.SetTarget(Creature.Player)
    Controller.SpellCast.SetTarget(Creature.Player)
    Controller.PlayerEpicBar.SetTarget(Creature.Player)
    -- Controller.AutoCaster.Register(Creature.Player.spells[1])
    Controller.Inputs.RegisterKeyDownEvent(Controller.Enum.KeyFunction.ExtraInfo, function ()
        Creature.DrawCreatureBase(CreatureData.CreatureBase.Base1)
    end)
    Controller.Inputs.RegisterKeyDownEvent(Controller.Enum.KeyFunction.ToggleDrawColliderEnable, function ()
        local enable = not Controller.ColliderCreator.ActiveSelf
        Controller.ColliderCreator:SetActive(enable)
    end)

    Text.Init()

    Creature.CreateBase(CreatureData.CreatureBase.Base1)
    Creature.CreateBase(CreatureData.CreatureBase.Base2)

    print("lua init ok")
end

local function MouseUpdate()
    M.MousePosition = CS.UnityEngine.Input.mousePosition
end

--- 每帧都会调用
function Update()
    MouseUpdate()

    M.UpdateTick = M.UpdateTick + 1
end