local PlayerMoveController = CS.Instance.PlayerMoveController
local PlayerSpellCaster = CS.Instance.PlayerSpellCaster
local EpicBarController = CS.Instance.EpicBarController
local AutoSpellCaster = CS.Instance.AutoSpellCaster
local DrawCreatureRadius = CS.Instance.DrawCreatureRadius

local function ControllerInit()
    PlayerMoveController.Instance:SetActive(true)
    PlayerMoveController.OnMoveInput = Controller.PlayerMove.OnMoveInput

    PlayerSpellCaster.Instance:SetActive(true)

    EpicBarController.Instance:SetActive(true)

    AutoSpellCaster.Instance:SetActive(true)

    -- DrawCreatureRadius.Instance:SetActive(true)

    CS.GameBase.Projectiles.ProjectileGizmos.ToggleShow()
    CS.GameBase.Creatures.CreatureGizmosDraw.ToggleShow()
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
        --- @arg1 target : Creature
        Register = AutoSpellCaster.Register,

        --- @arg2 target : Creature
        UnRegister = AutoSpellCaster.Register,
    },
}