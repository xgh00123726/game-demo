using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class SpellViewItem : BaseViewItem
    {
        internal Material iconMaterial;
        internal TextMeshProUGUI timeTMP;
        internal TextMeshProUGUI chargeTMP;

        public ISpellViewInfo viewInfo;
        public SpellViewItem()
        {
            ObjID = 11;
        }
    }
}
