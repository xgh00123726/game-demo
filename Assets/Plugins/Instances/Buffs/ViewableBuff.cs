using GameBase.Buffs;
using GameBase.Tools;
using GameBase.UI;

namespace Instance.Buffs
{
    public class ViewableBuff :
        IViewableBuff
    {
        public ViewableBuff(Buff buff, int textureID)
        {
            this.buff = buff;
            this.textureID = textureID;
        }

        private int textureID;
        private Buff buff;

        float IViewableBuff.DurationRemain => buff.DurationRemain;

        float IViewableBuff.DurationSet => buff.durationSet;

        int IViewableBuff.StackNum => 0;

        int IViewableBuff.TextureID => textureID;

        bool IViewableBuff.Alive => buff.ALive;
    }
}
