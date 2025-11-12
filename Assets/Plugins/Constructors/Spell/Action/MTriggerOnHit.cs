using Constructor.Flyings;
using Constructor.Projectiles;
using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Math;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action
{
    public class MTriggerOnHit : ModifyableAction
    {
        protected override Projectile GenProjectile(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c)
            {
                var p = ProjectileFactory.Instance.GetFromData(data.projectile);
                var f = p.flying;
                f.Src = c.HandPosition + data.flyingSrcOffset;
                f.target = new FixedFlyingTarget()
                {
                    Position = spell.castPosition,
                };
                var t = p.trigger;
                t.owner = c;
                t.targetCamp = (Camp)spell.targetCamp.ToUint();
                var damage = data.damage + c.modifyables["damage"].Value * data.ampFactor;
                t.action = new Damage(damage);

                return p;
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
