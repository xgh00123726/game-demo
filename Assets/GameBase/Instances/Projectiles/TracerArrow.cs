namespace GameBase.Projectile
{
    public class TracerArrow : FireArrow
    {
        protected override void Awake()
        {
            base.Awake();
            _curve = new Tracer(this);
            _curve.speed = 15f;
        }
    }
}
