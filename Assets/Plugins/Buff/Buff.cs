using GameBase.Modify;
using System.Collections.Generic;

namespace GameBase.Buffs
{
    public enum BuffTag : uint
    {
        InfiDuration = 1 << 0,
    }
    public class Buff
    {
        internal float durationRemain;
        internal float instantiateTime;
        internal int stackNum = 1;
        internal bool alive;
        internal IBuffOwner owner;

        public BuffTag tag;
        public string textureName;
        public int rarity;
        public float durationSet;
        public List<KeyValuePair<int, float>> iModifiers;
        public List<Modifyer> modifyers = new();

        public bool ALive => alive;
        public float DurationRemain => durationRemain;

        public bool IsInfiDuration => (tag & BuffTag.InfiDuration) != 0;


        public void AddTo(IBuffOwner owner, float duration = -1)
        {
            this.owner = owner;

            if (duration > 0)
            {
                this.durationSet = duration;
                this.durationRemain = duration;
            }
        }

        public void Remove()
        {
            BuffSys.Instance.RemoveEntity(this);
        }
    }
}
