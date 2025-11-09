using Constructor.Flyings;
using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Math;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action
{
    public class MTriggerOnHit : ModifyableAction
    {
        protected override Flying GenFlying(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c)
            {
                var f = Flyings.FlyingFactory.Instance.GetFromData(data.flying);
                f.Src = c.HandPosition + data.flyingSrcOffset;
                f.target = new FixedFlyingTarget()
                {
                    Position = spell.castPosition,
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
                    t.targetsSet = CommonTargetSet.Instance;
                    t.owner = c;
                    t.maxeffectTimes = data.maxEffectTimes;
                    t.camp = (GameBase.Triggers.CampType)spell.camp;
                    var damage = data.damage + c.modifyables["damage"].Value * data.ampFactor;
                    t.action = new Damage(damage);
                    t.hitAudio = data.hitAudio;
                    t.Trig();
                };

                return f;
            }

            return null;
        }

        protected override void RecorrectFlying(Flying flying)
        {
            if (flying.target is FixedFlyingTarget fixedTar)
            {
                fixedTar.Position = fixedTar.Position + GMath.RollRandomDir(Mathf.Sin(flying.startAngleOffset) * data.flyingSrcOffset.y);
            }
        }
    }
}
