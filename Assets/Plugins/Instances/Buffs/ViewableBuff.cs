using GameBase.Buffs;
using GameBase.Tools;
using GameBase.UI;

namespace Instance.Buffs
{
    public class ViewableBuff : Buff,
        IViewableBuff
    {
        public int textureID = 0;

        public ViewableBuff()
        {
            RegistertoActivesDelegate.Add(ShowBuffUI);
        }

        private void ShowBuffUI(Buff b)
        {
            var buffUI = BuffPanel.Instance.NewEntity<BuffItem>();
            buffUI.bindBuff = this;
        }

        float IViewableBuff.DurationRemain => durationRemain;

        float IViewableBuff.DurationSet => durationSet;

        int IViewableBuff.StackNum => stackNum;

        int IViewableBuff.TextureID => textureID;

        bool IViewableBuff.Alive => alive;
    }
}
