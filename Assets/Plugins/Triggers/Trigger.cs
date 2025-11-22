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
        internal float lastTrigTime;
        internal float instantiateTime;
        internal bool isTrig;
        internal int actualEffectTimes;
        internal HashSet<ITriggerTarget> whites;
        // require
        public int MaxEffectTimes {  get; set; }
        public ITriggerAttach Attach { get; set; }
        public ITriggerTarget Target { get; set; }
        public IShape2D Shape { get; set; }
        public ITriggerTargetsSet TargetsSet { get; set; }
        public ITriggerOwner Owner { get; set; }
        public ITriggerAction Action { get; set; }
        // optional
        public float Delay { get; set; }
        public float TrigPeriod { get; set; }
        public bool HasWhite {  get; set; }
        public float ExistTime { get; set; }
        public TrigStyle TrigStyle { get; set; }
        public ITriggerCamp TargetCamp { get; set; }
        public string CreateEffect {  get; set; }
        public string TrigEffect { get; set; }
        public string HitEffect { get; set; }
        public string CreateAudio { get; set; }
        public string TrigAudio { get; set; }
        public string HitAudio { get; set; }
        public Action OnTrig { get; set; }
        public Action OnTrigEnd { get; set; }
        public int ActualEffectTimes => actualEffectTimes;

        public void Trig()
        {
            isTrig = true;
        }
    }
}
