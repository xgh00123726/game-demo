using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class SpellViewItem : BaseViewItem
    {
        internal Material iconMaterial;
        internal TextMeshProUGUI timeTMP;
        internal TextMeshProUGUI chargeTMP;

        public IViewableSpell bindSpell;
        public SpellViewItem()
        {
            ObjID = 11;
        }
    }
}
