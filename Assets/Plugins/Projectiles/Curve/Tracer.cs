using UnityEngine;

namespace GameBase.Projectile
{
    public class Tracer : CurveBase
    {
        public float turnSpeed = 1f;
        public float turnAcc = 3f;
        public Tracer(ICurveProjectile projectile) : base(projectile)
        {
        }

        public override void DirUpdate()
        {
            Vector3 currDir = _projectile.Dir;
            Vector3 expectDir = _projectile.Dest - _projectile.Position;
            var tarDir = Vector3.Lerp(currDir, expectDir, Mathf.Clamp01(_projectile.LifeTime) * turnSpeed);
            _projectile.Dir = tarDir;
            turnSpeed += turnAcc * Time.deltaTime;
        }
    }
}
