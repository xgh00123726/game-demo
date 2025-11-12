using GameBase.EntitySystem;
using GameBase.Math;
using System;
using System.Collections.Generic;

namespace GameBase.Triggers
{ 
    public enum TrigStyle
    {
        External,
        Period,
        Always,
        Once,
    }

    public class Trigger
    {
        // require
        public int maxEffectTimes;
        public ITriggerAttach attach;
        public ITriggerTarget target;
        public IShape2D shape;
        public ITriggerTargetsSet targetsSet;
        public ITriggerOwner owner;
        public ITriggerAction action;

        // optional
        public float delay;
        public float trigPeriod;
        public bool hasWhite;
        public float existTime;
        public TrigStyle trigStyle;
        public ITriggerCamp targetCamp;

        public string createEffect;
        public string trigEffect;
        public string hitEffect;

        public string createAudio;
        public string trigAudio;
        public string hitAudio;

        public Action OnTrig;
        public Action OnTrigEnd;

        internal float lastTrigTime;
        internal float instantiateTime;
        internal bool isTrig;
        internal int actualEffectTimes;
        internal HashSet<ITriggerTarget> whites;

        public int ActualEffectTimes => actualEffectTimes;

        public void Trig()
        {
            isTrig = true;
        }
    }
}
