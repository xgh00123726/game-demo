using GameBase.Effects;
using GameBase.Entity;
using Logger = GameBase.Tools.Logger;
namespace GameBase.Projectile
{
    public class Explore : ImmediateProjectile
    {
        protected override void OnHit()
        {
            var entities = EntityMgr.EntityWithin(Dest, range);
            Logger.Instance.Log($"entities:{entities}, nums:{entities.Count}");
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
