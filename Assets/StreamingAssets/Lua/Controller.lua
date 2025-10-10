local PlayerMoveController = CS.Instance.PlayerMoveController
local PlayerSpellCaster = CS.Instance.PlayerSpellCaster
local EpicBarController = CS.Instance.EpicBarController
local AutoSpellCaster = CS.Instance.AutoSpellCaster
local DrawCreatureRadius = CS.Instance.DrawCreatureRadius
local CreatureGizmosDraw = CS.GameBase.Creatures.CreatureGizmosDraw
local TriggerGizmosDraw = CS.GameBase.Triggers.TriggerGizmos
local CreatureSelector = CS.Instance.CreatureSelector

local function ControllerInit()
    PlayerMoveController.Instance:SetActive(true)
    PlayerMoveController.OnMoveInput = Controller.PlayerMove.OnMoveInput

    PlayerSpellCaster.Instance:SetActive(true)

    EpicBarController.Instance:SetActive(true)

    AutoSpellCaster.Instance:SetActive(true)

    DrawCreatureRadius.Instance:SetActive(true)

    local KeyFunction = CS.GameBase.Tools.KeyFunction
    CreatureSelector.AddHotKeyCreature(KeyFunction.SelectF1, Creature.Player)
    CreatureSelector.AddHotKeyCreature(KeyFunction.SelectF2, Creature.Player)
    CreatureSelector.Instance:SetActive(true)
    
    
    CreatureGizmosDraw.isDrawGizmos = true
    CreatureGizmosDraw.isDrawCollider = false
    CreatureGizmosDraw.isDrawMove = false
    CreatureGizmosDraw.isDrawForce = false
    CreatureGizmosDraw.isDrawAI = false
    CreatureGizmosDraw.Init()

    TriggerGizmosDraw.ToggleShow()
end

Controller = {
    --- @norag
    Init = ControllerInit,

    PlayerMove = {
        --- @arg1 target : Creature
        SetTarget = PlayerMoveController.SetTarget,
    },

    SpellCast = {
        --- @arg1 target : Creature
        SetTarget = PlayerSpellCaster.SetTarget,

        --- @arg1 position : int
        --- @arg2 key : KeyFunction
        SetHotKey = PlayerSpellCaster.SetHotKey,
    },

    PlayerEpicBar = {
        --- @arg1 target : Creature
        SetTarget = EpicBarController.SetPlayerBarTarget
    },

    AutoCaster = {
        --- @arg1 spell : Spell
        Register = AutoSpellCaster.Register,

        --- @arg1 spell : Spell
        UnRegister = AutoSpellCaster.Register,

        --- @arg1 spell : Spell
        CastByStyle = AutoSpellCaster.CastByStyle,
    },
}