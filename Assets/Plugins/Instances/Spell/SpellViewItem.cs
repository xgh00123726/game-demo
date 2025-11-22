using GameBase.UI;
using TMPro;
using UnityEngine;

namespace Instance
{
    public class SpellViewItem : BaseViewItem
    {
        internal TextMeshProUGUI timeTMP;
        internal TextMeshProUGUI chargeTMP;

        public MaskImage MaskImage { get; set; }
        public float CoolingTimeRemain { get; set; }
        public float CoolingTimeSet { get; set; }
    }
}
