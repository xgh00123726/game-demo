using GameBase.Effects;
using GameBase.Entity;
using Logger = GameBase.Tools.Logger;
namespace GameBase.Projectile
{
    public class Explore : AOEProjectile
    {
        protected override void BeforeHit()
        {
            foreach (var entity in EntityMgr.EntityWithin(Dest, radius, EntityMgr.OnlyEnemy))
            {
                AddTarget(entity);
            }
        }

        protected override void OnInstantiate()
        {
            base.OnInstantiate();
            isImmediatly = true;
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            ExploreMgr.InvokeExplore(Dest);
        }
    }
}
