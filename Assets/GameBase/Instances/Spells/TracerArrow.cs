using System;
using GameBase.Projectile;
namespace GameBase.Spell
{
    public class TracerArrow : GSpell
    {
        public IProjectileTarget target;


        protected override void OnCast()
        {
            base.OnCast();
        }
    }
}
