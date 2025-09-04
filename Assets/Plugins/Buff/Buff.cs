using GameBase.Modify;
using GameBase.Tools;
using System;
using GameBase.EntitySystem;

namespace GameBase.Buffs
{
    public enum UIStyle
    {
        None = 0,

        Buff = 1 << 0,
        Passive = 1 << 1,
        Spell = 1 << 2,
        Equipment = 1 << 3,
    }

    public class Buff : IEntity,
        IPoolable
    {
        internal protected float durationRemain;
        internal protected int stackNum = 1;
        internal protected bool alive;

        public UIStyle uiStyle = UIStyle.None;
        public int textureID;
        public float durationSet;
        public IBuffOwner owner;
        public BuffModifyers modifyers = new ();

        int IEntity.InstanceID { get; set; }

        public bool ALive => alive;
        public float DurationRemain => durationRemain;

        void IPoolable.AfterGet()
        {
            textureID = 0;
            alive = true;
            uiStyle = UIStyle.None;
            durationRemain = 0;
        }

        void IPoolable.BeforeRelease()
        {
            textureID = 0;
            alive = false;
            owner = null;
        }
    }
}
