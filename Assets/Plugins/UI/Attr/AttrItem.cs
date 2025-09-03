using System;
using TMPro;

namespace GameBase.UI
{
    public class AttrItem : BasePanelItem
    {
        public IViewableAttr bindAttr;

        internal TextMeshProUGUI valueTMP;
        internal override int IconTexureID => bindAttr.IconTextureID;

        public AttrItem()
        {
            ObjID = 16;
        }
    }
}
