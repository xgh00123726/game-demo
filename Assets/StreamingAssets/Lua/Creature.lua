CreatureUtil = CS.LuaUtil.CreatureUtil

local function GenInitEnemy()
    CreatureUtil.Gen(0, 1, -5, 3)
end

Creature = {
    --- @nopara
    GenInitEnemy = GenInitEnemy,
}