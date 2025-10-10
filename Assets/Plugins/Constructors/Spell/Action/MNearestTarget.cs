using Constructor.Spells.Interactive;
using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.GCamera;
using GameBase.Modify;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.Triggers;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Spells.Action.Modifyables
{
    public struct MNearestTargetData
    {
        public Flyings.Type flyingType;
        public int flyingID;
        public TargetSetType targetSetType;
        public float damage;
        public float ampFactor;
    }

    public class MNearestTarget : ModifyableAction
    {
        public MNearestTargetData data;

        //protected override bool IsCast(Spell spell)
        //{
        //    _mOwner = spell.speller as IModifieder;

        //    if (_mOwner == null)
        //    {
        //        return false;
        //    }

        //    _pOwner = spell.speller as ITriggerOwner;
        //    if (_pOwner == null)
        //    {
        //        return false;
        //    }

        //    if (!_mOwner.Modifyables.ContainsKey("attackRange"))
        //    {
        //        return false;
        //    }
        //    _attackRange = _mOwner.Modifyables["attackRange"];

        //    Vector3 center = new Vector3(spell.speller.Position.x, 0, spell.speller.Position.z);
        //    _target = TargetSetFactorary.Get(data.targetSetType).NearestTarget(center, _attackRange);
        //    if (_target == null)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //protected override Flying GenFlying(Spell spell, float angleOffset, float flyingDistanceModify)
        //{
        //    var f = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
        //    f.Src = _pOwner.HandPosition + new Vector3(0, 1, 0);
        //    Vector3 dir = (_target.Center - f.Src).normalized;
        //    Quaternion rotate = Quaternion.Euler(0, angleOffset, 0);
        //    f.curve.DirInit(rotate * dir);
        //    f.target = new FixedFlyingTarget()
        //    {
        //        Position = _target.Center,
        //    };

        //    return f;
        //}

        protected override bool CastAction(Spell spell, in ModifyableModifyData modifyData)
        {
            if (spell.speller is Creature c)
            {
                float attackRange = c.modifyables["attackRange"];

                var center = new Vector3(c.Position.x, 0, c.Position.z);
                var target = TargetSetFactorary.Get(data.targetSetType).NearestTarget(center, attackRange);
                if (target != null)
                {
                    var t = TriggerSys.Instance.NewEntity();
                    t.owner = c;
                    t.target = target;

                    var damage = data.damage + c.modifyables["damage"] * data.ampFactor;
                    t.action = new Damage(damage);

                    var f = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
                    f.Src = c.HandPosition;
                    f.target = new FixedFlyingTarget()
                    {
                        Position = target.Center,
                    };
                    f.OnHit = t.Trig;
                    f.curve.DirInit();
                    f.arriveDis += t.target.Radius;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public class MNearestTargetCon : BaseConstructor<MNearestTargetData, MNearestTarget, MNearestTargetCon>
    {
        protected override string RelativePath => "Spell/Action/MNearestTarget.csv";

        protected override MNearestTarget Get()
        {
            return new MNearestTarget();
        }

        protected override void Set(MNearestTarget e, in MNearestTargetData data)
        {
            e.data = data;
        }
    }
}
