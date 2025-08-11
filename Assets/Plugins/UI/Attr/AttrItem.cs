using System;
using TMPro;

namespace GameBase.UI
{
    public class AttrItem : DetailableBaseItem
    {
        public AttrItem()
        {
            ObjID = 16;
        }
        public IViewableAttr bindAttr;

        internal TextMeshProUGUI valueTMP;
        internal override int IconTexureID => bindAttr.TextureID;

        public override void OnPointerEnter()
        {
            detailContent.value = bindAttr.Value;
        }
    }
}
