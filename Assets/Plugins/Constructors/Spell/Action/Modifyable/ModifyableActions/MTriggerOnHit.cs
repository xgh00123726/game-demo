using Constructor.Spells.Interactive;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Spells;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action.Modifyables
{
    public class MTriggerOnHit : ISpellAction
    {
        public TriggerOnHitData data;

        public bool CastAction(Spell spell)
        {
            var speller = spell.speller as ITriggerOwner;
            var interactive = spell.interactive as IDotInput;
            if (speller != null && interactive != null)
            {
                var f = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
                f.Src = speller.HandPosition + new Vector3(data.xOffset, data.yOffset, data.zOffset);
                f.target = new FixedFlyingTarget()
                {
                    Position = interactive.Position,
                };
                f.OnHit += () =>
                {
                    var t = Triggers.Factory.Instance.Get(data.triggerType, data.triggerID);
                    t.shape.Center = new Vector2(f.target.Position.x, f.target.Position.z);
                    t.owner = speller;
                    t.Trig();
                };

                return true;
            }

            return false;
        }
    }
    public class MTriggerOnHitCon : BaseConstructor<TriggerOnHitData, MTriggerOnHit, MTriggerOnHitCon>
    {
        protected override string RelativePath => "Spell/Action/TriggerOnHit.csv";

        protected override MTriggerOnHit Get()
        {
            return new MTriggerOnHit();
        }

        protected override void Set(MTriggerOnHit e, in TriggerOnHitData data)
        {
            e.data = data;
        }
    }
}
