using GameBase.Inventorys;

namespace Instance.MVC
{
    public enum InventoryTag
    {
        None = 0,
        Equipment,
        SpellActionModify,
    }
    public struct CommonItemData
    {
        public int id;
        public InventoryTag tag;
        public int iconTextureID;
        public int buffID;
        public int spellActionModifyerID;
    }
}
