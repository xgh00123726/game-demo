using GameBase.Buffs;
using GameBase.Tools;
using GameBase.UI;

namespace Instance.Buffs
{
    public class ViewableBuff :
        IViewableBuff
    {
        public ViewableBuff(Buff buff)
        {
            this.buff = buff;
        }

        private Buff buff;

        float IViewableBuff.DurationRemain => buff.DurationRemain;

        float IViewableBuff.DurationSet => buff.durationSet;

        int IViewableBuff.StackNum => 0;

        bool IViewableBuff.Alive => buff.ALive;
    }
}
