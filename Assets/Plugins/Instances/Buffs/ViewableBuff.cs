using GameBase.Buffs;
using GameBase.Tools;
using GameBase.UI;

namespace Instance.Buffs
{
    public class ViewableBuff : Buff,
        IViewableBuff
    {
        public int textureID = 0;
        private static DetailUI detailUI;

        static ViewableBuff()
        {
            detailUI = DetailUISys.Instance.NewEntity<DetailUI>();
        }

        public ViewableBuff()
        {
            RegistertoActivesDelegate.Add(ShowBuffUI);
        }

        private void ShowBuffUI(Buff b)
        {
            var buffUI = BuffPanel.Instance.NewEntity<BuffItem>();
            buffUI.bindBuff = this;

            buffUI.AfterInstantiateUObjectDelegate = (BasePanelItem item) =>
            {
                detailUI.detailables.Add(buffUI);
            };
        }

        float IViewableBuff.DurationRemain => durationRemain;

        float IViewableBuff.DurationSet => durationSet;

        int IViewableBuff.StackNum => stackNum;

        int IViewableBuff.TextureID => textureID;

        bool IViewableBuff.Alive => alive;
    }
}
