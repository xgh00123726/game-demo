using Constructor.Flyings;
using Constructor.Projectiles;
using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Math;
using GameBase.Resources;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action
{
    /// <summary>
    /// ’ŸªΩ∑…––πÃ∂®æ‡¿Îµƒ…‰µØ£¨…‰µØŒ™AOE
    /// </summary>
    public class MAreaFixedDis : ModifyableAction
    {
        protected override Flying GenFlying(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c)
            {
                var p = ProjectileFactory.Instance.GetFromData(data.projectile);
                var f = p.flying;
                f.Src = c.Position;
                f.Dir = (spell.castPosition - c.Position).normalized;
                f.curveType = CurveFactory.CurveType.Vector;

                var t = p.trigger;
                t.owner = c;
                t.camp = (GameBase.Triggers.CampType)spell.camp;

                var damage = data.damage + c.modifyables["damage"].Value * data.ampFactor;
                t.action = new Damage(damage);

                return f;
            }

            return null;
        }
    }
}
