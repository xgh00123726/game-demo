using GameBase.Tools;
using System;

namespace GameBase.Buffs
{
    public class Buff : IEntity
    {
        internal protected int textureID = -1;
        internal protected float durationRemain;
        internal protected float durationSet = 1;
        internal protected int stackNum = 1;
        internal protected bool alive = false;

        public PossibleObj<IBuffOwner> owner;

        public Action<Buff> InstantiateDelegate;
        public Action<Buff> ReleaseDelegate;

        int IEntity.InstanceID { get; set; }
    }
}
