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
        }
    },

    CreatureBase = {
        Base1 = {
            Capacity = 10,
            RefreshPeriod = 5,
            RefreshPerNum = 1,
            CreatureType = CreatureType.Common,
            CreatureID = 1,
            Position = {
                x = -6,
                y = -7,
                z = 0,
            },
            GenerateRange = {
                x = { min = -1, max = 1 },
                y = { min = 0, max = 0 },
                z = { min = -1, max = 1 },
            },
        }
    }
}