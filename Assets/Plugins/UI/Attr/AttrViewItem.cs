using System;
using TMPro;

namespace GameBase.UI
{
    public class AttrViewItem : BaseViewItem
    {
        public IViewableAttr bindAttr;

        internal TextMeshProUGUI valueTMP;

        public AttrViewItem()
        {
            ObjID = 16;
        }
    }
}
