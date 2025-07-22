using GameBase.Effects;
using GameBase.Entity;
using Logger = GameBase.Tools.Logger;
namespace GameBase.Projectile
{
    public class Explore : ProjectileObject
    {
        protected override void OnHit()
        {
            var entities = EntityMgr.EntityWithin(Dest, radius);
            foreach (var entity in entities)
            {
                entity.GetDamage(damage);
            }
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            ExploreMgr.InvokeExplore(Dest);
        }
    }
}
