using GameBase.Buffs;
using GameBase.Modify;
using GameBase.UI;

namespace Instance.Modify
{
    public class ViewableAttr<T> : IViewableAttr
    {
        private int _iconTextureID;
        private Modifyable<T> attr;
        public ViewableAttr(Modifyable<T> attr, int iconTextureID)
        {
            this.attr = attr;
            _iconTextureID = iconTextureID;
        }
        
        string IViewableAttr.Value =>  attr.Value.ToString();
        int IViewableAttr.IconTextureID => _iconTextureID;
    }
}
