namespace Instance
{
    public enum InventoryTag
    {
        None = 0,
        Equipment,
        SpellActionModify,
    }
    public class CommonInventoryData
    {
        public InventoryTag tag;
        public int buffID;
        public Constructor.Spells.Action.Modifyables.Modifier.Type spellActionModifierType;
        public int spellActionModifyerID;
        public int rarity;
        public int iconTextureID;
        public int ID;
    }
}
