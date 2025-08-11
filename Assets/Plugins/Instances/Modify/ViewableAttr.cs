using GameBase.Buffs;
using GameBase.Modify;
using GameBase.UI;
using System;

namespace Instance.Modify
{
    public class ViewableAttr<T> : Modifyable<T>,
        IViewableAttr
    {
        private static DetailUI detailUI;

        public int textureID = 0;
        int IViewableAttr.TextureID => textureID;
        string IViewableAttr.Value =>  Value.ToString();

        static ViewableAttr()
        {
            detailUI = DetailUISys.Instance.NewEntity<DetailUI>();
        }

        public ViewableAttr()
        {
            RegistertoActivesDelegate.Add(ShowAttrUI);
        }

        private void ShowAttrUI(Modifyable<T> modifyable)
        {
            var attrUI = AttrPanel.Instance.NewEntity<AttrItem>();
            attrUI.bindAttr = this;

            attrUI.AfterInstantiateUObjectDelegate = (BasePanelItem item) =>
            {
                detailUI.detailables.Add(attrUI);
            };
        }
    }
}
