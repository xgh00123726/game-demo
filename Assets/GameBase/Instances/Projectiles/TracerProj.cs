namespace GameBase.Projectile
{
    public class TracerProj : ProjectileObject
    {
        protected void Awake()
        {
            curve = new Tracer(this);
            curve.speed = 15f;
        }
    }
}
