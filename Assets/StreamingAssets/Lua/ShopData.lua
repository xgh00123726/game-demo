local ShopItemType = CS.Instance.ShopItemType

ShopData = {
    Enum = {
        ItemType = ShopItemType,
    },
    
    InteracitiveDistance = 100,
    Shop1 = {
        Name = "shop1",
        DataFile = "CommonShop_1.csv",
        PrefabName = "Prefabs/Projectile/FallingStone",
        GoodsNum = 5,
        Position = {
            x = -6,
            y = -7,
            z = 7,
        },
        DetectRange = 2,
    },
    AttrSelect = {
        S1 = {
            DataFile = "AttrSelect_1.csv",
            GoodsNum = 3,
        },
    },
}