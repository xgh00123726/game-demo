using UnityEngine;
using GameBase.Tools;
using GameBase.Projectile;
using Logger = GameBase.Tools.Logger;

namespace GameBase.Spell
{
    public class Explore : GSpell
    {
        protected override void OnCast()
        {
            base.OnCast();

            if (_speller is IProjectileOwner owner)
            {
                var proj = ProjectileMgr<GameBase.Projectile.Explore>.Instance.CreateProjectile();
                proj.Owner = owner;
                proj.radius = radius;
                proj.damage = effective;
                proj.Dest = dest;
            }
        }
    }
}
