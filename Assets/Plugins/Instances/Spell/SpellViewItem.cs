using GameBase.UI;
using TMPro;
using UnityEngine;

namespace Instance
{
    public class SpellViewItem : BaseViewItem
    {
        internal TextMeshProUGUI timeTMP;
        internal TextMeshProUGUI chargeTMP;

        public MaskImage maskImage;
        public float coolingTimeRemain;
        public float coolingTimeSet;
    }
}
