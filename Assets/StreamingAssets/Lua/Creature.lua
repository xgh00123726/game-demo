require("CreatureData")
require("Controller")

local Factory = CS.Constructor.Creatures.CreatureFactory.Instance
local Vector3 = CS.UnityEngine.Vector3
local Timer = CS.GameBase.Tools.Timer
local AIFactory = CS.GameBase.AI.AIFactory
local CreatureSelector = CS.Instance.CreatureSelector.Instance

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
    player:AddSpell(Spell.GenBlinkSpell())

    player:SetDefaultGetExpText()

    player.OnLevelUp = function ( c )
        for i = 1, #playerData.LevelUpAttr do
            local attr = playerData.LevelUpAttr[i].attr
            local value = playerData.LevelUpAttr[i].value
            player.modifyables:ModifyKey(attr, value)
        end

        Shop.AttrSelectInventory:Refresh()
        UI.AttrSelect.Panel:UpdatePanel(Shop.AttrSelectInventory)
        -- UI.AttrSelect.Panel:Show()
    end

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
    local deadExp = creatureBaseData.DeadExp

    local OnDead = function ( c )
        baseState.currentCreatureNum = baseState.currentCreatureNum - 1
        Creature.Player:GetExp(c.deadExp)
    end

    Timer.AddLoop(refreshPeriod, function ()
        if (baseState.currentCreatureNum < capacity) then
            for _ = 1, refreshPerNum do
                local c = Factory:Get(creatureType, creatureID)
                local x = math.random(xMin, xMax)
                local y = math.random(yMin, yMax)
                local z = math.random(zMin, zMax)
                c.Position = Vector3(x, y, z)
                c.deadExp = deadExp
                c.OnDead = OnDead
                c:AddSpell(Spell.Factory:Get(SpellData.Enum.SpellType.Common, 6))
                c:AddSpell(Spell.Factory:Get(SpellData.Enum.SpellType.Common, 8))

                if (c.ai ~= nil) then
                    c.ai.arriveDis = 2
                    c.ai.OnFollowArrive = function ()
                        -- c.spells[0]:TryCast()
                        Controller.AutoCaster.CastByStyle(c.spells[1])
                    end
                end

                baseState.currentCreatureNum = baseState.currentCreatureNum + 1
            end
        end
    end)

end


local function DrawCreatureBase( baseData )
    local posX = baseData.Position.x
    local posY = baseData.Position.y
    local posZ = baseData.Position.z
    local xMin = baseData.GenerateRange.x.min + posX
    local xMax = baseData.GenerateRange.x.max + posX
    local zMin = baseData.GenerateRange.z.min + posZ
    local zMax = baseData.GenerateRange.z.max + posZ

    local rect = CS.UnityEngine.Rect.MinMaxRect(xMin, zMin, xMax, zMax)
    Controller.AreaDrawer.SetDrawArea(rect, posY)
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

    --- @arg1 creatureBaseData : table
    DrawCreatureBase = DrawCreatureBase,

    --- @arg1 current : Creature
    GetCurrentSelect = function ()
        return CreatureSelector.currentSelect
    end
}