using GameBase.Modify;
using GameBase.Tools;
using System;
using GameBase.EntitySystem;
using UnityEngine;

namespace GameBase.Buffs
{
    public class Buff : IPoolable
    {
        internal float durationRemain;
        internal float instantiateTime;
        internal int stackNum = 1;
        internal bool alive;
        internal IBuffOwner owner;
        internal bool isInfiDuration;

        public int id;
        public float durationSet;
        public BuffModifyers modifyers = new ();

        public bool ALive => alive;
        public float DurationRemain => durationRemain;

        void IPoolable.AfterGet()
        {
            isInfiDuration = false;
            alive = true;
            durationRemain = 0;
            instantiateTime = Time.time;
        }

        void IPoolable.BeforeRelease()
        {
            alive = false;
            owner = null;
        }

        public void AddTo(IBuffOwner owner, float duration = 10)
        {
            durationSet = duration;
            durationRemain = duration;
            this.owner = owner;
            owner.OnGetBuff(this);

            foreach (var em in modifyers.FixedModifyers)
            {
                owner.Modifyables.ModifySet(em.Key, em.Value);
            }
            foreach (var em in modifyers.SetModifyers)
            {
                owner.Modifyables.ModifySetPer(em.Key, em.Value);
            }
            foreach (var em in modifyers.CurrModifyers)
            {
                owner.Modifyables.ModifySumPer(em.Key, em.Value);
            }
        }

        public void Remove()
        {
            BuffSys.Instance.RemoveBuff(this);
        }
    }
}
