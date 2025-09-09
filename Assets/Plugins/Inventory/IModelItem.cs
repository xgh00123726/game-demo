namespace GameBase.Inventorys
{
    public enum InventoryTag
    {
        None = 0,
        Equipment,
        SpellActionModify,
    }
    public interface IModelItem
    {
        public int ID { get; set; }
        public InventoryTag Tag { get; set; }
    }
}
