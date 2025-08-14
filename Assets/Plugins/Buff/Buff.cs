using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace GameBase.Buffs
{
    public class Buff : IEntity
    {
        internal protected float durationRemain;
        internal protected int stackNum = 1;
        internal protected bool alive = false;

        public float durationSet = 2;
        public IBuffOwner owner;
        public List<Action<Buff>> RegistertoActivesDelegate = new();
        public List<Action<Buff>> RemoveFromActiveDelegate = new();

        int IEntity.InstanceID { get; set; }
    }
}
