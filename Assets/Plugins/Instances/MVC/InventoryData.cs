using GameBase.Inventorys;

namespace Instance.MVC
{
    public enum InventoryTag
    {
        None = 0,
        Equipment,
        SpellActionModify,
    }
    public class InventoryData : IInventoryItem
    {
        public int id;
        public InventoryTag tag;
        public int buffID;
        public int spellActionModifyerID;

        public int IconTextureID { get; set;}
    }
}
