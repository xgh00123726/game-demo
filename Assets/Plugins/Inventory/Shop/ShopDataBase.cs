using GameBase.Tools;

namespace GameBase.Inventorys
{
    public enum ShopItemType
    {
        InventoryItem,
        Buff,
        Modifier,
    }
    public struct ShopItemInfo
    {
        public ShopItemType type;
        public int id;
        public int iconTextureID;
        public int rarity;
        public int price;
    }
    public class ShopDataBase : CsvDataBase<ShopItemInfo, ShopDataBase>
    {
        protected override string DataBasePath => CsvDataBasePath.DefaultFolder("ShopItemDataBase.csv");
    }
}
