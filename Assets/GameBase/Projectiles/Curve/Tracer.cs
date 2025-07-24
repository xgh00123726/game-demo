using UnityEngine;

namespace GameBase.Projectile
{
    public class Tracer : CurveBase
    {
        public float turnSpeed = 1f;
        public float turnAcc = 3f;
        public Tracer(ProjectileObject projectile) : base(projectile)
        {
        }

        public override void DirUpdate()
        {
            Vector3 currDir = _projectile.Dir;
            Vector3 expectDir = _projectile.Dest - _projectile.transform.position;
            _projectile.Dir = Vector3.Lerp(currDir, expectDir, Mathf.Clamp01(_projectile.LifeTime) * turnSpeed);
            turnSpeed += turnAcc * Time.deltaTime;
        }
    }
}
