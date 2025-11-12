using Constructor.Projectiles;
using GameBase.Creatures;
using GameBase.Flyings;
using GameBase.Projectiles;
using GameBase.Spells;

namespace Constructor.Spells
{
    public class MShot : ModifyableAction
    {
        protected override Projectile GenProjectile(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c)
            {
                var p = ProjectileFactory.Instance.GetFromData(data.projectile);
                p.Owner = c;
                p.flying.Dir = spell.castPosition - c.Position;
                p.flying.dest = spell.castPosition;

                return p;
            }

            return null;
        }
    }
}
