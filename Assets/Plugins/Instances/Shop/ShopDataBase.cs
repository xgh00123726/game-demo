using GameBase.Tools;

namespace Instance
{
    public enum ShopItemType
    {
        InventoryItem,
        Buff,
        Modifier,
    }
    public struct ShopItemInfo
    {
        public ShopItemType Type {  get; set; }
        public int TypeID { get; set; }
        public string TextureName { get; set; }
        public int Rarity { get; set; }
        public int Price { get; set; }
    }
    public class ShopDataBase : CsvDataBase<ShopItemInfo, ShopDataBase>
    {
        protected override string DataBasePath => CsvDataBasePath.DefaultFolder("ShopItemDataBase.csv");
    }
}
