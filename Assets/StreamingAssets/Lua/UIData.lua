local AlignType = CS.Instance.AlignType

UIData = {
    AlignType = AlignType,
    CommonDragView = {
        PrefabID = 37,
    },
    CommonDetailView = {
        PrefabID = 4,
    },
    Attr = {
        ShowAttr = {0, 1, 3, 2, 5, 7, 4, 12, 14, 6, 15, 9},
        Layout = {
            xInterval = 150,
            yInterval = 35,
            width = 460,
            height = 600,
            align = AlignType.Left | AlignType.Bottom
        }
    },
    Inventory = {
        EnterDragTime = 0.1,
        EnterDetailTime = 0.2,
        Layout = {
            xInterval = 115,
            yInterval = 115,
            width = 1200,
            height = 0,
            align = AlignType.Left | AlignType.Top
        }
    },
    Shop = {
        EnterDetailTime = 0.1,
        Layout = {
            xInterval = 150,
            yInterval = 115,
            width = 1200,
            height = 600,
            align = AlignType.Left | AlignType.Bottom
        }
    },
    Equipment = {
        EnterDetailTime = 0.2,
        EnterDragTime = 0.1,
        ItemNum = 6,
        Layout = {
            xInterval = 95,
            yInterval = 90,
            width = 315,
            height = 600,
            align = AlignType.Left | AlignType.Bottom
        }
    },
    Buff = {
        Layout = {
            xInterval = 31.5,
            yInterval = 31.5,
            width = 1000,
            height = 600,
            align = AlignType.Left | AlignType.Bottom
        }
    },
    Spell = {
        Layout = {
            xInterval = 235,
            yInterval = 100,
            width = 1200,
            height = 600,
            align = AlignType.Left | AlignType.Bottom
        }
    },
    SpellActionModifier = {
        Layout = {
            xInterval = 180,
            yInterval = 100,
            width = 1200,
            height = 600,
            align = AlignType.Left | AlignType.Bottom
        }
    },
    AttrSelect = {
        Layout = {
            xInterval = 600,
            yInterval = 100,
            width = 1810,
            height = 600,
            align = AlignType.Left | AlignType.Bottom
        }
    },
}