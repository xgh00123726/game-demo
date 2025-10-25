require("SpellData")

local SpellFactory = CS.Constructor.Spells.Main.SpellFactory.Instance
local SpellSys = CS.GameBase.Spells.SpellSys.Instance

local function CreatureBlink( spell )
    local c = spell.speller
    local inter = spell.interactive

    c.mover:LookAt(inter.position)
    c.Position = inter.position
    c.mover:Stop()
end

local function GenBlinkSpell()
    local e = SpellSys:NewEntity()
    e.interactive = CS.Constructor.Spells.Interactive.DotExternalSet()
    e.interactive.indicatorType = CS.GameBase.Indicators.IndicatorType.Linear
    e.action = CS.Constructor.Spells.Action.Editable()
    e.action.Action = CreatureBlink
    e.spellCoolingdown.CoolingSet = 1
    e.iconTextureID = 22

    return e
end

Spell = {
    ---@instance spell factory
    Factory = SpellFactory,

    GenBlinkSpell = GenBlinkSpell,
}