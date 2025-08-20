using GameBase.EntitySystem;
using System;

namespace GameBase.GEffects
{
    public class GEffect<T_Owner, T_Target> : IEntity,
        IPoolable
    {
        public bool trigOnGet;
        public float interval;
        public int maxTrigTimes;
        public T_Owner owner;
        public T_Target target;
        public Action<T_Owner, T_Target> EffectAction { set; protected internal get; }
        public Action EffectCallback { set; protected internal get; }

        internal float lastTrig;
        internal int trigedTimes;

        public int InstanceID { get; set; }

        public virtual void AfterGet()
        {
            trigOnGet = true;
            trigedTimes = 0;
            maxTrigTimes = 1;
        }

        public virtual void BeforeRelease()
        {
            owner = default;
            target = default;
            EffectAction = null;
            interval = 0;
        }
    }
}
