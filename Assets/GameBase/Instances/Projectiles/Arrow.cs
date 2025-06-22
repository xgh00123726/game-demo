using GameBase.Resources;
using GameBase.Effects;

namespace GameBase.Projectile
{
    public class Arrow : GProjectile
    {
        DestIndicator _destIndicator;
        protected override void OnEmit()
        {
            _destIndicator = PrefabMgr.Instance.GetFromPool<DestIndicator>(PrefabType.Effect);
            _destIndicator.transform.position = Dest;
        }

        protected override void OnStop()
        {
            //var explore = PrefabMgr.Instance.GetFromPool<Explore>();
            //explore.transform.position = transform.position;
            PrefabMgr.Instance.ReleaseToPool(_destIndicator);
        }
    }
}
