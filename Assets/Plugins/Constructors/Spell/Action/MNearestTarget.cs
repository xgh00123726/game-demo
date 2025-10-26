using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Spells;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action
{
    public struct MNearestTargetData
    {
        public Flyings.Type flyingType;
        public int flyingID;
        public TargetSetType targetSetType;
        public int slotNum;
        public float damage;
        public float ampFactor;
    }

    /// <summary>
    /// 召唤单体射弹，射弹自动追踪最近的敌人
    /// </summary>
    public class MNearestTarget : ModifyableAction
    {
        public MNearestTargetData data;

        protected override Flying GenFlying(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c)
            {
                float attackRange = c.modifyables["attackRange"].Value;

                var center = new Vector3(c.Position.x, 0, c.Position.z);
                var target = TargetSetFactorary.Get(data.targetSetType).NearestTarget(center, attackRange);
                if (target != null)
                {
                    var t = TriggerSys.Instance.NewEntity();
                    t.owner = c;
                    t.target = target;

                    var damage = data.damage + c.modifyables["damage"].Value * data.ampFactor;
                    t.action = new Damage(damage);

                    var f = Flyings.FlyingFactory.Instance.Get(data.flyingType, data.flyingID);
                    f.Src = c.HandPosition;
                    f.target = new FixedFlyingTarget()
                    {
                        Position = target.Center,
                    };
                    f.OnHit = t.Trig;
                    f.arriveDis += t.target.Radius;

                    return f;
                }
            }

            return null;
        }
    }

    public class MNearestTargetCon : SealedConstructor<MNearestTargetData, MNearestTarget, MNearestTargetCon>
    {
        protected override string RelativePath => "Spell/Action/MNearestTarget.csv";

        protected override MNearestTarget GetFromData(in MNearestTargetData data)
        {
            var e = new MNearestTarget()
            {
                Size = data.slotNum
            };
            e.data = data;
            return e;
        }
    }
}
