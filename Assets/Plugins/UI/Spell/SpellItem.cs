using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class SpellItem : BaseViewItem
    {
        internal Material iconMaterial;
        internal TextMeshProUGUI timeTMP;
        internal TextMeshProUGUI chargeTMP;

        public IViewableSpell bindSpell;

        internal override int IconTexureID => bindSpell.IconTextureID;
        public SpellItem()
        {
            ObjID = 11;
        }
    }
}
