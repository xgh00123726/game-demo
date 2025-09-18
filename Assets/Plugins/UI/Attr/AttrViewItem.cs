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
                valueTMP.text = value.ToString();
            }
        }
        public AttrViewItem()
        {
            ObjID = 16;
        }
    }
}
