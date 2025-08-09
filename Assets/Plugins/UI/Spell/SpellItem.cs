using TMPro;

namespace GameBase.UI
{
    public class SpellItem : BasePanelItem
    {
        public SpellItem()
        {
            ObjID = 11;
        }

        public IViewableSpell bindSpell;
        internal TextMeshProUGUI timeTMP;
        internal TextMeshProUGUI chargeTMP;

        internal override int IconTexureID => bindSpell.TextureID;
    }
}
