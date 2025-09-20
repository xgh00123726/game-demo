using GameBase.Inventorys;

namespace Instance.UI.MVC
{
    public enum InventoryTag
    {
        None = 0,
        Equipment,
        SpellActionModify,
    }
    public class InventoryData : IInventoryItem
    {
        public InventoryTag tag;
        public int buffID;
        public Constructor.Spells.Action.Modifyables.Modifier.Type spellActionModifierType;
        public int spellActionModifyerID;

        public int IconTextureID { get; set;}
        public int ID {  get; set; }
    }
}
