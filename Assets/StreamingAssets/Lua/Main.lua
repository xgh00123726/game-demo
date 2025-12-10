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
require("Todo")
require("Network")

require("Main/Util")
require("Main/Inventory")
require("Main/Shop")
require("Main/BonusSelect")
require("Main/Equipment")
require("Main/Spell")
require("Main/ToolBar")
require("Main/Todo")

M = {
    UpdateTick = 0,
    MousePosition = nil,
}

--- 整个lua的文件结构：
--- Lua
---  -Main
---  -- **.lua
---  -*Data.lua
---  -*.lua
---  -Main.lua
--- C#侧初始化完成后进入Main.lua完成生命周期管理
--- Main.lua进入同级目录下其他lua初始化所有工具函数/初始化函数
--- 再进入Main目录下其他.lua初始化所有业务函数
--- 最后在OnInitOK中完成初始化
--- 最终顺序为Main.lua -> 同级其他.lua -> Main/*.lua -> OnInitOK
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
    UI.EpicBonusSelect.Init()
    UI.ToolBar.Init()

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

    Todo.Init()
    Todo.ToolBarInit()

    Network.Init()

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