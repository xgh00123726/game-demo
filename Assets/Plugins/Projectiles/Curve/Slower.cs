using UnityEngine;

namespace GameBase.Projectile
{
    public class Slower : Linear
    {
        private float speedActual;
        public float factor = 0f;
        public float slowDis = 2f;
        public Slower(ICurveProjectile projectile) : base(projectile)
        {
        }

        public override void PosUpdate()
        {
            float disToDest = (_projectile.Position - _projectile.Dest).magnitude;

            if (disToDest < slowDis)
            {
                speedActual = (factor + disToDest) / (factor + slowDis) * speed;
            }
            else
            {
                speedActual = speed;
            }

                Vector3 delta = _projectile.Dir * speedActual * Time.deltaTime;
            float deltaMag = delta.magnitude;

            if (disToDest < deltaMag)
            {
                _projectile.Position = _projectile.Dest;
            }
            else
            {
                _projectile.Position = _projectile.Position + delta;
            }
        }
    }
}
