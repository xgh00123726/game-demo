using GameBase.EntitySystem;
using GameBase.Math;
using System.Collections.Generic;

namespace GameBase.Triggers
{ 
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
        public bool hasWhite;

        internal bool isTrig;
        internal int actualEffectTimes;
        internal HashSet<ITriggerTarget> whites;

        public void Trig()
        {
            isTrig = true;
        }

        void IPoolable.AfterGet()
        {
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
