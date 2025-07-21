using System;
using GameBase.Projectile;
namespace GameBase.Spell
{
    public class TracerArrow : SpellWithCircleIndicator
    {
        public delegate void TracerCastAction(TracerArrow tracerArrow);
        public TracerCastAction CastAction;
        public IProjectileTarget target;
        public float damage;

        public TracerArrow(ISpeller speller, IIndicatorCircleSpell indicator) : base(speller, indicator)
        {
            coolingTimeSet = 1f;
        }

        protected override void OnCast()
        {
            base.OnCast();
            CastAction?.Invoke(this);

            if (_speller is IProjectileOwner owner)
            {
                var projectile = ProjectileMgr<GameBase.Projectile.TracerArrow>.Instance.CreateProjectile(owner);
                projectile.Target = target;
                projectile.damage = damage;
            }

        }
    }
}
