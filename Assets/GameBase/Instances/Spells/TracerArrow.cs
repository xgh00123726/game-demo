using System;
using GameBase.Projectile;
namespace GameBase.Spell
{
    public class TracerArrow : GSpell
    {
        public delegate void TracerCastAction(TracerArrow tracerArrow);
        public IProjectileTarget target;


        protected override void OnCast()
        {
            base.OnCast();
            CastAction?.Invoke(this);

            if (_speller is IProjectileOwner owner)
            {
                new Projectile<GameBase.Projectile.TracerArrow>()
                {
                    target = target,
                    damage = effective,
                    owenr = owner,
                }.SetAttr();
            }

        }
    }
}
