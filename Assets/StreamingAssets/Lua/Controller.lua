local MoveCommander = CS.Instance.MoveCommander
local SpellCaster = CS.Instance.SpellCaster
local EpicBarController = CS.Instance.EpicBarController
local AutoSpellCaster = CS.Instance.AutoSpellCaster
local DrawCreatureRadius = CS.Instance.DrawCreatureRadius
local RectDrawer = CS.Instance.RectDrawer
local CreatureSelector = CS.Instance.CreatureSelector
local TriggerMonitor = CS.Instance.TriggerMonitor
local AreaDrawer = CS.Instance.AreaDrawer
local InputsEventer = CS.Instance.InputsEventer
local KeyFunction = CS.GameBase.Tools.KeyFunction
local ColliderCreator = CS.Instance.ColliderCreator

local GizmosCfg = CS.Instance.GizmosCfg
local CreatureGizmos = CS.Instance.CreatureGizmos
local ColliderGizmos = CS.Instance.ColliderGizmos
local TriggerGizmos = CS.Instance.TriggerGizmos

local function ControllerInit()
    MoveCommander.Instance:SetActive(true)
    MoveCommander.OnMoveInput = Controller.PlayerMove.OnMoveInput
    SpellCaster.Instance:SetActive(true)
    EpicBarController.Instance:SetActive(true)
    AutoSpellCaster.Instance:SetActive(true)
    DrawCreatureRadius.Instance:SetActive(true)
    TriggerMonitor.Instance:SetActive(true)
    AreaDrawer.Instance:SetActive(true)
    InputsEventer.Instance:SetActive(true)
    RectDrawer.Instance:SetActive(true)
    ColliderCreator.Instance:SetActive(false)

    local KeyFunction = CS.GameBase.Tools.KeyFunction
    CreatureSelector.AddHotKeyCreature(KeyFunction.SelectF1, Creature.Player)
    CreatureSelector.AddHotKeyCreature(KeyFunction.SelectF2, Creature.Player)
    CreatureSelector.Instance:SetActive(true)
    
    GizmosCfg.isDrawGizmos = true
    GizmosCfg.isDrawCollider = false
    GizmosCfg.isDrawMove = true
    GizmosCfg.isDrawForce = false
    GizmosCfg.isDrawAI = false
    GizmosCfg.isDrawAttackRange = false
    GizmosCfg.isDrawTrigger = true
    GizmosCfg.isDrawGrid = false
    GizmosCfg.isDrawObstacle = true
end

Controller = {
    Enum = {
        KeyFunction = KeyFunction,
    },

    --- @norag
    Init = ControllerInit,

    PlayerMove = {
        --- @arg1 target : Creature
        SetTarget = MoveCommander.SetTarget,
    },

    SpellCast = {
        --- @arg1 target : Creature
        SetTarget = SpellCaster.SetTarget,

        --- @arg1 position : int
        --- @arg2 key : KeyFunction
        SetHotKey = SpellCaster.SetHotKey,
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

    Inputs = {
        --- @arg1 func : KeyFunction
        --- @arg2 callback : Action
        RegisterKeyDownEvent = InputsEventer.RegisterKeyDownEvent,
    },

    AreaDrawer = {
        --- @arg1 area : Rect
        --- @arg2 drawY : float
        SetDrawArea = AreaDrawer.SetDrawArea,
    },

    ColliderCreator = ColliderCreator.Instance,
}