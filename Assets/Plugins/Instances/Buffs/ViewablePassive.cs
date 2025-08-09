using GameBase.Buffs;
using GameBase.UI;

namespace Instance.Buffs
{
    public class ViewablePassive : Buff,
        IViewablePassive
    {
        public int textureID = 0;
        int IViewablePassive.TextureID => textureID;

        public ViewablePassive()
        {
            RegistertoActivesDelegate.Add(ShowBuffUI);
        }

        private void ShowBuffUI(Buff b)
        {
            var buffUI = PassivePanel.Instance.NewEntity<PassiveItem>();
            buffUI.bindPassive = this;
        }
    }
}
