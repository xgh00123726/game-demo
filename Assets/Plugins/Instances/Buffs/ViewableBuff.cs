using GameBase.Buffs;
using GameBase.UI;

namespace Instance.Buffs
{
    public class ViewableBuff : Buff,
        IViewableBuff
    {
        float IViewableBuff.DurationRemain => durationRemain;

        float IViewableBuff.DurationSet => durationSet;

        int IViewableBuff.StackNum => stackNum;

        int IViewableBuff.TextureID => textureID;

        bool IViewableBuff.Alive => alive;
    }
}
