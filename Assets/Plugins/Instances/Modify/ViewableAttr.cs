using GameBase.Buffs;
using GameBase.Modify;
using GameBase.UI;
using System;

namespace Instance.Modify
{
    public class ViewableAttr<T> : IViewableAttr
    {
        public ViewableAttr(Modifyable<T> attr, int texureID)
        {
            this.textureID = texureID;
            this.attr = attr;
        }

        private int textureID = 0;
        private Modifyable<T> attr;
        int IViewableAttr.TextureID => textureID;
        string IViewableAttr.Value =>  attr.Value.ToString();
    }
}
