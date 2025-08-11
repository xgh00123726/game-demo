using GameBase.Buffs;
using GameBase.UI;

namespace Instance.Buffs
{
    public class ViewablePassive : Buff,
        IViewablePassive
    {
        public int textureID = 0; 
        private static DetailUI detailUI;
        int IViewablePassive.TextureID => textureID;

        static ViewablePassive()
        {
            detailUI = DetailUISys.Instance.NewEntity<DetailUI>();
        }

        public ViewablePassive()
        {
            RegistertoActivesDelegate.Add(ShowBuffUI);
        }

        private void ShowBuffUI(Buff b)
        {
            var buffUI = PassivePanel.Instance.NewEntity<PassiveItem>();
            buffUI.bindPassive = this;

            buffUI.AfterInstantiateUObjectDelegate = (BasePanelItem item) =>
            {
                detailUI.detailables.Add(buffUI);
            };
        }
    }
}
