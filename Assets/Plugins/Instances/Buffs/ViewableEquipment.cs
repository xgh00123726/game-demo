using GameBase.Buffs;
using GameBase.UI;
using System;

namespace Instance.Buffs
{
    public class ViewableEquipment :
        IViewableEquipment
    {
        public ViewableEquipment(Buff equipment, int textureID)
        {
            this.equipment = equipment;
            this.textureID = textureID;
        }


        private int textureID;
        private Buff equipment;

        public float DurationRemain => equipment.DurationRemain;

        public float DurationSet => equipment.durationSet;

        public int StackNum => 0;

        public int TextureID => textureID;

        int IViewableEquipment.Position => 0;
    }
}
