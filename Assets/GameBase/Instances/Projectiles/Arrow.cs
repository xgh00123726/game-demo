using GameBase.Resources;
using GameBase.Effects;

namespace GameBase.Projectile
{
    public class Arrow : CurveProjectile
    {
        DestIndicator _destIndicator;
        protected override void OnEmit()
        {
            _destIndicator = PoolablePrefabMgr.GetFromPool<DestIndicator>(PrefabType.Effect);
            _destIndicator.transform.position = Dest;
        }

        protected override void OnRelease()
        {
            PoolablePrefabMgr.ReleaseToPool(_destIndicator);
        }
    }
}
