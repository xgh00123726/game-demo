using GameBase.UI;
using System;
using TMPro;

namespace Instance
{
    public class AttrViewItem : BaseViewItem
    {
        internal TextMeshProUGUI valueTMP;

        internal int attrKey = -1;
        public float Value
        {
            set
            {
                valueTMP.text = String.Format("{0:0}", value);
            }
        }
    }
}
