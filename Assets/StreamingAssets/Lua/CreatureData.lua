local CreatureType = CS.Constructor.Creatures.Type
local AIType = CS.GameBase.AI.AIType
local SpellType = CS.Constructor.Spells.Main.Type

CreatureData = {
    CreatureType = CreatureType,
    AIType = AIType,

    Player = {
        Name = "Player",
        GenPosition = {
            x = -6,
            y = -7,
            z = 4,
        },
        LevelUpAttr = {
            { attr = "strength", value = 1.1 },
            { attr = "agility", value = 3.5 },
            { attr = "intelligence", value = 2 },
            { attr = "universal", value = 1 },
        }
    },

    CreatureBase = {
        Base1 = {
            Capacity = 5,
            RefreshPeriod = 1,
            RefreshPerNum = 3,
            CreatureName = "Enermy1",
            Spells = {"Attack1_1"},
            DeadExp = 3,
            Position = {
                x = -6,
                y = -7,
                z = 0,
            },
            GenerateRange = {
                x = { min = -2, max = 2 },
                y = { min = 0, max = 0 },
                z = { min = -2, max = 2 },
            },
        },
        Base2 = {
            Capacity = 1,
            RefreshPeriod = 1,
            RefreshPerNum = 1,
            CreatureName = "Enermy1",
            Spells = {"Attack1_1"},
            DeadExp = 3,
            Position = {
                x = 0,
                y = -7,
                z = 6,
            },
            GenerateRange = {
                x = { min = -2, max = 2 },
                y = { min = 0, max = 0 },
                z = { min = -2, max = 2 },
            },
        },
    }
}