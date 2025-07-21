using GameBase.Effects;

namespace GameBase.Projectile
{
    public class ExploreArrow : Arrow
    {
        protected override void Awake()
        {
            base.Awake();
            _curve = new ParabolicTimeLimit(this);
            _curve.speed = 15f;
        }
        protected override void OnRelease()
        {
            base.OnRelease();
            ExploreMgr.InvokeExplore(Dest);
        }
    }
}
