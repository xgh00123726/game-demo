using System;
using TMPro;

namespace GameBase.UI
{
    public class AttrViewItem : BaseViewItem
    {
        public IViewableAttr bindAttr;

        internal TextMeshProUGUI valueTMP;
        internal override int IconTexureID => bindAttr.IconTextureID;

        public AttrViewItem()
        {
            ObjID = 16;
        }
    }
}
