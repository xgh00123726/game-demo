using Constructor.Triggers;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.GCamera;
using GameBase.Modify;
using GameBase.Triggers;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;
using UnityEngine;
using Constructor.Spells.Interactive;

namespace Constructor.Spells.Action.Modifyables
{
    public class MNearestTarget : ModifyableAction
    {
        private IModifieder _mOwner;
        private float _attackRange;
        private ITriggerTarget _target;
        private ITriggerOwner _pOwner;

        public NearestTargetData data;



        protected override bool IsCast(Spell spell)
        {
            _mOwner = spell.speller as IModifieder;

            if (_mOwner == null)
            {
                return false;
            }

            _pOwner = spell.speller as ITriggerOwner;
            if (_pOwner == null)
            {
                return false;
            }

            if (!_mOwner.Modifyables.ContainsKey("attackRange"))
            {
                return false;
            }
            _attackRange = _mOwner.Modifyables["attackRange"];

            Vector3 center = new Vector3(spell.speller.Position.x, 0, spell.speller.Position.z);
            _target = TargetSetFactorary.Get(data.targetSetType).NearestTarget(center, _attackRange);
            if (_target == null)
            {
                return false;
            }

            return true;
        }

        protected override Flying GenFlying(Spell spell, float angleOffset, float flyingDistanceModify)
        {
            var f = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
            f.Src = _pOwner.HandPosition + new Vector3(0, 1, 0);
            Vector3 dir = (_target.Center - f.Src).normalized;
            Quaternion rotate = Quaternion.Euler(0, angleOffset, 0);
            f.curve.DirInit(rotate * dir);
            f.target = new FixedFlyingTarget()
            {
                Position = _target.Center,
            };

            return f;
        }

        protected override Trigger GenProjectile(Flying flying)
        {
            var t = Triggers.Factory.Instance.Get(data.triggerType, data.triggerID);
            t.target = _target;
            t.attach = flying;
            flying.OnHit = t.Trig;

            return t;
        }
    }

    public class MNearestTargetCon : BaseConstructor<NearestTargetData, MNearestTarget, MNearestTargetCon>
    {
        protected override string RelativePath => "Spell/Action/NearestTarget.csv";

        protected override MNearestTarget Get()
        {
            return new MNearestTarget();
        }

        protected override void Set(MNearestTarget e, in NearestTargetData data)
        {
            e.data = data;
        }
    }
}
