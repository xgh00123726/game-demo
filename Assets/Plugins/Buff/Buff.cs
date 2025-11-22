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

        public BuffTag Tag { get; set; }
        public string TextureName { get; set; }
        public int Rarity { get; set; }
        public float DurationSet { get; set; }
        public List<KeyValuePair<int, float>> IntKeyModifiers { get; set; }
        public List<Modifyer> Modifyers { get; set; } = new();

        public bool ALive => alive;
        public float DurationRemain => durationRemain;

        public bool IsInfiDuration => (Tag & BuffTag.InfiDuration) != 0;


        public void AddTo(IBuffOwner owner, float duration = -1)
        {
            this.owner = owner;

            if (duration > 0)
            {
                this.DurationSet = duration;
                this.durationRemain = duration;
            }
        }

        public void Remove()
        {
            BuffSys.Instance.RemoveEntity(this);
        }
    }
}
