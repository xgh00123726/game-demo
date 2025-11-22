using Constructor.Projectiles;
using GameBase.Creatures;
using GameBase.Flyings;
using GameBase.Projectiles;
using GameBase.Spells;

namespace Constructor.Spells
{
    public class MShot : ModifyableAction
    {
        public MShotData Data { get; set; }
        protected override Projectile GenProjectile(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.Speller is Creature c)
            {
                var p = ProjectileYamlFactory.Instance.GetFromData(Data.Projectile);
                p.Trigger.TargetCamp = (Camp)spell.TargetCamp.ToUint();
                p.Owner = c;
                p.Flying.Dir = spell.CastPosition - c.Position;
                p.Flying.Dest = spell.CastPosition;

                return p;
            }

            return null;
        }
    }
}
