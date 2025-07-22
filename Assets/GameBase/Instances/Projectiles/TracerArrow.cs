namespace GameBase.Projectile
{
    public class TracerArrow : ProjectileObject
    {
        protected void Awake()
        {
            curve = new Tracer(this);
            curve.speed = 15f;
        }
    }
}
