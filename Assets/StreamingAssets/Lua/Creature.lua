require("CreatureData")

local Factory = CS.Constructor.Creatures.Factory.Instance
local Vector3 = CS.UnityEngine.Vector3
local Timer = CS.GameBase.Tools.Timer
local AIFactory = CS.GameBase.AI.AIFactory

local function GenPlayer()
    local playerData = CreatureData.Player
    local player = Factory:Get(playerData.Type, playerData.ID)
    local x = playerData.GenPosition.x
    local y = playerData.GenPosition.y
    local z = playerData.GenPosition.z
    player.Position = Vector3(x, y, z)

    player:AddSpell(Spell.Factory:Get(SpellData.Enum.SpellType.Common, 0))
    player:AddSpell(Spell.Factory:Get(SpellData.Enum.SpellType.Common, 1))
    player:AddSpell(Spell.Factory:Get(SpellData.Enum.SpellType.Common, 5))
    player:AddSpell(Spell.Factory:Get(SpellData.Enum.SpellType.Common, 4))

    print(player:AddEquipment(3, 2))

    Creature.Player = player
end

--- 创建一个怪物刷新点
---@param creatureBaseData any:刷新点数据表
local function CreateBase( creatureBaseData )
    local baseState = {
        currentCreatureNum = 0,
    }
    local capacity = creatureBaseData.Capacity
    local refreshPeriod = creatureBaseData.RefreshPeriod
    local refreshPerNum = creatureBaseData.RefreshPerNum
    local creatureType = creatureBaseData.CreatureType
    local creatureID = creatureBaseData.CreatureID
    local basePosition = creatureBaseData.Position
    local generateRange = creatureBaseData.GenerateRange
    local xMin = basePosition.x + generateRange.x.min
    local xMax = basePosition.x + generateRange.x.max
    local yMin = basePosition.y + generateRange.y.min
    local yMax = basePosition.y + generateRange.y.max
    local zMin = basePosition.z + generateRange.z.min
    local zMax = basePosition.z + generateRange.z.max

    Timer.AddLoop(refreshPeriod, function ()
        if (baseState.currentCreatureNum < capacity) then
            for _ = 1, refreshPerNum do
                local c = Factory:Get(creatureType, creatureID)
                local x = math.random(xMin, xMax)
                local y = math.random(yMin, yMax)
                local z = math.random(zMin, zMax)
                c.Position = Vector3(x, y, z)
                c.OnRelease = function ()
                    baseState.currentCreatureNum = baseState.currentCreatureNum - 1
                end
                baseState.currentCreatureNum = baseState.currentCreatureNum + 1
            end
        end
    end)

end


local function GenInitEnemy()
    local c = Factory:Get(CreatureData.CreatureType.Common, 1)
    c.Position = Vector3(-6, -7, 0)
end

local function CreaturesInit()
    GenPlayer()
end

Creature = {
    --- @noarg
    Init = CreaturesInit,

    --- @Instance creature Factory
    Factory = Factory,

    --- @arg1 creatureBaseData : table
    CreateBase = CreateBase,
}