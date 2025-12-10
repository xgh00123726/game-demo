local AlignType = CS.Instance.AlignType
local CanvasType = CS.GameBase.UI.FreeViewItem.CanvasType

UIData = {
    Enum = {
        AlignType = AlignType,
        CanvasType = CanvasType,
    },
    CommonDragView = {
        PrefabName = "Prefabs/UI/InventoryShadowItem",
    },
    CommonDetailView = {
        PrefabName = "Prefabs/UI/DetailUI",
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
            xInterval = 185,
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
    EpicBonusSelect = {
        Layout = {
            xInterval = 600,
            yInterval = 100,
            width = 1810,
            height = 600,
            align = AlignType.Left | AlignType.Bottom
        }
    },
    ToolBar = {
        ExpandTime = 0.2,
        CollapseTime = 0.2,
        ExpandTextureName = "Textures/Icon/arrow_left_ring.png",
        CollapseTextureName = "Textures/Icon/arrow_right_ring.png",
        Items = {
            Parent = {
                PrefabName = "UI/ToolBarItem.prefab",
                IconTextureName = "Textures/Icon/arrow_left_ring.png",
                Position = {
                    x = 1700,
                    y = 900
                }
            },
            BackPack = {
                PrefabName = "UI/ToolBarItem.prefab",
                IconTextureName = "Textures/Icon/BackPack.png",
                Position = {
                    x = 1600,
                    y = 900
                }
            },
            Hot = {
                PrefabName = "UI/ToolBarItem.prefab",
                IconTextureName = "Textures/Icon/hot.png",
                Position = {
                    x = 1500,
                    y = 900
                }
            },
            Market = {
                PrefabName = "UI/ToolBarItem.prefab",
                IconTextureName = "Textures/Icon/market.png",
                Position = {
                    x = 1400,
                    y = 900
                }
            },
            Settings = {
                PrefabName = "UI/ToolBarItem.prefab",
                IconTextureName = "Textures/Icon/setting.png",
                Position = {
                    x = 1300,
                    y = 900
                }
            },
            Todo = {
                PrefabName = "UI/ToolBarItem.prefab",
                IconTextureName = "Textures/Icon/todo.png",
                Position = {
                    x = 1200,
                    y = 900
                }
            },
            Weapon = {
                PrefabName = "UI/ToolBarItem.prefab",
                IconTextureName = "Textures/Icon/wepon.png",
                Position = {
                    x = 1100,
                    y = 900
                }
            },
        }
    },
}