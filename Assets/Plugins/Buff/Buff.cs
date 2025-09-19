using GameBase.Modify;
using GameBase.Tools;
using System;
using GameBase.EntitySystem;
using UnityEngine;

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

    public class Buff : 
        IPoolable
    {
        internal float durationRemain;
        internal float instantiateTime;
        internal int stackNum = 1;
        internal bool alive;
        internal IBuffOwner owner;

        public UIStyle uiStyle = UIStyle.None;
        public int textureID;
        public float durationSet;
        public BuffModifyers modifyers = new ();

        public bool ALive => alive;
        public float DurationRemain => durationRemain;

        void IPoolable.AfterGet()
        {
            textureID = 0;
            alive = true;
            uiStyle = UIStyle.None;
            durationRemain = 0;
            instantiateTime = Time.time;
        }

        void IPoolable.BeforeRelease()
        {
            textureID = 0;
            alive = false;
            owner = null;
        }
    }
}
