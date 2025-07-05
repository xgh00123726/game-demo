using GameBase.Resources;
using GameBase.Effects;

namespace GameBase.Projectile
{
    public class Arrow : CurveProjectile
    {
        DestIndicator _destIndicator;
        protected override void OnEmit()
        {
            _destIndicator = PrefabMgr.GetFromPool<DestIndicator>(PrefabType.Effect);
            _destIndicator.transform.position = _dest;
        }

        protected override void OnRelease()
        {
            ExploreMgr.InvokeExplore(_dest);
            PrefabMgr.ReleaseToPool(_destIndicator);
        }
    }
}
