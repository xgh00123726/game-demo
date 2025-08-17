using GameBase.Buffs;
using GameBase.UI;

namespace Instance.Buffs
{
    public class ViewablePassive :
        IViewablePassive
    {
        public ViewablePassive(Buff passive, int textureID)
        {
            this.passive = passive;
            this.textureID = textureID;
        }

        private Buff passive;
        private int textureID;

        int IViewablePassive.TextureID => textureID;
    }
}
