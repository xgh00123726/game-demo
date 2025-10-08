using Constructor.Triggers;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.GCamera;
using GameBase.Modify;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.Triggers;
using NReco.Csv;
using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Constructor.Spells.Action
{
    public struct NearestTargetData
    {
        public Triggers.Type triggerType;
        public int triggerID;
        public Flyings.Type flyingType;
        public int flyingID;
        public TargetSetType targetSetType;
    }

    public class NearestTarget : ISpellAction
    {
        public NearestTargetData data;
        bool ISpellAction.CastAction(Spell spell)
        {
            var mOwner = spell.speller as IModifieder;
            if (mOwner == null)
            {
                XLogger.Instance.Log("owner null");
                return false;
            }
            if (!mOwner.Modifyables.ContainsKey("attackRange"))
            {
                XLogger.Instance.Log("no attack range");
                return false;
            }
            float attackRange = mOwner.Modifyables["attackRange"];
            Vector3 center = new Vector3(spell.speller.Position.x, 0, spell.speller.Position.z);

            var target = TargetSetFactorary.Get(data.targetSetType).NearestTarget(center, attackRange);
            if (target == null)
            {
                return false;
            }

            var pOwner = spell.speller as ITriggerOwner;

            var t = Triggers.Factory.Instance.Get(data.triggerType, data.triggerID);
            t.owner = pOwner;
            t.target = target;

            var f = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
            f.Src = pOwner.HandPosition + new Vector3(0, 1, 0);
            f.target = new FixedFlyingTarget()
            {
                Position = target.Center,
            };
            f.OnHit = t.Trig;
            f.curve.DirInit();
            f.releaseDistance += t.target.Radius;

            return true;
        }
    }

    public class NearestTargetCon : BaseConstructor<NearestTargetData, NearestTarget, NearestTargetCon>
    {
        protected override string RelativePath => "Spell/Action/NearestTarget.csv";

        protected override NearestTarget Get()
        {
            return new NearestTarget();
        }

        protected override void Set(NearestTarget e, in NearestTargetData data)
        {
            e.data = data;
        }
    }
}
