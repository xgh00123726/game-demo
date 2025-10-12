using Constructor.Spells.Interactive;
using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Math;
using GameBase.Spells;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action.Modifyables
{
    public struct MTriggerOnHitData
    {
        public Flyings.Type flyingType;
        public int flyingID;
        public TargetSetType targetSetType;
        public float radius;
        public float damage;
        public float ampFactor;
        public float xOffset;
        public float yOffset;
        public float zOffset;
    }
    public class MTriggerOnHit : ISpellAction
    {
        public MTriggerOnHitData data;

        public bool CastAction(Spell spell)
        {
            if (spell.speller is Creature c &&
                spell.interactive is DotExternalSet interactive)
            {
                var f = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
                f.Src = c.HandPosition + new Vector3(data.xOffset, data.yOffset, data.zOffset);
                f.target = new FixedFlyingTarget()
                {
                    Position = interactive.position,
                };
                f.OnHit += () =>
                {
                    var t = TriggerSys.Instance.NewEntity();
                    t.attach = f;
                    t.shape = new GMath.Circle()
                    {
                        c = new Vector2(f.target.Position.x, f.target.Position.z),
                        r = data.radius
                    };
                    t.targetsSet = TargetSetFactorary.Get(data.targetSetType);
                    t.owner = c;
                    var damage = data.damage + c.modifyables["damage"].Value * data.ampFactor;
                    t.action = new Damage(damage);
                    t.Trig();
                };

                return true;
            }

            return false;
        }
    }
    public class MTriggerOnHitCon : BaseConstructor<MTriggerOnHitData, MTriggerOnHit, MTriggerOnHitCon>
    {
        protected override string RelativePath => "Spell/Action/MTriggerOnHit.csv";

        protected override MTriggerOnHit Get()
        {
            return new MTriggerOnHit();
        }

        protected override void Set(MTriggerOnHit e, in MTriggerOnHitData data)
        {
            e.data = data;
        }
    }
}
