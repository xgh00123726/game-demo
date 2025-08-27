using GameBase.Buffs;
using GameBase.UI;
using System;

namespace Instance.Buffs
{
    public class ViewableEquipment :
        IViewableEquipment
    {
        public ViewableEquipment(Buff e)
        {
            this.e = e;
        }

        private Buff e;

        public float DurationRemain => e.DurationRemain;

        public float DurationSet => e.durationSet;

        public int StackNum => 0;

        public int TextureID => e.textureID;

        int IViewableEquipment.Position => 0;
    }
}
