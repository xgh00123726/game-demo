namespace GameBase.Projectile
{
    public class ImmediateProjectile : Projectile
    {
        public float range = 1f;

        protected override void OnHit()
        {
            
        }

        protected internal override bool JugRelease()
        {
            _destoryReson = DestoryReson.Hit;
            return true;
        }
    }
}
