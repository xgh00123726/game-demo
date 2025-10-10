using GameBase.EntitySystem;
using GameBase.Math;
using System;
using System.Collections.Generic;

namespace GameBase.Triggers
{ 
    public enum TrigStyle
    {
        External,
        PeriodImmediate,
        PeriodNext,
        Always,
    }

    public class Trigger :
        IPoolable
    {
        // require
        public int maxeffectTimes;
        public ITriggerAttach attach;
        public ITriggerTarget target;
        public IShape2D shape;
        public ITriggerTargetsSet targetsSet;
        public ITriggerOwner owner;
        public ITriggerAction action;

        // optional
        public float trigPeriod;
        public bool hasWhite;
        public float existTime;
        public TrigStyle trigStyle;
        public Action OnTrig;

        internal float lastTrigTime;
        internal float instantiateTime;
        internal bool isTrig;
        internal int actualEffectTimes;
        internal HashSet<ITriggerTarget> whites;

        public void Trig()
        {
            isTrig = true;
        }

        public void SetWhites()
        {
            hasWhite = true;
            whites = new();
        }

        void IPoolable.AfterGet()
        {
            trigPeriod = 1;
            trigStyle = TrigStyle.External;
            existTime = 1;
            actualEffectTimes = 0;
            maxeffectTimes = 1;
            hasWhite = false;
            isTrig = false;
        }

        void IPoolable.BeforeRelease()
        {
            whites = null;
            shape = null;
            targetsSet = null;
            target = null;
            owner = null;
            action = null;
        }
    }
}
