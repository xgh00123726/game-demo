using UnityEngine;
using GameBase.Projectile;
using static UnityEngine.GraphicsBuffer;

namespace GameBase.Spell
{
    public class Explore : GSpell
    {
        protected override void OnCast()
        {
            base.OnCast();

            if (_speller is IProjectileOwner owner)
            {
                new Projectile<GameBase.Projectile.Explore>()
                {
                    isImmediatly = true,
                    radius = radius,
                    dest = dest,
                    damage = effective,
                    owenr = owner
                }.SetAttr();
            }
        }
    }
}
