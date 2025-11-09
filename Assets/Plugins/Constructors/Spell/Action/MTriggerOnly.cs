using Constructor.Projectiles;
using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.Resources;
using GameBase.Spells;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action
{
    /// <summary>
    /// 只有一个触发器
    /// </summary>
    public class MTriggerOnly : ModifyableAction
    {
        protected override bool CastAction(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c)
            {
                var t = TriggerSys.Instance.NewEntity();
                t.trigStyle = TrigStyle.Once;
                t.hasWhite = data.projectile.hasWhite;
                t.attach = c;
                t.owner = c;
                var damage = data.damage + c.modifyables["damage"].Value * data.ampFactor;
                t.action = new Damage(damage);
                t.shape = ProjectileFactory.Instance.GetShape(data.projectile.shape);

                Vector3 position = c.Position;
                float size = t.shape.Size;
                Vector3 scale = new Vector3(size, 1, size);
                Vector3 dir = spell.castPosition - c.Position;

                EffectSys.Instance.PlayAtPSD(data.projectile.trigEffectName, c.Position, scale, dir);

                t.OnTrig += () =>
                {
                    t.shape.Center = new Vector2(c.Position.x, c.Position.z);
                    var dir = spell.castPosition - c.Position;
                    t.shape.Dir = new Vector2(dir.x, dir.z);
                };

                return true;
            }

            return false;
        }
    }
}
