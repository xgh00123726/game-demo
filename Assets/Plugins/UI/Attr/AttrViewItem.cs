using System;
using TMPro;

namespace GameBase.UI
{
    public class AttrViewItem : BaseViewItem
    {
        internal TextMeshProUGUI valueTMP;
        public float Value
        {
            set
            {
                valueTMP.text = String.Format("{0:0.##}", value);
            }
        }
        public AttrViewItem()
        {
            ObjID = 16;
        }
    }
}
