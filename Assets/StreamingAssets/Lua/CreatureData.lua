local CreatureType = CS.Constructor.Creatures.Type
local AIType = CS.GameBase.AI.AIType

CreatureData = {
    CreatureType = CreatureType,
    AIType = AIType,

    Player = {
        Type = CreatureType.Common,
        ID = 0,
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
            Capacity = 1,
            RefreshPeriod = 1,
            RefreshPerNum = 1,
            CreatureType = CreatureType.Common,
            CreatureID = 2,
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
        }
    }
}