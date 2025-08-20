using GameBase.Buffs;
using GameBase.Modify;
using GameBase.UI;

namespace Instance.Modify
{
    public class ViewableAttr<T> : IViewableAttr
    {
        public ViewableAttr(Modifyable<T> attr)
        {
            this.attr = attr;
        }
        private Modifyable<T> attr;
        string IViewableAttr.Value =>  attr.Value.ToString();
    }
}
