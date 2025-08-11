using TMPro;
using UnityEngine.EventSystems;

namespace GameBase.UI
{
    public class SpellItem : DetailableBaseItem
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
