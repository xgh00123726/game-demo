require("CreatureData")

local Factory = CS.Constructor.Creatures.Factory.Instance

local function GenPlayer()
    local playerData = CreatureData.Player
    local player = Factory:Get(playerData.Type, playerData.ID)
    local x = playerData.GenPosition.x
    local y = playerData.GenPosition.y
    local z = playerData.GenPosition.z
    player.Position = CS.UnityEngine.Vector3(x, y, z)

    player:AddSpell(Spell.Factory:Get(SpellData.Enum.SpellType.Common, 0))
    player:AddSpell(Spell.Factory:Get(SpellData.Enum.SpellType.Common, 1))
    player:AddSpell(Spell.Factory:Get(SpellData.Enum.SpellType.Common, 5))
    player:AddSpell(Spell.Factory:Get(SpellData.Enum.SpellType.Common, 4))

    Creature.Player = player
end

local function GenInitEnemy()
    local c = Factory:Get(CreatureData.CreatureType.Common, 1)
    c.Position = CS.UnityEngine.Vector3(-6, -7, 0)
end

local function CreaturesInit()
    GenPlayer()
    GenInitEnemy()
end

Creature = {
    --- @noarg
    Init = CreaturesInit,

    --- @Instance creature Factory
    Factory = Factory,
}