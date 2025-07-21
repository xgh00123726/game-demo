using UnityEngine;
using GameBase.Projectile;
using static UnityEngine.GraphicsBuffer;

namespace GameBase.Spell
{
    public class Explore : SpellWithCircleIndicator
    {
        public delegate void ExploreCastAction(Explore tracerArrow);
        public ExploreCastAction CastAction;
        public float damage;
        public Vector3 dest;

        public Explore(ISpeller speller, IIndicatorCircleSpell indicator) : base(speller, indicator)
        {
            coolingTimeSet = 3f;
        }

        protected override void OnCast()
        {
            base.OnCast();
            CastAction?.Invoke(this);

            if (_speller is IProjectileOwner owner)
            {
                var projectile = ProjectileMgr<GameBase.Projectile.Explore>.Instance.CreateProjectile(owner);
                projectile.range = Radius;
                projectile.Dest = dest;
                projectile.damage = damage;
            }
        }
    }
}
