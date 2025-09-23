using GameBase.Inventorys;
using GameBase.UI.MVC;

namespace Instance.UI.MVC
{
    public enum InventoryTag
    {
        None = 0,
        Equipment,
        SpellActionModify,
    }
    public class InventoryData
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
