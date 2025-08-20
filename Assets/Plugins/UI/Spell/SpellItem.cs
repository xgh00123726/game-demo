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
        public int iconTextureID;
        public IViewableSpell bindSpell;
        internal TextMeshProUGUI timeTMP;
        internal TextMeshProUGUI chargeTMP;

        internal override int IconTexureID => iconTextureID;
    }
}
