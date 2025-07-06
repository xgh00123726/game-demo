namespace GameBase.Projectile
{
    public class TracerProjectile : CurveProjectile
    {
        protected override void Awake()
        {
            base.Awake();
            _curve = new Tracer(this);
        }
    }
}
